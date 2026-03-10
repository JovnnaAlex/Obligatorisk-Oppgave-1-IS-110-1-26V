using System;

namespace Application;
//Arver fra User
public class Student : User
{
    // Deklarerer verdier med auto-imp
    public string StudentId {get; private set; } //ID skal låses etter objektet er laget
    public string StudyProgram {get; set; }
    // Konstruktøren 
    public Student(string name, 
                   string email, 
                   string studentId, 
                   string studyProgram)
    {
        Name = name;               // fra User klassen
        Email = email;             // fra User klassen
        StudentId = studentId;     // Lager ekstra parameter til denne klassen
        StudyProgram = studyProgram; 
    }
    public override void PrintUserInfo() // Erstatter den abstrakte metoden i UserAbstract, og returnerer "Student" for denne klassen
    {
         base.PrintUserInfo(); // Kaller den virtuelle metoden i User klassen for å skrive ut navn og e-post
        Console.WriteLine($"Student ID: {StudentId}, Study Program: {StudyProgram}"); // Skriver ut studentens ID og studieprogram, Navn og e-post kommer fra User klassen
    }
}
    
