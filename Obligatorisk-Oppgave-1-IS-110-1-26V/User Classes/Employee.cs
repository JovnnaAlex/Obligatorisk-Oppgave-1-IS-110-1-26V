using System;

namespace Application;


// Arver fra User
public class Employee : User
{
    // Deklarerer verdier med auto-imp
    public string EmployeeId { get; private set; }   // //ID skal låses etter objektet er laget
    public string Department { get; set; }           // Avdeling kan endres 
    public string Position { get; set; }             // Stilling kan endres

    // Konstruktør
    public Employee(string name, 
                    string email, 
                    string employeeId, 
                    string department,
                    string position)    
    {
        Name = name;             // fra User klassen
        Email = email;           // fra User klassen
        EmployeeId = employeeId; // unik ID for ansatte
        Department = department; // hvilken avdeling de jobber i
        Position = position;     // hvilken stilling de har 
    }
    public override void PrintUserInfo() // Erstatter den abstrakte metoden i UserAbstract, og returnerer "Student" for denne klassen
    {
        base.PrintUserInfo(); // Kaller den virtuelle metoden i User klassen for å skrive ut navn og e-post
        Console.WriteLine($"Employee ID: {EmployeeId}, Department: {Department}, Position: {Position}"); // Skriver ut ansattens ID og informasjon, Navn og e-post kommer fra User klassen
    }
}

