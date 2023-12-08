using Restaurant.DAL.Entities.Abstract;

namespace Restaurant.DAL.Entities
{
    public class Unit : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Portion> Portions { get; } = new List<Portion>();

        public Unit(string name)
        {
            Name = name;
        }
    }
}
