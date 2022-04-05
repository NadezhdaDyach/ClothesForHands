using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ClothesForHandsMaterials.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void MaterialCount()
        {
            //arrange
            int expected = 101;
            //act
            MaterialsList f = new MaterialsList();
            int actual = f.SelectMaterial();
            //assert
            Assert.AreEqual(expected,actual);
        }

        [TestMethod]
        public void SupplierCount()
        {
            //arrange
            int expected = 50;
            //act
            MaterialsList f = new MaterialsList();
            int actual = f.SelectSupplier();
            //assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void MaterialSupplierCount()
        {
            //arrange
            int expected = 99;
            //act
            MaterialsList f = new MaterialsList();
            int actual = f.SelectMaterialSupplier();
            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MaterialTypeCount()
        {
            //arrange
            int expected = 3;
            //act
            MaterialsList f = new MaterialsList();
            int actual = f.SelectMaterialType();
            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}
