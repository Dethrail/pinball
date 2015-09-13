using UnityEngine;

public class UIWindow:MonoBehaviour
{
	public UIWindowTypes WindowType;
	[HideInInspector]
	public UIWindow CallerWindow = null;

	public bool Visible { get; private set; }

	public void Initialize()
	{
		OnInitialize();

		gameObject.SetActive(false);
		Visible = false;
	}

	public void Show(UIWindow callerWindow)
	{
		CallerWindow = callerWindow;
		Show();
	}

	public void Show()
	{
		if(!Visible) {
			gameObject.SetActive(true);

			Visible = true;
			OnShown();
		}
	}

	public void Hide()
	{
		Hide(null);
	}


	protected void Hide(object result)
	{
		if(Visible) {
			OnHid();
			gameObject.SetActive(false);
			Visible = false;
		}
	}

	protected virtual void OnInitialize() { }

	protected virtual void OnShown()
	{
		//if(this != UIWindowManager.Instance.GetWindow(UIWindowTypes.ObjectMenu) && this != UIWindowManager.Instance.GetWindow(UIWindowTypes.MainTown)) {
		//	var objectMenu = UIWindowManager.Instance.GetWindow(UIWindowTypes.ObjectMenu);
		//	if(objectMenu != null) {
		//		objectMenu.Hide();
		//	}
		//}
	}

	protected virtual void OnHid()
	{
		//if(UIWindowManager.Instance.VisibleWindowCount == 1) {
		//	var objectMenu = UIWindowManager.Instance.GetWindow(UIWindowTypes.ObjectMenu);
		//	if(Field.Instance.SelectedObject != null && objectMenu != null) {
		//		objectMenu.Show();
		//	}
		//}
	}

	private void OnDestroy()
	{
	}

	//public void RefreshNGUI()
	//{
	//	OnRefreshNGUI();
	//}

	//protected virtual void OnRefreshNGUI() { }

	public virtual void Update() { }
}
