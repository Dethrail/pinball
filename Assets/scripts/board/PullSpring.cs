using UnityEngine;
using System.Collections;

/// <summary>
/// add acceleration to ball
/// </summary>
public class PullSpring:MonoBehaviour
{
	public GameObject _ball;
	public float Acceleration = 500;
	public float SpringPower;
	public bool Ready = false;
	public bool Fire = false;

	private void OnCollisionEnter(Collision other)
	{
		_ball = other.gameObject;
		Ready = true;
		
		if (Game.Instance.AIGame) {
			BallAI ballAI = _ball.GetComponentInChildren<BallAI>();
			ballAI.StartCoroutine(ballAI.Launch(1.3f));
		}

		UIWindowManager.WindowHUD.ShowHideLauncher(true);
	}

	private void OnCollisionexit(Collision other)
	{
		_ball = null;
		Ready = false;
	}

	private void Update()
	{
		if(Fire && Ready) {
			_ball.transform.TransformDirection(Vector3.forward * 10);
			SpringPower = Mathf.Max(0.05f, SpringPower); // min value for push the ball and allow again trigger OnCollisionEnter
			_ball.GetComponent<Rigidbody>().AddForce(Vector3.forward * Acceleration * SpringPower, ForceMode.Acceleration);
			Fire = false;
			Ready = false;
			UIWindowManager.WindowHUD.ShowHideLauncher(false);
		}
	}
}