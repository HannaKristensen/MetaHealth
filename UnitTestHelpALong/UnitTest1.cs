using System;
using Calendar.ASP.NET.MVC5.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MetaHealth.Controllers;
using MetaHealth.Models;
using System.Collections.Generic;

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


        [TestMethod]

        public void SelfCareQuiz_Result1_ReturnsResult1AndPass()
        {
            HomeController controller = new HomeController();

            //variables to plug in 
            //number of 1's is clearly greater than all the others 
            int num1 = 7;
            int num2 = 1;
            int num3 = 2;
            int num4 = 3;

            string expectedResult = "result1";

            string actualResult = controller.QuizAlgorithmTest(num1, num2, num3, num4);

            Assert.AreEqual(actualResult, expectedResult);

        }


        [TestMethod]

        public void SelfCareQuiz_TieBreaker_ReturnsResult1AndPass()
        {
            HomeController controller = new HomeController();

            //variables to plug in 
            //number of 1's & 2's is clearly greater than all the others 
            //tie breaker for 1 and 2s being equal is result 1 
            int num1 = 5;
            int num2 = 5;
            int num3 = 2;
            int num4 = 1;

            string expectedResult = "result1";

            string actualResult = controller.QuizAlgorithmTest(num1, num2, num3, num4);

            Assert.AreEqual(actualResult, expectedResult);

        }

        [TestMethod]

        public void GetAverageOfDailyMoods() {
            SepMoodsController controller = new SepMoodsController();
            #region really boring list set up
            SepMood sepMood1 = new SepMood(1);
            SepMood sepMood2 = new SepMood(2);
            SepMood sepMood3 = new SepMood(3);
            SepMood sepMood4 = new SepMood(4);
            SepMood sepMood5 = new SepMood(5);
            List<SepMood> listMoods = new List<SepMood>();
            listMoods.Add(sepMood1);
            listMoods.Add(sepMood2);
            listMoods.Add(sepMood3);
            listMoods.Add(sepMood4);
            listMoods.Add(sepMood5);
            #endregion
            var testValPos=controller.AverageDailyMood(listMoods, DateTime.MinValue);
            Assert.AreEqual(3, testValPos);

            //change the date on one of the sepMoods in the list
            listMoods[4].Date = DateTime.Now;
            var testValNeg = controller.AverageDailyMood(listMoods, DateTime.MinValue);
            Assert.AreNotEqual(testValNeg, 3);
        }
    }
}