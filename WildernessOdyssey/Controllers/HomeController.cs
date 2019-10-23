using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WildernessOdyssey.Models;

namespace WildernessOdyssey.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {

        private WildernessModelContainer db = new WildernessModelContainer();

        public ActionResult Index()
        {
           var resultset = db.UsersBookings
            .GroupBy(ddda => ddda.Trip.TripType).Select(n => new
            {
                Trip = n.Key,
                Count = n.Count()
            });

            List<DataPoint> dataPoints = new List<DataPoint>();
            foreach (var e in resultset) {

                dataPoints.Add(new DataPoint(e.Trip,e.Count));
            }         
            

            ViewBag.DataPoints = Newtonsoft.Json.JsonConvert.SerializeObject(dataPoints);

            List<DataPoint> dataPointDo = new List<DataPoint>();
            var rating = db.UsersBookings.Select(n => new
            {
                rat = n.RattingScale,
            });
            int totalRat = 0;
            int i = 0;
            foreach (var e in rating)
            {
                if (!string.IsNullOrEmpty(e.rat))
                {
                    totalRat = totalRat + int.Parse(e.rat);
                }
                i++;
            }
            var totalRecord = (i * 5);
            double averageRatting = (double)totalRat/totalRecord;
            dataPointDo.Add(new DataPoint("Travel Again!", averageRatting));
            dataPointDo.Add(new DataPoint("Maybe Not.", (5-averageRatting)));
            ViewBag.dataPointDo = Newtonsoft.Json.JsonConvert.SerializeObject(dataPointDo);
            return View();
           // return View();
        }

        [Authorize(Roles ="admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize(Roles = "admin,user")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Chat()
        {
            return View();
        }

        public class DataPoint
        {
            public DataPoint(string label, int y)
            {
                this.label = label;
                this.y = y;
            }

            public DataPoint(string label, double z)
            {
                this.label = label;
                this.z = z;
            }
           
            public string label = string.Empty;
            
            public Nullable<int> y = null;

            public Nullable<double> z = null;
        }

        //[HttpPost]
        //public ActionResult Send_Email(SendEmail model)
        //{
        //    if (ModelState.IsValid)
        //    {

        //    }

        //    return View();
        //}
    }
}