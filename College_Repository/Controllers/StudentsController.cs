using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using College_Repository.Data;

namespace College_Repository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly SQLITEContext _context;
        public StudentsController(SQLITEContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult<List<Student>> AddStudents(Student obj)
        {
            _context.Student.Add(obj);
            _context.SaveChanges();
            return Ok(_context.Student.ToList());
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Welcome to default Controller !");
        }
    }
}
