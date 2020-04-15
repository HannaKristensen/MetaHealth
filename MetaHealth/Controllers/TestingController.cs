using System;
using System.Web.Mvc;

namespace MetaHealth.Controllers
{
    public class TestingController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public String QuizAlgorithmTest(int numOf1, int numOf2, int numOf3, int numOf4)
        {
            string result = "start";

            //Checking for result 1....
            if (numOf1 >= numOf2 && numOf1 >= numOf3 && numOf1 >= numOf4)
            {
                //tie breaker
                if (numOf1 == numOf2)
                {
                    result = "result1";
                }

                //tie breaker
                else if (numOf1 == numOf3)
                {
                    result = "result3";
                }

                //tie breaker
                else if (numOf1 == numOf4)
                {
                    result = "result4";
                }

                //this is our best case scenario where over 50% of the answers are consistentley one answer
                else
                {
                    result = "result1";
                }
            }

            //Checking for result 2...
            if (numOf2 >= numOf1 && numOf2 >= numOf3 && numOf2 >= numOf4)
            {
                //tie breaker
                if (numOf2 == numOf1)
                {
                    result = "result1";
                }

                //tie breaker
                else if (numOf2 == numOf3)
                {
                    result = "result3";
                }

                //tie breaker
                else if (numOf2 == numOf4)
                {
                    result = "result4";
                }
                else
                {
                    result = "result2";
                }
            }

            //Checking for result 3....
            if (numOf3 >= numOf2 && numOf3 >= numOf1 && numOf3 >= numOf4)
            {
                //tie breaker
                if (numOf3 == numOf2)
                {
                    result = "result2";
                }

                //tie breaker
                else if (numOf3 == numOf1)
                {
                    result = "result1";
                }

                //tie breaker
                else if (numOf3 == numOf4)
                {
                    result = "result4";
                }
                else
                {
                    result = "result3";
                }
            }

            //RESULT 4
            if (numOf4 >= numOf2 && numOf4 >= numOf3 && numOf4 >= numOf1)
            {
                //tie breaker
                if (numOf4 == numOf2)
                {
                    result = "reuslt2";
                }

                //tie breaker
                else if (numOf4 == numOf3)
                {
                    result = "result3";
                }

                //tie breaker
                else if (numOf4 == numOf1)
                {
                    result = "result4";
                }
                else
                {
                    result = "result4";
                }
            }

            return result;
        }

        public string[] CountingTasks(string[] tasks)
        {
            int amountTask = 0;
            if (tasks != null)
            {
                foreach (var item in tasks)
                {
                    if (item == "needsAction")
                    {
                        amountTask++;
                    }
                }
            }

            string[] taskArr = new string[amountTask];
            int indexTask = 0;
            if (tasks != null)
            {
                for (int i = 0; i < tasks.Length; i++)
                {
                    if (tasks[i] == "needsAction")
                    {
                        taskArr[indexTask] = tasks[i];
                        indexTask++;
                    }
                }
            }

            return (taskArr);
        }
    }
}