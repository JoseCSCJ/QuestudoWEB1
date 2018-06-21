using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Questudo.Models;

namespace Questudo.Data
{

    public static class DbInitializer
    {
        public static void Initialize(ClassroomContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Students.Any())
            {
                return;   // DB has been seeded
            }

            var students = new Student[]
            {
            new Student{Name="Carson",Email="carson@gmail.com"},
            new Student{Name="Meredith",Email="meredith@gmail.com"},
            new Student{Name="Arturo",Email="arturo@gmail.com"},
            new Student{Name="Gytis",Email="gytis@gmail.com"},
            new Student{Name="Yan",Email="yan@gmail.com"},
            new Student{Name="Peggy",Email="peggy@gmail.com"},
            new Student{Name="Laura",Email="laura@gmail.com"},
            new Student{Name="Nino",Email="nino@gmail.com"}
            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var instructors = new Instructor[]
            {
            new Instructor{Name="Gabriela",Email="gabriela@gmail.com"},
            new Instructor{Name="Anna",Email="anna@gmail.com"},
            new Instructor{Name="Elsa",Email="elsa@gmail.com"},
            new Instructor{Name="Pedro",Email="pedro@gmail.com"},
            new Instructor{Name="Orion",Email="orion@gmail.com"}
            };
            foreach (Instructor i in instructors)
            {
                context.Instructors.Add(i);
            }
            context.SaveChanges();

            var classrooms = new Classroom[]
            {
            new Classroom{InstructorID=1, Name="Matemática"},
            new Classroom{InstructorID=2, Name="Fisica"},
            new Classroom{InstructorID=3, Name="Geografia"},
            new Classroom{InstructorID=4, Name="Quimica"},
                };
            foreach (Classroom c in classrooms)
            {
                context.Classrooms.Add(c);
            }
            context.SaveChanges();

            var enrolleds = new Enrolled[]
               {
            new Enrolled{StudentID=1, ClassroomID=1},
            new Enrolled{StudentID=2, ClassroomID=1},
            new Enrolled{StudentID=3, ClassroomID=1},
            new Enrolled{StudentID=4, ClassroomID=1},
            new Enrolled{StudentID=2, ClassroomID=2},
            new Enrolled{StudentID=3, ClassroomID=2},
            new Enrolled{StudentID=4, ClassroomID=2},
            new Enrolled{StudentID=4, ClassroomID=3},
            new Enrolled{StudentID=2, ClassroomID=3},
            new Enrolled{StudentID=3, ClassroomID=3},
            new Enrolled{StudentID=1, ClassroomID=4},
            new Enrolled{StudentID=2, ClassroomID=4},
                   };
            foreach (Enrolled e in enrolleds)
            {
                context.Enrolleds.Add(e);
            }
            context.SaveChanges();
        }
    }
}



