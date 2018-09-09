using MyProject.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.DataAccessLayer.EF
{
  public class StudentConfiguration : EntityTypeConfiguration<Student>
    {

        public StudentConfiguration()
        {
            ToTable("Student", "dbo");
            //Property(x => x.Id).HasColumnName("Id");
        }
    }
}
