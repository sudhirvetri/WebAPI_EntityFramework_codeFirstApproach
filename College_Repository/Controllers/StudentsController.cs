using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using College_Repository.Data;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;


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
        public async Task<ActionResult<List<Student>>> AddStudent(Student obj)
        {
            await _context.Student.AddAsync(obj);
            await _context.SaveChangesAsync();
            return Ok(await _context.Student.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudent()
        {
            var studentresult = await _context.Student.ToListAsync();
            var studentDTOResult = studentresult.Select(data => StudentMapper.Map(data)).ToList();
            return Ok(studentDTOResult);

        }

        [HttpGet("{id:int}")]
        //[Route("GetStudentbyID")]
        public async Task<ActionResult<List<Student>>> SearchStudentOnID(int id)
        {
            return Ok(await _context.Student.Where(data => data.Id == id).ToListAsync());
        }

        [HttpDelete]
        [Route("DeleteStudentbyID")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var result = await _context.Student.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            _context.Student.Remove(result);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        [Route("UpdateStudent")]
        public async Task<ActionResult> PutStudent(Student obj)
        {
            if (obj == null || obj.Id <= 0)
            {
                return BadRequest();
            }
            var existingStudent = await _context.Student.AsNoTracking().Where(x => x.Id == obj.Id).FirstOrDefaultAsync();
            if (existingStudent == null)
            {
                return NotFound("The student with given ID is not present in repository");
            }

            var newrecord = new Student()
            {
                Id = existingStudent.Id,
                Name = obj.Name,
                Email = obj.Email,
                AdmissionDate = obj.AdmissionDate,
                DateofBirth = obj.DateofBirth,
                Phone = obj.Phone,
                Status = obj.Status
            };

            // existingStudent.Name = obj.Name;
            // existingStudent.Phone = obj.Phone;
            // existingStudent.Email = obj.Email;
            // existingStudent.AdmissionDate = obj.AdmissionDate;
            // existingStudent.DateofBirth = obj.DateofBirth;
            // existingStudent.Status = obj.Status;
            _context.Student.Update(newrecord);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> PatchStudent(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDoc)
        {
            if (id <= 0)
            {
                return BadRequest("Id cannot be 0 or less than it");
            }

            if (patchDoc == null)
            {
                return BadRequest();
            }

            var existingStudent = await _context.Student.Where(data => data.Id == id).FirstOrDefaultAsync();
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

            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}
