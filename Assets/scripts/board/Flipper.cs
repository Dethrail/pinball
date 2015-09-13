using UnityEngine;
using System.Collections;

public class Flipper:MonoBehaviour
{
	public bool IsLeftFlipper;
	public int TargetRotation = 60;
	private HingeJoint _joint;

	private void Start()
	{
		_joint = GetComponent<HingeJoint>();
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
			if(Input.GetKey(KeyCode.LeftArrow) || TriggerAI) {
				sj.targetPosition = TargetRotation;
			}
			if(Input.GetKeyUp(KeyCode.LeftArrow) || (Game.Instance.AIGame && !TriggerAI)) {
				sj.targetPosition = 0;
			}
		} else {
			if(Input.GetKey(KeyCode.RightArrow) || TriggerAI) {
				sj.targetPosition = -TargetRotation;
			}
			if(Input.GetKeyUp(KeyCode.RightArrow) || (Game.Instance.AIGame && !TriggerAI)) {
				sj.targetPosition = 0;
			}
		}

		_joint.spring = sj;
	}
}