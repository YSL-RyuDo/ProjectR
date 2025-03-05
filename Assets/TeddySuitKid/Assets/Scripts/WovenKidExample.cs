using UnityEngine;
using System.Collections;

public class WovenKidExample : MonoBehaviour {
	private Animator anim;
	int Idle;
	int Walk;
	int Run;
	int BadgerPawAttack;
	int BalloonFishDive;
	int BalloonFishFloat;
	int BalloonFishSwim;
	int Celebrates;
	int Jump;
	int FoxJump;
	int KoalaClimb;
	int KoalaClimbReachTop;
	int KoalaJumpToClimb;
	int Collect;
	int FallSitted;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		Idle = Animator.StringToHash("Idle");
		Walk = Animator.StringToHash("Walk");
		Run = Animator.StringToHash("Run");
		BadgerPawAttack = Animator.StringToHash("BadgerPawAttack");
		BalloonFishDive = Animator.StringToHash("BalloonFishDive");
		BalloonFishFloat = Animator.StringToHash("BalloonFishFloat");
		BalloonFishSwim = Animator.StringToHash("BalloonFishSwim");
		Celebrates = Animator.StringToHash("Celebrates");
		Jump = Animator.StringToHash("Jump");
		FoxJump = Animator.StringToHash("FoxJump");
		KoalaClimb = Animator.StringToHash("KoalaClimb");
		KoalaClimbReachTop = Animator.StringToHash("KoalaClimbReachTop");
		KoalaJumpToClimb = Animator.StringToHash("KoalaJumpToClimb");
		Collect = Animator.StringToHash("Collect");
		FallSitted = Animator.StringToHash("FallSitted");



	
	}

	// Update is called once per frame
	void Update () {
		     if    (Input.GetKeyDown(KeyCode.W)) {
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {  
				anim.SetBool (Walk, true); 
				anim.SetBool (Idle, false);                                      
				anim.SetBool (Run, false);
				anim.SetBool (BadgerPawAttack, false);
				anim.SetBool (BalloonFishDive, false);  
				anim.SetBool (BalloonFishFloat, false);  
				anim.SetBool (BalloonFishSwim, false); 
				anim.SetBool (Celebrates, false);
				anim.SetBool (Jump, false);
				anim.SetBool (FoxJump, false);
				anim.SetBool (KoalaClimb, false);
				anim.SetBool (KoalaClimbReachTop, false);
				anim.SetBool (KoalaJumpToClimb, false);
				anim.SetBool (Collect, false);
				anim.SetBool (FallSitted, false);
			}
		} else if (Input.GetKeyUp(KeyCode.W)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Walk")) {            //W to walk
				anim.SetBool (Walk, false); 
				anim.SetBool (Idle, true);                                      
				anim.SetBool (Run, false);
				anim.SetBool (BadgerPawAttack, false);
				anim.SetBool (BalloonFishDive, false);  
				anim.SetBool (BalloonFishFloat, false);  
				anim.SetBool (BalloonFishSwim, false); 
				anim.SetBool (Celebrates, false);
				anim.SetBool (Jump, false);
				anim.SetBool (FoxJump, false);
				anim.SetBool (KoalaClimb, false);
				anim.SetBool (KoalaClimbReachTop, false);
				anim.SetBool (KoalaJumpToClimb, false);
				anim.SetBool (Collect, false);
				anim.SetBool (FallSitted, false); 
			}
		} else if (Input.GetKeyDown(KeyCode.Q)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {
				anim.SetBool (Walk, false); 
				anim.SetBool (Idle, false);                                      
				anim.SetBool (Run, false);
				anim.SetBool (BadgerPawAttack, false);
				anim.SetBool (BalloonFishDive, false);  
				anim.SetBool (BalloonFishFloat, false);  
				anim.SetBool (BalloonFishSwim, false); 
				anim.SetBool (Celebrates, false);
				anim.SetBool (Jump, true);
				anim.SetBool (FoxJump, false);
				anim.SetBool (KoalaClimb, false);
				anim.SetBool (KoalaClimbReachTop, false);
				anim.SetBool (KoalaJumpToClimb, false);
				anim.SetBool (Collect, false);
				anim.SetBool (FallSitted, false); 
			}
		} else if (Input.GetKeyUp(KeyCode.Q)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Jump")) {          //Q to Jump
				anim.SetBool (Walk, false); 
				anim.SetBool (Idle, true);                                      
				anim.SetBool (Run, false);
				anim.SetBool (BadgerPawAttack, false);
				anim.SetBool (BalloonFishDive, false);  
				anim.SetBool (BalloonFishFloat, false);  
				anim.SetBool (BalloonFishSwim, false); 
				anim.SetBool (Celebrates, false);
				anim.SetBool (Jump, false);
				anim.SetBool (FoxJump, false);
				anim.SetBool (KoalaClimb, false);
				anim.SetBool (KoalaClimbReachTop, false);
				anim.SetBool (KoalaJumpToClimb, false);
				anim.SetBool (Collect, false);
				anim.SetBool (FallSitted, false); 
		    }
		} else if (Input.GetKeyDown(KeyCode.E)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {
				anim.SetBool (Walk, false); 
				anim.SetBool (Idle, false);                                      
				anim.SetBool (Run, false);
				anim.SetBool (BadgerPawAttack, false);
				anim.SetBool (BalloonFishDive, false);  
				anim.SetBool (BalloonFishFloat, false);  
				anim.SetBool (BalloonFishSwim, false); 
				anim.SetBool (Celebrates, true);
				anim.SetBool (Jump, false);
				anim.SetBool (FoxJump, false);
				anim.SetBool (KoalaClimb, false);
				anim.SetBool (KoalaClimbReachTop, false);
				anim.SetBool (KoalaJumpToClimb, false);
				anim.SetBool (Collect, false);
				anim.SetBool (FallSitted, false);  
			}
		} else if (Input.GetKeyUp(KeyCode.E)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Celebrates")) {            //E to Celebrate
				anim.SetBool (Walk, false); 
				anim.SetBool (Idle, true);                                      
				anim.SetBool (Run, false);
				anim.SetBool (BadgerPawAttack, false);
				anim.SetBool (BalloonFishDive, false);  
				anim.SetBool (BalloonFishFloat, false);  
				anim.SetBool (BalloonFishSwim, false); 
				anim.SetBool (Celebrates, false);
				anim.SetBool (Jump, false);
				anim.SetBool (FoxJump, false);
				anim.SetBool (KoalaClimb, false);
				anim.SetBool (KoalaClimbReachTop, false);
				anim.SetBool (KoalaJumpToClimb, false);
				anim.SetBool (Collect, false);
				anim.SetBool (FallSitted, false); ; 
			}
		} else if (Input.GetKeyDown(KeyCode.R)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {
				anim.SetBool (Walk, false); 
				anim.SetBool (Idle, false);                                      
				anim.SetBool (Run, true);
				anim.SetBool (BadgerPawAttack, false);
				anim.SetBool (BalloonFishDive, false);  
				anim.SetBool (BalloonFishFloat, false);  
				anim.SetBool (BalloonFishSwim, false); 
				anim.SetBool (Celebrates, false);
				anim.SetBool (Jump, false);
				anim.SetBool (FoxJump, false);
				anim.SetBool (KoalaClimb, false);
				anim.SetBool (KoalaClimbReachTop, false);
				anim.SetBool (KoalaJumpToClimb, false);
				anim.SetBool (Collect, false);
				anim.SetBool (FallSitted, false); 
			}
		} else if (Input.GetKeyUp(KeyCode.R)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Run")) {                //R to run
				anim.SetBool (Walk, false); 
				anim.SetBool (Idle, true);                                      
				anim.SetBool (Run, false);
				anim.SetBool (BadgerPawAttack, false);
				anim.SetBool (BalloonFishDive, false);  
				anim.SetBool (BalloonFishFloat, false);  
				anim.SetBool (BalloonFishSwim, false); 
				anim.SetBool (Celebrates, false);
				anim.SetBool (Jump, false);
				anim.SetBool (FoxJump, false);
				anim.SetBool (KoalaClimb, false);
				anim.SetBool (KoalaClimbReachTop, false);
				anim.SetBool (KoalaJumpToClimb, false);
				anim.SetBool (Collect, false);
				anim.SetBool (FallSitted, false); 
			}
		} else if (Input.GetKeyDown(KeyCode.T)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {
				anim.SetBool (Walk, false); 
				anim.SetBool (Idle, false);                                      
				anim.SetBool (Run, false);
				anim.SetBool (BadgerPawAttack, true);
				anim.SetBool (BalloonFishDive, false);  
				anim.SetBool (BalloonFishFloat, false);  
				anim.SetBool (BalloonFishSwim, false); 
				anim.SetBool (Celebrates, false);
				anim.SetBool (Jump, false);
				anim.SetBool (FoxJump, false);
				anim.SetBool (KoalaClimb, false);
				anim.SetBool (KoalaClimbReachTop, false);
				anim.SetBool (KoalaJumpToClimb, false);
				anim.SetBool (Collect, false);
				anim.SetBool (FallSitted, false); 
			}
		} else if (Input.GetKeyUp(KeyCode.T)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("BadgerPawAttack")) {              //T to attack
				anim.SetBool (Walk, false); 
				anim.SetBool (Idle, true);                                      
				anim.SetBool (Run, false);
				anim.SetBool (BadgerPawAttack, false);
				anim.SetBool (BalloonFishDive, false);  
				anim.SetBool (BalloonFishFloat, false);  
				anim.SetBool (BalloonFishSwim, false); 
				anim.SetBool (Celebrates, false);
				anim.SetBool (Jump, false);
				anim.SetBool (FoxJump, false);
				anim.SetBool (KoalaClimb, false);
				anim.SetBool (KoalaClimbReachTop, false);
				anim.SetBool (KoalaJumpToClimb, false);
				anim.SetBool (Collect, false);
				anim.SetBool (FallSitted, false); 
			}
		} else if (Input.GetKeyDown(KeyCode.Y)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {
				anim.SetBool (Walk, false); 
				anim.SetBool (Idle, false);                                      
				anim.SetBool (Run, false);
				anim.SetBool (BadgerPawAttack, false);
				anim.SetBool (BalloonFishDive, true);  
				anim.SetBool (BalloonFishFloat, false);  
				anim.SetBool (BalloonFishSwim, false); 
				anim.SetBool (Celebrates, false);
				anim.SetBool (Jump, false);
				anim.SetBool (FoxJump, false);
				anim.SetBool (KoalaClimb, false);
				anim.SetBool (KoalaClimbReachTop, false);
				anim.SetBool (KoalaJumpToClimb, false);
				anim.SetBool (Collect, false);
				anim.SetBool (FallSitted, false); 
			}
		} else if (Input.GetKeyUp(KeyCode.Y)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("BalloonFishDive")) {          //Y to dive
				anim.SetBool (Walk, false); 
				anim.SetBool (Idle, true);                                      
				anim.SetBool (Run, false);
				anim.SetBool (BadgerPawAttack, false);
				anim.SetBool (BalloonFishDive, false);  
				anim.SetBool (BalloonFishFloat, false);  
				anim.SetBool (BalloonFishSwim, false); 
				anim.SetBool (Celebrates, false);
				anim.SetBool (Jump, false);
				anim.SetBool (FoxJump, false);
				anim.SetBool (KoalaClimb, false);
				anim.SetBool (KoalaClimbReachTop, false);
				anim.SetBool (KoalaJumpToClimb, false);
				anim.SetBool (Collect, false);
				anim.SetBool (FallSitted, false); ; 
			}
		} else if (Input.GetKeyDown(KeyCode.U)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {                        
				anim.SetBool (Walk, false); 
				anim.SetBool (Idle, false);                                      
				anim.SetBool (Run, false);
				anim.SetBool (BadgerPawAttack, false);
				anim.SetBool (BalloonFishDive, false);  
				anim.SetBool (BalloonFishFloat, true);  
				anim.SetBool (BalloonFishSwim, false); 
				anim.SetBool (Celebrates, false);
				anim.SetBool (Jump, false);
				anim.SetBool (FoxJump, false);
				anim.SetBool (KoalaClimb, false);
				anim.SetBool (KoalaClimbReachTop, false);
				anim.SetBool (KoalaJumpToClimb, false);
				anim.SetBool (Collect, false);
				anim.SetBool (FallSitted, false); ; 
			}
		} else if (Input.GetKeyUp(KeyCode.U)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("BalloonFishFloat")) {                        //U to float swim
				anim.SetBool (Walk, false); 
				anim.SetBool (Idle, true);                                      
				anim.SetBool (Run, false);
				anim.SetBool (BadgerPawAttack, false);
				anim.SetBool (BalloonFishDive, false);  
				anim.SetBool (BalloonFishFloat, false);  
				anim.SetBool (BalloonFishSwim, false); 
				anim.SetBool (Celebrates, false);
				anim.SetBool (Jump, false);
				anim.SetBool (FoxJump, false);
				anim.SetBool (KoalaClimb, false);
				anim.SetBool (KoalaClimbReachTop, false);
				anim.SetBool (KoalaJumpToClimb, false);
				anim.SetBool (Collect, false);
				anim.SetBool (FallSitted, false); ;  
		}
		} else if (Input.GetKeyDown(KeyCode.I)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {                        
				anim.SetBool (Walk, false); 
				anim.SetBool (Idle, false);                                      
				anim.SetBool (Run, false);
				anim.SetBool (BadgerPawAttack, false);
				anim.SetBool (BalloonFishDive, false);  
				anim.SetBool (BalloonFishFloat, false);  
				anim.SetBool (BalloonFishSwim, true); 
				anim.SetBool (Celebrates, false);
				anim.SetBool (Jump, false);
				anim.SetBool (FoxJump, false);
				anim.SetBool (KoalaClimb, false);
				anim.SetBool (KoalaClimbReachTop, false);
				anim.SetBool (KoalaJumpToClimb, false);
				anim.SetBool (Collect, false);
				anim.SetBool (FallSitted, false); ; 
			}
		} else if (Input.GetKeyUp(KeyCode.I)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("BalloonFishSwim")) {                        //I to swim
				anim.SetBool (Walk, false); 
				anim.SetBool (Idle, true);                                      
				anim.SetBool (Run, false);
				anim.SetBool (BadgerPawAttack, false);
				anim.SetBool (BalloonFishDive, false);  
				anim.SetBool (BalloonFishFloat, false);  
				anim.SetBool (BalloonFishSwim, false); 
				anim.SetBool (Celebrates, false);
				anim.SetBool (Jump, false);
				anim.SetBool (FoxJump, false);
				anim.SetBool (KoalaClimb, false);
				anim.SetBool (KoalaClimbReachTop, false);
				anim.SetBool (KoalaJumpToClimb, false);
				anim.SetBool (Collect, false);
				anim.SetBool (FallSitted, false); ; 
			}
		} else if (Input.GetKeyDown(KeyCode.O)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {                        
				anim.SetBool (Walk, false); 
				anim.SetBool (Idle, false);                                      
				anim.SetBool (Run, false);
				anim.SetBool (BadgerPawAttack, false);
				anim.SetBool (BalloonFishDive, false);  
				anim.SetBool (BalloonFishFloat, false);  
				anim.SetBool (BalloonFishSwim, false); 
				anim.SetBool (Celebrates, false);
				anim.SetBool (Jump, false);
				anim.SetBool (FoxJump, true);
				anim.SetBool (KoalaClimb, false);
				anim.SetBool (KoalaClimbReachTop, false);
				anim.SetBool (KoalaJumpToClimb, false);
				anim.SetBool (Collect, false);
				anim.SetBool (FallSitted, false); ; 
			}
		} else if (Input.GetKeyUp(KeyCode.O)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("FoxJump")) {                        //O to fox jump
				anim.SetBool (Walk, false); 
				anim.SetBool (Idle, true);                                      
				anim.SetBool (Run, false);
				anim.SetBool (BadgerPawAttack, false);
				anim.SetBool (BalloonFishDive, false);  
				anim.SetBool (BalloonFishFloat, false);  
				anim.SetBool (BalloonFishSwim, false); 
				anim.SetBool (Celebrates, false);
				anim.SetBool (Jump, false);
				anim.SetBool (FoxJump, false);
				anim.SetBool (KoalaClimb, false);
				anim.SetBool (KoalaClimbReachTop, false);
				anim.SetBool (KoalaJumpToClimb, false);
				anim.SetBool (Collect, false);
				anim.SetBool (FallSitted, false); ; 
			}
		} else if (Input.GetKeyDown(KeyCode.P)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {                        
				anim.SetBool (Walk, false); 
				anim.SetBool (Idle, false);                                      
				anim.SetBool (Run, false);
				anim.SetBool (BadgerPawAttack, false);
				anim.SetBool (BalloonFishDive, false);  
				anim.SetBool (BalloonFishFloat, false);  
				anim.SetBool (BalloonFishSwim, false); 
				anim.SetBool (Celebrates, false);
				anim.SetBool (Jump, false);
				anim.SetBool (FoxJump, false);
				anim.SetBool (KoalaClimb, true);
				anim.SetBool (KoalaClimbReachTop, false);
				anim.SetBool (KoalaJumpToClimb, false);
				anim.SetBool (Collect, false);
				anim.SetBool (FallSitted, false); ; 
			}
		} else if (Input.GetKeyUp(KeyCode.P)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("KoalaClimb")) {                        //P to climb
				anim.SetBool (Walk, false); 
				anim.SetBool (Idle, true);                                      
				anim.SetBool (Run, false);
				anim.SetBool (BadgerPawAttack, false);
				anim.SetBool (BalloonFishDive, false);  
				anim.SetBool (BalloonFishFloat, false);  
				anim.SetBool (BalloonFishSwim, false); 
				anim.SetBool (Celebrates, false);
				anim.SetBool (Jump, false);
				anim.SetBool (FoxJump, false);
				anim.SetBool (KoalaClimb, false);
				anim.SetBool (KoalaClimbReachTop, false);
				anim.SetBool (KoalaJumpToClimb, false);
				anim.SetBool (Collect, false);
				anim.SetBool (FallSitted, false); ; 
			}
		} else if (Input.GetKeyDown(KeyCode.A)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {                        
				anim.SetBool (Walk, false); 
				anim.SetBool (Idle, false);                                      
				anim.SetBool (Run, false);
				anim.SetBool (BadgerPawAttack, false);
				anim.SetBool (BalloonFishDive, false);  
				anim.SetBool (BalloonFishFloat, false);  
				anim.SetBool (BalloonFishSwim, false); 
				anim.SetBool (Celebrates, false);
				anim.SetBool (Jump, false);
				anim.SetBool (FoxJump, false);
				anim.SetBool (KoalaClimb, false);
				anim.SetBool (KoalaClimbReachTop, true);
				anim.SetBool (KoalaJumpToClimb, false);
				anim.SetBool (Collect, false);
				anim.SetBool (FallSitted, false); ; 
			}
		} else if (Input.GetKeyUp(KeyCode.A)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("KoalaClimbReachTop")) {                        //A to reach top
				anim.SetBool (Walk, false); 
				anim.SetBool (Idle, true);                                      
				anim.SetBool (Run, false);
				anim.SetBool (BadgerPawAttack, false);
				anim.SetBool (BalloonFishDive, false);  
				anim.SetBool (BalloonFishFloat, false);  
				anim.SetBool (BalloonFishSwim, false); 
				anim.SetBool (Celebrates, false);
				anim.SetBool (Jump, false);
				anim.SetBool (FoxJump, false);
				anim.SetBool (KoalaClimb, false);
				anim.SetBool (KoalaClimbReachTop, false);
				anim.SetBool (KoalaJumpToClimb, false);
				anim.SetBool (Collect, false);
				anim.SetBool (FallSitted, false); ; 
			}
		} else if (Input.GetKeyDown(KeyCode.S)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {                        
				anim.SetBool (Walk, false); 
				anim.SetBool (Idle, false);                                      
				anim.SetBool (Run, false);
				anim.SetBool (BadgerPawAttack, false);
				anim.SetBool (BalloonFishDive, false);  
				anim.SetBool (BalloonFishFloat, false);  
				anim.SetBool (BalloonFishSwim, false); 
				anim.SetBool (Celebrates, false);
				anim.SetBool (Jump, false);
				anim.SetBool (FoxJump, false);
				anim.SetBool (KoalaClimb, false);
				anim.SetBool (KoalaClimbReachTop, false);
				anim.SetBool (KoalaJumpToClimb, true);
				anim.SetBool (Collect, false);
				anim.SetBool (FallSitted, false); ; 
			}
		} else if (Input.GetKeyUp(KeyCode.S)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("KoalaJumpToClimb")) {                        //S to jump to climb
				anim.SetBool (Walk, false); 
				anim.SetBool (Idle, true);                                      
				anim.SetBool (Run, false);
				anim.SetBool (BadgerPawAttack, false);
				anim.SetBool (BalloonFishDive, false);  
				anim.SetBool (BalloonFishFloat, false);  
				anim.SetBool (BalloonFishSwim, false); 
				anim.SetBool (Celebrates, false);
				anim.SetBool (Jump, false);
				anim.SetBool (FoxJump, false);
				anim.SetBool (KoalaClimb, false);
				anim.SetBool (KoalaClimbReachTop, false);
				anim.SetBool (KoalaJumpToClimb, false);
				anim.SetBool (Collect, false);
				anim.SetBool (FallSitted, false); ; 
			}
		} else if (Input.GetKeyDown(KeyCode.D)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {                        
				anim.SetBool (Walk, false); 
				anim.SetBool (Idle, false);                                      
				anim.SetBool (Run, false);
				anim.SetBool (BadgerPawAttack, false);
				anim.SetBool (BalloonFishDive, false);  
				anim.SetBool (BalloonFishFloat, false);  
				anim.SetBool (BalloonFishSwim, false); 
				anim.SetBool (Celebrates, false);
				anim.SetBool (Jump, false);
				anim.SetBool (FoxJump, false);
				anim.SetBool (KoalaClimb, false);
				anim.SetBool (KoalaClimbReachTop, false);
				anim.SetBool (KoalaJumpToClimb, false);
				anim.SetBool (Collect, true);
				anim.SetBool (FallSitted, false); ; 
			}
		} else if (Input.GetKeyUp(KeyCode.D)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Collect")) {                        //D to collect
				anim.SetBool (Walk, false); 
				anim.SetBool (Idle, true);                                      
				anim.SetBool (Run, false);
				anim.SetBool (BadgerPawAttack, false);
				anim.SetBool (BalloonFishDive, false);  
				anim.SetBool (BalloonFishFloat, false);  
				anim.SetBool (BalloonFishSwim, false); 
				anim.SetBool (Celebrates, false);
				anim.SetBool (Jump, false);
				anim.SetBool (FoxJump, false);
				anim.SetBool (KoalaClimb, false);
				anim.SetBool (KoalaClimbReachTop, false);
				anim.SetBool (KoalaJumpToClimb, false);
				anim.SetBool (Collect, false);
				anim.SetBool (FallSitted, false); ; 
			}
		} else if (Input.GetKeyDown(KeyCode.F)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {                        
				anim.SetBool (Walk, false); 
				anim.SetBool (Idle, false);                                      
				anim.SetBool (Run, false);
				anim.SetBool (BadgerPawAttack, false);
				anim.SetBool (BalloonFishDive, false);  
				anim.SetBool (BalloonFishFloat, false);  
				anim.SetBool (BalloonFishSwim, false); 
				anim.SetBool (Celebrates, false);
				anim.SetBool (Jump, false);
				anim.SetBool (FoxJump, false);
				anim.SetBool (KoalaClimb, false);
				anim.SetBool (KoalaClimbReachTop, false);
				anim.SetBool (KoalaJumpToClimb, false);
				anim.SetBool (Collect, false);
				anim.SetBool (FallSitted, true); ; 
			}
		} else if (Input.GetKeyUp(KeyCode.F)) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("FallSitted")) {                        //F to fall
				anim.SetBool (Walk, false); 
				anim.SetBool (Idle, true);                                      
				anim.SetBool (Run, false);
				anim.SetBool (BadgerPawAttack, false);
				anim.SetBool (BalloonFishDive, false);  
				anim.SetBool (BalloonFishFloat, false);  
				anim.SetBool (BalloonFishSwim, false); 
				anim.SetBool (Celebrates, false);
				anim.SetBool (Jump, false);
				anim.SetBool (FoxJump, false);
				anim.SetBool (KoalaClimb, false);
				anim.SetBool (KoalaClimbReachTop, false);
				anim.SetBool (KoalaJumpToClimb, false);
				anim.SetBool (Collect, false);
				anim.SetBool (FallSitted, false); ; 
			}
				
	     } 
	}
}