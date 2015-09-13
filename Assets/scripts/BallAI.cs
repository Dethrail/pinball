using System.Runtime.Serialization.Formatters;
using UnityEngine;
using System.Collections;

public class BallAI:MonoBehaviour
{
	public Rigidbody BallRigidbody;
	public bool AIEnabled = false;

	private void Awake()
	{
		BallRigidbody = transform.GetComponentInParent<Rigidbody>();
	}

	public void OnTriggerEnter(Collider collider)
	{
		if(!AIEnabled) {
			return;
		}
		Flipper flipper = collider.gameObject.GetComponent<Flipper>();
		if(flipper != null) {
			if((collider.transform.position - transform.position).sqrMagnitude > 1.5f) {
				if(flipper.IsLeftFlipper && (collider.transform.position - transform.position).x > 0) {
					StartCoroutine(Delay(flipper, 0.2f));
					return;
				}
				if(!flipper.IsLeftFlipper && (collider.transform.position - transform.position).x < 0) {
					StartCoroutine(Delay(flipper, 0.2f));
					return;
				}
			}

			flipper.TriggerAI = true;
		}
	}

	public IEnumerator Launch(float time) // launch ball
	{
		while(true) {
			yield return new WaitForSeconds(time);
			if(BallRigidbody.velocity == Vector3.zero) {
				Game.Instance.PullSpring.Fire = true;
				break;
			}

			if((BallRigidbody.transform.position - Game.Instance.LauncherPoint.position).sqrMagnitude > 2) { // break infinity while
				break;
			}
		}
	}

	public void OnTriggerExit(Collider collider)
	{
		if(!AIEnabled) {
			return;
		}
		Flipper flipper = collider.gameObject.GetComponent<Flipper>();
		if(flipper != null) {
			flipper.TriggerAI = false;
		}
	}

	private IEnumerator Delay(Flipper target, float time)
	{
		yield return new WaitForSeconds(time);
		target.TriggerAI = true;
	}
}