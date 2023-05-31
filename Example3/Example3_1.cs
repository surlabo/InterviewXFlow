public class Player
{
	private List<Vector2> activeWalkPath = null;
	private bool isMoving;
	private Player currentEnemy;

	public void Update()
	{
		if (currentEnemy != null)
		{
			UpdatePathToEnemy();
		}
	}

	private void UpdatePathToEnemy()
	{
		activeWalkPath = <много строк кода по созданию пути>;
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
}
