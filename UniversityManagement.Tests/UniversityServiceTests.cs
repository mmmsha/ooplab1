using UniversityManagement.Core.Courses;
using UniversityManagement.Core.Entities;
using UniversityManagement.Core.Services;
using Xunit;

namespace UniversityManagement.Tests;

public class UniversityServiceTests
{
    [Fact]
    public void AddCourse_CourseIsAdded_AndCanBeRetrieved()
    {
        // Arrange
        var service = new UniversityService();
        Course course = new OnlineCourse(1, "C# Basics", "Zoom", "https://zoom.us/abc");

        // Act
        service.AddCourse(course);
        var saved = service.GetCourse(1);

        // Assert
        Assert.Equal(1, saved.Id);
        Assert.Equal("C# Basics", saved.Title);
    }

    [Fact]
    public void RemoveCourse_CourseIsRemoved_ThenGetThrows()
    {
        // Arrange
        var service = new UniversityService();
        service.AddCourse(new OfflineCourse(1, "OOP", "101", "Main campus"));

        // Act
        service.RemoveCourse(1);

        // Assert
        Assert.Throws<InvalidOperationException>(() => service.GetCourse(1));
    }

    [Fact]
    public void AssignTeacherToCourse_TeacherIsStoredInCourse()
    {
        // Arrange
        var service = new UniversityService();
        service.AddCourse(new OnlineCourse(1, "Async", "Teams", "https://teams/meet"));
        var teacher = new Teacher(10, "Ivan Petrov");

        // Act
        service.AssignTeacherToCourse(1, teacher);
        var course = service.GetCourse(1);

        // Assert
        Assert.NotNull(course.Teacher);
        Assert.Equal(10, course.Teacher!.Id);
    }

    [Fact]
    public void EnrollStudentToCourse_StudentAppearsInCourseStudents()
    {
        // Arrange
        var service = new UniversityService();
        service.AddCourse(new OfflineCourse(1, "Unit Testing", "202", "2nd building"));
        var student = new Student(100, "Anna Ivanova");

        // Act
        service.EnrollStudentToCourse(1, student);
        var students = service.GetStudentsByCourse(1);

        // Assert
        Assert.Single(students);
        Assert.Equal(100, students[0].Id);
    }

    [Fact]
    public void EnrollStudentToCourse_DuplicateStudent_Throws()
    {
        // Arrange
        var service = new UniversityService();
        service.AddCourse(new OnlineCourse(1, "Generics", "Zoom", "https://zoom.us/gen"));
        var student = new Student(100, "Anna Ivanova");

        // Act
        service.EnrollStudentToCourse(1, student);

        // Assert
        Assert.Throws<InvalidOperationException>(() => service.EnrollStudentToCourse(1, student));
    }

    [Fact]
    public void GetCoursesByTeacher_ReturnsOnlyCoursesOfThatTeacher()
    {
        // Arrange
        var service = new UniversityService();

        var t1 = new Teacher(1, "Teacher One");
        var t2 = new Teacher(2, "Teacher Two");

        var c1 = new OnlineCourse(10, "C# 1", "Zoom", "url1");
        var c2 = new OfflineCourse(20, "C# 2", "101", "Campus");
        var c3 = new OnlineCourse(30, "C# 3", "Teams", "url3");

        service.AddCourse(c1);
        service.AddCourse(c2);
        service.AddCourse(c3);

        service.AssignTeacherToCourse(10, t1);
        service.AssignTeacherToCourse(20, t1);
        service.AssignTeacherToCourse(30, t2);

        // Act
        var teacher1Courses = service.GetCoursesByTeacher(1);
        var teacher2Courses = service.GetCoursesByTeacher(2);

        // Assert
        Assert.Equal(2, teacher1Courses.Count);
        Assert.All(teacher1Courses, c => Assert.Equal(1, c.Teacher!.Id));

        Assert.Single(teacher2Courses);
        Assert.Equal(30, teacher2Courses[0].Id);
    }
}