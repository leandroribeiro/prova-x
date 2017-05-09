using System;
using Xunit;
using Moq;
using Domain;
using Domain.Services;
using Domain.Repositories;
using Domain.Entities;

namespace Domain.Tests
{
    public class InvoiceServiceTests
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ITaxRepository _taxRepository;

        private readonly Mock<IInvoiceRepository> _invoiceRepositoryMock = new Mock<IInvoiceRepository>();
        private readonly Mock<ITaxRepository> _taxRepositoryMock = new Mock<ITaxRepository>();

        private readonly InvoiceService _invoiceService;

        public InvoiceServiceTests()
        {
            _invoiceRepositoryMock.Setup(x => x.GetTotalAmountWithholdTaxesByCompany(1)).Returns(2000.00M);
            _invoiceRepositoryMock.Setup(x => x.GetTotalAmountWithholdTaxesByCompany(2)).Returns(3000.00M);
            _invoiceRepository = _invoiceRepositoryMock.Object;

            _taxRepositoryMock.Setup(x => x.GetByName("IR")).Returns(new Tax("IR", "IR Tax Rate", 15.00M));
            _taxRepositoryMock.Setup(x => x.GetByName("PIS")).Returns(new Tax("PIS", "PIS Tax Rate", 0.65M));
            _taxRepositoryMock.Setup(x => x.GetByName("CONFINS")).Returns(new Tax("CONFINS", "CONFIS Tax Rate", 3.0M));
            _taxRepositoryMock.Setup(x => x.GetByName("CSLL")).Returns(new Tax("CSLL", "CSLL Tax Rate", 1.0M));
            _taxRepository = _taxRepositoryMock.Object;

            _invoiceService = new InvoiceService(_invoiceRepository, _taxRepository);
        }   

        [Fact]
        public void ShouldCalculateWithholdTaxesWithoutTaxes()
        {
            //Arrange
            var company = new Company(1);
            var customer = new Customer();
            var invoice = new Invoice(company, customer);
            invoice.Ammout = 6.00M;

            //Act
            var amoutTotal = _invoiceService.Calculate(invoice);

            //Assert
            Assert.Equal(0.00M, amoutTotal);
        }

        [Fact]
        public void ShouldCalculateWithholdTaxesWithIRTaxRate()
        {
            //Arrange
            var company = new Company(1);
            var customer = new Customer();
            var invoice = new Invoice(company, customer);
            invoice.Ammout = 1000M;

            //Act
            var taxeAmmoutTotal = _invoiceService.Calculate(invoice);

            //Assert
            Assert.Equal(150M, taxeAmmoutTotal);
        }

        [Fact]
        public void ShouldCalculateWithholdTaxesWithAllTaxRate()
        {
            //Arrange
            var company = new Company(1);
            var customer = new Customer();
            var invoice = new Invoice(company, customer);
            invoice.Ammout = 10000M;

            //Act
            var taxeAmmoutTotal = _invoiceService.Calculate(invoice);

            //Assert
            Assert.Equal(1965M, taxeAmmoutTotal);
        }

        [Fact]
        public void ShouldReturnRoundTwoDigits()
        {
			//Arrange
			var company = new Company(1);
			var customer = new Customer();
			var invoice = new Invoice(company, customer);
			invoice.Ammout = 20000.98765431M;

			//Act
			var taxeAmmoutTotal = _invoiceService.Calculate(invoice);

			//Assert
			Assert.Equal(3930.19M, taxeAmmoutTotal);
        }


        [Fact]
        public void ShouldReturnTotalAmountWithholdTaxesByCompany()
        {
            var company = new Company(2);
            var amoutTotal = _invoiceService.GetTotalAmountWithholdTaxes(company);

            Assert.Equal(3000, amoutTotal);
        }
        
    }
}
