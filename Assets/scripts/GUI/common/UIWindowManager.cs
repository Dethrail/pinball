using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Window Manage know about all widnows in UI.
/// </summary>
public class UIWindowManager:MonoBehaviour
{
	public static UIWindowManager Instance;
	public UIWindow[] Windows;

	public UIWindow StartupWindow;
	private Dictionary<UIWindowTypes, UIWindow> _cachedWindows;

	public static UIWindowHUD WindowHUD
	{
		get
		{
			if(Instance._WindowHUD == null) {
				Instance._WindowHUD = (Instance.GetWindow(UIWindowTypes.HUD) as UIWindowHUD);
			}
			return Instance._WindowHUD;
		}
	}

	private UIWindowHUD _WindowHUD;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		InitializeCache();
		InitializeWindows();

		if(StartupWindow != null) {
			StartupWindow.Show();
		}
	}

	private void InitializeCache()
	{
		_cachedWindows = new Dictionary<UIWindowTypes, UIWindow>(Windows.Length);
		foreach(UIWindow w in Windows) {
			if(w != null && w.WindowType != UIWindowTypes.None && !_cachedWindows.ContainsKey(w.WindowType) && !_cachedWindows.ContainsValue(w)) {
				_cachedWindows.Add(w.WindowType, w);
			}
		}
	}

	private void InitializeWindows()
	{
		foreach(UIWindow w in _cachedWindows.Values) {
			w.gameObject.SetActive(true);
			w.Initialize();
		}
	}

	public UIWindow GetWindow(UIWindowTypes uiWindowType)
	{
		UIWindow w;
		if(!string.IsNullOrEmpty(name) && _cachedWindows.TryGetValue(uiWindowType, out w)) {
			return w;
		}
		return null;
	}
}

public enum UIWindowTypes
{
	None,
	HUD, // head-up display
	Menu,
	GameOver,
}