using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School2linq.Data;
using School2linq.Models;

namespace School2linq.Controllers
{
    public class SchoolConnectionsController : Controller
    {
        private readonly SchooltwolinqDBContext _context;

        public SchoolConnectionsController(SchooltwolinqDBContext context)
        {
            _context = context;
        }

        // GET: SchoolConnections
        public async Task<IActionResult> Index()
        {
            var schooltwolinqDBContext = _context.SchoolConnections.Include(s => s.Classes).Include(s => s.Courses).Include(s => s.Students).Include(s => s.Teachers);
            return View(await schooltwolinqDBContext.ToListAsync());
        }

        // GET: SchoolConnections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SchoolConnections == null)
            {
                return NotFound();
            }

            var schoolConnection = await _context.SchoolConnections
                .Include(s => s.Classes)
                .Include(s => s.Courses)
                .Include(s => s.Students)
                .Include(s => s.Teachers)
                .FirstOrDefaultAsync(m => m.ConnectionId == id);
            if (schoolConnection == null)
            {
                return NotFound();
            }

            return View(schoolConnection);
        }

        // GET: SchoolConnections/Create
        public IActionResult Create()
        {
            ViewData["FK_ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId");
            ViewData["FK_CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName");
            ViewData["FK_StudentId"] = new SelectList(_context.Students, "StudentId", "StudentFirstName");
            ViewData["FK_TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherFirstName");
            return View();
        }

        // POST: SchoolConnections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConnectionId,FK_StudentId,FK_TeacherId,FK_ClassId,FK_CourseId")] SchoolConnection schoolConnection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schoolConnection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FK_ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId", schoolConnection.FK_ClassId);
            ViewData["FK_CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", schoolConnection.FK_CourseId);
            ViewData["FK_StudentId"] = new SelectList(_context.Students, "StudentId", "StudentFirstName", schoolConnection.FK_StudentId);
            ViewData["FK_TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherFirstName", schoolConnection.FK_TeacherId);
            return View(schoolConnection);
        }

        // GET: SchoolConnections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SchoolConnections == null)
            {
                return NotFound();
            }

            var schoolConnection = await _context.SchoolConnections.FindAsync(id);
            if (schoolConnection == null)
            {
                return NotFound();
            }
            ViewData["FK_ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId", schoolConnection.FK_ClassId);
            ViewData["FK_CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", schoolConnection.FK_CourseId);
            ViewData["FK_StudentId"] = new SelectList(_context.Students, "StudentId", "StudentFirstName", schoolConnection.FK_StudentId);
            ViewData["FK_TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherFirstName", schoolConnection.FK_TeacherId);
            return View(schoolConnection);
        }

        // POST: SchoolConnections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConnectionId,FK_StudentId,FK_TeacherId,FK_ClassId,FK_CourseId")] SchoolConnection schoolConnection)
        {
            if (id != schoolConnection.ConnectionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schoolConnection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolConnectionExists(schoolConnection.ConnectionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FK_ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId", schoolConnection.FK_ClassId);
            ViewData["FK_CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", schoolConnection.FK_CourseId);
            ViewData["FK_StudentId"] = new SelectList(_context.Students, "StudentId", "StudentFirstName", schoolConnection.FK_StudentId);
            ViewData["FK_TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherFirstName", schoolConnection.FK_TeacherId);
            return View(schoolConnection);
        }

        // GET: SchoolConnections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SchoolConnections == null)
            {
                return NotFound();
            }

            var schoolConnection = await _context.SchoolConnections
                .Include(s => s.Classes)
                .Include(s => s.Courses)
                .Include(s => s.Students)
                .Include(s => s.Teachers)
                .FirstOrDefaultAsync(m => m.ConnectionId == id);
            if (schoolConnection == null)
            {
                return NotFound();
            }

            return View(schoolConnection);
        }

        // POST: SchoolConnections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SchoolConnections == null)
            {
                return Problem("Entity set 'SchooltwolinqDBContext.SchoolConnections'  is null.");
            }
            var schoolConnection = await _context.SchoolConnections.FindAsync(id);
            if (schoolConnection != null)
            {
                _context.SchoolConnections.Remove(schoolConnection);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoolConnectionExists(int id)
        {
          return (_context.SchoolConnections?.Any(e => e.ConnectionId == id)).GetValueOrDefault();
        }
    }
}
