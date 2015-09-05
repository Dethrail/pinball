using UnityEngine;
using System.Collections;

public class FlipperCapsule:MonoBehaviour
{
	public bool IsLeftFlipper;
	public int MotorSpeed = 500;
	public AudioClip sfx;

	private Quaternion _sourceRotation;
	public Quaternion TargetRotation;
	//r z -0.3826835
	//r w 0.9238795
	//0.9238794
	//-0.3826836

	private void Start()
	{
		_sourceRotation = transform.rotation;

	}

	private void Update()
	{
		//TargetRotation = transform.rotation;
		if(IsLeftFlipper) {
			if(Input.GetKeyDown(KeyCode.LeftArrow)) {
				//transform.rotation = TargetRotation;
				GetComponent<Rigidbody2D>().AddTorque(MotorSpeed);
				//transform.Rotate(Vector3.forward, 90f); // = _sourceRotation;
			}
			if(Input.GetKeyUp(KeyCode.LeftArrow)) {
				GetComponent<Rigidbody2D>().angularVelocity = 0;
				transform.rotation = _sourceRotation;
			}
		} else {
			if(Input.GetKeyDown(KeyCode.RightArrow)) {
				//transform.rotation = TargetRotation; 
				GetComponent<Rigidbody2D>().AddTorque(-MotorSpeed);
				//transform.Rotate(Vector3.forward, -90f);
			}
			if(Input.GetKeyUp(KeyCode.RightArrow)) {
				GetComponent<Rigidbody2D>().angularVelocity = 0;
				transform.rotation = _sourceRotation;
			}
		}
	}





	public void Animate(bool makeActive)
	{
	}
}

