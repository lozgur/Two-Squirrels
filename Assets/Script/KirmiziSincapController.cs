using UnityEngine;
using System.Collections;

public class KirmiziSincapController : MonoBehaviour {
	bool jumped;
	float boundary = 0;
	GM gm;
	Color x;
	Animator anim;
	bool dead = false;

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


		if (!dead && Input.GetKey (KeyCode.RightArrow)) { 
			transform.Translate (Vector2.right * speed * Time.deltaTime); 
			transform.eulerAngles = new Vector2 (0, 0);
			anim.SetBool ("KirmiziRun", true);
			anim.SetBool ("KirmiziJump", false);
		} else if (!dead && Input.GetKey (KeyCode.LeftArrow)) {
			transform.Translate (Vector2.right * speed * Time.deltaTime);
			transform.eulerAngles = new Vector2 (0, 180);
			anim.SetBool ("KirmiziRun", true);
			anim.SetBool ("KirmiziJump", false);
		} else {
			anim.SetBool("KirmiziRun", false);
			//anim.SetBool ("KirmiziJump", false);
		}


			
		if(!dead && !jumped && Input.GetKey(KeyCode.UpArrow)) 
		{
			GetComponent<Rigidbody2D>().AddForce(transform.up *250f);
			boundary = Time.time + 1f;
				anim.SetBool ("KirmiziJump", true);
				jumped=true;
			}

		if (jumped && (boundary<=Time.time)) {
			anim.SetBool("KirmiziJump", false);
			jumped=false;
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
		if (other.name == "kırmızı anahtar") {
			Destroy (other.gameObject);
			GameObject.Find ("kırmızı kapı").GetComponent<Animator> ().SetBool ("KırmızıAnahtar", true);
			GameObject.Find ("kırmızı kapı").GetComponent<Collider2D> ().enabled = true;
		} else if (other.tag == "RedDimond") {
			gm.AddRedPoint ();
			Destroy (other.gameObject);
		} else if (other.name == "kırmızı kapı") {
			Destroy (gameObject);
			gm.SetRedTime ();
			gm.EnterRed = true;
		} else if (other.tag == "water") {
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


