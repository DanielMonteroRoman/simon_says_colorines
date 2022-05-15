using System.Collections;
using System.Collections.Generic;
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

		/*Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		ss.Apply();*/

		Texture2D image = Resources.Load("image", typeof(Texture2D)) as Texture2D;
		

		string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
		File.WriteAllBytes(filePath, image.EncodeToPNG());

		//yield return null;

		// To avoid memory leaks
		new NativeShare().AddFile(filePath)
			.SetSubject("Colorines informa").SetText("Mira mi puntuación: " + scoreToShare +"puntos. ¿Capáz de Superarme? ¡Pruéba aquí!:").SetUrl("https://danielmontero.itch.io/colorines")
			.SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
			.Share();

		// Share on WhatsApp only, if installed (Android only)
		//if( NativeShare.TargetExists( "com.whatsapp" ) )
		//new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
	}


}
