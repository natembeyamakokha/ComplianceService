namespace Compliance.Application.UseCases.IndividualScreening
{
    public interface IIndividualScreeningActivity
    {
        Task<IndividualScreeningResult> IndividualProcessScreeningAsync(IndividualScreeningCommand request);
    }
}
