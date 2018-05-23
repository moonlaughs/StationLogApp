
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StationLogApp.Common;

namespace UnitTestStationLogApp
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
    }

    [TestClass]
    public class CollectionsTest
    {
        public Collections CollectionsClass { get; set; }

        [TestInitialize]
        public void BeforeTest()
        {
            CollectionsClass = new Collections();
        }

        [TestMethod]
        public void TestLoadToDo()
        {
            int myValue = 90;
            int numberOfItems = CollectionsClass.LoadToDo().Count;

            Assert.AreNotEqual(myValue, numberOfItems);
        }
    }
}