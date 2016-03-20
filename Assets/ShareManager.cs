using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;


public class ShareManager : MonoBehaviour {

	[DllImport("__Internal")]
	private static extern bool ShareFile( string path );





	public void shareFile(string path)
	{
		Debug.Log ("share:"+path);

		#if UNITY_IPHONE && !UNITY_EDITOR
				ShareFile (path);
		#endif

		#if UNITY_ANDROID

		Debug.Log("Ae");
		AndroidJavaClass player = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		Debug.Log("Be");
		AndroidJavaObject currentActivity = player.GetStatic<AndroidJavaObject>("currentActivity");
		Debug.Log("Ce");
		Debug.Log("Try to Share");
		currentActivity.Call<string>("ShareVideo",path);
		Debug.Log("Try to Share Done");
	
		/*
		AndroidJavaClass mediaStoreClass = new AndroidJavaClass("android.provider.MediaStore");
		Debug.Log("A-1");
		AndroidJavaObject values = new AndroidJavaObject("android.content.ContentValues",3);

		AndroidJavaObject DATA = new AndroidJavaClass("MediaStore.Video.Media").GetStatic<AndroidJavaObject>("DATA");

		Debug.Log("DATA:"+DATA.Call<string>("toString"));

		values.Call<string>("put", DATA, path);

		Debug.Log("Content:"+values.Call<string>("toString"));
		Debug.Log("A-2");


		values.Call<string>("put", "_data", path);
		Debug.Log("A-1-1");

		AndroidJavaObject resolver = currentActivity.Call<AndroidJavaObject>("getContentResolver");
		resolver.Call<AndroidJavaObject>("insert",mediaStoreClass.GetStatic<string>("MediaStore.Video.Media.EXTERNAL_CONTENT_URI"),values);




		AndroidJavaClass intentClass = new AndroidJavaClass ("android.content.Intent");
		AndroidJavaObject intentObject = new AndroidJavaObject ("android.content.Intent");
		
		AndroidJavaClass uri = new AndroidJavaClass ("android.net.Uri");
		AndroidJavaObject resultURI = uri.CallStatic<AndroidJavaObject>("parse",path);
		
		intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
		intentObject.Call<AndroidJavaObject>("setType", "image/*");
		
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "Video");
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), resultURI);


		AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share");
		currentActivity.Call("startActivity", jChooser);

*/

		/*
		AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		currentActivity.Call<string>("shareVideo",path);*/



		/*
		string temp ="/storage/emulated/0/DCIM/Camera/0_Video.mp4";

		if(File.Exists(temp))File.Delete(temp);

		Debug.Log("Try Copy");

		try{

		File.Copy(path,temp);
		Debug.Log("Try Copied");

		}
		catch(System.Exception e)
		{
			Debug.LogError(e.StackTrace);
		}*/




		/*
		AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
		AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
		intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
		intentObject.Call<AndroidJavaObject>("setType", "video/mp4");
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "Media Sharing ");
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), "Media Sharing ");
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "Media Sharing Android Demo");
		AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
		AndroidJavaClass fileClass = new AndroidJavaClass("java.io.File");
		AndroidJavaObject fileObject = new AndroidJavaObject("java.io.File", path);// Set Image Path Here
		AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("fromFile", fileObject);

		Debug.Log("result?");
		Debug.Log("Result Path:"+uriObject.Call<string>("toString"));

		intentObject.Call<AndroidJavaObject>("addFlags", 0x00000040);
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
	//	AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
//		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		currentActivity.Call("startActivity", intentObject);*/


		/*
		Debug.Log("A");
		AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
		AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
		intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_MEDIA_SCANNER_SCAN_FILE"));
		AndroidJavaClass uri = new AndroidJavaClass ("android.net.Uri");
		AndroidJavaObject resultURI = uri.CallStatic<AndroidJavaObject>("parse",temp);
		intentObject.Call<AndroidJavaObject>("setData", resultURI);
		Debug.Log("B");
		AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		currentActivity.Call("sendBroadcast", intentObject);
		Debug.Log("C");
		//sendBroadcast(new Intent(Intent.ACTION_MEDIA_SCANNER_SCAN_FILE, Uri.fromFile(imageAdded)));*/



		/*
		Debug.Log("A");

		AndroidJavaClass mediaStoreClass = new AndroidJavaClass("android.provider.MediaStore");
		Debug.Log("A-1");
		AndroidJavaObject values = new AndroidJavaObject("android.content.ContentValues",4);
		Debug.Log("A-1-1");
		values.Call<AndroidJavaObject>("put", mediaStoreClass.GetStatic<string>("MediaStore.Video.Media.TITLE"), "My video title");
		Debug.Log("A-2");
		values.Call<AndroidJavaObject>("put", mediaStoreClass.GetStatic<string>("MediaStore.Video.Media.MIME_TYPE"), "video/mp4");
		Debug.Log("A-3");
		values.Call<AndroidJavaObject>("put", mediaStoreClass.GetStatic<string>("MediaStore.Video.Media.DATA"), path);
		Debug.Log("B");
		AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		Debug.Log("C");
		AndroidJavaObject resolver = currentActivity.Call<AndroidJavaObject>("getContentResolver");
		resolver.Call<AndroidJavaObject>("insert",mediaStoreClass.GetStatic<string>("MediaStore.Video.Media.EXTERNAL_CONTENT_URI"),values);
		Debug.Log("D");*/
		                                 /*
		values.put(MediaStore.Video.Media.TITLE, "My video title");
		values.put(MediaStore.Video.Media.MIME_TYPE, "video/mp4");
		values.put(MediaStore.Video.Media.DATA, videoFile.getAbsolutePath());
		return getContentResolver().insert(MediaStore.Video.Media.EXTERNAL_CONTENT_URI, values);*/


		#endif

		//android.net.Uri

	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{


	
	}
}
