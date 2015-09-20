using UnityEngine;
using System.Collections;

public class LoadAssets:BaseLoader
{
	public string assetBundleName = "balls.unity3d";
	public string assetName1 = "ball1";
	public string assetName2 = "ball2";

	private IEnumerator Start()
	{
		yield return StartCoroutine(Initialize()); // initialize manager


		yield return StartCoroutine(LoadPrefab2GameDirector(assetBundleName, assetName1));
		yield return StartCoroutine(LoadPrefab2GameDirector(assetBundleName, assetName2));

		// Unload assetBundles.
		UIWindowManager.WindowMenu.EnableButtons();
		AssetBundleManager.UnloadAssetBundle(assetBundleName);
	}

	protected IEnumerator LoadPrefab2GameDirector(string assetBundleName, string assetName)
	{
		AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(assetBundleName, assetName, typeof(GameObject));
		if(request == null)
			yield break;
		yield return StartCoroutine(request);

		GameObject prefab = request.GetAsset<GameObject>();
		if(Game.Instance.BallPrefab == null) {
			Game.Instance.BallPrefab = prefab.GetComponent<Ball>();
		}
		if(prefab != null) {
			Game.Instance.Prefabs.Add(prefab.GetComponent<Ball>());
		}
	}
}
