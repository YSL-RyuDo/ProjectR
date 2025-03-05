using UnityEngine;
using System.Collections;

public class BackPackExample : MonoBehaviour {
	private Animator anim;
	int tailSwagIdle;
	int tailSwagRun;
	int tailSwagWalk;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		tailSwagIdle = Animator.StringToHash("tailSwagIdle");
		tailSwagRun = Animator.StringToHash("tailSwagRun");
		tailSwagWalk = Animator.StringToHash("tailSwagWalk");
	
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("tailSwagIdle")) {  
				anim.SetBool (tailSwagRun, true); 
				anim.SetBool (tailSwagIdle, false);                                      
				anim.SetBool (tailSwagWalk, false);
			}
		} else if (Input.GetKeyUp (KeyCode.R)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("tailSwagRun")) {            //R to run swag
				anim.SetBool (tailSwagRun, false); 
				anim.SetBool (tailSwagIdle, true);                                      
				anim.SetBool (tailSwagWalk, false);
			}
		} else if (Input.GetKeyDown (KeyCode.W)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("tailSwagIdle")) {
				anim.SetBool (tailSwagRun,false); 
				anim.SetBool (tailSwagIdle, false);                                      
				anim.SetBool (tailSwagWalk, true);
			}
		} else if (Input.GetKeyUp (KeyCode.W)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("tailSwagWalk")) {          //W to Walk swag
				anim.SetBool (tailSwagRun, false); 
				anim.SetBool (tailSwagIdle, true);                                      
				anim.SetBool (tailSwagWalk, false);
			}
			}
		}
	}