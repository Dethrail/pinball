using UnityEngine;
using System.Collections;

public class PullSpring:MonoBehaviour
{
	public string inputButtonName = "Fire1";
	public float distance = 50;
	public float speed = 1;
	public GameObject ball;
	public float Acceleration = 500;

	public bool ready = false;
	public bool fire = false;
	public float followedDistance = 0;

	void OnCollisionEnter(Collision other)
	{
		//if(other.gameObject.tag == "Ball") {
		ready = true;
		//}
	}

	void OnCollisionexit(Collision other)
	{
		//if(other.gameObject.tag == "Ball") {
		ready = false;
		//}
	}

	void Update()
	{

		if(Input.GetButton(inputButtonName)) {
			//As the button is held down, slowly move the piece
			if(followedDistance < distance) {
				transform.Translate(0, 0, -speed * Time.deltaTime);
				followedDistance += speed * Time.deltaTime;
				fire = true;
			}
		} else if(followedDistance > 0) {
			//Shoot the ball
			if(fire && ready) {
				ball.transform.TransformDirection(Vector3.forward * 10);
				//ball.GetComponent.<Rigidbody>().AddForce(0, 0, moveCount * power);
				ball.GetComponent<Rigidbody>().AddForce(Vector3.forward * (Acceleration * followedDistance / distance), ForceMode.Acceleration);
				fire = false;
				ready = false;
			}
			//Once we have reached the starting position fire off!
			transform.Translate(0, 0, 20 * Time.deltaTime);
			followedDistance -= 20 * Time.deltaTime;
		}

		//Just ensure we don't go past the end
		if(followedDistance <= 0) {
			fire = false;
			followedDistance = 0;
		}

	}
}