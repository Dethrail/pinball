using UnityEngine;
using System.Collections;

/// <summary>
/// Flipper, fixed by hingejoint 
/// Allows player control the ball
/// </summary>
public class Flipper:MonoBehaviour
{
	public bool IsLeftFlipper;
	public int TargetRotation = 60;
	private HingeJoint _joint;

	private void Start()
	{
		_joint = GetComponent<HingeJoint>();

		if(IsLeftFlipper) {
			Game.Instance.LeftFlippers.Add(this);
		} else {
			Game.Instance.RightFlippers.Add(this);
		}
	}

	private bool _triggerAI;
	public bool TriggerAI
	{
		get { return _triggerAI; }
		set
		{
			_triggerAI = value;
			_timerAI = 0;
		}
	}

	public bool TriggerUI;


	private float _timerAI;
	public float LockAITimer = 0.5f;

	private void Update()
	{
		_timerAI += Time.deltaTime;
		if(_timerAI > LockAITimer) {
			TriggerAI = false;
		}

		JointSpring sj = new JointSpring();
		sj.spring = 10000;
		sj.damper = 1;

		if(IsLeftFlipper) {
			if(Input.GetKeyUp(KeyCode.LeftArrow) || !TriggerUI || (Game.Instance.AIGame && !TriggerAI)) {
				sj.targetPosition = 0;
			}

			if(Input.GetKey(KeyCode.LeftArrow) || TriggerUI || TriggerAI) {
				sj.targetPosition = TargetRotation;
			}

		} else {
			if(Input.GetKeyUp(KeyCode.RightArrow) || !TriggerUI || (Game.Instance.AIGame && !TriggerAI)) {
				sj.targetPosition = 0;
			}

			if(Input.GetKey(KeyCode.RightArrow) || TriggerUI || TriggerAI) {
				sj.targetPosition = -TargetRotation;
			}

		}

		_joint.spring = sj;
	}
}