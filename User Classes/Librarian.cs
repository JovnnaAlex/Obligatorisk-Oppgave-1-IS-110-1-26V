
using System;

namespace Application;
public class Librarian : Employee
{
    public Librarian(string id,
                     string name,
                     string email,
                     string username,
                     string password,
                     string department,
                     string position)
        : base(id, name, email, username, password, department, position)
    {
        Role = "Librarian";
    }

    public override void PrintUserInfo()
    {
        base.PrintUserInfo();
        Console.WriteLine("Role: Librarian");
    }
}
