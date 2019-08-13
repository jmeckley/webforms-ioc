using System;

namespace WebApi.Controllers
{
    public class Entity
        : IEquatable<Entity>
    {
        public string Value { get; set; }
        public int Id { get; set; }

        public bool Equals(Entity other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Entity) obj);
        }

        public override int GetHashCode() => Id;

        public static bool operator ==(Entity left, Entity right) => Equals(left, right);

        public static bool operator !=(Entity left, Entity right) => Equals(left, right) == false;
    }
}