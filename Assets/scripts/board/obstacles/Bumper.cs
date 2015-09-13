using UnityEngine;
using System.Collections;

public class Bumper:Obstacle
{
	public override ObstacleType Type
	{
		get { return ObstacleType.Bumper; }
	}

	public float Force;
	//public float ForceRadius;
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

		foreach(Collider col in Physics.OverlapSphere(collision.contacts[0].point, 1)) { // ForceRadius
			if(col.GetComponent<Rigidbody>()) {
				col.GetComponent<Rigidbody>().AddExplosionForce(Force, collision.contacts[0].point, 1); // ForceRadius
				_time = 0;
				Game.ObstacleHandler[UintType](this);
				//_isActive = true;
			}
		}
	}

	private void Update()
	{
		if(_time > 1) {
			return;
		}
		_time += Time.deltaTime;
		transform.localScale = _startSize * SizeCurve.Evaluate(_time);
		_mat.SetColor("_SpecColor", HighlightColor * HighlightCurve.Evaluate(_time));

	}
}
