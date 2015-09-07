using UnityEngine;
using System.Collections;

public class Flipper:MonoBehaviour
{
	public bool IsLeftFlipper;
	public int TargetRotation = 60;
	public AudioClip sfx;
	public HingeJoint _joint;

	private void Start()
	{
		_joint = GetComponent<HingeJoint>();
	}

	private void Update()
	{
		JointSpring sj = new JointSpring();
		sj.spring = 10000;
		sj.damper = 1;
		//bool keyDown = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);

		//sj.targetPosition = (keyDown) ? TargetRotation : 0;
		//sj.targetPosition *= (IsLeftFlipper) ? 1 : -1;

		if(IsLeftFlipper) {
			if(Input.GetKey(KeyCode.LeftArrow)) {
				sj.targetPosition = TargetRotation;
			}
			if(Input.GetKeyUp(KeyCode.LeftArrow)) {
				sj.targetPosition = 0;
			}
		} else {
			if(Input.GetKey(KeyCode.RightArrow)) {
				sj.targetPosition = -TargetRotation;
			}
			if(Input.GetKeyUp(KeyCode.RightArrow)) {
				sj.targetPosition = 0;
			}
		}


		_joint.spring = sj;






	}
}