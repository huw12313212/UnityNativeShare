using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class RecordingExample : MonoBehaviour {


	public Button b_record;
	public Button b_stop;
	public Button b_play;
	public Button b_share;

	
	void Start() 
	{
		Debug.Log("Supported:"+RedCandleEveryPlayExtention.IsRecordingSupported());

		RedCandleEveryPlayExtention.SetMicrophoneEnable (true);
	}


	void Update()
	{
		UpdateView();
	}

	void UpdateView()
	{
		b_record.gameObject.SetActive (RedCandleEveryPlayExtention.IsRecordingSupported()&&!RedCandleEveryPlayExtention.IsRecording());
		b_stop.gameObject.SetActive (RedCandleEveryPlayExtention.IsRecordingSupported()&&RedCandleEveryPlayExtention.IsRecording());
		b_share.gameObject.SetActive (RedCandleEveryPlayExtention.IsRecordingSupported()&&!RedCandleEveryPlayExtention.IsRecording());
		b_play.gameObject.SetActive (RedCandleEveryPlayExtention.IsRecordingSupported()&&!RedCandleEveryPlayExtention.IsRecording());
	}

	public void ShareLastFile()
	{
		string result = RedCandleEveryPlayExtention.GetEveryPlayFile();
		RedCandleEveryPlayExtention.shareFile(result);
	}


	public void Record()
	{
		RedCandleEveryPlayExtention.StartRecord ();
	}

	public void Stop()
	{
		RedCandleEveryPlayExtention.StopRecord ();
	}

	public void PlayLast()
	{
		RedCandleEveryPlayExtention.PlayLastRecord ();
	}

	public void Share()
	{
		RedCandleEveryPlayExtention.ShareLastRecordFile ();
	}



}
