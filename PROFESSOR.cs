using System;

namespace BPG402
{
    //The Professor class inherits from the Person class and represents a professor in the university system
    public class PROFESSOR : PERSON
    {
        public string Department { get; set; }
        public double Salary { get; set; }

        public COURSE[] CoursesTeaching = new COURSE[UNIVERSITYSYSTEMMANAGEMENT.MAX_CAPACITY];
        public int CourseTeachingCount = 0;

        public PROFESSOR(int id, string name, string email, string dept, double salary)
            : base(id, name, email)
        {
            Department = dept;
            Salary = salary;
        }

        public void AssignCourse(COURSE c)
        {
            if (CourseTeachingCount < UNIVERSITYSYSTEMMANAGEMENT.MAX_CAPACITY)
            {
                CoursesTeaching[CourseTeachingCount] = c;
                CourseTeachingCount++;
            }
            else
            {
                Console.WriteLine($"Can't assign more courses to professor {Name}. The maximum capacity has reached.");
            }
        }

        //It print the professor information
        public override void PRINTINFO()
        {
            base.PRINTINFO(); //print (ID, Name, Email)
            Console.WriteLine($"Department: {Department} | Salary: {Salary}");
        }
    }
}
