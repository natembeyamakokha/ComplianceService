namespace Compliance.Application.UseCases.Compliance.IndividualScreening
{
    public interface IIndividualScreeningActivity
    {
        Task<IndividualScreeningResult> IndividualProcessScreeningAsync(IndividualScreeningCommand request);
    }
}
