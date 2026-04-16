using UnityEngine;

public class EasyScreenRecordInitializer : MonoBehaviour
{

    [SerializeField] private bool useThisSetting = true;

    [SerializeField] private string folderName = "SmileCapture";
    [SerializeField] private SmileSoftScreenRecordController.AudioRecordingMode audioRecordingMode = SmileSoftScreenRecordController.AudioRecordingMode.MicAudio;
  
    [Header("a * 512 * 512 . Please change the value of a")]
    [SerializeField] private int bitrate = 5242880;
    [SerializeField] private int fps = 24;
    [Header("value should be 0, 90, 180, 270  degree")]
    [SerializeField] private int videoRotation = 0;
    [SerializeField] private SmileSoftScreenRecordController.VideoEncoder videoEncoder = SmileSoftScreenRecordController.VideoEncoder.H264;
    [SerializeField] private bool addInGallery = false;

    void SetUp()
    {
        // If want to store video in persistant data Path (Private Path) then use following line
        //SmileSoftScreenRecordController.instance.SetVideoStoringDestination(Application.persistentDataPath);
        

        SmileSoftScreenRecordController.instance.SetStoredFolderName(folderName); // only Android
        SmileSoftScreenRecordController.instance.SetBitRate(bitrate); // only Android
        SmileSoftScreenRecordController.instance.SetFPS(fps); // only Android

        SmileSoftScreenRecordController.instance.SetVideoRotation(videoRotation); // only Android 
        SmileSoftScreenRecordController.instance.SetVideoEncoder((int)videoEncoder); // only Android
       // SmileSoftScreenRecordController.instance.SetVideoSize((int)(Screen.width), (int)(Screen.height)); // only Android

        // Set audio recording mode: 0 = no audio, 1 = system/app audio, 2 = mic audio
        SmileSoftScreenRecordController.instance.SetAudioRecordingMode(audioRecordingMode);  

        SmileSoftScreenRecordController.instance.SetGalleryAddingCapabilities(addInGallery);
    }

    void Start()
    {
        if (useThisSetting) {
            //Invoke(nameof(SetUp), 1f);
            SetUp();
        }
    }

}
