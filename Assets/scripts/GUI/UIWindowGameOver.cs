using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// GameOver window, onShown display total score. Allow to restart and exit.
/// </summary>
public class UIWindowGameOver:UIWindow
{
	public Text TextTotalScore;

	private string _rawTotalScore;

	protected override void OnInitialize()
	{
		base.OnInitialize();
		_rawTotalScore = TextTotalScore.text;
	}

	protected override void OnShown()
	{
		base.OnShown();

		var windows = UIWindowManager.Instance.Windows.Where(x => x.Visible && x != this).ToList();
		foreach(UIWindow window in windows) {
			window.Hide();
		}
		TextTotalScore.text = _rawTotalScore + Game.Instance.TotalScore.Sum();
	}

	public void OnRestartClick()
	{
		Game.Instance.RestartGame();
		Hide();
	}

	public void OnExitClick()
	{
		Application.Quit();
	}
}