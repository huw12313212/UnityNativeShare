using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;


public class RedCandleEveryPlayExtention : MonoBehaviour {

	[DllImport("__Internal")]
	private static extern bool ShareFile( string path );

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
		Debug.Log ("share:"+path);

		#if UNITY_IOS && !UNITY_EDITOR
				ShareFile (path);
		#endif

		#if UNITY_ANDROID
		AndroidJavaClass player = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = player.GetStatic<AndroidJavaObject>("currentActivity");
		currentActivity.Call<string>("ShareVideo",path);
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
}
