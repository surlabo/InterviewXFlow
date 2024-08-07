// Example1_1

// Pros:
// - Simplicity: Easy to understand and straightforward.
// - No external dependencies: Does not rely on file I/O or serialization.

// Cons:
// - Not scalable: The solution is not scalable and will not work for large datasets.
// - No persistence: Data will be lost after closing the application.


// Example1_2

// Pros:
// - Flexibility: Health and damage values are loaded from external files, making it more configurable.
// - Persistence: Player state can be saved and loaded, allowing for more complex game states.

// Cons:
// - Complexity: Requires serialization and file I/O.
// - Dependency: doesn't include case for corrupted files.




// First example is to simple for real world project usage, i would go for 2nd with extension of checking if file is missing or corrupted.
// Also would remove Set health method and leave Hit method, because it is more clear what is happening with player.


[Serializable]
public class Player
{
    public int Health { get; private set; }

    public Player(int health)
    {
        Health = health;
    }

    public void Hit(int damage)
    {
        Health -= damage;
    }
}

[Serializable]
public class Settings
{
    public int Damage { get; set; }
}

class Program
{
    private const string NewPlayerPath = "NewPlayer.json";
    private const string SettingsPath = "Settings.json";
    private const int DefaultDamage = 10;
    private const int DefaultHealth = 100;

    protected static Player player;

    // I dont like idea of using large logic in Main method, so I will move it to separate methods.
    public static void Main(string[] args)
    {
        player = LoadPlayer(NewPlayerPath);
        var settings = LoadSettings(SettingsPath);
        player.Hit(settings.Damage);
    }

    private static Player LoadPlayer(string path)
    {
        try
        {
            // Try to load player from file, otherwise create a new one.
            return Serializer.LoadFromFile<Player>(path) ?? new Player(DefaultHealth);
        }
        catch (Exception)
        {
            Console.WriteLine("Failed to load player data. Creating new player."); // Debug.LogWarning() in Unity
            return new Player(DefaultHealth);
        }
    }

    private static Settings LoadSettings(string path)
    {
        try
        {
            // Load settings from file.
            return Serializer.LoadFromFile<Settings>(path);
        }
        catch (Exception)
        {
            Console.WriteLine("Failed to load settings data. Using default settings."); // Debug.LogWarning() in Unity
            return new Settings { Damage = DefaultDamage };
        }
    }
}

// With this update we can easyly implement new features, like saving player state to file, or changing settings in runtime. 
