
using Application;
//----Lager tomme lister for å holde på data----

List<Student> students = new();
List<ExchangeStudent> exchangeStudents = new();
List<Employee> employees = new();
List<Book> books = new();
List<Course> courses = new();
List<Loan> loans = new();

//Konstruktør: (ID, Tittel, Forfatter, År, eksemplarer tilgjengelig)
var b1 = new Book("B001", "C# Programming", "John Doe", 2020, 20);
var b2 = new Book("B002", "Data Structures", "Jane Smith", 2018, 15);
// Legger til bøker i listen.  AddRange: Legger til flere elementer i listen på en gang, i stedet for å legge til én og én med Add. Tar inn en liste av bøker som skal legges til i "books" listen.
books.AddRange(new List<Book> { b1, b2 }); 

// Konstruktør: (ID, Navn, Studiepoeng, Antallplasse, Påkrevd studieprogram)
var c1 = new Course("C001", "Introduction to Programming", 5, 30, "IT");
var c2 = new Course("C002", "Advanced Biology", 10, 20, "Biology");
var c3 = new Course("C003", "Alex sitt solo kurs ...hemmelig", 7, 1, "IT");
courses.AddRange(new List<Course> {c1, c2, c3 });

// ---- Liste for polymorfisme ----
List<User> users = new();

// Legg inn startdata
// 1. Student
var s1 = new Student(
    "Alex Guttorm",
    "alex@gmail.com",
    "67676",
    "IT"
);
students.Add(s1);   // brukes av systemet
users.Add(s1);      // brukes til polymorfisme. Kunne også brukt courses.AddRange

// 2. Employee
var e1 = new Employee(
    "Jose Ermano Alexander Westergaard",
    "Jose333@gmail.com",
    "12412",
    "IT",
    "Lecturer"
);
employees.Add(e1);
users.Add(e1);

// 3. ExchangeStudent
var ex1 = new ExchangeStudent(
    "Bobby Alberta",
    "Bob333@gmail.com",
    "49494",
    "Biology",
    "MIT",
    "USA",
    DateTime.Now,
    DateTime.Now.AddMonths(6)
);
exchangeStudents.Add(ex1);
users.Add(ex1);
/*
ex1.PrintUserInfo(); // Skriver ut all informasjon om exchange student, inkludert navn og e-post 
*/

//                                                ------METODER------ 

// Skip metode
void Skip()
{
    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}

//----Metoder og lister som parametere----

//          [1] Create course
void AddCourse(List<Course> courses)
{
    Console.WriteLine("Enter course ID:");
    string? courseId = Console.ReadLine();

    Console.WriteLine("Enter course name:");
    string? courseName = Console.ReadLine();

    Console.WriteLine("Enter course credit:");
    int courseCredit = int.Parse(Console.ReadLine());

    Console.WriteLine("Enter course seats:");
    int courseSeats = int.Parse(Console.ReadLine());

    Console.WriteLine("Enter required study program:");
    string? courseRe = Console.ReadLine();

    Course newCourse = new(courseId, courseName, courseCredit, courseSeats, courseRe);
    courses.Add(newCourse);
    Console.WriteLine($"Course {courseName} added successfully.");
}

// [2] Register or unregister student from a course
// Denne delen kunne vært kortere hvis Exchange student også arvet fra Student, 
// men siden den ikke arver fra student, så blir det litt mer komplisert å sjekke både Student og ExchangeStudent, og må derfor sjekke begge typene i samme metode.
void RegisterStudent(List<Student> students, List<Course> courses)
{
    Console.WriteLine("Do you want to:");
    Console.WriteLine("[1] Register student to course");
    Console.WriteLine("[2] Unregister student from course");
    string? choice = Console.ReadLine();

    Console.WriteLine("Enter student ID:");
    string? studentId = Console.ReadLine();

    Console.WriteLine("Enter course ID:");
    string? courseId = Console.ReadLine();

    User? user = users.FirstOrDefault(u =>
        (u is Student s && s.StudentId == studentId) ||
        (u is ExchangeStudent ex && ex.ExStudentId == studentId)
    );

    // Sjekker om brukeren finnes
    if (user == null)
    {
        Console.WriteLine("Student not found.");
        return;
    }

    // Sjekker om det faktisk er en studenttype
    if (user is not Student && user is not ExchangeStudent)
    {
        Console.WriteLine("Only students can register for courses.");
        return;
    }

    Course? course = courses.FirstOrDefault(c => c.CourseId == courseId);


    if (course == null)
    {
        Console.WriteLine("Course not found.");
        return;
    }

    // --- Register ---
    if (choice == "1")
    {
    // Sjekker studieprogram
    string? studentProgram = user is Student s ? s.StudyProgram 
                : user is ExchangeStudent ex ? ex.ExStudyProgram 
                : null;

    if (!string.Equals(studentProgram, course.RequiredProgram, StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine($"Student cannot register. Required program: {course.RequiredProgram}, but student is in {studentProgram}.");
        return;
    }

    if (course.EnrolledStudents.Contains(user))
    {
        Console.WriteLine("Student is already registered in this course.");
        return;
    }

    if (course.EnrolledStudents.Count >= course.CourseSeats)
    {
        Console.WriteLine("Course is full. Cannot register student.");
        return;
    }

    course.EnrolledStudents.Add(user);
    Console.WriteLine($"Student {user.Name} registered for course {course.CourseName}.");
    }

    // --- Unregister ---
    else if (choice == "2")
    {
        if (!course.EnrolledStudents.Contains(user))
        {
            Console.WriteLine("Student is not enrolled in this course.");
            return;
        }

        course.EnrolledStudents.Remove(user);
        Console.WriteLine($"Student {user.Name} has been removed from course {course.CourseName}.");
    }

    else
    {
        Console.WriteLine("Invalid choice.");
    }
}
//         [3] Print courses and participants
void PrintCourses(List<Course> courses)
{
    Console.WriteLine("\n--- All Courses and Participants ---");
    

    foreach (var course in courses)
    {
        Console.WriteLine($"\nCourse: {course.CourseName} ({course.CourseId})");
        Console.WriteLine($"Credits: {course.CourseCredit}, Seats: {course.CourseSeats}, Seats left: {course.CourseSeats - course.EnrolledStudents.Count}, Required Program: {course.RequiredProgram}");
        Console.WriteLine("Participants:");

        if (course.EnrolledStudents.Count == 0)
        {
            Console.WriteLine("No students enrolled.");
        }
        else
        {
            foreach (var student in course.EnrolledStudents)
            {
                // Polymorfisme: student er en Student, men PrintUserInfo() kommer fra User
                student.PrintUserInfo();
            }
        }
    }
}

//         [4] Search for courses
void SearchCourse(List<Course> courses)
{
    Console.WriteLine("Enter course name to search:");
    string? courseName = Console.ReadLine();

    var foundCourses = courses.Where(c => c.CourseName.Contains(courseName, StringComparison.OrdinalIgnoreCase)).ToList();

    if (foundCourses.Any())
    {
        Console.WriteLine("Found courses:");
        foreach (var course in foundCourses)
        {
            Console.WriteLine($"Course ID: {course.CourseId}, Name: {course.CourseName}, Credit: {course.CourseCredit}, Seats: {course.CourseSeats}, Required Program: {course.RequiredProgram}");
        }
    }
    else
    {
        Console.WriteLine("No courses found with that name.");
    }
}

//         [5] Search for book
void SearchBook(List<Book> books)
{
    Console.WriteLine("Enter book title to search:");
    string? bookTitle = Console.ReadLine();

    var foundBooks = books.Where(b => b.Title.Contains(bookTitle, StringComparison.OrdinalIgnoreCase)).ToList();

    if (foundBooks.Any())
    {
        Console.WriteLine("Found books:");
        foreach (var book in foundBooks)
        {
            Console.WriteLine($"Book ID: {book.BookId}, Title: {book.Title}, Author: {book.Author}, Year: {book.Year}, Available: {book.Available}");
        }
    }
    else
    {
        Console.WriteLine("No books found with that title.");
    }
}

//         [6] Loan book
void LoanBook(List<Book> books, List<Loan> loans, List<Student> students, List<Employee> employees)
{
    Console.WriteLine("Enter book ID or title to loan:");
    string? input = Console.ReadLine();

    // 1) Først prøver vi å finne boken med ID
    //Linq med FirstOrDefault for å finne første bok som matcher ID, og ignorerer store/små bokstaver
    Book? book = books.FirstOrDefault(b => b.BookId.Equals(input, StringComparison.OrdinalIgnoreCase));

    // 2) Hvis ikke ser vi om det kan søkes etter tittel
    if (book == null)
    {
        // Små bruk av Linq for å finne bøker som inneholder input i tittelen, og ignorerer store/små bokstaver
        // Filterer med Where. Contains for delvis match, og ignorerer store/små bokstaver
        // ToList: Konverterer resultatet til en liste for enklere håndtering videre
        var matches = books
            .Where(b => b.Title.Contains(input, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (matches.Count == 0)
        {
            Console.WriteLine("Book not found.");
            return;
        }
        else if (matches.Count == 1)
        {
            book = matches[0];
        }
        else
        {
            Console.WriteLine("Multiple books found:");
            for (int i = 0; i < matches.Count; i++)
                Console.WriteLine($"[{i}] {matches[i].BookId} - {matches[i].Title}");

            Console.WriteLine("Choose book number:");
            int choice = int.Parse(Console.ReadLine());
            book = matches[choice];
        }
    }
// 3) Sjekk om det finnes tidligere lån for denne boken, og skriv ut historikk hvis det finnes
 var history = loans.Where(l => l.BookId == book.BookId && l.IsReturned);

    if (history.Any())
    {
        Console.WriteLine("\nPrevious loans for this book:");
        foreach (var loan in history)
        {
            Console.WriteLine(
                $"Borrower: {loan.UserId}, Loaned: {loan.LoanDate}, Returned: {loan.ReturnDate}"
            );
        }
        Console.WriteLine();
    }
    else
    {
        Console.WriteLine("\nNo previous loan history for this book.\n");
    }

    // 4) Sjekk tilgjengelighet
    if (book.Available <= 0)
    {
        Console.WriteLine("No copies available.");
        return;
    }

    Console.WriteLine("Enter user ID (student or employee):");
    string? userId = Console.ReadLine();

    // 5) Finn bruker (student eller employee)
    User? user =
        students.FirstOrDefault(s => s.StudentId == userId) as User ??
        employees.FirstOrDefault(e => e.EmployeeId == userId) as User;

    if (user == null)
    {
        Console.WriteLine("Invalid user ID.");
        return;
    }

    // 6) Opprett lån
    Loan newLoan = new(
        Guid.NewGuid().ToString(),
        userId,
        book.BookId,
        DateTime.Now,
        DateTime.Now.AddDays(14)
    );

    loans.Add(newLoan);

    // 7) Reduser tilgjengelige eksemplarer
    book.Available--;

    Console.WriteLine($"Book '{book.Title}' loaned to {user.Name}, forfall {newLoan.DueDate:yyyy-MM-dd}. Left available: {book.Available}");
}

//         [7] Return book
void ReturnBook(List<Loan> loans, List<Book> books)
{
    Console.WriteLine("Enter your user ID:");
    string userId = Console.ReadLine();

    Console.WriteLine("Enter book ID to return:");
    string bookId = Console.ReadLine();

    Book book = books.FirstOrDefault(b => b.BookId == bookId);

    if (book != null)
    {
        var loan = loans.FirstOrDefault(l =>
            l.BookId == bookId && // Sjekker både bookId og userId for å finne riktig lån.
            l.UserId == userId && // Uten l.UserId kunne man levert en bok som en annen har lånt, hvis
            l.ReturnDate == null);

        if (loan != null)
        {
            loan.ReturnDate = DateTime.Now;   //marker som returnert
            book.Available++;

            Console.WriteLine($"Book {book.Title} returned.");
        }
        else
        {
            Console.WriteLine("No active loan found for this book.");
        }
    }
    else
    {
        Console.WriteLine("Book not found.");
    }
}


//         [8] Register book
void RegisterBook(List<Book> books)
{
    string bookId;
    string title;
    string author;
    int year;

    // Book ID
    do
    {
        Console.WriteLine("Enter book ID:");
        bookId = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(bookId)) //Sjekker om input er null, tom eller bare whitespace (Så programmet ikke crasher)
            Console.WriteLine("Book ID cannot be empty. Try again.");
    }
    while (string.IsNullOrWhiteSpace(bookId));

    // Title
    do
    {
        Console.WriteLine("Enter book title:");
        title = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(title))
            Console.WriteLine("Title cannot be empty. Try again.");
    }
    while (string.IsNullOrWhiteSpace(title));

    // Author
    do
    {
        Console.WriteLine("Enter book author:");
        author = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(author))
            Console.WriteLine("Author cannot be empty. Try again.");
    }
    while (string.IsNullOrWhiteSpace(author));

    // Year (must be a number)
    while (true)
    {
        Console.WriteLine("Enter book year:");
        string input = Console.ReadLine();

        if (int.TryParse(input, out year))
            break;

        Console.WriteLine("Invalid year. Please enter a number.");
    }

    // Create book
    Book newBook = new(bookId, title, author, year, available: 1);
    books.Add(newBook);

    Console.WriteLine($"Book '{title}' registered successfully.");
}
//         [10] Add new user (Student, Employee, ExchangeStudent)
void AddUser(List<Student> students, List<Employee> employees, List<ExchangeStudent> exchangeStudents, List<User> users)
{
    Console.WriteLine("Choose user type:");
    Console.WriteLine("[1] Student");
    Console.WriteLine("[2] Employee");
    Console.WriteLine("[3] Exchange Student");

    string choice = Console.ReadLine();

    Console.WriteLine("Enter name:");
    string name = Console.ReadLine();

    Console.WriteLine("Enter email:");
    string email = Console.ReadLine();

    Console.WriteLine("Enter ID:");
    string id = Console.ReadLine();

    if (choice == "1")
    {
        Console.WriteLine("Enter study program:");
        string program = Console.ReadLine();

        Student newStudent = new(name, email, id, program);
        students.Add(newStudent);
        users.Add(newStudent);

        Console.WriteLine($"Student {name} added successfully.");
    }
    else if (choice == "2")
    {
        Console.WriteLine("Enter employee position:");
        string position = Console.ReadLine();

        Console.WriteLine("Enter department:");
        string department = Console.ReadLine();

        Employee newEmployee = new(name, email, id, position, department);
        employees.Add(newEmployee);
        users.Add(newEmployee);

        Console.WriteLine($"Employee {name} added successfully.");
    }
    else if (choice == "3")
    {
        Console.WriteLine("Enter study program:");
        string? program = Console.ReadLine();

        Console.WriteLine("Enter home university:");
        string? homeUni = Console.ReadLine();

        Console.WriteLine("Enter home country:");
        string? homeCountry = Console.ReadLine();

        Console.WriteLine("Enter start date (yyyy-mm-dd):");
        DateTime start = DateTime.Parse(Console.ReadLine());

        Console.WriteLine("Enter end date (yyyy-mm-dd):");
        DateTime end = DateTime.Parse(Console.ReadLine());

        ExchangeStudent newEx = new(name, email, id, program, homeUni, homeCountry, start, end);
        exchangeStudents.Add(newEx);
        users.Add(newEx);

        Console.WriteLine($"Exchange student {name} added successfully.");
    }
    else
    {
        Console.WriteLine("Invalid choice.");
    }
}

//---kjører programmet i en loop til brukeren velger å avslutte---
bool running = true;
while (running)
{
    Console.WriteLine("[1] Create course");
    Console.WriteLine("[2] Register or unregister student from a course");
    Console.WriteLine("[3] Print course and participants");
    Console.WriteLine("[4] Search for courses");
    Console.WriteLine("[5] Search for book");
    Console.WriteLine("[6] Loan book");
    Console.WriteLine("[7] Return book");
    Console.WriteLine("[8] Register book");
    Console.WriteLine("[9] View all users");
    Console.WriteLine("[10] Add new user");
    Console.WriteLine("[0] End");
    Console.WriteLine("Enter your choice:");
    
    string input = Console.ReadLine();
    //Kaller metoder og sender inn lister for å sjekke
    if (input == "1")
        AddCourse(courses);

    else if (input == "2")
        RegisterStudent(students, courses);

    else if (input == "3")
        PrintCourses(courses);

    else if (input == "4")
        SearchCourse(courses);

    else if (input == "5")
        SearchBook(books);

    else if (input == "6")
        LoanBook(books, loans, students, employees);

    else if (input == "7")
        ReturnBook(loans, books);

    else if (input == "8")
        RegisterBook(books);

    else if (input == "9")
    {
        Console.WriteLine("\n  --- Viewing all users ---");
        foreach (var user in users)
        {
            user.PrintUserInfo(); // Polymorfisme: Kaller PrintUserInfo() for hver bruker, og skriver ut informasjon basert på deres type (Student, Employee, ExchangeStudent)
        }
    }
    else if (input == "10")
        AddUser(students, employees, exchangeStudents, users);

    else if (input == "0")
        running = false;

    else
        Console.WriteLine("Invalid input, please try again.");
    Skip();
}