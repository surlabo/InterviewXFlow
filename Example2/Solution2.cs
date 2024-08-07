// First example has custom delegate with parameters, while second example uses built-in delegate System.Action
// While custom delegate is more flexible. System.Action is more concise and readable.

// Second example invokes event handler immediatly after subscription, which can lead to misbehavior and i strongly discourage this practice.

// While both of these examples change color of GUI element 2nd example also saves state of previous health.

// I would combine best of these 2.

public class ExtPlayer : Player
{
    public delegate void HealthChanged(int oldHealth, int newHealth);
    public event HealthChanged OnHealthChanged;

}

class ExtProgram : Program
{
    private static TextView healthView = new TextView();

    public static void ExtMain(string[] args)
    {
        Main(args);

        healthView.Text = player.Health.ToString();
        player.HealthChanged += OnPlayerHealthChanged;

        HitPlayer();
    }

    private static void OnPlayerHealthChanged(int oldHealth, int newHealth)
    {
        healthView.Text = newHealth.ToString();
        // use ternary conditional operator instead of if-else for easier readability
        healthView.Color = newHealth - oldHealth < -10 ? Color.Red : Color.White;
    }
}

/* one thing missing from my solution is old health value, but it should be calculate during HitPlayer method invocation, 
by saving current value in temp variable, something like this:
    int oldHealth = Health;
    Health = value;
    OnHealthChanged(oldHealth, Health); */
