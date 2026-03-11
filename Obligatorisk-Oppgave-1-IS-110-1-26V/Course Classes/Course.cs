using System;

namespace Application;

public class Course
{
    // Deklarerer verdier med auto-imp
    public string CourseId { get; private set; }  //ID skal låses etter objektet er laget
    public string CourseName { get; set; }     
    public int CourseCredit { get; set; }    
    public int CourseSeats { get; set; }   
    public string RequiredProgram { get; private set; } // Hvilket studieprogram kreves for å ta dette kurset, låses etter opprettelse
  

    //Lager en liste "alle user" funker ikke for exchange students hvis det settes "student"
    public List<User> EnrolledStudents { get; private set; } = new(); 

    
    // Konstruktør
    public Course(string courseId, 
                  string courseName, 
                  int courseCredit, 
                  int courseSeats,
                  string requiredProgram)

    {
        CourseId = courseId; 
        CourseName = courseName; 
        CourseCredit = courseCredit;
        CourseSeats = courseSeats;
        RequiredProgram = requiredProgram;

    }
}
