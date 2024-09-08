using College_Repository.Data;

public static class StudentMapper
{
    public static StudentDTO Map(Student obj)
    {
        return new StudentDTO
        {
            Id = obj.Id,
            Name = obj.Name,
            AdmissionDate = obj.AdmissionDate,
            DateofBirth = obj.DateofBirth,
            Email = obj.Email,
            Phone = obj.Phone,
            Status = obj.Status

        };
    }
}