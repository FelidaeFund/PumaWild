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

	// STATE MACHINE FOR GUI
	public string guiState;
	private float guiStateStartTime;
	private float guiStateDuration;
	private float guiFadePercentComplete;
	private float guiOpacity;
	
	// INFO PANEL MANAGEMENT
	private bool infoPanelVisible = false;
	private float infoPanelTransStart;
	private float infoPanelTransTime;

	// MISC VARIABLES
	public int selectedPuma = -1;			// TEMP: !!! should not be public; need to consolidate in either LevelManager or GUIManager, with function call to get it	
	public GUISkin customGUISkin;
	private Rect overlayRect;
	
	// KEYBOARD TRACKING
	private bool spacePressed = false;
	private bool leftShiftPressed = false;
	private bool rightShiftPressed = false;
	private bool leftArrowPressed = false;
	private bool rightArrowPressed = false;
	
	// KEYBOARD DEBOUNCING
	private bool debounceSpace = false;
	private bool debounceLeftShift = false;
	private bool debounceRightShift = false;
	private bool debounceLeftArrow = false;
	private bool debounceRightArrow = false;

	// PUMA CHARACTERISTICS
	private float[] speedArray = new float[] {0.85f, 0.75f, 0.55f, 0.45f, 0.25f, 0.15f};
	private float[] stealthArray = new float[] {0.15f, 0.25f, 0.45f, 0.55f, 0.75f, 0.85f};
	private float[] enduranceArray = new float[] {0.85f, 0.75f, 0.55f, 0.45f, 0.25f, 0.15f};
	private float[] powerArray = new float[] {0.15f, 0.25f, 0.45f, 0.55f, 0.75f, 0.85f};

	// EXTERNAL IMAGE FILES 
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
	public Texture2D highwayTexture; 
	public Texture2D overpassTexture; 
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

	// EXTERNAL MODULES
	private GuiComponents guiComponents;		// TEMP: may not need this when this file has been fully pruned
	private LevelManager levelManager;
	private ScoringSystem scoringSystem;
	private InputControls inputControls;
	private OverlayPanel overlayPanel;	
	private InfoPanel infoPanel;	
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
		infoPanel = GetComponent<InfoPanel>();
		feedingDisplay = GetComponent<FeedingDisplay>();
		gameplayDisplay = GetComponent<GameplayDisplay>();
		
		// additional initialization
		SetGuiState("guiStateStartApp1");
		infoPanelVisible = true;
		infoPanelTransStart = Time.time - 100000;
		customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);
		customGUISkin.button.hover.textColor = new Color(0.99f, 0.75f, 0.21f, 1f);
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
			//SetGuiState("guiStateStartApp1");
			selectedPuma = 0;
			levelManager.SetSelectedPuma(selectedPuma);			
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
				infoPanel.ClearIntroFlag();
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
			if (SelectedPumaIsFullHealth() == false)
				CheckForKeyboardEscapeFromFeeding();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStateFeeding4");
			break;

		case "guiStateFeeding4":
			// brief pause
			guiStateDuration = 1f;
			if (SelectedPumaIsFullHealth() == false)
				CheckForKeyboardEscapeFromFeeding();
			if (Time.time > guiStateStartTime + guiStateDuration) {
				if (SelectedPumaIsFullHealth() == true)
					SetGuiState("guiStatePumaWins1");
				else
					SetGuiState("guiStateFeeding5");
			}
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
			
		//------------------------------
		// Puma Wins
		//
		// if during feeding
		// puma health reaches 100%
		//------------------------------

		case "guiStatePumaWins1":
			// fade-in of puma wins display
			guiStateDuration = 2f;
			FadeInOpacityLinear();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStatePumaWins2");
			break;

		case "guiStatePumaWins2":
			// brief pause
			guiStateDuration = 1f;
			CheckForKeyboardEscapeFromPumaWins();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStatePumaWins3");
			break;

		case "guiStatePumaWins3":
			// fade-in of puma wins 'ok' button
			guiStateDuration = 1f;
			FadeInOpacityLinear();
			CheckForKeyboardEscapeFromPumaWins();
			if (Time.time > guiStateStartTime + guiStateDuration)
				SetGuiState("guiStatePumaWins4");
			break;

		case "guiStatePumaWins4":
			// ongoing view of puma wins display
			CheckForKeyboardEscapeFromPumaWins();
			break;

		case "guiStateLeavingPumaWins":
			// fade-out of puma wins display
			guiStateDuration = 0.7f;
			FadeOutOpacityLinear();
			CheckForKeyboardSelectionOfPuma();
			if (Time.time > guiStateStartTime + guiStateDuration) {
				SetGuiState("guiStateEnteringOverlay");
				selectedPuma = -1;
				overlayPanel.SetCurrentScreen(0);
			}
			break;
		
		//------------------
		// Error Check
		//------------------
		
		default:
			Debug.Log("ERROR - GuiManager.Update() got bad state: " + guiState);
			break;
		}
	}


	//=====================================
	//=====================================
	//	  UTILITIES USED BY UPDATE()
	//=====================================
	//=====================================


	private void CheckForKeyboardEscapeFromGameplay()
	{
		if (spacePressed || rightShiftPressed) {
			// use keyboard to leave gameplay
			SetGuiState("guiStateLeavingGameplay");
			levelManager.SetGameState("gameStateLeavingGameplay");
		}	
	}
	
	private void CheckForKeyboardEscapeFromOverlay()
	{
		if (selectedPuma >= 0 && (spacePressed || rightShiftPressed)) {
			// use keyboard to leave overlay
			SetGuiState("guiStateLeavingOverlay");
			levelManager.SetGameState("gameStateLeavingGui");
		}	
	}
	
	private void CheckForKeyboardEscapeFromFeeding()
	{
		if (spacePressed || rightShiftPressed) {
			// use keyboard to resume gameplay
			SetGuiState("guiStateFeeding7");
			levelManager.SetGameState("gameStateFeeding5");
		}
	}

	private void CheckForKeyboardEscapeFromPumaWins()
	{
		if (spacePressed || rightShiftPressed) {
			// use keyboard to go to overlay
			SetGuiState("guiStateLeavingPumaWins");
			levelManager.SetGameState("gameStateLeavingGameplay");
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
	
	////////////////////////////////
	////////////////////////////////
	////////////////////////////////
	
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
	
		if (infoPanelVisible == false || Time.time - infoPanelTransStart < infoPanelTransTime * 0.5f) {
		
			// DRAW SOMETHING - info panel is either not showing, or in first half of fade-in

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
				gameplayDisplay.Draw(guiOpacity, 0f, 0f);
				break;
				
			case "guiStateEnteringGameplay3":
				// fade-in of position indicators
				gameplayDisplay.Draw(1f, guiOpacity, 0f);
				break;
				
			case "guiStateEnteringGameplay4":
				// brief pause
				gameplayDisplay.Draw(1f, 1f, 0f);
				break;
				
			case "guiStateEnteringGameplay5":
				// fade-in of status displays
				gameplayDisplay.Draw(1f, 1f, guiOpacity);
				break;
				
			case "guiStateGameplay":
				// ongoing game-play state
				gameplayDisplay.Draw(1f, 1f, 1f);
				break;
				
			case "guiStateLeavingGameplay":
				// fade-out of game-play controls
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
				gameplayDisplay.Draw(guiOpacity, guiOpacity, guiOpacity);
				break;

			case "guiStateFeeding2":
				// no GUI display during attack on deer
				break;

			case "guiStateFeeding3":
				// fade-in of feeding display main panel
				feedingDisplay.Draw(guiOpacity, guiOpacity, 0f, 0f);
				break;

			case "guiStateFeeding4":
				// brief pause
				feedingDisplay.Draw(1f, 1f, 0f, 0f);
				break;

			case "guiStateFeeding5":
				// fade-in of feeding display 'ok' button
				feedingDisplay.Draw(1f, 1f, guiOpacity, 0f);
				break;

			case "guiStateFeeding6":
				// ongoing view of feeding display
				feedingDisplay.Draw(1f, 1f, 1f, 0f);
				break;

			case "guiStateFeeding7":
				// fade-out of feeding display
				feedingDisplay.Draw(guiOpacity, guiOpacity, guiOpacity, 0f);
				break;

			case "guiStateFeeding8":
				// fade-in of movement controls
				gameplayDisplay.Draw(guiOpacity, 0f, 0f);
				break;
				
			case "guiStateFeeding9":
				// fade-in of position indicators
				gameplayDisplay.Draw(1f, guiOpacity, 0f);
				break;
				
			case "guiStateFeeding10":
				// brief pause
				gameplayDisplay.Draw(1f, 1f, 0f);
				break;
				
			case "guiStateFeeding11":
				// fade-in of status indicators
				gameplayDisplay.Draw(1f, 1f, guiOpacity);
				break;
				
			//------------------------------
			// Puma Wins
			//
			// if during feeding
			// puma health reaches 100%
			//------------------------------

			case "guiStatePumaWins1":
				// fade-out of feeding display main content
				// fade-in of puma wins display
				feedingDisplay.Draw(1f, 1f - guiOpacity, 0f, guiOpacity);
				break;
				
			case "guiStatePumaWins2":
				// brief pause
				feedingDisplay.Draw(1f, 0f, 0f, 1f);
				break;
				
			case "guiStatePumaWins3":
				// fade-in of puma wins 'ok' button
				feedingDisplay.Draw(1f, 0f, guiOpacity, 1f);
				break;
				
			case "guiStatePumaWins4":
				// ongoing view of puma wins display
				feedingDisplay.Draw(1f, 0f, 1f, 1f);
				break;
				
			case "guiStateLeavingPumaWins":
				// fade-out of puma wins display
				feedingDisplay.Draw(guiOpacity, 0f, guiOpacity, guiOpacity);
				break;
			}
		}
		
		//------------------------------
		// Draw Info Panel
		//------------------------------
				
		float elapsedTime = Time.time - infoPanelTransStart;
		float percentVisible = 0f;
		
		if (infoPanelVisible == true && elapsedTime < infoPanelTransTime) {
			// fading in
			percentVisible = elapsedTime / infoPanelTransTime;			
			infoPanel.Draw(percentVisible);
		}
		else if (infoPanelVisible == true) {
			// fully visible
			infoPanel.Draw(1f);
		}
		else if (elapsedTime < infoPanelTransTime) {
			// fading out		
			percentVisible = 1f - elapsedTime / infoPanelTransTime;
			infoPanel.Draw(percentVisible);
		}

		//------------------------------
		// Frame Rate Display
		//------------------------------

		if (displayFrameRate == true && levelManager != null) {
			GUI.color = Color.white;

			int msec = levelManager.frameAverageDuration;
			GUI.Box(new Rect(Screen.width * 0.24f - 80f, 0, 160, 24), "Avg Frame time: " + msec.ToString());		

			GUI.Box(new Rect(Screen.width * 0.50f - 80f, 0, 160, 24), "Screen Res: " + Screen.width.ToString() + "x" + Screen.height.ToString());				
			
			int averageFrameRate = (levelManager.frameAverageDuration == 0) ? 0 : (int)(1000 / levelManager.frameAverageDuration);
			GUI.Box(new Rect(Screen.width * 0.76f - 80f, 0, 160, 24), "Avg Frame rate: " + averageFrameRate.ToString());		

			GUI.Box(new Rect(Screen.width * 0.24f - 80f, Screen.height - 24, 160, 24), "displayVar1:  " + levelManager.displayVar1.ToString());		
			GUI.Box(new Rect(Screen.width * 0.50f - 80f, Screen.height - 24, 160, 24), "displayVar2:  " + levelManager.displayVar2.ToString());				
			GUI.Box(new Rect(Screen.width * 0.76f - 80f, Screen.height - 24, 160, 24), "displayVar3:  " + levelManager.displayVar3.ToString());		
		}		
	}	
 
	//=====================================
	//=====================================
	//	UTILITY AND CONVENIENCE FUNCTIONS
	//=====================================
	//=====================================

	public void SetGuiState(string newState) 
	{	
		guiState = newState;
		guiStateStartTime = Time.time;
		Update();
		Debug.Log("NEW GUI STATE: " + newState);
	}

	public void OpenInfoPanel(int newPageNum)
	{
		if (newPageNum >= 0)
			infoPanel.SetCurrentPage(newPageNum);
		infoPanelTransTime = 0.3f;
		infoPanelTransStart = Time.time;
		infoPanelVisible = true;
	}
 
 	public void CloseInfoPanel(bool slowFlag = false)
	{
		infoPanelTransTime = slowFlag ? 0.6f : 0.3f;
		infoPanelTransStart = Time.time;
		infoPanelVisible = false;
	}
	
	//////////////////
	//////////////////

	public Rect GetOverlayRect()
	{
		return overlayRect;
	}

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

	//////////////////
	//////////////////

	bool SelectedPumaIsFullHealth()
	{	
		if (scoringSystem.GetPumaHealth(selectedPuma) >= 1f)
			return true;
			
		return false;
	}
	
		
	bool PumaIsSelectable(int pumaNum)
	{	
		if (scoringSystem.GetPumaHealth(pumaNum) <= 0f)
			return false;
			
		if (scoringSystem.GetPumaHealth(pumaNum) >= 1f)
			return false;
			
		return true;
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
	
	//////////////////
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

	//////////////////
	//////////////////

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
