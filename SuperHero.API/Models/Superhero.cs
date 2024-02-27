namespace SuperHero.API.Models
{

    public class Superhero
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public Powerstats Powerstats { get; set; } = new();
        public Biography Biography { get; set; } = new();
        public Appearance Appearance { get; set; } = new();
        public Uri? Image { get; set; }
    }

    public class Powerstats
    {
        public int Intelligence { get; set; }
        public int Strength { get; set; }
        public int Speed { get; set; }
        public int Durability { get; set; }
        public int Power { get; set; }
        public int Combat { get; set; }
    }

    public class Biography
    {
        public string? FullName{ get; set; }
        public List<string> AlterEgos { get; set; } = [];
        public List<string> Aliases { get; set; } = [];
        public string? Alignment { get; set; }
    }

    public class Appearance
    {
        public string? Gender { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string? EyeColor { get; set; }
        public string? HairColo { get; set; }
    }


}
