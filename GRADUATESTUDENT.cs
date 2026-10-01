using System;

namespace BPG402
{
    //GRADESTUDENT class inherits from Student class and represents a graduate student in the university system
    public class GRADUATESTUDENT : STUDENT
    {
        public string ResearchTopic { get; set; } // Research topic for graduate students
        public GRADUATESTUDENT(int id, string name, string email, string major, double gpa, string research)
            : base(id, name, email, major, gpa)
        {
            ResearchTopic = research;
        }

        // It print the graduate student information
        public override void PRINTINFO()
        {
            base.PRINTINFO(); // Call it to print (ID, Name, Email, Major, GPA)
            Console.Write($" | Research: {ResearchTopic}");
        }
        public override string Form()
        {
            //Call it to get base form then add research topic
            return base.Form() + $";{ResearchTopic}";
        }
    }
}