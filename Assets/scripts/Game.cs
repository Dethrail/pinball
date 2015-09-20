using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Game director, knows about everything and controls the game
/// </summary>
public class Game:MonoBehaviour
{
	private static Game _instance = null;
	public static Game Instance
	{
		get { return _instance; }
		private set
		{
			if(_instance == null) {
				_instance = value;
			} else {
				Debug.LogError("Can't set Game Instance");
			}
		}
	}

	[HideInInspector]
	public bool IsGameStarted;
	[HideInInspector]
	public bool AIGame;
	public Transform LauncherPoint;
	public PullSpring PullSpring;
	public Transform Table;
	[HideInInspector]
	public Ball BallPrefab;
	[HideInInspector]
	public List<Ball> Prefabs;
	[HideInInspector]
	public int Lives;
	public int DefaultLivesCount = 3;
	[HideInInspector]
	public List<int> TotalScore; // total score per all lives
	[HideInInspector]
	public int CacheTotalScore;
	[HideInInspector]
	public int CurrentScore;
	[HideInInspector]
	public List<Flipper> LeftFlippers = new List<Flipper>();
	[HideInInspector]
	public List<Flipper> RightFlippers = new List<Flipper>();

	public delegate void ObstacleHandle(IObstacle obstacle);
	public static ObstacleHandle[] ObstacleHandler = new ObstacleHandle[(uint)ObstacleType.Count];

	private void Awake()
	{
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
			UIWindowManager.WindowGameOver.Show();
			return;
		}

		Ball ball = Instantiate(BallPrefab);
		ball.transform.parent = Table;
		ball.transform.position = LauncherPoint.position;

		BallAI ballAI = ball.GetComponentInChildren<BallAI>();
		ballAI.AIEnabled = AIGame;

		if(!freeBall) {
			Lives--;
		}
	}

	public void RestartGame()
	{
		foreach(var ball in FindObjectsOfType<Ball>()) {
			Destroy(ball.gameObject);
		}

		TotalScore = new List<int>();
		CacheTotalScore = 0;
		CurrentScore = 0;

		IsGameStarted = true;
		GameResume();

		Lives = DefaultLivesCount;
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
		UIWindowManager.WindowMenu.Hide();
		UIWindowManager.WindowHUD.Show();
		Time.timeScale = 1;
		UIWindowManager.WindowMenu.ButtonResume.gameObject.SetActive(true);
	}

	public void SwitchBalls(int targetIndex)
	{
		BallPrefab = Prefabs[targetIndex];
		foreach(var ball in FindObjectsOfType<Ball>()) {
			ball.GetComponent<MeshRenderer>().material = BallPrefab.GetComponent<Renderer>().sharedMaterial;
		}
	}
}