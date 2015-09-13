using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Game:MonoBehaviour
{
	public static Game Instance;
	public bool IsGameStarted;
	public bool AIGame;
	public Transform LauncherPoint;
	public PullSpring PullSpring;
	public Transform Table;
	public Ball BallPrefab;
	public int Lives;
	public List<int> TotalScore; // total score per all lives
	public int CacheTotalScore;
	public int CurrentScore;


	public delegate void ObstacleHandle(IObstacle obstacle);
	public static ObstacleHandle[] ObstacleHandler = new ObstacleHandle[(uint)ObstacleType.Count];

	private void Awake()
	{
		Settings.Create();

		Instance = this;
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
		CacheTotalScore = TotalScore.Sum();

		CreateBall(false);
	}

	public void CreateBall(bool freeBall)
	{
		if(Lives == 0) {
			Debug.Log("Game over: " + CacheTotalScore);
			return;
		}

		Ball ball = Instantiate(BallPrefab) as Ball;
		ball.transform.parent = Table;
		ball.transform.position = LauncherPoint.position;

		BallAI ballAI = ball.GetComponentInChildren<BallAI>();
		ballAI.AIEnabled = AIGame;

		if(!freeBall) {
			Lives--;
		}
	}

	private void RestartGame()
	{
		foreach(var ball in FindObjectsOfType<Ball>()) {
			Destroy(ball.gameObject);
		}

		TotalScore = new List<int>();
		CacheTotalScore = 0;
		CurrentScore = 0;

		IsGameStarted = true;
		GameResume();

		Lives = Settings.Instance.LivesCount;
		CreateBall(false);
	}

	public void StartGame()
	{
		AIGame = false;
		RestartGame();
	}

	public void StartGameAI()
	{
		AIGame = true;
		RestartGame();
	}

	public void GameResume()
	{
		UIWindowManager.Instance.GetWindow(UIWindowTypes.Menu).Hide();
		UIWindowManager.WindowHUD.Show();
		Time.timeScale = 1;
		(UIWindowManager.Instance.GetWindow(UIWindowTypes.Menu) as UIWindowMenu).ButtonResume.gameObject.SetActive(true);
	}

	private void OnDestroy()
	{
		Settings.Instance.Save();
	}
}