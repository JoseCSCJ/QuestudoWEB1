using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Questudo.Data;
using Questudo.Models;
using Microsoft.AspNetCore.Authorization;

namespace Questudo.Controllers
{
    [Authorize]
    public class EnrolledsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnrolledsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Enrolleds
        public async Task<IActionResult> Index()
        {
            var classroomContext = _context.Enrolleds
                .Include(e => e.Student)
                .Include(e => e.Classroom)
                    .ThenInclude(c => c.Instructor);                        
                
            return View(await classroomContext.ToListAsync());
        }

        // GET: Enrolleds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrolled = await _context.Enrolleds
                .Include(e => e.Classroom)
                .Include(e => e.Student)
                .SingleOrDefaultAsync(m => m.EnrolledID == id);
            if (enrolled == null)
            {
                return NotFound();
            }

            return View(enrolled);
        }

        // GET: Enrolleds/Create
        public IActionResult Create()
        {
            PopulateClassroomDropDownList();
            PopulateStudentDropDownList();
            return View();
        }

        // POST: Enrolleds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnrolledID,StudentID,ClassroomID")] Enrolled enrolled)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrolled);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateClassroomDropDownList(enrolled.ClassroomID);
            PopulateStudentDropDownList(enrolled.StudentID);
            return View(enrolled);
        }

        // GET: Enrolleds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrolled = await _context.Enrolleds.SingleOrDefaultAsync(m => m.EnrolledID == id);
            if (enrolled == null)
            {
                return NotFound();
            }
            PopulateClassroomDropDownList(enrolled.ClassroomID);
            PopulateStudentDropDownList(enrolled.StudentID);
            return View(enrolled);
        }

        // POST: Enrolleds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnrolledID,StudentID,ClassroomID")] Enrolled enrolled)
        {
            if (id != enrolled.EnrolledID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrolled);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrolledExists(enrolled.EnrolledID))
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
            PopulateClassroomDropDownList(enrolled.ClassroomID);
            PopulateStudentDropDownList(enrolled.StudentID);
            return View(enrolled);
        }

        // GET: Enrolleds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrolled = await _context.Enrolleds
                .Include(e => e.Classroom)
                .Include(e => e.Student)
                .SingleOrDefaultAsync(m => m.EnrolledID == id);
            if (enrolled == null)
            {
                return NotFound();
            }

            return View(enrolled);
        }

        // POST: Enrolleds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrolled = await _context.Enrolleds.SingleOrDefaultAsync(m => m.EnrolledID == id);
            _context.Enrolleds.Remove(enrolled);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrolledExists(int id)
        {
            return _context.Enrolleds.Any(e => e.EnrolledID == id);
        }

        private void PopulateStudentDropDownList(object selectedStudent = null)
        {
            var studentsQuery = from s in _context.Students
                                orderby s.Name
                                select s;

            ViewBag.StudentID = new SelectList(studentsQuery, "StudentID", "Name", selectedStudent);
        }
        private void PopulateClassroomDropDownList(object selectedClassroom = null)
        {
            var classroomsQuery = from c in _context.Classrooms
                                 orderby c.Name
                                 select c;

            ViewBag.ClassroomID = new SelectList(classroomsQuery, "ClassroomID", "Name", selectedClassroom);
        }
    }

    
}
