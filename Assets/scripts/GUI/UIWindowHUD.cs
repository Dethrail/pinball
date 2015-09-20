using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// HUD window, informs the player about what is happening on scene, on mobile device have two huge buttons to control the flipper
/// </summary>
public class UIWindowHUD:UIWindow
{
	public Slider SliderLaunchPower;
	public Button ButtonLaunch;

	public Button ButtonLeftFlipper;
	public Button ButtonRightFlipper;


	public Text TextCurrentScore;
	public Text TextLives;
	public Text TextTotalScore;


	private string _rawTextLives;
	private string _rawTextCurrentScore;
	private string _rawTextTotalScore;

	protected override void OnInitialize()
	{
		base.OnInitialize();
		_rawTextCurrentScore = TextCurrentScore.text;
		_rawTextLives = TextLives.text;
		_rawTextTotalScore = TextTotalScore.text;

		Game.Instance.PullSpring.SpringPower = SliderLaunchPower.value;
	}

	public void ShowHideLauncher(bool state)
	{
		SliderLaunchPower.gameObject.SetActive(state);
		ButtonLaunch.gameObject.SetActive(state);
	}

	public void OnSliderValueChanged(float value)
	{
		Game.Instance.PullSpring.SpringPower = value;
	}

	public void OnLaunchClick()
	{
		Game.Instance.PullSpring.Fire = true;
	}

	public void OnFlipperClick(bool isLeftFlipper)
	{
		if(isLeftFlipper) {
			foreach(Flipper flipper in Game.Instance.LeftFlippers) {
				flipper.TriggerUI = true;
			}
		} else {
			foreach(Flipper flipper in Game.Instance.LeftFlippers) {
				flipper.TriggerUI = true;
			}
		}
	}

	public void OnMenuClick()
	{
		Time.timeScale = 0;
		UIWindowManager.WindowMenu.Show();
	}

	public override void Update()
	{
		TextLives.text = _rawTextLives + " " + Game.Instance.Lives;
		TextCurrentScore.text = _rawTextCurrentScore + " " + Game.Instance.CurrentScore;
		TextTotalScore.text = _rawTextTotalScore + " " + (Game.Instance.CacheTotalScore + Game.Instance.CurrentScore);

		SliderLaunchPower.value = Game.Instance.PullSpring.SpringPower;
	}
}