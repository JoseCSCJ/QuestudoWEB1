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
    public class ClassroomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClassroomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Classrooms
        public async Task<IActionResult> Index()
        {
            var classroomContext = _context.Classrooms.Include(c => c.Instructor);
            return View(await classroomContext.ToListAsync());
        }

        // GET: Classrooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classroom = await _context.Classrooms
                .Include(c => c.Instructor)
                .SingleOrDefaultAsync(m => m.ClassroomID == id);

            if (classroom == null)
            {
                return NotFound();
            }
            
            return View(classroom);
        }

        // GET: Classrooms/Create
        public IActionResult Create()
        {
            PopulateInstructorDropDownList();
            return View();
        }

        // POST: Classrooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClassroomID,InstructorID,Name")] Classroom classroom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classroom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateInstructorDropDownList(classroom.InstructorID);
            return View(classroom);
        }

        // GET: Classrooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classroom = await _context.Classrooms.SingleOrDefaultAsync(m => m.ClassroomID == id);
            if (classroom == null)
            {
                return NotFound();
            }
            PopulateInstructorDropDownList(classroom.InstructorID);
            return View(classroom);
        }

        // POST: Classrooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClassroomID,InstructorID,Name")] Classroom classroom)
        {
            if (id != classroom.ClassroomID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classroom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassroomExists(classroom.ClassroomID))
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
            PopulateInstructorDropDownList(classroom.InstructorID);
            return View(classroom);
        }

        // GET: Classrooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classroom = await _context.Classrooms
                .Include(c => c.Instructor)
                .SingleOrDefaultAsync(m => m.ClassroomID == id);
            if (classroom == null)
            {
                return NotFound();
            }

            return View(classroom);
        }

        // POST: Classrooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classroom = await _context.Classrooms.SingleOrDefaultAsync(m => m.ClassroomID == id);
            _context.Classrooms.Remove(classroom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassroomExists(int id)
        {
            return _context.Classrooms.Any(e => e.ClassroomID == id);
        }

        private void PopulateInstructorDropDownList(object selectedInstructor = null)
        {
            var instructorsQuery = from i in _context.Instructors
                                   orderby i.Name
                                   select i;
            ViewBag.InstructorID = new SelectList(instructorsQuery.AsNoTracking(), "InstructorID", "Name", selectedInstructor);
        }

        
    }
}
