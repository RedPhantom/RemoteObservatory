using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RemoteObservatory.Models.Astronomy;

namespace RemoteObservatory.Controllers
{
    /// <summary>
    /// Handles sending and receiving data to the StandAlone utility.
    /// </summary>
    public class CmdSenderController : Controller
    {
        public IActionResult Index()
        {
            return View("AccessDenied");
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