using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using College_Repository.Data;
using Microsoft.AspNetCore.JsonPatch;


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
        [Route("AddStudent")]
        public ActionResult<List<Student>> AddStudent(Student obj)
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

        [HttpGet("{id:int}")]
        //[Route("GetStudentbyID")]
        public ActionResult<List<Student>> SearchStudentOnID(int id)
        {
            return Ok(_context.Student.Where(data => data.Id == id).ToList());
        }

        [HttpDelete]
        [Route("DeleteStudentbyID")]
        public ActionResult DeleteStudent(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var result = _context.Student.Where(x => x.Id == id).FirstOrDefault();
            if (result == null)
            {
                return NotFound();
            }
            _context.Student.Remove(result);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("UpdateStudent")]
        public ActionResult PutStudent(Student obj)
        {
            if (obj == null || obj.Id <= 0)
            {
                return BadRequest();
            }
            var existingStudent = _context.Student.Where(x => x.Id == obj.Id).FirstOrDefault();
            if (existingStudent == null)
            {
                return NotFound("The student with given ID is not present in repository");
            }
            existingStudent.Name = obj.Name;
            existingStudent.Phone = obj.Phone;
            existingStudent.Email = obj.Email;
            existingStudent.AdmissionDate = obj.AdmissionDate;
            existingStudent.DateofBirth = obj.DateofBirth;
            existingStudent.Status = obj.Status;
            _context.Student.Update(existingStudent);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPatch("{id:int}")]
        public ActionResult PatchStudent(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDoc)
        {
            if (id <= 0)
            {
                return BadRequest("Id cannot be 0 or less than it");
            }

            if (patchDoc == null)
            {
                return BadRequest();
            }

            var existingStudent = _context.Student.Where(data => data.Id == id).FirstOrDefault();
            if (existingStudent == null)
            {
                return NotFound("Student not found");
            }
            var studentToPatch = new StudentDTO
            {

                Name = existingStudent.Name,
                Phone = existingStudent.Phone,
                Email = existingStudent.Email,
                AdmissionDate = existingStudent.AdmissionDate,
                DateofBirth = existingStudent.DateofBirth,
                Status = existingStudent.Status
            };
            patchDoc.ApplyTo(studentToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            existingStudent.Name = studentToPatch.Name;
            existingStudent.Phone = studentToPatch.Phone;
            existingStudent.Email = studentToPatch.Email;
            existingStudent.AdmissionDate = studentToPatch.AdmissionDate;
            existingStudent.DateofBirth = studentToPatch.DateofBirth;
            existingStudent.Status = studentToPatch.Status;

            _context.SaveChanges();
            return NoContent();
        }


    }
}
