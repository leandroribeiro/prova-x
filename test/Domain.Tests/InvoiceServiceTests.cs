using System;
using Xunit;

namespace Domain.Tests
{
    public class InvoiceServiceTests
    {
        private readonly InvoiceService _invoiceService;

        public InvoiceServiceTests(){
            _invoiceService = new InvoiceService();
        }   

        [Fact]
        public void ShouldReturnTotalAmountWithholdTaxesByCompany()
        {
            var company = new Company(){
                ID = 1
            };
            var amoutTotal = _invoiceService.GetTotalAmountWithholdTaxes(company);

            Assert.Equal(amoutTotal, 10000);            
        }
    }
}