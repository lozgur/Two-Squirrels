using UnityEngine;
using System.Collections;

public class Asansor : MonoBehaviour {
	Vector3 position1;
	Vector3 position2;
	GameObject asansor;


	void Awake(){
		asansor = GameObject.Find ("tas 1").gameObject;
		position1 = asansor.transform.position;
		position2 = new Vector3 (asansor.transform.position.x, asansor.transform.position.y+1.5f, asansor.transform.position.z);
	}
 void OnTriggerStay2D(Collider2D other){
	
		GetComponent<Animator>().SetBool("Press", true);

		asansor.transform.position = Vector3.Lerp (position1 , position2 , Time.time);
	}
	void OnTriggerExit2D(Collider2D other){
		GetComponent<Animator>().SetBool("Press", false);
		asansor.transform.position = Vector3.Lerp (position2 , position1 , Time.time);
	}


}
