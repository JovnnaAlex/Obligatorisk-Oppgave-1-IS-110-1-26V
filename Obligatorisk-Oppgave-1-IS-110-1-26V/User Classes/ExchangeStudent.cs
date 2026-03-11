using System;

namespace Application;
//Arver fra User
public class ExchangeStudent : User
{
    // Deklarerer verdier med auto-imp
    public string ExStudentId { get; private set; } //ID skal låses etter objektet er laget
    public string ExStudyProgram { get; set; }
    public string HomeUniversity { get; set; }
    public string Country { get; set; }
    public DateTime PeriodFrom { get; set; }
    public DateTime PeriodTo { get; set; }


    // Konstruktøren 
    public ExchangeStudent(string name,
                           string email,
                           string studentId,
                           string studyProgram,
                           string homeUniversity,
                           string country,
                           DateTime periodFrom,
                           DateTime periodTo)
                          
        
        
    {
        Name = name;               // fra User klassen
        Email = email;             // fra User klassen
        ExStudentId = studentId;   // Lager ekstra parameter til denne klassen
        ExStudyProgram = studyProgram;
        HomeUniversity = homeUniversity;
        Country = country;
        PeriodFrom = periodFrom;
        PeriodTo = periodTo;
    }
     public override void PrintUserInfo() // Erstatter den abstrakte metoden i UserAbstract, og returnerer "Student" for denne klassen
    {
        base.PrintUserInfo(); // Kaller den virtuelle metoden i User klassen for å skrive ut navn og e-post
        Console.WriteLine($"Exchange Student ID: {ExStudentId}, Exchange Study Program: {ExStudyProgram}, Home University: {HomeUniversity}, Country: {Country}, Period: {PeriodFrom} to {PeriodTo}"); // Skriver ut exchange studentens ID og informasjon, Navn og e-post kommer fra User klassen
    }
}

