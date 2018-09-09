using MyProject.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Domain
{
    public class Student : Entity<int>
    {
        public string Name { get; set; }
        public int Marks { get; set; }
        public string Email { get; set; }

    }
}
