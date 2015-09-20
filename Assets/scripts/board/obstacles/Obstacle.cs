using System;
using System.Linq;
using UnityEngine;

public class Obstacle:MonoBehaviour, IObstacle
{
	public virtual ObstacleType Type
	{
		get { return ObstacleType.None; }
	}

	public uint UintType
	{
		get { return (uint)Type; }
	}
	public int Score;
	public int GetScore()
	{
		return Score;
	}

	[HideInInspector]
	public Ball Trigger;
	public Ball GetTrigger()
	{
		return Trigger;
	}

	protected virtual void OnCollisionEnter(Collision collision)
	{
		var ball = collision.transform.GetComponent<Ball>();
		Trigger = ball;
	}
}

public interface IObstacle
{
	int GetScore();
	Ball GetTrigger();

}

public enum ObstacleType:uint
{
	None,
	Border,
	Bumper,
	Triangle,
	DropZone,
	Count,
}
