using System;

namespace Application;

//lager abstract klasse: andre klasser kan arve fra
public abstract class User  
{
    // Deklarerer verdier med auto-imp
    public string Id { get; private set; } //Unik ID for alle brukere, skal låses etter objektet er laget
    public string Name {get; set; } // Name kan få en verdi "get;", Henter lagret verdi
    public string Email {get; set; }// Name kan leses med  "set;", lagrer i verdi Name
    
    public string Username { get; set; } // Nye funskjoner for systemet, globalt unikt brukernavn for alle brukere, kan endres
    public string Password { get; set; } // Passord for brukere

    public string Role { get; set; } // Rolle for brukere, kan være "Student", "Employee" eller "ExchangeStudent"

    // Protected: Kode utenfor klassen kan ikke bruke denne konstruktørem, men subklasser kan bruke den for å lage objekter
    protected User(string id, string name, string email, string username, string password, string role)
    {
        Id = id;
        Name = name;
        Email = email;
        Username = username;
        Password = password;
        Role = role;
    }

    public virtual void PrintUserInfo() // Abstrakt metode som må implementeres i subklasser
    {
        Console.WriteLine($"\nName: {Name}, Email: {Email}"); // Skriver ut brukerens navn og e-post    
    }
}

