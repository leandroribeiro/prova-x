using System;
using Moq;
using Xunit;
using Domain;
using Domain.Services;
using Domain.Repositories;
using Domain.Entities;

namespace Domain.Tests
{
    public class TaxRepositoryTests
    {

        private readonly Mock<ITaxRepository> _repositoryMock = new Mock<ITaxRepository>();
        private readonly ITaxRepository _repository;

        public TaxRepositoryTests(){
            
            _repositoryMock.Setup(x => x.GetByName(It.IsAny<string>())).Returns(new Tax("PIS", "PIS Tax Rate", 200));
            _repositoryMock.Setup(x => x.Update(It.IsAny<Tax>())).Returns(true);
            _repository = _repositoryMock.Object;
        }   

        [Fact]
        public void ShouldToBeAbleToChargeTaxRates(){
            //Arange
            var name = "PIS";  
            var newTaxRate = 300;
            var beforeTax = _repository.GetByName(name);
            var beforeTaxRate = beforeTax.Rate;

            //Act            
            beforeTax.Rate = newTaxRate;
            var result = _repository.Update(beforeTax);            
            var afterTax = _repository.GetByName(name);

            //Assert
            Assert.True(result);

            Assert.Equal(beforeTaxRate, 200);
            Assert.Equal(beforeTax.Rate, 300);
            Assert.Equal(afterTax.Rate, 300);

            Assert.NotEqual(beforeTaxRate, afterTax.Rate);
            Assert.Equal(afterTax.Rate, newTaxRate);
        }
    
    }
}