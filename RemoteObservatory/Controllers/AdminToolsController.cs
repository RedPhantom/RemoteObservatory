using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace RemoteObservatory.Controllers
{
    public class AdminToolsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles="SysOp,Admin")]
        public IActionResult TelescopeControl()
        {
            return View();
        }
    }
}