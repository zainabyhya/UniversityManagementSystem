using System;

namespace BPG402
{
    //The Person class is abstract class, represents a person in the university system
    public abstract class PERSON
    {
        public int ID { get; set; }   //ID for person
        public string Name { get; set; } //Name for person
        public string Email { get; set; } //Email for person

        //Constructor to initialize the properties of the Person class
        public PERSON(int id, string name, string email)
        {
            ID = id;
            Name = name;
            Email = email;
        }
        //Virtual method to print the basic information of a person (ID, Name, Email)
        public virtual void PRINTINFO()
        {
            Console.Write($"ID: {ID} | Name: {Name} | Email: {Email} | ");
        }
    }
}