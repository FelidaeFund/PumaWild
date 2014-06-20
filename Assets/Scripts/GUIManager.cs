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

	private int currentScreen = 0;
	private int currentLevel = 0;			// TEMP: remove this when the code that uses it is gone !!!!
	public int selectedPuma = -1;			// TEMP: !!! should not be public; need to consolidate in either LevelManager or GUIManager, with function call to get it
	private int difficultyLevel = 1;
	private int soundEnable = 1;
	private float soundVolume = 0.5f;
	private float pawRightFlag = 1;
	private bool showHealthBarPumaIcons = true;
	private bool centerHealthBarLabels = true;
	
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
	private GUIStyle buttonStyle;	
	private GUIStyle buttonDownStyle;
	private GUIStyle buttonDisabledStyle;
	private GUIStyle bigButtonStyle;
	private GUIStyle bigButtonDisabledStyle;
	private GUIStyle swapButtonStyle;
	private GUIStyle buttonSimpleStyle;
	private GUISkin customSkin;
	private GUIStyle sliderBarStyle;
	private GUIStyle sliderThumbStyle;

	// EXTERNAL MODULES
	private GuiComponents guiComponents;		// TEMP: may not need this when this file has been fully pruned
	private LevelManager levelManager;
	private ScoringSystem scoringSystem;
	private InputControls inputControls;
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
				if (currentScreen == 3) {
					// return to select screen rather than quit screen
					currentScreen = 0;
				}
				if (scoringSystem.GetPumaHealth(selectedPuma) <= 0f) {
					// puma has died
					selectedPuma = -1;
					currentScreen = 0;
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
		if (currentScreen == 0 || currentScreen == 2) {
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
				DrawOverlayPanel();
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
 
 
	public void OpenPopupPanel(int panelNum)
	{
		infoPanelPage = panelNum;
		infoPanelVisible = true;
		infoPanelTransStart = Time.time;
	}
 
 

	
	void DrawOverlayPanel() 
	{ 
		// background panel
		GUI.color = new Color(0f, 0f, 0f, 1f * guiOpacity);
		GUI.Box(new Rect(overlayRect.x, overlayRect.y, overlayRect.width, overlayRect.height), "");
		//GUI.color = new Color(0f, 0f, 0f, 0.3f * guiOpacity);
		//GUI.Box(new Rect(overlayRect.x, overlayRect.y, overlayRect.width, overlayRect.height), "");
		
		//background image
		GUI.color = new Color(1f, 1f, 1f, 0.75f * guiOpacity);
		GUI.DrawTexture(new Rect(overlayRect.x + 4, overlayRect.y + 4, overlayRect.width-8, overlayRect.height-8), backgroundTexture);
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		
		// MAIN TTTLE
		
		float upperItemsYShift = overlayRect.height * 0.01f;

		float logoX = overlayRect.x + overlayRect.width * 0.36f;
		float logoY = overlayRect.y - overlayRect.height * 0.02f + upperItemsYShift;
		float logoWidth = overlayRect.width * 0.28f;
		float logoHeight = logoTexture.height * (logoWidth / logoTexture.width);
		//GUI.color = new Color(1f, 1f, 1f, 0.75f * guiOpacity);
		GUI.DrawTexture(new Rect(logoX, logoY, logoWidth, logoHeight), logoTexture);
		//GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);

/*		
		GUI.color = new Color(1f, 1f, 1f, 0.93f * guiOpacity);
		float xPos = overlayRect.x + overlayRect.width * 0.349f;
		style.fontSize = (int)(overlayRect.width * 0.048f);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.normal.textColor = new Color(0.2f, 0f, 0f, 1f);
		GUI.Button(new Rect(xPos - overlayRect.width * 0.003f, overlayRect.y + overlayRect.height * 0.008f + upperItemsYShift, overlayRect.width * 0.30f, overlayRect.height * 0.10f), "PumaWild", style);
		GUI.Button(new Rect(xPos + overlayRect.width * 0.003f, overlayRect.y + overlayRect.height * 0.008f + upperItemsYShift, overlayRect.width * 0.30f, overlayRect.height * 0.10f), "PumaWild", style);
		GUI.Button(new Rect(xPos - overlayRect.width * 0.003f, overlayRect.y + overlayRect.height * 0.016f + upperItemsYShift, overlayRect.width * 0.30f, overlayRect.height * 0.10f), "PumaWild", style);
		GUI.Button(new Rect(xPos + overlayRect.width * 0.003f, overlayRect.y + overlayRect.height * 0.016f + upperItemsYShift, overlayRect.width * 0.30f, overlayRect.height * 0.10f), "PumaWild", style);
		style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
		GUI.Button(new Rect(xPos, overlayRect.y + overlayRect.height * 0.012f + upperItemsYShift, overlayRect.width * 0.30f, overlayRect.height * 0.10f), "PumaWild", style);
		style.fontSize = (int)(overlayRect.width * 0.0228f);
		style.normal.textColor = new Color(0.2f, 0f, 0f, 1f);
		xPos = overlayRect.x + overlayRect.width * 0.351f;
		GUI.Button(new Rect(xPos - overlayRect.width * 0.001f, overlayRect.y + overlayRect.height * 0.084f + upperItemsYShift, overlayRect.width * 0.30f, overlayRect.height * 0.05f), "survival is not a given", style);
		GUI.Button(new Rect(xPos + overlayRect.width * 0.001f, overlayRect.y + overlayRect.height * 0.084f + upperItemsYShift, overlayRect.width * 0.30f, overlayRect.height * 0.05f), "survival is not a given", style);
		GUI.Button(new Rect(xPos - overlayRect.width * 0.001f, overlayRect.y + overlayRect.height * 0.088f + upperItemsYShift, overlayRect.width * 0.30f, overlayRect.height * 0.05f), "survival is not a given", style);
		GUI.Button(new Rect(xPos + overlayRect.width * 0.001f, overlayRect.y + overlayRect.height * 0.088f + upperItemsYShift, overlayRect.width * 0.30f, overlayRect.height * 0.05f), "survival is not a given", style);
		style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
		GUI.Button(new Rect(xPos, overlayRect.y + overlayRect.height * 0.086f + upperItemsYShift, overlayRect.width * 0.30f, overlayRect.height * 0.05f), "survival is not a given", style);
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
*/


		// LEVEL and STATUS DISPLAYS

		float levelPanelX = overlayRect.x + overlayRect.width * 0.04f;
		float levelPanelY = overlayRect.y + overlayRect.width * 0.016f + upperItemsYShift;
		float levelPanelWidth = overlayRect.width * 0.30f;
		float levelPanelHeight = overlayRect.width * 0.076f;
		guiComponents.DrawLevelPanel(guiOpacity, levelPanelX, levelPanelY, levelPanelWidth, levelPanelHeight, false);
					
		float statusPanelX = overlayRect.x + overlayRect.width * 0.66f;
		float statusPanelY = overlayRect.y + overlayRect.width * 0.016f + upperItemsYShift;
		float statusPanelWidth = overlayRect.width * 0.30f;
		float statusPanelHeight = overlayRect.width * 0.076f;
		guiComponents.DrawStatusPanel(guiOpacity, statusPanelX, statusPanelY, statusPanelWidth, statusPanelHeight, false);

		
		
		//=====================================
		// ADD BUTTONS
		//=====================================
		
		// background rectangle
		GUI.color = new Color(0f, 0f, 0f, 1f * guiOpacity);
		GUI.Box(new Rect(overlayRect.x + overlayRect.width * 0f, overlayRect.y + overlayRect.height * 0.926f, overlayRect.width * 1f, overlayRect.height * 0.074f), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);

		
		// 'help' button
		// background rectangle
		float helpButtonX = overlayRect.x + overlayRect.width * 0.59f;
		float helpButtonY = overlayRect.y + overlayRect.height * 0.937f;
		float helpButtonWidth = overlayRect.width * 0.1f;
		float helpButtonHeight = overlayRect.height * 0.05f;
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		GUI.skin = customGUISkin;
		customGUISkin.button.fontSize = (int)(overlayRect.width * 0.02);
		customGUISkin.button.fontStyle = FontStyle.Normal;
		GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
		GUI.color = new Color(1f, 1f, 1f, 0.5f * guiOpacity);
		if (GUI.Button(new Rect(helpButtonX, helpButtonY, helpButtonWidth, helpButtonHeight), "")) {
			infoPanelVisible = true;
			//infoPanelPage = 1;
			infoPanelTransStart = Time.time;
		}
		GUI.color = new Color(1f, 1f, 1f, 0.9f * guiOpacity);
		if (GUI.Button(new Rect(helpButtonX, helpButtonY, helpButtonWidth, helpButtonHeight), "Info...")) {
			infoPanelVisible = true;
			//infoPanelPage = 1;
			infoPanelTransStart = Time.time;
		}
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);

		
		// other buttons...
		
		buttonStyle.fontSize = buttonDownStyle.fontSize = (int)(overlayRect.width * 0.024);
		buttonDisabledStyle.fontSize = buttonDownStyle.fontSize = (int)(overlayRect.width * 0.024);
		float buttonWidth = overlayRect.width * 0.11f;
		float buttonGap = overlayRect.width * 0.011f;
		float buttonMargin = overlayRect.x + overlayRect.width * 0.04f;
		float buttonY = overlayRect.y + overlayRect.height * 0.924f;
		float buttonheight = overlayRect.height * 0.075f;
		
		int buttonIndex = (currentScreen == 1) ? 2 : ((currentScreen == 2) ? 1 : currentScreen);
		float backgroundRectWidthAdjust = (currentScreen == 1) ? buttonWidth * 0.1f : ((currentScreen == 3) ? buttonWidth * -0.1f : 0f);
		DrawRect(new Rect(buttonMargin + buttonWidth*buttonIndex + buttonGap*buttonIndex + buttonWidth*0.05f - backgroundRectWidthAdjust*0.5f, buttonY + buttonheight * 0.15f, buttonWidth - buttonWidth*0.1f + backgroundRectWidthAdjust, buttonheight - buttonheight * 0.29f), new Color(0f, 0f, 0f, 0.5f));	


		buttonDownStyle.normal.textColor = new Color(0.99f, 0.7f, 0.2f, 1f);
		buttonStyle.normal.textColor = new Color(0.99f, 0.88f, 0.6f, 1f);


		// 'select'
		if (GUI.Button(new Rect(buttonMargin, buttonY, buttonWidth, buttonheight), "Select", (currentScreen == 0) ? buttonDownStyle : buttonStyle))
			currentScreen = 0;
		// 'stats'
		if (GUI.Button(new Rect(buttonMargin + buttonWidth + buttonGap, buttonY, buttonWidth, buttonheight), "Stats", (currentScreen == 2) ? buttonDownStyle : buttonStyle))
			currentScreen = 2;
		// 'options'
		if (GUI.Button(new Rect(buttonMargin + buttonWidth*2f + buttonGap*2f, buttonY, buttonWidth, buttonheight), "Options", (currentScreen == 1) ? buttonDownStyle : buttonStyle))
			currentScreen = 1;
		// 'quit'
		if (GUI.Button(new Rect(buttonMargin + buttonWidth*3f + buttonGap*3f, buttonY, buttonWidth, buttonheight), "Quit", (currentScreen == 3) ? buttonDownStyle : buttonStyle))
			currentScreen = 3;

		buttonStyle.normal.textColor = new Color(0.99f, 0.7f, 0.2f, 1f);
		buttonDownStyle.normal.textColor = new Color(0.99f, 0.88f, 0.6f, 1f);


		buttonStyle.fontSize = buttonDownStyle.fontSize = (int)(overlayRect.width * 0.023);
		buttonDisabledStyle.fontSize = buttonDownStyle.fontSize = (int)(overlayRect.width * 0.023);
		buttonWidth = overlayRect.width * 0.12f;

		// 'intro'
		//GUI.Button(new Rect(buttonMargin, overlayRect.height * 0.90f, buttonWidth, overlayRect.height * 0.06f), "< Intro", buttonStyle);

		// 'more'
		//GUI.Button(new Rect(overlayRect.width - buttonMargin - buttonWidth, overlayRect.height * 0.90f, buttonWidth, overlayRect.height * 0.06f), "More >", buttonStyle);

		// 'play'
		// background rectangle
		float startButtonX = overlayRect.x + overlayRect.width * 0.795f;
		float startButtonY = overlayRect.y + overlayRect.height * 0.932f;
		float startButtonWidth = overlayRect.width * 0.15f;
		float startButtonHeight = overlayRect.height * 0.06f;
		GUI.color = (selectedPuma != -1) ? new Color(1f, 1f, 1f, 1f * guiOpacity) : new Color(1f, 1f, 1f, 0.5f * guiOpacity);
		bigButtonStyle.fontSize = (int)(overlayRect.width * 0.032);;
		bigButtonDisabledStyle.fontSize = (int)(overlayRect.width * 0.03);;
		GUI.skin = customGUISkin;
		customGUISkin.button.fontSize = (int)(overlayRect.width * 0.028);
		customGUISkin.button.fontStyle = FontStyle.Normal;
		GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
		if (selectedPuma != -1) {
			if (GUI.Button(new Rect(startButtonX, startButtonY, startButtonWidth, startButtonHeight), "")) {
				SetGuiState("guiStateLeavingOverlay");
				levelManager.SetGameState("gameStateLeavingGui");
			}
			if (GUI.Button(new Rect(startButtonX, startButtonY, startButtonWidth, startButtonHeight), "Start")) {
			//if (GUI.Button(new Rect(startButtonX, startButtonY, startButtonWidth, startButtonHeight), "Start", (selectedPuma != -1) ? bigButtonStyle : bigButtonDisabledStyle) && (selectedPuma != -1)) {
				SetGuiState("guiStateLeavingOverlay");
				levelManager.SetGameState("gameStateLeavingGui");
			}
		}
		else {
			GUI.Button(new Rect(startButtonX, startButtonY, startButtonWidth, startButtonHeight), "Start", bigButtonDisabledStyle);
		}
		
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		
		
		// add selected screen
		switch (currentScreen) {
		case 0:
			DrawSelectScreen();
			break;		
		case 1:
			DrawOptionsScreen();
			break;
		case 2:
			DrawStatsScreen();
			break;
		case 3:
			DrawQuitScreen();
			break;
		}
		
	}


	
	
	bool modifyLayoutFlag = false;

	void DrawSelectScreen() 
	{ 
		float textureX;
		float textureY;
		float textureWidth;
		float textureHeight;
		float oldTextureX;
		
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;

		// background rectangle

		float backRectX = overlayRect.x + overlayRect.width * 0.06f;
		float backRectY = overlayRect.y + overlayRect.height * 0.205f;
		float backRectWidth = overlayRect.width * 0.88f;
		float backRectHeight = overlayRect.height * 0.578f;
		float fontScale = 0.8f;

		GUI.color = new Color(0f, 0f, 0f, 1f * guiOpacity);
		GUI.Box(new Rect(backRectX - overlayRect.width * 0.02f, backRectY - overlayRect.height * 0.025f, backRectWidth + overlayRect.width * 0.04f, backRectHeight + overlayRect.height * 0.05f), "");
		GUI.color = new Color(0f, 0f, 0f, 0.5f * guiOpacity);
		GUI.Box(new Rect(backRectX, backRectY, backRectWidth, backRectHeight), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);


		// select screen prompt

		if (selectedPuma == -1) {
		
			float yOffset = overlayRect.height * -0.03f;
		
			//DrawRect(new Rect(overlayRect.width * 0.32f, overlayRect.height * 0.32f, overlayRect.width * 0.36f, overlayRect.height * 0.12f), new Color(1f, 1f, 1f, 0.4f));	
			//DrawRect(new Rect(overlayRect.width * 0.30f, overlayRect.height * 0.26f, overlayRect.width * 0.40f, overlayRect.height * 0.16f), new Color(1f, 1f, 1f, 0.6f));	
			GUI.color = new Color(0f, 0f, 0f, 1f * guiOpacity);
			GUI.Box(new Rect(overlayRect.x + overlayRect.width * 0.37f, yOffset + overlayRect.y + overlayRect.height * 0.195f, overlayRect.width * 0.26f, overlayRect.height * 0.064f), "");
			//GUI.color = new Color(0f, 0f, 0f, 0.5f * guiOpacity);
			//GUI.Box(new Rect(overlayRect.width * 0.32f, overlayRect.height * 0.31f, overlayRect.width * 0.36f, overlayRect.height * 0.16f), "");

			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			style.fontSize = (int)(overlayRect.width * 0.030f);
			style.fontStyle = FontStyle.BoldAndItalic;
			//style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);
			//style.normal.textColor = new Color(1f, 1f, 1f, 1f);
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			GUI.Button(new Rect(overlayRect.x + overlayRect.width * 0.3f, yOffset + overlayRect.y + overlayRect.height * 0.18f, overlayRect.width * 0.4f, overlayRect.height * 0.1f), "Select  Puma...", style);
		}

		style.normal.textColor = Color.white;
		style.fontStyle = FontStyle.BoldAndItalic;
		
		
		

		// add population bar
		
		float yOffsetForAddingPopulationBar = overlayRect.height * -0.012f;
		float actualGuiOpacity = guiOpacity;
		guiOpacity = guiOpacity * 0.9f;
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);		
		
		float healthBarX = overlayRect.x + overlayRect.width * 0.04f;
		float healthBarY = overlayRect.y + overlayRect.height * 0.844f + yOffsetForAddingPopulationBar;
		float healthBarWidth = overlayRect.width * 0.92f;
		float healthBarHeight = overlayRect.height * 0.048f;	
		float healthBarLabelWidth = healthBarWidth * 0.13f;

		GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
		GUI.Box(new Rect(healthBarX, healthBarY, healthBarLabelWidth * 0.985f, healthBarHeight), "");
		GUI.Box(new Rect(healthBarX, healthBarY, healthBarLabelWidth * 0.985f, healthBarHeight), "");
		GUI.Box(new Rect(healthBarX + healthBarWidth - healthBarLabelWidth * 0.985f, healthBarY, healthBarLabelWidth * 0.985f, healthBarHeight), "");
		GUI.Box(new Rect(healthBarX + healthBarWidth - healthBarLabelWidth * 0.985f, healthBarY, healthBarLabelWidth * 0.985f, healthBarHeight), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);

		guiComponents.DrawPopulationHealthBar(guiOpacity, healthBarX + healthBarLabelWidth, healthBarY, healthBarWidth - healthBarLabelWidth * 2f, healthBarHeight, false, false);

		guiOpacity = actualGuiOpacity;
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);		
		
		//
		
		yOffsetForAddingPopulationBar += overlayRect.height * -0.005f;
		Texture headshotTexture;
		float headshotX;
		float headshotY;
		float headshotWidth;
		float headshotHeight;
		
		Color unselectedTextColor = new Color(0.85f, 0.74f, 0.5f, 0.85f);

		
		// used in DrawSelectScreen() and DrawSelectScreenDetails()
		float headUpShift = overlayRect.height * -0.03f;
		float textUpShift = overlayRect.height * -0.283f;
		float barsDownShift = overlayRect.height * 0.06f;
		float iconDownShift = overlayRect.height * 0.06f;
		float healthDownShift = overlayRect.height * 0.315f;
		float headshotBackgroundHeightAddition = overlayRect.height * 0.06f;

		headUpShift = 0f;
		healthDownShift = 0f;

		if (modifyLayoutFlag == false) {
			textUpShift = 0f;
			barsDownShift = 0f;
			iconDownShift = 0f;
			headshotBackgroundHeightAddition = 0f;
		}


			//Color pumaFullHealthColor = new Color(0.84f, 0.99f, 0.0f, 0.72f * guiOpacity);
			//Color pumaDeadColor = new Color(0.76f, 0.0f, 0f, 0.47f * guiOpacity);
			//Color pumaDeadColor = new Color(0.1f, 0.1f, 0.1f, 0.6f * guiOpacity);



		//Color fullHealthPumaHeadshotColor = new Color(0.2f, 1f, 0.05f, 1f * guiOpacity);
		//Color fullHealthPumaIconColor = new Color(0.1f, 1f, 0f, 0.65f * guiOpacity);
		Color fullHealthPumaHeadshotColor = new Color(0.99f, 0.92f, 0f, 0.62f * guiOpacity);
		Color fullHealthPumaIconColor = new Color(0.99f, 0.92f, 0f, 0.27f * guiOpacity);
		//Color fullHealthPumaTextColor = new Color(0.1f, 0.5f, 0f, 0.8f * guiOpacity);
		Color fullHealthPumaTextColor = new Color(0.32f, 0.32f, 0.22f, 0.8f * guiOpacity);
		Color fullHealthPumaAnnounceColor = new Color(0.01f, 0.85f, 0f, 0.8f * guiOpacity);

		//Color deadPumaHeadshotColor = new Color(0.8f, 0.1f, 0f, 0.55f * guiOpacity);
		Color deadPumaHeadshotColor = new Color(0.12f, 0.12f, 0.12f, 0.7f * guiOpacity);
		//Color deadPumaIconColor = new Color(0.8f, 0f, 0f, 0.3f * guiOpacity);
		Color deadPumaIconColor = new Color(0.08f, 0.08f, 0.08f, 0.99f * guiOpacity);
		//Color deadPumaTextColor = new Color(0.05f, 0.05f, 0.02f, 1f * guiOpacity);
		Color deadPumaTextColor = new Color(0.32f, 0.32f, 0.22f, 0.8f * guiOpacity);
		Color deadPumaAnnounceColor = new Color(0.65f, 0.05f, 0f, 1f * guiOpacity);

		float endingLabelDownshift = overlayRect.height * 0.036f;
		
		// young male
		textureX = overlayRect.x + overlayRect.width * ((selectedPuma == -1) ? 0.102f : 0.089f);
		textureX += overlayRect.width * ((selectedPuma == 0) ? -0.002f : 0f);
		textureY = overlayRect.y + overlayRect.height * ((selectedPuma == 0) ? 0.572f : 0.585f) + yOffsetForAddingPopulationBar;
		textureWidth = overlayRect.width * ((selectedPuma == 0) ? 0.16f : 0.115f);
		textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
		if (selectedPuma != 0) {
			// background panel and puma head
			headshotY = overlayRect.y + overlayRect.height * 0.355f + yOffsetForAddingPopulationBar + headUpShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f + headshotBackgroundHeightAddition), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			// puma head
			headshotTexture = closeup1Texture;
			headshotHeight = overlayRect.height * 0.085f;
			headshotWidth = headshotTexture.width * (headshotHeight / headshotTexture.height);
			headshotX = textureX + (textureWidth - headshotWidth) * 0.5f;
			if (scoringSystem.GetPumaHealth(0) <= 0f)
				GUI.color = deadPumaHeadshotColor;
			else if (scoringSystem.GetPumaHealth(0) >= 1f)
				GUI.color = fullHealthPumaHeadshotColor;
			GUI.DrawTexture(new Rect(headshotX, headshotY, headshotWidth, headshotHeight), headshotTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			// health bar
			if (scoringSystem.GetPumaHealth(0) > 0f && scoringSystem.GetPumaHealth(0) < 1f)
				guiComponents.DrawPumaHealthBar(0, guiOpacity, textureX, headshotY - headshotHeight * 0.36f + healthDownShift, textureWidth, headshotHeight * 0.26f, true);
			// background panel for puma middle
			headshotY = overlayRect.y + overlayRect.height * 0.47f + yOffsetForAddingPopulationBar + barsDownShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			// display bars in puma middle
			DrawDisplayBars(0, headshotX, headshotY, headshotWidth, headshotHeight);
			// final ending label at very top
			if (scoringSystem.GetPumaHealth(0) <= 0f) {
				// puma is dead
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				style.normal.textColor = deadPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), "STARVED", style);
			}
			else if (scoringSystem.GetPumaHealth(0) >= 1f) {
				// puma at full health
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				style.normal.textColor = fullHealthPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), "Full Health", style);
				float checkMarkWidth = overlayRect.width * 0.021f;
				float checkMarkHeight = greenCheckTexture.height * (checkMarkWidth / greenCheckTexture.width);
				float checkMarkX = textureX + textureWidth * 0.84f;
				float checkMarkY = overlayRect.y + overlayRect.height * 0.282f + yOffsetForAddingPopulationBar + textUpShift;
				GUI.color = new Color(1f, 1f, 1f, 0.8f * guiOpacity);
				GUI.DrawTexture(new Rect(checkMarkX, checkMarkY + endingLabelDownshift, checkMarkWidth, checkMarkHeight), greenCheckTexture);
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			}
		}
		// background panel for text or puma icon
		if (selectedPuma == 0) {
			GUI.color = new Color(0f, 0f, 0f, 0.4f * guiOpacity);
			//GUI.Box(new Rect(textureX + overlayRect.width * .015f, overlayRect.y + overlayRect.height * 0.73f + yOffsetForAddingPopulationBar, textureWidth - overlayRect.width * .03f, overlayRect.height * 0.065f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		}
		else {
			// not selected
			GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
			GUI.Box(new Rect(textureX + overlayRect.width * .0f, overlayRect.y + overlayRect.height * 0.585f + yOffsetForAddingPopulationBar + iconDownShift, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.14f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		}
		// puma icon
		GUI.color = new Color(1f, 1f, 1f, 0f * guiOpacity);
		if (GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.315f, textureWidth, overlayRect.height * 0.47f), "") && PumaIsSelectable(0)) {
			selectedPuma = 0;
			levelManager.SetSelectedPuma(0);
		}
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		if (selectedPuma == 0) {
			float selectedIconX = textureX + overlayRect.width * 0.024f;
			float selectedIconY = textureY + overlayRect.height * 0.018f;
			float selectedIconWidth = textureWidth * 0.74f;
			float selectedIconHeight = textureHeight * 0.73f;
			GUI.DrawTexture(new Rect(selectedIconX - (selectedIconWidth * 0.05f), selectedIconY - (selectedIconHeight * 0.055f) + iconDownShift, selectedIconWidth * 1.116f, selectedIconHeight * 1.116f), pumaIconShadowTexture);
			GUI.DrawTexture(new Rect(selectedIconX, selectedIconY + iconDownShift, selectedIconWidth, selectedIconHeight), pumaIconTexture);
		}
		else {
			if (scoringSystem.GetPumaHealth(0) <= 0f)
				GUI.color = deadPumaIconColor;
			else if (scoringSystem.GetPumaHealth(0) >= 1f)
				GUI.color = fullHealthPumaIconColor;
			else
				GUI.color = new Color(1f, 1f, 1f, 0.7f * guiOpacity);
			GUI.DrawTexture(new Rect(textureX + textureWidth * 0.05f, textureY + textureHeight * 0.12f + iconDownShift, textureWidth * 0.9f, textureHeight * 0.9f), pumaIconTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		}
		if (selectedPuma == 0)
			DrawSelectScreenDetailsPanel(style, textureX, textureWidth, headshot1Texture);
		// text
		if (selectedPuma == 0)
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
		else if (scoringSystem.GetPumaHealth(0) <= 0f)
			style.normal.textColor = deadPumaTextColor;
		else if (scoringSystem.GetPumaHealth(0) >= 1f)
			style.normal.textColor = fullHealthPumaTextColor;
		else
			style.normal.textColor = unselectedTextColor;
		style.fontSize = (int)(overlayRect.width * ((selectedPuma == 0) ? 0.021f : 0.019f));
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((selectedPuma == 0) ? 0.71f : 0.71f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "Eric", style);
		style.fontSize = (int)(overlayRect.width * ((selectedPuma == 0) ? 0.015f : 0.014f));
		//style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((selectedPuma == 0) ? 0.735f : 0.735f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "2 years - male", style);
		style.normal.textColor = Color.white;
		


		// young female
		textureX += overlayRect.width * ((selectedPuma == 0) ? 0.18f : 0.135f);
		textureX += overlayRect.width * ((selectedPuma == 1) ? -0.002f : 0f);
		textureY = overlayRect.y + overlayRect.height * ((selectedPuma == 1) ? 0.572f : 0.585f) + yOffsetForAddingPopulationBar;
		textureWidth = overlayRect.width * ((selectedPuma == 1) ? 0.16f : 0.115f);
		textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
		if (selectedPuma != 1) {
			// background panel for puma head
			headshotY = overlayRect.y + overlayRect.height * 0.355f + yOffsetForAddingPopulationBar + headUpShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f + headshotBackgroundHeightAddition), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			// puma head
			headshotTexture = closeup2Texture;
			headshotHeight = overlayRect.height * 0.085f;
			headshotWidth = headshotTexture.width * (headshotHeight / headshotTexture.height);
			headshotX = textureX + (textureWidth - headshotWidth) * 0.5f;
			if (scoringSystem.GetPumaHealth(1) <= 0f)
				GUI.color = deadPumaHeadshotColor;
			else if (scoringSystem.GetPumaHealth(1) >= 1f)
				GUI.color = fullHealthPumaHeadshotColor;
			GUI.DrawTexture(new Rect(headshotX, headshotY, headshotWidth, headshotHeight), headshotTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			// health bar
			if (scoringSystem.GetPumaHealth(1) > 0f && scoringSystem.GetPumaHealth(1) < 1f)
				guiComponents.DrawPumaHealthBar(1, guiOpacity, textureX, headshotY - headshotHeight * 0.36f + healthDownShift, textureWidth, headshotHeight * 0.26f, true);
			// background panel for puma middle
			headshotY = overlayRect.y + overlayRect.height * 0.47f + yOffsetForAddingPopulationBar + barsDownShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			// display bars in puma middle
			DrawDisplayBars(1, headshotX, headshotY, headshotWidth, headshotHeight);
			// final ending label at very top
			if (scoringSystem.GetPumaHealth(1) <= 0f) {
				// puma is dead
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				style.normal.textColor = deadPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), "STARVED !", style);
			}
			else if (scoringSystem.GetPumaHealth(1) >= 1f) {
				// puma at full health
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				style.normal.textColor = fullHealthPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), "Full Health", style);
				float checkMarkWidth = overlayRect.width * 0.021f;
				float checkMarkHeight = greenCheckTexture.height * (checkMarkWidth / greenCheckTexture.width);
				float checkMarkX = textureX + textureWidth * 0.84f;
				float checkMarkY = overlayRect.y + overlayRect.height * 0.282f + yOffsetForAddingPopulationBar + textUpShift;
				GUI.color = new Color(1f, 1f, 1f, 0.8f * guiOpacity);
				GUI.DrawTexture(new Rect(checkMarkX, checkMarkY + endingLabelDownshift, checkMarkWidth, checkMarkHeight), greenCheckTexture);
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			}
		}
		// background panel for text or puma icon
		if (selectedPuma == 1) {
			GUI.color = new Color(0f, 0f, 0f, 0.4f * guiOpacity);
			//GUI.Box(new Rect(textureX + overlayRect.width * .015f, overlayRect.y + overlayRect.height * 0.73f + yOffsetForAddingPopulationBar, textureWidth - overlayRect.width * .03f, overlayRect.height * 0.065f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		}
		else {
			// not selected
			GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
			GUI.Box(new Rect(textureX + overlayRect.width * .0f, overlayRect.y + overlayRect.height * 0.585f + yOffsetForAddingPopulationBar + iconDownShift, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.14f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		}
		// puma icon
		GUI.color = new Color(1f, 1f, 1f, 0f * guiOpacity);
		if (GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.315f, textureWidth, overlayRect.height * 0.47f), "") && PumaIsSelectable(1)) {
			selectedPuma = 1;
			levelManager.SetSelectedPuma(1);
		}
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		if (selectedPuma == 1) {
			float selectedIconX = textureX + overlayRect.width * 0.024f;
			float selectedIconY = textureY + overlayRect.height * 0.018f;
			float selectedIconWidth = textureWidth * 0.74f;
			float selectedIconHeight = textureHeight * 0.73f;
			GUI.DrawTexture(new Rect(selectedIconX - (selectedIconWidth * 0.05f), selectedIconY - (selectedIconHeight * 0.055f) + iconDownShift, selectedIconWidth * 1.116f, selectedIconHeight * 1.116f), pumaIconShadowTexture);
			GUI.DrawTexture(new Rect(selectedIconX, selectedIconY + iconDownShift, selectedIconWidth, selectedIconHeight), pumaIconTexture);
		}
		else {
			if (scoringSystem.GetPumaHealth(1) <= 0f)
				GUI.color = deadPumaIconColor;
			else if (scoringSystem.GetPumaHealth(1) >= 1f)
				GUI.color = fullHealthPumaIconColor;
			else
				GUI.color = new Color(1f, 1f, 1f, 0.7f * guiOpacity);
			GUI.DrawTexture(new Rect(textureX + textureWidth * 0.05f, textureY + textureHeight * 0.12f + iconDownShift, textureWidth * 0.9f, textureHeight * 0.9f), pumaIconTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		}
		if (selectedPuma == 1)
			DrawSelectScreenDetailsPanel(style, textureX, textureWidth, headshot2Texture);
		// text
		if (selectedPuma == 1)
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
		else if (scoringSystem.GetPumaHealth(1) <= 0f)
			style.normal.textColor = deadPumaTextColor;
		else if (scoringSystem.GetPumaHealth(1) >= 1f)
			style.normal.textColor = fullHealthPumaTextColor;
		else
			style.normal.textColor = unselectedTextColor;
		style.fontSize = (int)(overlayRect.width * ((selectedPuma == 1) ? 0.021f : 0.019f));
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((selectedPuma == 1) ? 0.71f : 0.71f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "Palo", style);
		style.fontSize = (int)(overlayRect.width * ((selectedPuma == 1) ? 0.015f : 0.014f));
		//style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((selectedPuma == 1) ? 0.735f : 0.735f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "2 years - female", style);
		style.normal.textColor = Color.white;

		// adult male
		textureX += overlayRect.width * ((selectedPuma == 1) ? 0.18f : 0.135f);
		textureX += overlayRect.width * ((selectedPuma == 2) ? -0.002f : 0f);
		textureY = overlayRect.y + overlayRect.height * ((selectedPuma == 2) ? 0.572f : 0.585f) + yOffsetForAddingPopulationBar;
		textureWidth = overlayRect.width * ((selectedPuma == 2) ? 0.16f : 0.115f);
		textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
		if (selectedPuma != 2) {
			// background panel for puma head
			headshotY = overlayRect.y + overlayRect.height * 0.355f + yOffsetForAddingPopulationBar + headUpShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f + headshotBackgroundHeightAddition), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			// puma head
			headshotTexture = closeup3Texture;
			headshotHeight = overlayRect.height * 0.085f;
			headshotWidth = headshotTexture.width * (headshotHeight / headshotTexture.height);
			headshotX = textureX + (textureWidth - headshotWidth) * 0.5f;
			if (scoringSystem.GetPumaHealth(2) <= 0f)
				GUI.color = deadPumaHeadshotColor;
			else if (scoringSystem.GetPumaHealth(2) >= 1f)
				GUI.color = fullHealthPumaHeadshotColor;
			GUI.DrawTexture(new Rect(headshotX, headshotY, headshotWidth, headshotHeight), headshotTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			// health bar
			if (scoringSystem.GetPumaHealth(2) > 0f && scoringSystem.GetPumaHealth(2) < 1f)
				guiComponents.DrawPumaHealthBar(2, guiOpacity, textureX, headshotY - headshotHeight * 0.36f + healthDownShift, textureWidth, headshotHeight * 0.26f, true);
			// background panel for puma middle
			headshotY = overlayRect.y + overlayRect.height * 0.47f + yOffsetForAddingPopulationBar + barsDownShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			// display bars in puma middle
			DrawDisplayBars(2, headshotX, headshotY, headshotWidth, headshotHeight);
			// final ending label at very top
			if (scoringSystem.GetPumaHealth(2) <= 0f) {
				// puma is dead
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				style.normal.textColor = deadPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), "STARVED !", style);
			}
			else if (scoringSystem.GetPumaHealth(2) >= 1f) {
				// puma at full health
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				style.normal.textColor = fullHealthPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), "Full Health", style);
				float checkMarkWidth = overlayRect.width * 0.021f;
				float checkMarkHeight = greenCheckTexture.height * (checkMarkWidth / greenCheckTexture.width);
				float checkMarkX = textureX + textureWidth * 0.84f;
				float checkMarkY = overlayRect.y + overlayRect.height * 0.282f + yOffsetForAddingPopulationBar + textUpShift;
				GUI.color = new Color(1f, 1f, 1f, 0.8f * guiOpacity);
				GUI.DrawTexture(new Rect(checkMarkX, checkMarkY + endingLabelDownshift, checkMarkWidth, checkMarkHeight), greenCheckTexture);
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			}
		}
		// background panel for text or puma icon
		if (selectedPuma == 2) {
			GUI.color = new Color(0f, 0f, 0f, 0.4f * guiOpacity);
			//GUI.Box(new Rect(textureX + overlayRect.width * .015f, overlayRect.y + overlayRect.height * 0.73f + yOffsetForAddingPopulationBar, textureWidth - overlayRect.width * .03f, overlayRect.height * 0.065f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		}
		else {
			// not selected
			GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
			GUI.Box(new Rect(textureX + overlayRect.width * .0f, overlayRect.y + overlayRect.height * 0.585f + yOffsetForAddingPopulationBar + iconDownShift, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.14f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		}
		// puma icon
		GUI.color = new Color(1f, 1f, 1f, 0f * guiOpacity);
		if (GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.315f, textureWidth, overlayRect.height * 0.47f), "") && PumaIsSelectable(2)) {
			selectedPuma = 2;
			levelManager.SetSelectedPuma(2);
		}
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		if (selectedPuma == 2) {
			float selectedIconX = textureX + overlayRect.width * 0.024f;
			float selectedIconY = textureY + overlayRect.height * 0.018f;
			float selectedIconWidth = textureWidth * 0.74f;
			float selectedIconHeight = textureHeight * 0.73f;
			GUI.DrawTexture(new Rect(selectedIconX - (selectedIconWidth * 0.05f), selectedIconY - (selectedIconHeight * 0.055f) + iconDownShift, selectedIconWidth * 1.116f, selectedIconHeight * 1.116f), pumaIconShadowTexture);
			GUI.DrawTexture(new Rect(selectedIconX, selectedIconY + iconDownShift, selectedIconWidth, selectedIconHeight), pumaIconTexture);
		}
		else {
			if (scoringSystem.GetPumaHealth(2) <= 0f)
				GUI.color = deadPumaIconColor;
			else if (scoringSystem.GetPumaHealth(2) >= 1f)
				GUI.color = fullHealthPumaIconColor;
			else
				GUI.color = new Color(1f, 1f, 1f, 0.7f * guiOpacity);
			GUI.DrawTexture(new Rect(textureX + textureWidth * 0.05f, textureY + textureHeight * 0.12f + iconDownShift, textureWidth * 0.9f, textureHeight * 0.9f), pumaIconTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		}
		if (selectedPuma == 2)
			DrawSelectScreenDetailsPanel(style, textureX, textureWidth, headshot3Texture);
		// text
		if (selectedPuma == 2)
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
		else if (scoringSystem.GetPumaHealth(2) <= 0f)
			style.normal.textColor = deadPumaTextColor;
		else if (scoringSystem.GetPumaHealth(2) >= 1f)
			style.normal.textColor = fullHealthPumaTextColor;
		else
			style.normal.textColor = unselectedTextColor;
		style.fontSize = (int)(overlayRect.width * ((selectedPuma == 2) ? 0.021f : 0.019f));
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((selectedPuma == 2) ? 0.71f : 0.71f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "Mitch", style);
		style.fontSize = (int)(overlayRect.width * ((selectedPuma == 2) ? 0.015f : 0.014f));
		//style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((selectedPuma == 2) ? 0.735f : 0.735f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "5 years - male", style);
		style.normal.textColor = Color.white;

		// adult female
		textureX += overlayRect.width * ((selectedPuma == 2) ? 0.18f : 0.135f);
		textureX += overlayRect.width * ((selectedPuma == 3) ? -0.002f : 0f);
		textureY = overlayRect.y + overlayRect.height * ((selectedPuma == 3) ? 0.572f : 0.585f) + yOffsetForAddingPopulationBar;
		textureWidth = overlayRect.width * ((selectedPuma == 3) ? 0.16f : 0.115f);
		textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
		if (selectedPuma != 3) {
			// background panel for puma head
			headshotY = overlayRect.y + overlayRect.height * 0.355f + yOffsetForAddingPopulationBar + headUpShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f + headshotBackgroundHeightAddition), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			// puma head
			headshotTexture = closeup4Texture;
			headshotHeight = overlayRect.height * 0.085f;
			headshotWidth = headshotTexture.width * (headshotHeight / headshotTexture.height);
			headshotX = textureX + (textureWidth - headshotWidth) * 0.5f;
			if (scoringSystem.GetPumaHealth(3) <= 0f)
				GUI.color = deadPumaHeadshotColor;
			else if (scoringSystem.GetPumaHealth(3) >= 1f)
				GUI.color = fullHealthPumaHeadshotColor;
			GUI.DrawTexture(new Rect(headshotX, headshotY, headshotWidth, headshotHeight), headshotTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			// health bar
			if (scoringSystem.GetPumaHealth(3) > 0f && scoringSystem.GetPumaHealth(3) < 1f)
				guiComponents.DrawPumaHealthBar(3, guiOpacity, textureX, headshotY - headshotHeight * 0.36f + healthDownShift, textureWidth, headshotHeight * 0.26f, true);
			// background panel for puma middle
			headshotY = overlayRect.y + overlayRect.height * 0.47f + yOffsetForAddingPopulationBar + barsDownShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			// display bars in puma middle
			DrawDisplayBars(3, headshotX, headshotY, headshotWidth, headshotHeight);
			// final ending label at very top
			if (scoringSystem.GetPumaHealth(3) <= 0f) {
				// puma is dead
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				style.normal.textColor = deadPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), "STARVED !", style);
			}
			else if (scoringSystem.GetPumaHealth(3) >= 1f) {
				// puma at full health
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				style.normal.textColor = fullHealthPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), "Full Health", style);
				float checkMarkWidth = overlayRect.width * 0.021f;
				float checkMarkHeight = greenCheckTexture.height * (checkMarkWidth / greenCheckTexture.width);
				float checkMarkX = textureX + textureWidth * 0.84f;
				float checkMarkY = overlayRect.y + overlayRect.height * 0.282f + yOffsetForAddingPopulationBar + textUpShift;
				GUI.color = new Color(1f, 1f, 1f, 0.8f * guiOpacity);
				GUI.DrawTexture(new Rect(checkMarkX, checkMarkY + endingLabelDownshift, checkMarkWidth, checkMarkHeight), greenCheckTexture);
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			}
		}
		// background panel for text or puma icon
		if (selectedPuma == 3) {
			GUI.color = new Color(0f, 0f, 0f, 0.4f * guiOpacity);
			//GUI.Box(new Rect(textureX + overlayRect.width * .015f, overlayRect.y + overlayRect.height * 0.73f + yOffsetForAddingPopulationBar, textureWidth - overlayRect.width * .03f, overlayRect.height * 0.065f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		}
		else {
			// not selected
			GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
			GUI.Box(new Rect(textureX + overlayRect.width * .0f, overlayRect.y + overlayRect.height * 0.585f + yOffsetForAddingPopulationBar + iconDownShift, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.14f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		}
		// puma icon
		GUI.color = new Color(1f, 1f, 1f, 0f * guiOpacity);
		if (GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.315f, textureWidth, overlayRect.height * 0.47f), "") && PumaIsSelectable(3)) {
			selectedPuma = 3;
			levelManager.SetSelectedPuma(3);
		}
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		if (selectedPuma == 3) {
			float selectedIconX = textureX + overlayRect.width * 0.024f;
			float selectedIconY = textureY + overlayRect.height * 0.018f;
			float selectedIconWidth = textureWidth * 0.74f;
			float selectedIconHeight = textureHeight * 0.73f;
			GUI.DrawTexture(new Rect(selectedIconX - (selectedIconWidth * 0.05f), selectedIconY - (selectedIconHeight * 0.055f) + iconDownShift, selectedIconWidth * 1.116f, selectedIconHeight * 1.116f), pumaIconShadowTexture);
			GUI.DrawTexture(new Rect(selectedIconX, selectedIconY + iconDownShift, selectedIconWidth, selectedIconHeight), pumaIconTexture);
		}
		else {
			if (scoringSystem.GetPumaHealth(3) <= 0f)
				GUI.color = deadPumaIconColor;
			else if (scoringSystem.GetPumaHealth(3) >= 1f)
				GUI.color = fullHealthPumaIconColor;
			else
				GUI.color = new Color(1f, 1f, 1f, 0.7f * guiOpacity);
			GUI.DrawTexture(new Rect(textureX + textureWidth * 0.05f, textureY + textureHeight * 0.12f + iconDownShift, textureWidth * 0.9f, textureHeight * 0.9f), pumaIconTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		}
		if (selectedPuma == 3)
			DrawSelectScreenDetailsPanel(style, textureX, textureWidth, headshot4Texture);
		// text
		if (selectedPuma == 3)
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
		else if (scoringSystem.GetPumaHealth(3) <= 0f)
			style.normal.textColor = deadPumaTextColor;
		else if (scoringSystem.GetPumaHealth(3) >= 1f)
			style.normal.textColor = fullHealthPumaTextColor;
		else
			style.normal.textColor = unselectedTextColor;
		style.fontSize = (int)(overlayRect.width * ((selectedPuma == 3) ? 0.021f : 0.019f));
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((selectedPuma == 3) ? 0.71f : 0.71f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "Trish", style);
		style.fontSize = (int)(overlayRect.width * ((selectedPuma == 3) ? 0.015f : 0.014f));
		//style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((selectedPuma == 3) ? 0.735f : 0.735f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "5 years - female", style);
		style.normal.textColor = Color.white;

		// old male
		textureX += overlayRect.width * ((selectedPuma == 3) ? 0.18f : 0.135f);
		textureX += overlayRect.width * ((selectedPuma == 4) ? -0.002f : 0f);
		textureY = overlayRect.y + overlayRect.height * ((selectedPuma == 4) ? 0.572f : 0.585f) + yOffsetForAddingPopulationBar;
		textureWidth = overlayRect.width * ((selectedPuma == 4) ? 0.16f : 0.115f);
		textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
		if (selectedPuma != 4) {
			// background panel for puma head
			headshotY = overlayRect.y + overlayRect.height * 0.355f + yOffsetForAddingPopulationBar + headUpShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f + headshotBackgroundHeightAddition), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			// puma head
			headshotTexture = closeup5Texture;
			headshotHeight = overlayRect.height * 0.085f;
			headshotWidth = headshotTexture.width * (headshotHeight / headshotTexture.height);
			headshotX = textureX + (textureWidth - headshotWidth) * 0.5f;
			if (scoringSystem.GetPumaHealth(4) <= 0f)
				GUI.color = deadPumaHeadshotColor;
			else if (scoringSystem.GetPumaHealth(4) >= 1f)
				GUI.color = fullHealthPumaHeadshotColor;
			GUI.DrawTexture(new Rect(headshotX, headshotY, headshotWidth, headshotHeight), headshotTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			// health bar
			if (scoringSystem.GetPumaHealth(4) > 0f && scoringSystem.GetPumaHealth(4) < 1f)
				guiComponents.DrawPumaHealthBar(4, guiOpacity, textureX, headshotY - headshotHeight * 0.36f + healthDownShift, textureWidth, headshotHeight * 0.26f, true);
			// background panel for puma middle
			headshotY = overlayRect.y + overlayRect.height * 0.47f + yOffsetForAddingPopulationBar + barsDownShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			// display bars in puma middle
			DrawDisplayBars(4, headshotX, headshotY, headshotWidth, headshotHeight);
			// final ending label at very top
			if (scoringSystem.GetPumaHealth(4) <= 0f) {
				// puma is dead
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				style.normal.textColor = deadPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), "STARVED !", style);
			}
			else if (scoringSystem.GetPumaHealth(4) >= 1f) {
				// puma at full health
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				style.normal.textColor = fullHealthPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), "Full Health", style);
				float checkMarkWidth = overlayRect.width * 0.021f;
				float checkMarkHeight = greenCheckTexture.height * (checkMarkWidth / greenCheckTexture.width);
				float checkMarkX = textureX + textureWidth * 0.84f;
				float checkMarkY = overlayRect.y + overlayRect.height * 0.282f + yOffsetForAddingPopulationBar + textUpShift;
				GUI.color = new Color(1f, 1f, 1f, 0.8f * guiOpacity);
				GUI.DrawTexture(new Rect(checkMarkX, checkMarkY + endingLabelDownshift, checkMarkWidth, checkMarkHeight), greenCheckTexture);
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			}
		}
		// background panel for text or puma icon
		if (selectedPuma == 4) {
			GUI.color = new Color(0f, 0f, 0f, 0.4f * guiOpacity);
			//GUI.Box(new Rect(textureX + overlayRect.width * .015f, overlayRect.y + overlayRect.height * 0.73f + yOffsetForAddingPopulationBar, textureWidth - overlayRect.width * .03f, overlayRect.height * 0.065f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		}
		else {
			// not selected
			GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
			GUI.Box(new Rect(textureX + overlayRect.width * .0f, overlayRect.y + overlayRect.height * 0.585f + yOffsetForAddingPopulationBar + iconDownShift, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.14f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		}
		// puma icon
		GUI.color = new Color(1f, 1f, 1f, 0f * guiOpacity);
		if (GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.315f, textureWidth, overlayRect.height * 0.47f), "") && PumaIsSelectable(4)) {
			selectedPuma = 4;
			levelManager.SetSelectedPuma(4);
		}
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		if (selectedPuma == 4) {
			float selectedIconX = textureX + overlayRect.width * 0.024f;
			float selectedIconY = textureY + overlayRect.height * 0.018f;
			float selectedIconWidth = textureWidth * 0.74f;
			float selectedIconHeight = textureHeight * 0.73f;
			GUI.DrawTexture(new Rect(selectedIconX - (selectedIconWidth * 0.05f), selectedIconY - (selectedIconHeight * 0.055f) + iconDownShift, selectedIconWidth * 1.116f, selectedIconHeight * 1.116f), pumaIconShadowTexture);
			GUI.DrawTexture(new Rect(selectedIconX, selectedIconY + iconDownShift, selectedIconWidth, selectedIconHeight), pumaIconTexture);
		}
		else {
			if (scoringSystem.GetPumaHealth(4) <= 0f)
				GUI.color = deadPumaIconColor;
			else if (scoringSystem.GetPumaHealth(4) >= 1f)
				GUI.color = fullHealthPumaIconColor;
			else
				GUI.color = new Color(1f, 1f, 1f, 0.7f * guiOpacity);
			GUI.DrawTexture(new Rect(textureX + textureWidth * 0.05f, textureY + textureHeight * 0.12f + iconDownShift, textureWidth * 0.9f, textureHeight * 0.9f), pumaIconTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		}
		if (selectedPuma == 4)
			DrawSelectScreenDetailsPanel(style, textureX, textureWidth, headshot5Texture);
		// text
		if (selectedPuma == 4)
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
		else if (scoringSystem.GetPumaHealth(4) <= 0f)
			style.normal.textColor = deadPumaTextColor;
		else if (scoringSystem.GetPumaHealth(4) >= 1f)
			style.normal.textColor = fullHealthPumaTextColor;
		else
			style.normal.textColor = unselectedTextColor;
		style.fontSize = (int)(overlayRect.width * ((selectedPuma == 4) ? 0.021f : 0.019f));
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((selectedPuma == 4) ? 0.71f : 0.71f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "Liam", style);
		style.fontSize = (int)(overlayRect.width * ((selectedPuma == 4) ? 0.015f : 0.014f));
		//style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((selectedPuma == 4) ? 0.735f : 0.735f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "8 years - male", style);
		style.normal.textColor = Color.white;

		// old female
		textureX += overlayRect.width * ((selectedPuma == 4) ? 0.18f : 0.135f);
		textureX += overlayRect.width * ((selectedPuma == 5) ? -0.002f : 0f);
		textureY = overlayRect.y + overlayRect.height * ((selectedPuma == 5) ? 0.572f : 0.585f) + yOffsetForAddingPopulationBar;
		textureWidth = overlayRect.width * ((selectedPuma == 5) ? 0.16f : 0.115f);
		textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
		if (selectedPuma != 5) {
			// background panel for puma head
			headshotY = overlayRect.y + overlayRect.height * 0.355f + yOffsetForAddingPopulationBar + headUpShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f + headshotBackgroundHeightAddition), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			// puma head
			headshotTexture = closeup6Texture;
			headshotHeight = overlayRect.height * 0.085f;
			headshotWidth = headshotTexture.width * (headshotHeight / headshotTexture.height);
			headshotX = textureX + (textureWidth - headshotWidth) * 0.5f;
			if (scoringSystem.GetPumaHealth(5) <= 0f)
				GUI.color = deadPumaHeadshotColor;
			else if (scoringSystem.GetPumaHealth(5) >= 1f)
				GUI.color = fullHealthPumaHeadshotColor;
			GUI.DrawTexture(new Rect(headshotX, headshotY, headshotWidth, headshotHeight), headshotTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			// health bar
			if (scoringSystem.GetPumaHealth(5) > 0f && scoringSystem.GetPumaHealth(5) < 1f)
				guiComponents.DrawPumaHealthBar(5, guiOpacity, textureX, headshotY - headshotHeight * 0.36f + healthDownShift, textureWidth, headshotHeight * 0.26f, true);
			// background panel for puma middle
			headshotY = overlayRect.y + overlayRect.height * 0.47f + yOffsetForAddingPopulationBar + barsDownShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			// display bars in puma middle
			DrawDisplayBars(5, headshotX, headshotY, headshotWidth, headshotHeight);
			// final ending label at very top
			if (scoringSystem.GetPumaHealth(5) <= 0f) {
				// puma is dead
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				style.normal.textColor = deadPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), "STARVED !", style);
			}
			else if (scoringSystem.GetPumaHealth(5) >= 1f) {
				// puma at full health
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				style.normal.textColor = fullHealthPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), "Full Health", style);
				float checkMarkWidth = overlayRect.width * 0.021f;
				float checkMarkHeight = greenCheckTexture.height * (checkMarkWidth / greenCheckTexture.width);
				float checkMarkX = textureX + textureWidth * 0.84f;
				float checkMarkY = overlayRect.y + overlayRect.height * 0.282f + yOffsetForAddingPopulationBar + textUpShift;
				GUI.color = new Color(1f, 1f, 1f, 0.8f * guiOpacity);
				GUI.DrawTexture(new Rect(checkMarkX, checkMarkY + endingLabelDownshift, checkMarkWidth, checkMarkHeight), greenCheckTexture);
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			}
		}
		// background panel for text or puma icon
		if (selectedPuma == 5) {
			GUI.color = new Color(0f, 0f, 0f, 0.4f * guiOpacity);
			//GUI.Box(new Rect(textureX + overlayRect.width * .015f, overlayRect.y + overlayRect.height * 0.73f + yOffsetForAddingPopulationBar, textureWidth - overlayRect.width * .03f, overlayRect.height * 0.065f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		}
		else {
			// not selected
			GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
			GUI.Box(new Rect(textureX + overlayRect.width * .0f, overlayRect.y + overlayRect.height * 0.585f + yOffsetForAddingPopulationBar + iconDownShift, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.14f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		}
		// puma icon
		GUI.color = new Color(1f, 1f, 1f, 0f * guiOpacity);
		if (GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.315f, textureWidth, overlayRect.height * 0.47f), "") && PumaIsSelectable(5)) {
			selectedPuma = 5;
			levelManager.SetSelectedPuma(5);
		}
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		if (selectedPuma == 5) {
			float selectedIconX = textureX + overlayRect.width * 0.024f;
			float selectedIconY = textureY + overlayRect.height * 0.018f;
			float selectedIconWidth = textureWidth * 0.74f;
			float selectedIconHeight = textureHeight * 0.73f;
			GUI.DrawTexture(new Rect(selectedIconX - (selectedIconWidth * 0.05f), selectedIconY - (selectedIconHeight * 0.055f) + iconDownShift, selectedIconWidth * 1.116f, selectedIconHeight * 1.116f), pumaIconShadowTexture);
			GUI.DrawTexture(new Rect(selectedIconX, selectedIconY + iconDownShift, selectedIconWidth, selectedIconHeight), pumaIconTexture);
		}
		else {
			if (scoringSystem.GetPumaHealth(5) <= 0f)
				GUI.color = deadPumaIconColor;
			else if (scoringSystem.GetPumaHealth(5) >= 1f)
				GUI.color = fullHealthPumaIconColor;
			else
				GUI.color = new Color(1f, 1f, 1f, 0.7f * guiOpacity);
			GUI.DrawTexture(new Rect(textureX + textureWidth * 0.05f, textureY + textureHeight * 0.12f + iconDownShift, textureWidth * 0.9f, textureHeight * 0.9f), pumaIconTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		}
		if (selectedPuma == 5)
			DrawSelectScreenDetailsPanel(style, textureX, textureWidth, headshot6Texture);
		// text
		if (selectedPuma == 5)
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
		else if (scoringSystem.GetPumaHealth(5) <= 0f)
			style.normal.textColor = deadPumaTextColor;
		else if (scoringSystem.GetPumaHealth(5) >= 1f)
			style.normal.textColor = fullHealthPumaTextColor;
		else
			style.normal.textColor = unselectedTextColor;
		style.fontSize = (int)(overlayRect.width * ((selectedPuma == 5) ? 0.021f : 0.019f));
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((selectedPuma == 5) ? 0.71f : 0.71f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "Barb", style);
		style.fontSize = (int)(overlayRect.width * ((selectedPuma == 5) ? 0.015f : 0.014f));
		//style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((selectedPuma == 5) ? 0.735f : 0.735f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "8 years - female", style);
		style.normal.textColor = Color.white;
		
	}	
	

	void DrawSelectScreenDetailsPanel(GUIStyle style, float textureX, float textureWidth, Texture2D headshotTexture) 
	{ 
		float headshotY = overlayRect.y + overlayRect.height * 0.23f;
		float headshotHeight = overlayRect.height * 0.17f;
		float headshotWidth = headshotTexture.width * (headshotHeight / headshotTexture.height);
		float headshotX = textureX + (0.75f * (textureWidth - headshotWidth));

		float detailsPanelX = headshotX - overlayRect.width * 0.01f;
		float detailsPanelY = headshotY - overlayRect.height * 0.015f;
		float detailsPanelWidth = headshotWidth + overlayRect.width * 0.02f;
		float detailsPanelHeight = headshotHeight + overlayRect.height * 0.18f;
		
		
		float upperPanelShrinkFactor = 0.05f;
		

		// used in DrawSelectScreen() and DrawSelectScreenDetailsPanel()
		float headUpShift = overlayRect.height * -0.03f;
		float textUpShift = overlayRect.height * -0.303f;
		float barsDownShift = overlayRect.height * 0.06f;
		float healthDownShift = overlayRect.height * 0.37f;
		float headshotBackgroundHeightAddition = overlayRect.height * 0.06f;

		headUpShift = 0f;
		healthDownShift = 0f;

		if (modifyLayoutFlag == false) {
			textUpShift = 0f;
			barsDownShift = 0f;
			headshotBackgroundHeightAddition = 0f;
		}

		// background panel
		GUI.color = new Color(0f, 0f, 0f, 1f * guiOpacity);
		GUI.Box(new Rect(detailsPanelX + detailsPanelWidth * 0.03f, detailsPanelY + (detailsPanelHeight * 0.113f) + headUpShift, detailsPanelWidth - detailsPanelWidth * 0.06f, detailsPanelHeight * 0.526f + headshotBackgroundHeightAddition), "");
		GUI.Box(new Rect(headshotX, detailsPanelY + detailsPanelHeight * 0.68f + barsDownShift, headshotWidth, detailsPanelHeight * 0.285f), "");
		if (headshotTexture != headshot1Texture && headshotTexture != headshot2Texture && headshotTexture != headshot3Texture) {
			GUI.color = new Color(0f, 0f, 0f, 1f * guiOpacity);
			GUI.Box(new Rect(detailsPanelX + detailsPanelWidth * 0.03f, detailsPanelY + (detailsPanelHeight * 0.113f) + headUpShift, detailsPanelWidth - detailsPanelWidth * 0.06f, detailsPanelHeight * 0.526f + headshotBackgroundHeightAddition), "");
			GUI.Box(new Rect(headshotX, detailsPanelY + detailsPanelHeight * 0.68f + barsDownShift, headshotWidth, detailsPanelHeight * 0.285f), "");
		}
		//GUI.color = new Color(1f, 1f, 1f, 0.8f);
		//GUI.Box(new Rect(detailsPanelX, detailsPanelY, detailsPanelWidth, detailsPanelHeight * 0.85f), "");

		float origHeadshotYPos = headshotY + (headshotHeight * upperPanelShrinkFactor) - headshotHeight * 0.03f;
		float origHealthbarYPos = headshotY + headshotHeight + headshotHeight * 0.025f;
		
		// puma closeup headshot
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		GUI.DrawTexture(new Rect(headshotX + (headshotWidth * upperPanelShrinkFactor * 0.5f), origHeadshotYPos + headshotHeight * 0.19f + headUpShift, headshotWidth * (1f - upperPanelShrinkFactor), headshotHeight * (1f - upperPanelShrinkFactor)), headshotTexture);

		// health bar
		GUI.color = new Color(0f, 0f, 0f, 1f * guiOpacity);
		GUI.Box(new Rect(detailsPanelX + detailsPanelWidth * 0.03f, origHeadshotYPos - headshotHeight * 0.091f + healthDownShift, detailsPanelWidth - detailsPanelWidth * 0.06f, headshotHeight * 0.17f), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		guiComponents.DrawPumaHealthBar(selectedPuma, guiOpacity, detailsPanelX + detailsPanelWidth * 0.03f, origHeadshotYPos - headshotHeight * 0.091f + healthDownShift, detailsPanelWidth - detailsPanelWidth * 0.06f, headshotHeight * 0.17f);

		
		float displayBarsRightShift = headshotWidth * 0.018f;
		
		// list of characteristics
		float yOffset = headshotHeight * 0.31f + barsDownShift;
		style.normal.textColor = new Color(0.88f, 0.82f, 0.5f, 0.7f); //new Color(0.9f, 0.58f, 0f, 1f);
		style.fontSize = (int)(overlayRect.width * 0.0135f);
		style.alignment = TextAnchor.UpperRight;
		style.fontStyle = FontStyle.Bold;
		GUI.Button(new Rect(headshotX - overlayRect.width * 0.007f + headshotWidth * 0.30f + displayBarsRightShift, yOffset + headshotY + headshotHeight + overlayRect.height * 0.006f, headshotWidth * 0.22f, headshotHeight), "Speed", style);
		GUI.Button(new Rect(headshotX - overlayRect.width * 0.007f + headshotWidth * 0.30f + displayBarsRightShift, yOffset + headshotY + headshotHeight + overlayRect.height * 0.028f, headshotWidth * 0.22f, headshotHeight), "Stealth", style);
		GUI.Button(new Rect(headshotX - overlayRect.width * 0.007f + displayBarsRightShift, yOffset + headshotY + headshotHeight + overlayRect.height * 0.050f, headshotWidth * 0.52f, headshotHeight), "Endurance", style);
		GUI.Button(new Rect(headshotX - overlayRect.width * 0.007f + displayBarsRightShift, yOffset + headshotY + headshotHeight + overlayRect.height * 0.071f, headshotWidth * 0.52f, headshotHeight), "Experience", style);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.MiddleCenter;

		// display bars for characteristics



/*
		float yOffset = overlayRect.height * -0.085f;
		float displayBarsRightShift = overlayRect.height * -0.1265f;
		
		refWidth = refHeight * 3f;
*/


		DrawDisplayBars(selectedPuma, headshotX + displayBarsRightShift - overlayRect.height * -0.1265f, headshotY + overlayRect.height * 0.2295f + barsDownShift, headshotWidth, headshotWidth / 3f);


		
		// link to learn more page
/*		
		// button
		GUI.color = new Color(0.0f, 0.0f, 0.0f, 0.8f * guiOpacity);
		//DrawRect(new Rect(detailsPanelX + detailsPanelWidth * 0.15f - detailsPanelWidth * .005f, detailsPanelY + detailsPanelHeight * 0.87f - detailsPanelWidth * .005f, detailsPanelWidth * 0.7f + detailsPanelWidth * .01f, detailsPanelHeight * 0.097f + detailsPanelWidth * .01f), new Color(1f, 1f, 1f, 1f));	
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		GUI.skin = customGUISkin;
		customGUISkin.button.fontSize = buttonStyle.fontSize = buttonDownStyle.fontSize = (int)(overlayRect.width * 0.016);
		customGUISkin.button.fontStyle = FontStyle.Bold;
		GUI.backgroundColor = new Color(1f, 1f, 1f, 0.8f * guiOpacity);
		if (GUI.Button(new Rect(headshotX + overlayRect.width * 0.015f - detailsPanelWidth * 0.033f, yOffset + headshotY + headshotHeight + overlayRect.height * 0.008f, detailsPanelWidth * 0.15f, headshotHeight * 0.2f), "")) {
			infoPanelVisible = true;
			infoPanelPage = 1;
			infoPanelTransStart = Time.time;
		}
		if (GUI.Button(new Rect(headshotX + overlayRect.width * 0.015f - detailsPanelWidth * 0.033f, yOffset + headshotY + headshotHeight + overlayRect.height * 0.008f, detailsPanelWidth * 0.15f, headshotHeight * 0.2f), "")) {
			infoPanelVisible = true;
			infoPanelPage = 1;
			infoPanelTransStart = Time.time;
		}
		GUI.color = new Color(1f, 1f, 1f, 0.8f * guiOpacity);
		if (GUI.Button(new Rect(headshotX + overlayRect.width * 0.015f - detailsPanelWidth * 0.033f, yOffset + headshotY + headshotHeight + overlayRect.height * 0.008f, detailsPanelWidth * 0.15f, headshotHeight * 0.2f), "?")) {
			infoPanelVisible = true;
			infoPanelPage = 1;
			infoPanelTransStart = Time.time;
		}
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		GUI.backgroundColor = new Color(1f, 1f, 1f, 1f * guiOpacity);
*/		

	}
	
	
	//======================================================================
	//======================================================================
	//======================================================================
	//								OPTIONS
	//======================================================================
	//======================================================================
	//======================================================================
	
	void DrawOptionsScreen() 
	{ 
		float optionsScreenX = overlayRect.x + overlayRect.width * 0.06f;
		float optionsScreenY = overlayRect.y + overlayRect.height * 0.205f;
		float optionsScreenWidth = overlayRect.width * 0.88f;
		float optionsScreenHeight = overlayRect.height * 0.578f;
		float fontScale = 0.8f;

		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;

		// background rectangle
		GUI.color = new Color(0f, 0f, 0f, 1f * guiOpacity);
		GUI.Box(new Rect(optionsScreenX - overlayRect.width * 0.02f, optionsScreenY - overlayRect.height * 0.025f, optionsScreenWidth + overlayRect.width * 0.04f, optionsScreenHeight + overlayRect.height * 0.05f), "");
		GUI.color = new Color(0f, 0f, 0f, 0.5f * guiOpacity);
		GUI.Box(new Rect(optionsScreenX, optionsScreenY, optionsScreenWidth, optionsScreenHeight), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);


		// add population bar
		
		float yOffsetForAddingPopulationBar = overlayRect.height * -0.012f;
		float actualGuiOpacity = guiOpacity;
		guiOpacity = guiOpacity * 0.9f;
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);		
		
		float healthBarX = overlayRect.x + overlayRect.width * 0.04f;
		float healthBarY = overlayRect.y + overlayRect.height * 0.844f + yOffsetForAddingPopulationBar;
		float healthBarWidth = overlayRect.width * 0.92f;
		float healthBarHeight = overlayRect.height * 0.048f;	
		float healthBarLabelWidth = healthBarWidth * 0.13f;

		GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
		GUI.Box(new Rect(healthBarX, healthBarY, healthBarLabelWidth * 0.985f, healthBarHeight), "");
		GUI.Box(new Rect(healthBarX, healthBarY, healthBarLabelWidth * 0.985f, healthBarHeight), "");
		GUI.Box(new Rect(healthBarX + healthBarWidth - healthBarLabelWidth * 0.985f, healthBarY, healthBarLabelWidth * 0.985f, healthBarHeight), "");
		GUI.Box(new Rect(healthBarX + healthBarWidth - healthBarLabelWidth * 0.985f, healthBarY, healthBarLabelWidth * 0.985f, healthBarHeight), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);

		guiComponents.DrawPopulationHealthBar(guiOpacity, healthBarX + healthBarLabelWidth, healthBarY, healthBarWidth - healthBarLabelWidth * 2f, healthBarHeight, false, false);

		guiOpacity = actualGuiOpacity;
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);		




		return;


		float textureX;
		float textureY;
		float textureWidth;
		float textureHeight;
		
		float titleX = optionsScreenX + overlayRect.width * 0.027f;
		float titleY = optionsScreenY + overlayRect.height * 0.04f;

		style.alignment = TextAnchor.UpperCenter;
		style.fontStyle = FontStyle.BoldAndItalic;

		// background rectangle
		GUI.color = new Color(0f, 0f, 0f, 1f);
		GUI.Box(new Rect(optionsScreenX - overlayRect.width * 0.035f, optionsScreenY - overlayRect.height * 0.035f, optionsScreenWidth + overlayRect.width * 0.07f, optionsScreenHeight + overlayRect.height * 0.07f), "");
		GUI.color = new Color(0f, 0f, 0f, 0.5f);
		GUI.Box(new Rect(optionsScreenX, optionsScreenY, optionsScreenWidth, optionsScreenHeight), "");
		GUI.color = new Color(0f, 0f, 0f, 1f);
		GUI.color = new Color(1f, 1f, 1f, 1f);
		DrawRect(new Rect(optionsScreenX, optionsScreenY, optionsScreenWidth, optionsScreenHeight), new Color(1f, 1f, 1f, 0.665f));	

		// ======================================
		
		// DIFFICULTY

		// title
		style.fontSize = (int)(overlayRect.width * 0.024f);
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		style.alignment = TextAnchor.UpperLeft;
		GUI.Button(new Rect(titleX, titleY, overlayRect.width * 0.16f, overlayRect.height * 0.03f), "Challenge Level", style);
		style.alignment = TextAnchor.UpperCenter;

		// radio buttons and labels
		GUI.color = new Color(1f, 1f, 1f, 1f);
		style.fontSize = (int)(overlayRect.width * 0.025f);
		style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);
		style.alignment = TextAnchor.MiddleLeft;
		textureX = overlayRect.x + overlayRect.width * 0.44f;
		textureY = overlayRect.y + overlayRect.height * 0.264f;
		textureWidth = overlayRect.width * 0.026f;
		textureHeight = radioButtonTexture.height * (textureWidth / radioButtonTexture.width);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), radioButtonTexture);
		if (difficultyLevel == 0) {
			float selectTextureX = textureX + textureWidth * 0.165f;
			float selectTextureY = textureY + textureHeight * 0.165f;
			float selectTextureWidth = textureWidth * 0.65f;
			float selectTextureHeight = radioSelectTexture.height * (selectTextureWidth / radioSelectTexture.width);
			GUI.DrawTexture(new Rect(selectTextureX, selectTextureY, selectTextureWidth, selectTextureHeight), radioSelectTexture);
		}
		if (GUI.Button(new Rect(textureX, textureY, textureWidth * 3, textureHeight), "", style))
			difficultyLevel = 0;
		if (GUI.Button(new Rect(textureX + textureWidth, textureY, textureWidth * 3, textureHeight), " easy", style))
			difficultyLevel = 0;
		textureX += overlayRect.width * 0.135f;
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), radioButtonTexture);
		if (difficultyLevel == 1) {
			float selectTextureX = textureX + textureWidth * 0.165f;
			float selectTextureY = textureY + textureHeight * 0.165f;
			float selectTextureWidth = textureWidth * 0.65f;
			float selectTextureHeight = radioSelectTexture.height * (selectTextureWidth / radioSelectTexture.width);
			GUI.DrawTexture(new Rect(selectTextureX, selectTextureY, selectTextureWidth, selectTextureHeight), radioSelectTexture);
		}
		if (GUI.Button(new Rect(textureX, textureY, textureWidth * 3, textureHeight), "", style))
			difficultyLevel = 1;
		if (GUI.Button(new Rect(textureX + textureWidth, textureY, textureWidth * 3, textureHeight), " mid", style))
			difficultyLevel = 1;
		textureX += overlayRect.width * 0.125f;
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), radioButtonTexture);
		if (difficultyLevel == 2) {
			float selectTextureX = textureX + textureWidth * 0.165f;
			float selectTextureY = textureY + textureHeight * 0.165f;
			float selectTextureWidth = textureWidth * 0.65f;
			float selectTextureHeight = radioSelectTexture.height * (selectTextureWidth / radioSelectTexture.width);
			GUI.DrawTexture(new Rect(selectTextureX, selectTextureY, selectTextureWidth, selectTextureHeight), radioSelectTexture);
		}
		if (GUI.Button(new Rect(textureX, textureY, textureWidth * 3, textureHeight), "", style))
			difficultyLevel = 2;
		if (GUI.Button(new Rect(textureX + textureWidth, textureY, textureWidth * 3, textureHeight), " hard", style))
			difficultyLevel = 2;

		// ======================================	
		// DIVIDER
		DrawRect(new Rect(optionsScreenX, optionsScreenY + overlayRect.height * 0.113f, optionsScreenWidth, overlayRect.height * 0.005f), new Color(0f, 0f, 0f, 0.2f));	
		DrawRect(new Rect(optionsScreenX, optionsScreenY + overlayRect.height * 0.118f, optionsScreenWidth, overlayRect.height * 0.005f), new Color(1f, 1f, 1f, 0.5f));	
		// ======================================

		// SOUND VOLUME

		// title
		style.fontSize = (int)(overlayRect.width * 0.024f);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		style.alignment = TextAnchor.UpperLeft;
		GUI.Button(new Rect(titleX, titleY + overlayRect.height * 0.113f, overlayRect.width * 0.12f, overlayRect.height * 0.03f), "Sound Volume", style);
		style.alignment = TextAnchor.UpperCenter;

		// radio buttons and labels
		GUI.color = new Color(1f, 1f, 1f, 1f);
		style.fontSize = (int)((soundEnable == 1) ? overlayRect.width * 0.0235f : overlayRect.width * 0.028f );
		style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);
		style.alignment = TextAnchor.MiddleLeft;
		textureX = overlayRect.x + overlayRect.width * 0.44f;
		textureY = overlayRect.y + overlayRect.height * 0.377f;
		textureWidth = overlayRect.width * 0.026f;
		textureHeight = radioButtonTexture.height * (textureWidth / radioButtonTexture.width);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), radioButtonTexture);
		if (soundEnable == 1) {
			float selectTextureX = textureX + textureWidth * 0.165f;
			float selectTextureY = textureY + textureHeight * 0.165f;
			float selectTextureWidth = textureWidth * 0.65f;
			float selectTextureHeight = radioSelectTexture.height * (selectTextureWidth / radioSelectTexture.width);
			GUI.DrawTexture(new Rect(selectTextureX, selectTextureY, selectTextureWidth, selectTextureHeight), radioSelectTexture);
		}
		if (GUI.Button(new Rect(textureX, textureY, textureWidth * 2, textureHeight), "", style))
			soundEnable = (soundEnable == 1) ? 0 : 1;
		if (GUI.Button(new Rect(textureX + textureWidth, textureY, textureWidth * 2, textureHeight), (soundEnable == 1) ? " ON" : " off", style))
			soundEnable = (soundEnable == 1) ? 0 : 1;
		

		///////////////////////////////////////////////////////////////////
		// background rectangle -- FOR SCREEN CONFIG -- needs to go here to avoid problems after slider changes GUI.skin
		///////////////////////////////////////////////////////////////////
		float meterBoxX = optionsScreenX + ((pawRightFlag == 1) ? (optionsScreenWidth * 0.445f) : (optionsScreenWidth * 0.800f));
		float meterBoxY = optionsScreenY + optionsScreenHeight * 0.46f;
		float meterBoxWidth = optionsScreenWidth * 0.1f;
		float meterBoxHeight = optionsScreenHeight * 0.10f;
		GUI.color = new Color(0f, 0f, 0f, 0.8f);
		GUI.Box(new Rect(meterBoxX,  meterBoxY, meterBoxWidth, meterBoxHeight), "");
		//GUI.color = new Color(0f, 0f, 0f, 0.3f);
		//GUI.Box(new Rect(meterBoxX,  meterBoxY, meterBoxWidth, meterBoxHeight), "");
		GUI.color = new Color(1f, 1f, 1f, 1f);


		// volume slider
		float sliderX = overlayRect.x + overlayRect.width * 0.564f;
		float sliderY = overlayRect.y + overlayRect.height * 0.367f;
		float sliderWidth = overlayRect.width * 0.22f;
		float sliderHeight = overlayRect.height * 0.056f;
		//GUI.color = new Color(1f, 1f, 1f, soundEnable == 1 ? 1f : 0.6f);
		GUI.color = new Color(1f, 1f, 1f, soundEnable == 1 ? 1f : 0f);
		GUI.DrawTexture(new Rect(sliderX, sliderY + overlayRect.height * 0.0265f, sliderWidth, overlayRect.height * 0.01f), sliderBarTexture);
		GUI.skin = customSkin;		
		sliderThumbStyle.fixedWidth = overlayRect.width * 0.020f;
		//GUI.color = new Color(1f, 1f, 1f, soundEnable == 1 ? 1f : 0.35f);
		GUI.color = new Color(1f, 1f, 1f, soundEnable == 1 ? 1f : 0f);
		soundVolume = GUI.HorizontalSlider(new Rect(sliderX, sliderY, sliderWidth, sliderHeight), soundVolume, 0f, 1f);
		GUI.color = new Color(1f, 1f, 1f, 1f);
		

		// ======================================	
		// DIVIDER
		DrawRect(new Rect(optionsScreenX, optionsScreenY + overlayRect.height * 0.226f, optionsScreenWidth, overlayRect.height * 0.005f), new Color(0f, 0f, 0f, 0.2f));	
		DrawRect(new Rect(optionsScreenX, optionsScreenY + overlayRect.height * 0.231f, optionsScreenWidth, overlayRect.height * 0.005f), new Color(1f, 1f, 1f, 0.5f));	
		// ======================================

		// SCREEN CONFIG

		// title
		style.fontSize = (int)(overlayRect.width * 0.024f);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		style.alignment = TextAnchor.UpperLeft;
		GUI.Button(new Rect(titleX, titleY + overlayRect.height * 0.113f * 2f, overlayRect.width * 0.17f, overlayRect.height * 0.03f), "Screen Layout", style);
		style.alignment = TextAnchor.UpperCenter;

		// button
		GUI.skin = customGUISkin;
		customGUISkin.button.fontSize = (int)(overlayRect.width * 0.0165);
		customGUISkin.button.fontStyle = FontStyle.Normal;
		GUI.color = new Color(1f, 1f, 1f, 1f);
		GUI.backgroundColor = new Color(1f, 0f, 0f, 1f);
		if (GUI.Button(new Rect(overlayRect.width * -0.003f + optionsScreenX + overlayRect.width * 0.11f + overlayRect.width * 0.302f, overlayRect.width * -0.0025f + titleY + overlayRect.height * 0.113f * 2f - overlayRect.height * 0.006f, overlayRect.width * 0.006f + overlayRect.width * 0.06f, overlayRect.width * 0.005f + overlayRect.height * 0.0585f), ""))
			pawRightFlag = (pawRightFlag == 0) ? 1 : 0;
		GUI.backgroundColor = new Color(0.5f, 0.5f, 0.9f, 1f);
		if (GUI.Button(new Rect(optionsScreenX + overlayRect.width * 0.11f + overlayRect.width * 0.302f, titleY + overlayRect.height * 0.113f * 2f - overlayRect.height * 0.006f, overlayRect.width * 0.06f, overlayRect.height * 0.0585f), ""))
			pawRightFlag = (pawRightFlag == 0) ? 1 : 0;
		if (GUI.Button(new Rect(optionsScreenX + overlayRect.width * 0.11f + overlayRect.width * 0.302f, titleY + overlayRect.height * 0.113f * 2f - overlayRect.height * 0.006f, overlayRect.width * 0.06f, overlayRect.height * 0.0585f), ""))
			pawRightFlag = (pawRightFlag == 0) ? 1 : 0;
		GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
		GUI.color = new Color(1f, 1f, 1f, 1f);
		buttonStyle.fontSize = buttonDownStyle.fontSize = (int)(overlayRect.width * 0.019);
		if (GUI.Button(new Rect(titleX + overlayRect.width * 0.395f, titleY + overlayRect.height * 0.113f * 2f - overlayRect.height * 0.00f, overlayRect.width * 0.04f, overlayRect.height * 0.048f), "", swapButtonStyle))
			pawRightFlag = (pawRightFlag == 0) ? 1 : 0;
		GUI.color = new Color(1f, 1f, 1f, 1f);

		// arrow tray
		float trayX = optionsScreenX + ((pawRightFlag == 1) ? (optionsScreenWidth * 0.797f) : (optionsScreenWidth * 0.450f));
		float trayWidth = optionsScreenWidth * 0.097f;
		float trayHeight = arrowTrayTopTexture.height * (trayWidth / arrowTrayTopTexture.width);
		float trayY = optionsScreenY + optionsScreenHeight * 0.58f - trayHeight;
		GUI.color = new Color(1f, 1f, 1f, 0.75f);
		GUI.DrawTexture(new Rect(trayX, trayY, trayWidth, trayHeight), arrowTrayTopTexture);
		GUI.color = new Color(1f, 1f, 1f, 0.75f);
		GUI.DrawTexture(new Rect(trayX, trayY, trayWidth, trayHeight), arrowTrayTexture);
			
		// health meter...
		// background
		// PLACED ABOVE TO AVOID PROBLEMS FROM GUI.Skin BEING CHANGED BY SLIDER
		
		// filler
		GUI.color = new Color(1f, 1f, 1f, 1f);
		float health = 0.8f; 
		Color healthColor = (health > 0.66f) ? new Color(0f, 1f, 0f, 0.7f) : ((health > 0.33f) ? new Color(1f, 1f, 0f, 0.81f) : new Color(1f, 0f, 0f, 1f));
		DrawRect(new Rect(meterBoxX + meterBoxWidth * 0.05f,  meterBoxY + meterBoxHeight * 0.1f, meterBoxWidth * 0.9f, meterBoxHeight * 0.25f), new Color(0.61f, 0.64f, 0.66f, 1f));	
		DrawRect(new Rect(meterBoxX + meterBoxWidth * 0.07f,  meterBoxY + meterBoxHeight * 0.11f, (meterBoxWidth * 0.85f) * (health / 1.0f), meterBoxHeight * 0.23f), healthColor);			
		// 'pause' button
		GUI.color = new Color(1f, 1f, 1f, 0.75f);
		GUI.skin = customGUISkin;
		customGUISkin.button.fontSize = (int)(overlayRect.width * 0.009);;
		GUI.Button(new Rect(meterBoxX + meterBoxWidth * 0.05f,  meterBoxY + meterBoxHeight * 0.47f, meterBoxWidth * 0.9f, meterBoxHeight * 0.41f), "EXIT");
		GUI.color = new Color(1f, 1f, 1f, 1f);


		// ======================================	
		// DIVIDER
		DrawRect(new Rect(optionsScreenX, optionsScreenY + overlayRect.height * 0.339f, optionsScreenWidth, overlayRect.height * 0.005f), new Color(0f, 0f, 0f, 0.2f));	
		DrawRect(new Rect(optionsScreenX, optionsScreenY + overlayRect.height * 0.344f, optionsScreenWidth, overlayRect.height * 0.005f), new Color(1f, 1f, 1f, 0.5f));	
		// ======================================

		// INTRO VIDEO

		// title
		style.fontSize = (int)(overlayRect.width * 0.024f);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		style.alignment = TextAnchor.UpperLeft;
		GUI.Button(new Rect(titleX, titleY + overlayRect.height * 0.113f * 3f, overlayRect.width * 0.17f, overlayRect.height * 0.03f), "Video Options", style);
		style.alignment = TextAnchor.UpperCenter;

		// button
		//GUI.color = new Color(1f, 1f, 1f, 0.5f);
		//DrawRect(new Rect(optionsScreenX + overlayRect.width * 0.025f + overlayRect.width * 0.292f, titleY + overlayRect.height * 0.113f * 3f - overlayRect.height * 0.006f, overlayRect.width * 0.25f, overlayRect.height * 0.0585f), new Color(0f, 0f, 0f, 1f));	
		GUI.skin = customGUISkin;
		customGUISkin.button.fontSize = (int)(overlayRect.width * 0.0185);
		customGUISkin.button.fontStyle = FontStyle.Normal;
		GUI.color = new Color(1f, 1f, 1f, 1f);
		GUI.backgroundColor = new Color(1f, 0f, 0f, 1f);
		if (GUI.Button(new Rect(overlayRect.width * -0.003f + optionsScreenX + overlayRect.width * 0.025f + overlayRect.width * 0.292f, overlayRect.width * -0.0025f + titleY + overlayRect.height * 0.113f * 3f - overlayRect.height * 0.006f, overlayRect.width * 0.006f + overlayRect.width * 0.25f, overlayRect.width * 0.005f + overlayRect.height * 0.0585f), "")) {
			infoPanelVisible = true;
			infoPanelPage = 0;
			infoPanelTransStart = Time.time;
		}
		GUI.backgroundColor = new Color(0.5f, 0.5f, 0.9f, 1f);
		if (GUI.Button(new Rect(optionsScreenX + overlayRect.width * 0.025f + overlayRect.width * 0.292f, titleY + overlayRect.height * 0.113f * 3f - overlayRect.height * 0.006f, overlayRect.width * 0.25f, overlayRect.height * 0.0585f), "")) {
			infoPanelVisible = true;
			infoPanelPage = 0;
			infoPanelTransStart = Time.time;
		}
		if (GUI.Button(new Rect(optionsScreenX + overlayRect.width * 0.025f + overlayRect.width * 0.292f, titleY + overlayRect.height * 0.113f * 3f - overlayRect.height * 0.006f, overlayRect.width * 0.25f, overlayRect.height * 0.0585f), "Play Introduction....")) {
			infoPanelVisible = true;
			infoPanelPage = 0;
			infoPanelTransStart = Time.time;
		}
		GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
		GUI.color = new Color(1f, 1f, 1f, 1f);

		
		// ======================================	
		// DIVIDER
		DrawRect(new Rect(optionsScreenX, optionsScreenY + overlayRect.height * 0.452f, optionsScreenWidth, overlayRect.height * 0.005f), new Color(0f, 0f, 0f, 0.2f));	
		DrawRect(new Rect(optionsScreenX, optionsScreenY + overlayRect.height * 0.457f, optionsScreenWidth, overlayRect.height * 0.005f), new Color(1f, 1f, 1f, 0.5f));	
		// ======================================

		// OPTIONS FOR CHANGE

		// title
		style.fontSize = (int)(overlayRect.width * 0.024f);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		style.normal.textColor = new Color(0f, 0.35f, 0f, 1f);
		style.alignment = TextAnchor.UpperLeft;
		GUI.Button(new Rect(titleX, titleY + overlayRect.height * 0.113f * 4f, overlayRect.width * 0.17f, overlayRect.height * 0.03f), "Other Options", style);
		style.alignment = TextAnchor.UpperCenter;

		// button
		GUI.skin = customGUISkin;
		customGUISkin.button.fontSize = (int)(overlayRect.width * 0.0185);
		customGUISkin.button.fontStyle = FontStyle.Normal;
		GUI.color = new Color(1f, 1f, 1f, 1f);
		GUI.backgroundColor = new Color(1f, 0f, 0f, 1f);
		if (GUI.Button(new Rect(overlayRect.width * -0.003f + optionsScreenX + overlayRect.width * 0.025f + overlayRect.width * 0.292f, overlayRect.width * -0.0025f + titleY + overlayRect.height * 0.113f * 4f - overlayRect.height * 0.006f, overlayRect.width * 0.006f + overlayRect.width * 0.25f, overlayRect.width * 0.005f + overlayRect.height * 0.0585f), "")) {
			infoPanelVisible = true;
			infoPanelPage = 5;
			infoPanelTransStart = Time.time;
		}
		GUI.backgroundColor = new Color(0.5f, 0.5f, 0.9f, 1f);
		if (GUI.Button(new Rect(optionsScreenX + overlayRect.width * 0.025f + overlayRect.width * 0.292f, titleY + overlayRect.height * 0.113f * 4f - overlayRect.height * 0.006f, overlayRect.width * 0.25f, overlayRect.height * 0.0585f), "")) {
			infoPanelVisible = true;
			infoPanelPage = 5;
			infoPanelTransStart = Time.time;
		}
		if (GUI.Button(new Rect(optionsScreenX + overlayRect.width * 0.025f + overlayRect.width * 0.292f, titleY + overlayRect.height * 0.113f * 4f - overlayRect.height * 0.006f, overlayRect.width * 0.25f, overlayRect.height * 0.0585f), "Help Protect Pumas....")) {
			infoPanelVisible = true;
			infoPanelPage = 5;
			infoPanelTransStart = Time.time;
		}
		GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
		GUI.color = new Color(1f, 1f, 1f, 1f);
	}


	//======================================================================
	//======================================================================
	//======================================================================
	//								STATS
	//======================================================================
	//======================================================================
	//======================================================================
	
	void DrawStatsScreen() 
	{ 
		float statsX = overlayRect.x + overlayRect.width * 0.06f;
		float statsY = overlayRect.y + overlayRect.height * 0.205f;
		float statsWidth = overlayRect.width * 0.88f;
		float statsHeight = overlayRect.height * 0.578f;
		float fontScale = 1f;

		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		
	
	
		//Color fullHealthPumaHeadshotColor = new Color(0.2f, 1f, 0.1f, 1f * guiOpacity);
		//Color fullHealthPumaIconColor = new Color(0.1f, 1f, 0f, 0.65f * guiOpacity);
		//Color fullHealthPumaTextColor = new Color(0.32f, 0.32f, 0.22f, 0.8f * guiOpacity);

		//Color deadPumaHeadshotColor = new Color(0.8f, 0.1f, 0f, 0.55f * guiOpacity);
		//Color deadPumaIconColor = new Color(0.8f, 0f, 0f, 0.3f * guiOpacity);
		//Color deadPumaTextColor = new Color(0.32f, 0.32f, 0.22f, 0.8f * guiOpacity);

		//Color fullHealthPumaHeadshotColor = new Color(0.2f, 1f, 0.05f, 1f * guiOpacity);
		//Color fullHealthPumaHeadshotColor = new Color(1f, 0.87f, 0f, 0.75f * guiOpacity);
		//Color fullHealthPumaHeadshotColor = new Color(0.82f, 0.9f, 0f, 0.77f * guiOpacity);
		//Color fullHealthPumaDeerIconColor = new Color(0.9f, 0.9f, 0f, 0.5f * guiOpacity);
		Color fullHealthPumaHeadshotColor = new Color(0.99f, 0.92f, 0f, 0.7f * guiOpacity);
		Color fullHealthPumaDeerIconColor = new Color(0.99f, 0.92f, 0f, 0.4f * guiOpacity);
		Color fullHealthPumaTextColor = new Color(0.32f, 0.32f, 0.22f, 0.8f * guiOpacity);

		Color deadPumaHeadshotColor = new Color(0.8f, 0.1f, 0f, 0.55f * guiOpacity);
		Color deadPumaDeerIconColor = new Color(0.5f, 0.02f, 0f, 0.8f * guiOpacity);
		Color deadPumaTextColor = new Color(0.32f, 0.32f, 0.22f, 0.8f * guiOpacity);


		
		// background rectangles
		GUI.color = new Color(0f, 0f, 0f, 1f * guiOpacity);
		GUI.Box(new Rect(statsX - overlayRect.width * 0.02f, statsY - overlayRect.height * 0.025f, statsWidth + overlayRect.width * 0.04f, statsHeight + overlayRect.height * 0.05f), "");
		GUI.color = new Color(0f, 0f, 0f, 0.2f * guiOpacity);
		GUI.Box(new Rect(statsX, statsY, statsWidth, statsHeight), "");

		float columnCount = 7f;
		float columnGap = statsWidth * 0.02f;
		float midColumnSizeIncrease = columnGap * 2f;
		float columnWidth = (statsWidth - (columnGap * (columnCount-1)) - midColumnSizeIncrease) / columnCount;
		GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
		GUI.Box(new Rect(statsX + columnWidth*0 + columnGap*0, statsY, columnWidth, statsHeight), "");
		GUI.Box(new Rect(statsX + columnWidth*1 + columnGap*1, statsY, columnWidth, statsHeight), "");
		GUI.Box(new Rect(statsX + columnWidth*2 + columnGap*2, statsY, columnWidth, statsHeight), "");
		GUI.Box(new Rect(statsX + columnWidth*3 + columnGap*3, statsY, columnWidth + midColumnSizeIncrease, statsHeight), "");
		GUI.Box(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease, statsY, columnWidth, statsHeight), "");
		GUI.Box(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease, statsY, columnWidth, statsHeight), "");
		GUI.Box(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease, statsY, columnWidth, statsHeight), "");
		GUI.color = new Color(0f, 0f, 0f, 0.7f * guiOpacity);
		GUI.Box(new Rect(statsX + columnWidth*0 + columnGap*0, statsY, columnWidth, statsHeight), "");
		GUI.Box(new Rect(statsX + columnWidth*1 + columnGap*1, statsY, columnWidth, statsHeight), "");
		GUI.Box(new Rect(statsX + columnWidth*2 + columnGap*2, statsY, columnWidth, statsHeight), "");
		GUI.color = new Color(0f, 0f, 0f, 0.9f * guiOpacity);
		GUI.Box(new Rect(statsX + columnWidth*3 + columnGap*3, statsY, columnWidth + midColumnSizeIncrease, statsHeight), "");
		GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
		GUI.Box(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease, statsY, columnWidth, statsHeight), "");
		GUI.color = new Color(0f, 0f, 0f, 0.85f * guiOpacity);
		GUI.Box(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease, statsY, columnWidth, statsHeight), "");
		GUI.color = new Color(0f, 0f, 0f, 0.9f * guiOpacity);
		GUI.Box(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease, statsY, columnWidth, statsHeight), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
	

		// clickable invisible buttons to select puma
		GUI.color = new Color(1f, 1f, 1f, 0f * guiOpacity);

		if (GUI.Button(new Rect(statsX + columnWidth*0 + columnGap*0, statsY, columnWidth, statsHeight), "") && PumaIsSelectable(0)) {
			selectedPuma = 0;
			levelManager.SetSelectedPuma(0);
		}
		if (GUI.Button(new Rect(statsX + columnWidth*1 + columnGap*1, statsY, columnWidth, statsHeight), "") && PumaIsSelectable(1)) {
			selectedPuma = 1;
			levelManager.SetSelectedPuma(1);
		}
		if (GUI.Button(new Rect(statsX + columnWidth*2 + columnGap*2, statsY, columnWidth, statsHeight), "") && PumaIsSelectable(2)) {
			selectedPuma = 2;
			levelManager.SetSelectedPuma(2);
		}
		if (GUI.Button(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease, statsY, columnWidth, statsHeight), "") && PumaIsSelectable(3)) {
			selectedPuma = 3;
			levelManager.SetSelectedPuma(3);
		}
		if (GUI.Button(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease, statsY, columnWidth, statsHeight), "") && PumaIsSelectable(4)) {
			selectedPuma = 4;
			levelManager.SetSelectedPuma(4);
		}
		if (GUI.Button(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease, statsY, columnWidth, statsHeight), "") && PumaIsSelectable(5)) {
			selectedPuma = 5;
			levelManager.SetSelectedPuma(5);
		}

		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		
		
		
		
		// population bar
		
		float yOffsetForAddingPopulationBar = overlayRect.height * -0.012f;
		float actualGuiOpacity = guiOpacity;
		guiOpacity = guiOpacity * 0.9f;
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);		
		
		float healthBarX = overlayRect.x + overlayRect.width * 0.04f;
		float healthBarY = overlayRect.y + overlayRect.height * 0.844f + yOffsetForAddingPopulationBar;
		float healthBarWidth = overlayRect.width * 0.92f;
		float healthBarHeight = overlayRect.height * 0.048f;	
		float healthBarLabelWidth = 0f; //healthBarWidth * 0.13f;

		if (healthBarLabelWidth > 0f) {
			GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
			GUI.Box(new Rect(healthBarX, healthBarY, healthBarLabelWidth * 0.985f, healthBarHeight), "");
			GUI.Box(new Rect(healthBarX, healthBarY, healthBarLabelWidth * 0.985f, healthBarHeight), "");
			GUI.Box(new Rect(healthBarX + healthBarWidth - healthBarLabelWidth * 0.985f, healthBarY, healthBarLabelWidth * 0.985f, healthBarHeight), "");
			GUI.Box(new Rect(healthBarX + healthBarWidth - healthBarLabelWidth * 0.985f, healthBarY, healthBarLabelWidth * 0.985f, healthBarHeight), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		}

		guiComponents.DrawPopulationHealthBar(guiOpacity, healthBarX + healthBarLabelWidth, healthBarY, healthBarWidth - healthBarLabelWidth * 2f, healthBarHeight, false, true);

		guiOpacity = actualGuiOpacity;
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);		


		// puma heads at tops of columns
		
		float textureX;
		float textureY;
		float textureHeight;
		float textureWidth;
		Texture2D headshotTexture;
		
		GUI.color = new Color(1f, 1f, 1f, 0.85f * guiOpacity);
		
		float rightShift = columnWidth * 0.18f;
		float backgroundInset = columnWidth * 0.05f;
		float headSize = 0.135f;
		
		// background texture
		if (selectedPuma != -1) {
			int columnMultiplier = (selectedPuma < 3) ? selectedPuma : selectedPuma + 1;
			headshotTexture = closeupBackgroundTexture;
			textureX = statsX + columnWidth*columnMultiplier + columnGap*columnMultiplier + backgroundInset;
			textureX +=  (selectedPuma < 3) ? 0 : midColumnSizeIncrease;
			textureHeight = statsHeight * headSize;
			textureY = statsY + statsHeight * 0.0f + backgroundInset;
			textureWidth = columnWidth - backgroundInset * 2f;
			textureHeight = statsHeight * headSize * 1.115f;
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		}
		
		float headshotOpacity = 0.97f;
		
		textureY = statsY + statsHeight * 0.022f;

		// textures 1-6
		headshotTexture = closeup1Texture;
		textureX = rightShift + statsX + columnWidth*0 + columnGap*0;
		textureHeight = statsHeight * headSize;
		textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
		if (scoringSystem.GetPumaHealth(0) <= 0f)
			GUI.color = deadPumaHeadshotColor;
		else if (scoringSystem.GetPumaHealth(0) >= 1f)
			GUI.color = fullHealthPumaHeadshotColor;
		else
			GUI.color = new Color(1f, 1f, 1f, headshotOpacity * guiOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, headshotOpacity * guiOpacity);
		/////
		headshotTexture = closeup2Texture;
		textureX = rightShift + statsX + columnWidth*1 + columnGap*1;
		textureHeight = statsHeight * headSize;
		textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
		if (scoringSystem.GetPumaHealth(1) <= 0f)
			GUI.color = deadPumaHeadshotColor;
		else if (scoringSystem.GetPumaHealth(1) >= 1f)
			GUI.color = fullHealthPumaHeadshotColor;
		else
			GUI.color = new Color(1f, 1f, 1f, headshotOpacity * guiOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, headshotOpacity * guiOpacity);
		/////
		headshotTexture = closeup3Texture;
		textureX = rightShift + statsX + columnWidth*2 + columnGap*2;
		textureHeight = statsHeight * headSize;
		textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
		if (scoringSystem.GetPumaHealth(2) <= 0f)
			GUI.color = deadPumaHeadshotColor;
		else if (scoringSystem.GetPumaHealth(2) >= 1f)
			GUI.color = fullHealthPumaHeadshotColor;
		else
			GUI.color = new Color(1f, 1f, 1f, headshotOpacity * guiOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, headshotOpacity * guiOpacity);
		/////
		headshotTexture = closeup4Texture;
		textureX = rightShift + statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease;
		textureHeight = statsHeight * headSize;
		textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
		if (scoringSystem.GetPumaHealth(3) <= 0f)
			GUI.color = deadPumaHeadshotColor;
		else if (scoringSystem.GetPumaHealth(3) >= 1f)
			GUI.color = fullHealthPumaHeadshotColor;
		else
			GUI.color = new Color(1f, 1f, 1f, headshotOpacity * guiOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, headshotOpacity * guiOpacity);
		/////
		headshotTexture = closeup5Texture;
		textureX = rightShift + statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease;
		textureHeight = statsHeight * headSize;
		textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
		if (scoringSystem.GetPumaHealth(4) <= 0f)
			GUI.color = deadPumaHeadshotColor;
		else if (scoringSystem.GetPumaHealth(4) >= 1f)
			GUI.color = fullHealthPumaHeadshotColor;
		else
			GUI.color = new Color(1f, 1f, 1f, headshotOpacity * guiOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, headshotOpacity * guiOpacity);
		/////
		headshotTexture = closeup6Texture;
		textureX = rightShift + statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease;
		textureHeight = statsHeight * headSize;
		textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
		if (scoringSystem.GetPumaHealth(5) <= 0f)
			GUI.color = deadPumaHeadshotColor;
		else if (scoringSystem.GetPumaHealth(5) >= 1f)
			GUI.color = fullHealthPumaHeadshotColor;
		else
			GUI.color = new Color(1f, 1f, 1f, headshotOpacity * guiOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		
/*		
		// small puma heads in middle column
		float smallHeadSize = 0.075f;
		float smallHeadRightShift = (columnWidth + midColumnSizeIncrease) * 0.065f;
		float smallHeadUpperY = statsY + statsHeight * 0.02f;
		float smallHeadLowerY = statsY + statsHeight * 0.115f;

		textureHeight = statsHeight * smallHeadSize;	
		textureX = statsX + columnWidth*3 + columnGap*3 + smallHeadRightShift + (columnWidth + midColumnSizeIncrease) * 0.01f;
			
		headshotTexture = closeup1Texture;
		textureY = smallHeadUpperY;
		textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
		if (scoringSystem.GetPumaHealth(0) <= 0f)
			GUI.color = new Color(0.45f, 0f, 0f, 0.8f * guiOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);

		headshotTexture = closeup4Texture;
		textureY = smallHeadLowerY;
		textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
		if (scoringSystem.GetPumaHealth(3) <= 0f)
			GUI.color = new Color(0.45f, 0f, 0f, 0.8f * guiOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);

		textureX = statsX + columnWidth*3 + columnGap*3 + columnWidth * 0.4f + smallHeadRightShift;
			
		headshotTexture = closeup2Texture;
		textureY = smallHeadUpperY;
		textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
		if (scoringSystem.GetPumaHealth(1) <= 0f)
			GUI.color = new Color(0.45f, 0f, 0f, 0.8f * guiOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);

		headshotTexture = closeup5Texture;
		textureY = smallHeadLowerY;
		textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
		if (scoringSystem.GetPumaHealth(4) <= 0f)
			GUI.color = new Color(0.45f, 0f, 0f, 0.8f * guiOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);

		textureX = statsX + columnWidth*3 + columnGap*3 + columnWidth * 0.8f + smallHeadRightShift;
			
		headshotTexture = closeup3Texture;
		textureY = smallHeadUpperY;
		textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
		if (scoringSystem.GetPumaHealth(2) <= 0f)
			GUI.color = new Color(0.45f, 0f, 0f, 0.8f * guiOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);

		headshotTexture = closeup6Texture;
		textureY = smallHeadLowerY;
		textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
		if (scoringSystem.GetPumaHealth(5) <= 0f)
			GUI.color = new Color(0.45f, 0f, 0f, 0.8f * guiOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
*/
		

		// puma names

		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		
		float bigTextFont = 0.016f;
		float textY = statsY + statsHeight * 0.1705f;		
		textureHeight = statsHeight * headSize;
		
		//style.normal.textColor = new Color(0.99f, 0.62f, 0f, 0.95f);
		//style.normal.textColor = new Color(0.99f, 0.75f, 0.3f, 0.95f);
		Color unselectedTextColor = new Color(0.85f, 0.74f, 0.5f, 0.85f);	
		Color selectedTextColor = new Color(0.88f, 0.55f, 0f, 1f);
		selectedTextColor = unselectedTextColor;
		
		style.normal.textColor = (selectedPuma == 0) ? selectedTextColor : ((scoringSystem.GetPumaHealth(0) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(0) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * bigTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*0 + columnGap*0;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "Eric", style);
		style.alignment = TextAnchor.MiddleCenter;
		
		style.normal.textColor = (selectedPuma == 1) ? selectedTextColor : ((scoringSystem.GetPumaHealth(1) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(1) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * bigTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*1 + columnGap*1;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "Palo", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (selectedPuma == 2) ? selectedTextColor : ((scoringSystem.GetPumaHealth(2) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(2) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * bigTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*2 + columnGap*2;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "Mitch", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (selectedPuma == 3) ? selectedTextColor : ((scoringSystem.GetPumaHealth(3) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(3) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * bigTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "Trish", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (selectedPuma == 4) ? selectedTextColor : ((scoringSystem.GetPumaHealth(4) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(4) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * bigTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "Liam", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (selectedPuma == 5) ? selectedTextColor : ((scoringSystem.GetPumaHealth(5) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(5) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * bigTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "Barb", style);
		style.alignment = TextAnchor.MiddleCenter;

		
		
		// puma labels
		
		float smallTextFont = 0.013f;
		textY = statsY + statsHeight * 0.212f;	

		style.normal.textColor = (selectedPuma == 0) ? selectedTextColor : ((scoringSystem.GetPumaHealth(0) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(0) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*0 + columnGap*0;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "2 years - male", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (selectedPuma == 1) ? selectedTextColor : ((scoringSystem.GetPumaHealth(1) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(1) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*1 + columnGap*1;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "2 years - female", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (selectedPuma == 2) ? selectedTextColor : ((scoringSystem.GetPumaHealth(2) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(2) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*2 + columnGap*2;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "5 years - male", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (selectedPuma == 3) ? selectedTextColor : ((scoringSystem.GetPumaHealth(3) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(3) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "5 years - female", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (selectedPuma == 4) ? selectedTextColor : ((scoringSystem.GetPumaHealth(4) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(4) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "8 years - male", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (selectedPuma == 5) ? selectedTextColor : ((scoringSystem.GetPumaHealth(5) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(5) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "8 years - female", style);
		style.alignment = TextAnchor.MiddleCenter;

		// population labels
		
		style.normal.textColor = new Color(0.90f, 0.75f, 0.4f, 0.8f);	
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale * 1.15f);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*3 + columnGap*3;
		GUI.Button(new Rect(textureX, statsY + statsHeight * 0.02f, columnWidth + midColumnSizeIncrease, textureHeight), "Puma Population", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = new Color(0.90f, 0.85f, 0.4f, 0.7f);	
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale * 1.12f);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*3 + columnGap*3;
		GUI.Button(new Rect(textureX, statsY + statsHeight * 0.065f, columnWidth + midColumnSizeIncrease, textureHeight), "total results", style);
		style.alignment = TextAnchor.MiddleCenter;

		
		// deer heads

		float headstackBaseY = statsY + statsHeight * 0.291f;
		Texture2D displayHeadTexture;
		int columnNum;
		float columnShift;
		float incrementHeight = 0f;

		for (int j = 0; j < 6; j++) {
		
			if (scoringSystem.GetPumaHealth(j) <= 0f)
				GUI.color = deadPumaDeerIconColor;
			else if (scoringSystem.GetPumaHealth(j) >= 1f)
				GUI.color = fullHealthPumaDeerIconColor;

			columnNum = (j < 3) ? j : j+1;
			columnShift = (j < 3) ? 0 : midColumnSizeIncrease;
		
			displayHeadTexture = buckHeadTexture;
			textureX = statsX + columnWidth*columnNum + columnGap*columnNum + columnWidth*0.03f + columnShift;
			textureWidth = columnWidth * 0.24f;
			textureHeight = displayHeadTexture.height * (textureWidth / displayHeadTexture.width);
			incrementHeight = textureHeight * 1.1f;
			textureY = headstackBaseY - textureHeight * 0.0f;
			int kills = scoringSystem.GetBucksKilled(j);
			for (int i = 0; i < kills; i++) {
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), displayHeadTexture);
				textureY += incrementHeight;
			}

			displayHeadTexture = doeHeadTexture;
			textureX = statsX + columnWidth*columnNum + columnGap*columnNum + columnWidth*0.38f + columnShift;
			textureWidth = columnWidth * 0.26f;
			textureHeight = displayHeadTexture.height * (textureWidth / displayHeadTexture.width);
			textureY = headstackBaseY - textureHeight * 0.08f;
			kills = scoringSystem.GetDoesKilled(j);
			for (int i = 0; i < kills; i++) {
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), displayHeadTexture);
				textureY += incrementHeight;
			}
			
			displayHeadTexture = fawnHeadTexture;
			textureX = statsX + columnWidth*columnNum + columnGap*columnNum + columnWidth*0.72f + columnShift;
			textureY = headstackBaseY - textureHeight * 0.08f;
			textureWidth = columnWidth * 0.27f;
			textureHeight = displayHeadTexture.height * (textureWidth / displayHeadTexture.width);
			kills = scoringSystem.GetFawnsKilled(j);
			for (int i = 0; i < kills; i++) {
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), displayHeadTexture);
				textureY += incrementHeight;
			}
			
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		}
		
		// deer heads in center column
		
		columnNum = 3;
		columnShift = 0f;
		
		float centerColumnOffsetY = statsHeight * -0.15f;
		headstackBaseY += centerColumnOffsetY;
	
		// buck
		int bucksKilled = 0;
		for (int i = 0; i < 6; i++)
			bucksKilled += scoringSystem.GetBucksKilled(i);
		displayHeadTexture = buckHeadTexture;
		textureX = statsX + columnWidth*columnNum + columnGap*columnNum + columnWidth*0.03f + columnShift;
		textureWidth = columnWidth * 0.3f * 0.66f;
		textureHeight = displayHeadTexture.height * (textureWidth / displayHeadTexture.width);
		incrementHeight = textureHeight * 1.1f;
		textureY = headstackBaseY;
		for (int i = 0; i < (bucksKilled/2+bucksKilled%2); i++) {
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), displayHeadTexture);
			textureY += incrementHeight;
		}
		textureX = statsX + columnWidth*columnNum + columnGap*columnNum + columnWidth*0.22f + columnShift;
		textureY = headstackBaseY;
		for (int i = 0; i < (bucksKilled/2); i++) {
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), displayHeadTexture);
			textureY += incrementHeight;
		}

		// doe
		int doesKilled = 0;
		for (int i = 0; i < 6; i++)
			doesKilled += scoringSystem.GetDoesKilled(i);
		displayHeadTexture = doeHeadTexture;
		textureX = statsX + columnWidth*columnNum + columnGap*columnNum + columnWidth*0.48f + columnShift;
		textureWidth = columnWidth * 0.31f * 0.66f;
		textureHeight = displayHeadTexture.height * (textureWidth / displayHeadTexture.width);
		textureY = headstackBaseY - textureHeight * 0.10f;
		for (int i = 0; i < (doesKilled/2+doesKilled%2); i++) {
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), displayHeadTexture);
			textureY += incrementHeight;
		}
		textureX = statsX + columnWidth*columnNum + columnGap*columnNum + columnWidth*0.68f + columnShift;
		textureY = headstackBaseY - textureHeight * 0.10f;
		for (int i = 0; i < (doesKilled/2); i++) {
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), displayHeadTexture);
			textureY += incrementHeight;
		}
		
		// fawn
		int fawnsKilled = 0;
		for (int i = 0; i < 6; i++)
			fawnsKilled += scoringSystem.GetFawnsKilled(i);
		displayHeadTexture = fawnHeadTexture;
		textureX = statsX + columnWidth*columnNum + columnGap*columnNum + columnWidth*0.94f + columnShift;
		textureY = headstackBaseY;
		textureWidth = columnWidth * 0.3f * 0.66f;
		textureHeight = displayHeadTexture.height * (textureWidth / displayHeadTexture.width);
		for (int i = 0; i < (fawnsKilled/2+fawnsKilled%2); i++) {
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), displayHeadTexture);
			textureY += incrementHeight;
		}
		textureX = statsX + columnWidth*columnNum + columnGap*columnNum + columnWidth*1.08f + columnShift;
		textureY = headstackBaseY;
		for (int i = 0; i < (fawnsKilled/2); i++) {
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), displayHeadTexture);
			textureY += incrementHeight;
		}
	
		
		
		
		

		// vertical display bars
		
		float verticalBarsY = statsY + statsHeight * 0.71f;
		DrawDisplayBarsVert(0, statsX + columnWidth*0 + columnGap*0 + columnWidth*0.15f, verticalBarsY, columnWidth * 0.7f, statsHeight * 0.13f);
		DrawDisplayBarsVert(1, statsX + columnWidth*1 + columnGap*1 + columnWidth*0.15f, verticalBarsY, columnWidth * 0.7f, statsHeight * 0.13f);
		DrawDisplayBarsVert(2, statsX + columnWidth*2 + columnGap*2 + columnWidth*0.15f, verticalBarsY, columnWidth * 0.7f, statsHeight * 0.13f);
		DrawDisplayBarsVert(3, statsX + columnWidth*4 + columnGap*4 + columnWidth*0.15f + midColumnSizeIncrease, verticalBarsY, columnWidth * 0.7f, statsHeight * 0.13f);
		DrawDisplayBarsVert(4, statsX + columnWidth*5 + columnGap*5 + columnWidth*0.15f + midColumnSizeIncrease, verticalBarsY, columnWidth * 0.7f, statsHeight * 0.13f);
		DrawDisplayBarsVert(5, statsX + columnWidth*6 + columnGap*6 + columnWidth*0.15f + midColumnSizeIncrease, verticalBarsY, columnWidth * 0.7f, statsHeight * 0.13f);
		DrawDisplayBarsVert(6, statsX + columnWidth*3 + columnGap*3 + columnWidth*0.16f, verticalBarsY, columnWidth * 0.68f + midColumnSizeIncrease, statsHeight * 0.13f);

		
		// health bar
		
		float healthBarYFactor = 0.94f;

		guiComponents.DrawPumaHealthBar(0, guiOpacity, statsX + columnWidth*0 + columnGap*0, statsY + statsHeight * healthBarYFactor, columnWidth, statsHeight * 0.04f, true, true);
		guiComponents.DrawPumaHealthBar(1, guiOpacity, statsX + columnWidth*1 + columnGap*1, statsY + statsHeight * healthBarYFactor, columnWidth, statsHeight * 0.04f, true, true);
		guiComponents.DrawPumaHealthBar(2, guiOpacity, statsX + columnWidth*2 + columnGap*2, statsY + statsHeight * healthBarYFactor, columnWidth, statsHeight * 0.04f, true, true);
		guiComponents.DrawPumaHealthBar(3, guiOpacity, statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease, statsY + statsHeight * healthBarYFactor, columnWidth, statsHeight * 0.04f, true, true);
		guiComponents.DrawPumaHealthBar(4, guiOpacity, statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease, statsY + statsHeight * healthBarYFactor, columnWidth, statsHeight * 0.04f, true, true);
		guiComponents.DrawPumaHealthBar(5, guiOpacity, statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease, statsY + statsHeight * healthBarYFactor, columnWidth, statsHeight * 0.04f, true, true);


		
		// ======================================
		
		// DIVIDERS

		float dividerY;
		Color upperColor;
		Color lowerColor;
		

		// horizontal - upper	
		dividerY = statsY + statsHeight * 0.28f;
		upperColor = new Color(0f, 0f, 0f, 0.55f);
		lowerColor = new Color(0.5f, 0.49f, 0.47f, 0.3f);
		DrawRect(new Rect(statsX + columnWidth*0 + columnGap*0, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		DrawRect(new Rect(statsX + columnWidth*0 + columnGap*0, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		DrawRect(new Rect(statsX + columnWidth*1 + columnGap*1, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		DrawRect(new Rect(statsX + columnWidth*1 + columnGap*1, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		DrawRect(new Rect(statsX + columnWidth*2 + columnGap*2, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		DrawRect(new Rect(statsX + columnWidth*2 + columnGap*2, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		DrawRect(new Rect(statsX + columnWidth*3 + columnGap*3, dividerY - overlayRect.height * 0.005f + centerColumnOffsetY, columnWidth + midColumnSizeIncrease, overlayRect.height * 0.005f), upperColor);	
		DrawRect(new Rect(statsX + columnWidth*3 + columnGap*3, dividerY + centerColumnOffsetY, columnWidth + midColumnSizeIncrease, overlayRect.height * 0.003f), lowerColor);	
		DrawRect(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		DrawRect(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		DrawRect(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		DrawRect(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		DrawRect(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		DrawRect(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	

		// vertical dividers
		Color leftColor = new Color(0f, 0f, 0f, 0.8f);
		Color rightColor = new Color(0.3f, 0.3f, 0.3f, 0.3f);
		float leftWidth = columnWidth * 0.02f;
		float rightWidth = columnWidth * 0.017f;
		//float barsHeight = statsHeight * 0.395f;
		float barsHeight = ((statsY + statsHeight * 0.86f) - (overlayRect.height * 0.005f)) - (dividerY + overlayRect.height * 0.003f);
		float leftPercent = 0.34f;
		float rightPercent = 0.67f;
		float columnWideWidth = columnWidth + midColumnSizeIncrease;
		DrawRect(new Rect(statsX + columnWidth*0 + columnGap*0 + columnWidth * leftPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		DrawRect(new Rect(statsX + columnWidth*0 + columnGap*0 + columnWidth * leftPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		DrawRect(new Rect(statsX + columnWidth*0 + columnGap*0 + columnWidth * rightPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		DrawRect(new Rect(statsX + columnWidth*0 + columnGap*0 + columnWidth * rightPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		DrawRect(new Rect(statsX + columnWidth*1 + columnGap*1 + columnWidth * leftPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		DrawRect(new Rect(statsX + columnWidth*1 + columnGap*1 + columnWidth * leftPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		DrawRect(new Rect(statsX + columnWidth*1 + columnGap*1 + columnWidth * rightPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		DrawRect(new Rect(statsX + columnWidth*1 + columnGap*1 + columnWidth * rightPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		DrawRect(new Rect(statsX + columnWidth*2 + columnGap*2 + columnWidth * leftPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		DrawRect(new Rect(statsX + columnWidth*2 + columnGap*2 + columnWidth * leftPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		DrawRect(new Rect(statsX + columnWidth*2 + columnGap*2 + columnWidth * rightPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		DrawRect(new Rect(statsX + columnWidth*2 + columnGap*2 + columnWidth * rightPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		rightPercent = 0.69f;
		DrawRect(new Rect(statsX + columnWidth*3 + columnGap*3 + columnWideWidth * leftPercent - leftWidth, dividerY + overlayRect.height * 0.003f + centerColumnOffsetY, leftWidth, barsHeight - centerColumnOffsetY), leftColor);	
		DrawRect(new Rect(statsX + columnWidth*3 + columnGap*3 + columnWideWidth * leftPercent, dividerY + overlayRect.height * 0.003f + centerColumnOffsetY, rightWidth, barsHeight - centerColumnOffsetY), rightColor);	
		DrawRect(new Rect(statsX + columnWidth*3 + columnGap*3 + columnWideWidth * rightPercent - leftWidth, dividerY + overlayRect.height * 0.003f + centerColumnOffsetY, leftWidth, barsHeight - centerColumnOffsetY), leftColor);	
		DrawRect(new Rect(statsX + columnWidth*3 + columnGap*3 + columnWideWidth * rightPercent, dividerY + overlayRect.height * 0.003f + centerColumnOffsetY, rightWidth, barsHeight - centerColumnOffsetY), rightColor);	
		rightPercent = 0.67f;
		DrawRect(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease+ columnWidth * leftPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		DrawRect(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease+ columnWidth * leftPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		DrawRect(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease+ columnWidth * rightPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		DrawRect(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease+ columnWidth * rightPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		DrawRect(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease+ columnWidth * leftPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		DrawRect(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease+ columnWidth * leftPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		DrawRect(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease+ columnWidth * rightPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		DrawRect(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease+ columnWidth * rightPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		DrawRect(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease+ columnWidth * leftPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		DrawRect(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease+ columnWidth * leftPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		DrawRect(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease+ columnWidth * rightPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		DrawRect(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease+ columnWidth * rightPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	

/*
		// horizontal - middle	
		dividerY = statsY + statsHeight * 0.68f;
		upperColor = new Color(0f, 0f, 0f, 0.55f);
		lowerColor = new Color(0.4f, 0.4f, 0.4f, 0.3f);
		DrawRect(new Rect(statsX + columnWidth*0 + columnGap*0, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		DrawRect(new Rect(statsX + columnWidth*0 + columnGap*0, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		DrawRect(new Rect(statsX + columnWidth*1 + columnGap*1, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		DrawRect(new Rect(statsX + columnWidth*1 + columnGap*1, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		DrawRect(new Rect(statsX + columnWidth*2 + columnGap*2, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		DrawRect(new Rect(statsX + columnWidth*2 + columnGap*2, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		DrawRect(new Rect(statsX + columnWidth*3 + columnGap*3, dividerY - overlayRect.height * 0.005f, columnWidth + midColumnSizeIncrease, overlayRect.height * 0.005f), upperColor);	
		DrawRect(new Rect(statsX + columnWidth*3 + columnGap*3, dividerY, columnWidth + midColumnSizeIncrease, overlayRect.height * 0.003f), lowerColor);	
		DrawRect(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		DrawRect(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		DrawRect(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		DrawRect(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		DrawRect(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		DrawRect(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
*/
		// horizontal - lower	
		dividerY = statsY + statsHeight * 0.86f;
		upperColor = new Color(0f, 0f, 0f, 0.55f);
		lowerColor = new Color(0.5f, 0.49f, 0.47f, 0.3f);
		DrawRect(new Rect(statsX + columnWidth*0 + columnGap*0, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		DrawRect(new Rect(statsX + columnWidth*0 + columnGap*0, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		DrawRect(new Rect(statsX + columnWidth*1 + columnGap*1, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		DrawRect(new Rect(statsX + columnWidth*1 + columnGap*1, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		DrawRect(new Rect(statsX + columnWidth*2 + columnGap*2, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		DrawRect(new Rect(statsX + columnWidth*2 + columnGap*2, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		DrawRect(new Rect(statsX + columnWidth*3 + columnGap*3, dividerY - overlayRect.height * 0.005f, columnWidth + midColumnSizeIncrease, overlayRect.height * 0.005f), upperColor);	
		DrawRect(new Rect(statsX + columnWidth*3 + columnGap*3, dividerY, columnWidth + midColumnSizeIncrease, overlayRect.height * 0.003f), lowerColor);	
		DrawRect(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		DrawRect(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		DrawRect(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		DrawRect(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		DrawRect(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		DrawRect(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	

		// ======================================
		
		
		
		
		
		
		return;

		// ======================================
		
		// DIVIDER

		DrawRect(new Rect(statsX, statsY + (statsHeight * 0.375f) - overlayRect.height * 0.005f, statsWidth, overlayRect.height * 0.005f), new Color(0f, 0f, 0f, 0.35f));	
		DrawRect(new Rect(statsX, statsY + (statsHeight * 0.375f), statsWidth, overlayRect.height * 0.005f), new Color(1f, 1f, 1f, 0.5f));	

		DrawRect(new Rect(statsX, statsY + (statsHeight/2) - overlayRect.height * 0.005f, statsWidth, overlayRect.height * 0.005f), new Color(0f, 0f, 0f, 0.35f));	
		DrawRect(new Rect(statsX, statsY + (statsHeight/2), statsWidth, overlayRect.height * 0.005f), new Color(1f, 1f, 1f, 0.5f));	

		DrawRect(new Rect(statsX, statsY + (statsHeight * 0.875f) - overlayRect.height * 0.005f, statsWidth, overlayRect.height * 0.005f), new Color(0f, 0f, 0f, 0.35f));	
		DrawRect(new Rect(statsX, statsY + (statsHeight * 0.875f), statsWidth, overlayRect.height * 0.005f), new Color(1f, 1f, 1f, 0.5f));	

		// ======================================
		
		float lowerSectionStatsY = statsY + statsHeight/2 - overlayRect.height * 0.01f;

		statsX = overlayRect.x + overlayRect.width * 0.15f;
		statsY = lowerSectionStatsY;
		statsWidth = overlayRect.width * 0.7f;
		statsHeight = overlayRect.height * 0.43f;
			
		// HUNTING STATS

		// title
		style.fontSize = (int)(overlayRect.width * 0.034f * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.MiddleRight;
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.044f, statsWidth * 0.21f, statsHeight * 0.1f), "Predation", style);

		// column headings
		style.fontSize = (int)(overlayRect.width * 0.026f * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.MiddleCenter;
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.30f, statsY + statsHeight * 0.051f, statsWidth * 0.16f, statsHeight * 0.1f), "Buck", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.46f, statsY + statsHeight * 0.051f, statsWidth * 0.16f, statsHeight * 0.1f), "Doe", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.62f, statsY + statsHeight * 0.051f, statsWidth * 0.16f, statsHeight * 0.1f), "Fawn", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.80f, statsY + statsHeight * 0.051f, statsWidth * 0.15f, statsHeight * 0.1f), "TOTAL", style);

		// columns
		style.fontSize = (int)(overlayRect.width * 0.026f * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.MiddleCenter;
		style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);

		GUI.Button(new Rect(statsX + statsWidth * 0.30f, statsY + statsHeight * 0.14f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.30f, statsY + statsHeight * 0.215f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.30f, statsY + statsHeight * 0.29f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		style.normal.textColor = new Color(0f, 0.35f, 0f, 1f);
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.30f, statsY + statsHeight * 0.38f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);

		GUI.Button(new Rect(statsX + statsWidth * 0.46f, statsY + statsHeight * 0.14f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.46f, statsY + statsHeight * 0.215f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.46f, statsY + statsHeight * 0.29f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		style.normal.textColor = new Color(0f, 0.35f, 0f, 1f);
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.46f, statsY + statsHeight * 0.38f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);

		GUI.Button(new Rect(statsX + statsWidth * 0.62f, statsY + statsHeight * 0.14f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.62f, statsY + statsHeight * 0.215f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.62f, statsY + statsHeight * 0.29f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		style.normal.textColor = new Color(0f, 0.35f, 0f, 1f);
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.62f, statsY + statsHeight * 0.38f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);

		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.80f, statsY + statsHeight * 0.14f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.80f, statsY + statsHeight * 0.215f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.80f, statsY + statsHeight * 0.29f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		style.normal.textColor = new Color(0f, 0.35f, 0f, 1f);
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.80f, statsY + statsHeight * 0.38f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);

		// row labels
		style.fontSize = (int)(overlayRect.width * 0.023f * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.MiddleRight;
		style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.14f, statsWidth * 0.21f, statsHeight * 0.1f), "Prey Killed", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.215f, statsWidth * 0.21f, statsHeight * 0.1f), "Effort Spent", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.29f, statsWidth * 0.21f, statsHeight * 0.1f), "Calories Eaten", style);
		style.fontSize = (int)(overlayRect.width * 0.028f * fontScale);
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.38f, statsWidth * 0.21f, statsHeight * 0.1f), "Energy Gain", style);

		// grid lines
		//DrawRect(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.14f, statsWidth * 0.9f, statsHeight * 0.007f), new Color(1f, 1f, 1f, 0.5f));	
		//DrawRect(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.38f, statsWidth * 0.9f, statsHeight * 0.007f), new Color(1f, 1f, 1f, 0.5f));	

		// button
		GUI.skin = customGUISkin;
		customGUISkin.button.fontSize = (int)(overlayRect.width * 0.0165);
		customGUISkin.button.fontStyle = FontStyle.Normal;
		GUI.color = new Color(1f, 1f, 1f, 1f);
		GUI.backgroundColor = new Color(1f, 0f, 0f, 1f);
		if (GUI.Button(new Rect(overlayRect.width * -0.003f + statsX + statsWidth * 0.24f, overlayRect.width * -0.0025f + statsY + statsHeight * 0.56f, overlayRect.width * 0.006f + statsWidth * 0.3f, overlayRect.width * 0.005f + statsHeight * 0.098f), "")) {
			infoPanelVisible = true;
			infoPanelPage = 2;
			infoPanelTransStart = Time.time;
		}
		GUI.backgroundColor = new Color(0.5f, 0.5f, 0.9f, 1f);
		if (GUI.Button(new Rect(statsX + statsWidth * 0.24f, statsY + statsHeight * 0.56f, statsWidth * 0.3f, statsHeight * 0.098f), "")) {
			infoPanelVisible = true;
			infoPanelPage = 2;
			infoPanelTransStart = Time.time;
		}
		if (GUI.Button(new Rect(statsX + statsWidth * 0.24f, statsY + statsHeight * 0.56f, statsWidth * 0.3f, statsHeight * 0.098f), "How Pumas Hunt....")) {
			infoPanelVisible = true;
			infoPanelPage = 2;
			infoPanelTransStart = Time.time;
		}
		GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
		GUI.color = new Color(1f, 1f, 1f, 1f);
		//buttonStyle.fontSize = buttonDownStyle.fontSize = (int)(overlayRect.width * 0.0165);
		//GUI.color = new Color(1f, 1f, 1f, 0.84f);
		//if (GUI.Button(new Rect(statsX + statsWidth * 0.24f, statsY + statsHeight * 0.554f, statsWidth * 0.52f, statsHeight * 0.11f), "HOW REAL PUMAS HUNT THEIR PREY...", buttonStyle))
			//currentScreen = currentScreen;
		//GUI.color = new Color(1f, 1f, 1f, 1f);
		

		// ======================================

		//statsY = lowerSectionStatsY;
		statsY = overlayRect.y + overlayRect.height * 0.21f;
		
		// ======================================

		// SURVIVAL STATS

		// title
		style.fontSize = (int)(overlayRect.width * 0.034f * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.MiddleRight;
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.044f, statsWidth * 0.21f, statsHeight * 0.1f), "Survival", style);

		// column headings
		style.fontSize = (int)(overlayRect.width * 0.026f * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.MiddleCenter;
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.30f, statsY + statsHeight * 0.051f, statsWidth * 0.16f, statsHeight * 0.1f), "Level 1", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.46f, statsY + statsHeight * 0.051f, statsWidth * 0.16f, statsHeight * 0.1f), "Level 2", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.62f, statsY + statsHeight * 0.051f, statsWidth * 0.16f, statsHeight * 0.1f), "Level 3", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.80f, statsY + statsHeight * 0.051f, statsWidth * 0.15f, statsHeight * 0.1f), "TOTAL", style);

		// columns
		style.fontSize = (int)(overlayRect.width * 0.026f * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.MiddleCenter;
		style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);

		GUI.Button(new Rect(statsX + statsWidth * 0.30f, statsY + statsHeight * 0.14f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		//GUI.Button(new Rect(statsX + statsWidth * 0.30f, statsY + statsHeight * 0.215f, statsWidth * 0.16f, statsHeight * 0.1f), "--", style);
		//DrawRect(new Rect(statsX + statsWidth * 0.365f, statsY + statsHeight * 0.245f, statsWidth * 0.03f, statsHeight * 0.04f), new Color(0.06f, 0.06f, 0.16f, 0.17f));	
		//GUI.Button(new Rect(statsX + statsWidth * 0.30f, statsY + statsHeight * 0.29f, statsWidth * 0.16f, statsHeight * 0.1f), "--", style);
		//DrawRect(new Rect(statsX + statsWidth * 0.365f, statsY + statsHeight * 0.32f, statsWidth * 0.03f, statsHeight * 0.04f), new Color(1f, 1f, 1f, 0.0f));	
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.30f, statsY + statsHeight * 0.38f, statsWidth * 0.16f, statsHeight * 0.1f), "6", style);
		style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);

		GUI.Button(new Rect(statsX + statsWidth * 0.46f, statsY + statsHeight * 0.14f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.46f, statsY + statsHeight * 0.215f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		//GUI.Button(new Rect(statsX + statsWidth * 0.46f, statsY + statsHeight * 0.29f, statsWidth * 0.16f, statsHeight * 0.1f), "--", style);
		//DrawRect(new Rect(statsX + statsWidth * 0.525f, statsY + statsHeight * 0.32f, statsWidth * 0.03f, statsHeight * 0.04f), new Color(1f, 1f, 1f, 0.0f));	
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.46f, statsY + statsHeight * 0.38f, statsWidth * 0.16f, statsHeight * 0.1f), "6", style);
		style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);

		GUI.Button(new Rect(statsX + statsWidth * 0.62f, statsY + statsHeight * 0.14f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.62f, statsY + statsHeight * 0.215f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.62f, statsY + statsHeight * 0.29f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.62f, statsY + statsHeight * 0.38f, statsWidth * 0.16f, statsHeight * 0.1f), "6", style);
		style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);

		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.80f, statsY + statsHeight * 0.14f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.80f, statsY + statsHeight * 0.215f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.80f, statsY + statsHeight * 0.29f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.80f, statsY + statsHeight * 0.38f, statsWidth * 0.16f, statsHeight * 0.1f), "18", style);
		style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);

		// row labels
		style.fontSize = (int)(overlayRect.width * 0.023f * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.MiddleRight;
		style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.14f, statsWidth * 0.21f, statsHeight * 0.1f), "Starved", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.215f, statsWidth * 0.21f, statsHeight * 0.1f), "Poached", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.29f, statsWidth * 0.21f, statsHeight * 0.1f), "Road Killed", style);
		style.fontSize = (int)(overlayRect.width * 0.028f * fontScale);
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.38f, statsWidth * 0.21f, statsHeight * 0.1f), "Pumas Left", style);

		// grid lines
		//DrawRect(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.14f, statsWidth * 0.9f, statsHeight * 0.007f), new Color(1f, 1f, 1f, 0.5f));	
		//DrawRect(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.38f, statsWidth * 0.9f, statsHeight * 0.007f), new Color(1f, 1f, 1f, 0.5f));	
		
		// button
		GUI.skin = customGUISkin;
		customGUISkin.button.fontSize = (int)(overlayRect.width * 0.0165);
		customGUISkin.button.fontStyle = FontStyle.Normal;
		GUI.color = new Color(1f, 1f, 1f, 1f);
		GUI.backgroundColor = new Color(1f, 0f, 0f, 1f);
		if (GUI.Button(new Rect(overlayRect.width * -0.003f + statsX + statsWidth * 0.24f, overlayRect.width * -0.0025f + statsY + statsHeight * 0.56f, overlayRect.width * 0.006f + statsWidth * 0.3f, overlayRect.width * 0.005f + statsHeight * 0.098f), "")) {
			infoPanelVisible = true;
			infoPanelPage = 4;
			infoPanelTransStart = Time.time;
		}
		GUI.backgroundColor = new Color(0.5f, 0.5f, 0.9f, 1f);
		if (GUI.Button(new Rect(statsX + statsWidth * 0.24f, statsY + statsHeight * 0.56f, statsWidth * 0.3f, statsHeight * 0.098f), "")) {
			infoPanelVisible = true;
			infoPanelPage = 4;
			infoPanelTransStart = Time.time;
		}
		if (GUI.Button(new Rect(statsX + statsWidth * 0.24f, statsY + statsHeight * 0.56f, statsWidth * 0.3f, statsHeight * 0.098f), "Threats to Pumas....")) {
			infoPanelVisible = true;
			infoPanelPage = 4;
			infoPanelTransStart = Time.time;
		}
		GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
		GUI.color = new Color(1f, 1f, 1f, 1f);

		//buttonStyle.fontSize = buttonDownStyle.fontSize = (int)(overlayRect.width * 0.0165);
		//GUI.color = new Color(1f, 1f, 1f, 0.89f);
		//if (GUI.Button(new Rect(statsX + statsWidth * 0.24f, statsY + statsHeight * 0.545f, statsWidth * 0.52f, statsHeight * 0.11f), "THE THREATS REAL PUMAS FACE...", buttonStyle))
			//currentScreen = currentScreen;
		//GUI.color = new Color(1f, 1f, 1f, 1f);

	}


	void DrawQuitScreen() 
	{ 
		float quitScreenY = overlayRect.y + overlayRect.height * 0.37f;

		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;

		// background rectangle
		//DrawRect(new Rect(overlayRect.width * 0.315f, overlayRect.height * 0.3f, overlayRect.width * 0.37f, overlayRect.height * 0.3f), new Color(1f, 1f, 1f, 0.6f));	
		GUI.color = new Color(0f, 0f, 0f, 1f);
		GUI.Box(new Rect(overlayRect.x + overlayRect.width * 0.315f, quitScreenY, overlayRect.width * 0.37f, overlayRect.height * 0.3f), "");
		GUI.color = new Color(0f, 0f, 0f, 0.55f);
		GUI.Box(new Rect(overlayRect.x + overlayRect.width * 0.315f, quitScreenY, overlayRect.width * 0.37f, overlayRect.height * 0.3f), "");
		GUI.color = new Color(0f, 0f, 0f, 1f);

		GUI.color = new Color(1f, 1f, 1f, 1f);
		style.fontSize = (int)(overlayRect.width * 0.036f);
		style.fontStyle = FontStyle.BoldAndItalic;
		//style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);
		//style.normal.textColor = new Color(1f, 1f, 1f, 1f);
			
		// quit screen prompt
		//DrawRect(new Rect(overlayRect.width * 0.32f, overlayRect.height * 0.34f, overlayRect.width * 0.36f, overlayRect.height * 0.08f), new Color(1f, 1f, 1f, 0.7f));	
		style.fontSize = (int)(overlayRect.width * 0.036f);
		style.fontStyle = FontStyle.BoldAndItalic;
		//style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);
		style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
		GUI.Button(new Rect(overlayRect.x + overlayRect.width * 0.3f, quitScreenY + overlayRect.height * 0.03f, overlayRect.width * 0.4f, overlayRect.height * 0.1f), "Really Quit?", style);
		style.normal.textColor = Color.white;

		// quit button
		GUI.color = new Color(1f, 1f, 1f, 0.15f);
		DrawRect(new Rect(overlayRect.x + overlayRect.width * 0.415f, quitScreenY + overlayRect.height * 0.150f, overlayRect.width * 0.17f, overlayRect.height * 0.09f), new Color(1f, 1f, 1f, 1f));	
		GUI.skin = customGUISkin;
		customGUISkin.button.fontSize = (int)(overlayRect.width * 0.026);
		customGUISkin.button.fontStyle = FontStyle.Normal;
		GUI.color = new Color(1f, 1f, 1f, 1f);
		GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
		if (GUI.Button(new Rect(overlayRect.x + overlayRect.width * 0.42f, quitScreenY + overlayRect.height * 0.155f, overlayRect.width * 0.16f, overlayRect.height * 0.08f), "")) {
			Application.Quit();
		}
		if (GUI.Button(new Rect(overlayRect.x + overlayRect.width * 0.42f, quitScreenY + overlayRect.height * 0.155f, overlayRect.width * 0.16f, overlayRect.height * 0.08f), "Quit")) {
			Application.Quit();
		}
		GUI.color = new Color(1f, 1f, 1f, 1f);
		GUI.backgroundColor = new Color(0f, 0f, 0f, 0f);
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
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
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

	
	
	
	void DrawDisplayBars(int pumaNum, float refX, float refY, float refWidth, float refHeight, bool brightFlag = false) 
	{ 
		float yOffset = overlayRect.height * -0.085f;
		float displayBarsRightShift = overlayRect.height * -0.1265f;
		
		refWidth = refHeight * 3f;
	
		// display bars for characteristics: backgrounds
		GUI.color = new Color(0f, 0f, 0f, (brightFlag ? 0.9f : 0.75f) * guiOpacity);
		GUI.Box(new Rect(refX + refWidth * 0.52f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.012f - overlayRect.height * 0.00f, refWidth * 0.38f, overlayRect.height * 0.012f), "");
		GUI.Box(new Rect(refX + refWidth * 0.52f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.034f - overlayRect.height * 0.00f, refWidth * 0.38f, overlayRect.height * 0.012f), "");
		GUI.Box(new Rect(refX + refWidth * 0.52f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.056f - overlayRect.height * 0.00f, refWidth * 0.38f, overlayRect.height * 0.012f), "");
		GUI.Box(new Rect(refX + refWidth * 0.52f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.078f - overlayRect.height * 0.00f, refWidth * 0.38f, overlayRect.height * 0.012f), "");

		// display bars for characteristics: backgrounds again to darken them
		//GUI.Box(new Rect(headshotX + headshotWidth * 0.52f,  yOffset + headshotY + headshotHeight + overlayRect.height * 0.012f - overlayRect.height * 0.002f, headshotWidth * 0.38f, overlayRect.height * 0.012f), "");
		//GUI.Box(new Rect(headshotX + headshotWidth * 0.52f,  yOffset + headshotY + headshotHeight + overlayRect.height * 0.035f - overlayRect.height * 0.002f, headshotWidth * 0.38f, overlayRect.height * 0.012f), "");
		//GUI.Box(new Rect(headshotX + headshotWidth * 0.52f,  yOffset + headshotY + headshotHeight + overlayRect.height * 0.058f - overlayRect.height * 0.002f, headshotWidth * 0.38f, overlayRect.height * 0.012f), "");
		//GUI.Box(new Rect(headshotX + headshotWidth * 0.52f,  yOffset + headshotY + headshotHeight + overlayRect.height * 0.081f - overlayRect.height * 0.002f, headshotWidth * 0.38f, overlayRect.height * 0.012f), "");

		// display bars for characteristics: fillers
		GUI.color = new Color(1f, 1f, 1f, (brightFlag ? 0.7f : 0.55f) * guiOpacity);


		
		if (true) {

			if (scoringSystem.GetPumaHealth(pumaNum) >= 1f)
				GUI.color = new Color(0.1f, 1f, 0f, (brightFlag ? 0.7f : 0.55f) * guiOpacity);
			else if (scoringSystem.GetPumaHealth(pumaNum) <= 0f)
				GUI.color = new Color(0.4f, 0.4f, 0.4f, (brightFlag ? 0.5f : 0.4f) * guiOpacity);

			DrawRect(new Rect(refX + refWidth * 0.54f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.018f - overlayRect.height * 0.002f, refWidth * 0.34f, overlayRect.height * 0.0048f), new Color(0.42f, 0.404f, 0.533f, 1f));	
			DrawRect(new Rect(refX + refWidth * 0.54f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.040f - overlayRect.height * 0.002f, refWidth * 0.34f, overlayRect.height * 0.0048f), new Color(0.5f, 0.5f, 0.5f, 1f));	
			DrawRect(new Rect(refX + refWidth * 0.54f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.062f - overlayRect.height * 0.002f, refWidth * 0.34f, overlayRect.height * 0.0048f), new Color(0.5f, 0.5f, 0.5f, 1f));	
			DrawRect(new Rect(refX + refWidth * 0.54f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.084f - overlayRect.height * 0.002f, refWidth * 0.34f, overlayRect.height * 0.0048f), new Color(0.5f, 0.5f, 0.5f, 1f));	

			if (scoringSystem.GetPumaHealth(pumaNum) > 0f && scoringSystem.GetPumaHealth(pumaNum) < 1f) {
				float speed = GetPumaSpeed(pumaNum);
				Color speedColor = (speed > 0.66f) ? new Color(0f, 1f, 0f, 0.8f) : ((speed > 0.33f) ? new Color(1f, 1f, 0f, 0.85f) : new Color(1f, 0f, 0f, 1f));
				DrawRect(new Rect(refX + refWidth * 0.54f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.018f - overlayRect.height * 0.002f, refWidth * 0.34f * speed, overlayRect.height * 0.0048f), speedColor);	

				float stealth = GetPumaStealth(pumaNum);
				Color stealthColor = (stealth > 0.66f) ? new Color(0f, 1f, 0f,  0.8f) : ((stealth > 0.33f) ? new Color(1f, 1f, 0f, 0.85f) : new Color(1f, 0f, 0f, 1f));
				DrawRect(new Rect(refX + refWidth * 0.54f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.040f - overlayRect.height * 0.002f, refWidth * 0.34f * stealth, overlayRect.height * 0.0048f), stealthColor);	

				float endurance = GetPumaEndurance(pumaNum);
				Color enduranceColor = (endurance > 0.66f) ? new Color(0f, 1f, 0f,  0.8f) : ((endurance > 0.33f) ? new Color(1f, 1f, 0f, 0.85f) : new Color(1f, 0f, 0f, 1f));
				DrawRect(new Rect(refX + refWidth * 0.54f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.062f - overlayRect.height * 0.002f, refWidth * 0.34f * endurance, overlayRect.height * 0.0048f), enduranceColor);	

				float power = GetPumaPower(pumaNum);
				Color powerColor = (power > 0.66f) ? new Color(0f, 1f, 0f,  0.8f) : ((power > 0.33f) ? new Color(1f, 1f, 0f, 0.85f) : new Color(1f, 0f, 0f, 1f));
				DrawRect(new Rect(refX + refWidth * 0.54f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.084f - overlayRect.height * 0.002f, refWidth * 0.34f * power, overlayRect.height * 0.0048f), powerColor);	
			}
		}
				
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
	}
	
	
	void DrawDisplayBarsVert(int pumaNum, float barsX, float barsY, float barsWidth, float barsHeight) 
	{ 
		float rectWidth = barsWidth * 0.20f;
		float rect1X = barsX;
		float rect2X = barsX + barsWidth * 0.4f;
		float rect3X = barsX + barsWidth * 0.8f;
		float margin = rectWidth * 0.3f;
		
		// display bars for characteristics: backgrounds
		GUI.color = new Color(0f, 0f, 0f, 0.9f * guiOpacity);
		GUI.Box(new Rect(rect1X,  barsY, rectWidth, barsHeight), "");
		GUI.Box(new Rect(rect2X,  barsY, rectWidth, barsHeight), "");
		GUI.Box(new Rect(rect3X,  barsY, rectWidth, barsHeight), "");
		GUI.Box(new Rect(rect1X,  barsY, rectWidth, barsHeight), "");
		GUI.Box(new Rect(rect2X,  barsY, rectWidth, barsHeight), "");
		GUI.Box(new Rect(rect3X,  barsY, rectWidth, barsHeight), "");
		// display bars for characteristics: backgrounds again to darken them
		//GUI.Box(new Rect(headshotX + headshotWidth * 0.52f,  yOffset + headshotY + headshotHeight + overlayRect.height * 0.012f - overlayRect.height * 0.002f, headshotWidth * 0.38f, overlayRect.height * 0.012f), "");
		//GUI.Box(new Rect(headshotX + headshotWidth * 0.52f,  yOffset + headshotY + headshotHeight + overlayRect.height * 0.035f - overlayRect.height * 0.002f, headshotWidth * 0.38f, overlayRect.height * 0.012f), "");
		//GUI.Box(new Rect(headshotX + headshotWidth * 0.52f,  yOffset + headshotY + headshotHeight + overlayRect.height * 0.058f - overlayRect.height * 0.002f, headshotWidth * 0.38f, overlayRect.height * 0.012f), "");
		//GUI.Box(new Rect(headshotX + headshotWidth * 0.52f,  yOffset + headshotY + headshotHeight + overlayRect.height * 0.081f - overlayRect.height * 0.002f, headshotWidth * 0.38f, overlayRect.height * 0.012f), "");
		
		// display bars for characteristics: filler backgrounds
		Color barBackgroundColor = new Color(0.38f, 0.38f, 0.38f, 1f);	
		DrawRect(new Rect(rect1X+margin,  barsY+margin, rectWidth-margin*2f, barsHeight-margin*2f), barBackgroundColor);	
		DrawRect(new Rect(rect2X+margin,  barsY+margin, rectWidth-margin*2f, barsHeight-margin*2f), barBackgroundColor);	
		DrawRect(new Rect(rect3X+margin,  barsY+margin, rectWidth-margin*2f, barsHeight-margin*2f), barBackgroundColor);	
		
		// display bars for characteristics: fillers
		GUI.color = new Color(1f, 1f, 1f, 0.6f * guiOpacity);
		if (pumaNum < 6 && scoringSystem.GetPumaHealth(pumaNum) <= 0f)
			GUI.color = new Color(0.6f, 0.1f, 0.1f, 0.4f * guiOpacity);
		else if (pumaNum < 6 && scoringSystem.GetPumaHealth(pumaNum) >= 1f)
			//GUI.color = new Color(0.1f, 0.75f, 0.1f, 0.4f * guiOpacity);
			GUI.color = new Color(0.1f, 0.50f, 0f, 0.5f * guiOpacity);

		Color upperColor = new Color(0f, 0.75f, 0f, 1f);
		Color upperMiddleColor = new Color(0.5f * 1.04f, 0.7f * 1.04f, 0f, 1f);
		Color middleColor = new Color(0.85f * 0.99f, 0.85f * 0.99f, 0f, 1f);
		Color lowerMiddleColor = new Color(0.99f, 0.40f, 0f, 1f);
		Color lowerColor = new Color(0.86f, 0f, 0f, 1f);
		
		float buckCalories = scoringSystem.GetBuckCalories(pumaNum);
		float doeCalories  = scoringSystem.GetDoeCalories(pumaNum);
		float fawnCalories = scoringSystem.GetFawnCalories(pumaNum);
		
		if (buckCalories > 0f) {
			float buckExpenditures = scoringSystem.GetBuckExpenses(pumaNum);		
			float buckSuccess = 0f;
			if (buckCalories > buckExpenditures) {
				float percent = buckExpenditures / buckCalories;
				buckSuccess = 1f - percent * 0.5f;
			}
			else {
				float percent = buckCalories/ buckExpenditures;
				buckSuccess = percent * 0.5f;
			}
			buckSuccess = (buckSuccess < 0.05f) ? 0.05f : buckSuccess;
			Color buckColor = upperColor;
			if (buckSuccess < 0.2f)
				buckColor = lowerColor;
			else if (buckSuccess < 0.4f)
				buckColor = lowerMiddleColor;
			else if (buckSuccess < 0.6f)
				buckColor = middleColor;
			else if (buckSuccess < 0.8f)
				buckColor = upperMiddleColor;
			DrawRect(new Rect(rect1X+margin,  barsY+margin + (1f - buckSuccess) * (barsHeight-margin*2f), rectWidth-margin*2f, (barsHeight-margin*2f) * buckSuccess), buckColor);	
		}
		if (doeCalories > 0f) {
			float doeExpenditures = scoringSystem.GetDoeExpenses(pumaNum);		
			float doeSuccess = 0f;
			if (doeCalories > doeExpenditures) {
				float percent = doeExpenditures / doeCalories;
				doeSuccess = 1f - percent * 0.5f;
			}
			else {
				float percent = doeCalories/ doeExpenditures;
				doeSuccess = percent * 0.5f;
			}
			doeSuccess = (doeSuccess <  0.05f) ? 0.05f : doeSuccess;
			Color doeColor = upperColor;
			if (doeSuccess < 0.2f)
				doeColor = lowerColor;
			else if (doeSuccess < 0.4f)
				doeColor = lowerMiddleColor;
			else if (doeSuccess < 0.6f)
				doeColor = middleColor;
			else if (doeSuccess < 0.8f)
				doeColor = upperMiddleColor;
			DrawRect(new Rect(rect2X+margin,  barsY+margin + (1f - doeSuccess) * (barsHeight-margin*2f), rectWidth-margin*2f, (barsHeight-margin*2f) * doeSuccess), doeColor);	
		}
		if (fawnCalories > 0f) {
			float fawnExpenditures = scoringSystem.GetFawnExpenses(pumaNum);		
			float fawnSuccess = 0f;
			if (fawnCalories > fawnExpenditures) {
				float percent = fawnExpenditures / fawnCalories;
				fawnSuccess = 1f - percent * 0.5f;
			}
			else {
				float percent = fawnCalories/ fawnExpenditures;
				fawnSuccess = percent * 0.5f;
			}
			fawnSuccess = (fawnSuccess < 0.05f) ? 0.05f : fawnSuccess;
			Color fawnColor = upperColor;
			if (fawnSuccess < 0.2f)
				fawnColor = lowerColor;
			else if (fawnSuccess < 0.4f)
				fawnColor = lowerMiddleColor;
			else if (fawnSuccess < 0.6f)
				fawnColor = middleColor;
			else if (fawnSuccess < 0.8f)
				fawnColor = upperMiddleColor;
			DrawRect(new Rect(rect3X+margin,  barsY+margin + (1f - fawnSuccess) * (barsHeight-margin*2f), rectWidth-margin*2f, (barsHeight-margin*2f) * fawnSuccess), fawnColor);	
		}
		
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
	}
	
	

	/////////////////////
	////// UTILITIES
	/////////////////////
	

	void CalculateOverlayRect()
	{ 
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

	float GetPumaSpeed(int pumaNum)
	{
		return (speedArray[pumaNum]);
	}

	float GetPumaStealth(int pumaNum)
	{
		return (stealthArray[pumaNum]);
	}

	float GetPumaEndurance(int pumaNum)
	{
		return (enduranceArray[pumaNum]);
	}

	float GetPumaPower(int pumaNum)
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
