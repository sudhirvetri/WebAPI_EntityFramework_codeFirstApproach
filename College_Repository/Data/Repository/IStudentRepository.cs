namespace College_Repository.Data.repository
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllAsync();
        Task<Student> GetByStudentIdAsync(int id);
        Task<List<Student>> CreateStudentAsync(Student obj);
        Task<bool> UpdateStudentAsync(StudentDTO obj);
        Task<bool> DeleteStudentAsync(int id);
        // Task<bool> PatchStudentAsync(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDoc);
        
    }
}