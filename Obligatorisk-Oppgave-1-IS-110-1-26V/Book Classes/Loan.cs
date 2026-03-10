using System;

namespace Application;

public class Loan
{
    // Deklarerer verdier med auto-imp
    public string LoanId { get; private set; }          // Lånet får en unik ID
    public string UserId { get; private set; }          // Hvem låner boka
    public string BookId { get; private set; }          // Hvilken bok er lånt
    public DateTime LoanDate { get; private set; }      // Når ble boka lånt
    public DateTime DueDate { get; private set; }       // Når skal boka leveres
    public DateTime? ReturnDate { get; set; }   // Endres når boka leveres, nullable "?" fordi den kan være null før boka returneres

    public bool IsReturned => ReturnDate != null;  //Returnerer true hvis boka er levert, false hvis den fortsatt er lånt

    public Loan(string loanId, 
                string userId, 
                string bookId, 
                DateTime loanDate, 
                DateTime dueDate)
    {
        LoanId = loanId;
        UserId = userId;
        BookId = bookId;
        LoanDate = loanDate;
        DueDate = dueDate;
    }

    public void RegisterReturn(DateTime returnDate)
    {
        ReturnDate = returnDate;
    }
}
