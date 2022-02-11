using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CapstoneTests
{
    [TestClass]
    public class CateringTest
    {
        [TestMethod]
        public void Check_that_catering_object_is_created()
        {
            // Arrange 
            Catering catering = new Catering();

            // Act

            //Assert
            Assert.IsNotNull(catering);
        }
        [TestMethod]
        public void AddMoneyTest()
        {
            //arrange
            Catering testObject = new Catering();
            //act
            decimal result = testObject.AddMoney("100");
            //Assert
            Assert.AreEqual(100, result);
        }
        [TestMethod]
        public void CheckValidityTest()
        {
            //arrange
            Catering testObject = new Catering();
            //act
            bool result = testObject.CheckValidity("10", 100M);
            //Assert
            Assert.AreEqual(true, result);
            //arrange
            Catering testObject2 = new Catering();
            //act
            bool result2 = testObject2.CheckValidity("10", 500M);
            //Assert
            Assert.AreEqual(false, result2);
            //arrange
            Catering testObject3 = new Catering();
            //act
            bool result3 = testObject3.CheckValidity("15", 100M);
            //Assert
            Assert.AreEqual(false, result3);
            //arrange
            Catering testObject4 = new Catering();
            //act
            testObject4.AddMoney("1500");
            bool result4 = testObject4.CheckValidity("10", 0M);
            //Assert
            Assert.AreEqual(false, result4);
        }
        [TestMethod]
        public void AddToCartTest()
        {
            Catering testObject = new Catering();

            testObject.StockCatering();

            string result = testObject.AddToCart("A1", "4");

            Assert.AreEqual("Insufficient Funds", result);

            string result2 = testObject.AddToCart("F5", "4");

            Assert.AreEqual("Invalid Product Code", result2);

            testObject.AddMoney("100");

            string result3 = testObject.AddToCart("A1", "4");

            Assert.AreEqual("Added to Cart", result3);

            string result4 = testObject.AddToCart("A1", "26");

            Assert.AreEqual("Insufficient Inventory", result4);

        }
        [TestMethod]
        public void MakeChangeTest()
        {
            Catering testObject = new Catering();

            Dictionary<string, int> result2 = testObject.MakeChange();

            Assert.IsNull(result2);

            testObject.AddMoney("100");

            Dictionary<string, int> result = testObject.MakeChange();

            Dictionary<string, int> testInventory = new Dictionary<string, int>();
            testInventory["Fifties"] = 2;

            CollectionAssert.AreEquivalent(result, testInventory);

            testObject.AddMoney("20.50");

            Dictionary<string, int> result3 = testObject.MakeChange();

            Dictionary<string, int> testInventory3 = new Dictionary<string, int>();
            testInventory3["Fifties"] = 2;
            testInventory3["Twenties"] = 1;
            testInventory3["Quarters"] = 2;

            CollectionAssert.AreEquivalent(result3, testInventory3);

        }

    }
}
