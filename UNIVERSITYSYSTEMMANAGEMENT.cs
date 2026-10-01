using System;

namespace BPG402
{
    public class UNIVERSITYSYSTEMMANAGEMENT //Manage the university system, including students, professors, and courses
    {
        public const int MAX_CAPACITY = 50;  // Maximum the number of students, professors, and courses in the system

        public STUDENT[] Students = new STUDENT[MAX_CAPACITY]; //To store students in the system, with a maximum capacity
        public PROFESSOR[] Professors = new PROFESSOR[MAX_CAPACITY]; //To store professors in the system, with a maximum capacity
        public COURSE[] Courses = new COURSE[MAX_CAPACITY]; //To store courses in the system, with a maximum capacity
        public int StudentNum = 0, ProfessorNum = 0, CourseNum = 0;

        //Check if the count is less than the maximum capacity before adding student and if full it appears a message
        public void AddStudent(STUDENT s)
        {
            if (StudentNum < MAX_CAPACITY)
            {
                Students[StudentNum] = s;
                StudentNum++;
            }
            else
                Console.WriteLine($"Full Students number in system (Maximum capacity {MAX_CAPACITY})");
        }

        //Check if the count is less than the maximum capacity, if full it appears a message
        public void AddProfessor(PROFESSOR p)
        {
            if (ProfessorNum < MAX_CAPACITY)
            {
                Professors[ProfessorNum] = p;
                ProfessorNum++;
            }
            else
                Console.WriteLine($"Full Professors number in system (Maximum capacity {MAX_CAPACITY})");
        }

        //Check if the count is less than the maximum capacity, if full it appears a message
        public void AddCourse(COURSE c)
        {
            if (CourseNum < MAX_CAPACITY)
            {
                Courses[CourseNum] = c;
                CourseNum++;
            }
            else
                Console.WriteLine($"Full Courses number in system (Maximum capacity {MAX_CAPACITY})");
        }

        //It shows all courses data (Course ID, Course Name, Credits, Professor ID), if there is no courses it appears "No courses available"
        public void ShowCourses()
        {
            if (CourseNum == 0)
            {
                Console.WriteLine("No courses available");
                return;
            }
            for (int i = 0; i < CourseNum; i++)
            {
                if (Courses[i] != null) 
                {
                    Courses[i].PRINTINFO();
                }
            }
        }
        public void LnkCoursesToProf()
        {
            for (int i = 0; i < ProfessorNum; i++)
            {
                Professors[i].CourseTeachingCount = 0;
            }
            for (int i = 0; i < CourseNum; i++)
            {
                for (int j = 0; j < ProfessorNum; j++)
                {
                    if (Courses[i].ProfessorID == Professors[j].ID)
                    {
                        Professors[j].AssignCourse(Courses[i]);
                        break; //Stop searching for professor id if we've found it
                    }
                }
            }
        }
    }
}
