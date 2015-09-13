using UnityEngine;
using System.Collections;
using UnityEditor;

public class Helper
{
	static public Vector3 Round(Vector3 v)
	{
		v.x = Mathf.Round(v.x);
		v.y = Mathf.Round(v.y);
		v.z = Mathf.Round(v.z);
		return v;
	}

	private const float blockScale = 3.2f;

	static public Vector3 Round10(Vector3 v)
	{
		v.x = Mathf.Round(v.x * 10) / 10;
		v.y = Mathf.Round(v.y * 10) / 10;
		v.z = Mathf.Round(v.z * 10) / 10;
		return v;
	}

	static public void MakePixelPerfect(Transform t)
	{
		t.localPosition = Round(t.localPosition);
		t.localScale = Round(t.localScale);

		for(int i = 0, imax = t.childCount; i < imax; ++i) {
			MakePixelPerfect(t.GetChild(i));
		}
	}

	static public void RoundTo10(Transform t)
	{
		t.localPosition = Round10(t.localPosition);
		t.localScale = Round10(t.localScale);

		for(int i = 0, imax = t.childCount; i < imax; ++i) {
			RoundTo10(t.GetChild(i));
		}
	}

	[MenuItem("Helper/Make Pixel Perfect %r")] // #&d
	static void PixelPerfectSelection()
	{
		if(Selection.activeTransform == null) {
			Debug.Log("You must select an object in the scene hierarchy first");
			return;
		}
		foreach(Transform t in Selection.transforms)
			Helper.MakePixelPerfect(t);
	}

	[MenuItem("Helper/Make Pixel Perfect %e")]
	static void PixelPerfectSelectionR100()
	{
		if(Selection.activeTransform == null) {
			Debug.Log("You must select an object in the scene hierarchy first");
			return;
		}
		foreach(Transform t in Selection.transforms)
			Helper.RoundTo10(t);
	}

	[MenuItem("Helper/Reset position %q")]
	static void ResetPosition()
	{
		if(Selection.activeTransform == null) {
			Debug.Log("You must select an object in the scene hierarchy first");
			return;
		}

		Selection.activeTransform.localPosition = Vector3.zero;
	}

	[MenuItem("Helper/Reset scale %w")]
	static void ResetScale()
	{
		if(Selection.activeTransform == null) {
			Debug.Log("You must select an object in the scene hierarchy first");
			return;
		}

		Selection.activeTransform.localScale = Vector3.one;
	}

	static bool HasValidTransform()
	{
		if(Selection.activeTransform == null) {
			Debug.LogWarning("You must select an object first");
			return false;
		}
		return true;
	}

	[MenuItem("GameObject/Selection/Toggle 'Active' #a")]
	static void ActivateDeactivate()
	{
		if(HasValidTransform()) {
			GameObject[] gos = Selection.gameObjects;
			bool val = !(Selection.activeGameObject && Selection.activeGameObject.activeInHierarchy);
			foreach(GameObject go in gos) {
				go.SetActive(val);
			}
		}
	}
}
