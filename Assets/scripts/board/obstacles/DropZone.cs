﻿using UnityEngine;
using System.Collections;

public class DropZone:Obstacle
{
	public override ObstacleType Type
	{
		get { return ObstacleType.DropZone; }
	}

	private void Awake()
	{
	}

	protected override void OnCollisionEnter(Collision collision)
	{
		base.OnCollisionEnter(collision);
		Game.ObstacleHandler[UintType](this);
	}
}