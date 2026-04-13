using System;

namespace Application;


// Arver fra User
public class Employee : User
{
    // Deklarerer verdier med auto-imp
   //public string EmployeeId { get; private set; }   // //ID skal låses etter objektet er laget
    public string Department { get; set; }           // Avdeling kan endres 
    public string Position { get; set; }             // Stilling kan endres

    // Konstruktør
    public Employee(string id, 
                   string name, 
                   string email, 
                   string username,
                   string password,
                   string department,
                   string position)   
        // : base = lager student objekt etter user klassen har kjørt sin konstruktør.
        // Vi kaller "protected user" 
        : base(id, name, email, username, password, role: "Employee") // Kaller konstruktøren i User klassen for å sette de felles verdiene 
    {
        Department = department; // hvilken avdeling de jobber i
        Position = position;     // hvilken stilling de har 
    }
    public override void PrintUserInfo() // Overstyrer PrintUserInfo fra User for å vise ansatt‑spesifikk info

    {
        base.PrintUserInfo(); // Kaller den virtuelle metoden i User klassen for å skrive ut navn og e-post
        Console.WriteLine($"Department: {Department}, Position: {Position}"); // Skriver ut ansattens ID og informasjon, Navn og e-post kommer fra User klassen
    }
}

