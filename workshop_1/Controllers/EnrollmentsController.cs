using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using workshop_1.Data;
using workshop_1.Models;

namespace workshop_1.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Enrollments
        public async Task<IActionResult> Index(int courseId)
        {
            var enrollments = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Where(e => e.CourseId == courseId)
                .OrderBy(e => e.Student.LastName)
                .ThenBy(e => e.Student.FirstName)
                .ToListAsync();

            ViewBag.CourseId = courseId;
            ViewBag.CourseTitle = enrollments.FirstOrDefault()?.Course.Title;

            return View(enrollments);
        }


        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Enrollments/Create
        public IActionResult Create(int courseId)
        {
            ViewBag.CourseId = courseId;
            LoadStudentDropdown();
            return View();
        }


        // POST: Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Enrollment enrollment)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => $"{x.Key}: {string.Join(", ", x.Value.Errors.Select(e => e.ErrorMessage))}");

                return Content(string.Join("\n", errors));
            }

            bool exists = await _context.Enrollments.AnyAsync(e =>
                e.CourseId == enrollment.CourseId &&
                e.StudentId == enrollment.StudentId);

            if (!exists && ModelState.IsValid)
            {
                _context.Enrollments.Add(enrollment);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), new { courseId = enrollment.CourseId });
            }

            ViewBag.CourseId = enrollment.CourseId;
            LoadStudentDropdown(enrollment.StudentId);
            return View(enrollment);
        }


        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(long id)
        {
            var enrollment = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (enrollment == null)
                return NotFound();

            LoadStudentDropdown(enrollment.StudentId);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Enrollment enrollment)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(
                    "Details",
                    "Students",
                    new { id = enrollment.StudentId }
                );
            }

            // SEMINAR FILE UPLOAD 
            if (enrollment.SeminarFile != null && enrollment.SeminarFile.Length > 0)
            {
                var uploadsFolder = Path.Combine("wwwroot", "uploads", "enrollments");
                Directory.CreateDirectory(uploadsFolder);

                var ext = Path.GetExtension(enrollment.SeminarFile.FileName);
                var storedFileName = Guid.NewGuid() + ext;

                var path = Path.Combine(uploadsFolder, storedFileName);
                using var stream = new FileStream(path, FileMode.Create);
                await enrollment.SeminarFile.CopyToAsync(stream);

                enrollment.SeminarUrl = "/uploads/enrollments/" + storedFileName;
                enrollment.SeminarOriginalName = enrollment.SeminarFile.FileName; // OVA
            }


            _context.Enrollments.Update(enrollment);
            await _context.SaveChangesAsync();

            // 🔁 BACK TO STUDENT DETAILS
            return RedirectToAction(
                "Details",
                "Students",
                new { id = enrollment.StudentId }
            );
        }


        // GET: Enrollments/Delete/5
        public async Task<IActionResult> Delete(long id)
        {
            var enrollment = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (enrollment == null)
                return NotFound();

            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);

            if (enrollment != null)
            {
                int courseId = enrollment.CourseId;
                _context.Enrollments.Remove(enrollment);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), new { courseId });
            }

            return RedirectToAction(nameof(Index));
        }

        private void LoadStudentDropdown(long? selectedStudentId = null)
        {
            ViewData["StudentId"] = new SelectList(
                _context.Students
                    .OrderBy(s => s.LastName)
                    .ThenBy(s => s.FirstName)
                    .Select(s => new
                    {
                        s.Id,
                        FullName = s.FirstName + " " + s.LastName + " (" + s.StudentId + ")"
                    })
                    .ToList(),
                "Id",
                "FullName",
                selectedStudentId
            );
        }

        private bool EnrollmentExists(long id)
        {
            return _context.Enrollments.Any(e => e.Id == id);
        }
    }
}
