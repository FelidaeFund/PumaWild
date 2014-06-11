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
	private NavState navStateLeft = NavState.Off;
	private NavState navStateRight = NavState.Off;
	private NavState navStateForward = NavState.Off;
	private NavState navStateBack = NavState.Off;
	private float navValLeft = 0;
	private float navValRight = 0;
	private float navValForward = 0;
	private float navValBack = 0;

	// four diagonal directions
	private NavState navStateForwardLeft = NavState.Off;
	private NavState navStateForwardRight = NavState.Off;
	private NavState navStateBackLeft = NavState.Off;
	private NavState navStateBackRight = NavState.Off;
	private float navValForwardLeft = 0;
	private float navValForwardRight = 0;
	private float navValBackLeft = 0;
	private float navValBackRight = 0;
	
	private bool leftKey = false;
	private bool rightKey = false;
	private bool forwardKey = false;
	private bool backKey = false;

	private NavState newNavState;
	private float newNavVal;
	
	public bool forwardClicked = false;
	public bool backClicked = false;
	public bool sideLeftClicked = false;
	public bool sideRightClicked = false;
	
	public bool leftArrowMouseEvent = false;
	public bool rightArrowMouseEvent = false;

	private float inputVert = 0f;
	private float inputHorz = 0f;

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
		bool leftKeyState = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.J) || leftArrowMouseEvent == true;
		bool rightKeyState = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.L) || rightArrowMouseEvent == true;
		bool forwardKeyState = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.I) || forwardClicked == true;
		bool backKeyState = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.K) || backClicked == true;
		bool sideLeftState = Input.GetKey(KeyCode.U) || sideLeftClicked == true;
		bool sideRightState = Input.GetKey(KeyCode.O) || sideRightClicked == true;
		
		forwardClicked = backClicked = sideLeftClicked = sideRightClicked = false;
	
		if (forwardKeyState == true)
			levelManager.pumaHeadingOffset = 0f;

		if (sideLeftState == true) {
			levelManager.pumaHeadingOffset = -60f;
			forwardKeyState = true;
		}
		else if (sideRightState == true) {
			levelManager.pumaHeadingOffset = 60f;
			forwardKeyState = true;
		}
	
		leftKey = leftKeyState;
		rightKey = rightKeyState;

		if (inputVert == 0) {
			if (forwardKey == false)
				forwardKey = forwardKeyState;
			if (forwardKey == false)
				backKey = backKeyState;	
		}
		else if (inputVert > 0) {
			if (forwardKey == false)
				forwardKey = forwardKeyState;
			else if (backKeyState == true)
				forwardKey = false;
		}
		else {
			if (backKey == false)
				backKey = backKeyState;
			else if (forwardKeyState == true)
				backKey = false;
		}

		
		// basic input processing
	
		UpdateNavChannel(navStateForward, navValForward, forwardKey, Time.deltaTime * 3, Time.deltaTime * 3);
		navStateForward = newNavState;
		navValForward = newNavVal;

		UpdateNavChannel(navStateBack, -navValBack * 2, backKey, Time.deltaTime * 3, Time.deltaTime * 3);
		navStateBack = newNavState;
		navValBack = -newNavVal / 2;

		UpdateNavChannel(navStateLeft, -navValLeft, leftKey, Time.deltaTime * ((gameState == "gameStateStalking") ? 3f : 4.4f), Time.deltaTime * 3);
		navStateLeft = newNavState;
		navValLeft = -newNavVal;

		UpdateNavChannel(navStateRight, navValRight, rightKey, Time.deltaTime * ((gameState == "gameStateStalking") ? 3f : 4.4f), Time.deltaTime * 3);
		navStateRight = newNavState;
		navValRight = newNavVal;
		
		//inputVert = navValForward; // + navValBack;	 // disable down motion
		inputVert = navValForward + navValBack;		 // enable down motion
		inputHorz = navValRight + navValLeft;
		
		if (inputVert == 0)
			levelManager.pumaHeadingOffset = 0;
		
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

	public void ResetControls()
	{
		navStateLeft = NavState.Off;
		navStateRight = NavState.Off;
		navStateForward = NavState.Off;
		navStateBack = NavState.Off;

		navStateForwardLeft = NavState.Off;
		navStateForwardRight = NavState.Off;
		navStateBackLeft = NavState.Off;
		navStateBackRight = NavState.Off;
		
		navValLeft = 0;
		navValRight = 0;
		navValForward = 0;
		navValBack = 0;

		navValForwardLeft = 0;
		navValForwardRight = 0;
		navValBackLeft = 0;
		navValBackRight = 0;
		
		leftKey = false;
		rightKey = false;
		forwardKey = false;
		backKey = false;
		
		forwardClicked = false;
		backClicked = false;
		sideLeftClicked = false;
		sideRightClicked = false;
		
		leftArrowMouseEvent = false;
		rightArrowMouseEvent = false;

		inputVert = 0f;
		inputHorz = 0f;
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
	
	
}

















