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
        public ActionResult<List<Student>> GetStudent()
        {
            return Ok(_context.Student.ToList());
        }

        [HttpDelete]
        public ActionResult DeleteStudent(int id)
        {
            if(id<=0)
            {
                return BadRequest();
            }
            var result = _context.Student.Where(x => x.Id == id).FirstOrDefault();
            if(result==null)
            {
                return NotFound();
            }
            _context.Student.Remove(result); 
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public ActionResult PutStudent(Student obj)
        {
            if(obj==null || obj.Id<=0)
            {
                return BadRequest();
            }
            var existingStudent = _context.Student.Where(x => x.Id==obj.Id).FirstOrDefault();
            if(existingStudent==null)
            {
                return NotFound("The student with given ID is not present in repository");
            }
            existingStudent.Name=obj.Name;
            existingStudent.Phone=obj.Phone;
            existingStudent.Email=obj.Email;
            existingStudent.AdmissionDate=obj.AdmissionDate;
            existingStudent.DateofBirth=obj.DateofBirth;
            _context.Student.Update(existingStudent);
            _context.SaveChanges();

            return Ok();
        }

    }
}
