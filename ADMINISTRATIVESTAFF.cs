using System;

namespace BPG402
{
    public class ADMINISTRATIVESTAFF : PERSON //AdministrativeStaff class inherits from Person class, represents administrative staff member in the university system
    {
        public string Position { get; set; } //Position of the administrative staff member
        public string Office { get; set; }  //Office of administrative staff member
        private UNIVERSITYSYSTEMMANAGEMENT uniDB; //Reference To perform operations, adding students & professors, fetching data

        public ADMINISTRATIVESTAFF(int id, string name, string email, string pos, string office, UNIVERSITYSYSTEMMANAGEMENT sys) //Constructor to initialize properties of AdministrativeStaff class
            : base(id, name, email)
        {
            Position = pos;
            Office = office;
            uniDB = sys;
        }

        public override void PRINTINFO()
        {
            Console.WriteLine($"[Admin] ID: {ID} | Name: {Name} | Position: {Position} | Office: {Office}"); //Print administrative staff information (it shows [Admin] for administrative staff)
        }

        public void AddStudent(STUDENT s) { uniDB.AddStudent(s); } //Add student to system by calling AddStudent method of UniversitySystemManagement class
        public void ShowAllStudents() //It shows students data (ID, Name, Email, Major, GPA) + if there is graduate or still student
        {
            Console.WriteLine("\n--- All Students ---");
            if (uniDB.StudentNum == 0)
            {
                Console.WriteLine("No Students available");
                return;
            }
            
            for (int i = 0; i < uniDB.StudentNum; i++)
            {
                if (uniDB.Students[i] != null)
                { //Check if student slot isn't null to avoid null reference errors
                    uniDB.Students[i].PRINTINFO(); //Print student information
                    Console.WriteLine();
                }
            }
        }
       
        public void FindStudent(int id) //Find student by ID and print it if found, if not founded then it will print "Student not found."
        {
            for (int i = 0; i < uniDB.StudentNum; i++)
            {
                if (uniDB.Students[i] != null && uniDB.Students[i].ID == id) //Check if student slot isn't null and student ID matches the searched ID to avoid null reference errors
                {
                    uniDB.Students[i].PRINTINFO();
                    return;
                }
            }
            Console.WriteLine("Student not found.");
        }
        public int AutoStudentID() { return uniDB.StudentNum + 1; } //Automatic student id, so each new student gets a unique id


        public void AddProfessor(PROFESSOR p) { uniDB.AddProfessor(p); } //Add professor to system by calling AddProfessor method of UniversitySystemManagement class
        public void ShowAllProfessors() //It show all professors data (ID, Name, Email, Department, Salary)
        {
            Console.WriteLine("\n--- All Professors ---");
            if (uniDB.ProfessorNum == 0)
            {
                Console.WriteLine("No Professors available");
                return;
            }
            
            for (int i = 0; i < uniDB.ProfessorNum; i++)
            {
                if (uniDB.Professors[i] != null)
                { //Check if professor slot isn't null to avoid null reference errors
                    uniDB.Professors[i].PRINTINFO(); //Print professor information
                }
            }
        }
        public void FindProfessor(int id) //Fetch professor information by id and print it if found, if not founded then, print "Professor not found."
        {
            for (int i = 0; i < uniDB.ProfessorNum; i++)
            {
                if (uniDB.Professors[i] != null && uniDB.Professors[i].ID == id) //Check if professor slot isn't null and professor ID matches the searched ID to avoid null reference errors
                {
                    uniDB.Professors[i].PRINTINFO();
                    return;
                }
            }
            Console.WriteLine("Professor not found.");
        }
        public int AutoProfessorID() { return uniDB.ProfessorNum + 1; } //Automatic professor id, so each new professor gets a unique id
    }
}