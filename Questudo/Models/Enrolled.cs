using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Questudo.Models
{
    public class Enrolled
    {
        public int EnrolledID { get; set; }
        public int StudentID { get; set; }
        public int ClassroomID { get; set; }
        
        public Classroom Classroom { get; set; }
        public Student Student { get; set; }
    }
}
