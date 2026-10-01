using System;
using System.IO;

namespace BPG402
{
    public static class FILEMANAGER
    {
        public static void LoadStudents(string fileName, UNIVERSITYSYSTEMMANAGEMENT sys)
        {
            StreamReader sr = null;

            //Handle errors when reading the file
            try
            {
                //Use StreamReader to read the file line by line
                sr = new StreamReader(fileName);
                string rdline;
                int lineCount = 0;
                //Read each line until the maximum capacity of students reached 50
                while ((rdline = sr.ReadLine()) != null && sys.StudentNum < UNIVERSITYSYSTEMMANAGEMENT.MAX_CAPACITY)
                {
                    lineCount++;
                    if (rdline == null || rdline.Trim().Length == 0) continue;
                    string[] data = rdline.Split(';');
                    if (data.Length == 5 || data.Length == 6)
                    {
                        bool IdValid = int.TryParse(data[0].Trim(), out int id);
                        bool GpaValid = double.TryParse(data[4].Trim(), out double gpa);
                        if (IdValid && GpaValid)
                        {
                            string name = data[1].Trim();
                            string email = data[2].Trim();
                            string major = data[3].Trim();

                            if (data.Length == 6) //If there are 6 fields for graduate student
                            {
                                string researchTopic = data[5].Trim();
                                sys.AddStudent(new GRADUATESTUDENT(id, name, email, major, gpa, researchTopic));
                            }
                            else //Regular student without research topic
                            {
                                sys.AddStudent(new STUDENT(id, name, email, major, gpa));
                            }
                        }
                        else
                        {
                            //Printed if there's invalid numeric data for ID or GPA
                            Console.WriteLine($"Error: Invalid numeric data in student line: {rdline}");
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                //Print error message if there is issue with reading STUDENTS.txt
                Console.WriteLine("STUDENTS.txt not found");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close(); //Close the file
                }
            }
        }

        //Save student data to STUDENTS.txt, if the student is a graduate student, add research topic in saved data
        public static void SaveStudent(string fileName, STUDENT s)  //Save Student and GraduateStudent data to file with inheritance
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(fileName, true);
                sw.WriteLine(s.Form()); //Save Student data without research topic for regular students + research topic for graduate students

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }
        //Save all students in the system to STUDENTS.txt, it will works before exit, Save both regular students and graduate students
        public static void SaveAllStudents(string fileName, UNIVERSITYSYSTEMMANAGEMENT sys)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(fileName, false);
                for (int i = 0; i < sys.StudentNum; i++)
                {
                    if (sys.Students[i] != null)
                        sw.WriteLine(sys.Students[i].Form());    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }


        //Load professor data from PROFESSORS.txt and add it to the system
        public static void LoadProfessors(string fileName, UNIVERSITYSYSTEMMANAGEMENT sys)
        {
            StreamReader sr = null;

            //Handle errors when reading the file
            try
            {
                //Use StreamReader to read the file line by line
                sr = new StreamReader(fileName);
                string rdline;

                //Read each line until the maximum capacity of professors reached 50
                while ((rdline = sr.ReadLine()) != null && sys.ProfessorNum < UNIVERSITYSYSTEMMANAGEMENT.MAX_CAPACITY)
                {
                    //Skip empty lines to avoid errors when splitting data
                    if (rdline == null || rdline.Trim().Length == 0) continue;
                    string[] data = rdline.Split(';');

                    //Check if it has 5 fields before trying to parse it
                    if (data.Length == 5)
                    {
                        bool IdValid = int.TryParse(data[0].Trim(), out int id);
                        bool SalaryValid = double.TryParse(data[4].Trim(), out double salary);
                        if (IdValid && SalaryValid)
                        {
                            string name = data[1].Trim();
                            string email = data[2].Trim();
                            string department = data[3].Trim();
                            sys.AddProfessor(new PROFESSOR(id, name, email, department, salary));
                        }
                        else
                        {
                            Console.WriteLine($"Error: Invalid numeric data in professors file: {rdline}");
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                //Print error message if there is issue with reading PROFESSORS.txt
                Console.WriteLine("PROFESSORS.txt not found");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (sr != null) sr.Close(); //Close the file
            }
        }



        public static void SaveProfessor(string fileName, PROFESSOR p) //Save Professor data to file with inheritance
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(fileName, true);
                sw.WriteLine($"{p.ID};{p.Name};{p.Email};{p.Department};{p.Salary}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in save professor: " + ex.Message);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }
        //Save all professors in the system to PROFESSORS.txt it will works before exit
        public static void SaveAllProfessors(string fileName, UNIVERSITYSYSTEMMANAGEMENT sys)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(fileName, false);
                for (int i = 0; i < sys.ProfessorNum; i++)
                {
                    PROFESSOR p = sys.Professors[i];
                    sw.WriteLine($"{p.ID};{p.Name};{p.Email};{p.Department};{p.Salary}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }


        //Load course data from COURSES.txt and add it to the system
        public static void LoadCourses(string fileName, UNIVERSITYSYSTEMMANAGEMENT sys)
        {
            StreamReader sr = null;

            //Handle errors when reading the file
            try
            {
                //Use StreamReader to read the file line by line
                sr = new StreamReader(fileName);
                string rdline;
                //Read each line until the maximum capacity of students reached 50
                while ((rdline = sr.ReadLine()) != null && sys.CourseNum < UNIVERSITYSYSTEMMANAGEMENT.MAX_CAPACITY)
                {
                    string[] data = rdline.Split(';');
                    if (rdline == null || rdline.Trim().Length == 0) continue;

                    if (data.Length == 4)
                    {
                        bool IdValid = int.TryParse(data[0].Trim(), out int id);
                        bool CreditsValid = int.TryParse(data[2].Trim(), out int credits);
                        bool ProfIdValid = int.TryParse(data[3].Trim(), out int profId);

                        if (IdValid && CreditsValid && ProfIdValid)
                        {
                            string CourseName = data[1].Trim();
                            sys.AddCourse(new COURSE(id, CourseName, credits, profId));
                        }
                        else
                        {
                            Console.WriteLine($"Error: Invalid numeric data in courses file: {rdline}");
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                //Print error message if there is issue with reading COURSES.txt
                Console.WriteLine("COURSES.txt not found");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (sr != null)
                { 
                    sr.Close();
                }//Close the file
            }
        }
        public static void SaveCourse(string fileName, COURSE c) //Save Course data to file with inheritance
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(fileName, true);
                //Save Course data: ID; Course name; Credits; Professor ID
                sw.WriteLine($"{c.CourseID};{c.CourseName};{c.Credits};{c.ProfessorID}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }
        //Save all courses in the system to COURSES.txt it will works before exit
        public static void SaveAllCourses(string fileName, UNIVERSITYSYSTEMMANAGEMENT sys)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(fileName, false);
                for (int i = 0; i < sys.CourseNum; i++)
                {
                    COURSE c = sys.Courses[i];
                    sw.WriteLine($"{c.CourseID};{c.CourseName};{c.Credits};{c.ProfessorID}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in save courses: " + ex.Message);
            }
            finally
            {
                //Close the file to ensure data is saved correctly, check if isn't null to avoid null reference errors
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }
    }
}