using Xunit;
using Application;

public class UserAbstractTests
{
    // Test som verifiserer at brukernavn og e‑post lagres riktig
    [Fact]
    public void UserAbstract_Stores_Username_And_Email()
    {
        // Arrange
        var student = new Student("S001", "Ola Nordmann", "ola@mail.com", "ola", "pass123", "IT");

        // Act
        string username = student.Username;
        string email = student.Email;

        // Assert
        Assert.Equal("ola", username);
        Assert.Equal("ola@mail.com", email);
    }

    // Test som verifiserer at ID lagres riktig
    [Fact]
    public void UserAbstract_Id_Is_Stored_Correctly()
    {
        // Arrange
        var student = new Student("S002", "Kari", "kari@mail.com", "kari", "pass123", "IT");

        // Act
        string id = student.Id;

        // Assert
        Assert.Equal("S002", id);
    }
}