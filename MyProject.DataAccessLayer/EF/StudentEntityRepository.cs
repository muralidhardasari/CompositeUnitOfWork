using MyProject.Domain;
using MyProject.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.DataAccessLayer.EF
{
    public class StudentEntityRepository : EntityRepository<int, Student>
    {
        public StudentEntityRepository()
        {
        }

        public StudentEntityRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
