using UnityEngine;
using System.Collections;

/// GuiManager
/// Main handling of all user interface elements

public class GuiManager : MonoBehaviour 
{
	// DEBUGGING OPTIONS
	private bool displayFrameRate = false;
	private bool skipStraightToLevel = false;
	private int  skipStraightToLevelFrameCount = 0;

	//===================================
	//===================================
	//		MODULE VARIABLES
	//===================================
	//===================================

	// PUMA CHARACTERISTICS
	private float[] speedArray = new float[] {0.85f, 0.75f, 0.55f, 0.45f, 0.25f, 0.15f};
	private float[] stealthArray = new float[] {0.15f, 0.25f, 0.45f, 0.55f, 0.75f, 0.85f};
	private float[] enduranceArray = new float[] {0.85f, 0.75f, 0.55f, 0.45f, 0.25f, 0.15f};
	private float[] powerArray = new float[] {0.15f, 0.25f, 0.45f, 0.55f, 0.75f, 0.85f};

	public string guiState;
	private float guiStateStartTime;
	private float guiStateDuration;
	private float guiFadePercentComplete;
	
	public GUISkin customGUISkin;
	private float guiOpacity = 1f;
	
	private bool infoPanelVisible = false;
	private int infoPanelPage = 0;
	private float infoPanelTransStart;
	private float infoPanelTransTime;
	private bool infoPanelIntroFlag;

	public Texture2D logoTexture; 
	public Texture2D backgroundTexture; 
	public Texture2D pumaIconTexture; 
	public Texture2D pumaIconShadowTexture; 
	public Texture2D pumaIconShadowYellowTexture; 
	public Texture2D pumaIconHighlightTexture; 
	public Texture2D buckHeadTexture; 
	public Texture2D doeHeadTexture; 
	public Texture2D fawnHeadTexture; 
	public Texture2D hunterTexture; 
	public Texture2D vehicleTexture; 
	public Texture2D forestTexture; 
	
	public Texture2D buttonTexture; 
	public Texture2D buttonHoverTexture; 
	public Texture2D buttonDownTexture; 
	public Texture2D swapButtonTexture; 
	public Texture2D swapButtonHoverTexture; 
	public Texture2D radioButtonTexture; 
	public Texture2D radioSelectTexture; 
	public Texture2D sliderBarTexture; 
	public Texture2D sliderThumbTexture; 
	public Texture2D greenCheckTexture; 
	public Texture2D pumaCrossbonesTexture; 
	public Texture2D pumaCrossbonesRedTexture; 
	public Texture2D pumaCrossbonesDarkRedTexture; 
	public Texture2D greenHeartTexture; 

	public Texture2D headshot1Texture; 
	public Texture2D headshot2Texture; 
	public Texture2D headshot3Texture; 
	public Texture2D headshot4Texture; 
	public Texture2D headshot5Texture; 
	public Texture2D headshot6Texture; 

	public Texture2D closeup1Texture; 
	public Texture2D closeup2Texture; 
	public Texture2D closeup3Texture; 
	public Texture2D closeup4Texture; 
	public Texture2D closeup5Texture; 
	public Texture2D closeup6Texture; 
	public Texture2D closeupBackgroundTexture; 

	public Texture2D arrowTrayTexture; 
	public Texture2D arrowTrayTopTexture; 
	public Texture2D arrowLeftTexture; 
	public Texture2D arrowRightTexture; 
	public Texture2D arrowUpTexture; 
	public Texture2D arrowDownTexture; 
	public Texture2D arrowLeftOnTexture; 
	public Texture2D arrowRightOnTexture; 
	public Texture2D arrowUpOnTexture; 
	public Texture2D arrowDownOnTexture; 
	public Texture2D arrowTurnLeftTexture; 
	public Texture2D arrowTurnRightTexture; 

	public Texture2D indicatorBuck; 
	public Texture2D indicatorDoe; 
	public Texture2D indicatorFawn; 
	public Texture2D indicatorBkgnd; 

	public Texture2D iconFacebookTexture; 
	public Texture2D iconTwitterTexture; 
	public Texture2D iconGoogleTexture; 
	public Texture2D iconPinterestTexture; 
	public Texture2D iconYouTubeTexture; 
	public Texture2D iconLinkedInTexture; 

	public int selectedPuma = -1;			// TEMP: !!! should not be public; need to consolidate in either LevelManager or GUIManager, with function call to get it
	
	private bool spacePressed = false;
	private bool leftShiftPressed = false;
	private bool rightShiftPressed = false;
	private bool leftArrowPressed = false;
	private bool rightArrowPressed = false;
	
	private bool debounceSpace = false;
	private bool debounceLeftShift = false;
	private bool debounceRightShift = false;
	private bool debounceLeftArrow = false;
	private bool debounceRightArrow = false;
	
	private Rect overlayRect;
	private Texture2D rectTexture;				// TEMP: remove when code using this is gone	
	private GUIStyle rectStyle;					// TEMP: remove when code using this is gone	
	private GUIStyle buttonStyle;				// TEMP: remove when code using this is gone	
	private GUIStyle buttonDownStyle;			// TEMP: remove when code using this is gone	
	private GUIStyle buttonDisabledStyle;		// TEMP: remove when code using this is gone	
	private GUIStyle bigButtonStyle;			// TEMP: remove when code using this is gone	
	private GUIStyle bigButtonDisabledStyle;	// TEMP: remove when code using this is gone	
	private GUIStyle swapButtonStyle;			// TEMP: remove when code using this is gone	
	private GUIStyle buttonSimpleStyle;			// TEMP: remove when code using this is gone	
	private GUISkin customSkin;					// TEMP: remove when code using this is gone	
	private GUIStyle sliderBarStyle;			// TEMP: remove when code using this is gone	
	private GUIStyle sliderThumbStyle;			// TEMP: remove when code using this is gone	

	// EXTERNAL MODULES
	private GuiComponents guiComponents;		// TEMP: may not need this when this file has been fully pruned
	private LevelManager levelManager;
	private ScoringSystem scoringSystem;
	private InputControls inputControls;
	private OverlayPanel overlayPanel;	
	private FeedingDisplay feedingDisplay;
	private GameplayDisplay gameplayDisplay;	

	//===================================
	//===================================
	//		INITIALIZATION
	//===================================
	//===================================

	void Start() 
	{	
		// connect to external modules
		guiComponents = GetComponent<GuiComponents>();
		levelManager = GetComponent<LevelManager>();
		scoringSystem = GetComponent<ScoringSystem>();
		inputControls = GetComponent<InputControls>();
		overlayPanel = GetComponent<OverlayPanel>();
		feedingDisplay = GetComponent<FeedingDisplay>();
		gameplayDisplay = GetComponent<GameplayDisplay>();
		
		// initialize state
		SetGuiState("guiStateStartApp1");
		infoPanelVisible = true;
		infoPanelTransStart = Time.time - 100000;
		infoPanelTransTime = 0.3f;
		infoPanelIntroFlag = true;
		
		// basic rect
		rectTexture = new Texture2D(2,2);
		rectStyle = new GUIStyle();


		customGUISkin.button.normal.textColor = new Color(0.99f, 0.75f, 0.21f, 1f);
		customGUISkin.button.hover.textColor = new Color(0.99f, 0.75f, 0.21f, 1f);


		customGUISkin.button.normal.textColor = new Color(1f, 1f, 1f, 1f);
		customGUISkin.button.hover.textColor = new Color(1f, 1f, 1f, 1f);

		customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);
		customGUISkin.button.hover.textColor = new Color(0.99f, 0.75f, 0.21f, 1f);

		// custom button
		buttonStyle = new GUIStyle();
		buttonStyle.normal.textColor = Color.white;
		//buttonStyle.normal.background = buttonTexture;
		buttonStyle.hover.textColor = Color.white;
		//buttonStyle.hover.background = buttonHoverTexture;
		buttonStyle.alignment = TextAnchor.MiddleCenter;

		buttonDownStyle = new GUIStyle();
		buttonDownStyle.normal.textColor = new Color(0.99f, 0.88f, 0.6f, 1f);
		//buttonDownStyle.normal.background = buttonDownTexture;
		buttonDownStyle.hover.textColor = new Color(0.99f, 0.88f, 0.6f, 1f);
		//buttonDownStyle.hover.background = buttonDownTexture;
		buttonDownStyle.alignment = TextAnchor.MiddleCenter;

		buttonDisabledStyle = new GUIStyle();
		buttonDisabledStyle.normal.textColor = new Color(0.3f, 0.3f, 0.3f, 1f);
		//buttonDisabledStyle.normal.background = buttonTexture;
		buttonDisabledStyle.hover.textColor = new Color(0.3f, 0.3f, 0.3f, 1f);
		//buttonDisabledStyle.hover.background = buttonTexture;
		buttonDisabledStyle.alignment = TextAnchor.MiddleCenter;

		swapButtonStyle = new GUIStyle();
		swapButtonStyle.normal.textColor = Color.white;
		swapButtonStyle.normal.background = swapButtonTexture;
		swapButtonStyle.hover.textColor = Color.white;
		swapButtonStyle.hover.background = swapButtonHoverTexture;
		swapButtonStyle.alignment = TextAnchor.MiddleCenter;

		buttonSimpleStyle = new GUIStyle();
		buttonSimpleStyle.normal.textColor = Color.white;
		buttonSimpleStyle.hover.textColor = Color.white;
		buttonSimpleStyle.alignment = TextAnchor.MiddleCenter;

		buttonStyle.normal.textColor = new Color(0.99f, 0.7f, 0.2f, 1f);
		buttonStyle.hover.textColor = new Color(0.99f, 0.8f, 0.4f, 1f);

		bigButtonStyle = new GUIStyle();
		bigButtonStyle.normal.textColor = Color.white;
		bigButtonStyle.hover.textColor = Color.white;
		bigButtonStyle.alignment = TextAnchor.MiddleCenter;

		bigButtonDisabledStyle = new GUIStyle();
		bigButtonDisabledStyle.normal.textColor = new Color(0.4f, 0.4f, 0.4f, 1f);
		bigButtonDisabledStyle.hover.textColor = new Color(0.4f, 0.4f, 0.4f, 1f);
		bigButtonDisabledStyle.alignment = TextAnchor.MiddleCenter;
		
		bigButtonStyle.normal.textColor = new Color(0.99f, 0.7f, 0.2f, 1f);
		bigButtonStyle.hover.textColor = new Color(0.99f, 0.8f, 0.4f, 1f);
		
		// custom slider
		sliderBarStyle = new GUIStyle();
		sliderThumbStyle = new GUIStyle();
		sliderThumbStyle.normal.background = sliderThumbTexture;
		sliderThumbStyle.padding = new RectOffset(10,10,10,10);
		customSkin = (GUISkin)ScriptableObject.CreateInstance("GUISkin");
		customSkin.horizontalSlider = sliderBarStyle;
		customSkin.horizontalSliderThumb = sliderThumbStyle;	
	}
	
	public void SetGuiState(string newState) 
	{	
		guiState = newState;
		guiStateStartTime = Time.time;
		Update();
		Debug.Log("NEW GUI STATE: " + newState);
	}

	
	//======================================
	//
	//	Update() - SET PARAMS PRIOR TO DRAW
	//
	//	This function is where any params 
	//	get changed prior to the draw code
	//	being called; all the param changes 
	//	must happen here because the draw
	//	code gets called twice (layout+render),
	//	and therefore must not change params
	//
	//======================================
	
	void Update() 
	{
		// clean handling for modifier keys
		DebounceKeyboardInput();

		// debug option (enabled at top of file)
		if (skipStraightToLevel == true) {
			SetGuiState("guiStateStartApp1");
			infoPanelVisible = false;
			if (++skipStraightToLevelFrameCount == 10) {
				SetGuiState("guiStateGameplay");
				levelManager.SetGameState("gameStateLeavingGui");
				skipStraightToLevel = false;
			}
		}
		
		// detect caught condition
		if (levelManager.IsCaughtState() == true && guiState != "guiStateFeeding1" && guiState != "guiStateFeeding2") {
			SetGuiState("guiStateFeeding1");
		}
		
		//======================================
		// GUI STATE MACHINE
		//
		// Processing for GUI state machine
		//======================================
		
		switch (guiState) {
		
		//---------------------
		// StartApp States
		//
		// game launch
		//---------------------

		case "guiStateStartApp1":
			break;

		case "guiStateStartApp2":
			// fade-out of popup panel
			guiStateDuration = 1.1f;
			if (Time.time > guiStateStartTime + guiStateDuration) {
				infoPanelTransTime = 0.3f;
				infoPanelIntroFlag = false;
				SetGuiState("guiStateEnteringOverlay");
			}
			break;

		//-----------------------
		// Overlay States
		//
		// entering, viewing and
		// leaving the main GUI
		//-----------------------

		case "guiStateEnteringOverlay":
			// fade-in of overlay panel
			guiStateDuration = 1.6f;
			FadeInOpacityLogarithmic();		
			CheckForKeyboardEscapeFromOverlay();
			CheckForKeyboardSelectionOfPuma();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateOverlay");
			break;

		case "guiStateOverlay":
			// ongoing overlay state
			CheckForKeyboardEscapeFromOverlay();
			CheckForKeyboardSelectionOfPuma();
			break;

		case "guiStateLeavingOverlay":
			// fade-out of overlay panel
			guiStateDuration = 1f;
			FadeOutOpacityLogarithmic();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateEnteringGameplay1");
			break;
			
		//-----------------------
		// Gameplay States
		//
		// entering, viewing and
		// leaving the 3D world
		//-----------------------

		case "guiStateEnteringGameplay1":
			// no GUI during initial camera zoom
			guiStateDuration = 1.9f;
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateEnteringGameplay2");
			break;
			
		case "guiStateEnteringGameplay2":
			// fade-in of movement controls
			guiStateDuration = 1.8f;
			FadeInOpacityLogarithmic();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateEnteringGameplay3");
			break;
			
		case "guiStateEnteringGameplay3":
			// fade-in of position indicators
			guiStateDuration = 0.7f;
			FadeInOpacityLogarithmic();
			CheckForKeyboardEscapeFromGameplay();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateEnteringGameplay4");
			break;
			
		case "guiStateEnteringGameplay4":
			// brief pause
			guiStateDuration = 0.2f;
			CheckForKeyboardEscapeFromGameplay();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateEnteringGameplay5");
			break;
			
		case "guiStateEnteringGameplay5":
			// fade-in of status displays
			guiStateDuration = 1.2f;
			FadeInOpacityLinear();
			CheckForKeyboardEscapeFromGameplay();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateGameplay");
			break;
			
		case "guiStateGameplay":
			// ongoing game-play state
			CheckForKeyboardEscapeFromGameplay();
			break;
			
		case "guiStateLeavingGameplay":
			// fade-out of game-play controls
			guiStateDuration = 0.7f;
			FadeOutOpacityLinear();
			CheckForKeyboardSelectionOfPuma();
			if (Time.time > guiStateStartTime + guiStateDuration) {
				SetGuiState("guiStateEnteringOverlay");
				if (overlayPanel.GetCurrentScreen() == 3) {
					// return to select screen rather than quit screen
					overlayPanel.SetCurrentScreen(0);
				}
				if (scoringSystem.GetPumaHealth(selectedPuma) <= 0f) {
					// puma has died
					selectedPuma = -1;
					overlayPanel.SetCurrentScreen(0);
				}
			}
			break;
		
		//------------------------------
		// Feeding States
		//
		// entering, viewing and
		// leaving the feeding display
		//------------------------------

		case "guiStateFeeding1":
			// fade-out of game-play controls
			guiStateDuration = 1f;
			FadeOutOpacityLinear();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateFeeding2");
			break;

		case "guiStateFeeding2":
			// pause during attack on deer
			guiStateDuration = 2f;
			if (Time.time - guiStateStartTime > guiStateDuration)
				SetGuiState("guiStateFeeding3");
			break;

		case "guiStateFeeding3":
			// fade-in of feeding display main panel
			guiStateDuration = 1f;
			FadeInOpacityLinear();
			CheckForKeyboardEscapeFromFeeding();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateFeeding4");
			break;

		case "guiStateFeeding4":
			// brief pause
			guiStateDuration = 1f;
			CheckForKeyboardEscapeFromFeeding();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateFeeding5");
			break;

		case "guiStateFeeding5":
			// fade-in of feeding display 'ok' button
			guiStateDuration = 1f;
			FadeInOpacityLinear();
			CheckForKeyboardEscapeFromFeeding();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateFeeding6");
			break;

		case "guiStateFeeding6":
			// ongoing view of feeding display
			CheckForKeyboardEscapeFromFeeding();
			break;

		case "guiStateFeeding7":
			// fade-out of feeding display
			guiStateDuration = 2f;
			FadeOutOpacityLinear();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateFeeding8");
			break;

		case "guiStateFeeding8":
			// fade-in of movement controls
			guiStateDuration = 0.9f;
			FadeInOpacityLinear();
			CheckForKeyboardEscapeFromGameplay();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateFeeding9");
			break;

		case "guiStateFeeding9":
			// fade-in of position indicators
			guiStateDuration = 1f;
			FadeInOpacityLogarithmic();
			CheckForKeyboardEscapeFromGameplay();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateFeeding10");
			break;
			
		case "guiStateFeeding10":
			// brief pause
			guiStateDuration = 0.1f;
			CheckForKeyboardEscapeFromGameplay();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateFeeding11");
			break;
			
		case "guiStateFeeding11":
			// fade-in of status indicators
			guiStateDuration = 0.7f;
			FadeInOpacityLinear();
			CheckForKeyboardEscapeFromGameplay();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateGameplay");
			break;
			
		//------------------
		// Error Check
		//------------------
		
		default:
			Debug.Log("ERROR - GUIManager.Update() got bad state: " + guiState);
			break;
		}
	}


	//------------------------------
	//
	//	Utilities used by Update()
	//
	//------------------------------

	private void CheckForKeyboardEscapeFromGameplay()
	{
		if (spacePressed || leftShiftPressed || rightShiftPressed) {
			// use keyboard to leave gameplay
			SetGuiState("guiStateLeavingGameplay");
			levelManager.SetGameState("gameStateLeavingGameplay");
		}	
	}
	
	private void CheckForKeyboardEscapeFromOverlay()
	{
		if (selectedPuma >= 0 && (spacePressed || leftShiftPressed || rightShiftPressed)) {
			// use keyboard to leave overlay
			SetGuiState("guiStateLeavingOverlay");
			levelManager.SetGameState("gameStateLeavingGui");
		}	
	}
	
	private void CheckForKeyboardEscapeFromFeeding()
	{
		if (spacePressed || leftShiftPressed || rightShiftPressed) {
			// use keyboard to resume gameplay
			SetGuiState("guiStateFeeding7");
			levelManager.SetGameState("gameStateCaught5");
		}
	}

	private void CheckForKeyboardSelectionOfPuma()
	{
		if (overlayPanel.GetCurrentScreen() == 0 || overlayPanel.GetCurrentScreen() == 2) {
			// we are in 'select' or 'stats' screen
			if (leftArrowPressed)
				DecrementPuma();
			if (rightArrowPressed)
				IncrementPuma();
		}
	}
	
	private void FadeInOpacityLogarithmic()
	{
		// logarithmic curve 
		if (Time.time - guiStateStartTime < (guiStateDuration * 0.5f)) {
			guiFadePercentComplete = (Time.time - guiStateStartTime) / (guiStateDuration * 0.5f);
			guiFadePercentComplete = guiFadePercentComplete * guiFadePercentComplete;
			guiOpacity = guiFadePercentComplete * 0.5f;
		}
		else if (Time.time - guiStateStartTime < guiStateDuration) {
			guiFadePercentComplete = ((Time.time - guiStateStartTime) - (guiStateDuration * 0.5f)) / (guiStateDuration * 0.5f);				
			guiFadePercentComplete = guiFadePercentComplete + (guiFadePercentComplete - (guiFadePercentComplete * guiFadePercentComplete));
			guiOpacity = 0.5f + guiFadePercentComplete * 0.5f;
		}
	}

	private void FadeInOpacityLinear()
	{
		// linear curve
		guiFadePercentComplete = (Time.time - guiStateStartTime) / guiStateDuration;
		guiOpacity = guiFadePercentComplete;
	}

	private void FadeOutOpacityLogarithmic()
	{
		// logarithmic curve
		if (Time.time - guiStateStartTime < (guiStateDuration * 0.5f)) {
			guiFadePercentComplete = (Time.time - guiStateStartTime) / (guiStateDuration * 0.5f);
			guiFadePercentComplete = guiFadePercentComplete * guiFadePercentComplete;
			guiOpacity =  1f - (guiFadePercentComplete * 0.5f);
		}
		else if (Time.time - guiStateStartTime < guiStateDuration) {
			guiFadePercentComplete = ((Time.time - guiStateStartTime) - (guiStateDuration * 0.5f)) / (guiStateDuration * 0.5f);				
			guiFadePercentComplete = guiFadePercentComplete + (guiFadePercentComplete - (guiFadePercentComplete * guiFadePercentComplete));
			guiOpacity =  1f - (0.5f + guiFadePercentComplete * 0.5f);
		}
	}

	private void FadeOutOpacityLinear()
	{
		// linear curve
		guiFadePercentComplete = (Time.time - guiStateStartTime) / guiStateDuration;
		guiOpacity =  1f - guiFadePercentComplete;
	}

	
	
	//======================================
	//
	//	OnGUI() - DRAW THE USER INTERFACE
	//
	//	This function is the top level
	//	draw routine for rendering the UI;
	//	it's called twice: once to do the
	//	layout, and once to draw the UI
	//
	//======================================
	
	void OnGUI()
	{	
		CalculateOverlayRect();

		switch (guiState) {
	
		//-----------------------
		// Overlay States
		//
		// entering, viewing and
		// leaving the main GUI
		//-----------------------

		case "guiStateEnteringOverlay":
		case "guiStateOverlay":
		case "guiStateLeavingOverlay":
			if (infoPanelVisible == false || Time.time - infoPanelTransStart < infoPanelTransTime * 0.5f)
				overlayPanel.Draw(guiOpacity);
			break;
						
		//-----------------------
		// Gameplay States
		//
		// entering, viewing and
		// leaving the 3D world
		//-----------------------

		case "guiStateEnteringGameplay1":
			// no GUI during initial camera zoom
			break;
			
		case "guiStateEnteringGameplay2":
			// fade-in of movement controls
			if (infoPanelVisible == false || Time.time - infoPanelTransStart < infoPanelTransTime * 0.5f)
				gameplayDisplay.Draw(guiOpacity, 0f, 0f);
			break;
			
		case "guiStateEnteringGameplay3":
			// fade-in of position indicators
			if (infoPanelVisible == false || Time.time - infoPanelTransStart < infoPanelTransTime * 0.5f)
				gameplayDisplay.Draw(1f, guiOpacity, 0f);
			break;
			
		case "guiStateEnteringGameplay4":
			// brief pause
			if (infoPanelVisible == false || Time.time - infoPanelTransStart < infoPanelTransTime * 0.5f)
				gameplayDisplay.Draw(1f, 1f, 0f);
			break;
			
		case "guiStateEnteringGameplay5":
			// fade-in of status displays
			if (infoPanelVisible == false || Time.time - infoPanelTransStart < infoPanelTransTime * 0.5f)
				gameplayDisplay.Draw(1f, 1f, guiOpacity);
			break;
			
		case "guiStateGameplay":
			// ongoing game-play state
			if (infoPanelVisible == false || Time.time - infoPanelTransStart < infoPanelTransTime * 0.5f)
				gameplayDisplay.Draw(1f, 1f, 1f);
			break;
			
		case "guiStateLeavingGameplay":
			// fade-out of game-play controls
			if (infoPanelVisible == false || Time.time - infoPanelTransStart < infoPanelTransTime * 0.5f)
				gameplayDisplay.Draw(guiOpacity, guiOpacity, guiOpacity);
			break;
		
		//------------------------------
		// Feeding States
		//
		// entering, viewing and
		// leaving the feeding display
		//------------------------------

		case "guiStateFeeding1":
			// fade-out of game-play controls
			if (infoPanelVisible == false || Time.time - infoPanelTransStart < infoPanelTransTime * 0.5f)
				gameplayDisplay.Draw(guiOpacity, guiOpacity, guiOpacity);
			break;

		case "guiStateFeeding2":
			// no GUI display during attack on deer
			break;

		case "guiStateFeeding3":
			// fade-in of feeding display main panel
			if (infoPanelVisible == false || Time.time - infoPanelTransStart < infoPanelTransTime * 0.5f)
				feedingDisplay.Draw(guiOpacity, 0f);
			break;

		case "guiStateFeeding4":
			// brief pause
			if (infoPanelVisible == false || Time.time - infoPanelTransStart < infoPanelTransTime * 0.5f)
				feedingDisplay.Draw(1f, 0f);
			break;

		case "guiStateFeeding5":
			// fade-in of feeding display 'ok' button
			if (infoPanelVisible == false || Time.time - infoPanelTransStart < infoPanelTransTime * 0.5f)
				feedingDisplay.Draw(1f, guiOpacity);
			break;

		case "guiStateFeeding6":
			// ongoing view of feeding display
			if (infoPanelVisible == false || Time.time - infoPanelTransStart < infoPanelTransTime * 0.5f)
				feedingDisplay.Draw(1f, 1f);
			break;

		case "guiStateFeeding7":
			// fade-out of feeding display
			if (infoPanelVisible == false || Time.time - infoPanelTransStart < infoPanelTransTime * 0.5f)
				feedingDisplay.Draw(guiOpacity, guiOpacity);
			break;

		case "guiStateFeeding8":
			// fade-in of movement controls
			if (infoPanelVisible == false || Time.time - infoPanelTransStart < infoPanelTransTime * 0.5f)
				gameplayDisplay.Draw(guiOpacity, 0f, 0f);
			break;
			
		case "guiStateFeeding9":
			// fade-in of position indicators
			if (infoPanelVisible == false || Time.time - infoPanelTransStart < infoPanelTransTime * 0.5f)
				gameplayDisplay.Draw(1f, guiOpacity, 0f);
			break;
			
		case "guiStateFeeding10":
			// brief pause
			if (infoPanelVisible == false || Time.time - infoPanelTransStart < infoPanelTransTime * 0.5f)
				gameplayDisplay.Draw(1f, 1f, 0f);
			break;
			
		case "guiStateFeeding11":
			// fade-in of status indicators
			if (infoPanelVisible == false || Time.time - infoPanelTransStart < infoPanelTransTime * 0.5f)
				gameplayDisplay.Draw(1f, 1f, guiOpacity);
			break;
		}
		
		//------------------------------
		// Frame Rate Display
		//------------------------------

		if (displayFrameRate == true && levelManager != null) {
			GUI.color = Color.white;

			int msec = levelManager.frameAverageDuration;
			GUI.Box(new Rect(Screen.width * 0.24f - 80f, 0, 160, 24), "Avg Frame time: " + msec.ToString());		

			GUI.Box(new Rect(Screen.width * 0.50f - 80f, 0, 160, 24), "Screen Res: " + Screen.width.ToString() + "x" + Screen.height.ToString());				
			
			int averageFrameRate = (int)(1000 / levelManager.frameAverageDuration);
			GUI.Box(new Rect(Screen.width * 0.76f - 80f, 0, 160, 24), "Avg Frame rate: " + averageFrameRate.ToString());		

			GUI.Box(new Rect(Screen.width * 0.24f - 80f, Screen.height - 24, 160, 24), "displayVar1:  " + levelManager.displayVar1.ToString());		
			GUI.Box(new Rect(Screen.width * 0.50f - 80f, Screen.height - 24, 160, 24), "displayVar2:  " + levelManager.displayVar2.ToString());				
			GUI.Box(new Rect(Screen.width * 0.76f - 80f, Screen.height - 24, 160, 24), "displayVar3:  " + levelManager.displayVar3.ToString());		
		}
			
		//------------------------------
		// Draw Popup Panel
		//------------------------------
				
		float elapsedTime = Time.time - infoPanelTransStart;
		float percentVisible = 0f;
		
		if (infoPanelVisible == true && elapsedTime < infoPanelTransTime) {
			// sliding in
			percentVisible = elapsedTime / infoPanelTransTime;			
			DrawInfoPanel(percentVisible);
		}
		else if (infoPanelVisible == true) {
			// fully open
			DrawInfoPanel(1f);
		}
		else if (elapsedTime < infoPanelTransTime) {
			// sliding out		
			percentVisible = 1f - elapsedTime / infoPanelTransTime;
			DrawInfoPanel(percentVisible);
		}
	}	
 
 


	

	
	/////////////////////
	////// INFO PANEL
	/////////////////////
	

	void DrawInfoPanel(float infoPanelOpacity) 
	{ 
		GUIStyle style = new GUIStyle();

		float infoPanelX = Screen.width * 0.5f - Screen.height * 0.75f;
		float infoPanelWidth = Screen.height * 1.5f;
		float infoPanelY = Screen.height * 0.025f;
		float infoPanelHeight = Screen.height * 0.95f;

		float popupInnerRectX = infoPanelX + infoPanelWidth * 0.01f;
		float popupInnerRectY = infoPanelY + infoPanelWidth * 0.01f;
		float popupInnerRectWidth = infoPanelWidth - infoPanelWidth * 0.02f;
		float popupInnerRectHeight = infoPanelHeight - infoPanelWidth * 0.02f;

		//backdrop
		GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
		GUI.Box(new Rect(infoPanelX, infoPanelY, infoPanelWidth, infoPanelHeight), "");
		//DrawRect(new Rect(infoPanelX + infoPanelWidth * 0.01f, infoPanelY + infoPanelWidth * 0.01f, infoPanelWidth * 0.98f, infoPanelHeight - infoPanelWidth * 0.02f), new Color(0.22f, 0.21f, 0.20f, 1f));	
		DrawRect(new Rect(popupInnerRectX, popupInnerRectY, popupInnerRectWidth, popupInnerRectHeight * 0.07f), new Color(0.205f, 0.21f, 0.19f, 1f));	
		DrawRect(new Rect(popupInnerRectX, popupInnerRectY + popupInnerRectHeight * 0.11f, popupInnerRectWidth, popupInnerRectHeight - popupInnerRectHeight * 0.11f - popupInnerRectHeight * 0.09f), new Color(0.205f, 0.21f, 0.19f, 1f));	
				
				
		// main title & page contents
		
		float textureX;
		float textureY;
		float textureHeight;
		float textureWidth;
		
		string titleString = "not specified";
		switch (infoPanelPage) {

		case 0:
			titleString = "";
			// chapter title
			style.fontStyle = FontStyle.BoldAndItalic;
			style.alignment = TextAnchor.MiddleCenter;
			style.fontSize = (int)(popupInnerRectWidth * 0.024f);
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			style.normal.textColor = new Color(0.65f, 0.60f, 0.50f, 1f);
			GUI.Button(new Rect(popupInnerRectX, popupInnerRectY + popupInnerRectHeight * 0.13f, popupInnerRectWidth, popupInnerRectHeight * 0.093f), "Chapter 1: Natural Wilderness", style);
			// left side text
			popupInnerRectY -= popupInnerRectWidth * 0.01f;
			style.fontStyle = FontStyle.BoldAndItalic;
			style.alignment = TextAnchor.MiddleCenter;
			style.fontSize = (int)(popupInnerRectWidth * 0.023f);
			style.normal.textColor = new Color(0.99f, 0.66f, 0f, 1f);
			GUI.Button(new Rect(popupInnerRectX + popupInnerRectWidth * 0.015f, popupInnerRectY + popupInnerRectHeight * 0.24f, popupInnerRectWidth * 0.5f, popupInnerRectHeight * 0.093f), "A Regional Population of Pumas", style);
			style.fontStyle = FontStyle.BoldAndItalic;
			style.alignment = TextAnchor.MiddleCenter;
			style.fontSize = (int)(popupInnerRectWidth * 0.017f);
			style.normal.textColor = new Color(0.65f * 1.05f, 0.60f * 1.05f, 0.50f * 1.05f, 1f);
			GUI.Button(new Rect(popupInnerRectX + popupInnerRectWidth * 0.015f, popupInnerRectY + popupInnerRectHeight * 0.28f, popupInnerRectWidth * 0.5f, popupInnerRectHeight * 0.093f), "6 pumas with varying strengths and skills", style);
			// right side text
			style.fontStyle = FontStyle.BoldAndItalic;
			style.alignment = TextAnchor.MiddleCenter;
			style.fontSize = (int)(popupInnerRectWidth * 0.023f);
			style.normal.textColor = new Color(0.99f, 0.66f, 0f, 1f);
			GUI.Button(new Rect(popupInnerRectX + popupInnerRectWidth * 0.49f, popupInnerRectY + popupInnerRectHeight * 0.24f, popupInnerRectWidth * 0.5f, popupInnerRectHeight * 0.093f), "A Pristine Natural Environment", style);
			style.fontStyle = FontStyle.BoldAndItalic;
			style.alignment = TextAnchor.MiddleCenter;
			style.fontSize = (int)(popupInnerRectWidth * 0.017f);
			style.normal.textColor = new Color(0.65f * 1.05f, 0.60f * 1.05f, 0.50f * 1.05f, 1f);
			GUI.Button(new Rect(popupInnerRectX + popupInnerRectWidth * 0.49f, popupInnerRectY + popupInnerRectHeight * 0.28f, popupInnerRectWidth * 0.5f, popupInnerRectHeight * 0.093f), "Catch prey efficiently to survive and thrive", style);	
			// puma heads
			GUI.color = new Color(1f, 1f, 1f, 0.4f * infoPanelOpacity);
			textureX = popupInnerRectX + popupInnerRectWidth * 0.052f;
			textureY = popupInnerRectY + popupInnerRectHeight * 0.32f;
			textureHeight = popupInnerRectHeight * 0.55f;
			textureWidth = forestTexture.width * (textureHeight / forestTexture.height);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), forestTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			textureX = popupInnerRectX + popupInnerRectWidth * 0.08f;
			textureY = popupInnerRectY + popupInnerRectHeight * 0.38f;
			textureHeight = popupInnerRectHeight * 0.15f;
			textureWidth = closeup1Texture.width * (textureHeight / closeup1Texture.height);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), closeup1Texture);
			textureX = popupInnerRectX + popupInnerRectWidth * 0.08f;
			textureY = popupInnerRectY + popupInnerRectHeight * 0.56f;
			textureHeight = popupInnerRectHeight * 0.15f;
			textureWidth = closeup2Texture.width * (textureHeight / closeup2Texture.height);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), closeup2Texture);
			textureX = popupInnerRectX + popupInnerRectWidth * 0.20f;
			textureY = popupInnerRectY + popupInnerRectHeight * 0.38f;
			textureHeight = popupInnerRectHeight * 0.15f;
			textureWidth = closeup3Texture.width * (textureHeight / closeup3Texture.height);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), closeup3Texture);
			textureX = popupInnerRectX + popupInnerRectWidth * 0.20f;
			textureY = popupInnerRectY + popupInnerRectHeight * 0.56f;
			textureHeight = popupInnerRectHeight * 0.15f;
			textureWidth = closeup4Texture.width * (textureHeight / closeup4Texture.height);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), closeup4Texture);
			textureX = popupInnerRectX + popupInnerRectWidth * 0.32f;
			textureY = popupInnerRectY + popupInnerRectHeight * 0.38f;
			textureHeight = popupInnerRectHeight * 0.15f;
			textureWidth = closeup5Texture.width * (textureHeight / closeup5Texture.height);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), closeup5Texture);
			textureX = popupInnerRectX + popupInnerRectWidth * 0.32f;
			textureY = popupInnerRectY + popupInnerRectHeight * 0.56f;
			textureHeight = popupInnerRectHeight * 0.15f;
			textureWidth = closeup6Texture.width * (textureHeight / closeup6Texture.height);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), closeup6Texture);
			// deer heads
			GUI.color = new Color(1f, 1f, 1f, 0.4f * infoPanelOpacity);
			textureX = popupInnerRectX + popupInnerRectWidth * 0.53f;
			textureY = popupInnerRectY + popupInnerRectHeight * 0.32f;
			textureHeight = popupInnerRectHeight * 0.55f;
			textureWidth = forestTexture.width * (textureHeight / forestTexture.height);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), forestTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			textureX = popupInnerRectX + popupInnerRectWidth * 0.56f;
			textureY = popupInnerRectY + popupInnerRectHeight * 0.39f;
			textureHeight = popupInnerRectHeight * 0.28f;
			textureWidth = buckHeadTexture.width * (textureHeight / buckHeadTexture.height);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), buckHeadTexture);
			textureX = popupInnerRectX + popupInnerRectWidth * 0.70f;
			textureY = popupInnerRectY + popupInnerRectHeight * 0.35f;
			textureHeight = popupInnerRectHeight * 0.24f;
			textureWidth = doeHeadTexture.width * (textureHeight / doeHeadTexture.height);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), doeHeadTexture);
			textureX = popupInnerRectX + popupInnerRectWidth * 0.80f;
			textureY = popupInnerRectY + popupInnerRectHeight * 0.45f;
			textureHeight = popupInnerRectHeight * 0.22f;
			textureWidth = fawnHeadTexture.width * (textureHeight / fawnHeadTexture.height);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), fawnHeadTexture);
			popupInnerRectY += popupInnerRectWidth * 0.01f;
			// logo
			float xPos = popupInnerRectX;
			float yPos = popupInnerRectY + popupInnerRectHeight * -0.0225f;
			style.fontSize = (int)(overlayRect.width * 0.04f);
			style.fontStyle = FontStyle.BoldAndItalic;
			style.alignment = TextAnchor.MiddleCenter;
			style.normal.textColor = new Color(0.2f, 0f, 0f, 1f);
			GUI.Button(new Rect(xPos - overlayRect.width * 0.003f, yPos + overlayRect.height * 0.008f, popupInnerRectWidth, overlayRect.height * 0.10f), "PumaWild", style);
			GUI.Button(new Rect(xPos + overlayRect.width * 0.003f, yPos + overlayRect.height * 0.008f, popupInnerRectWidth, overlayRect.height * 0.10f), "PumaWild", style);
			GUI.Button(new Rect(xPos - overlayRect.width * 0.003f, yPos + overlayRect.height * 0.016f, popupInnerRectWidth, overlayRect.height * 0.10f), "PumaWild", style);
			GUI.Button(new Rect(xPos + overlayRect.width * 0.003f, yPos + overlayRect.height * 0.016f, popupInnerRectWidth, overlayRect.height * 0.10f), "PumaWild", style);
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			GUI.Button(new Rect(xPos, yPos + overlayRect.height * 0.012f, popupInnerRectWidth, overlayRect.height * 0.10f), "PumaWild", style);
			style.fontSize = (int)(overlayRect.width * 0.0245f);
			style.normal.textColor = new Color(0.2f, 0f, 0f, 1f);
			xPos = popupInnerRectX;
			yPos = popupInnerRectY + popupInnerRectHeight * 0.715f;
			//GUI.Button(new Rect(xPos - overlayRect.width * 0.001f, yPos + overlayRect.height * 0.084f, popupInnerRectWidth, overlayRect.height * 0.05f), "Survival is Not a Given...", style);
			//GUI.Button(new Rect(xPos + overlayRect.width * 0.001f, yPos + overlayRect.height * 0.084f, popupInnerRectWidth, overlayRect.height * 0.05f), "Survival is Not a Given...", style);
			//GUI.Button(new Rect(xPos - overlayRect.width * 0.001f, yPos + overlayRect.height * 0.088f, popupInnerRectWidth, overlayRect.height * 0.05f), "Survival is Not a Given...", style);
			//GUI.Button(new Rect(xPos + overlayRect.width * 0.001f, yPos + overlayRect.height * 0.088f, popupInnerRectWidth, overlayRect.height * 0.05f), "Survival is Not a Given...", style);
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			GUI.Button(new Rect(xPos, yPos + overlayRect.height * 0.086f, popupInnerRectWidth, overlayRect.height * 0.05f), "Survival is not a given...", style);
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			break;

		case 1:
			titleString = "Puma Biology";
			break;
			
		case 2:
			titleString = "Puma Ecology";
			break;
			
		case 3:
			titleString = "Catching Prey";
			break;
			
		case 4:
			titleString = "Survival Threats";
			break;
			
		case 5:
			titleString = "Help Pumas";
			break;
		}

		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.MiddleCenter;
		style.fontSize = (int)(popupInnerRectWidth * 0.03f);
		style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
		GUI.Button(new Rect(popupInnerRectX, popupInnerRectY, popupInnerRectWidth, popupInnerRectHeight * 0.07f), titleString, style);

		///////////////////
		// buttons
		///////////////////

		float buttonSideGap = popupInnerRectWidth * 0.005f;
		float buttonGap = popupInnerRectWidth * 0.02f;
		float buttonBorderWidth = popupInnerRectWidth * 0.005f;
		float buttonX = popupInnerRectX + buttonSideGap;
		float buttonY = popupInnerRectY + popupInnerRectHeight * 0.935f;
		float buttonWidth = (popupInnerRectWidth - buttonGap * 6 - buttonSideGap * 2) / 7;
		float buttonHeight = overlayRect.height * 0.06f;

		if (infoPanelIntroFlag == false) {
		
			// DRAW SELECT BUTTONS AT BOTTOM
		
			customGUISkin.button.fontSize = (int)(overlayRect.width * 0.0196);
		
			// introduction
			
			GUI.color = new Color(1f, 1f, 1f, 0.15f * infoPanelOpacity);
			if (infoPanelPage != 0)
				DrawRect(new Rect(buttonX - buttonBorderWidth, buttonY - buttonBorderWidth, buttonWidth + buttonBorderWidth * 2f, buttonHeight + buttonBorderWidth * 2f), new Color(1f, 1f, 1f, 1f));	
			GUI.skin = customGUISkin;
			customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
			if (infoPanelPage != 0)
				customGUISkin.button.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1f);
			else
				customGUISkin.button.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "")) {
				infoPanelPage = 0;
			}
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "Overview")) {
				infoPanelPage = 0;
			}
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);

			// puma biology
			
			buttonX += buttonWidth + buttonGap;
			
			GUI.color = new Color(1f, 1f, 1f, 0.15f * infoPanelOpacity);
			if (infoPanelPage != 1)
				DrawRect(new Rect(buttonX - buttonBorderWidth, buttonY - buttonBorderWidth, buttonWidth + buttonBorderWidth * 2f, buttonHeight + buttonBorderWidth * 2f), new Color(1f, 1f, 1f, 1f));	
			GUI.skin = customGUISkin;
			customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
			if (infoPanelPage != 1)
				customGUISkin.button.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1f);
			else
				customGUISkin.button.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "")) {
				infoPanelPage = 1;
			}
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "Biology")) {
				infoPanelPage = 1;
			}
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);

			// puma ecology
			
			buttonX += buttonWidth + buttonGap;
			
			GUI.color = new Color(1f, 1f, 1f, 0.15f * infoPanelOpacity);
			if (infoPanelPage != 2)
				DrawRect(new Rect(buttonX - buttonBorderWidth, buttonY - buttonBorderWidth, buttonWidth + buttonBorderWidth * 2f, buttonHeight + buttonBorderWidth * 2f), new Color(1f, 1f, 1f, 1f));	
			GUI.skin = customGUISkin;
			customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
			if (infoPanelPage != 2)
				customGUISkin.button.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1f);
			else
				customGUISkin.button.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "")) {
				infoPanelPage = 2;
			}
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "Ecology")) {
				infoPanelPage = 2;
			}
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);

			// catching prey
			
			buttonX += buttonWidth + buttonGap;
			
			GUI.color = new Color(1f, 1f, 1f, 0.15f * infoPanelOpacity);
			if (infoPanelPage != 3)
				DrawRect(new Rect(buttonX - buttonBorderWidth, buttonY - buttonBorderWidth, buttonWidth + buttonBorderWidth * 2f, buttonHeight + buttonBorderWidth * 2f), new Color(1f, 1f, 1f, 1f));	
			GUI.skin = customGUISkin;
			customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
			if (infoPanelPage != 3)
				customGUISkin.button.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1f);
			else
				customGUISkin.button.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "")) {
				infoPanelPage = 3;
			}
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "Predation")) {
				infoPanelPage = 3;
			}
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);

			// survival threats
			
			buttonX += buttonWidth + buttonGap;
			
			GUI.color = new Color(1f, 1f, 1f, 0.15f * infoPanelOpacity);
			if (infoPanelPage != 4)
				DrawRect(new Rect(buttonX - buttonBorderWidth, buttonY - buttonBorderWidth, buttonWidth + buttonBorderWidth * 2f, buttonHeight + buttonBorderWidth * 2f), new Color(1f, 1f, 1f, 1f));	
			GUI.skin = customGUISkin;
			customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
			if (infoPanelPage != 4)
				customGUISkin.button.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1f);
			else
				customGUISkin.button.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "")) {
				infoPanelPage = 4;
			}
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "Mortality")) {
				infoPanelPage = 4;
			}
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);

			// help pumas
			
			buttonX += buttonWidth + buttonGap;
			
			GUI.color = new Color(1f, 1f, 1f, 0.15f * infoPanelOpacity);
			if (infoPanelPage != 5)
				DrawRect(new Rect(buttonX - buttonBorderWidth, buttonY - buttonBorderWidth, buttonWidth + buttonBorderWidth * 2f, buttonHeight + buttonBorderWidth * 2f), new Color(1f, 1f, 1f, 1f));	
			GUI.skin = customGUISkin;
			customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
			if (infoPanelPage != 5)
				customGUISkin.button.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1f);
			else
				customGUISkin.button.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "")) {
				infoPanelPage = 5;
			}
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "How to Help")) {
				infoPanelPage = 5;
			}
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);
			
			// DONE
			
			buttonX += buttonWidth + buttonGap;
			
			GUI.color = new Color(1f, 1f, 1f, 0.15f * infoPanelOpacity);
			if (infoPanelPage != 6)
				DrawRect(new Rect(buttonX - buttonBorderWidth, buttonY - buttonBorderWidth, buttonWidth + buttonBorderWidth * 2f, buttonHeight + buttonBorderWidth * 2f), new Color(1f, 1f, 1f, 1f));	
			GUI.skin = customGUISkin;
			customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
			if (infoPanelPage != 6)
				customGUISkin.button.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1f);
			else
				customGUISkin.button.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "")) {
				infoPanelVisible = false;
				infoPanelTransStart = Time.time;
			}
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "CLOSE   X")) {
				infoPanelVisible = false;
				infoPanelTransStart = Time.time;
			}
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);
			
		}
		else {
		
			// DRAW 'OK' BUTTON FOR WELCOME SCREEN
			
			buttonY -= buttonHeight * 0.15f;
			buttonHeight *= 1.3f;

			buttonWidth *= 1.2f;
			buttonX = popupInnerRectX + popupInnerRectWidth * 0.5f - buttonWidth * 0.5f;

			GUI.color = new Color(1f, 1f, 1f, 0.15f * infoPanelOpacity);
			if (infoPanelPage != 0)
				DrawRect(new Rect(buttonX - buttonBorderWidth, buttonY - buttonBorderWidth, buttonWidth + buttonBorderWidth * 2f, buttonHeight + buttonBorderWidth * 2f), new Color(1f, 1f, 1f, 1f));	
			GUI.skin = customGUISkin;
			customGUISkin.button.fontSize = (int)(overlayRect.width * 0.026);
			customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "")) {
				infoPanelVisible = false;
				infoPanelTransTime = 0.6f;
				infoPanelTransStart = Time.time;
				SetGuiState("guiStateStartApp2");
			}
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "GO")) {
				infoPanelVisible = false;
				infoPanelTransTime = 0.6f;
				infoPanelTransStart = Time.time;
				SetGuiState("guiStateStartApp2");
			}
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
		}
		
		
		if (infoPanelIntroFlag == true || infoPanelPage == 0) {
		
		
		
		}
		else if (infoPanelPage != 5) {
		
			// vertical divider
			//DrawRect(new Rect(popupInnerRectX + popupInnerRectWidth * 0.4965f, popupInnerRectY + popupInnerRectHeight * 0.19f, popupInnerRectWidth * 0.0035f, popupInnerRectHeight * 0.69f), new Color(0.31f, 0.305f, 0.30f, 1f));	
			//DrawRect(new Rect(popupInnerRectX + popupInnerRectWidth * 0.500f, popupInnerRectY + popupInnerRectHeight * 0.19f, popupInnerRectWidth * 0.005f, popupInnerRectHeight * 0.69f), new Color(0.13f, 0.125f, 0.12f, 1f));	
			
			// left and right titles

			style.fontStyle = FontStyle.BoldAndItalic;
			style.alignment = TextAnchor.MiddleCenter;
			style.fontSize = (int)(popupInnerRectWidth * 0.021f);
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			GUI.Button(new Rect(popupInnerRectX, popupInnerRectY + popupInnerRectHeight * 0.06f, popupInnerRectWidth * 0.4f, popupInnerRectHeight * 0.06f), "In the Game", style);
			GUI.Button(new Rect(popupInnerRectX + popupInnerRectWidth * 0.5f, popupInnerRectY + popupInnerRectHeight * 0.06f, popupInnerRectWidth * 0.6f, popupInnerRectHeight * 0.06f), "the Real World", style);
		}
		else {
			// Help Pumas
		
			// title
			style.fontStyle = FontStyle.BoldAndItalic;
			style.alignment = TextAnchor.MiddleCenter;
			style.fontSize = (int)(popupInnerRectWidth * 0.024f);
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			GUI.Button(new Rect(popupInnerRectX, popupInnerRectY + popupInnerRectHeight * 0.17f, popupInnerRectWidth * 1f, popupInnerRectHeight * 0.093f), "Pumas in the Real World need your help", style);

			style.fontStyle = FontStyle.BoldAndItalic;
			style.alignment = TextAnchor.MiddleCenter;
			style.fontSize = (int)(popupInnerRectWidth * 0.018f);
			style.normal.textColor = new Color(0.6f, 0.6f, 0.6f, 1f);
			GUI.Button(new Rect(popupInnerRectX + popupInnerRectWidth * 0.4f, popupInnerRectY + popupInnerRectHeight * 0.25f, popupInnerRectWidth * 0.2f, popupInnerRectHeight * 0.06f), "Help support our work to study and protect pumas and their habitats", style);

			// donate button
			customGUISkin.button.fontSize = (int)(overlayRect.width * 0.032);
			customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			customGUISkin.button.normal.textColor = new Color(0.8f, 0.1f, 0f, 1f);
			Color defaultHoverColor = customGUISkin.button.hover.textColor;
			customGUISkin.button.hover.textColor = new Color(0.9f, 0.12f, 0f, 1f);
			DrawRect(new Rect(popupInnerRectX + popupInnerRectWidth * 0.35f + popupInnerRectHeight * 0.01f, popupInnerRectY + popupInnerRectHeight * 0.423f + popupInnerRectHeight * 0.01f, popupInnerRectWidth * 0.3f - popupInnerRectHeight * 0.02f, popupInnerRectHeight * 0.15f - popupInnerRectHeight * 0.023f), new Color(1f, 1f, 1f, 0.15f));	
			if (GUI.Button(new Rect(popupInnerRectX + popupInnerRectWidth * 0.35f, popupInnerRectY + popupInnerRectHeight * 0.42f, popupInnerRectWidth * 0.3f, popupInnerRectHeight * 0.15f), "Make a Donation")) {
				Application.OpenURL("http://www.felidaefund.org/donate");
			}
			customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);
			customGUISkin.button.hover.textColor = defaultHoverColor;
		}

		// "learn more" section

		if (infoPanelIntroFlag == false && infoPanelPage != 0) {
		
			float yOffsetForPage5 = infoPanelPage == 5 ? (popupInnerRectHeight * -0.12f) : 0f;
		
			if (infoPanelPage == 5)
				customGUISkin.button.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			else
				customGUISkin.button.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1f);

			style.fontStyle = FontStyle.BoldAndItalic;
			style.alignment = TextAnchor.MiddleCenter;
			style.fontSize = (int)(popupInnerRectWidth * 0.014f);
			style.normal.textColor = new Color(0.6f, 0.6f, 0.6f, 1f);
			if (infoPanelPage == 5)
				GUI.Button(new Rect(popupInnerRectX + popupInnerRectWidth * 0.4f, popupInnerRectY + popupInnerRectHeight * 0.765f + yOffsetForPage5, popupInnerRectWidth * 0.2f, popupInnerRectHeight * 0.06f), "Get involved...", style);
			else
				GUI.Button(new Rect(popupInnerRectX + popupInnerRectWidth * 0.4f, popupInnerRectY + popupInnerRectHeight * 0.765f, popupInnerRectWidth * 0.2f, popupInnerRectHeight * 0.06f), "Learn more at...", style);

			GUI.color = new Color(1f, 1f, 1f, 0.9f * infoPanelOpacity);

			customGUISkin.button.fontSize = (int)(overlayRect.width * 0.016);
			customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			if (GUI.Button(new Rect(popupInnerRectX + popupInnerRectWidth * 0.312f, popupInnerRectY + popupInnerRectHeight * 0.835f + yOffsetForPage5, popupInnerRectWidth * 0.17f, popupInnerRectHeight * 0.045f), "Felidae Fund")) {
				Application.OpenURL("http://www.felidaefund.org");
			}
		
			customGUISkin.button.fontSize = (int)(overlayRect.width * 0.016);
			customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			if (GUI.Button(new Rect(popupInnerRectX + popupInnerRectWidth * 0.518f, popupInnerRectY + popupInnerRectHeight * 0.835f + yOffsetForPage5, popupInnerRectWidth * 0.17f, popupInnerRectHeight * 0.045f), "Bay Area Puma Project")) {
				Application.OpenURL("http://www.bapp.org");
			}	
			
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);


			if (infoPanelPage == 5) {

				// facebook
				textureHeight = popupInnerRectHeight * 0.055f;
				textureWidth = iconFacebookTexture.width * (textureHeight / iconFacebookTexture.height);
				textureX = popupInnerRectX + popupInnerRectWidth * 0.333f;
				textureY = popupInnerRectY + popupInnerRectHeight * 0.8f;
				if (GUI.Button(new Rect(textureX, textureY, textureWidth, textureHeight), "")) {
					Application.OpenURL("http://www.facebook.com/felidaefund");
				}	
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), iconFacebookTexture);
				// twitter
				textureWidth = iconTwitterTexture.width * (textureHeight / iconTwitterTexture.height);
				textureX += popupInnerRectWidth * 0.06f;
				if (GUI.Button(new Rect(textureX, textureY, textureWidth, textureHeight), "")) {
					Application.OpenURL("http://www.twitter.com/felidaefund");
				}	
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), iconTwitterTexture);
				// google
				textureWidth = iconGoogleTexture.width * (textureHeight / iconGoogleTexture.height);
				textureX += popupInnerRectWidth * 0.06f;
				if (GUI.Button(new Rect(textureX, textureY, textureWidth, textureHeight), "")) {
					Application.OpenURL("http://plus.google.com/u/0/118124929806137459330/posts");
				}	
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), iconGoogleTexture);
				// pinterest
				textureWidth = iconPinterestTexture.width * (textureHeight / iconPinterestTexture.height);
				textureX += popupInnerRectWidth * 0.06f;
				if (GUI.Button(new Rect(textureX, textureY, textureWidth, textureHeight), "")) {
					Application.OpenURL("http://www.pinterest.com/felidaefund");
				}	
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), iconPinterestTexture);
				// youtube
				textureWidth = iconYouTubeTexture.width * (textureHeight / iconYouTubeTexture.height);
				textureX += popupInnerRectWidth * 0.06f;
				if (GUI.Button(new Rect(textureX, textureY, textureWidth, textureHeight), "")) {
					Application.OpenURL("http://www.youtube.com/felidaefund");
				}	
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), iconYouTubeTexture);
				// linkedin
				textureWidth = iconLinkedInTexture.width * (textureHeight / iconLinkedInTexture.height);
				textureX += popupInnerRectWidth * 0.06f;
				if (GUI.Button(new Rect(textureX, textureY, textureWidth, textureHeight), "")) {
					Application.OpenURL("http://www.linkedin.com/groups/Felidae-Conservation-Fund-1108927?gid=1108927&trk=hb_side_g");
				}	
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), iconLinkedInTexture);
			}
		}
		
		
	}

	
	

	/////////////////////
	////// UTILITIES
	/////////////////////



	

	void CalculateOverlayRect()
	{ 
		// this rect is used by both OverlayPanel and InfoPanel
		
		float overlayBackgroundInset = backgroundTexture.width * 0.02f;
		float overlayWidth = backgroundTexture.width + overlayBackgroundInset;
		float overlayHeight = backgroundTexture.height + overlayBackgroundInset;
		float overlayAspectRatio = overlayWidth / overlayHeight;
		float overlayInsetHorz = Screen.width * 0.05f;
		float overlayInsetVert = overlayInsetHorz / overlayAspectRatio;

		if (overlayAspectRatio > (Screen.width - overlayInsetHorz) / (Screen.height - overlayInsetVert)) {
			// overlay wider than screen; tight to left/right
			overlayRect.width = Screen.width - (overlayInsetHorz * 2);
			overlayRect.height = overlayRect.width / overlayAspectRatio;
			overlayRect.x = overlayInsetHorz;
			overlayRect.y = Screen.height/2 - overlayRect.height/2;
		}
		else {
			// overlay narrower than screen; tight to top/bottom
			overlayRect.height = Screen.height - (overlayInsetVert * 2);
			overlayRect.width = overlayRect.height * overlayAspectRatio;
			overlayRect.y = overlayInsetVert;
			overlayRect.x = Screen.width/2 - overlayRect.width/2;
		}	
	}

	public Rect GetOverlayRect()
	{
		return overlayRect;
	}








	public void OpenInfoPanel(int newPanelNum)
	{
		if (newPanelNum >= 0)
			infoPanelPage = newPanelNum;
		infoPanelVisible = true;
		infoPanelTransStart = Time.time;
	}
 
 

	
	
	void DrawRect(Rect position, Color color)
	{
		for(int i = 0; i < rectTexture.width; i++) {
			for(int j = 0; j < rectTexture.height; j++) {
				rectTexture.SetPixel(i, j, color);
			}
		}
		rectTexture.Apply();
		rectStyle.normal.background = rectTexture;
		GUILayout.BeginArea(position, rectStyle);
		GUILayout.EndArea();		
	}
	
	
	//////////////////

	public float GetPumaSpeed(int pumaNum)
	{
		return (speedArray[pumaNum]);
	}

	public float GetPumaStealth(int pumaNum)
	{
		return (stealthArray[pumaNum]);
	}

	public float GetPumaEndurance(int pumaNum)
	{
		return (enduranceArray[pumaNum]);
	}

	public float GetPumaPower(int pumaNum)
	{
		return (powerArray[pumaNum]);
	}

	//////////////////

	float GetSelectedPumaSpeed()
	{
		return (selectedPuma == -1) ?  0f : speedArray[selectedPuma];
	}

	float GetSelectedPumaStealth()
	{
		return (selectedPuma == -1) ?  0f : stealthArray[selectedPuma];
	}

	float GetSelectedPumaEndurance()
	{
		return (selectedPuma == -1) ?  0f : enduranceArray[selectedPuma];
	}

	float GetSelectedPumaPower()
	{
		return (selectedPuma == -1) ?  0f : powerArray[selectedPuma];
	}

	
	bool PumaIsSelectable(int pumaNum)
	{	
		if (scoringSystem.GetPumaHealth(pumaNum) <= 0f)
			return false;
			
		if (scoringSystem.GetPumaHealth(pumaNum) >= 1f)
			return false;
			
		return true;
	}
	
	
	void DecrementPuma()
	{
		for (int i = selectedPuma-1; i >= 0; i--) {
			if (PumaIsSelectable(i)) {
				selectedPuma = i;
				levelManager.SetSelectedPuma(selectedPuma);
				return;
			}
		}
	}
	
	
	void IncrementPuma()
	{
		for (int i = selectedPuma+1; i < 6; i++) {
			if (PumaIsSelectable(i)) {
				selectedPuma = i;
				levelManager.SetSelectedPuma(selectedPuma);
				return;
			}
		}
	}
	
	
	void DebounceKeyboardInput()
	{
		// filter out key repeats
				
		spacePressed = false;
		leftShiftPressed = false;
		rightShiftPressed = false;
		leftArrowPressed = false;
		rightArrowPressed = false;
		
		bool leftArrowState = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.J);
		bool rightArrowState = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.L);


		if (debounceSpace == true) {
			if (Input.GetKey(KeyCode.Space) == false)
				debounceSpace = false;
		}
		else if (Input.GetKey(KeyCode.Space) == true) {
			spacePressed = true;
			debounceSpace = true;
		}
	
		if (debounceLeftShift == true) {
			if (Input.GetKey(KeyCode.LeftShift) == false)
				debounceLeftShift = false;
		}
		else if (Input.GetKey(KeyCode.LeftShift) == true) {
			leftShiftPressed = true;
			debounceLeftShift = true;
		}
	
		if (debounceRightShift == true) {
			if (Input.GetKey(KeyCode.RightShift) == false)
				debounceRightShift = false;
		}
		else if (Input.GetKey(KeyCode.RightShift) == true) {
			rightShiftPressed = true;
			debounceRightShift = true;
		}
		
		if (debounceLeftArrow == true) {
			if (leftArrowState == false)
				debounceLeftArrow = false;
		}
		else if (leftArrowState == true) {
			leftArrowPressed = true;
			debounceLeftArrow = true;
		}
	
		if (debounceRightArrow == true) {
			if (rightArrowState == false)
				debounceRightArrow = false;
		}
		else if (rightArrowState == true) {
			rightArrowPressed = true;
			debounceRightArrow = true;
		}
		
	
	
	}
	
	
}
