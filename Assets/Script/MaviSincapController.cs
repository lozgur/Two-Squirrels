using UnityEngine;
using System.Collections;

public class MaviSincapController : MonoBehaviour {
	Color x;
	bool jumped;
	bool dead = false;
	float boundary = 0;
	GM gm;
	Animator anim;
	public float speed = 6.0f;
	// Use this for initialization
	void Start () {
		x.r = 255;
		x.g = 255;
		x.b = 255;
		x.a = 1;
		anim = GetComponent<Animator>();
		gm = GameObject.Find ("GM").GetComponent<GM> ();
	}
	


	// Update is called once per frame
	void Update () {
			Movement ();

	}




	void Movement(){


		if (!dead && Input.GetKey (KeyCode.D)) { 
			transform.Translate (Vector2.right * speed * Time.deltaTime); 
			transform.eulerAngles = new Vector2 (0, 0);
			anim.SetBool ("MaviRun", true);
			anim.SetBool ("MaviJump", false);
		} else if (!dead && Input.GetKey (KeyCode.A)) {
			transform.Translate (Vector2.right * speed * Time.deltaTime);
			transform.eulerAngles = new Vector2 (0, 180);
			anim.SetBool ("MaviRun", true);
			anim.SetBool ("MaviJump", false);
		} else {
			anim.SetBool ("MaviRun", false);
			//anim.SetBool ("MaviJump", false);
		}


			
		if (!dead && !jumped && Input.GetKey (KeyCode.W)) {
			GetComponent<Rigidbody2D> ().AddForce (transform.up * 250f);
			boundary = Time.time + 1f;
			anim.SetBool ("MaviJump", true);
			jumped = true;
		}
		



		if (jumped && boundary <= Time.time) {
			anim.SetBool ("MaviJump", false);
			jumped = false;
		
		}
	}
	void OnCollisionStay2D(Collision2D other){
		if (other.gameObject.name == "tas 1") {
			gameObject.transform.parent = other.transform;
		}
	}

	void OnCollisionExit2D(Collision2D other){
		if (other.gameObject.name == "tas 1") {
			gameObject.transform.parent = null;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "mavi anahtar") {
			Destroy (other.gameObject);
			GameObject.Find ("mavi kapı").GetComponent<Animator> ().SetBool ("MaviAnahtar", true);
			GameObject.Find ("mavi kapı").GetComponent<Collider2D> ().enabled = true;
		} else if (other.tag == "BlueDimond") {
			gm.AddBluePoint ();
			Destroy (other.gameObject);
		} else if (other.name == "mavi kapı") {
			Destroy (gameObject);
			gm.SetBlueTime ();
			gm.EnterBlue = true;
		} else if (other.tag == "fire") {
			gameObject.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().enabled = true;
			gameObject.transform.GetChild (0).gameObject.GetComponent<Animator> ().SetBool ("patlama", true);
			gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			StartCoroutine(SpriteAlfaKapa());
			dead = true;
			// GameOver...
		}


	}

	IEnumerator SpriteAlfaKapa(){
		yield return new WaitForSeconds (1);
		for(float i = 1f; i> 0; i-= 0.02f){
			x.a=i;
			gameObject.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().color = x ;
			yield return null;
		}
		yield return new WaitForSeconds (1);
		Application.LoadLevel("gameover");
	}

	}


