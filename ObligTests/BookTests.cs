using Xunit;
using Application;

public class BookTests
{
    // Test som verifiserer at utlån reduserer antall tilgjengelige bøker
    [Fact]
    public void LoaningBook_Reduces_Available_Count()
    {
        // Arrange
        var book = new Book("B1", "TestBook", "Author", 2024, 3);

        // Act
        book.Available--;

        // Assert
        Assert.Equal(2, book.Available);
    }

    // Test som verifiserer at tittelen settes riktig
    [Fact]
    public void Book_Has_Correct_Title()
    {
        // Arrange
        var book = new Book("B2", "CSharp Basics", "John Doe", 2024, 5);

        // Act
        string title = book.Title;

        // Assert
        Assert.Equal("CSharp Basics", title);
    }
}