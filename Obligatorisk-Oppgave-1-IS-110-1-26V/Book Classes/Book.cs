using System;

namespace Application;

public class Book
{
    // Deklarerer verdier med auto-imp
    public string BookId { get; private set; }      // ID låses etter opprettelse
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    public int Available { get; set; }  // Eksemplarer tilgjengelig

//konstruktør
    public Book(string bookId, 
                string title, 
                string author, 
                int year,
                int available)
            
    {
        BookId = bookId;
        Title = title;
        Author = author;
        Year = year;
        Available = available;
    }
}

