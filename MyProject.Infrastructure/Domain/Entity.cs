using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Infrastructure.Domain
{
    public abstract class Entity<T> : IEquatable<Entity<T>>, IEntity<T>
    {
        protected Entity(T id)
        {
            Id = id;
        }

        protected Entity()
        {
        }

        public virtual T Id { get; set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        bool IEquatable<Entity<T>>.Equals(Entity<T> other)
        {
            return Id.Equals(other.Id);
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
