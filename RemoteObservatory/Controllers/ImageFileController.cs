using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RemoteObservatory.Data;
using RemoteObservatory.Models.Astronomy;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using RemoteObservatory.Models;
using Microsoft.AspNetCore.Identity;

namespace RemoteObservatory.Controllers
{
    [Authorize]
    public class ImageFileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ImageFileController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ImageFile
        public async Task<IActionResult> Index()
        {
            return View(await _context.FileModel.ToListAsync());
        }

        // GET: ImageFile/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileModel = await _context.FileModel
                .SingleOrDefaultAsync(m => m.ID == id);
            if (fileModel == null)
            {
                return NotFound();
            }

            return View(fileModel);
        }

        // GET: ImageFile/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ImageFile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SensetivityMethod,SensetivityValue,ColorMethod,VerticalResolution,HorizontalResolution,VerticalOffset,HorizontalOffset,ExposureTime,FrameRate")] FileModel fileModel)
        {
            if (ModelState.IsValid)
            {
                fileModel.OwnerID = _userManager.GetUserId(HttpContext.User); // get the current logged in user ID
                _context.Add(fileModel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(fileModel);
        }

        // GET: ImageFile/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileModel = await _context.FileModel.SingleOrDefaultAsync(m => m.ID == id);
            if (fileModel == null)
            {
                return NotFound();
            }
            return View(fileModel);
        }

        // POST: ImageFile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SensetivityMethod,SensetivityValue,ColorMethod,VerticalResolution,HorizontalResolution,VerticalOffset,HorizontalOffset,ExposureTime,FrameRate")] FileModel fileModel)
        {
            if (id != fileModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fileModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FileModelExists(fileModel.ID))
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
            return View(fileModel);
        }

        // GET: ImageFile/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileModel = await _context.FileModel
                .SingleOrDefaultAsync(m => m.ID == id);
            if (fileModel == null)
            {
                return NotFound();
            }

            return View(fileModel);
        }

        // POST: ImageFile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fileModel = await _context.FileModel.SingleOrDefaultAsync(m => m.ID == id);
            _context.FileModel.Remove(fileModel);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool FileModelExists(int id)
        {
            return _context.FileModel.Any(e => e.ID == id);
        }
    }
}
