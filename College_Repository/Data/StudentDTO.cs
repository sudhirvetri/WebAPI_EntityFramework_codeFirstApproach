

namespace College_Repository.Data
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string? StudentName { get; set; }
        public string? Email { get; set; }
        public long? Phone { get; set; }
        public DateTime AdmissionDate { get; set; }
        public DateTime DateofBirth { get; set; }
        public string? Status { get; set; }

    }
}
