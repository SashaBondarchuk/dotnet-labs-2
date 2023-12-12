using Restaurant.DAL.Entities.Abstract;

namespace Restaurant.DAL.Entities
{
    public class Unit : BaseEntity
    {
        public string Name { get; set; }

        public Unit(string name)
        {
            Name = name;
        }
    }
}
