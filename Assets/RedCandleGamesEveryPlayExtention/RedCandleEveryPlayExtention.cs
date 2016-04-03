using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;


public class RedCandleEveryPlayExtention : MonoBehaviour {

	[DllImport("__Internal")]
	private static extern bool ShareFile( string path );

	[DllImport("__Internal")]
	private static extern bool MergeFile( string video ,string audio);

	private static bool MicrophoneEnabled = false;

	public static void SetMicrophoneEnable(bool enabled)
	{
		MicrophoneEnabled = enabled;

		if(enabled)
		{
			Everyplay.FaceCamSetAudioOnly (true);
			Everyplay.FaceCamRequestRecordingPermission ();
		}
		else
		{
			Everyplay.FaceCamStopSession();
		}
	}


	public static void ShareLastRecordFile()
	{
		string file = GetEveryPlayFile ();

		if (file.Trim ().Length > 3) 
		{
			shareFile(file);
		}
		else
		{
			Debug.Log("No Record Founded");
		}
	}


	public static void shareFile(string path)
	{
		#if UNITY_IOS && !UNITY_EDITOR
				MergeAndShare ();
		#endif

		#if UNITY_ANDROID
		AndroidJavaClass player = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = player.GetStatic<AndroidJavaObject>("currentActivity");
		currentActivity.Call<string>("ShareVideo",path);
		string tempPath = path.Replace(".mp4","withAudio.mp4");
		if(File.Exists(tempPath))
		{
			File.Delete(tempPath);
		}

		currentActivity.Call<string>("MergeVideo",path,GetEveryPlayAudioFile(),tempPath);

		#endif
	}

	public static string GetEveryPlayFile()
	{
		try
		{
			#if UNITY_IOS
			string dic = Application.persistentDataPath.Replace ("Documents", "tmp/Everyplay/session");
			#elif UNITY_ANDROID
			string dic = Application.temporaryCachePath+"/sessions";
			#endif
			
			string[] directory = Directory.GetDirectories (dic);
			string[] subdirectoryFiles =  Directory.GetFiles (directory[0],"*.mp4");
			return subdirectoryFiles [0];
		}
		catch(System.Exception e)
		{
			return "";
		}
	}

	public static string GetEveryPlayAudioFile()
	{
		try
		{
			#if UNITY_IOS
			string dic = Application.persistentDataPath.Replace ("Documents", "tmp/Everyplay/session");
			#elif UNITY_ANDROID
			string dic = Application.temporaryCachePath+"/sessions";
			#endif
			
			string[] directory = Directory.GetDirectories (dic);
			string[] subdirectoryFiles =  Directory.GetFiles (directory[0],"*.m4a");
			return subdirectoryFiles [0];
		}
		catch(System.Exception e)
		{
			return "";
		}
	}

	public static bool IsRecording()
	{
		return Everyplay.IsRecording ();
	}

	public static bool IsRecordingSupported()
	{
		return 	Everyplay.IsRecordingSupported ();
	}

	public static void StartRecord()
	{
		if (MicrophoneEnabled) 
		{
			if (!Everyplay.FaceCamIsSessionRunning ()) 
			{

				Everyplay.FaceCamStartSession ();

			}
		}


		Everyplay.StartRecording ();
	}
	
	public static void StopRecord()
	{
		Everyplay.StopRecording ();
		Everyplay.FaceCamStopSession();
	}

	public static void PlayLastRecord()
	{
		Everyplay.PlayLastRecording ();
	}

	private static void MergeAndShare()
	{
		string audio = GetEveryPlayAudioFile ();
		string video = GetEveryPlayFile ();

		Debug.Log ("Unity try to merge :"+video +":"+ audio);

		MergeFile(video,audio);

		//MergeFile
	}
}
