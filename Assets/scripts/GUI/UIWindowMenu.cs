using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;


public class UIWindowMenu:UIWindow
{
	public Text PlayRestartText;
	public Text PlayAIRestartText;
	public Button ButtonResume;

	protected override void OnInitialize()
	{
		base.OnInitialize();
		ButtonResume.gameObject.SetActive(false);
	}
	
	public void OnPlayRestartClick()
	{
		PlayRestartText.text = "Restart";
		PlayAIRestartText.text = "Play AI";

		Game.Instance.StartGame();
	}

	public void OnPlayRestartAIClick()
	{
		PlayRestartText.text = "Play";
		PlayAIRestartText.text = "Restart AI";

		Game.Instance.StartGameAI();
	}

	public void OnResumeClick()
	{
		Game.Instance.GameResume();
	}

	public void OnGlobalScoreClick()
	{
	}

	public void OnExitClick()
	{
		Application.Quit();
	}

	private List<UIWindow> _windows;

	protected override void OnShown()
	{
		base.OnShown();
		_windows = UIWindowManager.Instance.Windows.Where(x => x.Visible && x != this).ToList();
		foreach(UIWindow window in _windows) {
			window.Hide();
		}
	}

	protected override void OnHid()
	{
		base.OnHid();
		foreach(UIWindow window in _windows) {
			window.Show();
		}
	}
}