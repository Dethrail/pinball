using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Main menu
/// </summary>
public class UIWindowMenu:UIWindow
{
	public Text PlayRestartText;
	public Text PlayRestartAIText;
	public Button ButtonResume;
	public Button ButtonPlayRestart;
	public Button ButtonPlayRestartAI;
	public Text DonwloadNotice;

	public Button[] BallButtons;

	protected override void OnInitialize()
	{
		base.OnInitialize();
		ButtonResume.gameObject.SetActive(false);
		DisableButtons();
	}

	public void OnPlayRestartClick()
	{
		PlayRestartText.text = "Restart";
		PlayRestartAIText.text = "Play AI";

		Game.Instance.StartGame();
	}

	public void OnPlayRestartAIClick()
	{
		PlayRestartText.text = "Play";
		PlayRestartAIText.text = "Restart AI";

		Game.Instance.StartGameAI();
	}

	public void OnBallSelect(int index)
	{
		BallButtons[index].interactable = false;
		BallButtons[index ^ 1].interactable = true;

		Game.Instance.SwitchBalls(index);
	}

	public void OnResumeClick()
	{
		Game.Instance.GameResume();
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

	public void EnableButtons()
	{
		ButtonPlayRestart.interactable = true;
		ButtonPlayRestartAI.interactable = true;
		DonwloadNotice.gameObject.SetActive(false);
		foreach(Button button in BallButtons) {
			button.gameObject.SetActive(true);
		}
	}

	public void DisableButtons()
	{
		ButtonPlayRestart.interactable = false;
		ButtonPlayRestartAI.interactable = false;
		foreach(Button button in BallButtons) {
			button.gameObject.SetActive(false);
		}
	}
}