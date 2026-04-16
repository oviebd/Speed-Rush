using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ExampleScreenRecorder : MonoBehaviour
{

	[SerializeField] private GameObject afterVideoCompletePanel;
	[SerializeField] private Text savedPathText;
	[SerializeField] private Button previewButton;
	[SerializeField] private Button ShareButton;
	[SerializeField] private AlertPanel alertPanel;

	private string _recordedFilePath;

	void Start()
	{
		alertPanel.Hide();
		HideAfterVideoCompletePanel();
	}


	public void StartRecording()
	{
		SetFileName();

		bool iSitIos = SmileSoftScreenRecordController.instance.IsIosPlatform();
		// If it is iOS 
		if (iSitIos && SmileSoftScreenRecordController.instance.IsRecordingAvailable() == false)
		{
			// show error message
			alertPanel.ShowAlert("Recorder Unavailable in this device!");
			return;
        }

		SmileSoftScreenRecordController.instance.StartRecording();

	}
	public void StopRecording()
	{
		_recordedFilePath = null;
		_recordedFilePath = SmileSoftScreenRecordController.instance.StopRecording();

		if (SmileSoftScreenRecordController.instance.IsIosPlatform())
        {
			PreviewVideo();
        }
		if (SmileSoftScreenRecordController.instance.IsAndroidPlatform())
        {
			ShowAndroidVideoCompletatoonDialog();
		}
			
	}

	private void SetFileName()
	{
		System.DateTime now = System.DateTime.Now;
		string date = now.ToShortDateString().Replace('/', '_')
					+ now.ToLongTimeString().Replace(':', '_');
		string fileName = "Record_" + date;

		SmileSoftScreenRecordController.instance.SetVideoName(fileName);
	}

	private void ShowAndroidVideoCompletatoonDialog()
	{
		afterVideoCompletePanel.SetActive(true);
		if (_recordedFilePath != null && File.Exists(_recordedFilePath))
		{
			previewButton.interactable = true;
			ShareButton.interactable = true;
			savedPathText.text = "Video saved successfully at : " + _recordedFilePath;
		}
		else
		{
			previewButton.interactable = false;
			ShareButton.interactable = false;
			savedPathText.text = "Error occured. Can not record video";
		}

	}

	public void  PreviewVideo()
	{
		//for iOS this perameter is not affect anything. You can just send a null or empty value as a perameter for iOS 
	    SmileSoftScreenRecordController.instance.PreviewVideo(_recordedFilePath);
	}

	public void ShareVideo()
	{
		SmileSoftScreenRecordController.instance.ShareVideo(_recordedFilePath, "Greetings From SmileSoft", "Sunshine Native Share");
	}

	public void HideAfterVideoCompletePanel()
	{
		afterVideoCompletePanel.SetActive(false);
	}

}
