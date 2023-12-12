using AspNetMvcCrud.Data;
using AspNetMvcCrud.Models;
using AspNetMvcCrud.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcCrud.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MvcDbContext mvcDbContext;

        public EmployeesController(MvcDbContext mvcDbContext) 
        {
            this.mvcDbContext = mvcDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await mvcDbContext.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {

            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
                Department = addEmployeeRequest.Department,
            };

            await mvcDbContext.Employees.AddAsync(employee);
            await mvcDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await mvcDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
           
            if(employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    DateOfBirth = employee.DateOfBirth,
                    Department = employee.Department,
                };
                return await View(viewModel);
            }
            return RedirectToAction("Index");
        }

       [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await mvcDbContext.Employees.FindAsync(model.Id);
            if(employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Department = model.Department;

                await mvcDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]

        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await mvcDbContext.Employees.FindAsync(model.Id);
            if(employee != null)
            {
               mvcDbContext.Employees.Remove(employee); 
               await mvcDbContext.SaveChangesAsync();

               return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    
    }
  
}
