using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Questudo.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
  

        public ICollection<Classroom> Classrooms { get; set; }

    }
}
