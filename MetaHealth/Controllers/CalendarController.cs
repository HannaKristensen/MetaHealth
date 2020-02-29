/*
Copyright 2015 Google Inc
Licensed under the Apache License, Version 2.0(the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
    http://www.apache.org/licenses/LICENSE-2.0
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Calendar.ASP.NET.MVC5.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        private readonly IDataStore dataStore = new FileDataStore(GoogleWebAuthorizationBroker.Folder);

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

            var token = await dataStore.GetAsync<TokenResponse>(userId);
            return new UserCredential(flow, userId, token);
        }

        // GET: /Calendar/UpcomingEvents
        public async Task<ActionResult> UpcomingEvents()
        {
            const int MaxEventsPerCalendar = 20;
            const int MaxEventsOverall = 50;

            var model = new UpcomingEventsViewModel();

            var credential = await GetCredentialForApiAsync();

            var initializer = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "ASP.NET MVC5 Calendar Sample",
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
                ApplicationName = "ASP.NET MVC5 Calendar Sample",
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
            if (tasks != null)
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
            if (tasks != null)
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

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> MarkDownTask()
        {
            string task = Request.QueryString["task"];
            string taskID = Request.QueryString["taskID"];

            var credential = await GetCredentialForApiAsync();

            var initializer = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "ASP.NET MVC5 Calendar Sample",
            };
            var service = new TasksService(initializer);

            // Define parameters of request.
            TasklistsResource.ListRequest listRequest = service.Tasklists.List();
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

            Google.Apis.Tasks.v1.Data.Task taskObj = service.Tasks.Get("@default", taskID).Execute();
            taskObj.Status = "completed";

            Google.Apis.Tasks.v1.Data.Task result = service.Tasks.Update(taskObj, "@default", taskID).Execute();

            Google.Apis.Tasks.v1.Data.Tasks tasks = service.Tasks.List("@default").Execute();
            int amountTask = 0;
            if (tasks != null)
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

            if (tasks != null)
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
    }
}