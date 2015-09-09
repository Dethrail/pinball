﻿using JetBrains.Annotations;
using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;

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

	//public float Force;
	//public float ForceRadius;
	//public AnimationCurve HighlightCurve;
	//public AnimationCurve SizeCurve;
	//public Color HighlightColor;

	////private bool _isActive;
	//private float _time = 1; // disable first play
	//private Material _mat;
	//private Vector3 _startSize;

	//private void Awake()
	//{
	//	_mat = GetComponent<MeshRenderer>().material;
	//	_startSize = transform.localScale;
	//}

	//private void OnCollisionEnter()
	//{
	//	foreach(Collider col in Physics.OverlapSphere(transform.position, ForceRadius)) {
	//		if(col.GetComponent<Rigidbody>()) {
	//			col.GetComponent<Rigidbody>().AddExplosionForce(Force, transform.position, ForceRadius);
	//			_time = 0;

	//			//_isActive = true;
	//		}
	//	}
	//}


	//public Color DebugColor;
	//private void Update()
	//{
	//	if (_time > 1) {
	//		return;
	//	}
	//	_time += Time.deltaTime;
	//	transform.localScale = _startSize * SizeCurve.Evaluate(_time);
	//	//var col = 
	//	DebugColor = HighlightColor * HighlightCurve.Evaluate(_time);
	//	//_mat.SetColor("_SpecColor", col);
	//	_mat.SetColor("_SpecColor", DebugColor);

	//}
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
