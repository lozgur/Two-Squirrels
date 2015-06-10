using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

	GM gm;
	[HideInInspector]

	// Use this for initialization
	void Start () {

		if (Application.loadedLevelName != "MainMenu" && Application.loadedLevelName != "gameover") {
			gm = GameObject.Find ("GM").GetComponent<GM> ();
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayButton(){

		Application.LoadLevel (Application.loadedLevel + 1);
	}

	public void ContinueButton(){
		gm.CountinueButton ();
	}

	public void QuitMainMenuButton(){
		Time.timeScale = 1;
		Application.LoadLevel (0);
	}

	public void QuitButton(){
		Application.Quit ();
	}
}
