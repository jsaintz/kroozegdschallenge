
namespace Krooze.EntranceTest.WriteHere.Structure.Services
{
    public class CalculateInstallmentsService
    {
        private const int min_value_installments =200;
        private const int max_number_of_installments = 12;

        public int GetInstallments(decimal fullPrice) =>
               fullPrice < min_value_installments ? 1 : CalculateInstallment(fullPrice);

        private int CalculateInstallment(decimal fullPrice) =>
        (GetTotalInstallments(fullPrice) > max_number_of_installments ? max_number_of_installments
            : GetTotalInstallments(fullPrice));
      

        private int GetTotalInstallments(decimal fullPrice) => (int)(fullPrice / min_value_installments);
 
    }
}
