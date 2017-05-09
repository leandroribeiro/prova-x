using Domain;
using Domain.Entities;
using Domain.Repositories;

namespace Domain.Services
{
    public class InvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ITaxRepository _taxRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository, ITaxRepository taxRepository)
        {
            _invoiceRepository = invoiceRepository;
            _taxRepository = taxRepository;
        }
        public decimal GetTotalAmountWithholdTaxes(Company company)
        {
            return _invoiceRepository.GetTotalAmountWithholdTaxesByCompany(company.ID);
        }

        public decimal Calculate(Invoice invoice)
        {
            if (ApplyTax("IR", invoice.Ammout) > 10)
            {
                invoice.IRAmmoutWithhold += ApplyTax("IR", invoice.Ammout);
            }

            if (invoice.Ammout > 5000)
            {
                invoice.PISAmmoutWithhold += ApplyTax("PIS", invoice.Ammout);
                invoice.COFINSAmmoutWithhold += ApplyTax("CONFINS", invoice.Ammout);
                invoice.CSLLAmmoutWithhold += ApplyTax("CSLL", invoice.Ammout);
            }

            return invoice.TotalAmountWithholdTaxes;

        }

        private decimal ApplyTax(string taxName, decimal ammount)
        {
            var tax = _taxRepository.GetByName(taxName);

            if (tax != null)
                return tax.Apply(ammount);

            return 0;
        }

    }
}