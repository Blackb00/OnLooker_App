using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnLooker.Core;
using OnLooker.DataBaseAccess;

namespace Tests
{
    [TestClass]
    public class CRUDCurrencyPairTests
    {
        CCurrencyPairGateway _currencyPairGateway = new CCurrencyPairGateway();
        CCurrencyPair _pair;
       CCurrencyPair _pairFromDb;
        CCurrencyPair _pairChanged;
        CCurrencyGateway _currencyGateway = new CCurrencyGateway();
        CurrencyInfo _currencyBase;
        CurrencyInfo _currencyQuoted;
        CurrencyInfo _currencyQuotedForChange;



        [TestInitialize]
        public void Initialize()
        {
            CDbDeploy.ConnectDataBase();
            _currencyBase = new CurrencyInfo()
            {
                Name = "Test Currency Dollar",
                Code = "T#1"
            };
            _currencyBase.ID = _currencyGateway.Create(_currencyBase);
            _currencyGateway.Get(_currencyBase.ID);
            _currencyQuoted = new CurrencyInfo()
            {
                Name = "Test Currency Euro",
                Code = "T#2"
            };
            _currencyQuoted.ID = _currencyGateway.Create(_currencyQuoted);
            _pair = new CCurrencyPair()
            {
                BaseCurrency = _currencyBase,
                QuotedCurrency = _currencyQuoted
            };
            _currencyQuotedForChange = new CurrencyInfo()
            {
                Name = "Test Currency Euro",
                Code = "T#3"
            };
            _currencyQuotedForChange.ID = _currencyGateway.Create(_currencyQuotedForChange);
            _pairChanged = new CCurrencyPair()
            {
                BaseCurrency = _currencyBase,
                QuotedCurrency = _currencyQuotedForChange
            };


        }
        [TestMethod]
        public void ShouldCreateAndReadCurrencyPair()
        {
            //Act
            _pair.ID = _currencyPairGateway.Create(_pair);
            _pairFromDb = _currencyPairGateway.Get(_pair.ID);

            //Assert
            Assert.IsTrue(_pairFromDb.QuotedCurrency.Name == _pair.QuotedCurrency.Name);

        }

        [TestCleanup]
        public void DeleteTestData()
        {
            _currencyPairGateway.Delete(_pair.ID);

            _currencyPairGateway.Delete(_currencyQuotedForChange.ID);
            _currencyGateway.Delete(_currencyBase.ID);
            _currencyGateway.Delete(_currencyQuoted.ID);
           
           
            
        }

        [TestMethod]
        public void ShouldUpdateCurrencyPair()
        {
            //Arrange
            _pair.ID = _currencyPairGateway.Create(_pair);
            _pairChanged.ID = _pair.ID;
            //Act 
            _currencyPairGateway.Update(_pairChanged);
            _pairFromDb = _currencyPairGateway.Get(_pair.ID);
            
            //Assert
            Assert.IsFalse(_pairFromDb.QuotedCurrency.Code == _pair.QuotedCurrency.Code);

        }

        [TestMethod]
        public void ShouldDeleteCurrencyPair()
        {
            //Arrange
            _pair.ID = _currencyPairGateway.Create(_pair);

            //Act
            _currencyPairGateway.Delete(_pair.ID);
            var baseFromDeletedPair = _currencyGateway.Get(_pair.BaseCurrency.ID);

            //Assert
            Assert.IsTrue(baseFromDeletedPair.ID == _pair.BaseCurrency.ID);
            Assert.IsTrue(_currencyPairGateway.Get(_pair.ID) == null);
        }
    }
}
