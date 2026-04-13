using Xunit;
using Application;

public class CourseTests
{
    // Test som verifiserer at en student ikke kan registrere seg to ganger
    [Fact]
    public void StudentCannotRegisterTwice()
    {
        // Arrange
        var student = new Student("1", "Ola", "ola@mail.com", "ola", "fwadwa", "pass");
        var course = new Course("IS110", "Programming", 10, 30, "IT");

        // Act
        course.EnrolledStudents.Add(student);
        bool duplicate = course.EnrolledStudents.Contains(student);

        // Assert
        Assert.True(duplicate);
    }
}