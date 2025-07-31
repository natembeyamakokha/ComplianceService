using Compliance.Application.UseCases.SimSwap.ProcessNotifySimSwapResult;
using Compliance.Application.UseCases.VerifyLastSwapDate;
using Compliance.Domain.Entity.Interfaces;
using Compliance.Domain.Entity.SimSwapTask.Event;
using Compliance.Domain.Entity.SimSwapTasks;
using Compliance.Domain.Enum;
using Compliance.Domain.Form;
using MediatR;
using Newtonsoft.Json;
using Omni;
using Omni.CQRS.Commands;

namespace Compliance.Application.UseCases.SimSwap.ProcessVerifySimSwap;

internal sealed class ProcessVerifySimSwapCommandHandler : ICommandHandler<ProcessVerifySimSwapCommand, string>
{
    private readonly IMediator _mediator;
    private readonly ISimSwapCheckTaskRepository _swapCheckTaskRepository;
    private const string Source = "System";

    public ProcessVerifySimSwapCommandHandler(IMediator mediator, ISimSwapCheckTaskRepository swapCheckTaskRepository)
    {
        _mediator = mediator;
        _swapCheckTaskRepository = swapCheckTaskRepository;
    }

    public async Task<Result<string>> Handle(ProcessVerifySimSwapCommand command, CancellationToken cancellationToken)
    {
        var simSwapForms = command.PhoneNumbers.Select(p => new SimSwapForm
        {
            AllowedSwappedDays = command.AllowedSwappedDaysCount,
            CountryCode = command.CountryCode,
            PhoneNumber = p
        }).ToList();

        var verifyLastSwapDateCommand = new VerifyLastSwapDateMultipleCommand(simSwapForms);

        //TODO: Refine the command handler VerifyLastSwapDateCommandHandler
        var result = await _mediator.Send(verifyLastSwapDateCommand, cancellationToken);

        var serializedResponse = JsonConvert.SerializeObject(result);

        if (!result.Successful || !result.ResponseObject.Successful)
        {
            return new Error(serializedResponse, Shared.StatusCodes.INVALID_REQUEST, true);
        }

        var processNotifySimSwapResultCommand = new ProcessNotifySimSwapResultCommand(
            command.CallbackUrl,
            JsonConvert.SerializeObject(result.ResponseObject));

        var notifyResultTask = SimSwapCheckTask.Create(
            (int)SimSwapCheckTaskStatus.Pending,
            nameof(SimSwapCheckTaskStatus.Pending),
            typeof(ProcessNotifySimSwapResultCommand).FullName,
            JsonConvert.SerializeObject(processNotifySimSwapResultCommand),
            command.CountryCode.ToString(),
            command.Cif,
            Source,
            nameof(Operation.NotifySimSwapResult));

        if (notifyResultTask.HasError) 
        {
            return new Error(notifyResultTask.Error.FullMessage, Shared.StatusCodes.INVALID_REQUEST, false);
        }
        var task = notifyResultTask.Value;
        await _swapCheckTaskRepository.AddAndSaveChangesAsync(task, cancellationToken);

        task.AddSimSwapTaskEvent(new SimSwapTaskCreatedDomainEvent(task.Id,task.CreatedOn,task.TaskStatusDescription));

        return serializedResponse;
    }
}
