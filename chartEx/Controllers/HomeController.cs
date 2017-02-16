using chartEx.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace chartEx.Controllers
{
    public class HomeController : Controller
    {
        //GET: Index
        public ActionResult Index()
        {
            return View();
        }
        //AJAX Morris Bar Chart in Static Chart
        public ActionResult StaticData()
        {
            object[] dataArray = new object[12];
            var month = DateTime.Today.Month;
            var randomNum = new Random();
            for (var i = 0; i <= 11; i++)
            {
                dataArray[i] = new { Month = i + 1, Income = randomNum.Next(1000, 10000), Expense = randomNum.Next(1000, 10000) };
            }

            return Content(JsonConvert.SerializeObject(dataArray), "application/json");
        }
        //AJAX Morris Bar Chart in Dynamic Chart
        public ActionResult graph(AjaxEx d)
        {
            var exercise = AllLists.Ajaxexercise;
            
            if (exercise != null)
            {
                if (d.Month != null)
                {
                    if(exercise.Any(a => a.Month == d.Month))
                    {
                        exercise.Remove(d);
                    }
                    else
                    {
                        exercise.Add(d);
                    }
                    
                    exercise.ToArray();

                    ViewBag.Month = new SelectList(AllLists.listItems, "Value", "Key");
                    return Content(JsonConvert.SerializeObject(exercise), "application/json");
                }
            }

            d.Month = "January";
            d.Income = 3000;
            d.Expense = 2000;
            if (exercise.Any(a => a.Month == d.Month))
            {
                exercise.Remove(d);
            }
            else
            {
                exercise.Add(d);
            }

            exercise.ToArray();

            ViewBag.Month = new SelectList(AllLists.listItems, "Value", "Key");
            return Content(JsonConvert.SerializeObject(exercise), "application/json");
        }

        //GET: GetMe - MVC Chart in Dynamic Chart
        public ActionResult GetMe()
        {
            ViewBag.Month = new SelectList(AllLists.listItems, "Value", "Key");
            return View();
        }

        //POST: GetMe - MVC Chart in Dynamic Chart
        [HttpPost]
        public ActionResult GetMe(AjaxEx data)
        {
            var exercise = AllLists.MVCexercise;
            if (exercise != null)
            {
                if (data.Month != null)
                {
                    if (exercise.Any(a => a.Month == data.Month))
                    {
                        exercise.Remove(data);
                    }
                    else
                    {
                        exercise.Add(data);
                        ViewBag.ArrData = exercise.ToArray();

                        ViewBag.Month = new SelectList(AllLists.listItems, "Value", "Key");
                        return View();
                    }
                }
            }

            data.Month = "January";
            data.Income = 3000;
            data.Expense = 2000;
            exercise.Add(data);
            ViewBag.ArrData = exercise.ToArray();

            ViewBag.Month = new SelectList(AllLists.listItems, "Value", "Key");
            return View();
        }
       
        //Temp Lists
        public static class AllLists
        {
            public static List<AjaxEx> MVCexercise = new List<AjaxEx>();
            public static List<AjaxEx> Ajaxexercise = new List<AjaxEx>();
            public static Dictionary<string, string> listItems = new Dictionary<string, string>
            {
                {"January", "January"},
                {"February", "February"},
                {"March", "March"},
                {"April", "April"},
                {"May", "May"},
                {"June", "June"},
                {"July", "July"},
                {"August", "August"},
                {"September", "September"},
                {"October", "October"},
                {"November", "November"},
                {"December", "December"}
            };
        }

        //GET: Resources
        public ActionResult Resources()
        {
            ViewBag.Message = "Here are some resouces that you can use to build awesome charts!!";
            return View();
        }
    }
}