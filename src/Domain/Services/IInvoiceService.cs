using Domain;
using Domain.Entities;

namespace Domain.Services
{
    public interface IInvoiceService
    {
        decimal GetTotalAmountWithholdTaxes(Company company);
    }

}