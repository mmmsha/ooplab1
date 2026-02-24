using UniversityManagement.Core.Courses;
using UniversityManagement.Core.Entities;

namespace UniversityManagement.Core.Services;

public class UniversityService
{
    private readonly Dictionary<int, Course> _courses = new();

    public void AddCourse(Course course)
    {
        if (course is null) throw new ArgumentNullException(nameof(course));

        if (_courses.ContainsKey(course.Id))
            throw new InvalidOperationException("Курс с таким Id уже существует.");

        _courses.Add(course.Id, course);
    }

    public void RemoveCourse(int courseId)
    {
        if (!_courses.Remove(courseId))
            throw new InvalidOperationException("Курс не найден.");
    }

    public Course GetCourse(int courseId)
    {
        if (!_courses.TryGetValue(courseId, out var course))
            throw new InvalidOperationException("Курс не найден.");

        return course;
    }

    public void AssignTeacherToCourse(int courseId, Teacher teacher)
    {
        var course = GetCourse(courseId);
        course.AssignTeacher(teacher);
    }

    public void EnrollStudentToCourse(int courseId, Student student)
    {
        var course = GetCourse(courseId);
        course.EnrollStudent(student);
    }

    public IReadOnlyList<Student> GetStudentsByCourse(int courseId)
    {
        var course = GetCourse(courseId);
        return course.Students;
    }

    public IReadOnlyList<Course> GetCoursesByTeacher(int teacherId)
    {
        // Берём только те курсы, где препод назначен и совпадает айди
        return _courses.Values
            .Where(c => c.Teacher is not null && c.Teacher.Id == teacherId)
            .ToList();
    }
}