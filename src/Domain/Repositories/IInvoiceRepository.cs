namespace Domain.Repositories{
    
    public interface IInvoiceRepository
    {
        decimal GetTotalAmountWithholdTaxesByCompany(int companyID);
    
    }
}