using System;
using Calendar.ASP.NET.MVC5.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestHelpALong
{
    [TestClass]
    public class UnitTest1
    {
        //Sorting out the needsAction Tasks. The Api returns all task and needs to be sorted
        //on which ones are done or "needsAction"
        [TestMethod]
        public void IsNotCompleteTaskSorted_TwoNeedsActionInList_Returns2AndPass()
        {
            //Arrange objects, creating and setting them up as necessary.
            CalendarController controller = new CalendarController();
            string[] tasks = { "needsAction", "completed", "needsAction", "completed" };

            //Act on an object.
            string[] result = controller.CountingTasks(tasks);

            //Assert that something is as expected.
            Assert.AreEqual(2, result.Length);
        }

        [TestMethod]
        public void IsNotCompleteTaskSorted_ZeroNeedsActionInList_ReturnsZeroAndPass()
        {
            //Arrange objects, creating and setting them up as necessary.
            CalendarController controller = new CalendarController();
            string[] tasks = { "completed", "completed", "completed", "completed" };

            //Act on an object.
            string[] result = controller.CountingTasks(tasks);

            //Assert that something is as expected.
            Assert.AreEqual(0, result.Length);
        }
    }
}