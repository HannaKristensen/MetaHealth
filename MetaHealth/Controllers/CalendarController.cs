using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Calendar.ASP.NET.MVC5.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.Tasks.v1;
using Google.Apis.Tasks.v1.Data;
using Task = System.Threading.Tasks.Task;
using Newtonsoft.Json;
using System.Data.Entity;
using MetaHealth.Controllers;
using MetaHealth.DAL;
using MetaHealth.Models;

namespace Calendar.ASP.NET.MVC5.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        private readonly IDataStore dataStore = new FileDataStore(GoogleWebAuthorizationBroker.Folder);
        private Model db = new Model();

        #region GetCredentialForApiAsync

        private async Task<UserCredential> GetCredentialForApiAsync()
        {
            var initializer = new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = MyClientSecrets.ClientId,
                    ClientSecret = MyClientSecrets.ClientSecret,
                },
                Scopes = MyRequestedScopes.Scopes,
            };
            var flow = new GoogleAuthorizationCodeFlow(initializer);

            var identity = await HttpContext.GetOwinContext().Authentication.GetExternalIdentityAsync(
                DefaultAuthenticationTypes.ApplicationCookie);
            var userId = identity.FindFirstValue(MyClaimTypes.GoogleUserId);

            var token = await dataStore.GetAsync<TokenResponse>(userId); ;
            return new UserCredential(flow, userId, token);
        }

        #endregion GetCredentialForApiAsync

        //loads the entire dashboard

        #region UpcomingEvents()

        // GET: For the Home Page
        public async Task<ActionResult> UpcomingEvents()
        {
            string curUser = User.Identity.GetUserId();
            const int MaxEventsPerCalendar = 20;
            const int MaxEventsOverall = 50;
            var controller = new SepMoodsController();
            var model = new UpcomingEventsViewModel();

            var credential = await GetCredentialForApiAsync();

            var initializer = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "MetaHealth",
            };
            var service = new CalendarService(initializer);

            // Fetch the list of calendars.
            var calendars = await service.CalendarList.List().ExecuteAsync();

            // Fetch some events from each calendar.
            var fetchTasks = new List<Task<Google.Apis.Calendar.v3.Data.Events>>(calendars.Items.Count);
            foreach (var calendar in calendars.Items)
            {
                var request = service.Events.List(calendar.Id);
                request.MaxResults = MaxEventsPerCalendar;
                request.SingleEvents = true;
                request.TimeMin = DateTime.Now;
                request.TimeMax = DateTime.Now.AddDays(7.0); //maximum events shown is for 7 days
                fetchTasks.Add(request.ExecuteAsync());
            }
            var fetchResults = await Task.WhenAll(fetchTasks);

            // Sort the events and put them in the model.
            var upcomingEvents = from result in fetchResults
                                 from evt in result.Items
                                 where evt.Start != null
                                 let date = evt.Start.DateTime.HasValue ?
                                     evt.Start.DateTime.Value.Date :
                                     DateTime.ParseExact(evt.Start.Date, "yyyy-MM-dd", null)
                                 let sortKey = evt.Start.DateTimeRaw ?? evt.Start.Date
                                 orderby sortKey
                                 select new { evt, date };
            var eventsByDate = from result in upcomingEvents.Take(MaxEventsOverall)
                               group result.evt by result.date into g
                               orderby g.Key
                               select g;

            var eventGroups = new List<CalendarEventGroup>();
            foreach (var grouping in eventsByDate)
            {
                eventGroups.Add(new CalendarEventGroup
                {
                    GroupTitle = grouping.Key.ToLongDateString(),
                    Events = grouping,
                });
            }

            model.EventGroups = eventGroups;

            var initializer2 = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "MetaHealth",
            };
            var service2 = new TasksService(initializer2);

            // Define parameters of request.
            TasklistsResource.ListRequest listRequest = service2.Tasklists.List();
            listRequest.MaxResults = 10;

            string[] listOtasks = new string[10];
            // List task lists.
            IList<TaskList> taskLists = listRequest.Execute().Items;
            if (taskLists != null && taskLists.Count > 0)
            {
                int i = 0;
                foreach (var taskList in taskLists)
                {
                    listOtasks[i] = taskList.Title;
                    i++;
                }
            }

            Google.Apis.Tasks.v1.Data.Tasks tasks = service2.Tasks.List("@default").Execute();
            int amountTask = 0;
            if (tasks.Items != null)
            {
                foreach (var item in tasks.Items)
                {
                    if (item.Status == "needsAction")
                    {
                        amountTask++;
                    }
                }
            }

            string[] taskArr = new string[amountTask];
            string[] taskIDArr = new string[amountTask];
            int indexTask = 0;
            if (tasks.Items != null)
            {
                for (int i = 0; i < tasks.Items.Count; i++)
                {
                    if (tasks.Items[i].Status == "needsAction" && tasks.Items[i].Title != " ")
                    {
                        taskArr[indexTask] = tasks.Items[i].Title;
                        taskIDArr[indexTask] = tasks.Items[i].Id;
                        indexTask++;
                    }
                }
            }

            model.MultiTask = taskArr;
            model.MultiTaskID = taskIDArr;
            model.MultiList = listOtasks;
            model.MultiTask = taskArr;

            #region Populate data for graph

            Dictionary<string, double> dummyDict = new Dictionary<string, double>();
            Dictionary<string, List<int>> fakeDict = new Dictionary<string, List<int>>();
            model.SepMood = db.SepMoods.Where(n => n.UserID == curUser).OrderBy(d => d.Date).ToList();
            model.MoodDate = db.SepMoods
                .Where(n => n.UserID == curUser)
                .Select(n => DbFunctions.TruncateTime(n.Date) ?? DateTime.Now)
                .Distinct()
                .ToList();
            model.MoodNum = db.SepMoods.Where(n => n.UserID == curUser).Select(n => n.MoodNum).ToList();
            model.MoodDictionaryAvg = dummyDict;
            model.MoodDictionaryByDay = fakeDict;
            Dictionary<DateTime, List<int>> tempDictofValues = new Dictionary<DateTime, List<int>>();
            List<double> tempList = new List<double>();
            foreach (DateTime date in model.MoodDate)
            {
                tempDictofValues.Add(date, controller.GetMoodsByDate(model.SepMood, date));
                model.MoodDictionaryByDay.Add(date.Date.ToString(), controller.GetMoodsByDate(model.SepMood, date));
            }
            foreach (var list in tempDictofValues)
            {
                tempList.Add(controller.AverageDailyMood(list.Value));
            }
            Dictionary<DateTime, double> dictOfMoodAverages = new Dictionary<DateTime, double>();
            for (int i = 0; i < tempList.Count; i++)
            {
                dictOfMoodAverages.Add(model.MoodDate[i], tempList[i]);
            }
            foreach (var key in dictOfMoodAverages)
            {
                model.MoodDictionaryAvg.Add(key.Key.ToShortDateString(), key.Value);
            }

            #endregion Populate data for graph

            bool eventsOrNo = false;
            if (model.EventGroups.Count() == 0)
            {
                eventsOrNo = true;
                ViewBag.NoEvents = eventsOrNo;
            }
            else ViewBag.NoEvents = eventsOrNo;

            //Custom List
            var userId = User.Identity.GetUserId();
            model.CustomTask = db.CustomLists.Where(x => x.UserID == userId).Select(x => x.TaskTitle).ToArray();
            model.id = db.CustomLists.Where(x => x.UserID == userId).Select(x => x.UserID).ToArray();
            model.PK = db.CustomLists.Where(x => x.UserID == userId).Select(x => x.PK).ToArray();

            //JS for opening calendar
            model.EventsOrNah = await AmountOfEvents();
            model.UserName = db.AspNetUsers.Where(x => x.Id == userId).Select(x => x.UserName).FirstOrDefault();

            return View(model);
        }

        #endregion UpcomingEvents()

        //Marks OFf Tasks Through Ajax

        #region MarkDownTask()

        [HttpGet]
        public async Task<ActionResult> MarkDownTask()
        {
            string task = Request.QueryString["task"];
            string taskID = Request.QueryString["taskID"];

            var credential = await GetCredentialForApiAsync();

            var initializer = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "MetaHealth",
            };
            var service = new TasksService(initializer);

            // Define parameters of request.
            TasklistsResource.ListRequest listRequest = service.Tasklists.List();
            listRequest.MaxResults = 10;

            string[] listOtasks = new string[10];
            // List task lists.
            IList<TaskList> taskLists = listRequest.Execute().Items;
            if (taskLists != null)
            {
                int i = 0;
                foreach (var taskList in taskLists)
                {
                    listOtasks[i] = taskList.Title;
                    i++;
                }
            }

            if (taskID != null)
            {
                Google.Apis.Tasks.v1.Data.Task taskObj = service.Tasks.Get("@default", taskID).Execute();
                taskObj.Status = "completed";

                Google.Apis.Tasks.v1.Data.Task result = service.Tasks.Update(taskObj, "@default", taskID).Execute();
            }

            Google.Apis.Tasks.v1.Data.Tasks tasks = service.Tasks.List("@default").Execute();
            int amountTask = 0;
            if (tasks.Items != null)
            {
                foreach (var item in tasks.Items)
                {
                    if (item.Status == "needsAction")
                    {
                        amountTask++;
                    }
                }
            }

            string[,] taskArr = new string[2, amountTask];
            int indexTask = 0;

            if (tasks.Items != null)
            {
                for (int i = 0; i < tasks.Items.Count; i++)
                {
                    if (tasks.Items[i].Status == "needsAction" && tasks.Items[i].Title != " ")
                    {
                        taskArr[1, indexTask] = tasks.Items[i].Title;
                        taskArr[0, indexTask] = tasks.Items[i].Id;
                        indexTask++;
                    }
                }
            }

            var json = JsonConvert.SerializeObject(taskArr);

            return Content(json);
        }

        #endregion MarkDownTask()

        //Adding a new Task

        #region UpcomingEvents()

        [HttpPost]
        public async Task<ActionResult> UpcomingEvents(string taskTitle)
        {
            var credential = await GetCredentialForApiAsync();

            //Add a new task
            var initializer3 = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "MetaHealth",
            };
            var service3 = new TasksService(initializer3);

            Google.Apis.Tasks.v1.Data.Tasks tasks = service3.Tasks.List("@default").Execute();

            Google.Apis.Tasks.v1.Data.Task task = new Google.Apis.Tasks.v1.Data.Task { Title = taskTitle };

            Google.Apis.Tasks.v1.Data.Task newTask = service3.Tasks.Insert(task, "@default").Execute();

            UpcomingEventsViewModel model = await GetCurrentEventsTask();

            ModelState.Clear();

            return View(model);
        }

        #endregion UpcomingEvents()

        #region Adding Premade Tasks

        //Adding premade task Level One
        [HttpPost]
        public async Task<ActionResult> AddPreMadeOne()
        {
            var credential = await GetCredentialForApiAsync();
            var initializer3 = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "MetaHealth",
            };
            var service3 = new TasksService(initializer3);

            Google.Apis.Tasks.v1.Data.Tasks tasks = service3.Tasks.List("@default").Execute();

            Google.Apis.Tasks.v1.Data.Task task = new Google.Apis.Tasks.v1.Data.Task { Title = "Get out of bed" };
            service3.Tasks.Insert(task, "@default").Execute();

            Google.Apis.Tasks.v1.Data.Task task1 = new Google.Apis.Tasks.v1.Data.Task { Title = "Drink a glass of water" };
            service3.Tasks.Insert(task1, "@default").Execute();

            Google.Apis.Tasks.v1.Data.Task task2 = new Google.Apis.Tasks.v1.Data.Task { Title = "Eat a meal" };
            service3.Tasks.Insert(task2, "@default").Execute();

            UpcomingEventsViewModel model = await GetCurrentEventsTask();

            return View("UpcomingEvents", model);
        }

        //Adding premade task Level Two
        [HttpPost]
        public async Task<ActionResult> AddPreMadeTwo()
        {
            var credential = await GetCredentialForApiAsync();
            var initializer3 = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "MetaHealth",
            };
            var service3 = new TasksService(initializer3);

            Google.Apis.Tasks.v1.Data.Tasks tasks = service3.Tasks.List("@default").Execute();

            Google.Apis.Tasks.v1.Data.Task task = new Google.Apis.Tasks.v1.Data.Task { Title = "Take a shower" };
            service3.Tasks.Insert(task, "@default").Execute();

            Google.Apis.Tasks.v1.Data.Task task1 = new Google.Apis.Tasks.v1.Data.Task { Title = "Talk to someone" };
            service3.Tasks.Insert(task1, "@default").Execute();

            Google.Apis.Tasks.v1.Data.Task task2 = new Google.Apis.Tasks.v1.Data.Task { Title = "Brush Teeth" };
            service3.Tasks.Insert(task2, "@default").Execute();

            Google.Apis.Tasks.v1.Data.Task task3 = new Google.Apis.Tasks.v1.Data.Task { Title = "Put on Deodorant" };
            service3.Tasks.Insert(task3, "@default").Execute();

            UpcomingEventsViewModel model = await GetCurrentEventsTask();

            return View("UpcomingEvents", model);
        }

        //Adding premade task Level Three
        [HttpPost]
        public async Task<ActionResult> AddPreMadeThree()
        {
            var credential = await GetCredentialForApiAsync();
            var initializer3 = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "MetaHealth",
            };
            var service3 = new TasksService(initializer3);

            Google.Apis.Tasks.v1.Data.Tasks tasks = service3.Tasks.List("@default").Execute();

            Google.Apis.Tasks.v1.Data.Task task = new Google.Apis.Tasks.v1.Data.Task { Title = "Make your bed" };
            service3.Tasks.Insert(task, "@default").Execute();

            Google.Apis.Tasks.v1.Data.Task task1 = new Google.Apis.Tasks.v1.Data.Task { Title = "Talk to someone face to face" };
            service3.Tasks.Insert(task1, "@default").Execute();

            Google.Apis.Tasks.v1.Data.Task task2 = new Google.Apis.Tasks.v1.Data.Task { Title = "Go on a walk" };
            service3.Tasks.Insert(task2, "@default").Execute();

            Google.Apis.Tasks.v1.Data.Task task3 = new Google.Apis.Tasks.v1.Data.Task { Title = "Pick up room" };
            service3.Tasks.Insert(task3, "@default").Execute();

            UpcomingEventsViewModel model = await GetCurrentEventsTask();

            return View("UpcomingEvents", model);
        }

        #endregion Adding Premade Tasks

        // Model for the page

        #region GetCurrentEventsTask()

        public async Task<UpcomingEventsViewModel> GetCurrentEventsTask()
        {
            Dictionary<string, double> dummyDict = new Dictionary<string, double>();
            string curUser = User.Identity.GetUserId();
            var controller = new SepMoodsController();

            const int MaxEventsPerCalendar = 20;
            const int MaxEventsOverall = 50;

            var model = new UpcomingEventsViewModel();

            var credential = await GetCredentialForApiAsync();

            var initializer = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "MetaHealth",
            };
            var service = new CalendarService(initializer);

            // Fetch the list of calendars.
            var calendars = await service.CalendarList.List().ExecuteAsync();

            // Fetch some events from each calendar.
            var fetchTasks = new List<Task<Google.Apis.Calendar.v3.Data.Events>>(calendars.Items.Count);
            foreach (var calendar in calendars.Items)
            {
                var request = service.Events.List(calendar.Id);
                request.MaxResults = MaxEventsPerCalendar;
                request.SingleEvents = true;
                request.TimeMin = DateTime.Now;
                request.TimeMax = DateTime.Now.AddDays(7.0);
                fetchTasks.Add(request.ExecuteAsync());
            }
            var fetchResults = await Task.WhenAll(fetchTasks);

            // Sort the events and put them in the model.
            var upcomingEvents = from result in fetchResults
                                 from evt in result.Items
                                 where evt.Start != null
                                 let date = evt.Start.DateTime.HasValue ?
                                     evt.Start.DateTime.Value.Date :
                                     DateTime.ParseExact(evt.Start.Date, "yyyy-MM-dd", null)
                                 let sortKey = evt.Start.DateTimeRaw ?? evt.Start.Date
                                 orderby sortKey
                                 select new { evt, date };
            var eventsByDate = from result in upcomingEvents.Take(MaxEventsOverall)
                               group result.evt by result.date into g
                               orderby g.Key
                               select g;

            var eventGroups = new List<CalendarEventGroup>();
            foreach (var grouping in eventsByDate)
            {
                eventGroups.Add(new CalendarEventGroup
                {
                    GroupTitle = grouping.Key.ToLongDateString(),
                    Events = grouping,
                });
            }

            model.EventGroups = eventGroups;

            //Tasks
            var initializer3 = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "MetaHealth",
            };
            var service3 = new TasksService(initializer3);

            Google.Apis.Tasks.v1.Data.Tasks tasks = service3.Tasks.List("@default").Execute();

            int amountTask = 0;
            if (tasks.Items != null)
            {
                foreach (var item in tasks.Items)
                {
                    if (item.Status == "needsAction")
                    {
                        if (item.Id != null)
                        {
                            amountTask++;
                        }
                    }
                }
            }

            string[] taskArr = new string[amountTask];
            string[] taskIDArr = new string[amountTask];
            int indexTask = 0;
            if (tasks.Items != null)
            {
                for (int i = 0; i < tasks.Items.Count; i++)
                {
                    if (tasks.Items[i].Status == "needsAction" && tasks.Items[i].Id != null)
                    {
                        taskArr[indexTask] = tasks.Items[i].Title;
                        taskIDArr[indexTask] = tasks.Items[i].Id;
                        indexTask++;
                    }
                }
            }

            model.MultiTask = taskArr;
            model.MultiTaskID = taskIDArr;

            // Define parameters of request.
            var service2 = new TasksService(initializer3);
            TasklistsResource.ListRequest listRequest = service2.Tasklists.List();
            listRequest.MaxResults = 10;

            string[] listOtasks = new string[10];
            // List task lists.
            IList<TaskList> taskLists = listRequest.Execute().Items;
            if (taskLists != null && taskLists.Count > 0)
            {
                int i = 0;
                foreach (var taskList in taskLists)
                {
                    listOtasks[i] = taskList.Title;
                    i++;
                }
            }

            model.MultiList = listOtasks;

            #region Populate data for graph

            model.SepMood = db.SepMoods.Where(n => n.UserID == curUser).OrderBy(d => d.Date).ToList();
            model.MoodDate = db.SepMoods
                .Where(n => n.UserID == curUser)
                .Select(n => DbFunctions.TruncateTime(n.Date) ?? DateTime.Now)
                .Distinct()
                .ToList();
            model.MoodNum = db.SepMoods.Where(n => n.UserID == curUser).Select(n => n.MoodNum).ToList();
            model.MoodDictionaryAvg = dummyDict;
            Dictionary<DateTime, List<int>> tempDictofValues = new Dictionary<DateTime, List<int>>();
            List<double> tempList = new List<double>();
            foreach (DateTime date in model.MoodDate)
            {
                tempDictofValues.Add(date, controller.GetMoodsByDate(model.SepMood, date));
            }
            foreach (var list in tempDictofValues)
            {
                tempList.Add(controller.AverageDailyMood(list.Value));
            }
            Dictionary<DateTime, double> dictOfMoodAverages = new Dictionary<DateTime, double>();
            for (int i = 0; i < tempList.Count; i++)
            {
                dictOfMoodAverages.Add(model.MoodDate[i], tempList[i]);
            }
            foreach (var key in dictOfMoodAverages)
            {
                model.MoodDictionaryAvg.Add(key.Key.ToShortDateString(), key.Value);
            }

            #endregion Populate data for graph

            var userId = User.Identity.GetUserId();
            model.CustomTask = db.CustomLists.Where(x => x.UserID == userId).Select(x => x.TaskTitle).ToArray();
            model.id = db.CustomLists.Where(x => x.UserID == userId).Select(x => x.UserID).ToArray();
            model.PK = db.CustomLists.Where(x => x.UserID == userId).Select(x => x.PK).ToArray();

            //JS for opening calendar
            model.EventsOrNah = await AmountOfEvents();

            //UserName
            model.UserName = db.AspNetUsers.Where(x => x.Id == userId).Select(x => x.UserName).FirstOrDefault();

            return model;
        }

        #endregion GetCurrentEventsTask()

        #region Add Event

        //Add an event
        [HttpPost]
        public async Task<ActionResult> AddEvent(string EventSummary, string EventLocation, string EventDescription, string EventStartDate, string EventStartTime, string EventEndDate, string EventEndTime, string Remind)
        {
            Model context = new Model();
            string databaseName = context.Database.Connection.Database;

            DateTime EventStartDateTime = Convert.ToDateTime(EventStartDate).Add(TimeSpan.Parse(EventStartTime));
            DateTime EventEndDateTime = Convert.ToDateTime(EventEndDate).Add(TimeSpan.Parse(EventEndTime));

            if (EventStartDateTime > EventEndDateTime)
            {
                UpcomingEventsViewModel modelError = await GetCurrentEventsTask();
                modelError.ErrorDate = "Error";
                return View("UpcomingEvents", modelError);
            }

            var credential = await GetCredentialForApiAsync();

            var initializer = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "MetaHealth",
            };
            var calendarService = new CalendarService(initializer);

            if (calendarService != null)
            {
                var list = calendarService.CalendarList.List().Execute();
                var listcnt = list.Items;
                var calendarId = "primary";

                Google.Apis.Calendar.v3.Data.Event calendarEvent = new Google.Apis.Calendar.v3.Data.Event();

                calendarEvent.Summary = EventSummary;
                calendarEvent.Location = EventLocation;
                calendarEvent.Description = EventDescription;

                calendarEvent.Start = new Google.Apis.Calendar.v3.Data.EventDateTime
                {
                    DateTime = EventStartDateTime,
                    TimeZone = "America/Los_Angeles"
                };

                //Trying to split the time zone indicator
                calendarEvent.Start.DateTimeRaw = calendarEvent.Start.DateTimeRaw.Replace("Z", "");
                calendarEvent.End = new Google.Apis.Calendar.v3.Data.EventDateTime
                {
                    DateTime = EventEndDateTime,
                    TimeZone = "America/Los_Angeles"
                };
                calendarEvent.End.DateTimeRaw = calendarEvent.End.DateTimeRaw.Replace("Z", "");
                calendarEvent.Recurrence = new List<string>();
                if (Remind == "none")
                {
                    calendarEvent.Reminders = new Google.Apis.Calendar.v3.Data.Event.RemindersData
                    {
                        UseDefault = false
                    };
                }
                else if (Remind == "atTime")
                {
                    calendarEvent.Reminders = new Google.Apis.Calendar.v3.Data.Event.RemindersData
                    {
                        UseDefault = false,
                        Overrides = new Google.Apis.Calendar.v3.Data.EventReminder[]
                            {
                                new Google.Apis.Calendar.v3.Data.EventReminder() { Method = "email", Minutes = 0 }
                            }
                    };
                }
                else if (Remind == "5Mins")
                    calendarEvent.Reminders = new Google.Apis.Calendar.v3.Data.Event.RemindersData
                    {
                        UseDefault = false,
                        Overrides = new Google.Apis.Calendar.v3.Data.EventReminder[]
                            {
                                new Google.Apis.Calendar.v3.Data.EventReminder() { Method = "email", Minutes = 5 }
                            }
                    };
                else if (Remind == "10Mins")
                    calendarEvent.Reminders = new Google.Apis.Calendar.v3.Data.Event.RemindersData
                    {
                        UseDefault = false,
                        Overrides = new Google.Apis.Calendar.v3.Data.EventReminder[]
                            {
                                new Google.Apis.Calendar.v3.Data.EventReminder() { Method = "email", Minutes = 10 }
                            }
                    };
                else if (Remind == "30Mins")
                {
                    calendarEvent.Reminders = new Google.Apis.Calendar.v3.Data.Event.RemindersData
                    {
                        UseDefault = false,
                        Overrides = new Google.Apis.Calendar.v3.Data.EventReminder[]
                            {
                                new Google.Apis.Calendar.v3.Data.EventReminder() { Method = "email", Minutes = 30 }
                            }
                    };
                }
                else if (Remind == "oneHour")
                {
                    calendarEvent.Reminders = new Google.Apis.Calendar.v3.Data.Event.RemindersData
                    {
                        UseDefault = false,
                        Overrides = new Google.Apis.Calendar.v3.Data.EventReminder[]
                            {
                                new Google.Apis.Calendar.v3.Data.EventReminder() { Method = "email", Minutes = 60 }
                            }
                    };
                }
                else if (Remind == "twoHours")
                {
                    calendarEvent.Reminders = new Google.Apis.Calendar.v3.Data.Event.RemindersData
                    {
                        UseDefault = false,
                        Overrides = new Google.Apis.Calendar.v3.Data.EventReminder[]
                            {
                                new Google.Apis.Calendar.v3.Data.EventReminder() { Method = "email", Minutes = 120}
                            }
                    };
                }
                else if (Remind == "oneDay")
                {
                    calendarEvent.Reminders = new Google.Apis.Calendar.v3.Data.Event.RemindersData
                    {
                        UseDefault = false,
                        Overrides = new Google.Apis.Calendar.v3.Data.EventReminder[]
                            {
                                new Google.Apis.Calendar.v3.Data.EventReminder() { Method = "email", Minutes = 1440 }
                            }
                    };
                }
                else if (Remind == "twoDays")
                {
                    calendarEvent.Reminders = new Google.Apis.Calendar.v3.Data.Event.RemindersData
                    {
                        UseDefault = false,
                        Overrides = new Google.Apis.Calendar.v3.Data.EventReminder[]
                            {
                                new Google.Apis.Calendar.v3.Data.EventReminder() { Method = "email", Minutes = 2880 }
                            }
                    };
                }
                else if (Remind == "oneWeek")
                {
                    calendarEvent.Reminders = new Google.Apis.Calendar.v3.Data.Event.RemindersData
                    {
                        UseDefault = false,
                        Overrides = new Google.Apis.Calendar.v3.Data.EventReminder[]
                            {
                                new Google.Apis.Calendar.v3.Data.EventReminder() { Method = "email", Minutes = 10080 }
                            }
                    };
                }
                else
                {
                    calendarEvent.Reminders = new Google.Apis.Calendar.v3.Data.Event.RemindersData
                    {
                        UseDefault = false
                    };
                }
                var newEventRequest = calendarService.Events.Insert(calendarEvent, calendarId);
                var eventResult = newEventRequest.Execute();
            }

            ModelState.Clear();

            UpcomingEventsViewModel model = await GetCurrentEventsTask();
            return View("UpcomingEvents", model);
        }

        #endregion Add Event

        //function to make sure there are no null events in the list

        #region CheckEvents()

        public bool CheckEvents(List<string> events)
        {
            foreach (string item in events)
            {
                if (item == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        #endregion CheckEvents()

        //Adding Custom Tasks

        #region AddCustomTasks()

        [HttpGet]
        public async Task<ActionResult> AddCustomTasks()
        {
            var userId = User.Identity.GetUserId();
            string[] tasksCustom = db.CustomLists.Where(x => x.UserID == userId).Select(x => x.TaskTitle).ToArray();

            var credential = await GetCredentialForApiAsync();
            var initializer3 = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "MetaHealth",
            };
            var service3 = new TasksService(initializer3);

            for (int i = 0; i < tasksCustom.Length; i++)
            {
                Google.Apis.Tasks.v1.Data.Task task = new Google.Apis.Tasks.v1.Data.Task { Title = tasksCustom[i] };
                service3.Tasks.Insert(task, "@default").Execute();
            }

            Google.Apis.Tasks.v1.Data.Tasks tasks = service3.Tasks.List("@default").Execute();
            int amountTask = 0;
            if (tasks.Items != null)
            {
                foreach (var item in tasks.Items)
                {
                    if (item.Status == "needsAction")
                    {
                        amountTask++;
                    }
                }
            }

            string[,] taskArr = new string[2, amountTask];
            int indexTask = 0;

            if (tasks.Items != null)
            {
                for (int i = 0; i < tasks.Items.Count; i++)
                {
                    if (tasks.Items[i].Status == "needsAction" && tasks.Items[i].Title != " ")
                    {
                        taskArr[1, indexTask] = tasks.Items[i].Title;
                        taskArr[0, indexTask] = tasks.Items[i].Id;
                        indexTask++;
                    }
                }
            }

            var json = JsonConvert.SerializeObject(taskArr);

            return Content(json);
        }

        #endregion AddCustomTasks()

        #region AmountOfEvents()

        public async Task<bool> AmountOfEvents()
        {
            var credential = await GetCredentialForApiAsync();

            var initializer = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "MetaHealth",
            };
            var service = new CalendarService(initializer);

            // Fetch the list of calendars.
            var calendars = await service.CalendarList.List().ExecuteAsync();

            // Fetch some events from each calendar.
            var fetchTasks = new List<Task<Google.Apis.Calendar.v3.Data.Events>>(calendars.Items.Count);
            foreach (var calendar in calendars.Items)
            {
                var request = service.Events.List(calendar.Id);
                request.MaxResults = 2;
                request.SingleEvents = true;
                request.TimeMin = DateTime.Now;
                fetchTasks.Add(request.ExecuteAsync());
            }
            var fetchResults = await Task.WhenAll(fetchTasks);

            if (fetchResults.Length > 1)
            {
                return true;
            }
            else return false;
        }

        #endregion AmountOfEvents()

        #region Create/Editing custom tasks

        [HttpPost]
        public ActionResult CreateCustom(string titleCustom)
        {
            CustomListsController controller = new CustomListsController();
            CustomList entry = new CustomList();
            entry.TaskTitle = titleCustom;
            string userID = User.Identity.GetUserId();
            entry.UserID = userID;
            controller.Create(entry);

            CustomList[] arr = db.CustomLists.Where(x => x.UserID == userID).ToArray();
            CustomList obj = arr[arr.Length - 1];
            int pk = obj.PK;
            string task = obj.TaskTitle;
            var result = new { PK = pk, title = task };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditCustom(string customTaskContent, string task)
        {
            CustomListsController controller = new CustomListsController();
            CustomList entry = new CustomList();
            entry.PK = Int32.Parse(customTaskContent);
            entry.TaskTitle = task;
            entry.UserID = User.Identity.GetUserId();
            controller.Edit(entry);

            return Content("");
        }

        public ActionResult DeleteCustom(string customTaskContent)
        {
            CustomListsController controller = new CustomListsController();
            int key = Int32.Parse(customTaskContent);
            controller.DeleteConfirmed(key);
            return Content("");
        }

        #endregion Create/Editing custom tasks

        //Adding daily suggestion to tasks

        #region DailySugg()

        [HttpPost]
        public async Task<ActionResult> DailySugg(string sugg)
        {
            var credential1 = await GetCredentialForApiAsync();

            //Add a new task
            var initializer3 = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential1,
                ApplicationName = "MetaHealth",
            };
            var service1 = new TasksService(initializer3);

            Google.Apis.Tasks.v1.Data.Tasks tasks = service1.Tasks.List("@default").Execute();

            Google.Apis.Tasks.v1.Data.Task task = new Google.Apis.Tasks.v1.Data.Task { Title = sugg };

            Google.Apis.Tasks.v1.Data.Task newTask = service1.Tasks.Insert(task, "@default").Execute();

            var credential = await GetCredentialForApiAsync();

            var initializer = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "MetaHealth",
            };
            var service = new TasksService(initializer);

            // Define parameters of request.
            TasklistsResource.ListRequest listRequest = service.Tasklists.List();
            listRequest.MaxResults = 10;

            string[] listOtasks = new string[10];
            // List task lists.
            IList<TaskList> taskLists = listRequest.Execute().Items;
            if (taskLists != null)
            {
                int i = 0;
                foreach (var taskList in taskLists)
                {
                    listOtasks[i] = taskList.Title;
                    i++;
                }
            }

            Google.Apis.Tasks.v1.Data.Tasks tasks1 = service.Tasks.List("@default").Execute();
            int amountTask = 0;
            if (tasks1.Items != null)
            {
                foreach (var item in tasks1.Items)
                {
                    if (item.Status == "needsAction")
                    {
                        amountTask++;
                    }
                }
            }

            string[,] taskArr = new string[2, amountTask];
            int indexTask = 0;

            if (tasks1.Items != null)
            {
                for (int i = 0; i < tasks1.Items.Count; i++)
                {
                    if (tasks1.Items[i].Status == "needsAction" && tasks1.Items[i].Title != " ")
                    {
                        taskArr[1, indexTask] = tasks1.Items[i].Title;
                        taskArr[0, indexTask] = tasks1.Items[i].Id;
                        indexTask++;
                    }
                }
            }

            var json = JsonConvert.SerializeObject(taskArr);

            return Content(json);
        }

        #endregion DailySugg()

        #region QuizTask11()

        [HttpPost]
        public async Task<ActionResult> QuizTask11(string result1title1)
        {
            var credential1 = await GetCredentialForApiAsync();

            //Add a new task
            var initializer3 = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential1,
                ApplicationName = "MetaHealth",
            };
            var service1 = new TasksService(initializer3);

            Google.Apis.Tasks.v1.Data.Tasks tasks = service1.Tasks.List("@default").Execute();

            Google.Apis.Tasks.v1.Data.Task task = new Google.Apis.Tasks.v1.Data.Task { Title = result1title1 };

            Google.Apis.Tasks.v1.Data.Task newTask = service1.Tasks.Insert(task, "@default").Execute();

            return Content("");
        }

        #endregion QuizTask11()

        #region QuizTask12()

        [HttpPost]
        public async Task<ActionResult> QuizTask12(string result1title2)
        {
            var credential1 = await GetCredentialForApiAsync();

            //Add a new task
            var initializer3 = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential1,
                ApplicationName = "MetaHealth",
            };
            var service1 = new TasksService(initializer3);

            Google.Apis.Tasks.v1.Data.Tasks tasks = service1.Tasks.List("@default").Execute();

            Google.Apis.Tasks.v1.Data.Task task = new Google.Apis.Tasks.v1.Data.Task { Title = result1title2 };

            Google.Apis.Tasks.v1.Data.Task newTask = service1.Tasks.Insert(task, "@default").Execute();

            return Content("");
        }

        #endregion QuizTask12()

        #region QuizTask13()

        [HttpPost]
        public async Task<ActionResult> QuizTask13(string result1title3)
        {
            var credential1 = await GetCredentialForApiAsync();

            //Add a new task
            var initializer3 = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential1,
                ApplicationName = "MetaHealth",
            };
            var service1 = new TasksService(initializer3);

            Google.Apis.Tasks.v1.Data.Tasks tasks = service1.Tasks.List("@default").Execute();

            Google.Apis.Tasks.v1.Data.Task task = new Google.Apis.Tasks.v1.Data.Task { Title = result1title3 };

            Google.Apis.Tasks.v1.Data.Task newTask = service1.Tasks.Insert(task, "@default").Execute();

            return Content("");
        }

        #endregion QuizTask13()

        #region QuizTask21()

        [HttpPost]
        public async Task<ActionResult> QuizTask21(string result2title1)
        {
            var credential1 = await GetCredentialForApiAsync();

            //Add a new task
            var initializer3 = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential1,
                ApplicationName = "MetaHealth",
            };
            var service1 = new TasksService(initializer3);

            Google.Apis.Tasks.v1.Data.Tasks tasks = service1.Tasks.List("@default").Execute();

            Google.Apis.Tasks.v1.Data.Task task = new Google.Apis.Tasks.v1.Data.Task { Title = result2title1 };

            Google.Apis.Tasks.v1.Data.Task newTask = service1.Tasks.Insert(task, "@default").Execute();

            return Content("");
        }

        #endregion QuizTask21()

        #region QuizTask22()

        [HttpPost]
        public async Task<ActionResult> QuizTask22(string result2title2)
        {
            var credential1 = await GetCredentialForApiAsync();

            //Add a new task
            var initializer3 = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential1,
                ApplicationName = "MetaHealth",
            };
            var service1 = new TasksService(initializer3);

            Google.Apis.Tasks.v1.Data.Tasks tasks = service1.Tasks.List("@default").Execute();

            Google.Apis.Tasks.v1.Data.Task task = new Google.Apis.Tasks.v1.Data.Task { Title = result2title2 };

            Google.Apis.Tasks.v1.Data.Task newTask = service1.Tasks.Insert(task, "@default").Execute();

            return Content("");
        }

        #endregion QuizTask22()

        #region QuizTask23()

        [HttpPost]
        public async Task<ActionResult> QuizTask23(string result2title3)
        {
            var credential1 = await GetCredentialForApiAsync();

            //Add a new task
            var initializer3 = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential1,
                ApplicationName = "MetaHealth",
            };
            var service1 = new TasksService(initializer3);

            Google.Apis.Tasks.v1.Data.Tasks tasks = service1.Tasks.List("@default").Execute();

            Google.Apis.Tasks.v1.Data.Task task = new Google.Apis.Tasks.v1.Data.Task { Title = result2title3 };

            Google.Apis.Tasks.v1.Data.Task newTask = service1.Tasks.Insert(task, "@default").Execute();

            return Content("");
        }

        #endregion QuizTask23()

        #region QuizTask31()

        [HttpPost]
        public async Task<ActionResult> QuizTask31(string result3title1)
        {
            var credential1 = await GetCredentialForApiAsync();

            //Add a new task
            var initializer3 = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential1,
                ApplicationName = "MetaHealth",
            };
            var service1 = new TasksService(initializer3);

            Google.Apis.Tasks.v1.Data.Tasks tasks = service1.Tasks.List("@default").Execute();

            Google.Apis.Tasks.v1.Data.Task task = new Google.Apis.Tasks.v1.Data.Task { Title = result3title1 };

            Google.Apis.Tasks.v1.Data.Task newTask = service1.Tasks.Insert(task, "@default").Execute();

            return Content("");
        }

        #endregion QuizTask31()

        #region QuizTask32()

        [HttpPost]
        public async Task<ActionResult> QuizTask32(string result3title2)
        {
            var credential1 = await GetCredentialForApiAsync();

            //Add a new task
            var initializer3 = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential1,
                ApplicationName = "MetaHealth",
            };
            var service1 = new TasksService(initializer3);

            Google.Apis.Tasks.v1.Data.Tasks tasks = service1.Tasks.List("@default").Execute();

            Google.Apis.Tasks.v1.Data.Task task = new Google.Apis.Tasks.v1.Data.Task { Title = result3title2 };

            Google.Apis.Tasks.v1.Data.Task newTask = service1.Tasks.Insert(task, "@default").Execute();

            return Content("");
        }

        #endregion QuizTask32()

        #region QuizTask33()

        [HttpPost]
        public async Task<ActionResult> QuizTask33(string result3title3)
        {
            var credential1 = await GetCredentialForApiAsync();

            //Add a new task
            var initializer3 = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential1,
                ApplicationName = "MetaHealth",
            };
            var service1 = new TasksService(initializer3);

            Google.Apis.Tasks.v1.Data.Tasks tasks = service1.Tasks.List("@default").Execute();

            Google.Apis.Tasks.v1.Data.Task task = new Google.Apis.Tasks.v1.Data.Task { Title = result3title3 };

            Google.Apis.Tasks.v1.Data.Task newTask = service1.Tasks.Insert(task, "@default").Execute();

            return Content("");
        }

        #endregion QuizTask33()

        #region QuizTask41()

        [HttpPost]
        public async Task<ActionResult> QuizTask41(string result4title1)
        {
            var credential1 = await GetCredentialForApiAsync();

            //Add a new task
            var initializer3 = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential1,
                ApplicationName = "MetaHealth",
            };
            var service1 = new TasksService(initializer3);

            Google.Apis.Tasks.v1.Data.Tasks tasks = service1.Tasks.List("@default").Execute();

            Google.Apis.Tasks.v1.Data.Task task = new Google.Apis.Tasks.v1.Data.Task { Title = result4title1 };

            Google.Apis.Tasks.v1.Data.Task newTask = service1.Tasks.Insert(task, "@default").Execute();

            return Content("");
        }

        #endregion QuizTask41()

        #region QuizTask42()

        [HttpPost]
        public async Task<ActionResult> QuizTask42(string result4title2)
        {
            var credential1 = await GetCredentialForApiAsync();

            //Add a new task
            var initializer3 = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential1,
                ApplicationName = "MetaHealth",
            };
            var service1 = new TasksService(initializer3);

            Google.Apis.Tasks.v1.Data.Tasks tasks = service1.Tasks.List("@default").Execute();

            Google.Apis.Tasks.v1.Data.Task task = new Google.Apis.Tasks.v1.Data.Task { Title = result4title2 };

            Google.Apis.Tasks.v1.Data.Task newTask = service1.Tasks.Insert(task, "@default").Execute();

            return Content("");
        }

        #endregion QuizTask42()

        #region QuizTask43()

        [HttpPost]
        public async Task<ActionResult> QuizTask43(string result4title3)
        {
            var credential1 = await GetCredentialForApiAsync();

            //Add a new task
            var initializer3 = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential1,
                ApplicationName = "MetaHealth",
            };
            var service1 = new TasksService(initializer3);

            Google.Apis.Tasks.v1.Data.Tasks tasks = service1.Tasks.List("@default").Execute();

            Google.Apis.Tasks.v1.Data.Task task = new Google.Apis.Tasks.v1.Data.Task { Title = result4title3 };

            Google.Apis.Tasks.v1.Data.Task newTask = service1.Tasks.Insert(task, "@default").Execute();

            return Content("");
        }

        #endregion QuizTask43()
    }
}