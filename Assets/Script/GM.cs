using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GM : MonoBehaviour {
	int KırmızıPuan = 0;
	int MaviPuan = 0;
	bool PauseGame = false;
	[HideInInspector]
	public bool EnterBlue = false;
	[HideInInspector]
	public bool EnterRed = false;
	[HideInInspector]
    public int GameTime;

	// Use this for initialization
	void Start () {
		GameTime =0;	
	}
	
	// Update is called once per frame
	void Update () {
		if (GameTime == 0) {
			GameTime=(int)Time.time;
		}
		if (Input.GetKeyDown (KeyCode.Escape) && !PauseGame) {
			Time.timeScale = 0;
			GameObject.Find ("AraMenu").GetComponent<Image> ().enabled = true;
			GameObject.Find ("Continue").GetComponent<Image> ().enabled = true;
			GameObject.Find ("QuitMenu").GetComponent<Image> ().enabled = true;
			PauseGame = true;
		} else if (Input.GetKeyDown (KeyCode.Escape) && PauseGame){
			Time.timeScale = 1;
			GameObject.Find ("AraMenu").GetComponent<Image> ().enabled = false;
			GameObject.Find ("Continue").GetComponent<Image> ().enabled = false;
			GameObject.Find ("QuitMenu").GetComponent<Image> ().enabled = false;
			PauseGame = false;
		}

		GameObject.Find ("Text").GetComponent<Text>().text = "time: " + (60-((int)Time.time-GameTime));
		GameObject.Find ("MaviText").GetComponent<Text> ().text = "Score: " + MaviPuan;
		GameObject.Find ("KirmiziText").GetComponent<Text>().text = "Score: " + KırmızıPuan;

		if((int)Time.time-GameTime >=60){
			//GameOver....
			GameTime=0;
			Application.LoadLevel("gameover");
			Debug.LogError("GameOver");
		}

		if (EnterBlue && EnterRed) {
			// Load NEXT LEVEL....
			Debug.LogError("FinishGame :D");
			if(Application.loadedLevel + 1 < 4){
				Application.LoadLevel(Application.loadedLevel + 1);
			}

		}
	}


	public void CountinueButton(){
		Time.timeScale = 1;
		GameObject.Find ("AraMenu").GetComponent<Image> ().enabled = false;
		GameObject.Find ("Continue").GetComponent<Image> ().enabled = false;
		GameObject.Find ("QuitMenu").GetComponent<Image> ().enabled = false;
		PauseGame = false;
	}

	public void SetRedTime(){

		KırmızıPuan += 60-((int)Time.time-GameTime);
	}

	public void SetBlueTime(){

		MaviPuan += 60-((int)Time.time-GameTime);
	}

	public void AddBluePoint(){
		MaviPuan += 10;
	}
	
	public void AddRedPoint(){
		KırmızıPuan += 10;
	}
	
}
