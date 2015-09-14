using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// HUD buttons, allow control flippers
/// </summary>
public class FlipperButton:MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	public bool IsLeftFlipper;

	public void OnPointerUp(PointerEventData eventData)
	{
		if(IsLeftFlipper) {
			foreach(Flipper flipper in Game.Instance.LeftFlippers) {
				flipper.TriggerUI = false;
			}
		} else {
			foreach(Flipper flipper in Game.Instance.RightFlippers) {
				flipper.TriggerUI = false;
			}
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if(IsLeftFlipper) {
			foreach(Flipper flipper in Game.Instance.LeftFlippers) {
				flipper.TriggerUI = true;
			}
		} else {
			foreach(Flipper flipper in Game.Instance.RightFlippers) {
				flipper.TriggerUI = true;
			}
		}
	}
}
