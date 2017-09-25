using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RemoteObservatory.Models.Astronomy;

namespace RemoteObservatory.Controllers
{
    public class CmdSenderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string ExportCommand(ObservationModel Observation)
        {
            return Observation.ConvertToJson();
        }

        [HttpPost]
        public void ImportData(string Json)
        {
            
        }
    }
}