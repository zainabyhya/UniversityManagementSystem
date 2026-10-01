using System;

namespace BPG402
{
    internal class Program
    {
        static bool dataChanged = false; //To save data changes if there's any change in data to avoid repeating saving without need
        static void Main(string[] args)
        {
            UNIVERSITYSYSTEMMANAGEMENT uniDB = new UNIVERSITYSYSTEMMANAGEMENT(); //Create instance of UNIVERSITYSYSTEMMANAGEMENT class

            //Exception handling for loading data from files to system (STUDENTS, PROFESSORS, COURSES), if issue appear with files it will show error message without crashing
            try
            {
                //Load files: STUDENTS.txt, PROFESSORS.txt, COURSES.txt to the system 
                FILEMANAGER.LoadStudents("STUDENTS.txt", uniDB);
                FILEMANAGER.LoadProfessors("PROFESSORS.txt", uniDB);
                FILEMANAGER.LoadCourses("COURSES.txt", uniDB);
                uniDB.LnkCoursesToProf();
            }
            catch (Exception Ex)
            {
                //Print error message if there's issue with loading files
                Console.WriteLine("Error loading files " + Ex.Message);
            }


            //Create AdministrativeStaff
            ADMINISTRATIVESTAFF admin = new ADMINISTRATIVESTAFF(1, "Admin User", "admin@svuonline.org", "Admin", "A1", uniDB);

            bool exit = false;
            do
            {
                //The Menu for admin system:
                Console.WriteLine("\n===========================================");
                Console.WriteLine($" Operator: {admin.Name} | {admin.Position} | Office: {admin.Office}");
                Console.WriteLine("===========================================");
                Console.WriteLine("\n===== UNIVERSITY SYSTEM MANAGEMENT =====");
                Console.WriteLine("1. Show All Students");
                Console.WriteLine("2. Show All Professors");
                Console.WriteLine("3. Show All Courses");
                Console.WriteLine("4. Find Student By ID");
                Console.WriteLine("5. Find Professor By ID");
                Console.WriteLine("6. Add New Student (Regular)");
                Console.WriteLine("7. Add New Graduate Student");
                Console.WriteLine("8. Add New Professor");
                Console.WriteLine("9. Add New Course");
                Console.WriteLine("0. Save & Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();  //Input choice

                switch (choice)
                {
                    case "1":
                        admin.ShowAllStudents();
                        break;
                    case "2":
                        admin.ShowAllProfessors();
                        break;
                    case "3":
                        Console.WriteLine("\n--- All Courses ---");
                        uniDB.ShowCourses(); //It show all courses data: Course ID, Course Name, Credits, Professor ID)
                        break;
                    case "4":
                        admin.FindStudent(GetValidID("Student"));
                        break;
                    case "5":
                        admin.FindProfessor(GetValidID("Professor"));
                        break;
                    case "6":
                        AddNewStudent(admin, false); //For choosing to add a new student to system (false for regular student)
                        break;
                    case "7":
                        AddNewStudent(admin, true); //For choosing to add a new professor to system (true for graduate student)
                        break;
                    case "8":
                        AddNewProfessor(admin);
                        break;
                    case "9":
                        AddNewCourse(uniDB);
                        break;
                    case "0":
                        if (dataChanged)
                        {
                            FILEMANAGER.SaveAllStudents("STUDENTS.txt", uniDB);
                            FILEMANAGER.SaveAllProfessors("PROFESSORS.txt", uniDB);
                            FILEMANAGER.SaveAllCourses("COURSES.txt", uniDB);
                        }
                        Console.WriteLine("Saving data and Exiting program...");
                        exit = true;  //To Exit the program
                        break;
                    default:
                        Console.WriteLine("Invalid option"); //If entered invalid option it will show this message and return to menu
                        break;
                }
            } while (!exit);
        }
        //Validate input for: name, major, department to accept only letters and spaces
        static string ChkInput(string msg)
        {
            string input;
            bool chkString;
            do
            {
                chkString = true;
                Console.Write(msg);
                input = Console.ReadLine();


                if (input == null || input.Trim() == "") //Ensure that the input isn't null or empty to be valid
                {
                    chkString = false;
                }
                else
                {
                    foreach (char c in input)
                    {
                        bool Lower = (c >= 'a' && c <= 'z');
                        bool Upper = (c >= 'A' && c <= 'Z');
                        bool Space = (c == ' ');

                        if (!(Lower || Upper || Space))
                        {
                            chkString = false;
                            break;
                        }
                    }
                }

                if (!chkString) Console.WriteLine("Invalid input!");

            } while (!chkString);

            return input;
        }

        static void AddNewStudent(ADMINISTRATIVESTAFF admin, bool graduate) //To add a data for new student to system and save it in STUDENTS.txt
        {
            if (graduate)
            {
                Console.WriteLine("\n--- Add New Graduate Student ---");
            }
            else
            {
                Console.WriteLine("\n--- Add New Student ---");
            }
            string name = ChkInput("Name: "); //New student name accept only letters and spaces

            //Email input + email validation for new students
            string email;
            bool chkEmail;
            do
            {
                chkEmail = true;
                Console.Write("Email: ");
                email = Console.ReadLine(); //New student email
                if (email == null || email.Trim().Length == 0 || email.IndexOf("@") == -1 || email.IndexOf(".") == -1)
                {
                    chkEmail = false;
                    Console.WriteLine("Invalid Email!");
                }
            } while (!chkEmail);

            string major = ChkInput("Major: ");

            //GPA input + GPA validation for the new student (accept only numbers from 0 to 4.0)
            double gpa;
            Console.Write("GPA: ");
            while (!double.TryParse(Console.ReadLine(), out gpa) || gpa < 0 || gpa > 4.0)
            {
                Console.Write("Please Enter a number between 0.0 and 4.0: ");
            }

            //Add a new ID automatically for new student
            int id = admin.AutoStudentID();

            STUDENT newStu;
            if (graduate) //If graduate it will ask to write research topic
            {
                string research;
                bool chkResearch;
                do
                {
                    chkResearch = false;
                    Console.Write("Research Topic: ");
                    research = Console.ReadLine();
                    bool contLetter = false; 
                    bool AllowedChars = true; 
                    if (research != null && research.Trim() != "") //Ensure research topic isn't empty and contains 1 letter at least to be accept
                    {
                        foreach (char c in research)
                        {
                            //Accept letters in course name as a first priority
                            bool Lower = (c >= 'a' && c <= 'z');
                            bool Upper = (c >= 'A' && c <= 'Z');

                            if (Lower || Upper)
                            {
                                contLetter = true;
                            }

                            //Accept numbers, and spaces in course name as a second priority
                            bool Digit = (c >= '0' && c <= '9');
                            bool Space = (c == ' ');

                            if (!(Lower || Upper || Digit || Space))
                            {
                                AllowedChars = false;
                                break;
                            }
                        }
                    }
                    if (contLetter && AllowedChars)
                    {
                        chkResearch = true;
                    }

                    if (!chkResearch)
                    {
                        Console.WriteLine("Research must contain letters");
                    }
                } while (!chkResearch);

                newStu = new GRADUATESTUDENT(id, name, email, major, gpa, research); //If it's graduate student data will be (ID, Name, Email, Major, GPA, Research Topic)
            }
            else //If regular it won't ask to write research topic
            {
                newStu = new STUDENT(id, name, email, major, gpa); //If it's regular student data will be (ID, Name, Email, Major, GPA)
            }

            admin.AddStudent(newStu); //It add the new student to system
            FILEMANAGER.SaveStudent("STUDENTS.txt", newStu);  //Save new student data to STUDENTS.txt
            dataChanged = true;
            Console.WriteLine("Student added and saved to file.");
        }

        static void AddNewProfessor(ADMINISTRATIVESTAFF admin) //To add a data for new professor to system and save it in PROFESSORS.txt
        {
            Console.WriteLine("\n--- Add New Professor ---");

            string name = ChkInput("Name: ");


            //Email validation for new professor and its email input
            string email;
            bool chkEmail;
            do
            {
                chkEmail = true;
                Console.Write("Email: ");
                email = Console.ReadLine(); //Enter email of the new professor
                if (email == null || email.Trim().Length == 0 || email.IndexOf("@") == -1 || email.IndexOf(".") == -1) //Ensure email contains @ . to be valid
                {
                    chkEmail = false;
                    Console.WriteLine("Invalid Email!");
                }
            } while (!chkEmail);

            //Check for new professor and its department input
            string dept = ChkInput("Department: ");


            //Check for new professor and its salary input (accept only positive numbers)
            double salary;
            Console.Write("Salary: "); //Enter salary of the new professor + It should be a positive number to be accepted
            while (!double.TryParse(Console.ReadLine(), out salary) || salary < 0)
            {
                Console.Write("Invalid! Please enter a positive number: ");
            }

            int id = admin.AutoProfessorID();  //New ID automatically for new professor

            PROFESSOR newProf = new PROFESSOR(id, name, email, dept, salary); //Create a new professor object with the entered data
            admin.AddProfessor(newProf);   //It add the new professor to the system
            FILEMANAGER.SaveProfessor("PROFESSORS.txt", newProf);  //Save the new professor data to PROFESSORS.txt
            dataChanged = true;
            Console.WriteLine("Professor added and saved to file.");
        }
        //It's adding data for new course to system and save it in COURSES.txt
        static void AddNewCourse(UNIVERSITYSYSTEMMANAGEMENT uniDB)
        {
            Console.WriteLine("\n--- Add New Course ---");

            string courseName;
            bool chkCN; //"Check Course Name"

            do
            {
                chkCN = false;
                bool contLetter = false; //Ensure that course name contains at least 1 letter to be accept
                bool AllowedChars = true; //Ensure that course name contains only letters, numbers, and spaces to be accept

                Console.Write("Course Name: ");
                courseName = Console.ReadLine();
                //For check that course name isn't empty and contains at least 1 letter, and only allowed characters (letters, numbers, spaces) so the user can't enter invalid course name such as numbers or special characters or empty ones
                if (courseName != null && courseName.Trim().Length > 0)
                {
                    foreach (char c in courseName)
                    {
                        //Accept letters in course name as a first priority
                        bool Lower = (c >= 'a' && c <= 'z');
                        bool Upper = (c >= 'A' && c <= 'Z');

                        if (Lower || Upper)
                        {
                            contLetter = true;
                        }

                        //Accept numbers, and spaces in course name as a second priority
                        bool Digit = (c >= '0' && c <= '9');
                        bool Space = (c == ' ');

                        if (!(Lower || Upper || Digit || Space))
                        {
                            AllowedChars = false;
                            break;
                        }
                    }
                }

                //To be accepted, course name must contain only allowed characters (letters, numbers, spaces)
                if (contLetter && AllowedChars)
                {
                    chkCN = true;
                }

                if (!chkCN)
                {
                    Console.WriteLine("Invalid course name!");
                }

            } while (!chkCN);

            //Credits input + credits validation for new course (accept only positive integers)
            int credits;
            Console.Write("Credits: ");
            while (!int.TryParse(Console.ReadLine(), out credits) || credits <= 0)
            {
                Console.Write("Invalid input! Enter positive number for Credits: ");
            }
            //Professor ID input + professor ID validation for new course (accept only positive integers)
            int profId;
            Console.Write("Professor ID: ");
            while (!int.TryParse(Console.ReadLine(), out profId) || profId <= 0)
            {
                Console.Write("Invalid input! Enter Professor ID: ");
            }
            bool profFound = false;
            for (int i = 0; i < uniDB.ProfessorNum; i++)
            {
                if (uniDB.Professors[i].ID == profId)
                {
                    int courseId = uniDB.CourseNum + 1;
                    COURSE newCourse = new COURSE(courseId, courseName, credits, profId);
                    uniDB.AddCourse(newCourse);
                    uniDB.Professors[i].AssignCourse(newCourse);

                    FILEMANAGER.SaveCourse("COURSES.txt", newCourse);
                    dataChanged = true;
                    Console.WriteLine("Course added!");
                    return;
                }
            }
            if (!profFound)
            {
                Console.WriteLine("Professor ID not found.. Course was Not added.");
            }
        }
        static int GetValidID(string type)
        {
            int id;
            Console.Write($"Enter {type} ID: ");
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write($"Invalid input! Enter number for {type} ID: ");
            }
            return id;
        }
    }
}
