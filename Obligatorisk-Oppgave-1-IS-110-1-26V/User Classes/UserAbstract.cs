using System;

namespace Application;

//lager abstract klasse: andre klasser kan arve fra
public abstract class User  
{
    // Deklarerer verdier med auto-imp
    public string Name {get; set; } // - Name kan få en verdi "get;", Henter lagret verdi
    public string Email {get; set; }// - Name kan leses med  "set;", lagrer i verdi Name

    public virtual void PrintUserInfo() // Abstrakt metode som må implementeres i subklasser
    {
        Console.WriteLine($"\nName: {Name}, Email: {Email}"); // Skriver ut brukerens navn og e-post    
    }
}

