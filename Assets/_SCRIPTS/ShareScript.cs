using System.Collections;
using UnityEngine;
using System.IO;

public class ShareScript : MonoBehaviour
{
	ScoreManager scoreMan;

	int scoreToShare;

	private void Awake()
    {
       scoreMan = GameObject.Find("SCORE MANAGER").GetComponent<ScoreManager>();
    }
    public void ClickShare()
    {
		scoreToShare=scoreMan._bigHScore;

		StartCoroutine(LoadImageAndShare());
    }

	private IEnumerator LoadImageAndShare()
	{
		yield return new WaitForEndOfFrame();

		//HACER UNA CAPTURA DE PANTALLA, LA IMAGEN CREADA SE LLAMA "ss":
		
		Texture2D image = Resources.Load("compartir", typeof(Texture2D)) as Texture2D;
		

		string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
		File.WriteAllBytes(filePath, image.EncodeToPNG());

		//yield return null;

		// To avoid memory leaks
		new NativeShare().AddFile(filePath)
			.SetSubject("Colorines info").SetText("Look at my score: " + scoreToShare +" points. Can you beat it? ¡try here!:").SetUrl("https://play.google.com/store/apps/details?id=com.DefaultCompany.COLORINES&hl=es&gl=US")
			.SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
			.Share();

		// Share on WhatsApp only, if installed (Android only)
		//if( NativeShare.TargetExists( "com.whatsapp" ) )
		//new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
	}


}
