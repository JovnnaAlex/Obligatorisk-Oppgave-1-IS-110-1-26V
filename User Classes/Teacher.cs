
using System;

namespace Application;

// Arver fra Employee, siden en Teacher er en type Employee
public class Teacher : Employee

{
    public Teacher(string id,
                   string name,
                   string email,
                   string username,
                   string password,
                   string department,
                   string position)
        : base(id, name, email, username, password, department, position)
    {
        Role = "Teacher";
    }

    public override void PrintUserInfo()
    {
        base.PrintUserInfo();
        Console.WriteLine("Role: Teacher");
    }
}
