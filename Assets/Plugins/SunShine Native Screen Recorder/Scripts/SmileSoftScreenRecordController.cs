using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SmileSoftScreenRecordController : MonoBehaviour
{
	public static SmileSoftScreenRecordController instance;

	private string _fileProvider = "com.SmileSoft.unityplugin.ScreenRecordProvider";

	private AndroidJavaObject screenRecorder;

	private ReplayKitHelper _iosRecorder;

	private bool _isIosTryingToStartRecording = false;

	private void Awake()
	{
		if (instance == null)
			instance = this;

		Setup();
	}

    private void Start()
    {
		//Debug.Log("U>> Height is " + Screen.height + " Width " + Screen.width);
    }


	void OnApplicationFocus(bool hasFocus)
	{
		if (IsIosPlatform() && hasFocus && _isIosTryingToStartRecording)
		{
			Invoke("CheckIsRecording", 1.0f);
		}
	}

	private void CheckIsRecording()
    {
		OnIosRecordStatus(_iosRecorder.IsRecording());
	}

	void Setup()
	{
		if (IsAndroidPlatform())
		{
			screenRecorder = new AndroidJavaObject("com.SmileSoft.unityplugin.ScreenCapture.ScreenRecordFragment");
			screenRecorder?.Call("SetUp");
		}

		if (IsIosPlatform())
		{
			 _iosRecorder = new ReplayKitHelper();
		}
	}

	public void StartRecording()
	{
		if (IsAndroidPlatform())
			screenRecorder?.Call("StartRecording");

		if (IsIosPlatform())
        {

			if (_iosRecorder.IsRecorderAvailable())
            {
				_isIosTryingToStartRecording = true;
				_iosRecorder.StartRecording();
			}
            else
            {
				OnIosRecordStatus(false);
			}
        }
			
	}

	public string StopRecording()
	{
		if (IsAndroidPlatform())
		{
			string recordedPath = screenRecorder?.Call<string>("StopRecording");
			Debug.Log("Unity>> record path : " + recordedPath);
			return recordedPath;
		}

		if (IsIosPlatform())
        {
			_iosRecorder.StopRecording();
        }
			

		return null;
	}

	

	public void SetVideoStoringDestination(string destination)
	{
		if (IsAndroidPlatform())
			screenRecorder?.Call("SetVideoStoringDestination", destination);
	}
	public void SetStoredFolderName(string folderName)
	{
		if (IsAndroidPlatform())
			screenRecorder?.Call("SetVideoStoredFolderName", folderName);
	}

	public void SetVideoName(string videoName)
	{
		if (IsAndroidPlatform())
			screenRecorder?.Call("SetVideoName", videoName);
	}

	public void SetGalleryAddingCapabilities(bool canAddintoGallery)
	{
		if (IsAndroidPlatform())
			screenRecorder?.Call("SetGalleryAddingCapabilities", canAddintoGallery);
	}

	/// <summary>
	/// Audio recording mode enum
	/// </summary>
	public enum AudioRecordingMode
	{
		NoAudio = 0,        // No audio recording
		SystemAudio = 1,    // Record system/app audio (Android 10+)
		MicAudio = 2         // Record microphone audio
	}

	/// <summary>
	/// Set audio recording mode
	/// 0 = No audio recording (no permissions needed)
	/// 1 = System/app audio (uses MediaProjection API on Android 10+, falls back to mic if not supported)
	///     Note: RECORD_AUDIO permission is NOT required for mode 1 - uses a service with only mediaProjection type
	/// 2 = Microphone audio (requires RECORD_AUDIO permission for actual microphone usage)
	/// </summary>
	/// <param name="mode">Audio recording mode (0=no audio, 1=system audio, 2=mic audio)</param>
	private void SetAudioRecordingMode(int mode)
	{
		if (IsAndroidPlatform())
			screenRecorder?.Call("SetAudioRecordingMode", mode);
		if (IsIosPlatform())
		{
			// iOS only supports mic audio, so convert mode 1 to mode 2
			if (mode == 1)
				_iosRecorder.SetAudioCapability(true);
			else if (mode == 2)
				_iosRecorder.SetAudioCapability(true);
			else
				_iosRecorder.SetAudioCapability(false);
		}
	}

	/// <summary>
	/// Set audio recording mode using enum
	/// </summary>
	/// <param name="mode">Audio recording mode enum</param>
	public void SetAudioRecordingMode(AudioRecordingMode mode)
	{
		SetAudioRecordingMode((int)mode);
	}

	/// <summary>
	/// Check if the device supports system/app audio recording.
	/// System audio recording is supported on Android 10+ (API 29+) using MediaProjection API.
	/// On older Android versions, this will return false and the plugin will fallback to microphone audio.
	/// </summary>
	/// <returns>true if system audio recording is supported, false otherwise</returns>
	public bool IsSystemAudioSupported()
	{
		if (IsAndroidPlatform())
		{
			return screenRecorder?.Call<bool>("IsSystemAudioSupported") ?? false;
		}
		// iOS doesn't support system audio recording
		return false;
	}
	public void SetFPS(int fps)
	{
		if (IsAndroidPlatform())
			screenRecorder?.Call("SetVideoFps", fps);
	}

	public void SetVideoRotation(int rotation)
	{
		if (IsAndroidPlatform())
			screenRecorder?.Call("SetVideoRotation", rotation);
	}

	public void SetBitRate(int bitRate)
	{
		if (IsAndroidPlatform())
			screenRecorder?.Call("SetBitrate", bitRate);
	}

	public void SetVideoSize(int width, int height)
	{
		if (IsAndroidPlatform())
			screenRecorder?.Call("SetVideoSize", width, height);
	}

	public void SetVideoEncoder(int encoder)
	{
		if (IsAndroidPlatform())
			screenRecorder?.Call("SetVideoEncoder", encoder);
	}

	public void PreviewVideo(string videoPath)
	{
		if (IsAndroidPlatform() && (videoPath != null && File.Exists(videoPath)))
        {
			screenRecorder?.Call("PreviewVideo", videoPath);
			return;
		}

		if (IsIosPlatform())
        {
			StartCoroutine(IosPreview((isSuccess) => {
				
			}));
		}
	}

	// Wait a bit for preparing the preview
	private IEnumerator IosPreview (System.Action<bool> callback)
	{
		yield return new WaitForSeconds(1.0f);
		bool isSuccess = _iosRecorder.ShowPreview();
		callback(isSuccess);

	}

	public void ShareVideo(string filePath,string message, string title)
	{
		if (IsAndroidPlatform() &&  (filePath != null && File.Exists(filePath)))
			screenRecorder?.Call("ShareVideo", filePath,message,title,_fileProvider);
	}

	public enum VideoEncoder
	{
		DEFAULT = 0, H263 = 1, H264 = 2, MPEG_4_SP = 3, VP8 = 4, HEVC = 5
	}


	//CallBack From Android library
	public void OnRecordPermissionGranted(string status)
	{
		if (status == "True")
        {
			Debug.Log("Unity>> All Permissions are granted  ");
        }
        else
        {
			Debug.Log("Unity>> All Permissions are not granted.Please check the permission list from app setting.");
		}
	}

	public void OnRecordStartStatus(string status)
	{
		if (status == "True")
		{
			Debug.Log("Unity>> Record Started  ");
		}
		else
		{
			Debug.Log("Unity>> Record Failed ");
		}
	}


	// Only Work in IOS
	public bool IsRecordingAvailable()
	{
		if (IsIosPlatform())
		{
			return _iosRecorder.IsRecorderAvailable();
		}

		return true;
	}

	// Only Work in IOS
	private void OnIosRecordStatus(bool isSuccess)
    {
		if (isSuccess)
		{
			Debug.Log("Unity>> Ios Record Started  ");
		}
		else
		{
			Debug.Log("Unity>> Ios Record Failed  ");
		}
		
	}

	public bool IsAndroidPlatform()
	{
		bool result = false;

#if UNITY_ANDROID && !UNITY_EDITOR
		result = true;
#endif

		return result;
	}

	public bool IsIosPlatform()
	{
		bool result = false;

#if UNITY_IPHONE && !UNITY_EDITOR
		result = true;
#endif

		return result;
	}


}
