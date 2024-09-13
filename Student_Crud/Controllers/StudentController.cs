using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Crud.Data;
using Student_Crud.Models;
using Student_Crud.Models.Entities;

namespace Student_Crud.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext DbContext;
        public StudentController(ApplicationDbContext db) {
            DbContext = db;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            var student = new Student()
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subscribed = viewModel.Subscribed,
            };
            await DbContext.TbStudent.AddAsync(student);
            await DbContext.SaveChangesAsync(); // should be save in db
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
           var AllStudents = await DbContext.TbStudent.ToListAsync();
            return View(AllStudents);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var EditStudents = await DbContext.TbStudent.FindAsync(id);
            return View(EditStudents);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var Student = await DbContext.TbStudent.FindAsync(viewModel.Id);
            if(Student is not null) {
                Student.Name = viewModel.Name;
                Student.Email = viewModel.Email;    
                Student.Phone = viewModel.Phone;
                Student.Subscribed = viewModel.Subscribed;

                await DbContext.SaveChangesAsync();
            }
            // it's redirect data to list view inside Student controller
            return RedirectToAction("List" , "Student");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Student viewModel)
        {
            var del = await DbContext.TbStudent.
                AsNoTracking().
                FirstOrDefaultAsync( x => x.Id ==  viewModel.Id);

            if(del is not null){
                 DbContext.TbStudent.Remove(viewModel);
                 await DbContext.SaveChangesAsync();
            }
            // it's redirect data to list view inside Student controller
            return RedirectToAction("List", "Student");
        }

    }
}
