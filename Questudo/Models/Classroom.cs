using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Questudo.Models
{
    public class Classroom
    {
             
        public int ClassroomID { get; set; }
        public int InstructorID { get; set; }
        public string Name { get; set; }

        public Instructor Instructor { get; set; }
    }
}
