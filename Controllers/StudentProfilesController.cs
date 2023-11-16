using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalExam.Data;
using FinalExam.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace FinalExam.Controllers
{
    // use Authorize keyword: need to login to see the cart 
    [Authorize]
    public class StudentProfilesController : Controller
    {
        private readonly FinalExamContext _context;

        public StudentProfilesController(FinalExamContext context)
        {
            _context = context;
        }

        // GET: StudentProfiles
        public async Task<IActionResult> Index()
        {
            /*var finalExamContext = _context.StudentProfile.Include(s => s.Course);
            return View(await finalExamContext.ToListAsync());*/

            /*var courses = await _context.Course
                .Where(s => s != null && !s.IsCompleted)
                .ToListAsync();
            return View(courses);*/

            var studentProfiles = await _context.StudentProfile
               .Include(s => s.Course)
               .Where(s => s.Course == null || !s.Course.IsCompleted)
               .ToListAsync();

            var incompleteCourses = await _context.Course
                .Where(c => !c.IsCompleted)
                .ToListAsync();

            ViewData["IncompleteCourses"] = incompleteCourses;

            return View(studentProfiles);
        }

        // GET: StudentProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StudentProfile == null)
            {
                return NotFound();
            }

            var studentProfile = await _context.StudentProfile
                .Include(s => s.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentProfile == null)
            {
                return NotFound();
            }

            return View(studentProfile);
        }

        // only give access to admin
        [Authorize(Roles = "Admin")]
        // GET: StudentProfiles/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Id");
            return View();
        }

        // only give access to admin
        [Authorize(Roles = "Admin")]

        // POST: StudentProfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CourseId")] StudentProfile studentProfile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Id", studentProfile.CourseId);
            return View(studentProfile);
        }

        // only give access to admin
        [Authorize(Roles = "Admin")]
        // GET: StudentProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StudentProfile == null)
            {
                return NotFound();
            }

            var studentProfile = await _context.StudentProfile.FindAsync(id);
            if (studentProfile == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Id", studentProfile.CourseId);
            return View(studentProfile);
        }

        // only give access to admin
        [Authorize(Roles = "Admin")]

        // POST: StudentProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CourseId")] StudentProfile studentProfile)
        {
            if (id != studentProfile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentProfileExists(studentProfile.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Id", studentProfile.CourseId);
            return View(studentProfile);
        }

        // only give access to admin
        [Authorize(Roles = "Admin")]

        // GET: StudentProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StudentProfile == null)
            {
                return NotFound();
            }

            var studentProfile = await _context.StudentProfile
                .Include(s => s.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentProfile == null)
            {
                return NotFound();
            }

            return View(studentProfile);
        }

        // only give access to admin
        [Authorize(Roles = "Admin")]

        // POST: StudentProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StudentProfile == null)
            {
                return Problem("Entity set 'FinalExamContext.StudentProfile'  is null.");
            }
            var studentProfile = await _context.StudentProfile.FindAsync(id);
            if (studentProfile != null)
            {
                _context.StudentProfile.Remove(studentProfile);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentProfileExists(int id)
        {
          return (_context.StudentProfile?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
