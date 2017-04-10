using UnityEngine;
using System.Collections;

public class DemoUI : MonoBehaviour {

	bool cursorlock = true;

	// Use this for initialization
	void Start () {
		
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
	
		/*
		//Uncomment for Unity 5 to get rid of the warnings.
		if (cursorlock)
			Cursor.lockState = CursorLockMode.Locked;
		else
			Cursor.lockState = CursorLockMode.None;
		*/
		
		//Delete this statement for Unity 5.
		if (cursorlock)
			Screen.lockCursor = true;
		else
			Screen.lockCursor = false;		
		
		
		if (Input.GetKeyDown(KeyCode.Escape))
			cursorlock = !cursorlock;
		
		if (Input.GetKeyDown(KeyCode.C)) {
			CustomPointer.instance.pointer_returns_to_center =  !CustomPointer.instance.pointer_returns_to_center;
			
		}
		
		if (Input.GetKeyDown(KeyCode.L)) {
			CustomPointer.instance.center_lock =  !CustomPointer.instance.center_lock;
			
		}		
		
	}
}
