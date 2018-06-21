using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Questudo.Models
{
    public class Instructor
    {
        public int InstructorID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ICollection<Classroom> Classrooms { get; set; }
    }
}
