using MediatR;
using Compliance.Shared;
using Compliance.Domain.Enum;
using Compliance.Domain.Response;
using Compliance.Application.Contracts;
using System.Collections.Concurrent;
using Microsoft.Extensions.Configuration;
using Compliance.Application.Contracts.Handlers;

namespace Compliance.Application.UseCases.VerifyLastSwapDate;

public class VerifyLastSwapDateCommandHandler : ICommandHandler<VerifyLastSwapDateCommand, Result<SimSwapResponse>>,
                                              ICommandHandler<VerifyLastSwapDateMultipleCommand, Result<BulkSimSwapResponse>>
{
    private readonly IMediator _mediator;
    private readonly ITelcoResolver _telcoResolver;
    private readonly ISimSwapService _simSwapService;
    private readonly int _allowedSwappedDays;

    public VerifyLastSwapDateCommandHandler(ITelcoResolver telcoResolver, ISimSwapService simSwapService, IMediator mediator, IConfiguration configuration)
    {
        _mediator = mediator;
        _telcoResolver = telcoResolver;
        _simSwapService = simSwapService;
        _allowedSwappedDays = Convert.ToInt32(configuration["AllowedNumberOfSwapDays"]);
    }

    public async Task<Result<SimSwapResponse>> Handle(VerifyLastSwapDateCommand request, CancellationToken cancellationToken)
    {
        var validData = _telcoResolver.TryResolve(request.CountryCode, request.PhoneNumber, out Telco telco);
        if (!validData)
            return VerifyLastSwapDateResult.Failure(new() { PhoneNumber = request.PhoneNumber, IsSuccessful = false }, "Sorry! Sim swap check not available for this phone number");

        var result = await _simSwapService.GetLastSwappedDateAsync(request.PhoneNumber, request.CountryCode, telco, _allowedSwappedDays);

        if (!result.Successful)
            return VerifyLastSwapDateResult.Failure(new() { PhoneNumber = request.PhoneNumber, IsSuccessful = false }, result?.StatusMessage);

        result.ResponseObject.PhoneNumber = request.PhoneNumber;
        if (result.ResponseObject.LastSwap.HasValue)
            if (result.ResponseObject.LastSwap.Value.Date > DateTime.UtcNow.AddDays(-request.AllowedDaysCount))
                result.ResponseObject.IsSwaped = true;
            else
                result.ResponseObject.IsSwaped = false;
        else
            result.ResponseObject.IsSwaped = false;

        return VerifyLastSwapDateResult.Success(result.ResponseObject);
    }

    public async Task<Result<BulkSimSwapResponse>> Handle(VerifyLastSwapDateMultipleCommand request, CancellationToken cancellationToken)
    {
        var simSwapResult = new ConcurrentBag<SimSwapResponse>();

        ParallelOptions parallelOptions = new()
        {
            MaxDegreeOfParallelism = 5
        };

        await Parallel.ForEachAsync(request.SimSwapRequests, parallelOptions, async (simSwapRequest, cancellationToken) =>
        {
            var result = await _mediator.Send(new VerifyLastSwapDateCommand(simSwapRequest.PhoneNumber, simSwapRequest.CountryCodeEnum, simSwapRequest.AllowedSwappedDays), cancellationToken);
            simSwapResult.Add(result.ResponseObject);
        });

        var isAnyProviderNotReachable = simSwapResult.Any(x => !x.ApiReached);
        var simSwapChecksDidntPass = simSwapResult.Any(x => x.IsSwaped);
        
        return Result<BulkSimSwapResponse>.Success(new()
        {
            Successful = !isAnyProviderNotReachable,
            SimSwapChecksPassed = isAnyProviderNotReachable ? false : !simSwapChecksDidntPass,
            SimSwapResponse = simSwapResult.ToArray()
        });
    }
}
