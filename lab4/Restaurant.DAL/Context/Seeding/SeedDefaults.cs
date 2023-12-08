using Restaurant.DAL.Entities;

namespace Restaurant.DAL.Context.Seeding
{
    public static class SeedDefaults
    {
        private static readonly IEnumerable<Unit> units = new List<Unit>(new Unit[]
        {
            new("g"){ Id = 1 },
            new("ml"){ Id = 2 },
            new("pcs"){ Id = 3 },
        });

        public static IEnumerable<Unit> Units { get => units; }

        public static readonly int Seed = 1234;

        public static readonly string[] IngredientsNames = {
            "Tomato Sauce",
            "Mozzarella Cheese",
            "Pepperoni",
            "Mushrooms",
            "Green Peppers",
            "Onions",
            "Black Olives",
            "Italian Sausage",
            "Basil",
            "Garlic",
            "Pineapple",
            "Cherry Tomatoes",
            "Feta Cheese",
            "Parmesan Cheese",
            "Spinach",
            "Artichoke Hearts",
            "Provolone Cheese",
            "Sun-Dried Tomatoes",
            "Red Pepper Flakes"
        };

        public static readonly string[] DishesNames = {
            "Margherita",
            "Pepperoni",
            "Vegetarian Pizza",
            "Hawaiian Roll",
            "BBQ Chicken"
        };
    }
}
