using System;

namespace BPG402
{
    //Student class inherits from Person class and represents a student in the university system
    public class STUDENT : PERSON
    {
        public string Major { get; set; }
        public double GPA { get; set; }

        public COURSE[] Courses = new COURSE[UNIVERSITYSYSTEMMANAGEMENT.MAX_CAPACITY];
        public int RegisteredCourseCount = 0;

        public STUDENT(int id, string name, string email, string major, double gpa)
            : base(id, name, email)
        {
            Major = major;
            GPA = gpa;
        }

        //It print the student information
        public override void PRINTINFO()
        {
            Console.Write($"ID: {ID} | Name: {Name} | Email: {Email} | Major: {Major} | GPA: {GPA}");

        }

        public virtual string Form()
        {
            return $"{ID};{Name};{Email};{Major};{GPA}";
        }
    }
}