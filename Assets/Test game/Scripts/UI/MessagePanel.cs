using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : Panel
{
	public static MessagePanel instance;

	[SerializeField] private Text messageText;

	private void Awake()
	{
		if (instance == null)
			instance = this;

		Hide();
	}

	public void ShowMessage(string message)
	{
		Show();
		messageText.text = message;
	}
}
