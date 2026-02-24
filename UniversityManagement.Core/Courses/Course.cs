using UniversityManagement.Core.Entities;

namespace UniversityManagement.Core.Courses;

public abstract class Course
{
    private readonly List<Student> _students = new();

    public int Id { get; }
    public string Title { get; }

    // Преподаватель может быть не назначен
    public Teacher? Teacher { get; private set; }

    public IReadOnlyList<Student> Students => _students.AsReadOnly();

    protected Course(int id, string title)
    {
        if (id <= 0) throw new ArgumentException("Id должен быть положительным.", nameof(id));
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Название курса не может быть пустым.", nameof(title));

        Id = id;
        Title = title;
    }

    public void AssignTeacher(Teacher teacher)
    {
        Teacher = teacher ?? throw new ArgumentNullException(nameof(teacher));
    }

    public void EnrollStudent(Student student)
    {
        if (student is null) throw new ArgumentNullException(nameof(student));

        //защита от дубликатов
        if (_students.Any(s => s.Id == student.Id))
            throw new InvalidOperationException("Студент уже записан на этот курс.");

        _students.Add(student);
    }

    public void RemoveStudent(int studentId)
    {
        var existing = _students.FirstOrDefault(s => s.Id == studentId);
        if (existing is null)
            throw new InvalidOperationException("Студент не найден на этом курсе.");

        _students.Remove(existing);
    }

    public abstract string GetDetails();
}