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

        public TaxRepositoryTests()
        {            
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

            Assert.Equal(200, beforeTaxRate);
            Assert.Equal(300, beforeTax.Rate);
            Assert.Equal(300, afterTax.Rate);

            Assert.NotEqual(afterTax.Rate, beforeTaxRate);
            Assert.Equal(newTaxRate, afterTax.Rate);
        }
    
    }
}