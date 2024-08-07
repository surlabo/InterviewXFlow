// This way we avoid nested if-else, keep modularity of TryBuildPathToCoord, keep Update method clean.

public class Player
{
    private List<Vector2> activeWalkPath = null;
    private bool isMoving;
    private Vector2 currentPosition;
    private Player currentEnemy;

    public void Update()
    {
        if (currentEnemy != null)
        {
            UpdatePathToEnemy(currentEnemy.currentPosition);
        }
    }

    private void UpdatePathToEnemy(Vector2 targetPosition)
    {
        activeWalkPath = TryBuildPathToCoord(targetPosition);
        if (activeWalkPath == null)
        {
            isMoving = false;
            currentEnemy = null;
        }
        else
        {
            isMoving = true;
        }
    }

    private List<Vector2> TryBuildPathToCoord(Vector2 target)
    {
        return < много строк кода по созданию пути >;
    }
}