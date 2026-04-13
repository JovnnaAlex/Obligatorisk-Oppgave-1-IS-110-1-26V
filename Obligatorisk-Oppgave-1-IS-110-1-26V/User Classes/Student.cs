using System;

namespace Application;
//Arver fra User
public class Student : User
{
    // Deklarerer verdier med auto-imp
    // public string StudentId {get; private set; } //ID skal låses etter objektet er laget. **Trenger ikke denne, da vi har Id i User klassen**
    public string StudyProgram {get; set; }
    // Konstruktøren 
    public Student(string id, 
                   string name, 
                   string email,
                   string username,
                   string password, 
                   string studyProgram)
        // : base = lager student objekt etter user klassen har kjørt sin konstruktør.
        // Vi kaller "protected user" 
        : base(id, name, email, username, password, role: "Student")
    {
        StudyProgram = studyProgram; 
    }
    public override void PrintUserInfo() // Erstatter den abstrakte metoden i UserAbstract, og returnerer "Student" for denne klassen
    {
         base.PrintUserInfo(); // Kaller den virtuelle metoden i User klassen for å skrive ut navn og e-post
        Console.WriteLine($"Study Program: {StudyProgram}"); // Skriver ut studentens studieprogram + "Student" for denne klassen, Navn og e-post kommer fra User klassen

    }
}
    
