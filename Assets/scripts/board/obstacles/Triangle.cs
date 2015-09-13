using UnityEngine;
using System.Collections;

public class Triangle:Obstacle
{
	public override ObstacleType Type
	{
		get { return ObstacleType.Triangle; }
	}

	public float Force;

	protected override void OnCollisionEnter(Collision collision)
	{
		base.OnCollisionEnter(collision);
		GetTrigger().GetComponent<Rigidbody>().AddForce(transform.forward * Force);
	}
}