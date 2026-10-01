# University Management System (C# & OOP)

A robust console-based application built in **C#** and **.NET Framework**, demonstrating Object-Oriented Programming (OOP) principles, structured inheritance hierarchies, and persistent file-based data storage (File I/O).

---

## 📌 Project Overview

The **University Management System** manages university entities—including students, graduate researchers, professors, and administrative staff—along with academic courses. The system provides a centralized console interface for tracking academic records, course enrollments, and staff assignments, ensuring data persistence across runs via external text files.

---

## ✨ Key Features & OOP Concepts

* **Inheritance & Polymorphism:** Base class `PERSON.cs` extended by specialized roles (`STUDENT`, `PROFESSOR`, `GRADUATESTUDENT`, `ADMINISTRATIVESTAFF`).
* **Encapsulation:** Properties and methods defined with appropriate access modifiers to maintain entity integrity.
* **Persistent Storage (File I/O):** Dedicated `FILEMANAGER.cs` handles reading from and writing to external data files (`STUDENTS.txt`, `PROFESSORS.txt`, `COURSES.txt`).
* **Modular Management:** Core operations coordinated through `UNIVERSITYSYSTEMMANAGEMENT.cs` and orchestrated in `Program.cs`.

---

## 📁 Repository Structure

```text
UniversityManagementSystem/
├── BPG402.sln                  # Visual Studio Solution File
├── BPG402.csproj               # C# Project File
├── App.config                  # Application Configuration
├── Properties/
│   └── AssemblyInfo.cs         # Assembly Metadata
├── Program.cs                  # Entry Point
├── PERSON.cs                   # Base Person Model
├── STUDENT.cs                  # Student Entity
├── PROFESSOR.cs                # Professor Entity
├── GRADUATESTUDENT.cs          # Graduate Student Model
├── ADMINISTRATIVESTAFF.cs      # Staff Entity
├── COURSE.cs                   # Course Entity
├── FILEMANAGER.cs              # File Reading/Writing Logic
├── UNIVERSITYSYSTEMMANAGEMENT.cs # Core Business Logic
├── STUDENTS.txt                # Sample Student Data
├── PROFESSORS.txt              # Sample Professor Data
└── COURSES.txt                 # Sample Course Data
