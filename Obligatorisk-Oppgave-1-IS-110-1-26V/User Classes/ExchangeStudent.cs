using System;

namespace Application;
//Arver fra student
public class ExchangeStudent : Student
{
    // Deklarerer verdier med auto-imp
    // public string ExStudentId { get; private set; } //ID skal låses etter objektet er laget *trenger ikke*
    // public string ExStudyProgram { get; set; } *trenger ikke, da vi har StudyProgram i Student klassen*
    public string HomeUniversity { get; set; }
    public string Country { get; set; }
    public DateTime PeriodFrom { get; set; }
    public DateTime PeriodTo { get; set; }


    // Konstruktøren 
    public ExchangeStudent(string id, 
                           string name, 
                           string email, 
                           string username,
                           string password,
                           string studyProgram,
                           string homeUniversity,
                           string country,
                           DateTime periodFrom,
                           DateTime periodTo)
        // : base = lager exchange student objekt etter student klassen har kjørt sin konstruktør
        : base(id, name, email, username, password, studyProgram)

    {
        HomeUniversity = homeUniversity;
        Country = country;
        PeriodFrom = periodFrom;
        PeriodTo = periodTo;
    }
     public override void PrintUserInfo() // Erstatter den abstrakte metoden i UserAbstract, og returnerer "Student" for denne klassen
    {
        base.PrintUserInfo(); // Kaller den virtuelle metoden i User klassen for å skrive ut navn og e-post
        Console.WriteLine($"Home University: {HomeUniversity}, Country: {Country}, Period: {PeriodFrom} to {PeriodTo}"); // Skriver ut exchange studentens ID og informasjon, Navn og e-post kommer fra User klassen og studieprogram kommer fra Student klassen
    }
}

