using System;

namespace BPG402
{
    public class COURSE //Course class represents a course in the university system 
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public int Credits { get; set; }
        public int ProfessorID { get; set; }

        public COURSE(int id, string name, int credits, int profid) //Constructor to initialize COURSE class properties
        {
            CourseID = id;
            CourseName = name;
            Credits = credits;
            ProfessorID = profid;
        }

        public void PRINTINFO()
        {
            //Print courses data in ([Course ID] Course name, Credits, Professor ID)
            Console.WriteLine($"ID: [{CourseID}] Course: {CourseName}, Credits: {Credits}, ProfID: {ProfessorID}");
        }
    }
}