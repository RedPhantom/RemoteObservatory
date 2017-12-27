using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RemoteObservatory.Data;
using RemoteObservatory.Models.Astronomy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using RemoteObservatory.Models;
using System.Security.Claims;

namespace RemoteObservatory.Controllers
{
    public class ObservationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ObservationController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;    
        }

        // GET: Observation
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Index()
        {
            // return all of the rows created by the logged in user.

            ClaimsPrincipal principal = new ClaimsPrincipal();
            
            var newContext = _context.ObservationModel.Where(x => x.ID.ToString() == _userManager.GetUserId(principal));

            return View(await newContext.ToListAsync());
        }

        
        // GET: Observation/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var observationModel = await _context.ObservationModel
                .SingleOrDefaultAsync(m => m.ID == id);
            if (observationModel == null)
            {
                return NotFound();
            }

            return View(observationModel);
        }

        // GET: Observation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Observation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ObservationStart,Longtitude,Latitude,ObjectID,ObjectName,CaptureMethod,Status,CoordinateSystem")] ObservationModel observationModel)
        {
            if (ModelState.IsValid)
            {
                observationModel.OwnerID = _userManager.GetUserId(HttpContext.User);
                _context.Add(observationModel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(observationModel);
        }

        // GET: Observation/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var observationModel = await _context.ObservationModel.SingleOrDefaultAsync(m => m.ID == id);
            if (observationModel == null)
            {
                return NotFound();
            }
            return View(observationModel);
        }

        // POST: Observation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ID,OrderingUser,ObservationStart,Longtitude,Latitude,ObjectID,ObjectName,CaptureMethod,Status,CoordinateSystem")] ObservationModel observationModel)
        {
            if (id != observationModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(observationModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ObservationModelExists(observationModel.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(observationModel);
        }

        // GET: Observation/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var observationModel = await _context.ObservationModel
                .SingleOrDefaultAsync(m => m.ID == id);
            if (observationModel == null)
            {
                return NotFound();
            }

            return View(observationModel);
        }

        // POST: Observation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var observationModel = await _context.ObservationModel.SingleOrDefaultAsync(m => m.ID == id);
            _context.ObservationModel.Remove(observationModel);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ObservationModelExists(long id)
        {
            return _context.ObservationModel.Any(e => e.ID == id);
        }
    }
}
