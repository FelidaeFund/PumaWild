using UnityEngine;
using System.Collections;

/// MovementControls
/// Handles user input and the movement of the puma

public class InputControls : MonoBehaviour
{
	//===================================
	//===================================
	//		MODULE VARIABLES
	//===================================
	//===================================

	// possible states for each direction of movement
	enum NavState {Off, Inc, Full, Dec};
	
	// four main directions
	private NavState navStateLeft;
	private NavState navStateRight;
	private NavState navStateForward;
	private NavState navStateBack;
	private float navValLeft;
	private float navValRight;
	private float navValForward;
	private float navValBack;

	// four diagonal directions (FOR SMOOTHING; NOT YET IMPLEMENTED)
	private NavState navStateForwardLeft;
	private NavState navStateForwardRight;
	private NavState navStateBackLeft;
	private NavState navStateBackRight;
	private float navValForwardLeft;
	private float navValForwardRight;
	private float navValBackLeft;
	private float navValBackRight;

	// used in resolving interactions between forward and back
	private bool forwardKey;
	private bool backKey;

	// main variables for current movement status
	private float inputVert;
	private float inputHorz;

	// used for passing back data from subroutine
	private NavState newNavState;
	private float newNavVal;
	
	// onscreen locations for control boxes
	private Rect rectForward;
	private Rect rectBack;
	private Rect rectDiagLeft;
	private Rect rectDiagRight;
	private Rect rectTurnLeft;
	private Rect rectTurnRight;
	
	// external modules
	private LevelManager levelManager;

	//===================================
	//===================================
	//		INITIALIZATION
	//===================================
	//===================================

    void Start()
    {
		// connect to external modules
		levelManager = GetComponent<LevelManager>();
		
		rectForward = new Rect (0f, 0f, 0f, 0f);
		rectBack = new Rect (0f, 0f, 0f, 0f);
		rectDiagLeft = new Rect (0f, 0f, 0f, 0f);
		rectDiagRight = new Rect (0f, 0f, 0f, 0f);
		rectTurnLeft = new Rect (0f, 0f, 0f, 0f);
		rectTurnRight = new Rect (0f, 0f, 0f, 0f);		

		ResetControls();	
	}

	public void ResetControls()
	{
		navStateForward = NavState.Off;
		navStateBack = NavState.Off;
		navStateLeft = NavState.Off;
		navStateRight = NavState.Off;
		navValForward = 0;
		navValBack = 0;
		navValLeft = 0;
		navValRight = 0;

		navStateForwardLeft = NavState.Off;
		navStateForwardRight = NavState.Off;
		navStateBackLeft = NavState.Off;
		navStateBackRight = NavState.Off;
		navValForwardLeft = 0;
		navValForwardRight = 0;
		navValBackLeft = 0;
		navValBackRight = 0;
		
		forwardKey = false;
		backKey = false;
		
		inputVert = 0f;
		inputHorz = 0f;
	}

	//===================================
	//===================================
	//		CONTROL PROCESSING
	//===================================
	//===================================

	//--------------------------------------------
	// ProcessControls()
	// 
	// This is the main entry point for input
	// processing, called at the beginning of
	// the main Update() function in LevelManager
	//--------------------------------------------

	public void ProcessControls(string gameState)
	{
		// initialize key states to 'off'
		bool keyStateForward = false;
		bool keyStateBack = false;
		bool keyStateDiagLeft = false;
		bool keyStateDiagRight = false;
		bool keyStateTurnLeft = false;
		bool keyStateTurnRight = false;

		forwardKey = true;

		// check for pressed mouse within any of the onscreen rects
		if (Input.GetMouseButton(0)) {
			float mouseX = Input.mousePosition.x;
			float mouseY = Screen.height - Input.mousePosition.y;	
			
			if (mouseX >= rectForward.xMin && mouseX <= rectForward.xMax && mouseY >= rectForward.yMin && mouseY <= rectForward.yMax) {
				keyStateForward = true;
			}
			if (mouseX >= rectBack.x && mouseX <= rectBack.x+rectBack.width && mouseY >= rectBack.y && mouseY <= rectBack.y+rectBack.height) {
				keyStateBack = true;
			}
			if (mouseX >= rectDiagLeft.x && mouseX <= rectDiagLeft.x+rectDiagLeft.width && mouseY >= rectDiagLeft.y && mouseY <= rectDiagLeft.y+rectDiagLeft.height) {
				keyStateDiagLeft = true;
			}
			if (mouseX >= rectDiagRight.x && mouseX <= rectDiagRight.x+rectDiagRight.width && mouseY >= rectDiagRight.y && mouseY <= rectDiagRight.y+rectDiagRight.height) {
				keyStateDiagRight = true;
			}
			if (mouseX >= rectTurnLeft.x && mouseX <= rectTurnLeft.x+rectTurnLeft.width && mouseY >= rectTurnLeft.y && mouseY <= rectTurnLeft.y+rectTurnLeft.height) {
				keyStateTurnLeft = true;
			}
			if (mouseX >= rectTurnRight.x && mouseX <= rectTurnRight.x+rectTurnRight.width && mouseY >= rectTurnRight.y && mouseY <= rectTurnRight.y+rectTurnRight.height) {
				keyStateTurnRight = true;
			}
		}
	
		// check for relevant keys pressed on the physical keyboard
		if (Input.GetKey(KeyCode.I) || Input.GetKey(KeyCode.UpArrow))
			keyStateForward = true;
		if (Input.GetKey(KeyCode.K) || Input.GetKey(KeyCode.DownArrow))
			keyStateBack = true;
		if (Input.GetKey(KeyCode.U))
			keyStateDiagLeft = true;
		if (Input.GetKey(KeyCode.O))
			keyStateDiagRight = true;
		if (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.LeftArrow))
			keyStateTurnLeft = true;
		if (Input.GetKey(KeyCode.L) || Input.GetKey(KeyCode.RightArrow))
			keyStateTurnRight = true;
			
		// set the heading to either straight ahead or diagonal
	
		if (keyStateForward == true)
			levelManager.pumaHeadingOffset = 0f;

		if (keyStateDiagLeft == true) {
			levelManager.pumaHeadingOffset = -60f;
			keyStateForward = true;
		}
		else if (keyStateDiagRight == true) {
			levelManager.pumaHeadingOffset = 60f;
			keyStateForward = true;
		}
		
		// deal with interactions between forward and back keys
	
		if (inputVert == 0) {
			if (forwardKey == false)
				forwardKey = keyStateForward;
			if (forwardKey == false)
				backKey = keyStateBack;	
		}
		else if (inputVert > 0) {
			if (forwardKey == false)
				forwardKey = keyStateForward;
			else if (keyStateBack == true)
				forwardKey = false;
		}
		else {
			if (backKey == false)
				backKey = keyStateBack;
			else if (keyStateForward == true)
				backKey = false;
		}
		
		// now do main input processing
	
		UpdateNavChannel(navStateForward, navValForward, forwardKey, Time.deltaTime * 3, Time.deltaTime * 3);
		navStateForward = newNavState;
		navValForward = newNavVal;

		UpdateNavChannel(navStateBack, -navValBack * 2, backKey, Time.deltaTime * 3, Time.deltaTime * 3);
		navStateBack = newNavState;
		navValBack = -newNavVal / 2;

		UpdateNavChannel(navStateLeft, -navValLeft, keyStateTurnLeft, Time.deltaTime * ((gameState == "gameStateStalking") ? 3f : 4.4f), Time.deltaTime * 3);
		navStateLeft = newNavState;
		navValLeft = -newNavVal;

		UpdateNavChannel(navStateRight, navValRight, keyStateTurnRight, Time.deltaTime * ((gameState == "gameStateStalking") ? 3f : 4.4f), Time.deltaTime * 3);
		navStateRight = newNavState;
		navValRight = newNavVal;
		
		//inputVert = navValForward;				 // disable backward motion
		inputVert = navValForward + navValBack;		 // enable backward motion
		inputHorz = navValRight + navValLeft;
		
		if (inputVert == 0) {
			// reset heading when puma stopped
			levelManager.pumaHeadingOffset = 0;
		}
	}

	private void UpdateNavChannel(NavState previousNavState, float previousNavVal, bool keyPressed, float incStep, float decStep)
	{
		newNavState = previousNavState;
		newNavVal = previousNavVal;
	
		switch (previousNavState) {

		case NavState.Off:
			if (keyPressed) {
				newNavState = NavState.Inc;
				newNavVal = previousNavVal + incStep;
				if (newNavVal >= 1f) {
					newNavState = NavState.Full;
					newNavVal = 1f;
				}
			}
			else {
				newNavVal = 0f;
			}
			break;

		case NavState.Inc:
			if (keyPressed) {
				newNavVal = previousNavVal + incStep;
				if (newNavVal >= 1f) {
					newNavState = NavState.Full;
					newNavVal = 1f;
				}
			}
			else {
				newNavState = NavState.Dec;
				newNavVal = previousNavVal - decStep;
				if (newNavVal <= 0f) {
					newNavState = NavState.Off;
					newNavVal = 0f;
				}
			}
			break;
			
		case NavState.Full:
			if (keyPressed) {
				newNavVal = 1f;
			}
			else {
				newNavState = NavState.Dec;
				newNavVal = previousNavVal - decStep;
				if (newNavVal <= 0f) {
					newNavState = NavState.Off;
					newNavVal = 0f;
				}
			}
			break;
			
		case NavState.Dec:
			if (keyPressed) {
				newNavState = NavState.Inc;
				newNavVal = previousNavVal + incStep;
				if (newNavVal >= 1f) {
					newNavState = NavState.Full;
					newNavVal = 1f;
				}
			}
			else {
				newNavVal = previousNavVal - decStep;
				if (newNavVal <= 0f) {
					newNavState = NavState.Off;
					newNavVal = 0f;
				}
			}
			break;
		}
	}

	//===================================
	//===================================
	//		READ CURRENT STATE
	//===================================
	//===================================

	public float GetInputVert()
	{
		return inputVert;
	}
	
	public float GetInputHorz()
	{
		return inputHorz;
	}
	
	//===================================
	//===================================
	//		SET ONSCREEN BOXES
	//===================================
	//===================================

	public void SetRectForward(Rect rect)
	{
		rectForward = rect;	
	}
	
	public void SetRectBack(Rect rect)
	{
		rectBack = rect;	
	}
	
	public void SetRectDiagLeft(Rect rect)
	{
		rectDiagLeft = rect;	
	}
	
	public void SetRectDiagRight(Rect rect)
	{
		rectDiagRight = rect;	
	}
	
	public void SetRectTurnLeft(Rect rect)
	{
		rectTurnLeft = rect;	
	}
	
	public void SetRectTurnRight(Rect rect)
	{
		rectTurnRight = rect;	
	}
}

















