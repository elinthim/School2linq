using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School2linq.Data;
using School2linq.Models;

namespace School2linq.Controllers
{
    public class SearchSchoolViewController : Controller
    {      
        
            private readonly SchooltwolinqDBContext _context;
            public SearchSchoolViewController(SchooltwolinqDBContext context)
            {
                _context = context;
            }

            //GET: Teacher who teaches the subject Programming 1
            public async Task<IActionResult> TeacherProgramming()
            {
                var teacherList = await (from t in _context.Teachers
                                         join sc in _context.SchoolConnections on t.TeacherId equals sc.FK_TeacherId
                                         join c in _context.Courses on sc.FK_CourseId equals c.CourseId
                                         where c.CourseName == "Programming 1"
                                         select new SearchSchoolViewModel
                                         {
                                             TeacherFirstName = t.TeacherFirstName,
                                             CourseName = c.CourseName
                                         }).Distinct().ToListAsync();
                return View(teacherList);
            }

            //GET: All students and their teachers
            public async Task<IActionResult> StudentsAndTeachers()
            {
                var studentsList = await (from s in _context.Students
                                          join sc in _context.SchoolConnections on s.StudentId equals sc.FK_StudentId
                                          join t in _context.Teachers on sc.FK_TeacherId equals t.TeacherId
                                          select new SearchSchoolViewModel
                                          {
                                              StudentFirstName = s.StudentFirstName,
                                              TeacherFirstName = t.TeacherFirstName
                                          }).Distinct().ToListAsync();
                return View(studentsList);
            }

            //GET: All students in programming 1
            public async Task<IActionResult> StudentsInProgramming()
            {
                var studentsList = await (from s in _context.Students
                                          join sc in _context.SchoolConnections on s.StudentId equals sc.FK_StudentId
                                          join c in _context.Courses on sc.FK_CourseId equals c.CourseId
                                          join t in _context.Teachers on sc.FK_TeacherId equals t.TeacherId
                                          where c.CourseName == "Programming 1"
                                          select new SearchSchoolViewModel
                                          {
                                              StudentId = s.StudentId,
                                              StudentFirstName = s.StudentFirstName,
                                              CourseName = c.CourseName,
                                              TeacherId = t.TeacherId,
                                              TeacherFirstName = t.TeacherFirstName
                                          }).Distinct().ToListAsync();

                ViewBag.TeacherList = new SelectList(_context.Teachers, "TeacherId", "TeacherFirstName");
                return View(studentsList.ToList());
            }

            [HttpGet]
            public async Task<IActionResult> EditTeacher(int id)
            {

                var teacher = await (from t in _context.Teachers
                                     where t.TeacherId == id
                                     select t).SingleOrDefaultAsync();
                if (teacher == null)
                {
                    return NotFound();
                }

                //var teacherName = await (from t in _context.Teachers
                //                          select t.TeacherFirstName)
                //                          .ToListAsync();

                //ViewBag.TeacherFirstName = teacherName;
                return View(teacher);

            }
            // POST : Edit Teacher
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> EditTeacher(int id, [Bind("TeacherID, TeacherFirstName")] Teacher teacher)
            {
                if (id != teacher.TeacherId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        var teacherToUpdate = _context.Teachers.FirstOrDefault(t => t.TeacherId == id);
                        if (teacherToUpdate == null)
                        {
                            return NotFound();
                        }

                        teacherToUpdate.TeacherFirstName = teacher.TeacherFirstName.Trim();

                        await _context.SaveChangesAsync();

                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TeacherExists(teacher.TeacherId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(StudentsInProgramming));
                }
                ModelState.AddModelError(string.Empty, "An error occurred while saving changes.");
                return View(teacher);
            }
            private bool TeacherExists(int teacher)
            {
                throw new NotImplementedException();
            }
            //GET: Course list 
            public async Task<IActionResult> GetEditCourse()
            {
                var courses = from c in _context.Courses
                              select c;
                return View(await courses.ToListAsync());
            }

            public async Task<IActionResult> EditCourse(int id)
            {
                var course = await _context.Courses.FindAsync(id);
                if (course == null)
                {
                    return NotFound();
                }
                return View(course);
            }

            //POST: Edit course name
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> EditCourse(int id, [Bind("CourseI, CourseName")] Course course)
            {
                if (id != course.CourseId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        var courseToUpdate = _context.Courses.FirstOrDefault(c => c.CourseId == id);
                        if (courseToUpdate == null)
                        {
                            return NotFound();
                        }

                        courseToUpdate.CourseName = course.CourseName.Trim();

                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CourseExists(course.CourseId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(GetEditCourse));
                }
                return View(course);
            }

            private bool CourseExists(int courseId)
            {
                throw new NotImplementedException();
            }

        }

    }
    //public IActionResult Index()
    //{
    //    return View();
    //}


