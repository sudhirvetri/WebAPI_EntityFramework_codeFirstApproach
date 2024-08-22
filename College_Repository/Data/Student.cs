using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace College_Repository.Data
{
    public class Student
    {
  
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public long Phone { get; set; }
        public DateTime AdmissionDate { get; set; }

    }

}
