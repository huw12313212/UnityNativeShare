using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class RecordingTest : MonoBehaviour {


	public Button b_record;
	public Button b_stop;
	public Button b_modal;
	public Button b_play;

	public void OnReadyForRecording(bool enabled) 
	{
		Debug.Log ("enabled:"+enabled);
	}
	
	void Start() 
	{
		Everyplay.ReadyForRecording += OnReadyForRecording;
		Debug.Log("Supported:"+Everyplay.IsRecordingSupported());
		Everyplay.PlayLastRecording ();
	}


	void Update()
	{
		UpdateView();
	}

	void UpdateView()
	{
		b_record.gameObject.SetActive (Everyplay.IsRecordingSupported()&&!Everyplay.IsRecording());
		b_stop.gameObject.SetActive (Everyplay.IsRecordingSupported()&&Everyplay.IsRecording());
		b_modal.gameObject.SetActive (Everyplay.IsRecordingSupported()&&!Everyplay.IsRecording());
		b_play.gameObject.SetActive (Everyplay.IsRecordingSupported()&&!Everyplay.IsRecording());
	}

	public void CrawlAllFiles()
	{
		try
		{
			string result = GetEveryplayFile();
			Debug.Log("File :"+result);
			this.GetComponent<ShareManager>().shareFile(result);

	

		}
		catch(System.Exception e)
		{
			Debug.Log ("No File Founded");
		}
		//DumpPath();


	}

	private string GetEveryplayFile()
	{
		try
		{
			#if UNITY_IPHONE
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

	private void DumpPath(string path)
	{

		string result = "";

		result+="--------------------\n";
		result+=("Start Dumpping:"+path+"\n");
		result+="--------------------\n";

		string[] files = Directory.GetFiles (path);
		string[] directory = Directory.GetDirectories (path);
		//string[] subdirectory =  Directory.GetFiles (directory[0]);



		result+="---------Directory-----------"+"\n";
		foreach(string s in directory)
		{
			result+=(s+"\n");
		}
		result+="---------Directory-----------"+"\n";

		
		result+="---------Files-----------"+"\n";
		foreach(string s in files)
		{
			result+=(s+"\n");
		}
		result+="---------Files-----------"+"\n";

		/*
		result+="---------SubDirectoryFiles-----------"+"\n";
		foreach(string s in subdirectory)
		{
			result+=(s+"\n");
		}
		result+="---------SubDirectoryFiles-----------"+"\n";
*/
		Debug.Log (result);
	}

	public void StartRecord()
	{
		Everyplay.StartRecording ();
	}

	public void StopRecord()
	{
		Everyplay.StopRecording ();
	}

	public void ShowModalRecord()
	{
		Everyplay.ShowSharingModal ();
	}

	public void PlayLastRecord()
	{
		Everyplay.PlayLastRecording ();
	}
}
