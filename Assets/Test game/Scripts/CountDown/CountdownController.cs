using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownController : Panel, Timer.Delegate
{
	[HideInInspector]
	public static CountdownController instance;

	[SerializeField] private Image fillerImage;
	[SerializeField] private Text timerText;
	[SerializeField]private Timer timer;

	private bool _isTimerStarted = false;
	private float _countDownTime;

	private void Awake()
	{
		if (instance == null)
			instance = this;
		Hide();
		//_timer = this.gameObject.GetComponent<Timer>();
	}
	private void Update()
	{
		if (_isTimerStarted)
		{
			UpdateCountDownUi();
		}
	}

	public void SHowCountDown(float time)
	{
		Show();
		timer.SetDelegate(this);
		_countDownTime = time;
		timer.StartTimer(time);
		_isTimerStarted = true;
	}

	public void OnTimeComplete()
	{
		Hide();
		_isTimerStarted = false;
	}

	private void UpdateCountDownUi()
	{
		float remainingTime = _countDownTime - timer.GetElapsedTime();
		
		if (remainingTime >= 0)
		{
			float fillAmount =   remainingTime  / _countDownTime;
			fillerImage.fillAmount = fillAmount;
			timerText.text = (int) (remainingTime) + "s";
		}

	}
}
