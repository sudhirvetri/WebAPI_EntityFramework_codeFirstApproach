

using Microsoft.EntityFrameworkCore;

namespace College_Repository.Data.repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SQLITEContext _context;

        public StudentRepository(SQLITEContext context)
        {
            _context = context;
        }
        public async Task<List<Student>> CreateStudentAsync(Student obj)
        {
            await _context.Student.AddAsync(obj);
            await _context.SaveChangesAsync();
            return await GetAllAsync();
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid ID");
            }
            var result = await _context.Student.FindAsync(id);
            if (result == null)
            {
                throw new KeyNotFoundException("Student not found");
            }
            _context.Student.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _context.Student.ToListAsync();
        }

        public async Task<Student> GetByStudentIdAsync(int id)
        {
            return await _context.Student.Where(data => data.Id == id).FirstAsync();
        }

        public async Task<bool> UpdateStudentAsync(StudentDTO obj)
        {
            if (obj == null || obj.Id <= 0)
            {
                throw new ArgumentNullException("No Data in the Argument");
            }
            var existingStudent = await _context.Student.Where(data => data.Id == obj.Id).FirstOrDefaultAsync();
            if (existingStudent == null)
            {
                throw new ArgumentNullException("No Student Found in Repo");
            }

            existingStudent.Name = obj.StudentName;
            existingStudent.Phone = obj.Phone;
            existingStudent.Email = obj.Email;
            existingStudent.AdmissionDate = obj.AdmissionDate;
            existingStudent.DateofBirth = obj.DateofBirth;
            existingStudent.Status = obj.Status;

            await _context.SaveChangesAsync();
            return true;
        }
    }

}