
using System.Reflection.Metadata;
using Application;
using System.Linq;
//----Lager tomme lister for å holde på data----

// --- Gamle lister som ikke brukes i alle met
//List<Student> students = new();
//List<ExchangeStudent> exchangeStudents = new();
//List<Employee> employees = new();

List<Book> books = new();
List<Course> courses = new();
List<Loan> loans = new();

//Konstruktør: (ID, Tittel, Forfatter, År, eksemplarer tilgjengelig)
var b1 = new Book("B001", "C# Programming", "John Doe", 2020, 20);
var b2 = new Book("B002", "Data Structures", "Jane Smith", 2018, 15);
books.AddRange(new List<Book> { b1, b2 }); // Legger til bøker i listen


// Konstruktør: (ID, Navn, Studiepoeng, Antallplasse, Påkrevd studieprogram)
var c1 = new Course("C001", "Introduction to Programming", 5, 30, "IT");
courses.Add(c1);

// ---- Liste for polymorfisme ----
List<User> users = new(); // Ny liste for polymorfisme, holder alle typer brukere (Student, Employee, ExchangeStudent)

// Legg inn startdata

// 1. Student
// Legger til "id: " og "name: " for å gjøre koden lettere å lese, og for å unngå feil med rekkefølge av parametere
var s1 = new Student(
    id: "S001",
    name: "Alex Guttorm",
    email: "alex@gmail.com",
    username: "alexg",
    password: "1234",
    studyProgram: "IT"

);
//students.Add(s1);   // brukes av systemet *trenger ikke med ny system*
users.Add(s1);      // brukes til polymorfisme

// 2. Employee
var e1 = new Employee(
    id: "E001",
    name: "Jose Ermano Alexander Westergaard",
    email: "Jose333@gmail.com",
    username: "josew",
    password: "12412",
    department: "IT",
    position: "Lecturer"
);
//employees.Add(e1);
users.Add(e1);

// 3. ExchangeStudent
var ex1 = new ExchangeStudent(
    id: "EX001",
    name: "Bobby Alberta",
    email: "Bob333@gmail.com",
    username: "bob333",
    password: "49494",
    studyProgram: "IT",
    homeUniversity: "MIT",
    country: "USA",
    periodFrom: DateTime.Now,
    periodTo: DateTime.Now.AddMonths(6)
);
//exchangeStudents.Add(ex1);
users.Add(ex1);
/*
ex1.PrintUserInfo(); // Skriver ut all informasjon om exchange student, inkludert navn og e-post 
*/


//                                                                      ------METODER------ 

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
    string courseId = Console.ReadLine();

    Console.WriteLine("Enter course name:");
    string courseName = Console.ReadLine();

    Console.WriteLine("Enter course credit:");
    int courseCredit = int.Parse(Console.ReadLine());

    Console.WriteLine("Enter course seats:");
    int courseSeats = int.Parse(Console.ReadLine());

    Console.WriteLine("Enter required study program:");
    string courseRe = Console.ReadLine();

    Course newCourse = new(courseId, courseName, courseCredit, courseSeats, courseRe);
    courses.Add(newCourse);
    Console.WriteLine($"Course {courseName} added successfully.");
}
   
// [2] Register or unregister student from a course
void RegisterStudent(List<User> users, List<Course> courses)
{
    Console.WriteLine("Do you want to:");
    Console.WriteLine("[1] Register student to course");
    Console.WriteLine("[2] Unregister student from course");
    string choice = Console.ReadLine();

    Console.WriteLine("Enter student ID:");
    string studentId = Console.ReadLine();

    Console.WriteLine("Enter course ID:");
    string courseId = Console.ReadLine();

    //Student student = students.FirstOrDefault(s => s.StudentId == studentId); **legacy**
    // Bruker OfType<Student>() for å filtrere ut bare Student-objekter fra users-listen, og deretter FirstOrDefault for å finne studenten med matching ID
    Student student = users
        .OfType<Student>()
        .FirstOrDefault(s => s.Id == studentId);

    Course course = courses.FirstOrDefault(c => c.CourseId == courseId);

    if (student == null)
    {
        Console.WriteLine("Student not found.");
        return;
    }

    if (course == null)
    {
        Console.WriteLine("Course not found.");
        return;
    }

    // --- REGISTER ---
    if (choice == "1")
    {
        if (course.EnrolledStudents.Contains(student))
        {
            Console.WriteLine("Student is already registered in this course.");
            return;
        }

        if (course.EnrolledStudents.Count >= course.CourseSeats)
        {
            Console.WriteLine("Course is full. Cannot register student.");
            return;
        }

        course.EnrolledStudents.Add(student);
        Console.WriteLine($"Student {student.Name} registered for course {course.CourseName}.");
    }

    // --- UNREGISTER ---
    else if (choice == "2")
    {
        if (!course.EnrolledStudents.Contains(student))
        {
            Console.WriteLine("Student is not enrolled in this course.");
            return;
        }

        course.EnrolledStudents.Remove(student);
        Console.WriteLine($"Student {student.Name} has been removed from course {course.CourseName}.");
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
        Console.WriteLine($"Credits: {course.CourseCredit}, Seats: {course.CourseSeats}");
        Console.WriteLine("Participants:");

        if (course.EnrolledStudents.Count == 0)
        {
            Console.WriteLine("  No students enrolled.");
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
    string courseName = Console.ReadLine();

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
    string bookTitle = Console.ReadLine();

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
void LoanBook(List<Book> books, List<Loan> loans, List<User> users)
{
    Console.WriteLine("Enter book ID or title to loan:");
    string input = Console.ReadLine();

    // 1) Først prøver vi å finne boken med ID
    Book book = books.FirstOrDefault(b => b.BookId.Equals(input, StringComparison.OrdinalIgnoreCase));

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

            Console.WriteLine("Input book number:");
            // Feilhåndtering på valg av bok, hvis vi skriver bokstaver vil ikke den krasje.
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice >= matches.Count)
            {
                Console.WriteLine("Invalid choice.");
                return;
            }

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

    Console.WriteLine("Enter user ID:");
    string userId = Console.ReadLine();

    // 5) Finn bruker (student eller employee)
    User user =
        users.FirstOrDefault(u => u.Id == userId);
        //students.FirstOrDefault(s => s.StudentId == userId) as User ??;  *Gammelt*
        //employees.FirstOrDefault(e => e.EmployeeId == userId) as User ));

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

    Console.WriteLine($"Book '{book.Title}' loaned to {user.Name}.");
}


//         [7] Return book
void ReturnBook(List<Loan> loans, List<Book> books)
{
    Console.WriteLine("Enter book ID to return:");
    string bookId = Console.ReadLine();

    Book book = books.FirstOrDefault(b => b.BookId == bookId);

    if (book != null)
    {
        var loan = loans.FirstOrDefault(l => l.BookId == bookId && l.ReturnDate == null);

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
//         [10] Add new user 
void AddUser(List<User> users)
{
    Console.WriteLine("Choose user type:");
    Console.WriteLine("[1] Student");
    Console.WriteLine("[2] Employee (base)");
    Console.WriteLine("[3] Exchange Student");
    Console.WriteLine("[4] Librarian");
    Console.WriteLine("[5] Teacher");
    Console.WriteLine("[6] Admin");

    string choice = Console.ReadLine();

    Console.WriteLine("Enter name:");
    string name = Console.ReadLine();

    Console.WriteLine("Enter email:");
    string email = Console.ReadLine();

    Console.WriteLine("Enter ID:");
    string id = Console.ReadLine();

    Console.WriteLine("Enter username:");
    string username = Console.ReadLine();

    Console.WriteLine("Enter password:");
    string password = Console.ReadLine();

    if (choice == "1")
    {
        Console.WriteLine("Enter study program:");
        string program = Console.ReadLine();

        var newStudent = new Student(id, name, email, username, password, program);
        users.Add(newStudent);

        Console.WriteLine($"Student {name} added successfully.");
    }
    else if (choice == "2")
    {
        Console.WriteLine("Enter employee position:");
        string position = Console.ReadLine();

        Console.WriteLine("Enter department:");
        string department = Console.ReadLine();

        var newEmployee = new Employee(id, name, email, username, password, department, position);
        users.Add(newEmployee);

        Console.WriteLine($"Employee {name} added successfully.");
    }
    else if (choice == "3")
    {
        Console.WriteLine("Enter study program:");
        string program = Console.ReadLine();

        Console.WriteLine("Enter home university:");
        string homeUni = Console.ReadLine();

        Console.WriteLine("Enter home country:");
        string homeCountry = Console.ReadLine();

        Console.WriteLine("Enter start date (yyyy-mm-dd):");
        DateTime start = DateTime.Parse(Console.ReadLine());

        Console.WriteLine("Enter end date (yyyy-mm-dd):");
        DateTime end = DateTime.Parse(Console.ReadLine());

        var newEx = new ExchangeStudent(id, name, email, username, password, program, homeUni, homeCountry, start, end);
        users.Add(newEx);

        Console.WriteLine($"Exchange student {name} added successfully.");
    }
    else if (choice == "4")
    {
        Console.WriteLine("Enter employee position:");
        string position = Console.ReadLine();

        Console.WriteLine("Enter department:");
        string department = Console.ReadLine();

        var newLib = new Librarian(id, name, email, username, password, department, position);
        users.Add(newLib);

        Console.WriteLine($"Librarian {name} added successfully.");
    }
    else if (choice == "5")
    {
        Console.WriteLine("Enter employee position:");
        string position = Console.ReadLine();

        Console.WriteLine("Enter department:");
        string department = Console.ReadLine();

        var newTeacher = new Teacher(id, name, email, username, password, department, position);
        users.Add(newTeacher);

        Console.WriteLine($"Teacher {name} added successfully.");
    }
    else if (choice == "6")
    {
        var newAdmin = new Admin(id, name, email, username, password);
        users.Add(newAdmin);

        Console.WriteLine($"Admin {name} added successfully.");
    }
    else
    {
        Console.WriteLine("Invalid choice.");
    }
}

//         [Extra] View loans for alle users
void ViewLoans(User user, List<Loan> loans, List<Book> books)
{
    Console.WriteLine($"\n--- Loans for {user.Name} ---");

    // Finn alle lån som tilhører brukeren
    var myLoans = loans.Where(l => l.UserId == user.Id).ToList();

    if (myLoans.Count == 0)
    {
        Console.WriteLine("You have no active loans.");
        return;
    }

    // Skriv ut alle lån
    foreach (var loan in myLoans)
    {
        var book = books.FirstOrDefault(b => b.BookId == loan.BookId);

        Console.WriteLine($"\nBook: {book?.Title}");
        Console.WriteLine($"Loan date: {loan.LoanDate.ToShortDateString()}");
        Console.WriteLine($"Due date: {loan.DueDate.ToShortDateString()}");

        if (loan.ReturnDate != null)
            Console.WriteLine($"Returned: {loan.ReturnDate.Value.ToShortDateString()}");
        else
            Console.WriteLine("Status: Not returned");
    }
}


void ViewLoanHistory(List<Loan> loans, List<Book> books)

{
    Console.WriteLine("\n--- Loan History ---");

    var history = loans.Where(l => l.ReturnDate != null).ToList();

    if (history.Count == 0)
    {
        Console.WriteLine("There is no loan history.");
        return;
    }

    foreach (var loan in history)
    {
        var book = books.FirstOrDefault(b => b.BookId == loan.BookId);

        Console.WriteLine($"\nBook: {book?.Title}");
        Console.WriteLine($"Borrower: {loan.UserId}");
        Console.WriteLine($"Loan date: {loan.LoanDate.ToShortDateString()}");
        Console.WriteLine($"Returned: {loan.ReturnDate.Value.ToShortDateString()}");
    }
}
//         [Extra] View active loans for librarians
void ViewActiveLoans(List<Loan> loans, List<Book> books)
{
    Console.WriteLine("\n--- Active Loans ---");

    var active = loans.Where(l => l.ReturnDate == null).ToList();

    if (active.Count == 0)
    {
        Console.WriteLine("There are no active loans.");
        return;
    }

    foreach (var loan in active)
    {
        var book = books.FirstOrDefault(b => b.BookId == loan.BookId);

        Console.WriteLine($"\nBook: {book?.Title}");
        Console.WriteLine($"Borrower: {loan.UserId}");
        Console.WriteLine($"Loan date: {loan.LoanDate.ToShortDateString()}");
        Console.WriteLine($"Due date: {loan.DueDate.ToShortDateString()}");
    }
}
//   ----Ulike menyer BASERT PÅ USER----

//           Admin menu metode
//       Bruker metodene [1-10] som kalles etter innlogging
// Dette er menyen for admin, som kalles etter innlogging.
void AdminMenu(User user, List<User> users, List<Book> books, List<Course> courses, List<Loan> loans)
{
    bool adminRunning = true; // adminRunning: Kontrollerer løkken for adminmenyen, og holder den gående til brukeren velger å logge ut (adminRunning = false)

    while (adminRunning)
    {
        Console.WriteLine($"\n--- Admin ({user.Name}) ---");
        Console.WriteLine("\n[1] Create course");
        Console.WriteLine("\n[2] Register or unregister student from a course");
        Console.WriteLine("\n[3] Print course and participants");
        Console.WriteLine("\n[4] Search for courses");
        Console.WriteLine("\n[5] Search for book");
        Console.WriteLine("\n[6] Loan book");
        Console.WriteLine("\n[7] Return book");
        Console.WriteLine("\n[8] Register book");
        Console.WriteLine("\n[9] View all users");
        Console.WriteLine("\n[10] Add new user");
        Console.WriteLine("\n[11] View my loans");
        Console.WriteLine("\n[0] End");
        Console.WriteLine("Enter your choice:");
        
        string input = Console.ReadLine();
        //Kaller metoder og sender inn lister for å sjekke
        if (input == "1")
            AddCourse(courses);

        else if (input == "2")
            RegisterStudent(users, courses);

        else if (input == "3")
            PrintCourses(courses);

        else if (input == "4")
            SearchCourse(courses);

        else if (input == "5")
            SearchBook(books);

        else if (input == "6")
            LoanBook(books, loans, users);

        else if (input == "7")
            ReturnBook(loans, books);

        else if (input == "8")
            RegisterBook(books);

        else if (input == "9")
        {
            Console.WriteLine("\n  --- Viewing all users ---");
            foreach (var u in users)
            {
                u.PrintUserInfo(); // Polymorfisme: Kaller PrintUserInfo() for hver bruker, og skriver ut informasjon basert på deres type (Student, Employee, ExchangeStudent)
            }
        }
        else if (input == "10")
            AddUser(users);

        else if (input == "0")
            adminRunning = false;

        else if (input == "11")
            ViewLoans(user, loans, books);
        else
            Console.WriteLine("Invalid input, please try again.");
        Skip();
    }
}

// Librarian menu metode 
void LibrarianMenu(User user, List<User> users, List<Book> books, List<Course> courses, List<Loan> loans)
{
    bool running = true;

    while (running)
    {
        Console.WriteLine($"\n--- Librarian ({user.Name}) ---");
        Console.WriteLine("[1] Search for book");
        Console.WriteLine("[2] Register book");
        Console.WriteLine("[3] Loan book");
        Console.WriteLine("[4] Return book");
        Console.WriteLine("[5] View active loans");
        Console.WriteLine("[6] View loan history");
        Console.WriteLine("[7] View my loans");
        Console.WriteLine("[0] Log out");

        string input = Console.ReadLine();

        if (input == "1") SearchBook(books);
        else if (input == "2") RegisterBook(books);
        else if (input == "3") LoanBook(books, loans, users);
        else if (input == "4") ReturnBook(loans, books);
        else if (input == "5") ViewActiveLoans(loans, books);
        else if (input == "6") ViewLoanHistory(loans, books);
        else if (input == "7") ViewLoans(user, loans, books);
        else if (input == "0") running = false;
        else Console.WriteLine("Invalid input.");

        Skip();
    }
}

// Teacher menu metode  
void TeacherMenu(User user, List<User> users, List<Book> books, List<Course> courses, List<Loan> loans)
{
    bool running = true;

    while (running)
    {
        Console.WriteLine($"\n--- Teacher ({user.Name}) ---");
        Console.WriteLine("[1] Create course");
        Console.WriteLine("[2] Register/unregister student");
        Console.WriteLine("[3] Print course and participants");
        Console.WriteLine("[4] Search for course");
        Console.WriteLine("[5] Search for book");
        Console.WriteLine("[6] Loan book");
        Console.WriteLine("[7] Return book");
        Console.WriteLine("[8] View my loans");
        Console.WriteLine("[0] Log out");

        string input = Console.ReadLine();

        if (input == "1") AddCourse(courses);
        else if (input == "2") RegisterStudent(users, courses);
        else if (input == "3") PrintCourses(courses);
        else if (input == "4") SearchCourse(courses);
        else if (input == "5") SearchBook(books);
        else if (input == "6") LoanBook(books, loans, users);
        else if (input == "7") ReturnBook(loans, books);
        else if (input == "8") ViewLoans(user, loans, books);
        else if (input == "0") running = false;
        else Console.WriteLine("Invalid input.");

        Skip();
    }
}

// Employee menu for andre ansatte
void EmployeeMenu(User user, List<User> users, List<Book> books, List<Course> courses, List<Loan> loans)
{
    bool running = true;

    while (running)
    {
        Console.WriteLine($"\n--- Employee ({user.Name}) ---");
        Console.WriteLine("[1] Search for book");
        Console.WriteLine("[2] Loan book");
        Console.WriteLine("[3] Return book");
        Console.WriteLine("[4] View my info");
        Console.WriteLine("[5] View my loans");
        Console.WriteLine("[0] Log out");

        string input = Console.ReadLine();

        if (input == "1") SearchBook(books);
        else if (input == "2") LoanBook(books, loans, users);
        else if (input == "3") ReturnBook(loans, books);
        else if (input == "4") user.PrintUserInfo();
        else if (input == "5") ViewLoans(user, loans, books);
        else if (input == "0") running = false;
        else Console.WriteLine("Invalid input.");

        Skip();
    }
}

// Metode for studentmenyen, som kalles etter innlogging. Viser færre valg enn ansattmenyen, og ingen alternativer for å redigere kurs eller brukere.
void StudentMenu(User user, List<User> users, List<Book> books, List<Course> courses, List<Loan> loans)
{
    bool studentRunning = true;

    while (studentRunning)
    {
        Console.WriteLine("\n--- STUDENT MENU ---");
        Console.WriteLine("[1] View profile");
        Console.WriteLine("[2] Search for courses");
        Console.WriteLine("[3] View all courses");
        Console.WriteLine("[4] Search for book");
        Console.WriteLine("[5] Loan book");
        Console.WriteLine("[6] Return book");
        Console.WriteLine("[7] View my loans");
        Console.WriteLine("[0] Log out");

        string input = Console.ReadLine();

        if (input == "1")
        {
            Console.WriteLine("\n--- PROFILE ---");
            user.PrintUserInfo();
        }
        else if (input == "2")
        {
            SearchCourse(courses);
        }
        else if (input == "3")
        {
            PrintCourses(courses);
        }
        else if (input == "4")
        {
            SearchBook(books);
        }
        else if (input == "5")
        {
            LoanBook(books, loans, users); 
        }
        else if (input == "6")
        {
            ReturnBook(loans, books);
        }
        else if (input == "7")
        {
            ViewLoans(user, loans, books);
        }
        else if (input == "0")
        {
            studentRunning = false;
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }

        Skip();
    }
}

// Login menu metode, som sjekker brukernavn og passord, og sender brukeren til riktig meny basert på rolle
void Login(List<User> users, List<Book> books, List<Course> courses, List<Loan> loans)
{
    Console.WriteLine("Enter username:");
    string username = Console.ReadLine();

    Console.WriteLine("Enter password:");
    string password = Console.ReadLine();

    User user = users.FirstOrDefault(u => u.Username == username && u.Password == password);

    if (user == null)
    {
        Console.WriteLine("Invalid login.");
        return;
    }

    // HVIS ROLLE MENY
    if (user.Role == "Admin")
        AdminMenu(user, users, books, courses, loans);

    else if (user.Role == "Librarian")
        LibrarianMenu(user, users, books, courses, loans);

    else if (user.Role == "Teacher")
        TeacherMenu(user, users, books, courses, loans);

    else if (user.Role == "Employee")
        EmployeeMenu(user, users, books, courses, loans);

    else if (user.Role == "Student" || user.Role == "ExchangeStudent")
        StudentMenu(user, users, books, courses, loans); 

    else
        Console.WriteLine("Unknown role.");
}
// Register menu metode
void RegisterUser(List<User> users)
{
    AddUser(users);
}
//---kjører programmet i en loop til brukeren velger å avslutte---
bool running = true;
while (running)

{
    Console.WriteLine("\n--- MAIN MENU ---");
    Console.WriteLine("1. Log in");
    Console.WriteLine("2. Register new user");
    Console.WriteLine("3. Exit");
    
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Login(users, books, courses, loans);
            break;

        case "2":
            RegisterUser(users);
            break;

        case "3":
            running = false;
            break;

        default:
            Console.WriteLine("Invalid choice.");
            break;
    }
}

