using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Game:MonoBehaviour
{
	public static Game Instance;

	public Transform LauncherPoint;
	public Transform Table;
	public Ball BallPrefab;

	public int Lives;

	public List<int> TotalScore; // total score per all lives

	private int _cacheTotalScore;
	public int CurrentScore;

	public delegate void ObstacleHandle(IObstacle obstacle);
	public static ObstacleHandle[] ObstacleHandler = new ObstacleHandle[(uint)ObstacleType.Count];

	private void Awake()
	{
		TotalScore = new List<int>();
		ObstacleHandler[(uint)ObstacleType.None] = OnNone;
		ObstacleHandler[(uint)ObstacleType.Border] = OnBorder;
		ObstacleHandler[(uint)ObstacleType.Bumper] = OnBumper;
		ObstacleHandler[(uint)ObstacleType.Triangle] = OnTriangle;
		ObstacleHandler[(uint)ObstacleType.DropZone] = OnBallDrop;

	}

	public void OnNone(IObstacle obstacle)
	{
	}

	public void OnBorder(IObstacle obstacle)
	{
	}

	public void OnBumper(IObstacle obstacle)
	{
		//Debug.Log("Bumper = " + obstacle.GetScore() + obstacle.GetTrigger().name);
		CurrentScore += obstacle.GetScore();
	}

	public void OnTriangle(IObstacle obstacle)
	{
		CurrentScore += obstacle.GetScore();
	}

	public void OnBallDrop(IObstacle obstacle)
	{
		//Debug.Log("Bumper = " + obstacle.GetScore() + " " + obstacle.GetTrigger().name);
		Destroy(obstacle.GetTrigger().gameObject);

		TotalScore.Add(CurrentScore);
		CurrentScore = 0;
		_cacheTotalScore = TotalScore.Sum();
		//Debug.Log(_cacheTotalScore);
		if(Lives == 0) {
			return;
		}
	
		Lives--;
		Ball ball = Instantiate(BallPrefab) as Ball;
		ball.transform.parent = Table;
		ball.transform.position = LauncherPoint.position;
	}
}