using UnityEngine;
using UnityEngine.UI;

public class ScreenRecordManager : MonoBehaviour
{
    [SerializeField] private Text recordButtonText;

    private bool isRecording = false;

  
    void Start()
    {
        UpdateButtonState();
    }


    public void OnRecordButtonClicked()
    {
        if (isRecording)
        {
            StopRecording();
        }
        else
        {
            StartRecording();
        }

        isRecording = !isRecording;
        UpdateButtonState();
    }

    private void UpdateButtonState()
    {
        Debug.Log("U>> is Recording " + isRecording);
        if (recordButtonText != null)
            recordButtonText.text = isRecording ? "End Record" : "Start Record";
    }

    private void StartRecording()
    {
        SmileSoftScreenRecordController.instance.StartRecording();

    }

    private void StopRecording()
    {
        string path = SmileSoftScreenRecordController.instance.StopRecording();
        SmileSoftScreenRecordController.instance.PreviewVideo(path);


    }
}
