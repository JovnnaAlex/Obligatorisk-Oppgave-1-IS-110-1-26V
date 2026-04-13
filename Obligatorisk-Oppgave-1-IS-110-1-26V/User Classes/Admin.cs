using System;

namespace Application;
public class Admin : User
{
    public Admin(string id,
                 string name,
                 string email,
                 string username,
                 string password)
        : base(id, name, email, username, password, "Admin")
    {
    }

    public override void PrintUserInfo()
    {
        base.PrintUserInfo();
        Console.WriteLine("Role: Admin");
    }
}
