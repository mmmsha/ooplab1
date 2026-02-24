namespace UniversityManagement.Core.Entities;

public class Student
{
    public int Id { get; }
    public string FullName { get; }

    public Student(int id, string fullName)
    {
        if (id <= 0) throw new ArgumentException("Id должен быть положительным.", nameof(id));
        if (string.IsNullOrWhiteSpace(fullName)) throw new ArgumentException("ФИО не может быть пустым.", nameof(fullName));

        Id = id;
        FullName = fullName;
    }
}