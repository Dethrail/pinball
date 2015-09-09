﻿using UnityEngine;
using System.Collections;

public class Bumper:Obstacle
{
	public override ObstacleType Type
	{
		get { return ObstacleType.Bumper; }
	}

	public float Force;
	public float ForceRadius;
	public AnimationCurve HighlightCurve;
	public AnimationCurve SizeCurve;
	public Color HighlightColor;

	//private bool _isActive;
	private float _time = 1; // disable first play
	private Material _mat;
	private Vector3 _startSize;

	private void Awake()
	{
		_mat = GetComponent<MeshRenderer>().material;
		_startSize = transform.localScale;
	}

	protected override void OnCollisionEnter(Collision collision)
	{
		base.OnCollisionEnter(collision);

		foreach(Collider col in Physics.OverlapSphere(transform.position, ForceRadius)) {
			if(col.GetComponent<Rigidbody>()) {
				col.GetComponent<Rigidbody>().AddExplosionForce(Force, transform.position, ForceRadius);
				_time = 0;
				Game.ObstacleHandler[UintType](this);
				//_isActive = true;
			}
		}
	}


	public Color DebugColor;
	private void Update()
	{
		if(_time > 1) {
			return;
		}
		_time += Time.deltaTime;
		transform.localScale = _startSize * SizeCurve.Evaluate(_time);
		//var col = 
		DebugColor = HighlightColor * HighlightCurve.Evaluate(_time);
		//_mat.SetColor("_SpecColor", col);
		_mat.SetColor("_SpecColor", DebugColor);

	}
}