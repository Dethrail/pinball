using UnityEngine;
using System.Collections;

public class HandlerScript:MonoBehaviour
{
	/*
		Torque Force 	=	500
		Body mass 		= 	1
		Ball mass		= 	0.0001
	 */

	public float torqueForce;
	public bool isLeft;
	Rigidbody2D body;
	Vector3 pos;
	Vector3 euler;

	void Start()
	{

		body = GetComponent<Rigidbody2D>();
		pos = transform.position;
	}

	void FixedUpdate()
	{

		//Rotate flipper when key is pressed 
		if(isLeft) {
			if(Input.GetKeyDown(KeyCode.LeftArrow)) {
				body.AddTorque(torqueForce);
			}
		} else {
			if(Input.GetKeyDown(KeyCode.RightArrow)) {
				body.AddTorque(torqueForce);
			}
		}
		//keep track of the euler angles 
		euler = transform.eulerAngles;

		//if limit reached 
		if(euler.z >= 60) {

			//stop rotating the flipper
			body.angularVelocity = 0;

			//if key is still pressed then keep the flipper uo
			//if not then reset the flipper
			if(!Input.GetKey(KeyCode.LeftArrow)) {
				euler.z = 0;
				transform.eulerAngles = euler;
			}
		}




	}

	void Update()
	{
		//to freez the flipper position
		transform.position = pos;
	}

}