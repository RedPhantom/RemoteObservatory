using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using RemoteObservatory.Models.Astronomy;
using System.Collections;
using System.Collections.ObjectModel;

namespace RemoteObservatory.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public string Test()
        {
            ObservationModel obs = new ObservationModel()
            {
                CaptureMethod = ObservationModel.CaptureMethods.ObjectID,
                ID = 666,
                ObservationStart = DateTime.Now,
                ObjectID = 420,
                ObjectName = "M42",
                Files = new Collection<FileModel> { }
            };

            obs.Files.Add(new FileModel()
            {
                ColorMethod = FileModel.ColorMethods.GrayScale,
                ExposureTime = 30,
                FrameRate = 105.5F,
                SensetivitMethod = FileModel.SensetivityMethods.ISO,
                SensetivityValue = 1600
            });
            return obs.ConvertToJson();
        }
    }
}
