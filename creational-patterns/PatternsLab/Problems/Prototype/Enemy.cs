namespace PatternsLab.Problems.Prototype;

public class Weapon
{
    public string Name { get; set; } = string.Empty;
    public int Damage { get; set; }

    public Weapon Clone() => new() { Name = Name, Damage = Damage };
}

public abstract class Enemy
{
    private string _modelData;

    public string Name { get; set; }
    public int Health { get; set; }
    public Weapon Weapon { get; set; }
    public List<string> Abilities { get; set; } = new();
    public string ModelId => _modelData;

    protected Enemy()
    {
        Console.WriteLine("   ...loading 3D model (slow)...");
        Thread.Sleep(500);
        _modelData = "MODEL_" + Guid.NewGuid().ToString("N")[..6];
    }

    protected Enemy(Enemy prototype)
    {
        _modelData = prototype._modelData;
        Name = prototype.Name;
        Health = prototype.Health;
        Weapon = prototype.Weapon.Clone();
        Abilities = new List<string>(prototype.Abilities);
    }

    public abstract Enemy Clone();
}

public class Orc : Enemy
{
    public Orc()
    {
        Name = "Orc";
        Health = 100;
        Weapon = new Weapon { Name = "Axe", Damage = 25 };
        Abilities.Add("Rage");
    }

    private Orc(Orc prototype) : base(prototype) { }

    public override Enemy Clone() => new Orc(this);
}

public class Elf : Enemy
{
    public Elf()
    {
        Name = "Elf";
        Health = 70;
        Weapon = new Weapon { Name = "Bow", Damage = 18 };
        Abilities.Add("Stealth");
    }

    private Elf(Elf prototype) : base(prototype) { }

    public override Enemy Clone() => new Elf(this);
}

public static class EnemyCopyHelper
{
    public static Enemy CopyEnemy(Enemy e) => e.Clone();
}

public sealed class EnemyPrototypeRegistry
{
    private readonly Dictionary<string, Enemy> _prototypes = new(StringComparer.OrdinalIgnoreCase);

    public void Register(string name, Enemy prototype)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Prototype name is required.", nameof(name));

        ArgumentNullException.ThrowIfNull(prototype);
        _prototypes[name] = prototype;
    }

    public Enemy Create(string name)
    {
        if (!_prototypes.TryGetValue(name, out var prototype))
            throw new KeyNotFoundException($"No enemy prototype registered as '{name}'.");

        return prototype.Clone();
    }
}
