using UnityEngine;
using System.Collections;

/// Main handling of all user interface elements
/// 

public class GUIManager : MonoBehaviour 
{
	// DEBUGGING OPTIONS
	private bool displayFrameRate = false;
	private int startCount = 0;

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
	private float stateStartTime;
	private float guiFadeTime;
	private float guiFadePercentComplete;
	
	public GUISkin customGUISkin;
	private float guiOpacity = 1f;
	private float guiBackdropOpacity = 1f;
	private float levelDisplayOpacity = 1f;
	private float statusDisplayOpacity = 1f;
	private float feedingDisplayStartTime;
	
	private bool popupPanelVisible = false;
	private int popupPanelPage = 0;
	private float popupPanelTransStart;
	private float popupPanelTransTime;
	private bool popupPanelIntroFlag;

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

	public Texture2D iconFacebookTexture; 
	public Texture2D iconTwitterTexture; 
	public Texture2D iconGoogleTexture; 
	public Texture2D iconPinterestTexture; 
	public Texture2D iconYouTubeTexture; 
	public Texture2D iconLinkedInTexture; 

	private int currentScreen = 0;
	private int currentLevel = 0;
	private int pumasAlive = 6;
	private int selectedPuma = -1;
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
	private Texture2D rectTexture;		
	private GUIStyle rectStyle;
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
	private LevelManager levelManager;
	private PositionIndicator positionIndicator;
	private ScoringSystem scoringSystem;

	//===================================
	//===================================
	//		INITIALIZATION
	//===================================
	//===================================

	void Start() 
	{	
		// connect to external modules
		levelManager = GetComponent<LevelManager>();
		positionIndicator = GetComponent<PositionIndicator>();
		scoringSystem = GetComponent<ScoringSystem>();
		
		// initialize state
		SetGuiState("guiStateEnteringApp1");
		popupPanelVisible = true;
		popupPanelTransStart = Time.time - 100000;
		popupPanelTransTime = 0.3f;
		popupPanelIntroFlag = true;
		//SetGuiState("guiStateEnteringApp1");
		//SetGuiState("guiStateCaught4");
		startCount = 1;
		

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
		stateStartTime = Time.time;
		
		
		//System.Console.WriteLine("SET STATE: " + newState);	
		
	}
	
	void Update() 
	{	
		DebounceKeyboardInput();
	
		// initial state override
	
		if (false && startCount > 10) {
			SetGuiState("guiStateLeavingOverlay");
			levelManager.SetGameState("gameStateLeavingGui");
			startCount = 0;
		}
		else if (startCount != 0) {
			startCount++;
		}
		
		// detect caught condition
	
		if (guiState != "guiStateCaught1" && guiState != "guiStateCaught2" && levelManager.IsCaughtState() == true) {
			SetGuiState("guiStateCaught1");
		}
		
		// MAIN PROCESSING
		
		switch (guiState) {

		case "guiStateEnteringApp1":
			guiFadeTime = 0f;
			if (Time.time - stateStartTime < guiFadeTime) {

			}
			else {
				SetGuiState("guiStateEnteringApp2");
			}
			break;

		case "guiStateEnteringApp2":
			popupPanelTransTime = 0.6f;
			if (selectedPuma >= 0 && (spacePressed || leftShiftPressed || rightShiftPressed)) {
				// use keyboard to go to overlay screen
				popupPanelVisible = false;
				popupPanelTransStart = Time.time;
				SetGuiState("guiStateEnteringApp3");
			}	
			break;

		case "guiStateEnteringApp3":
			guiFadeTime = 1.2f;
			if (Time.time - stateStartTime < guiFadeTime) {

			}
			else {
				guiOpacity = 0f;
				guiBackdropOpacity = 0f;
				SetGuiState("guiStateEnteringOverlay1");
			}
			break;

		case "guiStateEnteringGameplay1":
			popupPanelTransTime = 0.3f;
			guiFadeTime = 1.5f;
			if (Time.time - stateStartTime > guiFadeTime) {
				SetGuiState("guiStateEnteringGameplay2");
				guiOpacity = 0f;
			}
			break;
			
		case "guiStateEnteringGameplay2":
			guiFadeTime = 0.4f;
			if (Time.time - stateStartTime > guiFadeTime) {
				SetGuiState("guiStateEnteringGameplay3");
				guiOpacity = 0f;
			}
			break;
			
		case "guiStateEnteringGameplay3":
			guiFadeTime = 1.8f;
			if (Time.time - stateStartTime < (guiFadeTime * 0.5f)) {
				guiFadePercentComplete = (Time.time - stateStartTime) / (guiFadeTime * 0.5f);
				guiFadePercentComplete = guiFadePercentComplete * guiFadePercentComplete;
				guiOpacity = guiFadePercentComplete * 0.5f;
			}
			else if (Time.time - stateStartTime < guiFadeTime) {
				guiFadePercentComplete = ((Time.time - stateStartTime) - (guiFadeTime * 0.5f)) / (guiFadeTime * 0.5f);				
				guiFadePercentComplete = guiFadePercentComplete + (guiFadePercentComplete - (guiFadePercentComplete * guiFadePercentComplete));
				guiOpacity = 0.5f + guiFadePercentComplete * 0.5f;
			}
			else {
				SetGuiState("guiStateEnteringGameplay4");
			}
			break;
			
		case "guiStateEnteringGameplay4":
			guiFadeTime = 0.0f;
			if (Time.time - stateStartTime < (guiFadeTime * 0.5f)) {
				guiFadePercentComplete = (Time.time - stateStartTime) / (guiFadeTime * 0.5f);
				guiFadePercentComplete = guiFadePercentComplete * guiFadePercentComplete;
				guiOpacity = guiFadePercentComplete * 0.5f;
			}
			else if (Time.time - stateStartTime < guiFadeTime) {
				guiFadePercentComplete = ((Time.time - stateStartTime) - (guiFadeTime * 0.5f)) / (guiFadeTime * 0.5f);				
				guiFadePercentComplete = guiFadePercentComplete + (guiFadePercentComplete - (guiFadePercentComplete * guiFadePercentComplete));
				guiOpacity = 0.5f + guiFadePercentComplete * 0.5f;
			}
			else {
				SetGuiState("guiStateEnteringGameplay5");
			}
			break;
			
		case "guiStateEnteringGameplay5":
			guiFadeTime = 0.0f;
			if (Time.time - stateStartTime > guiFadeTime) {
				SetGuiState("guiStateEnteringGameplay6");
				guiOpacity = 0f;
			}
			break;
			
		case "guiStateEnteringGameplay6":
			guiFadeTime = 0.7f;
			if (spacePressed || rightShiftPressed) {
				// use keyboard to leave gameplay
				SetGuiState("guiStateLeavingGameplay");
				levelManager.SetGameState("gameStateLeavingGameplay");
			}	
			if (Time.time - stateStartTime < (guiFadeTime * 0.5f)) {
				guiFadePercentComplete = (Time.time - stateStartTime) / (guiFadeTime * 0.5f);
				guiFadePercentComplete = guiFadePercentComplete * guiFadePercentComplete;
				guiOpacity = guiFadePercentComplete * 0.5f;
			}
			else if (Time.time - stateStartTime < guiFadeTime) {
				guiFadePercentComplete = ((Time.time - stateStartTime) - (guiFadeTime * 0.5f)) / (guiFadeTime * 0.5f);				
				guiFadePercentComplete = guiFadePercentComplete + (guiFadePercentComplete - (guiFadePercentComplete * guiFadePercentComplete));
				guiOpacity = 0.5f + guiFadePercentComplete * 0.5f;
			}
			else {
				SetGuiState("guiStateEnteringGameplay7");
			}
			break;
			
		case "guiStateEnteringGameplay7":
			guiFadeTime = 0.2f;
			if (spacePressed || rightShiftPressed) {
				// use keyboard to leave gameplay
				SetGuiState("guiStateLeavingGameplay");
				levelManager.SetGameState("gameStateLeavingGameplay");
			}	
			if (Time.time - stateStartTime > guiFadeTime) {
				SetGuiState("guiStateEnteringGameplay8");
				guiOpacity = 0f;
			}
			break;
			
		case "guiStateEnteringGameplay8":
			//float guiFadeInTime = 1.2f;
			//float guiFadeOutTime = 3.2f;
			//float fadeOutStartTime = stateStartTime + guiFadeInTime;
			guiFadeTime = 1.2f;
			if (spacePressed || rightShiftPressed) {
				// use keyboard to leave gameplay
				SetGuiState("guiStateLeavingGameplay");
				levelManager.SetGameState("gameStateLeavingGameplay");
			}	
			if (Time.time - stateStartTime < guiFadeTime) {
				guiOpacity = (Time.time - stateStartTime) / guiFadeTime;
			}
			//if (Time.time - stateStartTime < guiFadeInTime) {
				//guiOpacity = (Time.time - stateStartTime) / guiFadeInTime;
			//}
			//else if (Time.time - stateStartTime < (guiFadeInTime + guiFadeOutTime)) {
				//guiOpacity = 1f - (Time.time - fadeOutStartTime) / guiFadeOutTime;
			//}
			
			/*
			if (Time.time - stateStartTime < (guiFadeTime * 0.5f)) {
				guiFadePercentComplete = (Time.time - stateStartTime) / (guiFadeTime * 0.5f);
				guiFadePercentComplete = guiFadePercentComplete * guiFadePercentComplete;
				guiOpacity = guiFadePercentComplete * 0.5f;
			}
			else if (Time.time - stateStartTime < guiFadeTime) {
				guiFadePercentComplete = ((Time.time - stateStartTime) - (guiFadeTime * 0.5f)) / (guiFadeTime * 0.5f);				
				guiFadePercentComplete = guiFadePercentComplete + (guiFadePercentComplete - (guiFadePercentComplete * guiFadePercentComplete));
				guiOpacity = 0.5f + guiFadePercentComplete * 0.5f;
			}
			*/
			else {
				//guiOpacity = 1.0f;
				SetGuiState("guiStateGameplay");
			}
			break;
			
		case "guiStateGameplay":
			if (spacePressed || rightShiftPressed) {
				// use keyboard to leave gameplay
				SetGuiState("guiStateLeavingGameplay");
				levelManager.SetGameState("gameStateLeavingGameplay");
			}	
			break;
			
		case "guiStateLeavingGameplay":
			guiFadeTime = 0.7f;
			if (currentScreen == 0 || currentScreen == 2) {
				// select or stat screen
				if (leftArrowPressed)
					DecrementPuma();
				if (rightArrowPressed)
					IncrementPuma();
			}
			if (Time.time - stateStartTime < guiFadeTime) {		
				guiFadePercentComplete = (Time.time - stateStartTime) / guiFadeTime;
				guiOpacity = 1f - guiFadePercentComplete;
			}
			else {
				SetGuiState("guiStateEnteringOverlay1");
				if (currentScreen == 3) {
					// return to select screen rather than quit screen
					currentScreen = 0;
				}
				if (levelManager.GetPumaHealth(selectedPuma) <= 0f) {
					// puma has died
					selectedPuma = -1;
					currentScreen = 0;
				}
				guiFadePercentComplete = 0f;
				guiBackdropOpacity = 0f;
			}
			break;
		
		case "guiStateEnteringOverlay1":
			guiFadeTime = 1.6f;
			if (selectedPuma >= 0 && (spacePressed || leftShiftPressed || rightShiftPressed)) {
				// use keyboard to leave overlay
				SetGuiState("guiStateLeavingOverlay");
				levelManager.SetGameState("gameStateLeavingGui");
			}	
			if (currentScreen == 0 || currentScreen == 2) {
				// select or stat screen
				if (leftArrowPressed)
					DecrementPuma();
				if (rightArrowPressed)
					IncrementPuma();
			}
			if (Time.time - stateStartTime < (guiFadeTime * 0.5f)) {
				guiBackdropOpacity = (Time.time - stateStartTime) / guiFadeTime;
				guiFadePercentComplete = (Time.time - stateStartTime) / (guiFadeTime * 0.5f);
				guiFadePercentComplete = guiFadePercentComplete * guiFadePercentComplete;
				guiOpacity = guiFadePercentComplete * 0.5f;
			}
			else if (Time.time - stateStartTime < guiFadeTime) {
				guiBackdropOpacity = (Time.time - stateStartTime) / guiFadeTime;
				guiFadePercentComplete = ((Time.time - stateStartTime) - (guiFadeTime * 0.5f)) / (guiFadeTime * 0.5f);				
				guiFadePercentComplete = guiFadePercentComplete + (guiFadePercentComplete - (guiFadePercentComplete * guiFadePercentComplete));
				guiOpacity = 0.5f + guiFadePercentComplete * 0.5f;
			}
			else {
				SetGuiState("guiStateEnteringOverlay2");
				guiFadePercentComplete = 0f;
			}
			break;

		case "guiStateEnteringOverlay2":
			guiFadeTime = 0.4f;
			if (selectedPuma >= 0 && (spacePressed || leftShiftPressed || rightShiftPressed)) {
				// use keyboard to leave overlay
				SetGuiState("guiStateLeavingOverlay");
				levelManager.SetGameState("gameStateLeavingGui");
			}
			if (currentScreen == 0 || currentScreen == 2) {
				// select or stat screen
				if (leftArrowPressed)
					DecrementPuma();
				if (rightArrowPressed)
					IncrementPuma();
			}
			if (Time.time - stateStartTime < guiFadeTime) {		
				guiFadePercentComplete = (Time.time - stateStartTime) / guiFadeTime;
			}
			else {
				guiFadePercentComplete = 1f;
				guiOpacity = 1f;
				GUI.color = new Color(1f, 1f, 1f, 1f);
				SetGuiState("guiStateOverlay");
			}
			break;

		case "guiStateOverlay":
			popupPanelIntroFlag = false;
			if (selectedPuma >= 0 && (spacePressed || leftShiftPressed || rightShiftPressed)) {
				// use keyboard to leave overlay
				SetGuiState("guiStateLeavingOverlay");
				levelManager.SetGameState("gameStateLeavingGui");
			}	
			if (currentScreen == 0 || currentScreen == 2) {
				// select or stat screen
				if (leftArrowPressed)
					DecrementPuma();
				if (rightArrowPressed)
					IncrementPuma();
			}
			break;

		case "guiStateLeavingOverlay":
			guiFadeTime = 1f;
			if (Time.time - stateStartTime < (guiFadeTime * 0.5f)) {
				guiBackdropOpacity = 1f - (Time.time - stateStartTime) / guiFadeTime;
				guiFadePercentComplete = (Time.time - stateStartTime) / (guiFadeTime * 0.5f);
				guiFadePercentComplete = guiFadePercentComplete * guiFadePercentComplete;
				guiOpacity =  1f - (guiFadePercentComplete * 0.5f);
			}
			else if (Time.time - stateStartTime < guiFadeTime) {
				guiBackdropOpacity =  1f - (Time.time - stateStartTime) / guiFadeTime;
				guiFadePercentComplete = ((Time.time - stateStartTime) - (guiFadeTime * 0.5f)) / (guiFadeTime * 0.5f);				
				guiFadePercentComplete = guiFadePercentComplete + (guiFadePercentComplete - (guiFadePercentComplete * guiFadePercentComplete));
				guiOpacity =  1f - (0.5f + guiFadePercentComplete * 0.5f);
			}
			else {
				guiFadePercentComplete = 1f;
				guiOpacity = 1f;
				GUI.color = new Color(1f, 1f, 1f, 1f);
				SetGuiState("guiStateEnteringGameplay1");
			}
			break;
			
		//=============
		//=============		
		
		case "guiStateCaught1":
			guiFadeTime = 1f;
			if (Time.time - stateStartTime < guiFadeTime) {		
				guiFadePercentComplete = (Time.time - stateStartTime) / guiFadeTime;
				guiOpacity = 1f - guiFadePercentComplete;
			}
			else {
				guiOpacity = 0f;
				SetGuiState("guiStateCaught2");
			}
			break;

		case "guiStateCaught2":
			guiFadeTime = 2f;
			if (Time.time - stateStartTime > guiFadeTime) {		
				SetGuiState("guiStateCaught3");
				feedingDisplayStartTime = Time.time;
			}
			break;

		case "guiStateCaught3":
			guiFadeTime = 1f;
			if (spacePressed || leftShiftPressed || rightShiftPressed) {
				// use keyboard to resume gameplay
				SetGuiState("guiStateCaught5");
				levelManager.SetGameState("gameStateCaught5");
			}
			if (Time.time - stateStartTime < guiFadeTime) {		
				guiFadePercentComplete = (Time.time - stateStartTime) / guiFadeTime;
				guiOpacity = guiFadePercentComplete;
			}
			else {
				guiOpacity = 1f;
				SetGuiState("guiStateCaught4");
			}
			break;

		case "guiStateCaught4":
			if (spacePressed || leftShiftPressed || rightShiftPressed) {
				// use keyboard to resume gameplay
				SetGuiState("guiStateCaught5");
				levelManager.SetGameState("gameStateCaught5");
			}
			break;

		case "guiStateCaught5":
			guiFadeTime = 2f;
			if (Time.time - stateStartTime < guiFadeTime) {		
				guiFadePercentComplete = (Time.time - stateStartTime) / guiFadeTime;
				guiOpacity = 1f - guiFadePercentComplete;
			}
			else {
				guiOpacity = 0f;
				SetGuiState("guiStateCaught6");
			}
			break;

		case "guiStateCaught6":
			guiFadeTime = 0.5f;
			if (selectedPuma >= 0 && (spacePressed || leftShiftPressed || rightShiftPressed)) {
				// use keyboard to leave gameplay
				SetGuiState("guiStateLeavingGameplay");
				levelManager.SetGameState("gameStateLeavingGameplay");
			}	
			if (Time.time - stateStartTime < guiFadeTime) {		
				guiFadePercentComplete = (Time.time - stateStartTime) / guiFadeTime;
				guiOpacity = guiFadePercentComplete;
			}
			else {
				guiOpacity = 0f;
				SetGuiState("guiStateCaught7");
			}
			break;

		case "guiStateCaught7":
			guiFadeTime = 1f;
			if (selectedPuma >= 0 && (spacePressed || leftShiftPressed || rightShiftPressed)) {
				// use keyboard to leave gameplay
				SetGuiState("guiStateLeavingGameplay");
				levelManager.SetGameState("gameStateLeavingGameplay");
			}	
			if (Time.time - stateStartTime < (guiFadeTime * 0.5f)) {
				guiFadePercentComplete = (Time.time - stateStartTime) / (guiFadeTime * 0.5f);
				guiFadePercentComplete = guiFadePercentComplete * guiFadePercentComplete;
				guiOpacity = guiFadePercentComplete * 0.5f;
			}
			else if (Time.time - stateStartTime < guiFadeTime) {
				guiFadePercentComplete = ((Time.time - stateStartTime) - (guiFadeTime * 0.5f)) / (guiFadeTime * 0.5f);				
				guiFadePercentComplete = guiFadePercentComplete + (guiFadePercentComplete - (guiFadePercentComplete * guiFadePercentComplete));
				guiOpacity = 0.5f + guiFadePercentComplete * 0.5f;
			}
			else {
				SetGuiState("guiStateCaught8");
			}
			break;
			
		case "guiStateCaught8":
			guiFadeTime = 0.1f;
			if (selectedPuma >= 0 && (spacePressed || leftShiftPressed || rightShiftPressed)) {
				// use keyboard to leave gameplay
				SetGuiState("guiStateLeavingGameplay");
				levelManager.SetGameState("gameStateLeavingGameplay");
			}	
			if (Time.time - stateStartTime > guiFadeTime) {
				SetGuiState("guiStateCaught9");
				guiOpacity = 0f;
			}
			break;
			
		case "guiStateCaught9":
			guiFadeTime = 0.7f;
			if (selectedPuma >= 0 && (spacePressed || leftShiftPressed || rightShiftPressed)) {
				// use keyboard to leave gameplay
				SetGuiState("guiStateLeavingGameplay");
				levelManager.SetGameState("gameStateLeavingGameplay");
			}	
			if (Time.time - stateStartTime < guiFadeTime) {
				guiOpacity = (Time.time - stateStartTime) / guiFadeTime;
			}
			else {
				SetGuiState("guiStateGameplay");
			}
			break;
		//=============
		//=============		
		
		}
	}


	void OnGUI()
	{	
		CalculateOverlayRect();

		switch (guiState) {
	
		case "guiStateEnteringGameplay2":
		case "guiStateEnteringGameplay3":
		case "guiStateEnteringGameplay4":
		case "guiStateEnteringGameplay5":
		case "guiStateEnteringGameplay6":
		case "guiStateEnteringGameplay7":
		case "guiStateEnteringGameplay8":
		case "guiStateEnteringGameplay9":
		case "guiStateGameplay":
		case "guiStateLeavingGameplay":
		case "guiStateCaught1":
		case "guiStateCaught6":
		case "guiStateCaught7":
		case "guiStateCaught8":
		case "guiStateCaught9":
			if (popupPanelVisible == false || Time.time - popupPanelTransStart < popupPanelTransTime * 0.5f)
				CreateGameplayDisplay();
			break;


		case "guiStateCaught3":
		case "guiStateCaught4":
		case "guiStateCaught5":
			if (popupPanelVisible == false || Time.time - popupPanelTransStart < popupPanelTransTime * 0.5f)
				CreateFeedingDisplay((Screen.width / 2) - (Screen.height * 0.7f), Screen.height * 0.025f, Screen.height * 1.4f, Screen.height * 0.37f);
			break;
			
		case "guiStateEnteringOverlay1":
			GUI.color = new Color(1f, 1f, 1f, 1f * ((guiBackdropOpacity > 0.5f) ? ((guiBackdropOpacity - 0.5f) * 1.5f) : 0f));
			//DrawRect(new Rect(0,0,Screen.width,Screen.height), new Color(0.06f, 0.07f, 0.06f, 0.6f));
			if (popupPanelVisible == false || Time.time - popupPanelTransStart < popupPanelTransTime * 0.5f)
				CreateOverlayPanel();
			break;

		case "guiStateEnteringOverlay2":
			guiOpacity = 0.75f + (guiFadePercentComplete * 0.25f);
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			//DrawRect(new Rect(0,0,Screen.width,Screen.height), new Color(0.06f, 0.07f, 0.06f, 0.6f));
			guiOpacity = 1f;
			if (popupPanelVisible == false || Time.time - popupPanelTransStart < popupPanelTransTime * 0.5f)
				CreateOverlayPanel();
			break;

		case "guiStateOverlay":
			GUI.color = new Color(1f, 1f, 1f, 1f);
			//DrawRect(new Rect(0,0,Screen.width,Screen.height), new Color(0.06f, 0.07f, 0.06f, 0.6f));
			if (popupPanelVisible == false || Time.time - popupPanelTransStart < popupPanelTransTime * 0.5f)
				CreateOverlayPanel();
			break;

		case "guiStateLeavingOverlay":
			GUI.color = new Color(1f, 1f, 1f,  1f * guiOpacity);
			//DrawRect(new Rect(0,0,Screen.width,Screen.height), new Color(0.06f, 0.07f, 0.06f, 0.6f));
			if (popupPanelVisible == false || Time.time - popupPanelTransStart < popupPanelTransTime * 0.5f)
				CreateOverlayPanel();
			break;
		}

		// frame rate display
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
		
		
		// draw popup panel
				
		float elapsedTime = Time.time - popupPanelTransStart;
		float percentVisible = 0f;
		
		if (popupPanelVisible == true && elapsedTime < popupPanelTransTime) {
			// sliding in
			percentVisible = elapsedTime / popupPanelTransTime;			
			DrawPopupPanel(percentVisible);
		}
		else if (popupPanelVisible == true) {
			// fully open
			DrawPopupPanel(1f);
		}
		else if (elapsedTime < popupPanelTransTime) {
			// sliding out		
			percentVisible = 1f - elapsedTime / popupPanelTransTime;
			DrawPopupPanel(percentVisible);
		}


	}	
 
 
 	void CreateGameplayDisplay() 
	{ 
		float actualGuiOpacity = guiOpacity;
		float prevGuiOpacity;
		
		if (guiState == "guiStateEnteringGameplay2" || guiState == "guiStateEnteringGameplay3" || guiState == "guiStateEnteringGameplay4" || guiState == "guiStateEnteringGameplay5" || guiState == "guiStateEnteringGameplay6" || guiState == "guiStateEnteringGameplay7" || guiState == "guiStateEnteringGameplay8" || guiState == "guiStateCaught6" || guiState == "guiStateCaught7" || guiState == "guiStateCaught8" || guiState == "guiStateCaught9")
			guiOpacity = 0f;
	
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;

		// establish scale factor for movement controls tray and health meter / exit button
		float boxWidth;
		float boxHeight;
		float boxMargin;
		float width = (float)Screen.width;
		float height = (float)Screen.height;
		float aspectRatio = width/height;
		if (aspectRatio > 2f) {
			// wide screen -- height controls size
			//System.Console.WriteLine("WIDE SCREEN width: " + Screen.width.ToString() + "  height: " + Screen.height.ToString());	
			boxWidth = Screen.height * 0.30f * 1.4f;
			boxHeight = arrowTrayTexture.height * (boxWidth / arrowTrayTexture.width);
			boxMargin = Screen.height * 0.150f * 0.5f;
		}
		else {
			// tall screen -- width controls size			
			//System.Console.WriteLine("TALL SCREEN width: " + Screen.width.ToString() + "  height: " + Screen.height.ToString());	
			boxWidth = Screen.width * 0.15f * 1.4f;
			boxHeight = arrowTrayTexture.height * (boxWidth / arrowTrayTexture.width);
			boxMargin = Screen.width * 0.075f * 0.5f;
		}
		GUI.color = new Color(0f, 0f, 0f, 0f * guiOpacity);
		GUI.Box(new Rect(boxMargin,  Screen.height - boxHeight - boxMargin, boxWidth, boxHeight), "");
		GUI.Box(new Rect(Screen.width - boxWidth - boxMargin,  Screen.height - boxHeight - boxMargin, boxWidth, boxHeight), "");
		
		prevGuiOpacity = guiOpacity;
		if (guiState == "guiStateEnteringGameplay6" || guiState == "guiStateEnteringGameplay7" || guiState == "guiStateCaught7")
			guiOpacity = actualGuiOpacity;
		else if (guiState == "guiStateEnteringGameplay8" || guiState == "guiStateCaught8" || guiState == "guiStateCaught9")
			guiOpacity = 1f;

		// outer edge display
		GUI.color = new Color(1f, 1f, 1f, 0.9f * guiOpacity);
		int borderThickness = (int)(boxMargin * 0.8f);//(int)(Screen.width * 0.026f);
		Color edgeColor = new Color(0f, 0f, 0f, 0.35f);	
		DrawRect(new Rect(0, 0, Screen.width, borderThickness), edgeColor);
		DrawRect(new Rect(0, Screen.height-borderThickness, Screen.width, borderThickness), edgeColor);
		DrawRect(new Rect(0, borderThickness, borderThickness, Screen.height-(borderThickness*2)), edgeColor);
		DrawRect(new Rect(Screen.width-borderThickness, borderThickness, borderThickness, Screen.height-(borderThickness*2)), edgeColor);

		// deer head indicators
		if (levelManager.buck != null && levelManager.doe != null && levelManager.fawn != null ) {
			positionIndicator.DrawIndicator(levelManager.cameraRotY, levelManager.pumaObj, levelManager.buck.gameObj, levelManager.buck.type, borderThickness, guiOpacity);
			positionIndicator.DrawIndicator(levelManager.cameraRotY, levelManager.pumaObj, levelManager.doe.gameObj, levelManager.doe.type, borderThickness, guiOpacity);
			positionIndicator.DrawIndicator(levelManager.cameraRotY, levelManager.pumaObj, levelManager.fawn.gameObj, levelManager.fawn.type, borderThickness, guiOpacity);
		}
		guiOpacity = prevGuiOpacity;
		

		
		
		prevGuiOpacity = guiOpacity;
		if (guiState == "guiStateEnteringGameplay8" || guiState == "guiStateCaught9")
			guiOpacity = actualGuiOpacity;

		// level and status displays
		//if (guiState != "guiStateGameplay" && (levelManager.IsStalkingState() || guiState == "guiStateLeavingGameplay")) {  // || levelManager.IsCaughtState() || guiState == "guiStateCaught1") {
		if (levelManager.IsStalkingState() || levelManager.IsChasingState() || guiState == "guiStateLeavingGameplay") {
			float levelDisplayX = boxMargin * 0.2f;
			float levelDisplayY = Screen.height - boxMargin * 0.82f - boxMargin * 0.78f;
			float levelDisplayWidth = boxWidth * 1.5f;
			float levelDisplayHeight = boxHeight / 3.03f;
			levelDisplayOpacity = 0.9f;
			CreateLevelDisplay(levelDisplayX, levelDisplayY, levelDisplayWidth, levelDisplayHeight, true);
						
			float statusDisplayX = Screen.width - (boxMargin * 0.2f) - boxWidth * 1.5f;
			float statusDisplayY = Screen.height - boxMargin * 1.06f - boxMargin * 0.78f;
			float statusDisplayWidth = boxWidth * 1.5f;
			float statusDisplayHeight = boxHeight / 2.7f;
			statusDisplayOpacity = 0.9f;
			CreateStatusDisplay(statusDisplayX, statusDisplayY, statusDisplayWidth, statusDisplayHeight, true);
		}
		
		// 'exit' button
		GUI.color = new Color(0f, 0f, 0f, 1f * guiOpacity);
		GUI.Box(new Rect(Screen.width * 0.5f - boxWidth * 0.35f,  Screen.height - (boxMargin * 0.0f) - (boxHeight * 0.15f), boxWidth * 0.7f, boxHeight * 0.15f), "");
		//GUI.color = new Color(0f, 0f, 0f, 0.3f * guiOpacity);
		//GUI.Box(new Rect(Screen.width * 0.5f - boxWidth * 0.35f,  Screen.height - (boxMargin * 0.0f) - (boxHeight * 0.15f), boxWidth * 0.7f, boxHeight * 0.15f), "");
		GUI.color = new Color(1f, 1f, 1f, 0.8f * guiOpacity);
		GUI.skin = customGUISkin;
		customGUISkin.button.fontSize = (int)(boxWidth * 0.0675);
		customGUISkin.button.fontStyle = FontStyle.Bold;	
		float exitButtonX = Screen.width * 0.5f - boxWidth * 0.3f;
		float exitButtonY = Screen.height - (boxMargin * 0.0f + boxHeight * 0.03f) - (boxHeight * 0.096f);
		float exitButtonWidth = boxWidth * 0.6f;
		float exitButtonHeight = boxHeight * 0.105f;
		if (GUI.Button(new Rect(exitButtonX,  exitButtonY, exitButtonWidth, exitButtonHeight), "")) {
			SetGuiState("guiStateLeavingGameplay");
			//SetGuiState("guiStateOverlay");
			levelManager.SetGameState("gameStateLeavingGameplay");
		}
		else if (GUI.Button(new Rect(exitButtonX,  exitButtonY, exitButtonWidth, exitButtonHeight), "")) {
			SetGuiState("guiStateLeavingGameplay");
			//SetGuiState("guiStateOverlay");
			levelManager.SetGameState("gameStateLeavingGameplay");
		}
		else if (GUI.Button(new Rect(exitButtonX,  exitButtonY, exitButtonWidth, exitButtonHeight), "Main Menu")) {
			SetGuiState("guiStateLeavingGameplay");
			//SetGuiState("guiStateOverlay");
			levelManager.SetGameState("gameStateLeavingGameplay");
		}

		guiOpacity = prevGuiOpacity;


		
		// movement controls tray
		
		float trayScaleFactor = (8.5f/7f);

		prevGuiOpacity = guiOpacity;
		if (guiState == "guiStateEnteringGameplay2" || guiState == "guiStateEnteringGameplay3" || guiState == "guiStateCaught6")
			guiOpacity = actualGuiOpacity;
		else if (guiState == "guiStateEnteringGameplay4" || guiState == "guiStateEnteringGameplay5" || guiState == "guiStateEnteringGameplay6" || guiState == "guiStateEnteringGameplay7" || guiState == "guiStateEnteringGameplay8" || guiState == "guiStateCaught7" || guiState == "guiStateCaught8" || guiState == "guiStateCaught9")
			guiOpacity = 1f;
			
		// lower right paw

		float textureWidth = boxWidth * 0.7f * trayScaleFactor;
		float textureX = Screen.width - textureWidth*1.2f - boxMargin;
		float textureHeight = arrowTrayTexture.height * (textureWidth / arrowTrayTopTexture.width);
		float textureY = Screen.height - boxHeight*0.8f - boxMargin;			

		GUI.color = new Color(1f, 1f, 1f, 0.55f * guiOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY + textureHeight * 0.05f, textureWidth, textureHeight * 0.9f), arrowTrayTopTexture);
		GUI.color = new Color(1f, 1f, 1f, 0.7f * guiOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), arrowTrayTexture);
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), arrowTurnRightTexture);
		float rightBoxX = textureX + textureWidth * 0.1f;
		float rightBoxY = textureY + textureHeight * 0.4f;
		float rightBoxWidth = textureWidth * 0.8f;
		float rightBoxHeight = textureHeight * 0.6f;
		
		// upper right paw
	
		textureX = Screen.width - boxWidth * trayScaleFactor - boxMargin;
		textureWidth = boxWidth * trayScaleFactor;
		textureHeight = arrowTrayTexture.height * (textureWidth / arrowTrayTopTexture.width);
		textureY = Screen.height - boxHeight*0.8f - boxMargin;			
		textureY -= textureHeight * 1f;
		
		GUI.color = new Color(1f, 1f, 1f, 0.55f * guiOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY + textureHeight * 0.05f, textureWidth, textureHeight * 0.9f), arrowTrayTopTexture);
		GUI.color = new Color(1f, 1f, 1f, 0.7f * guiOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), arrowTrayTexture);
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), arrowLeftTexture);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), arrowRightTexture);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), arrowUpTexture);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), arrowDownTexture);
		GUI.color = new Color(1f, 1f, 1f, 0f);
		if (GUI.Button(new Rect(textureX + textureWidth * 0.37f, textureY + textureHeight * 0.45f, textureWidth * 0.24f, textureHeight * 0.24f), "")) {
			levelManager.forwardClicked = true;
		}
		if (GUI.Button(new Rect(textureX + textureWidth * 0.37f, textureY + textureHeight * 0.69f, textureWidth * 0.24f, textureHeight * 0.24f), "")) {
			levelManager.backClicked = true;
		}
		//if (GUI.Button(new Rect(textureX + textureWidth * 0.24f, textureY + textureHeight * 0.53f, textureWidth * 0.13f, textureHeight * 0.40f), "")) {
			//levelManager.diagLeftClicked = true;
		//}
		//if (GUI.Button(new Rect(textureX + textureWidth * 0.61f, textureY + textureHeight * 0.53f, textureWidth * 0.13f, textureHeight * 0.40f), "")) {
			//levelManager.diagRightClicked = true;
		//}
		if (GUI.Button(new Rect(textureX + textureWidth * 0.11f, textureY + textureHeight * 0.63f, textureWidth * 0.26f, textureHeight * 0.3f), "")) {
			levelManager.sideLeftClicked = true;
		}
		if (GUI.Button(new Rect(textureX + textureWidth * 0.61f, textureY + textureHeight * 0.63f, textureWidth * 0.26f, textureHeight * 0.3f), "")) {
			levelManager.sideRightClicked = true;
		}
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		
		// upper left paw
	
		textureX = Screen.width - boxWidth - boxMargin;
		textureWidth = boxWidth * trayScaleFactor;
		textureHeight = arrowTrayTexture.height * (textureWidth / arrowTrayTopTexture.width);
		textureY = Screen.height - boxHeight - boxMargin;			
		textureY -= textureHeight * 1f;
		textureX = boxMargin;
	
		//GUI.color = new Color(1f, 1f, 1f, 0.55f * guiOpacity);
		//GUI.DrawTexture(new Rect(textureX, textureY + textureHeight * 0.05f, textureWidth, textureHeight * 0.9f), arrowTrayTopTexture);
		//GUI.color = new Color(1f, 1f, 1f, 0.7f * guiOpacity);
		//GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), arrowTrayTexture);
		//GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		//GUI.color = new Color(1f, 1f, 1f, 0f);
		//GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		
		// lower left paw

		textureWidth = boxWidth * 0.7f * trayScaleFactor;
		textureHeight = arrowTrayTexture.height * (textureWidth / arrowTrayTopTexture.width);
		textureX = boxMargin + textureWidth*0.2f;
		textureY = Screen.height - boxHeight*0.8f - boxMargin;

		GUI.color = new Color(1f, 1f, 1f, 0.55f * guiOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY + textureHeight * 0.05f, textureWidth, textureHeight * 0.9f), arrowTrayTopTexture);
		GUI.color = new Color(1f, 1f, 1f, 0.7f * guiOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), arrowTrayTexture);
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), arrowTurnLeftTexture);
		float leftBoxX = textureX + textureWidth * 0.1f;
		float leftBoxY = textureY + textureHeight * 0.4f;
		float leftBoxWidth = textureWidth * 0.8f;
		float leftBoxHeight = textureHeight * 0.6f;


			
				
		
		
		if (Event.current != null) {
			if (Event.current.type == EventType.mouseDown || Event.current.type == EventType.mouseDrag) {
				float mouseX = Event.current.mousePosition.x;
				float mouseY = Event.current.mousePosition.y;
				if (mouseX >= leftBoxX && mouseX <= leftBoxX+leftBoxWidth && mouseY >= leftBoxY && mouseY <= leftBoxY+leftBoxHeight) {
					levelManager.leftArrowMouseEvent = true;
					levelManager.rightArrowMouseEvent = false;
				}
				else if (mouseX >= rightBoxX && mouseX <= rightBoxX+rightBoxWidth && mouseY >= rightBoxY && mouseY <= rightBoxY+rightBoxHeight) {
					levelManager.leftArrowMouseEvent = false;
					levelManager.rightArrowMouseEvent = true;
				}
				else {
					levelManager.leftArrowMouseEvent = false;
					levelManager.rightArrowMouseEvent = false;	
				}
			}
			else if (Event.current.type == EventType.mouseUp) {
				levelManager.leftArrowMouseEvent = false;
				levelManager.rightArrowMouseEvent = false;	
			}	
		}	
		if (levelManager.leftArrowMouseEvent == true) {
			//GUI.Button(new Rect(leftBoxX, leftBoxY, leftBoxWidth, leftBoxHeight), "");
		}
		if (levelManager.rightArrowMouseEvent == true) {
			//GUI.Button(new Rect(rightBoxX, rightBoxY, rightBoxWidth, rightBoxHeight), "");
		}
		
		
		guiOpacity = prevGuiOpacity;


		float oldBoxHeight = boxHeight;
		float bottomBoxMargin = boxMargin * 1.05f;
		boxHeight = boxHeight * 0.865f;
		//boxMargin = boxMargin * 1.2f;

		if (guiState == "guiStateEnteringGameplay2" || guiState == "guiStateEnteringGameplay3" || guiState == "guiStateCaught6")
			guiOpacity = actualGuiOpacity;
		else if (guiState == "guiStateEnteringGameplay4" || guiState == "guiStateEnteringGameplay5" || guiState == "guiStateEnteringGameplay6" || guiState == "guiStateEnteringGameplay7" || guiState == "guiStateEnteringGameplay8" || guiState == "guiStateCaught7" || guiState == "guiStateCaught8" || guiState == "guiStateCaught9")
			guiOpacity = 1f;



		// puma identity background
		float pumaIdentityY = Screen.height - bottomBoxMargin - boxHeight * 0.58f;
		float pumaIdentityHeight = boxHeight * 0.26f;
		float pumaIdentityWidth = boxWidth * 0.8f;
		GUI.color = new Color(0f, 0f, 0f, 1f * guiOpacity);
		//GUI.Box(new Rect(healthMeterX,  pumaIdentityY, pumaIdentityWidth, pumaIdentityHeight), "");
		GUI.color = new Color(0f, 0f, 0f, 0.3f * guiOpacity);
		//GUI.Box(new Rect(healthMeterX,  pumaIdentityY, pumaIdentityWidth, pumaIdentityHeight), "");
		// puma identity
		Texture2D headshotTexture = headshot1Texture;
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		string pumaName = "no name";
		string pumaVitals = "unspecified";
		switch (selectedPuma) {
		case 0:
			headshotTexture = headshot1Texture;
			pumaName = "Eric";
			pumaVitals = "male, age 2";
			break;
		case 1:
			headshotTexture = headshot2Texture;
			pumaName = "Palo";
			pumaVitals = "female, age 2";
			break;
		case 2:
			headshotTexture = headshot3Texture;
			pumaName = "Mitch";
			pumaVitals = "male, age 5";
			break;
		case 3:
			headshotTexture = headshot4Texture;
			pumaName = "Trish";
			pumaVitals = "female, age 5";
			break;
		case 4:
			headshotTexture = headshot5Texture;
			pumaName = "Liam";
			pumaVitals = "male, age 8";
			break;
		case 5:
			headshotTexture = headshot6Texture;
			pumaName = "Barb";
			pumaVitals = "female, age 8";
			break;
		}
		// puma headshot
		float headshotHeight = pumaIdentityHeight - boxHeight * 0.066f;
		float headshotWidth = headshotTexture.width * (headshotHeight / headshotTexture.height);
		GUI.color = new Color(0f, 0f, 0f, 1f * guiOpacity);
		//GUI.Box(new Rect(healthMeterX + boxWidth * 0.047f, pumaIdentityY + boxHeight * 0.023f, headshotWidth + boxWidth * 0.02f, headshotHeight + boxWidth * 0.015f), "");
		GUI.color = new Color(1f, 1f, 1f, 0.93f * guiOpacity);
		//GUI.DrawTexture(new Rect(healthMeterX + boxWidth * 0.057f, pumaIdentityY + boxHeight * 0.033f, headshotWidth, headshotHeight), headshotTexture);
		// puma name
		style.normal.textColor = new Color(0.99f, 0.62f, 0f, 0.95f);
		style.fontSize = (int)(boxWidth * 0.086);
		//GUI.Button(new Rect(healthMeterX + boxWidth * 0.05f + headshotWidth, pumaIdentityY - pumaIdentityHeight * 0.16f, pumaIdentityWidth - (headshotWidth + boxWidth * 0.075f), pumaIdentityHeight), pumaName, style);
		style.fontSize = (int)(boxWidth * 0.0635);
		style.normal.textColor = new Color(0.93f, 0.57f, 0f, 0.95f);
		//style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		//GUI.Button(new Rect(healthMeterX + boxWidth * 0.05f + headshotWidth, pumaIdentityY + pumaIdentityHeight * 0.16f, pumaIdentityWidth - (headshotWidth + boxWidth * 0.075f), pumaIdentityHeight), pumaVitals, style);

		
		boxHeight = oldBoxHeight;
		guiOpacity = actualGuiOpacity;
	}







	
	void CreateOverlayPanel() 
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

		float levelDisplayX = overlayRect.x + overlayRect.width * 0.04f;
		float levelDisplayY = overlayRect.y + overlayRect.width * 0.016f + upperItemsYShift;
		float levelDisplayWidth = overlayRect.width * 0.30f;
		float levelDisplayHeight = overlayRect.width * 0.076f;
		levelDisplayOpacity = 1f;
		CreateLevelDisplay(levelDisplayX, levelDisplayY, levelDisplayWidth, levelDisplayHeight, false);
					
		float statusDisplayX = overlayRect.x + overlayRect.width * 0.66f;
		float statusDisplayY = overlayRect.y + overlayRect.width * 0.016f + upperItemsYShift;
		float statusDisplayWidth = overlayRect.width * 0.30f;
		float statusDisplayHeight = overlayRect.width * 0.076f;
		statusDisplayOpacity = 1f;
		CreateStatusDisplay(statusDisplayX, statusDisplayY, statusDisplayWidth, statusDisplayHeight, false);

		
		
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
			popupPanelVisible = true;
			//popupPanelPage = 1;
			popupPanelTransStart = Time.time;
		}
		GUI.color = new Color(1f, 1f, 1f, 0.9f * guiOpacity);
		if (GUI.Button(new Rect(helpButtonX, helpButtonY, helpButtonWidth, helpButtonHeight), "Info...")) {
			popupPanelVisible = true;
			//popupPanelPage = 1;
			popupPanelTransStart = Time.time;
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
			CreateSelectScreen();
			break;		
		case 1:
			CreateOptionsScreen();
			break;
		case 2:
			CreateStatsScreen();
			break;
		case 3:
			CreateQuitScreen();
			break;
		}
		
	}


	
	
	bool modifyLayoutFlag = false;

	void CreateSelectScreen() 
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

		CreatePopulationHealthBar(healthBarX + healthBarLabelWidth, healthBarY, healthBarWidth - healthBarLabelWidth * 2f, healthBarHeight, false, false);

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

		
		// used in CreateSelectScreen() and CreateSelectScreenDetails()
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
			if (levelManager.GetPumaHealth(0) <= 0f)
				GUI.color = deadPumaHeadshotColor;
			else if (levelManager.GetPumaHealth(0) >= 1f)
				GUI.color = fullHealthPumaHeadshotColor;
			GUI.DrawTexture(new Rect(headshotX, headshotY, headshotWidth, headshotHeight), headshotTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			// health bar
			if (levelManager.GetPumaHealth(0) > 0f && levelManager.GetPumaHealth(0) < 1f)
				CreateHealthBar(0, textureX, headshotY - headshotHeight * 0.36f + healthDownShift, textureWidth, headshotHeight * 0.26f, true);
			// background panel for puma middle
			headshotY = overlayRect.y + overlayRect.height * 0.47f + yOffsetForAddingPopulationBar + barsDownShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			// display bars in puma middle
			DrawDisplayBars(0, headshotX, headshotY, headshotWidth, headshotHeight);
			// final ending label at very top
			if (levelManager.GetPumaHealth(0) <= 0f) {
				// puma is dead
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				style.normal.textColor = deadPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), "STARVED", style);
			}
			else if (levelManager.GetPumaHealth(0) >= 1f) {
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
			if (levelManager.GetPumaHealth(0) <= 0f)
				GUI.color = deadPumaIconColor;
			else if (levelManager.GetPumaHealth(0) >= 1f)
				GUI.color = fullHealthPumaIconColor;
			else
				GUI.color = new Color(1f, 1f, 1f, 0.7f * guiOpacity);
			GUI.DrawTexture(new Rect(textureX + textureWidth * 0.05f, textureY + textureHeight * 0.12f + iconDownShift, textureWidth * 0.9f, textureHeight * 0.9f), pumaIconTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		}
		if (selectedPuma == 0)
			CreateSelectScreenDetailsPanel(style, textureX, textureWidth, headshot1Texture);
		// text
		if (selectedPuma == 0)
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
		else if (levelManager.GetPumaHealth(0) <= 0f)
			style.normal.textColor = deadPumaTextColor;
		else if (levelManager.GetPumaHealth(0) >= 1f)
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
			if (levelManager.GetPumaHealth(1) <= 0f)
				GUI.color = deadPumaHeadshotColor;
			else if (levelManager.GetPumaHealth(1) >= 1f)
				GUI.color = fullHealthPumaHeadshotColor;
			GUI.DrawTexture(new Rect(headshotX, headshotY, headshotWidth, headshotHeight), headshotTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			// health bar
			if (levelManager.GetPumaHealth(1) > 0f && levelManager.GetPumaHealth(1) < 1f)
				CreateHealthBar(1, textureX, headshotY - headshotHeight * 0.36f + healthDownShift, textureWidth, headshotHeight * 0.26f, true);
			// background panel for puma middle
			headshotY = overlayRect.y + overlayRect.height * 0.47f + yOffsetForAddingPopulationBar + barsDownShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			// display bars in puma middle
			DrawDisplayBars(1, headshotX, headshotY, headshotWidth, headshotHeight);
			// final ending label at very top
			if (levelManager.GetPumaHealth(1) <= 0f) {
				// puma is dead
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				style.normal.textColor = deadPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), "STARVED !", style);
			}
			else if (levelManager.GetPumaHealth(1) >= 1f) {
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
			if (levelManager.GetPumaHealth(1) <= 0f)
				GUI.color = deadPumaIconColor;
			else if (levelManager.GetPumaHealth(1) >= 1f)
				GUI.color = fullHealthPumaIconColor;
			else
				GUI.color = new Color(1f, 1f, 1f, 0.7f * guiOpacity);
			GUI.DrawTexture(new Rect(textureX + textureWidth * 0.05f, textureY + textureHeight * 0.12f + iconDownShift, textureWidth * 0.9f, textureHeight * 0.9f), pumaIconTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		}
		if (selectedPuma == 1)
			CreateSelectScreenDetailsPanel(style, textureX, textureWidth, headshot2Texture);
		// text
		if (selectedPuma == 1)
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
		else if (levelManager.GetPumaHealth(1) <= 0f)
			style.normal.textColor = deadPumaTextColor;
		else if (levelManager.GetPumaHealth(1) >= 1f)
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
			if (levelManager.GetPumaHealth(2) <= 0f)
				GUI.color = deadPumaHeadshotColor;
			else if (levelManager.GetPumaHealth(2) >= 1f)
				GUI.color = fullHealthPumaHeadshotColor;
			GUI.DrawTexture(new Rect(headshotX, headshotY, headshotWidth, headshotHeight), headshotTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			// health bar
			if (levelManager.GetPumaHealth(2) > 0f && levelManager.GetPumaHealth(2) < 1f)
				CreateHealthBar(2, textureX, headshotY - headshotHeight * 0.36f + healthDownShift, textureWidth, headshotHeight * 0.26f, true);
			// background panel for puma middle
			headshotY = overlayRect.y + overlayRect.height * 0.47f + yOffsetForAddingPopulationBar + barsDownShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			// display bars in puma middle
			DrawDisplayBars(2, headshotX, headshotY, headshotWidth, headshotHeight);
			// final ending label at very top
			if (levelManager.GetPumaHealth(2) <= 0f) {
				// puma is dead
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				style.normal.textColor = deadPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), "STARVED !", style);
			}
			else if (levelManager.GetPumaHealth(2) >= 1f) {
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
			if (levelManager.GetPumaHealth(2) <= 0f)
				GUI.color = deadPumaIconColor;
			else if (levelManager.GetPumaHealth(2) >= 1f)
				GUI.color = fullHealthPumaIconColor;
			else
				GUI.color = new Color(1f, 1f, 1f, 0.7f * guiOpacity);
			GUI.DrawTexture(new Rect(textureX + textureWidth * 0.05f, textureY + textureHeight * 0.12f + iconDownShift, textureWidth * 0.9f, textureHeight * 0.9f), pumaIconTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		}
		if (selectedPuma == 2)
			CreateSelectScreenDetailsPanel(style, textureX, textureWidth, headshot3Texture);
		// text
		if (selectedPuma == 2)
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
		else if (levelManager.GetPumaHealth(2) <= 0f)
			style.normal.textColor = deadPumaTextColor;
		else if (levelManager.GetPumaHealth(2) >= 1f)
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
			if (levelManager.GetPumaHealth(3) <= 0f)
				GUI.color = deadPumaHeadshotColor;
			else if (levelManager.GetPumaHealth(3) >= 1f)
				GUI.color = fullHealthPumaHeadshotColor;
			GUI.DrawTexture(new Rect(headshotX, headshotY, headshotWidth, headshotHeight), headshotTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			// health bar
			if (levelManager.GetPumaHealth(3) > 0f && levelManager.GetPumaHealth(3) < 1f)
				CreateHealthBar(3, textureX, headshotY - headshotHeight * 0.36f + healthDownShift, textureWidth, headshotHeight * 0.26f, true);
			// background panel for puma middle
			headshotY = overlayRect.y + overlayRect.height * 0.47f + yOffsetForAddingPopulationBar + barsDownShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			// display bars in puma middle
			DrawDisplayBars(3, headshotX, headshotY, headshotWidth, headshotHeight);
			// final ending label at very top
			if (levelManager.GetPumaHealth(3) <= 0f) {
				// puma is dead
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				style.normal.textColor = deadPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), "STARVED !", style);
			}
			else if (levelManager.GetPumaHealth(3) >= 1f) {
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
			if (levelManager.GetPumaHealth(3) <= 0f)
				GUI.color = deadPumaIconColor;
			else if (levelManager.GetPumaHealth(3) >= 1f)
				GUI.color = fullHealthPumaIconColor;
			else
				GUI.color = new Color(1f, 1f, 1f, 0.7f * guiOpacity);
			GUI.DrawTexture(new Rect(textureX + textureWidth * 0.05f, textureY + textureHeight * 0.12f + iconDownShift, textureWidth * 0.9f, textureHeight * 0.9f), pumaIconTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		}
		if (selectedPuma == 3)
			CreateSelectScreenDetailsPanel(style, textureX, textureWidth, headshot4Texture);
		// text
		if (selectedPuma == 3)
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
		else if (levelManager.GetPumaHealth(3) <= 0f)
			style.normal.textColor = deadPumaTextColor;
		else if (levelManager.GetPumaHealth(3) >= 1f)
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
			if (levelManager.GetPumaHealth(4) <= 0f)
				GUI.color = deadPumaHeadshotColor;
			else if (levelManager.GetPumaHealth(4) >= 1f)
				GUI.color = fullHealthPumaHeadshotColor;
			GUI.DrawTexture(new Rect(headshotX, headshotY, headshotWidth, headshotHeight), headshotTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			// health bar
			if (levelManager.GetPumaHealth(4) > 0f && levelManager.GetPumaHealth(4) < 1f)
				CreateHealthBar(4, textureX, headshotY - headshotHeight * 0.36f + healthDownShift, textureWidth, headshotHeight * 0.26f, true);
			// background panel for puma middle
			headshotY = overlayRect.y + overlayRect.height * 0.47f + yOffsetForAddingPopulationBar + barsDownShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			// display bars in puma middle
			DrawDisplayBars(4, headshotX, headshotY, headshotWidth, headshotHeight);
			// final ending label at very top
			if (levelManager.GetPumaHealth(4) <= 0f) {
				// puma is dead
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				style.normal.textColor = deadPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), "STARVED !", style);
			}
			else if (levelManager.GetPumaHealth(4) >= 1f) {
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
			if (levelManager.GetPumaHealth(4) <= 0f)
				GUI.color = deadPumaIconColor;
			else if (levelManager.GetPumaHealth(4) >= 1f)
				GUI.color = fullHealthPumaIconColor;
			else
				GUI.color = new Color(1f, 1f, 1f, 0.7f * guiOpacity);
			GUI.DrawTexture(new Rect(textureX + textureWidth * 0.05f, textureY + textureHeight * 0.12f + iconDownShift, textureWidth * 0.9f, textureHeight * 0.9f), pumaIconTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		}
		if (selectedPuma == 4)
			CreateSelectScreenDetailsPanel(style, textureX, textureWidth, headshot5Texture);
		// text
		if (selectedPuma == 4)
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
		else if (levelManager.GetPumaHealth(4) <= 0f)
			style.normal.textColor = deadPumaTextColor;
		else if (levelManager.GetPumaHealth(4) >= 1f)
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
			if (levelManager.GetPumaHealth(5) <= 0f)
				GUI.color = deadPumaHeadshotColor;
			else if (levelManager.GetPumaHealth(5) >= 1f)
				GUI.color = fullHealthPumaHeadshotColor;
			GUI.DrawTexture(new Rect(headshotX, headshotY, headshotWidth, headshotHeight), headshotTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			// health bar
			if (levelManager.GetPumaHealth(5) > 0f && levelManager.GetPumaHealth(5) < 1f)
				CreateHealthBar(5, textureX, headshotY - headshotHeight * 0.36f + healthDownShift, textureWidth, headshotHeight * 0.26f, true);
			// background panel for puma middle
			headshotY = overlayRect.y + overlayRect.height * 0.47f + yOffsetForAddingPopulationBar + barsDownShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			// display bars in puma middle
			DrawDisplayBars(5, headshotX, headshotY, headshotWidth, headshotHeight);
			// final ending label at very top
			if (levelManager.GetPumaHealth(5) <= 0f) {
				// puma is dead
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * guiOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				style.normal.textColor = deadPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), "STARVED !", style);
			}
			else if (levelManager.GetPumaHealth(5) >= 1f) {
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
			if (levelManager.GetPumaHealth(5) <= 0f)
				GUI.color = deadPumaIconColor;
			else if (levelManager.GetPumaHealth(5) >= 1f)
				GUI.color = fullHealthPumaIconColor;
			else
				GUI.color = new Color(1f, 1f, 1f, 0.7f * guiOpacity);
			GUI.DrawTexture(new Rect(textureX + textureWidth * 0.05f, textureY + textureHeight * 0.12f + iconDownShift, textureWidth * 0.9f, textureHeight * 0.9f), pumaIconTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		}
		if (selectedPuma == 5)
			CreateSelectScreenDetailsPanel(style, textureX, textureWidth, headshot6Texture);
		// text
		if (selectedPuma == 5)
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
		else if (levelManager.GetPumaHealth(5) <= 0f)
			style.normal.textColor = deadPumaTextColor;
		else if (levelManager.GetPumaHealth(5) >= 1f)
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
	

	void CreateSelectScreenDetailsPanel(GUIStyle style, float textureX, float textureWidth, Texture2D headshotTexture) 
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
		

		// used in CreateSelectScreen() and CreateSelectScreenDetailsPanel()
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
		CreateHealthBar(selectedPuma, detailsPanelX + detailsPanelWidth * 0.03f, origHeadshotYPos - headshotHeight * 0.091f + healthDownShift, detailsPanelWidth - detailsPanelWidth * 0.06f, headshotHeight * 0.17f);

		
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
			popupPanelVisible = true;
			popupPanelPage = 1;
			popupPanelTransStart = Time.time;
		}
		if (GUI.Button(new Rect(headshotX + overlayRect.width * 0.015f - detailsPanelWidth * 0.033f, yOffset + headshotY + headshotHeight + overlayRect.height * 0.008f, detailsPanelWidth * 0.15f, headshotHeight * 0.2f), "")) {
			popupPanelVisible = true;
			popupPanelPage = 1;
			popupPanelTransStart = Time.time;
		}
		GUI.color = new Color(1f, 1f, 1f, 0.8f * guiOpacity);
		if (GUI.Button(new Rect(headshotX + overlayRect.width * 0.015f - detailsPanelWidth * 0.033f, yOffset + headshotY + headshotHeight + overlayRect.height * 0.008f, detailsPanelWidth * 0.15f, headshotHeight * 0.2f), "?")) {
			popupPanelVisible = true;
			popupPanelPage = 1;
			popupPanelTransStart = Time.time;
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
	
	void CreateOptionsScreen() 
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

		CreatePopulationHealthBar(healthBarX + healthBarLabelWidth, healthBarY, healthBarWidth - healthBarLabelWidth * 2f, healthBarHeight, false, false);

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
			popupPanelVisible = true;
			popupPanelPage = 0;
			popupPanelTransStart = Time.time;
		}
		GUI.backgroundColor = new Color(0.5f, 0.5f, 0.9f, 1f);
		if (GUI.Button(new Rect(optionsScreenX + overlayRect.width * 0.025f + overlayRect.width * 0.292f, titleY + overlayRect.height * 0.113f * 3f - overlayRect.height * 0.006f, overlayRect.width * 0.25f, overlayRect.height * 0.0585f), "")) {
			popupPanelVisible = true;
			popupPanelPage = 0;
			popupPanelTransStart = Time.time;
		}
		if (GUI.Button(new Rect(optionsScreenX + overlayRect.width * 0.025f + overlayRect.width * 0.292f, titleY + overlayRect.height * 0.113f * 3f - overlayRect.height * 0.006f, overlayRect.width * 0.25f, overlayRect.height * 0.0585f), "Play Introduction....")) {
			popupPanelVisible = true;
			popupPanelPage = 0;
			popupPanelTransStart = Time.time;
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
			popupPanelVisible = true;
			popupPanelPage = 5;
			popupPanelTransStart = Time.time;
		}
		GUI.backgroundColor = new Color(0.5f, 0.5f, 0.9f, 1f);
		if (GUI.Button(new Rect(optionsScreenX + overlayRect.width * 0.025f + overlayRect.width * 0.292f, titleY + overlayRect.height * 0.113f * 4f - overlayRect.height * 0.006f, overlayRect.width * 0.25f, overlayRect.height * 0.0585f), "")) {
			popupPanelVisible = true;
			popupPanelPage = 5;
			popupPanelTransStart = Time.time;
		}
		if (GUI.Button(new Rect(optionsScreenX + overlayRect.width * 0.025f + overlayRect.width * 0.292f, titleY + overlayRect.height * 0.113f * 4f - overlayRect.height * 0.006f, overlayRect.width * 0.25f, overlayRect.height * 0.0585f), "Help Protect Pumas....")) {
			popupPanelVisible = true;
			popupPanelPage = 5;
			popupPanelTransStart = Time.time;
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
	
	void CreateStatsScreen() 
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

		CreatePopulationHealthBar(healthBarX + healthBarLabelWidth, healthBarY, healthBarWidth - healthBarLabelWidth * 2f, healthBarHeight, false, true);

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
		if (levelManager.GetPumaHealth(0) <= 0f)
			GUI.color = deadPumaHeadshotColor;
		else if (levelManager.GetPumaHealth(0) >= 1f)
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
		if (levelManager.GetPumaHealth(1) <= 0f)
			GUI.color = deadPumaHeadshotColor;
		else if (levelManager.GetPumaHealth(1) >= 1f)
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
		if (levelManager.GetPumaHealth(2) <= 0f)
			GUI.color = deadPumaHeadshotColor;
		else if (levelManager.GetPumaHealth(2) >= 1f)
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
		if (levelManager.GetPumaHealth(3) <= 0f)
			GUI.color = deadPumaHeadshotColor;
		else if (levelManager.GetPumaHealth(3) >= 1f)
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
		if (levelManager.GetPumaHealth(4) <= 0f)
			GUI.color = deadPumaHeadshotColor;
		else if (levelManager.GetPumaHealth(4) >= 1f)
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
		if (levelManager.GetPumaHealth(5) <= 0f)
			GUI.color = deadPumaHeadshotColor;
		else if (levelManager.GetPumaHealth(5) >= 1f)
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
		if (levelManager.GetPumaHealth(0) <= 0f)
			GUI.color = new Color(0.45f, 0f, 0f, 0.8f * guiOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);

		headshotTexture = closeup4Texture;
		textureY = smallHeadLowerY;
		textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
		if (levelManager.GetPumaHealth(3) <= 0f)
			GUI.color = new Color(0.45f, 0f, 0f, 0.8f * guiOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);

		textureX = statsX + columnWidth*3 + columnGap*3 + columnWidth * 0.4f + smallHeadRightShift;
			
		headshotTexture = closeup2Texture;
		textureY = smallHeadUpperY;
		textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
		if (levelManager.GetPumaHealth(1) <= 0f)
			GUI.color = new Color(0.45f, 0f, 0f, 0.8f * guiOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);

		headshotTexture = closeup5Texture;
		textureY = smallHeadLowerY;
		textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
		if (levelManager.GetPumaHealth(4) <= 0f)
			GUI.color = new Color(0.45f, 0f, 0f, 0.8f * guiOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);

		textureX = statsX + columnWidth*3 + columnGap*3 + columnWidth * 0.8f + smallHeadRightShift;
			
		headshotTexture = closeup3Texture;
		textureY = smallHeadUpperY;
		textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
		if (levelManager.GetPumaHealth(2) <= 0f)
			GUI.color = new Color(0.45f, 0f, 0f, 0.8f * guiOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);

		headshotTexture = closeup6Texture;
		textureY = smallHeadLowerY;
		textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
		if (levelManager.GetPumaHealth(5) <= 0f)
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
		
		style.normal.textColor = (selectedPuma == 0) ? selectedTextColor : ((levelManager.GetPumaHealth(0) <= 0f) ? deadPumaTextColor : ((levelManager.GetPumaHealth(0) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * bigTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*0 + columnGap*0;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "Eric", style);
		style.alignment = TextAnchor.MiddleCenter;
		
		style.normal.textColor = (selectedPuma == 1) ? selectedTextColor : ((levelManager.GetPumaHealth(1) <= 0f) ? deadPumaTextColor : ((levelManager.GetPumaHealth(1) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * bigTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*1 + columnGap*1;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "Palo", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (selectedPuma == 2) ? selectedTextColor : ((levelManager.GetPumaHealth(2) <= 0f) ? deadPumaTextColor : ((levelManager.GetPumaHealth(2) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * bigTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*2 + columnGap*2;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "Mitch", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (selectedPuma == 3) ? selectedTextColor : ((levelManager.GetPumaHealth(3) <= 0f) ? deadPumaTextColor : ((levelManager.GetPumaHealth(3) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * bigTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "Trish", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (selectedPuma == 4) ? selectedTextColor : ((levelManager.GetPumaHealth(4) <= 0f) ? deadPumaTextColor : ((levelManager.GetPumaHealth(4) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * bigTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "Liam", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (selectedPuma == 5) ? selectedTextColor : ((levelManager.GetPumaHealth(5) <= 0f) ? deadPumaTextColor : ((levelManager.GetPumaHealth(5) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * bigTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "Barb", style);
		style.alignment = TextAnchor.MiddleCenter;

		
		
		// puma labels
		
		float smallTextFont = 0.013f;
		textY = statsY + statsHeight * 0.212f;	

		style.normal.textColor = (selectedPuma == 0) ? selectedTextColor : ((levelManager.GetPumaHealth(0) <= 0f) ? deadPumaTextColor : ((levelManager.GetPumaHealth(0) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*0 + columnGap*0;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "2 years - male", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (selectedPuma == 1) ? selectedTextColor : ((levelManager.GetPumaHealth(1) <= 0f) ? deadPumaTextColor : ((levelManager.GetPumaHealth(1) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*1 + columnGap*1;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "2 years - female", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (selectedPuma == 2) ? selectedTextColor : ((levelManager.GetPumaHealth(2) <= 0f) ? deadPumaTextColor : ((levelManager.GetPumaHealth(2) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*2 + columnGap*2;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "5 years - male", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (selectedPuma == 3) ? selectedTextColor : ((levelManager.GetPumaHealth(3) <= 0f) ? deadPumaTextColor : ((levelManager.GetPumaHealth(3) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "5 years - female", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (selectedPuma == 4) ? selectedTextColor : ((levelManager.GetPumaHealth(4) <= 0f) ? deadPumaTextColor : ((levelManager.GetPumaHealth(4) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "8 years - male", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (selectedPuma == 5) ? selectedTextColor : ((levelManager.GetPumaHealth(5) <= 0f) ? deadPumaTextColor : ((levelManager.GetPumaHealth(5) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
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
		GUI.Button(new Rect(textureX, statsY + statsHeight * 0.065f, columnWidth + midColumnSizeIncrease, textureHeight), "local results", style);
		style.alignment = TextAnchor.MiddleCenter;

		
		
		
		
		

		// deer heads

		float headstackBaseY = statsY + statsHeight * 0.291f;
		Texture2D displayHeadTexture;
		int columnNum;
		float columnShift;
		float incrementHeight = 0f;

		for (int j = 0; j < 6; j++) {
		
			if (levelManager.GetPumaHealth(j) <= 0f)
				GUI.color = deadPumaDeerIconColor;
			else if (levelManager.GetPumaHealth(j) >= 1f)
				GUI.color = fullHealthPumaDeerIconColor;

			columnNum = (j < 3) ? j : j+1;
			columnShift = (j < 3) ? 0 : midColumnSizeIncrease;
		
			displayHeadTexture = buckHeadTexture;
			textureX = statsX + columnWidth*columnNum + columnGap*columnNum + columnWidth*0.03f + columnShift;
			textureWidth = columnWidth * 0.24f;
			textureHeight = displayHeadTexture.height * (textureWidth / displayHeadTexture.width);
			incrementHeight = textureHeight * 1.1f;
			textureY = headstackBaseY - textureHeight * 0.0f;
			int kills = levelManager.GetBucksKilled(j);
			for (int i = 0; i < kills; i++) {
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), displayHeadTexture);
				textureY += incrementHeight;
			}

			displayHeadTexture = doeHeadTexture;
			textureX = statsX + columnWidth*columnNum + columnGap*columnNum + columnWidth*0.38f + columnShift;
			textureWidth = columnWidth * 0.26f;
			textureHeight = displayHeadTexture.height * (textureWidth / displayHeadTexture.width);
			textureY = headstackBaseY - textureHeight * 0.08f;
			kills = levelManager.GetDoesKilled(j);
			for (int i = 0; i < kills; i++) {
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), displayHeadTexture);
				textureY += incrementHeight;
			}
			
			displayHeadTexture = fawnHeadTexture;
			textureX = statsX + columnWidth*columnNum + columnGap*columnNum + columnWidth*0.72f + columnShift;
			textureY = headstackBaseY - textureHeight * 0.08f;
			textureWidth = columnWidth * 0.27f;
			textureHeight = displayHeadTexture.height * (textureWidth / displayHeadTexture.width);
			kills = levelManager.GetFawnsKilled(j);
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
			bucksKilled += levelManager.GetBucksKilled(i);
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
			doesKilled += levelManager.GetDoesKilled(i);
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
			fawnsKilled += levelManager.GetFawnsKilled(i);
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

		CreateHealthBar(0, statsX + columnWidth*0 + columnGap*0, statsY + statsHeight * healthBarYFactor, columnWidth, statsHeight * 0.04f, true, true);
		CreateHealthBar(1, statsX + columnWidth*1 + columnGap*1, statsY + statsHeight * healthBarYFactor, columnWidth, statsHeight * 0.04f, true, true);
		CreateHealthBar(2, statsX + columnWidth*2 + columnGap*2, statsY + statsHeight * healthBarYFactor, columnWidth, statsHeight * 0.04f, true, true);
		CreateHealthBar(3, statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease, statsY + statsHeight * healthBarYFactor, columnWidth, statsHeight * 0.04f, true, true);
		CreateHealthBar(4, statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease, statsY + statsHeight * healthBarYFactor, columnWidth, statsHeight * 0.04f, true, true);
		CreateHealthBar(5, statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease, statsY + statsHeight * healthBarYFactor, columnWidth, statsHeight * 0.04f, true, true);


		
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
			popupPanelVisible = true;
			popupPanelPage = 2;
			popupPanelTransStart = Time.time;
		}
		GUI.backgroundColor = new Color(0.5f, 0.5f, 0.9f, 1f);
		if (GUI.Button(new Rect(statsX + statsWidth * 0.24f, statsY + statsHeight * 0.56f, statsWidth * 0.3f, statsHeight * 0.098f), "")) {
			popupPanelVisible = true;
			popupPanelPage = 2;
			popupPanelTransStart = Time.time;
		}
		if (GUI.Button(new Rect(statsX + statsWidth * 0.24f, statsY + statsHeight * 0.56f, statsWidth * 0.3f, statsHeight * 0.098f), "How Pumas Hunt....")) {
			popupPanelVisible = true;
			popupPanelPage = 2;
			popupPanelTransStart = Time.time;
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
			popupPanelVisible = true;
			popupPanelPage = 4;
			popupPanelTransStart = Time.time;
		}
		GUI.backgroundColor = new Color(0.5f, 0.5f, 0.9f, 1f);
		if (GUI.Button(new Rect(statsX + statsWidth * 0.24f, statsY + statsHeight * 0.56f, statsWidth * 0.3f, statsHeight * 0.098f), "")) {
			popupPanelVisible = true;
			popupPanelPage = 4;
			popupPanelTransStart = Time.time;
		}
		if (GUI.Button(new Rect(statsX + statsWidth * 0.24f, statsY + statsHeight * 0.56f, statsWidth * 0.3f, statsHeight * 0.098f), "Threats to Pumas....")) {
			popupPanelVisible = true;
			popupPanelPage = 4;
			popupPanelTransStart = Time.time;
		}
		GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
		GUI.color = new Color(1f, 1f, 1f, 1f);

		//buttonStyle.fontSize = buttonDownStyle.fontSize = (int)(overlayRect.width * 0.0165);
		//GUI.color = new Color(1f, 1f, 1f, 0.89f);
		//if (GUI.Button(new Rect(statsX + statsWidth * 0.24f, statsY + statsHeight * 0.545f, statsWidth * 0.52f, statsHeight * 0.11f), "THE THREATS REAL PUMAS FACE...", buttonStyle))
			//currentScreen = currentScreen;
		//GUI.color = new Color(1f, 1f, 1f, 1f);

	}


	void CreateQuitScreen() 
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

	
	void CreateLevelDisplay(float levelDisplayX, float levelDisplayY, float levelDisplayWidth, float levelDisplayHeight, bool bareBonesFlag = false) 
	{ 
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		
		float oldGuiOpacity = guiOpacity;
		guiOpacity = guiOpacity * levelDisplayOpacity;
		
		if (bareBonesFlag == false) {
			GUI.color = new Color(1f, 1f, 1f, 0.8f * guiOpacity);
			GUI.Box(new Rect(levelDisplayX, levelDisplayY, levelDisplayWidth, levelDisplayHeight), "");
			GUI.color = new Color(1f, 1f, 1f, 0.2f * guiOpacity);
			GUI.Box(new Rect(levelDisplayX, levelDisplayY, levelDisplayWidth, levelDisplayHeight), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			//DrawRect(new Rect(levelDisplayX + levelDisplayWidth * 0.33f, levelDisplayY, levelDisplayWidth * 0.005f, levelDisplayHeight), new Color(0f, 0f, 0f, 0.2f));	
			//DrawRect(new Rect(levelDisplayX + levelDisplayWidth * 0.335f, levelDisplayY, levelDisplayWidth * 0.005f, levelDisplayHeight), new Color(1f, 1f, 1f, 0.2f));	
		}
		
		float fontRef = levelDisplayWidth * 1000f / 320f;
		string levelLabel = "unknown";
		float textureX;
		float textureY;
		float textureWidth;
		float textureHeight;

		// level icon

		if (bareBonesFlag == false) {
		
			if (currentLevel == 0) {
				textureX = levelDisplayX + levelDisplayWidth * 0.74f;
				textureY = levelDisplayY + levelDisplayHeight * 0.05f;
				textureWidth = levelDisplayWidth * 0.20f;
				textureHeight = buckHeadTexture.height * (textureWidth / buckHeadTexture.width);
				GUI.color = new Color(1f, 1f, 1f, 0.9f * guiOpacity);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), buckHeadTexture);
				levelLabel = "Natural Wilderness";
			}

			if (currentLevel == 1) {
				textureX = levelDisplayX + levelDisplayWidth * 0.80f;
				textureY = levelDisplayY + levelDisplayHeight * 0.12f;
				textureWidth = levelDisplayWidth * 0.15f;
				textureHeight = hunterTexture.height * (textureWidth / hunterTexture.width);
				//GUI.color = new Color(1f, 1f, 1f, (currentLevel > 0) ? 0.97f : 0.4f);
				GUI.color = new Color(1f, 1f, 1f, (currentLevel > 0) ? (0.94f  * guiOpacity) : (0f * guiOpacity));
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), hunterTexture);
				levelLabel = "2: Human Presence";
			}

			if (currentLevel == 2) {
				textureX = levelDisplayX + levelDisplayWidth * 0.75f;
				textureY = levelDisplayY + levelDisplayHeight * 0.26f;
				textureWidth = levelDisplayWidth * 0.21f;
				textureHeight = vehicleTexture.height * (textureWidth / vehicleTexture.width);
				//GUI.color = new Color(1f, 1f, 1f, (currentLevel > 1) ? 0.93f : 0.36f);
				GUI.color = new Color(1f, 1f, 1f, (currentLevel > 1) ? (0.93f  * guiOpacity) : (0f * guiOpacity));
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), vehicleTexture);
				levelLabel = "3: Human Expansion";
			}
		}
			
		// level label
		
		//style.fontSize = (int)(fontRef * 0.020f);
		//style.normal.textColor = new Color(0f, 0.25f, 0f, 1f);
		//style.normal.textColor = new Color(0.99f, 0.63f, 0f, 0.95f);
		//style.normal.textColor = new Color(0.99f, 0.7f, 0f, 0.95f);
		//style.fontStyle = FontStyle.Bold;
		//style.fontStyle = FontStyle.BoldAndItalic;
		//GUI.Button(new Rect(levelDisplayX - levelDisplayWidth * 0.053f, levelDisplayY + levelDisplayHeight * 0.29f, levelDisplayWidth * 0.3f, levelDisplayHeight * 0.03f), (currentLevel == 0) ? " " : ((currentLevel == 1) ? " " : " "), style);
		if (bareBonesFlag == false) {
			style.fontSize = (int)(fontRef * 0.0185f);
			style.normal.textColor = new Color(0.99f, 0.7f, 0f, 0.92f);
			style.normal.textColor = new Color(0.99f, 0.62f, 0f, 0.95f);
			style.normal.textColor = new Color(0.99f, 0.66f, 0f, 0.935f);
			style.fontStyle = FontStyle.BoldAndItalic;
			style.alignment = TextAnchor.MiddleCenter;
			GUI.Button(new Rect(levelDisplayX + levelDisplayWidth * 0.07f, levelDisplayY + levelDisplayHeight * 0.26f, levelDisplayWidth * 0.64f, levelDisplayHeight * 0.03f), levelLabel, style);
			style.alignment = TextAnchor.MiddleCenter;
		}

		// feeding bar
		guiOpacity = (oldGuiOpacity + guiOpacity) / 2;		
		if (bareBonesFlag == true) {
			GUI.color = new Color(1f, 1f, 1f, 0.5f * guiOpacity);
			GUI.Box(new Rect(levelDisplayX + levelDisplayWidth * 0.07f, levelDisplayY + levelDisplayHeight * 0.525f, levelDisplayWidth * 0.64f, levelDisplayHeight * 0.35f), "");
		}
		CreateFeedingBar(levelDisplayX + levelDisplayWidth * 0.07f, levelDisplayY + levelDisplayHeight * 0.525f, levelDisplayWidth * 0.64f, levelDisplayHeight * 0.35f);
		guiOpacity = oldGuiOpacity;


		// invisible button
		if (GUI.Button(new Rect(levelDisplayX + levelDisplayWidth * 0.7f, levelDisplayY, levelDisplayWidth * 0.3f, levelDisplayHeight), "", style)) {
			currentLevel += 1;
			if (currentLevel > 2)
				currentLevel = 0;
		}
		//GUI.Box(new Rect(levelDisplayX + levelDisplayWidth * 0.7f, levelDisplayY, levelDisplayWidth * 0.3f, levelDisplayHeight), "");



	}
	
	
	void CreateStatusDisplay(float statusDisplayX, float statusDisplayY, float statusDisplayWidth, float statusDisplayHeight, bool bareBonesFlag = false) 
	{ 
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		
		float oldGuiOpacity = guiOpacity;
		guiOpacity = guiOpacity * statusDisplayOpacity;

		if (bareBonesFlag == false) {
			GUI.color = new Color(1f, 1f, 1f, 0.8f * guiOpacity);
			GUI.Box(new Rect(statusDisplayX, statusDisplayY, statusDisplayWidth, statusDisplayHeight), "");
			GUI.color = new Color(1f, 1f, 1f, 0.4f * guiOpacity);
			GUI.Box(new Rect(statusDisplayX, statusDisplayY, statusDisplayWidth, statusDisplayHeight), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			//DrawRect(new Rect(statusDisplayX + statusDisplayWidth * 0.33f, statusDisplayY, statusDisplayWidth * 0.005f, statusDisplayHeight), new Color(0f, 0f, 0f, 0.2f));	
			//DrawRect(new Rect(statusDisplayX + statusDisplayWidth * 0.335f, statusDisplayY, statusDisplayWidth * 0.005f, statusDisplayHeight), new Color(1f, 1f, 1f, 0.2f));	
			//DrawRect(new Rect(statusDisplayX + statusDisplayWidth * 0f, statusDisplayY + statusDisplayHeight * 0.50f, statusDisplayWidth * 1f, statusDisplayWidth * 0.005f), new Color(0f, 0f, 0f, 0.3f));	
			//DrawRect(new Rect(statusDisplayX + statusDisplayWidth * 0f, statusDisplayY + statusDisplayHeight * 0.50f + statusDisplayWidth * 0.005f, statusDisplayWidth * 1f, statusDisplayWidth * 0.005f), new Color(1f, 1f, 1f, 0.2f));	
		}
		
		float fontRef = statusDisplayWidth * 1000f / 320f;
				
		style.fontSize = (int)(fontRef * 0.020f);
		style.normal.textColor = new Color(0f, 0.68f, 0f, 1f);
		style.normal.textColor =  (pumasAlive >= 5) ? new Color(0f, 0.66f, 0f, 1f) : ((pumasAlive >= 3) ? new Color(0.83f, 0.78f, 0f, 1f) : new Color(0.8f, 0f, 0f, 1f));
		//style.normal.textColor = new Color(0.99f, 0.66f, 0f, 1f);
		style.fontStyle = FontStyle.Bold;
		//GUI.Button(new Rect(statusDisplayX + statusDisplayWidth * 0.018f, statusDisplayY + statusDisplayHeight * 0.19f, statusDisplayWidth * 0.3f, statusDisplayHeight * 0.03f), (pumasAlive >= 5) ? "STATUS" : ((pumasAlive >= 3) ? "CAUTION" : "WARNING"), style);
		style.fontSize = (int)(fontRef * 0.015f);
		style.normal.textColor =   (pumasAlive >= 5) ? new Color(0f, 0.70f, 0f, 1f) : ((pumasAlive >= 3) ? new Color(0.85f, 0.80f, 0f, 1f) : new Color(0.82f, 0f, 0f, 1f));
		style.fontStyle = FontStyle.BoldAndItalic;
		//GUI.Button(new Rect(statusDisplayX + statusDisplayWidth * 0.018f, statusDisplayY + statusDisplayHeight * 0.12f, statusDisplayWidth * 0.3f, statusDisplayHeight * 0.03f), (pumasAlive >= 5) ? "Healthy" : ((pumasAlive >= 3) ? "Dwindling" : "Threat of"), style);
		//GUI.Button(new Rect(statusDisplayX + statusDisplayWidth * 0.018f, statusDisplayY + statusDisplayHeight * 0.34f, statusDisplayWidth * 0.3f, statusDisplayHeight * 0.03f), (pumasAlive >= 5) ? "Population" : ((pumasAlive >= 3) ? "Population" : "Extinction"), style);

		float textureX;
		float textureY;
		float textureHeight;
		float textureWidth;

		if (selectedPuma != -1) {

			// puma identity
			Texture2D headshotTexture = closeup1Texture;
			string pumaName = "no name";
			switch (selectedPuma) {
			case 0:
				headshotTexture = closeup1Texture;
				pumaName = "Eric";
				break;
			case 1:
				headshotTexture = closeup2Texture;
				pumaName = "Palo";
				break;
			case 2:
				headshotTexture = closeup3Texture;
				pumaName = "Mitch";
				break;
			case 3:
				headshotTexture = closeup4Texture;
				pumaName = "Trish";
				break;
			case 4:
				headshotTexture = closeup5Texture;
				pumaName = "Liam";
				break;
			case 5:
				headshotTexture = closeup6Texture;
				pumaName = "Barb";
				break;
			}


			float statusDisplayOpacityDrop = 1f - statusDisplayOpacity;

			// puma head
			if (bareBonesFlag == false) {
				guiOpacity = oldGuiOpacity * (1f - (statusDisplayOpacityDrop * 0.25f));
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				textureX = statusDisplayX + statusDisplayWidth * 0.05f;
				textureY = statusDisplayY + statusDisplayHeight * 0.20f;
				textureHeight = statusDisplayHeight * 0.62f;
				textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
				guiOpacity = oldGuiOpacity * statusDisplayOpacity;
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			}

			// puma name
			guiOpacity = oldGuiOpacity * (1f - (statusDisplayOpacityDrop * 0.75f));
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
			//style.normal.textColor = new Color(0.99f, 0.62f, 0f, 0.95f);
			style.normal.textColor = new Color(0.99f, 0.66f, 0f, 0.935f);
			style.fontSize = (int)(fontRef * 0.016f);
			style.alignment = TextAnchor.UpperCenter;
			//GUI.Button(new Rect(textureX, textureY + textureHeight + textureHeight * 0.035f, textureWidth, textureHeight), pumaName, style);
			style.alignment = TextAnchor.MiddleCenter;
			//style.fontSize = (int)(boxWidth * 0.0635);
			//style.normal.textColor = new Color(0.93f, 0.57f, 0f, 0.95f);
			//style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
			//GUI.Button(new Rect(textureX, textureY + textureHeight + textureHeight * 0.1f, textureWidth, textureHeight), pumaVitals, style);
			guiOpacity = oldGuiOpacity * statusDisplayOpacity;
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);


		}
		else {
			// no puma selected
			style.normal.textColor = new Color(0.99f, 0.7f, 0f, 0.92f);
			style.normal.textColor = new Color(0.99f, 0.62f, 0f, 0.95f);
			style.normal.textColor = new Color(0.99f, 0.66f, 0f, 0.935f);
			style.fontSize = (int)(fontRef * 0.016f);
			style.alignment = TextAnchor.UpperCenter;
			GUI.Button(new Rect(statusDisplayX + statusDisplayWidth * 0.045f, statusDisplayY + statusDisplayHeight * 0.2f, statusDisplayWidth * 0.2f, statusDisplayWidth * 0.3f), "No", style);
			GUI.Button(new Rect(statusDisplayX + statusDisplayWidth * 0.045f, statusDisplayY + statusDisplayHeight * 0.4f, statusDisplayWidth * 0.2f, statusDisplayWidth * 0.3f), "Puma", style);
			GUI.Button(new Rect(statusDisplayX + statusDisplayWidth * 0.045f, statusDisplayY + statusDisplayHeight * 0.6f, statusDisplayWidth * 0.2f, statusDisplayWidth * 0.3f), "Selected", style);
			style.alignment = TextAnchor.MiddleCenter;
		
		}

		// six puma icons
		
		if (bareBonesFlag == false) {
		
			Color pumaAliveColor = new Color(1f, 1f, 1f, 0.9f * guiOpacity);
			//Color pumaFullHealthColor = new Color(0.32f, 0.99f, 0f, 0.99f * guiOpacity);
			Color pumaFullHealthColor = new Color(0.84f, 0.99f, 0.0f, 0.72f * guiOpacity);
			//Color pumaDeadColor = new Color(0.76f, 0.0f, 0f, 0.47f * guiOpacity);
			Color pumaDeadColor = new Color(0.1f, 0.1f, 0.1f, 0.6f * guiOpacity);


			textureX = statusDisplayX + statusDisplayWidth * 0.280f;
			textureY = statusDisplayY + statusDisplayHeight * 0.1f;
			textureWidth = statusDisplayWidth * 0.12f;
			textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
			if (levelManager.GetPumaHealth(0) >= 1f) {
				GUI.color = new Color(1f, 1f, 1f, 0.7f * guiOpacity);
				GUI.DrawTexture(new Rect(textureX - (textureWidth * 0.1f), textureY - (textureHeight * 0.11f), textureWidth * 1.2f, textureHeight * 1.2f), pumaIconShadowYellowTexture);
				//GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				//GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}
			//else {
				GUI.color = (levelManager.GetPumaHealth(0) <= 0f) ? pumaDeadColor : ((levelManager.GetPumaHealth(0) >= 1f) ? pumaFullHealthColor : pumaAliveColor);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			//}
			
			textureX = statusDisplayX + statusDisplayWidth * 0.388f;
			textureY = statusDisplayY + statusDisplayHeight * 0.1f;
			textureWidth = statusDisplayWidth * 0.12f;
			textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
			if (levelManager.GetPumaHealth(1) >= 1f) {
				GUI.color = new Color(1f, 1f, 1f, 0.7f * guiOpacity);
				GUI.DrawTexture(new Rect(textureX - (textureWidth * 0.1f), textureY - (textureHeight * 0.11f), textureWidth * 1.2f, textureHeight * 1.2f), pumaIconShadowYellowTexture);
				//GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				//GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}
			//else {
				GUI.color = (levelManager.GetPumaHealth(1) <= 0f) ? pumaDeadColor : ((levelManager.GetPumaHealth(1) >= 1f) ? pumaFullHealthColor : pumaAliveColor);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			//}

			textureX = statusDisplayX + statusDisplayWidth * 0.496f;
			textureY = statusDisplayY + statusDisplayHeight * 0.1f;
			textureWidth = statusDisplayWidth * 0.12f;
			textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
			if (levelManager.GetPumaHealth(2) >= 1f) {
				GUI.color = new Color(1f, 1f, 1f, 0.7f * guiOpacity);
				GUI.DrawTexture(new Rect(textureX - (textureWidth * 0.1f), textureY - (textureHeight * 0.11f), textureWidth * 1.2f, textureHeight * 1.2f), pumaIconShadowYellowTexture);
				//GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				//GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}
			//else {
				GUI.color = (levelManager.GetPumaHealth(2) <= 0f) ? pumaDeadColor : ((levelManager.GetPumaHealth(2) >= 1f) ? pumaFullHealthColor : pumaAliveColor);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			//}

			textureX = statusDisplayX + statusDisplayWidth * 0.604f;
			textureY = statusDisplayY + statusDisplayHeight * 0.1f;
			textureWidth = statusDisplayWidth * 0.12f;
			textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
			if (levelManager.GetPumaHealth(3) >= 1f) {
				GUI.color = new Color(1f, 1f, 1f, 0.7f * guiOpacity);
				GUI.DrawTexture(new Rect(textureX - (textureWidth * 0.1f), textureY - (textureHeight * 0.11f), textureWidth * 1.2f, textureHeight * 1.2f), pumaIconShadowYellowTexture);
				//GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				//GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}
			//else {
				GUI.color = (levelManager.GetPumaHealth(3) <= 0f) ? pumaDeadColor : ((levelManager.GetPumaHealth(3) >= 1f) ? pumaFullHealthColor : pumaAliveColor);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			//}

			textureX = statusDisplayX + statusDisplayWidth * 0.712f;
			textureY = statusDisplayY + statusDisplayHeight * 0.1f;
			textureWidth = statusDisplayWidth * 0.12f;
			textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
			if (levelManager.GetPumaHealth(4) >= 1f) {
				GUI.color = new Color(1f, 1f, 1f, 0.7f * guiOpacity);
				GUI.DrawTexture(new Rect(textureX - (textureWidth * 0.1f), textureY - (textureHeight * 0.11f), textureWidth * 1.2f, textureHeight * 1.2f), pumaIconShadowYellowTexture);
				//GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				//GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}
			//else {
				GUI.color = (levelManager.GetPumaHealth(4) <= 0f) ? pumaDeadColor : ((levelManager.GetPumaHealth(4) >= 1f) ? pumaFullHealthColor : pumaAliveColor);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			//}

			textureX = statusDisplayX + statusDisplayWidth * 0.820f;
			textureY = statusDisplayY + statusDisplayHeight * 0.1f;
			textureWidth = statusDisplayWidth * 0.12f;
			textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
			if (levelManager.GetPumaHealth(0) >= 1f) {
				GUI.color = new Color(1f, 1f, 1f, 0.7f * guiOpacity);
				GUI.DrawTexture(new Rect(textureX - (textureWidth * 0.1f), textureY - (textureHeight * 0.11f), textureWidth * 1.2f, textureHeight * 1.2f), pumaIconShadowYellowTexture);
				//GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				//GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}
			//else {
				GUI.color = (levelManager.GetPumaHealth(5) <= 0f) ? pumaDeadColor : ((levelManager.GetPumaHealth(5) >= 1f) ? pumaFullHealthColor : pumaAliveColor);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			//}
		}
		
		// health bar
		guiOpacity = (oldGuiOpacity + guiOpacity) / 2;		
		if (bareBonesFlag == true) {
			GUI.color = new Color(1f, 1f, 1f, 0.5f * guiOpacity);
			GUI.Box(new Rect(statusDisplayX + statusDisplayWidth * 0.29f, statusDisplayY + statusDisplayHeight * 0.59f, statusDisplayWidth * 0.64f, statusDisplayHeight * 0.3f), "");
		}
		CreateHealthBar(selectedPuma, statusDisplayX + statusDisplayWidth * 0.29f, statusDisplayY + statusDisplayHeight * 0.59f, statusDisplayWidth * 0.64f, statusDisplayHeight * 0.3f);
		guiOpacity = oldGuiOpacity;
	}
	

	void CreateFeedingDisplay(float feedingDisplayX, float feedingDisplayY, float feedingDisplayWidth, float feedingDisplayHeight) 
	{ 
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		
		// panel background
		GUI.color = new Color(1f, 1f, 1f, 0.8f * guiOpacity);
		GUI.Box(new Rect(feedingDisplayX, feedingDisplayY + feedingDisplayHeight * 0.06f, feedingDisplayWidth, feedingDisplayHeight * 1.2f - feedingDisplayHeight * 0.06f), "");
		GUI.color = new Color(1f, 1f, 1f, 0.3f * guiOpacity);
		GUI.Box(new Rect(feedingDisplayX, feedingDisplayY + feedingDisplayHeight * 0.06f, feedingDisplayWidth, feedingDisplayHeight * 1.2f - feedingDisplayHeight * 0.06f), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
	
		// main text
		Color topColor;
		Color midColor;
		Color bottomColor;
		string topString;
		string midString;
		int efficiencyLevel;
		string bottomString1;
		string bottomString2;
		float title1Offset = feedingDisplayWidth * -0.215f;
		float title2Offset = feedingDisplayWidth * 0.06f;
		float backgroundOffset = feedingDisplayWidth * 0f;

		if (levelManager.GetCaloriesExpended() > 1.2f * levelManager.GetCaloriesGained())
			efficiencyLevel = 0;
		else if (levelManager.GetCaloriesExpended() > levelManager.GetCaloriesGained())
			efficiencyLevel = 1;
		else if (levelManager.GetCaloriesExpended() > 0.8 * levelManager.GetCaloriesGained())
			efficiencyLevel = 2;
		else
			efficiencyLevel = 3;
				
		float calorieChange = levelManager.GetCaloriesGained() - levelManager.GetCaloriesExpended();
		if (calorieChange < 0)
			calorieChange = -calorieChange;
		int calorieDisplay = (int)calorieChange;

		switch (efficiencyLevel) {
		case 0:
			topColor = new Color(0.8f, 0f, 0f, 1f);
			midColor = new Color(0.82f, 0f, 0f, 1f);
			bottomColor = new Color(0.8f, 0f, 0f, 1f);
			topString = "WARNING:";
			midString = "WARNING: Your hunt was very inefficient";
			bottomString1 = "NET  LOSS -";
			bottomString2 = calorieDisplay.ToString("n0"); // + " calories";
			title1Offset = feedingDisplayWidth * -0.163f;
			title2Offset = feedingDisplayWidth * 0.075f;
			backgroundOffset = feedingDisplayWidth * 0.03f;
			break;
		
		case 1:
			topColor = new Color(0.83f, 0.78f, 0f, 1f);
			midColor = new Color(0.85f, 0.80f, 0f, 1f);
			bottomColor = new Color(0.8f, 0f, 0f, 1f);
			topString = "CAREFUL -";
			midString = "CAREFUL - Your hunt was somewhat inefficient";
			bottomString1 = "NET  LOSS -";
			bottomString2 = calorieDisplay.ToString("n0"); // + " calories";
			title1Offset = feedingDisplayWidth * -0.195f;
			title2Offset = feedingDisplayWidth * 0.08f;
			break;
		
		case 2:
			topColor = new Color(0.83f, 0.78f, 0f, 1f);
			midColor = new Color(0.85f, 0.80f, 0f, 1f);
			bottomColor = new Color(0f, 0.66f, 0f, 1f);
			topString = "WELL DONE -";
			midString = "WELL DONE - Your hunt was slightly efficient";
			bottomString1 = "NET  GAIN +";
			bottomString2 = calorieDisplay.ToString("n0"); // + " calories";
			title1Offset = feedingDisplayWidth * -0.18f;
			title2Offset = feedingDisplayWidth * 0.09f;
			backgroundOffset = feedingDisplayWidth * 0.01f;
			break;
		
		default:
			topColor = new Color(0f, 0.66f, 0f, 1f);
			midColor = new Color(0f, 0.70f, 0f, 1f);
			bottomColor = new Color(0f, 0.66f, 0f, 1f);
			topString = "CONGRATS!";
			midString = "CONGRATS! Your hunt was very efficient";
			bottomString1 = "NET  GAIN +";
			bottomString2 = calorieDisplay.ToString("n0"); // + " calories";
			title1Offset = feedingDisplayWidth * -0.158f;
			title2Offset = feedingDisplayWidth * 0.0845f;
			backgroundOffset = feedingDisplayWidth * 0.035f;
			break;
		}
		
		float fontRef = feedingDisplayHeight * 0.5f;
		style.fontStyle = FontStyle.BoldAndItalic;

		// main title

		GUI.color = new Color(1f, 1f, 1f, 0.8f * guiOpacity);
		GUI.Box(new Rect(feedingDisplayX, feedingDisplayY + feedingDisplayHeight * 0.06f, feedingDisplayWidth, feedingDisplayHeight * 0.18f), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);

		GUI.color = new Color(1f, 1f, 1f, 0.9f * guiOpacity);
		GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.22f + backgroundOffset, feedingDisplayY + feedingDisplayHeight * 0.1f, feedingDisplayWidth * 0.56f - backgroundOffset * 02f, feedingDisplayHeight * 0.11f), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);

		GUI.color = new Color(1f, 1f, 1f, 0.1f * guiOpacity);
		//GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.23f + backgroundOffset, feedingDisplayY + feedingDisplayHeight * 0.1f, feedingDisplayWidth * 0.54f - backgroundOffset * 02f, feedingDisplayHeight * 0.11f), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);

		style.fontSize = (int)(fontRef * 0.22f);
		style.normal.textColor =  topColor;
		style.fontStyle = FontStyle.Bold;
		//GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.3f + title1Offset, feedingDisplayY + feedingDisplayHeight * 0.135f, feedingDisplayWidth * 0.4f, feedingDisplayHeight * 0.03f), topString, style);
		style.fontSize = (int)(fontRef * 0.18f);
		GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.3f, feedingDisplayY + feedingDisplayHeight * 0.136f, feedingDisplayWidth * 0.4f, feedingDisplayHeight * 0.03f), midString, style);

		style.normal.textColor = midColor;
		style.fontStyle = FontStyle.BoldAndItalic;
		//GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.25f, feedingDisplayY + feedingDisplayHeight * 0.18f, feedingDisplayWidth * 0.5f, feedingDisplayHeight * 0.03f), midString, style);

		
		// "main menu" and "hunting tips" buttons
		
		GUI.skin = customGUISkin;
		customGUISkin.button.fontSize = (int)(feedingDisplayHeight * 0.067);
		customGUISkin.button.fontStyle = FontStyle.Normal;
		customGUISkin.button.fontStyle = FontStyle.Bold;
		customGUISkin.button.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);

		if (GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.035f,  feedingDisplayY + feedingDisplayHeight * 0.095f, feedingDisplayWidth * 0.14f, feedingDisplayHeight * 0.11f), "")) {
			SetGuiState("guiStateLeavingGameplay");
			levelManager.SetGameState("gameStateLeavingGameplay");
		}	
		if (GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.035f,  feedingDisplayY + feedingDisplayHeight * 0.095f, feedingDisplayWidth * 0.14f, feedingDisplayHeight * 0.11f), "Main Menu")) {
			SetGuiState("guiStateLeavingGameplay");
			levelManager.SetGameState("gameStateLeavingGameplay");
		}	
		
		GUI.skin = customGUISkin;
		customGUISkin.button.fontSize = (int)(feedingDisplayHeight * 0.0635);
		customGUISkin.button.fontStyle = FontStyle.Normal;
		customGUISkin.button.fontStyle = FontStyle.Bold;
		if (GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.825f,  feedingDisplayY + feedingDisplayHeight * 0.095f, feedingDisplayWidth * 0.14f, feedingDisplayHeight * 0.11f), "")) {
			popupPanelVisible = true;
			popupPanelPage = 3;
			popupPanelTransStart = Time.time;
		}
		if (GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.825f,  feedingDisplayY + feedingDisplayHeight * 0.095f, feedingDisplayWidth * 0.14f, feedingDisplayHeight * 0.11f), "Hunting Tips")) {
			popupPanelVisible = true;
			popupPanelPage = 3;
			popupPanelTransStart = Time.time;
		}
		
		customGUISkin.button.normal.textColor = new Color(1f, 0f, 0f, 1f);
		
		customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);
		
		

		// center panel

		GUI.color = new Color(1f, 1f, 1f, 0.8f * guiOpacity);
		GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.335f, feedingDisplayY + feedingDisplayHeight * 0.3f, feedingDisplayWidth * 0.33f, feedingDisplayHeight * 0.27f), "");
		GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.335f, feedingDisplayY + feedingDisplayHeight * 0.3f, feedingDisplayWidth * 0.33f, feedingDisplayHeight * 0.27f), "");
		GUI.color = new Color(1f, 1f, 1f, 0.9f * guiOpacity);
		GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.43f, feedingDisplayY + feedingDisplayHeight * 0.43f, feedingDisplayWidth * 0.14f, feedingDisplayHeight * 0.127f), "");
		//GUI.color = new Color(1f, 1f, 1f, 0.4f * guiOpacity);
		//GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.43f, feedingDisplayY + feedingDisplayHeight * 0.43f, feedingDisplayWidth * 0.14f, feedingDisplayHeight * 0.127f), "");

		style.fontSize = (int)(fontRef * 0.145f);
		style.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);
		style.normal.textColor =  bottomColor;
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.255f, feedingDisplayY + feedingDisplayHeight * 0.355f, feedingDisplayWidth * 0.5f, feedingDisplayHeight * 0.03f), bottomString1, style);

		style.fontSize = (int)(fontRef * 0.197f);
		style.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);
		style.normal.textColor =  midColor;
		GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.25f, feedingDisplayY + feedingDisplayHeight * 0.478f, feedingDisplayWidth * 0.5f, feedingDisplayHeight * 0.03f), bottomString2, style);
		
		// deer head & status info
		
		float panelOffsetY = -0.1f;

		GUI.color = new Color(1f, 1f, 1f, 0.8f * guiOpacity);
		GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.035f, feedingDisplayY + feedingDisplayHeight * 0.3f, feedingDisplayWidth * 0.3f, feedingDisplayHeight * 0.62f), "");
		GUI.color = new Color(1f, 1f, 1f, 0.5f * guiOpacity);
		GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.035f, feedingDisplayY + feedingDisplayHeight * 0.3f, feedingDisplayWidth * 0.3f, feedingDisplayHeight * 0.62f), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);

		//style.fontSize = (int)(fontRef * 0.28f);
		//style.normal.textColor = new Color(0.99f, 0.63f, 0f, 0.95f);
		//GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.15f, feedingDisplayY + feedingDisplayHeight * 0.6f, feedingDisplayWidth * 0.1f, feedingDisplayHeight * 0.03f), "|", style);
		//style.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);
		style.normal.textColor = new Color(0.90f, 0.65f, 0f,  0.9f);
		style.fontSize = (int)(fontRef * 0.16f);
		int meatJustEaten = (int)levelManager.GetMeatJustEaten();
		//GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.220f, feedingDisplayY + feedingDisplayHeight * (0.60f + panelOffsetY), feedingDisplayWidth * 0.1f, feedingDisplayHeight * 0.03f), meatJustEaten.ToString() + " lbs", style);
		GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.220f, feedingDisplayY + feedingDisplayHeight * (0.6f + panelOffsetY), feedingDisplayWidth * 0.1f, feedingDisplayHeight * 0.03f), "Meat", style);
		style.fontSize = (int)(fontRef * 0.12f);
		GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.220f, feedingDisplayY + feedingDisplayHeight * (0.678f + panelOffsetY), feedingDisplayWidth * 0.1f, feedingDisplayHeight * 0.03f), meatJustEaten.ToString() + " lbs", style);

		Texture2D displayHeadTexture = buckHeadTexture;
		string displayHeadLabel = "unnamed";
				
		switch (levelManager.GetDeerType()) {
			case 0:
				displayHeadTexture = buckHeadTexture;
				displayHeadLabel = "Buck";
				break;
			case 1:
				displayHeadTexture = doeHeadTexture;
				displayHeadLabel = "Doe";
				break;
			case 2:
				displayHeadTexture = fawnHeadTexture;
				displayHeadLabel = "Fawn";
				break;		
		}

		
		float textureX = feedingDisplayX + feedingDisplayWidth * 0.125f;
		float textureWidth = feedingDisplayHeight * 0.4f;
		float textureHeight = displayHeadTexture.height * (textureWidth / displayHeadTexture.width);
		float textureY = feedingDisplayY + feedingDisplayHeight * (0.32f + panelOffsetY);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), displayHeadTexture);

		style.normal.textColor = new Color(0.99f * 0.9f, 0.63f * 0.8f, 0f, 1f);
		style.fontSize = (int)(fontRef * 0.13f);
		GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.137f, feedingDisplayY + feedingDisplayHeight * (0.78f + panelOffsetY), feedingDisplayWidth * 0.1f, feedingDisplayHeight * 0.03f), displayHeadLabel, style);

		//style.fontSize = (int)(fontRef * 0.28f);
		//style.normal.textColor = new Color(0.99f, 0.63f, 0f, 0.95f);
		//GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.35f, feedingDisplayY + feedingDisplayHeight * 0.6f, feedingDisplayWidth * 0.1f, feedingDisplayHeight * 0.03f), "|", style);
		//style.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);
		style.normal.textColor = new Color(0.1f, 0.80f, 0.1f, 1f);
		//style.fontSize = (int)(fontRef * 0.33f);
		//GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.30f, feedingDisplayY + feedingDisplayHeight * 0.50f, feedingDisplayWidth * 0.1f, feedingDisplayHeight * 0.03f), "+", style);
		style.fontSize = (int)(fontRef * 0.18f);
		int caloriesGained = (int)levelManager.GetCaloriesGained();
		GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.040f, feedingDisplayY + feedingDisplayHeight * (0.60f + panelOffsetY), feedingDisplayWidth * 0.1f, feedingDisplayHeight * 0.03f), caloriesGained.ToString("n0"), style);
		style.fontSize = (int)(fontRef * 0.12f);
		GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.045f, feedingDisplayY + feedingDisplayHeight * (0.68f + panelOffsetY), feedingDisplayWidth * 0.1f, feedingDisplayHeight * 0.03f), "calories +", style);
		
		CreateFeedingBar(feedingDisplayX + feedingDisplayWidth * 0.040f + feedingDisplayHeight * 0.03f, feedingDisplayY + feedingDisplayHeight * 0.77f, feedingDisplayWidth * 0.29f - feedingDisplayHeight * 0.06f, feedingDisplayHeight * 0.12f);
		
		// puma head & status info

		
		GUI.color = new Color(1f, 1f, 1f, 0.8f * guiOpacity);
		GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.665f, feedingDisplayY + feedingDisplayHeight * 0.3f, feedingDisplayWidth * 0.3f, feedingDisplayHeight * 0.62f), "");
		GUI.color = new Color(1f, 1f, 1f, 0.5f * guiOpacity);
		GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.665f, feedingDisplayY + feedingDisplayHeight * 0.3f, feedingDisplayWidth * 0.3f, feedingDisplayHeight * 0.62f), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);

		style.normal.textColor = new Color(0.90f, 0.65f, 0f, 0.9f);
		style.fontSize = (int)(fontRef * 0.15f);
		GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.668f, feedingDisplayY + feedingDisplayHeight * (0.596f + panelOffsetY), feedingDisplayWidth * 0.1f, feedingDisplayHeight * 0.03f), "Energy", style);
		style.fontSize = (int)(fontRef * 0.14f);
		GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.668f, feedingDisplayY + feedingDisplayHeight * (0.678f + panelOffsetY), feedingDisplayWidth * 0.1f, feedingDisplayHeight * 0.03f), "Spent", style);


		// puma identity
		Texture2D headshotTexture = closeup1Texture;
		string pumaName = "no name";
		switch (selectedPuma) {
		case 0:
			headshotTexture = closeup1Texture;
			pumaName = "Eric";
			break;
		case 1:
			headshotTexture = closeup2Texture;
			pumaName = "Palo";
			break;
		case 2:
			headshotTexture = closeup3Texture;
			pumaName = "Mitch";
			break;
		case 3:
			headshotTexture = closeup4Texture;
			pumaName = "Trish";
			break;
		case 4:
			headshotTexture = closeup5Texture;
			pumaName = "Liam";
			break;
		case 5:
			headshotTexture = closeup6Texture;
			pumaName = "Barb";
			break;
		}


		// puma head
		//float statusDisplayOpacityDrop = 1f - statusDisplayOpacity;
		//guiOpacity = 1f - (statusDisplayOpacityDrop * 0.25f);
		//GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		textureX = feedingDisplayX + feedingDisplayWidth * 0.76f;
		textureY = feedingDisplayY + feedingDisplayHeight * (0.42f + panelOffsetY);
		textureWidth = feedingDisplayHeight * 0.39f;
		textureHeight = headshotTexture.height * (textureWidth / headshotTexture.width);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		//guiOpacity = guiOpacity * statusDisplayOpacity;
		//GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);

		// puma name
		//guiOpacity = 1f - (statusDisplayOpacityDrop * 0.75f);
		//GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		style.normal.textColor = new Color(0.99f * 0.9f, 0.63f * 0.8f, 0f, 1f);
		style.fontSize = (int)(fontRef * 0.13f);
		GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.767f, feedingDisplayY + feedingDisplayHeight * (0.78f + panelOffsetY), feedingDisplayWidth * 0.1f, feedingDisplayHeight * 0.03f), pumaName, style);
		//guiOpacity = guiOpacity * statusDisplayOpacity;
		//GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);

		
		style.normal.textColor = new Color(0.78f, 0f, 0f, 1f);
		//style.fontSize = (int)(fontRef * 0.12f);
		//GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.85f, feedingDisplayY + feedingDisplayHeight * 0.51f, feedingDisplayWidth * 0.1f, feedingDisplayHeight * 0.03f), "minus", style);
		style.fontSize = (int)(fontRef * 0.18f);
		int caloriesExpended = (int)levelManager.GetCaloriesExpended();
		GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.855f, feedingDisplayY + feedingDisplayHeight * (0.60f + panelOffsetY), feedingDisplayWidth * 0.1f, feedingDisplayHeight * 0.03f), caloriesExpended.ToString("n0"), style);
		style.fontSize = (int)(fontRef * 0.125f);
		GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.86f, feedingDisplayY + feedingDisplayHeight * (0.68f + panelOffsetY), feedingDisplayWidth * 0.1f, feedingDisplayHeight * 0.03f), "points -", style);
		
		CreateHealthBar(selectedPuma, feedingDisplayX + feedingDisplayWidth * 0.670f + feedingDisplayHeight * 0.03f, feedingDisplayY + feedingDisplayHeight * 0.775f, feedingDisplayWidth * 0.29f - feedingDisplayHeight * 0.06f, feedingDisplayHeight * 0.11f);

		
		// population bar
		
		GUI.color = new Color(1f, 1f, 1f, 0.8f * guiOpacity);
		GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.37f, feedingDisplayY + feedingDisplayHeight * 0.65f, feedingDisplayWidth * 0.26f, feedingDisplayHeight * 0.34f), "");
		GUI.color = new Color(1f, 1f, 1f, 0.4f * guiOpacity);
		GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.37f, feedingDisplayY + feedingDisplayHeight * 0.65f, feedingDisplayWidth * 0.26f, feedingDisplayHeight * 0.34f), "");
		GUI.color = new Color(1f, 1f, 1f, 0.4f * guiOpacity);
		GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.37f, feedingDisplayY + feedingDisplayHeight * 0.65f, feedingDisplayWidth * 0.26f, feedingDisplayHeight * 0.34f), "");
	
		GUI.color = new Color(1f, 1f, 1f, 0.4f * guiOpacity);
		GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.035f, feedingDisplayY + feedingDisplayHeight * 0.99f, feedingDisplayWidth * 0.93f, feedingDisplayHeight * 0.145f), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		CreatePopulationHealthBar(feedingDisplayX + feedingDisplayWidth * 0.035f, feedingDisplayY + feedingDisplayHeight * 0.99f, feedingDisplayWidth * 0.93f, feedingDisplayHeight * 0.145f, true, true);
		

		// 'Go' button
		
		if (guiState != "guiStateCaught3") {

			float storedGuiOpacity = guiOpacity;
			float elapsedTime = Time.time - stateStartTime;

			if (guiState == "guiStateCaught4") {
				if (elapsedTime <= 1f)
					guiOpacity = 0;
				else if (elapsedTime < 2f)
					guiOpacity = elapsedTime - 1f;
			}
			
			feedingDisplayX -= feedingDisplayWidth * 0.02f;
			feedingDisplayY += feedingDisplayHeight * 1.3f; // 1.5f;

			GUI.color = new Color(1f, 1f, 1f, 0.8f * guiOpacity);
			GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.78f, feedingDisplayY + feedingDisplayHeight * 0.67f, feedingDisplayWidth * 0.20f, feedingDisplayHeight * 0.37f), "");
			GUI.color = new Color(1f, 1f, 1f, 0.6f * guiOpacity);
			GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.78f, feedingDisplayY + feedingDisplayHeight * 0.67f, feedingDisplayWidth * 0.20f, feedingDisplayHeight * 0.37f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);

			GUI.color = new Color(1f, 1f, 1f, 0.8f * guiOpacity);
			GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.81f,  feedingDisplayY + feedingDisplayHeight * 0.727f, feedingDisplayWidth * 0.14f, feedingDisplayHeight * 0.25f), "");
			GUI.color = new Color(1f, 1f, 1f, 0.8f * guiOpacity);
			GUI.Box(new Rect(feedingDisplayX + feedingDisplayWidth * 0.81f,  feedingDisplayY + feedingDisplayHeight * 0.727f, feedingDisplayWidth * 0.14f, feedingDisplayHeight * 0.25f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);

			GUI.skin = customGUISkin;
			customGUISkin.button.fontSize = (int)(feedingDisplayHeight * 0.14);
			customGUISkin.button.fontStyle = FontStyle.Normal;
			if (GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.81f,  feedingDisplayY + feedingDisplayHeight * 0.727f, feedingDisplayWidth * 0.14f, feedingDisplayHeight * 0.25f), "Go")) {
				SetGuiState("guiStateCaught5");
				levelManager.SetGameState("gameStateCaught5");
			}	
			
			guiOpacity = storedGuiOpacity;
		
		}
		
		
		
/*		
		// health meter
		float healthMeterX = feedingDisplayX + feedingDisplayWidth * 0.05f;
		float healthMeterY = feedingDisplayY + feedingDisplayHeight * 0.75f;
		float healthMeterWidth = feedingDisplayWidth * 0.9f;
		float healthMeterHeight = feedingDisplayHeight * 0.17f;
		GUI.color = new Color(0f, 0f, 0f, 1f * guiOpacity);
		GUI.Box(new Rect(healthMeterX,  healthMeterY, healthMeterWidth, healthMeterHeight), "");
		GUI.Box(new Rect(healthMeterX,  healthMeterY, healthMeterWidth, healthMeterHeight), "");
		GUI.color = new Color(0f, 0f, 0f, 0.3f * guiOpacity);
		//GUI.Box(new Rect(healthMeterX,  healthMeterY, boxWidth * 0.8f, boxHeight * 0.12f), "");
		//GUI.Box(new Rect(healthMeterX,  healthMeterY, feedingDisplayWidth * 0.8f, feedingDisplayHeight * 0.15f), "");
		// health meter
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		float health = GetCurrentHealth();
		if (health >= 0.5f)
			health = 0.5f + (1f - health);
		Color healthColor = (health > 0.75f) ? new Color(0f, 1f, 0f, 0.7f) : ((health > 0.25f) ? new Color(1f, 1f, 0f, 0.81f) : new Color(1f, 0f, 0f, 1f));
		DrawRect(new Rect(healthMeterX + healthMeterWidth * 0.015f,  healthMeterY + healthMeterWidth * 0.015f, healthMeterWidth - healthMeterWidth * 0.03f, healthMeterHeight - healthMeterWidth * 0.03f), new Color(0f, 0f, 0f, 0.4f));	
		DrawRect(new Rect(healthMeterX + healthMeterWidth * 0.025f,  healthMeterY + healthMeterWidth * 0.025f, healthMeterWidth - healthMeterWidth * 0.05f, healthMeterHeight - healthMeterWidth * 0.05f), new Color(0.47f, 0.5f, 0.45f, 0.5f));	
		if (health >= 0.5f) {
			DrawRect(new Rect(healthMeterX + healthMeterWidth * 0.5f,  healthMeterY + healthMeterWidth * 0.025f, (healthMeterWidth*0.5f - healthMeterWidth * 0.025f) * ((health-0.5f) / 0.5f), healthMeterHeight - healthMeterWidth * 0.05f), healthColor);			
		}
		else {
			float rectRight = healthMeterX + healthMeterWidth * 0.5f;
			float rectWidth = (healthMeterWidth * 0.5f - healthMeterWidth * 0.025f) * ((0.5f - health) / 0.5f);
			DrawRect(new Rect(rectRight - rectWidth,  healthMeterY + healthMeterWidth * 0.025f, rectWidth, healthMeterHeight - healthMeterWidth * 0.05f), healthColor);			
		}
		// health meter graduations
		float barHeight = (healthMeterHeight - healthMeterWidth * 0.05f) * 3f;
		float barY = (healthMeterY + healthMeterWidth * 0.025f) - ((healthMeterHeight - healthMeterWidth * 0.05f) * 2f);
		float barWidth = healthMeterWidth * 0.016f;
		Color barColor = new Color(1f, 1f, 0f, 0.81f);
		DrawRect(new Rect(healthMeterX + healthMeterWidth * 0.5f - barWidth * 0.5f,  barY, barWidth, barHeight), barColor);			
		barColor = new Color(0f, 1f, 0f, 0.7f);
		DrawRect(new Rect(healthMeterX + healthMeterWidth  - healthMeterWidth * 0.025f - barWidth,  barY, barWidth, barHeight), barColor);			
		barColor = new Color(1f, 0f, 0f, 1f);
		DrawRect(new Rect(healthMeterX + healthMeterWidth * 0.025f,  barY, barWidth, barHeight), barColor);			
*/		
		
		
		
/*
		
		DrawRect(new Rect(feedingDisplayX + feedingDisplayWidth * 0.33f, feedingDisplayY, feedingDisplayWidth * 0.005f, feedingDisplayHeight), new Color(0f, 0f, 0f, 0.2f));	
		DrawRect(new Rect(feedingDisplayX + feedingDisplayWidth * 0.335f, feedingDisplayY, feedingDisplayWidth * 0.005f, feedingDisplayHeight), new Color(1f, 1f, 1f, 0.2f));	
		DrawRect(new Rect(feedingDisplayX + feedingDisplayWidth * 0.66f, feedingDisplayY, feedingDisplayWidth * 0.005f, feedingDisplayHeight * 0.52f), new Color(0f, 0f, 0f, 0.2f));	
		DrawRect(new Rect(feedingDisplayX + feedingDisplayWidth * 0.665f, feedingDisplayY, feedingDisplayWidth * 0.005f, feedingDisplayHeight * 0.52f), new Color(1f, 1f, 1f, 0.2f));	
		DrawRect(new Rect(feedingDisplayX + feedingDisplayWidth * 0.340f, feedingDisplayY + feedingDisplayHeight * 0.52f, feedingDisplayWidth * 0.660f, feedingDisplayWidth * 0.005f), new Color(0f, 0f, 0f, 0.2f));	
		DrawRect(new Rect(feedingDisplayX + feedingDisplayWidth * 0.340f, feedingDisplayY + feedingDisplayHeight * 0.52f + feedingDisplayWidth * 0.005f, feedingDisplayWidth * 0.660f, feedingDisplayWidth * 0.005f), new Color(1f, 1f, 1f, 0.2f));	

		style.fontSize = (int)(overlayRect.width * 0.020f);
		style.normal.textColor = new Color(0f, 0.68f, 0f, 1f);
		style.normal.textColor =  (pumasAlive >= 5) ? new Color(0f, 0.66f, 0f, 1f) : ((pumasAlive >= 3) ? new Color(0.83f, 0.78f, 0f, 1f) : new Color(0.8f, 0f, 0f, 1f));
		//style.normal.textColor = new Color(0.99f, 0.66f, 0f, 1f);
		style.fontStyle = FontStyle.Bold;
		GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.018f, feedingDisplayY + feedingDisplayHeight * 0.19f, feedingDisplayWidth * 0.3f, feedingDisplayHeight * 0.03f), (pumasAlive >= 5) ? "STATUS" : ((pumasAlive >= 3) ? "CAUTION" : "WARNING"), style);
		style.fontSize = (int)(overlayRect.width * 0.018f);
		style.normal.textColor =   (pumasAlive >= 5) ? new Color(0f, 0.70f, 0f, 1f) : ((pumasAlive >= 3) ? new Color(0.85f, 0.80f, 0f, 1f) : new Color(0.82f, 0f, 0f, 1f));
		style.fontStyle = FontStyle.BoldAndItalic;
		GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.018f, feedingDisplayY + feedingDisplayHeight * 0.48f, feedingDisplayWidth * 0.3f, feedingDisplayHeight * 0.03f), (pumasAlive >= 5) ? "Healthy" : ((pumasAlive >= 3) ? "Dwindling" : "Threat of"), style);
		GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.018f, feedingDisplayY + feedingDisplayHeight * 0.75f, feedingDisplayWidth * 0.3f, feedingDisplayHeight * 0.03f), (pumasAlive >= 5) ? "Population" : ((pumasAlive >= 3) ? "Population" : "Extinction"), style);

		style.fontSize = (int)(overlayRect.width * 0.0135f);
		style.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);
		style.fontStyle = FontStyle.Bold;
		GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.35f, feedingDisplayY + feedingDisplayHeight * 0.14f, feedingDisplayWidth * 0.3f, feedingDisplayHeight * 0.03f), "CALORIES", style);

		style.fontSize = (int)(overlayRect.width * 0.019f);
		style.normal.textColor = new Color(0.94f, 0.7f, 0f, 1f);
		style.fontStyle = FontStyle.BoldAndItalic;
		GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.35f, feedingDisplayY + feedingDisplayHeight * 0.35f, feedingDisplayWidth * 0.3f, feedingDisplayHeight * 0.03f), "11,700", style);

		style.fontSize = (int)(overlayRect.width * 0.0135f);
		style.normal.textColor = new Color(0f, 0.67f, 0f, 1f);
		style.fontStyle = FontStyle.Bold;
		GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.67f, feedingDisplayY + feedingDisplayHeight * 0.14f, feedingDisplayWidth * 0.3f, feedingDisplayHeight * 0.03f), "TARGET", style);

		style.fontSize = (int)(overlayRect.width * 0.019f);
		style.normal.textColor = new Color(0f, 0.70f, 0f, 1f);
		style.fontStyle = FontStyle.BoldAndItalic;
		GUI.Button(new Rect(feedingDisplayX + feedingDisplayWidth * 0.67f, feedingDisplayY + feedingDisplayHeight * 0.35f, feedingDisplayWidth * 0.3f, feedingDisplayHeight * 0.03f), "100,000", style);

		float pumaAliveOpacity = 0.95f;
		
		float textureX = feedingDisplayX + feedingDisplayWidth * 0.34f;
		float textureY = feedingDisplayY + feedingDisplayHeight * 0.58f;
		float textureWidth = feedingDisplayWidth * 0.12f;
		float textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
		GUI.color = new Color(1f, 1f, 1f, (pumasAlive >= 1) ? (pumaAliveOpacity * guiOpacity) : (0.4f * guiOpacity));
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);

		textureX = feedingDisplayX + feedingDisplayWidth * 0.44f;
		textureY = feedingDisplayY + feedingDisplayHeight * 0.58f;
		textureWidth = feedingDisplayWidth * 0.12f;
		textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
		GUI.color = new Color(1f, 1f, 1f, (pumasAlive >= 2) ? (pumaAliveOpacity * guiOpacity) : (0.4f * guiOpacity));
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);

		textureX = feedingDisplayX + feedingDisplayWidth * 0.54f;
		textureY = feedingDisplayY + feedingDisplayHeight * 0.58f;
		textureWidth = feedingDisplayWidth * 0.12f;
		textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
		GUI.color = new Color(1f, 1f, 1f, (pumasAlive >= 3) ? (pumaAliveOpacity * guiOpacity) : (0.4f * guiOpacity));
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);

		textureX = feedingDisplayX + feedingDisplayWidth * 0.64f;
		textureY = feedingDisplayY + feedingDisplayHeight * 0.58f;
		textureWidth = feedingDisplayWidth * 0.12f;
		textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
		GUI.color = new Color(1f, 1f, 1f, (pumasAlive >= 4) ? (pumaAliveOpacity * guiOpacity) : (0.4f * guiOpacity));
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);

		textureX = feedingDisplayX + feedingDisplayWidth * 0.74f;
		textureY = feedingDisplayY + feedingDisplayHeight * 0.58f;
		textureWidth = feedingDisplayWidth * 0.12f;
		textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
		GUI.color = new Color(1f, 1f, 1f, (pumasAlive >= 5) ? (pumaAliveOpacity * guiOpacity) : (0.4f * guiOpacity));
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);

		textureX = feedingDisplayX + feedingDisplayWidth * 0.84f;
		textureY = feedingDisplayY + feedingDisplayHeight * 0.58f;
		textureWidth = feedingDisplayWidth * 0.12f;
		textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
		GUI.color = new Color(1f, 1f, 1f, (pumasAlive >= 6) ? (pumaAliveOpacity * guiOpacity) : (0.4f * guiOpacity));
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
		
		
		*/
	}
	


	void CreateFeedingBar(float feedingBarX, float feedingBarY, float feedingBarWidth, float feedingBarHeight) 
	{ 
		float meatLevel = levelManager.GetMeatLevel();
		if (meatLevel > 1f)
			meatLevel = 1f;
			
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		
		// panel background
		GUI.color = new Color(1f, 1f, 1f, 0.8f * guiOpacity);
		GUI.Box(new Rect(feedingBarX, feedingBarY, feedingBarWidth, feedingBarHeight), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
	
		// meter label
		float fontRef = feedingBarHeight * 2f;
		style.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);
		style.fontSize = (int)(fontRef * 0.22f);
		style.fontStyle = FontStyle.Bold;
		GUI.Button(new Rect(feedingBarX + feedingBarWidth * 0.058f, feedingBarY + feedingBarHeight * 0.46f, feedingBarWidth * 0.1f, feedingBarHeight * 0.03f), "MEAT", style);
		style.fontSize = (int)(fontRef * 0.2f);
		//GUI.Button(new Rect(feedingBarX + feedingBarWidth * 0.055f, feedingBarY + feedingBarHeight * 0.65f, feedingBarWidth * 0.1f, feedingBarHeight * 0.03f), "Eaten", style);


		// feeding meter
		float meterLeft = 0.2075f;
		float meterRight = 0.2075f;
		float meterTop = 0.27f;		
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		GUI.Box(new Rect(feedingBarX + feedingBarWidth * meterLeft, feedingBarY + feedingBarHeight * meterTop, feedingBarWidth - feedingBarWidth * (meterLeft + meterRight), feedingBarHeight - feedingBarHeight * meterTop * 2), "");
		GUI.Box(new Rect(feedingBarX + feedingBarWidth * meterLeft, feedingBarY + feedingBarHeight * meterTop, feedingBarWidth - feedingBarWidth * (meterLeft + meterRight), feedingBarHeight - feedingBarHeight * meterTop * 2), "");

		meterLeft += 0.017f;
		meterRight += 0.017f;
		meterTop += 0.12f;		

		float meterWidth = feedingBarWidth - feedingBarWidth * (meterLeft + meterRight);
		float meterStatWidth = meterWidth * (meatLevel >= 0.1f ? 0.24f : 0.18f);
		Color feedingColor = new Color(0.99f, 0.6f, 0f, 1f);
		DrawRect(new Rect(feedingBarX + feedingBarWidth * meterLeft, feedingBarY + feedingBarHeight * meterTop, feedingBarWidth - feedingBarWidth * (meterLeft + meterRight), feedingBarHeight - feedingBarHeight * meterTop * 2), new Color(0.47f, 0.5f, 0.45f, 0.5f));	
		DrawRect(new Rect(feedingBarX + feedingBarWidth * meterLeft, feedingBarY + feedingBarHeight * meterTop, ((feedingBarWidth - feedingBarWidth * (meterLeft + meterRight)) - meterStatWidth) * meatLevel, feedingBarHeight - feedingBarHeight * meterTop * 2), feedingColor);			


		if (meatLevel > 0f) {
			// current value display
			meterTop -= 0.12f;		
			float meterX = feedingBarX + feedingBarWidth * meterLeft;
			float meterY = feedingBarY + feedingBarHeight * meterTop;
			float meterHeight = feedingBarHeight - feedingBarHeight * meterTop * 2;
			int meatLevelInt = (int)(meatLevel * 1000f);
			float meterStatX = meterX + (meterWidth - meterStatWidth) * meatLevel;
			GUI.Box(new Rect(meterStatX, meterY - meterHeight * 0.15f, meterStatWidth, meterHeight + meterHeight * 0.3f), "");
			//DrawRect(new Rect(meterStatX, meterY - meterHeight * 0.15f, meterStatWidth, meterHeight + meterHeight * 0.3f), new Color(0f, 0f, 0f, 1f));	
			string displayString = meatLevelInt.ToString();
			style.fontSize = (int)(fontRef * 0.235f);
			style.alignment = TextAnchor.MiddleCenter;
			style.normal.textColor = feedingColor;
			style.fontStyle = FontStyle.Bold;
			GUI.Button(new Rect(meterStatX - (meterStatWidth * (meatLevel == 1f ? -0.0f : (meatLevel >= 0.1f ? 0.06f : 0.046f))), meterY, meterStatWidth, meterHeight), displayString, style);
		}


		// meter label 2

		float textureX = feedingBarX + feedingBarWidth * 0.91f;
		float textureY = feedingBarY + feedingBarHeight * 0.1f;
		float textureWidth = feedingBarHeight * 0.67f;
		float textureHeight = greenCheckTexture.height * (textureWidth / greenCheckTexture.width);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), greenCheckTexture);


		style.normal.textColor = new Color(0f, 0.70f, 0f, 1f);
		style.fontSize = (int)(fontRef * 0.2f);
		style.fontStyle = FontStyle.BoldAndItalic;
		GUI.Button(new Rect(feedingBarX + feedingBarWidth * 0.8f, feedingBarY + feedingBarHeight * 0.30f, feedingBarWidth * 0.1f, feedingBarHeight * 0.03f), "1000", style);
		style.fontSize = (int)(fontRef * 0.18f);
		GUI.Button(new Rect(feedingBarX + feedingBarWidth * 0.805f, feedingBarY + feedingBarHeight * 0.65f, feedingBarWidth * 0.1f, feedingBarHeight * 0.03f), "Lbs", style);


	}

	
	void CreateHealthBar(int pumaNum, float healthBarX, float healthBarY, float healthBarWidth, float healthBarHeight, bool hideStatFlag = false, bool shiftStatFlag = false) 
	{ 
		float health = levelManager.GetPumaHealth(pumaNum); 
		float textureX;
		float textureY;
		float textureWidth;
		float textureHeight;

		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		
		// panel background
		GUI.color = new Color(1f, 1f, 1f, 0.8f * guiOpacity);
		GUI.Box(new Rect(healthBarX, healthBarY, healthBarWidth, healthBarHeight), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
	
		// puma crossbones
		if (health < 1f) {
			Texture2D crossboneTexture = (health > 0.66f || health < 0f) ? pumaCrossbonesDarkRedTexture : (health > 0.33 ? pumaCrossbonesDarkRedTexture : pumaCrossbonesRedTexture);
			textureX = healthBarX + healthBarWidth * 0.053f;
			textureY = healthBarY + healthBarHeight * 0.15f;
			textureWidth = healthBarHeight * .8f;
			textureHeight = crossboneTexture.height * (textureWidth / crossboneTexture.width) * 1f;
			GUI.color = new Color(1f, 1f, 1f, ((health > 0.66f || health < 0f) ? 0.9f : (health > 0.33f ? 0.975f : 1f)) * guiOpacity);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), crossboneTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		}

		// health meter

/*
		Color healthColor = (health > 0.66f) ? new Color(0f, 0.9f, 0f, 0.7f) : ((health > 0.33f) ? new Color(1f, 1f, 0f, 0.7f) : new Color(1f, 0f, 0f, 1f));
		float meterLeft = 0.2075f;
		float meterRight = 0.2075f;
		float meterTop = 0.27f;		
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		GUI.Box(new Rect(healthBarX + healthBarWidth * meterLeft, healthBarY + healthBarHeight * meterTop, healthBarWidth - healthBarWidth * (meterLeft + meterRight), healthBarHeight - healthBarHeight * meterTop * 2), "");
		GUI.Box(new Rect(healthBarX + healthBarWidth * meterLeft, healthBarY + healthBarHeight * meterTop, healthBarWidth - healthBarWidth * (meterLeft + meterRight), healthBarHeight - healthBarHeight * meterTop * 2), "");

		meterLeft += 0.017f;
		meterRight += 0.017f;
		meterTop += 0.12f;		
		DrawRect(new Rect(healthBarX + healthBarWidth * meterLeft, healthBarY + healthBarHeight * meterTop, healthBarWidth - healthBarWidth * (meterLeft + meterRight), healthBarHeight - healthBarHeight * meterTop * 2), new Color(0.47f, 0.5f, 0.45f, 0.5f));	
		if (health >= 0)
			DrawRect(new Rect(healthBarX + healthBarWidth * meterLeft, healthBarY + healthBarHeight * meterTop, (healthBarWidth - healthBarWidth * (meterLeft + meterRight)) * health, healthBarHeight - healthBarHeight * meterTop * 2), healthColor);			
*/
					
		Color labelColor;
		Color barColor;
		string displayString;
		
		//health = 0.7f;

		if (health < 0.2f) {
			labelColor = new Color(0.9f, 0f, 0f, 1f);
			barColor = new Color(0.86f, 0f, 0f, 1f);
		}
		else if (health < 0.4f) {
			labelColor = new Color(0.99f, 0.40f, 0f, 1f);
			barColor = new Color(0.99f, 0.40f, 0f, 1f);
		}
		else if (health < 0.6f) {
			labelColor = new Color(0.85f * 1f, 0.80f * 1f, 0f, 1f);
			barColor = new Color(0.85f * 0.85f, 0.80f * 0.85f, 0f, 1f);
		}
		else if (health < 0.8f) {
			labelColor = new Color(0.5f * 1.4f, 0.7f * 1.4f, 0f, 1f);
			barColor = new Color(0.5f * 1.04f, 0.7f * 1.04f, 0f, 1f);
		}
		else {
			labelColor = new Color(0f, 0.85f, 0f, 1f);
			barColor = (health >= 1f) ? new Color(0f, 0.75f, 0f, 0.45f) : new Color(0f, 0.75f, 0f, 0.95f);
		}			

		float fontRef = healthBarHeight * 2f;

		float meterLeft = 0.17f;
		float meterRight = 0.17f;
		float meterTop = 0.27f;		
		float meterX = healthBarX + healthBarWidth * meterLeft;
		float meterY = healthBarY + healthBarHeight * meterTop;
		float meterHeight = healthBarHeight - healthBarHeight * meterTop * 2;
		float meterWidth = healthBarWidth - healthBarWidth * (meterLeft + meterRight);
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		GUI.Box(new Rect(meterX, meterY, meterWidth, meterHeight), "");
		GUI.Box(new Rect(meterX, meterY, meterWidth, meterHeight), "");

		meterLeft += 0.007f;
		meterRight += 0.007f;
		meterTop += 0.12f;		
		meterX = healthBarX + healthBarWidth * meterLeft;
		meterWidth = healthBarWidth - healthBarWidth * (meterLeft + meterRight);
		float meterStatWidth = hideStatFlag == true ? 0f : (meterWidth * (health == 1f ? 0.28f : 0.24f));
		meterY = healthBarY + healthBarHeight * meterTop;
		meterHeight = healthBarHeight - healthBarHeight * meterTop * 2;
		if (health > 0) {
			DrawRect(new Rect(meterX, meterY, meterWidth, meterHeight), new Color(0.47f, 0.5f, 0.45f, 0.5f));	
			DrawRect(new Rect(meterX, meterY, (meterWidth - meterStatWidth) * health, meterHeight), barColor);		
		}
		else {
			DrawRect(new Rect(meterX, meterY, meterWidth, meterHeight), new Color(0.47f, 0.5f, 0.45f, 0.25f));	
		}

		if (hideStatFlag == false && health >= 0f) {
			// display current value 
			meterTop -= 0.12f;		
			meterY = healthBarY + healthBarHeight * meterTop;
			meterHeight = healthBarHeight - healthBarHeight * meterTop * 2;
			int healthPercent = (int)(health * 100f);
			float meterStatX = meterX + (meterWidth - meterStatWidth) * health;
			GUI.Box(new Rect(meterStatX, meterY - meterHeight * 0.3f, meterStatWidth, meterHeight + meterHeight * 0.6f), "");
			//DrawRect(new Rect(meterStatX, meterY - meterHeight * 0.3f, meterStatWidth, meterHeight + meterHeight * 0.6f), new Color(0f, 0f, 0f, 1f));	
			displayString = healthPercent.ToString() + "%";
			style.fontSize = (int)(fontRef * 0.28f);
			style.alignment = TextAnchor.MiddleCenter;
			style.normal.textColor = labelColor;
			style.fontStyle = FontStyle.Bold;
			GUI.Button(new Rect(meterStatX - (meterStatWidth * (health == 1f ? -0.0f : 0.025f)), meterY, meterStatWidth, meterHeight), displayString, style);
		}
		else if (hideStatFlag == true && shiftStatFlag == true) {
			// display current value above the bar and extra large
			meterStatWidth = (meterWidth * (health == 1f ? 0.68f : 0.64f));
			meterTop -= 0.12f;		
			meterY = healthBarY - healthBarHeight * 1.37f;
			meterHeight = healthBarHeight * 1f;
			int healthPercent = (int)(health * 100f);
			float meterStatX = meterX + meterWidth * 0.5f - meterStatWidth * 0.5f;
			if (health <= 0f || health >= 1f) {
				meterStatX = healthBarX + healthBarWidth * 0.05f;
				meterStatWidth = healthBarWidth * 0.9f;
			}
			GUI.Box(new Rect(meterStatX, meterY - meterHeight * 0.13f, meterStatWidth, meterHeight + meterHeight * 0.3f), "");
			GUI.Box(new Rect(meterStatX, meterY - meterHeight * 0.13f, meterStatWidth, meterHeight + meterHeight * 0.3f), "");
			//DrawRect(new Rect(meterStatX, meterY - meterHeight * 0.3f, meterStatWidth, meterHeight + meterHeight * 0.6f), new Color(0f, 0f, 0f, 1f));	

			float textLeftShift = 0f;

			if (health <= 0f) {
				displayString = " Starved to Death";
				style.fontStyle = FontStyle.BoldAndItalic;
				style.fontSize = (int)(fontRef * 0.37f);
				style.normal.textColor = new Color(0.54f, 0.02f, 0f, 0.99f);
			}
			else if (health >= 1f) {
				displayString = "Full Health";
				style.fontStyle = FontStyle.BoldAndItalic;
				style.fontSize = (int)(fontRef * 0.37f);
				style.normal.textColor = new Color(0.05f, 0.62f, 0f, 0.9f);			
				float checkMarkWidth = meterStatWidth * 0.2f;
				float checkMarkHeight = greenCheckTexture.height * (checkMarkWidth / greenCheckTexture.width);
				float checkMarkX = meterStatX + meterStatWidth * 0.8f;
				float checkMarkY = meterY - meterHeight * 0.2f;
				GUI.color = new Color(1f, 1f, 1f, 0.75f * guiOpacity);
				GUI.DrawTexture(new Rect(checkMarkX, checkMarkY, checkMarkWidth, checkMarkHeight), greenCheckTexture);
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				textLeftShift = meterStatWidth * 0.04f;
			}
			else {
				displayString = healthPercent.ToString() + "%";
				style.fontStyle = FontStyle.Bold;
				style.fontSize = (int)(fontRef * 0.48f);
				style.normal.textColor = labelColor;
			}
			style.alignment = TextAnchor.MiddleCenter;
			GUI.Button(new Rect(meterStatX - (meterStatWidth * (health == 1f ? -0.0f : 0.025f)) - textLeftShift, meterY, meterStatWidth, meterHeight), displayString, style);
		}
			
		// green heart
		if (health > 0f) {
			textureX = healthBarX + healthBarWidth * 0.86f;
			textureY = healthBarY + healthBarHeight * 0.19f;
			textureWidth = healthBarHeight * 0.64f;
			textureHeight = greenHeartTexture.height * (textureWidth / greenHeartTexture.width) * 1f;
			GUI.color = new Color(1f, 1f, 1f, (health > 0.66f ? 0.8f : (health > 0.33f ? 0.68f : 0.5f)) * guiOpacity);
			if (health >= 1f)
				GUI.color = new Color(1f, 1f, 1f, 0.9f * guiOpacity);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), greenHeartTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		}
	}

	
	void CreatePopulationHealthBar(float healthBarX, float healthBarY, float healthBarWidth, float healthBarHeight, bool showPumaIcons, bool centerLabels) 
	{ 
		float health = 0; 
		health += levelManager.GetPumaHealth(0); 
		health += levelManager.GetPumaHealth(1); 
		health += levelManager.GetPumaHealth(2); 
		health += levelManager.GetPumaHealth(3); 
		health += levelManager.GetPumaHealth(4); 
		health += levelManager.GetPumaHealth(5); 
		health /= 6;
		
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;

		// panel background
		GUI.color = new Color(1f, 1f, 1f, 0.8f * guiOpacity);
		GUI.Box(new Rect(healthBarX, healthBarY, healthBarWidth, healthBarHeight), "");
		GUI.Box(new Rect(healthBarX, healthBarY, healthBarWidth, healthBarHeight), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
	

		float textureX;
		float textureY;
		float textureWidth;
		float textureHeight;


		if (showPumaIcons == true) {

			// six puma icons
			
			float pumaIconWidth = healthBarHeight * 0.85f;
			float pumaIncrementX = healthBarWidth * 0.038f;
			float pumaIconY = healthBarY - healthBarHeight * 1.32f;
			
			//Color pumaAliveColor = new Color(1f, 1f, 1f, 0.85f * guiOpacity);
			//Color pumaDeadColor = new Color(0.5f, 0.05f, 0f, 0.9f * guiOpacity);
		

			Color pumaAliveColor = new Color(1f, 1f, 1f, 0.9f * guiOpacity);
			Color pumaFullHealthColor = new Color(0.32f, 0.99f, 0f, 0.9f * guiOpacity);
			Color pumaDeadColor = new Color(0.6f, 0.05f, 0f, 0.8f * guiOpacity);


			textureX = healthBarX + healthBarWidth * 0.383f;
			textureY = pumaIconY;
			textureWidth = pumaIconWidth;
			textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
			if (selectedPuma == -1) {
				GUI.color = new Color(1f, 1f, 1f, 0.8f * guiOpacity);
				GUI.DrawTexture(new Rect(textureX - (textureWidth * 0.1f), textureY - (textureHeight * 0.11f), textureWidth * 1.2f, textureHeight * 1.2f), pumaIconShadowYellowTexture);
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}
			else {
				GUI.color = (levelManager.GetPumaHealth(0) <= 0f) ? pumaDeadColor : ((levelManager.GetPumaHealth(0) >= 1f) ? pumaFullHealthColor : pumaAliveColor);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}
			
			textureX += pumaIncrementX;
			textureY = pumaIconY;
			textureWidth = pumaIconWidth;
			textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
			if (selectedPuma == -1) {
				GUI.color = new Color(1f, 1f, 1f, 0.8f * guiOpacity);
				GUI.DrawTexture(new Rect(textureX - (textureWidth * 0.1f), textureY - (textureHeight * 0.11f), textureWidth * 1.2f, textureHeight * 1.2f), pumaIconShadowYellowTexture);
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}
			else {
				GUI.color = (levelManager.GetPumaHealth(1) <= 0f) ? pumaDeadColor : ((levelManager.GetPumaHealth(1) >= 1f) ? pumaFullHealthColor : pumaAliveColor);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}

			textureX += pumaIncrementX;
			textureY = pumaIconY;
			textureWidth = pumaIconWidth;
			textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
			if (selectedPuma == -1) {
				GUI.color = new Color(1f, 1f, 1f, 0.8f * guiOpacity);
				GUI.DrawTexture(new Rect(textureX - (textureWidth * 0.1f), textureY - (textureHeight * 0.11f), textureWidth * 1.2f, textureHeight * 1.2f), pumaIconShadowYellowTexture);
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}
			else {
				GUI.color = (levelManager.GetPumaHealth(2) <= 0f) ? pumaDeadColor : ((levelManager.GetPumaHealth(2) >= 1f) ? pumaFullHealthColor : pumaAliveColor);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}

			textureX += pumaIncrementX;
			textureY = pumaIconY;
			textureWidth = pumaIconWidth;
			textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
			if (selectedPuma == -1) {
				GUI.color = new Color(1f, 1f, 1f, 0.8f * guiOpacity);
				GUI.DrawTexture(new Rect(textureX - (textureWidth * 0.1f), textureY - (textureHeight * 0.11f), textureWidth * 1.2f, textureHeight * 1.2f), pumaIconShadowYellowTexture);
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}
			else {
				GUI.color = (levelManager.GetPumaHealth(3) <= 0f) ? pumaDeadColor : ((levelManager.GetPumaHealth(3) >= 1f) ? pumaFullHealthColor : pumaAliveColor);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}

			textureX += pumaIncrementX;
			textureY = pumaIconY;
			textureWidth = pumaIconWidth;
			textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
			if (selectedPuma == -1) {
				GUI.color = new Color(1f, 1f, 1f, 0.8f * guiOpacity);
				GUI.DrawTexture(new Rect(textureX - (textureWidth * 0.1f), textureY - (textureHeight * 0.11f), textureWidth * 1.2f, textureHeight * 1.2f), pumaIconShadowYellowTexture);
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}
			else {
				GUI.color = (levelManager.GetPumaHealth(4) <= 0f) ? pumaDeadColor : ((levelManager.GetPumaHealth(4) >= 1f) ? pumaFullHealthColor : pumaAliveColor);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}

			textureX += pumaIncrementX;
			textureY = pumaIconY;
			textureWidth = pumaIconWidth;
			textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
			if (selectedPuma == -1) {
				GUI.color = new Color(1f, 1f, 1f, 0.8f * guiOpacity);
				GUI.DrawTexture(new Rect(textureX - (textureWidth * 0.1f), textureY - (textureHeight * 0.11f), textureWidth * 1.2f, textureHeight * 1.2f), pumaIconShadowYellowTexture);
				GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}
			else {
				GUI.color = (levelManager.GetPumaHealth(5) <= 0f) ? pumaDeadColor : ((levelManager.GetPumaHealth(5) >= 1f) ? pumaFullHealthColor : pumaAliveColor);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}
		}

		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);

		Color labelColor;
		Color barColor;
		string displayString;
		float xOffset;
		float xOffset2;
			
		//health = 0.7f;

		if (health < 0.2f) {
			labelColor = new Color(0.9f, 0f, 0f, 1f);
			barColor = new Color(0.86f, 0f, 0f, 1f);
			displayString = "Critical";
			xOffset = healthBarWidth * 0.02f;
			xOffset2 = healthBarWidth * 0f;
		}
		else if (health < 0.4f) {
			labelColor = new Color(0.975f, 0.40f, 0f, 1f);
			barColor = new Color(0.99f, 0.40f, 0f, 1f);
			displayString = "Endangered";
			xOffset = healthBarWidth * -0.005f;
			xOffset2 = healthBarWidth * -0.012f;
		}
		else if (health < 0.6f) {
			labelColor = new Color(0.85f * 1.13f, 0.80f * 1.13f, 0f, 1f);
			barColor = new Color(0.85f * 0.90f, 0.80f * 0.90f, 0f, 1f);
			displayString = "Sustaining";
			xOffset = healthBarWidth * 0.005f;
			xOffset2 = healthBarWidth * 0f;
		}
		else if (health < 0.8f) {
			labelColor = new Color(0.5f * 1.4f, 0.7f * 1.4f, 0f, 1f);
			barColor = new Color(0.5f * 1.04f, 0.7f * 1.04f, 0f, 1f);
			displayString = "Established";
			xOffset = healthBarWidth * 0f;
			xOffset2 = healthBarWidth * 0f;
		}
		else {
			labelColor = new Color(0f, 0.85f, 0f, 1f);
			barColor = new Color(0f, 0.75f, 0f, 1f);
			displayString = "Thriving";
			xOffset = healthBarWidth * 0.018f;
			xOffset2 = healthBarWidth * 0f;
		}			

		float fontRef = healthBarHeight * 2f;
		
		if (showPumaIcons == true) {
			// label goes above bar
			style.fontStyle = FontStyle.Bold;
			style.alignment = TextAnchor.MiddleLeft;
			style.fontSize = (int)(fontRef * 0.20f);
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			GUI.Button(new Rect(xOffset + healthBarX + healthBarWidth * 0.383f, healthBarY - healthBarHeight * 1.85f, healthBarWidth * 0.3f, healthBarHeight * 0.03f), "POPULATION:", style);
			style.fontSize = (int)(fontRef * 0.24f);
			style.normal.textColor = labelColor;
			GUI.Button(new Rect(xOffset + healthBarX + healthBarWidth * 0.505f, healthBarY - healthBarHeight * 1.85f, healthBarWidth * 0.3f, healthBarHeight * 0.03f), displayString, style);
		}
		else if (centerLabels == false) {
			// labels go to sides of bar
			style.fontStyle = FontStyle.Bold;
			style.alignment = TextAnchor.MiddleLeft;
			style.fontSize = (int)(fontRef * 0.21f);
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			GUI.Button(new Rect(xOffset2 + overlayRect.x + overlayRect.width * 0.060f, healthBarY + healthBarHeight * 0.01f, healthBarWidth * 0.3f, healthBarHeight * 1f), "POPULATION", style);
			style.fontSize = (int)(fontRef * 0.26f);
			style.normal.textColor = labelColor;
			GUI.Button(new Rect(xOffset2 + overlayRect.x + overlayRect.width * 0.86f, healthBarY - healthBarHeight * 0.0255f, healthBarWidth * 0.3f, healthBarHeight * 1f), displayString, style);
		}
		else {
			// labels go stacked above bar
			style.fontStyle = FontStyle.Bold;
			style.alignment = TextAnchor.MiddleCenter;
			style.fontSize = (int)(fontRef * 0.20f);
			//style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			style.normal.textColor = new Color(0.99f * 0.9f, 0.75f * 0.9f, 0.3f * 0.9f, 0.95f);
			//GUI.Button(new Rect(healthBarX, healthBarY - healthBarHeight * 2.27f, healthBarWidth, healthBarHeight * 0.03f), "- STATUS -", style);
			style.fontSize = (int)(fontRef * 0.275f);
			style.normal.textColor = labelColor;
			GUI.Button(new Rect(healthBarX, healthBarY - healthBarHeight * 1.9f, healthBarWidth, healthBarHeight * 0.03f), displayString, style);
		}

		// puma crossbones
		Texture2D crossboneTexture = (health > 0.60f || health < 0f) ? pumaCrossbonesDarkRedTexture : (health > 0.40 ? pumaCrossbonesDarkRedTexture : pumaCrossbonesRedTexture);
		textureX = healthBarX + healthBarWidth * 0.02f;
		textureY = healthBarY + healthBarHeight * 0.14f;
		textureWidth = healthBarHeight * .76f;
		textureHeight = crossboneTexture.height * (textureWidth / crossboneTexture.width) * 1f;
		GUI.color = new Color(1f, 1f, 1f, ((health > 0.66f || health < 0f) ? 0.9f : (health > 0.33f ? 0.975f : 1f)) * guiOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), crossboneTexture);
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);


		// health meter
		float meterLeft = 0.065f;
		float meterRight = 0.065f;
		float meterTop = 0.287f;		
		float meterX = healthBarX + healthBarWidth * meterLeft;
		float meterWidth = healthBarWidth - healthBarWidth * (meterLeft + meterRight);
		float meterY = healthBarY + healthBarHeight * meterTop;
		float meterHeight = healthBarHeight - healthBarHeight * meterTop * 2;
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
		GUI.Box(new Rect(meterX, meterY, meterWidth, meterHeight), "");

		meterLeft += 0.007f;
		meterRight += 0.007f;
		meterTop += 0.12f;		
		meterX = healthBarX + healthBarWidth * meterLeft;
		meterWidth = healthBarWidth - healthBarWidth * (meterLeft + meterRight);
		float meterStatWidth = meterWidth * 0.065f;
		meterY = healthBarY + healthBarHeight * meterTop;
		meterHeight = healthBarHeight - healthBarHeight * meterTop * 2;
		DrawRect(new Rect(meterX, meterY, meterWidth, meterHeight), new Color(0.47f, 0.5f, 0.45f, 0.5f));	
		if (health >= 0)
			DrawRect(new Rect(meterX, meterY, (meterWidth - meterStatWidth) * health, meterHeight), barColor);			

			
		// display current value
		meterTop -= 0.12f;		
		meterY = healthBarY + healthBarHeight * meterTop;
		meterHeight = healthBarHeight - healthBarHeight * meterTop * 2;
		int healthPercent = (int)(health * 100f);
		float meterStatX = meterX + (meterWidth - meterStatWidth) * health;
		GUI.Box(new Rect(meterStatX, meterY - meterHeight * 0.25f, meterStatWidth, meterHeight + meterHeight * 0.5f), "");
		//DrawRect(new Rect(meterStatX, meterY - meterHeight * 0.25f, meterStatWidth, meterHeight + meterHeight * 0.5f), new Color(0f, 0f, 0f, 1f));	
		displayString = healthPercent.ToString() + "%";
		style.fontSize = (int)(fontRef * 0.26f);
		style.alignment = TextAnchor.MiddleCenter;
		style.normal.textColor = labelColor;
		style.fontStyle = FontStyle.Bold;
		GUI.Button(new Rect(meterStatX - (meterStatWidth * (health == 1f ? -0.07f : 0.02f)), meterY, meterStatWidth, meterHeight), displayString, style);
			
		
		// green heart
		textureX = healthBarX + healthBarWidth * 0.947f;
		textureY = healthBarY + healthBarHeight * 0.17f;
		textureWidth = healthBarHeight * 0.64f;
		textureHeight = greenHeartTexture.height * (textureWidth / greenHeartTexture.width) * 1f;
		GUI.color = new Color(1f, 1f, 1f, (health > 0.66f ? 0.8f : (health > 0.33f ? 0.65f : 0.5f)) * guiOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), greenHeartTexture);
		GUI.color = new Color(1f, 1f, 1f, 1f * guiOpacity);
	}

	
	/////////////////////
	////// POPUP PANEL
	/////////////////////
	

	void DrawPopupPanel(float percentVisible) 
	{ 
		GUIStyle style = new GUIStyle();

		float popupPanelX = Screen.width * 0.5f - Screen.height * 0.75f;
		float popupPanelWidth = Screen.height * 1.5f;
		float popupPanelY = Screen.height * 0.025f;
		float popupPanelHeight = Screen.height * 0.95f;

		float popupInnerRectX = popupPanelX + popupPanelWidth * 0.01f;
		float popupInnerRectY = popupPanelY + popupPanelWidth * 0.01f;
		float popupInnerRectWidth = popupPanelWidth - popupPanelWidth * 0.02f;
		float popupInnerRectHeight = popupPanelHeight - popupPanelWidth * 0.02f;

		//backdrop
		GUI.color = new Color(1f, 1f, 1f, 1f * percentVisible);
		GUI.Box(new Rect(popupPanelX, popupPanelY, popupPanelWidth, popupPanelHeight), "");
		//DrawRect(new Rect(popupPanelX + popupPanelWidth * 0.01f, popupPanelY + popupPanelWidth * 0.01f, popupPanelWidth * 0.98f, popupPanelHeight - popupPanelWidth * 0.02f), new Color(0.22f, 0.21f, 0.20f, 1f));	
		DrawRect(new Rect(popupInnerRectX, popupInnerRectY, popupInnerRectWidth, popupInnerRectHeight * 0.07f), new Color(0.205f, 0.21f, 0.19f, 1f));	
		DrawRect(new Rect(popupInnerRectX, popupInnerRectY + popupInnerRectHeight * 0.11f, popupInnerRectWidth, popupInnerRectHeight - popupInnerRectHeight * 0.11f - popupInnerRectHeight * 0.09f), new Color(0.205f, 0.21f, 0.19f, 1f));	
				
				
		// main title & page contents
		
		float textureX;
		float textureY;
		float textureHeight;
		float textureWidth;
		
		string titleString = "not specified";
		switch (popupPanelPage) {

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
			GUI.color = new Color(1f, 1f, 1f, 0.4f * percentVisible);
			textureX = popupInnerRectX + popupInnerRectWidth * 0.052f;
			textureY = popupInnerRectY + popupInnerRectHeight * 0.32f;
			textureHeight = popupInnerRectHeight * 0.55f;
			textureWidth = forestTexture.width * (textureHeight / forestTexture.height);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), forestTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * percentVisible);
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
			GUI.color = new Color(1f, 1f, 1f, 0.4f * percentVisible);
			textureX = popupInnerRectX + popupInnerRectWidth * 0.53f;
			textureY = popupInnerRectY + popupInnerRectHeight * 0.32f;
			textureHeight = popupInnerRectHeight * 0.55f;
			textureWidth = forestTexture.width * (textureHeight / forestTexture.height);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), forestTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * percentVisible);
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

		if (popupPanelIntroFlag == false) {

			customGUISkin.button.fontSize = (int)(overlayRect.width * 0.0196);
		
			// introduction
			
			GUI.color = new Color(1f, 1f, 1f, 0.15f * percentVisible);
			if (popupPanelPage != 0)
				DrawRect(new Rect(buttonX - buttonBorderWidth, buttonY - buttonBorderWidth, buttonWidth + buttonBorderWidth * 2f, buttonHeight + buttonBorderWidth * 2f), new Color(1f, 1f, 1f, 1f));	
			GUI.skin = customGUISkin;
			customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			GUI.color = new Color(1f, 1f, 1f, 1f * percentVisible);
			GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
			if (popupPanelPage != 0)
				customGUISkin.button.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1f);
			else
				customGUISkin.button.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "")) {
				popupPanelPage = 0;
			}
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "Overview")) {
				popupPanelPage = 0;
			}
			GUI.color = new Color(1f, 1f, 1f, 1f * percentVisible);
			customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);

			// puma biology
			
			buttonX += buttonWidth + buttonGap;
			
			GUI.color = new Color(1f, 1f, 1f, 0.15f * percentVisible);
			if (popupPanelPage != 1)
				DrawRect(new Rect(buttonX - buttonBorderWidth, buttonY - buttonBorderWidth, buttonWidth + buttonBorderWidth * 2f, buttonHeight + buttonBorderWidth * 2f), new Color(1f, 1f, 1f, 1f));	
			GUI.skin = customGUISkin;
			customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			GUI.color = new Color(1f, 1f, 1f, 1f * percentVisible);
			GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
			if (popupPanelPage != 1)
				customGUISkin.button.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1f);
			else
				customGUISkin.button.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "")) {
				popupPanelPage = 1;
			}
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "Biology")) {
				popupPanelPage = 1;
			}
			GUI.color = new Color(1f, 1f, 1f, 1f * percentVisible);
			customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);

			// puma ecology
			
			buttonX += buttonWidth + buttonGap;
			
			GUI.color = new Color(1f, 1f, 1f, 0.15f * percentVisible);
			if (popupPanelPage != 2)
				DrawRect(new Rect(buttonX - buttonBorderWidth, buttonY - buttonBorderWidth, buttonWidth + buttonBorderWidth * 2f, buttonHeight + buttonBorderWidth * 2f), new Color(1f, 1f, 1f, 1f));	
			GUI.skin = customGUISkin;
			customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			GUI.color = new Color(1f, 1f, 1f, 1f * percentVisible);
			GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
			if (popupPanelPage != 2)
				customGUISkin.button.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1f);
			else
				customGUISkin.button.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "")) {
				popupPanelPage = 2;
			}
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "Ecology")) {
				popupPanelPage = 2;
			}
			GUI.color = new Color(1f, 1f, 1f, 1f * percentVisible);
			customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);

			// catching prey
			
			buttonX += buttonWidth + buttonGap;
			
			GUI.color = new Color(1f, 1f, 1f, 0.15f * percentVisible);
			if (popupPanelPage != 3)
				DrawRect(new Rect(buttonX - buttonBorderWidth, buttonY - buttonBorderWidth, buttonWidth + buttonBorderWidth * 2f, buttonHeight + buttonBorderWidth * 2f), new Color(1f, 1f, 1f, 1f));	
			GUI.skin = customGUISkin;
			customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			GUI.color = new Color(1f, 1f, 1f, 1f * percentVisible);
			GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
			if (popupPanelPage != 3)
				customGUISkin.button.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1f);
			else
				customGUISkin.button.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "")) {
				popupPanelPage = 3;
			}
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "Predation")) {
				popupPanelPage = 3;
			}
			GUI.color = new Color(1f, 1f, 1f, 1f * percentVisible);
			customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);

			// survival threats
			
			buttonX += buttonWidth + buttonGap;
			
			GUI.color = new Color(1f, 1f, 1f, 0.15f * percentVisible);
			if (popupPanelPage != 4)
				DrawRect(new Rect(buttonX - buttonBorderWidth, buttonY - buttonBorderWidth, buttonWidth + buttonBorderWidth * 2f, buttonHeight + buttonBorderWidth * 2f), new Color(1f, 1f, 1f, 1f));	
			GUI.skin = customGUISkin;
			customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			GUI.color = new Color(1f, 1f, 1f, 1f * percentVisible);
			GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
			if (popupPanelPage != 4)
				customGUISkin.button.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1f);
			else
				customGUISkin.button.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "")) {
				popupPanelPage = 4;
			}
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "Mortality")) {
				popupPanelPage = 4;
			}
			GUI.color = new Color(1f, 1f, 1f, 1f * percentVisible);
			customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);

			// help pumas
			
			buttonX += buttonWidth + buttonGap;
			
			GUI.color = new Color(1f, 1f, 1f, 0.15f * percentVisible);
			if (popupPanelPage != 5)
				DrawRect(new Rect(buttonX - buttonBorderWidth, buttonY - buttonBorderWidth, buttonWidth + buttonBorderWidth * 2f, buttonHeight + buttonBorderWidth * 2f), new Color(1f, 1f, 1f, 1f));	
			GUI.skin = customGUISkin;
			customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			GUI.color = new Color(1f, 1f, 1f, 1f * percentVisible);
			GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
			if (popupPanelPage != 5)
				customGUISkin.button.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1f);
			else
				customGUISkin.button.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "")) {
				popupPanelPage = 5;
			}
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "How to Help")) {
				popupPanelPage = 5;
			}
			GUI.color = new Color(1f, 1f, 1f, 1f * percentVisible);
			customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);
			
			// DONE
			
			buttonX += buttonWidth + buttonGap;
			
			GUI.color = new Color(1f, 1f, 1f, 0.15f * percentVisible);
			if (popupPanelPage != 6)
				DrawRect(new Rect(buttonX - buttonBorderWidth, buttonY - buttonBorderWidth, buttonWidth + buttonBorderWidth * 2f, buttonHeight + buttonBorderWidth * 2f), new Color(1f, 1f, 1f, 1f));	
			GUI.skin = customGUISkin;
			customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			GUI.color = new Color(1f, 1f, 1f, 1f * percentVisible);
			GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
			if (popupPanelPage != 6)
				customGUISkin.button.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1f);
			else
				customGUISkin.button.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "")) {
				popupPanelVisible = false;
				popupPanelTransStart = Time.time;
			}
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "CLOSE   X")) {
				popupPanelVisible = false;
				popupPanelTransStart = Time.time;
			}
			GUI.color = new Color(1f, 1f, 1f, 1f * percentVisible);
			customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);
			
		}
		else {
			// skip intro
			
			buttonY -= buttonHeight * 0.15f;
			buttonHeight *= 1.3f;

			buttonWidth *= 1.2f;
			buttonX = popupInnerRectX + popupInnerRectWidth * 0.5f - buttonWidth * 0.5f;

			GUI.color = new Color(1f, 1f, 1f, 0.15f * percentVisible);
			if (popupPanelPage != 0)
				DrawRect(new Rect(buttonX - buttonBorderWidth, buttonY - buttonBorderWidth, buttonWidth + buttonBorderWidth * 2f, buttonHeight + buttonBorderWidth * 2f), new Color(1f, 1f, 1f, 1f));	
			GUI.skin = customGUISkin;
			customGUISkin.button.fontSize = (int)(overlayRect.width * 0.026);
			customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			GUI.color = new Color(1f, 1f, 1f, 1f * percentVisible);
			GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "")) {
				popupPanelVisible = false;
				popupPanelTransStart = Time.time;
				SetGuiState("guiStateEnteringApp3");
			}
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "GO")) {
				popupPanelVisible = false;
				popupPanelTransStart = Time.time;
				SetGuiState("guiStateEnteringApp3");
			}
			GUI.color = new Color(1f, 1f, 1f, 1f * percentVisible);
		}
		
		
		if (popupPanelIntroFlag == true || popupPanelPage == 0) {
		
		
		
		}
		else if (popupPanelPage != 5) {
		
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

		if (popupPanelIntroFlag == false && popupPanelPage != 0) {
		
			float yOffsetForPage5 = popupPanelPage == 5 ? (popupInnerRectHeight * -0.12f) : 0f;
		
			if (popupPanelPage == 5)
				customGUISkin.button.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			else
				customGUISkin.button.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1f);

			style.fontStyle = FontStyle.BoldAndItalic;
			style.alignment = TextAnchor.MiddleCenter;
			style.fontSize = (int)(popupInnerRectWidth * 0.014f);
			style.normal.textColor = new Color(0.6f, 0.6f, 0.6f, 1f);
			if (popupPanelPage == 5)
				GUI.Button(new Rect(popupInnerRectX + popupInnerRectWidth * 0.4f, popupInnerRectY + popupInnerRectHeight * 0.765f + yOffsetForPage5, popupInnerRectWidth * 0.2f, popupInnerRectHeight * 0.06f), "Get involved...", style);
			else
				GUI.Button(new Rect(popupInnerRectX + popupInnerRectWidth * 0.4f, popupInnerRectY + popupInnerRectHeight * 0.765f, popupInnerRectWidth * 0.2f, popupInnerRectHeight * 0.06f), "Learn more at...", style);

			GUI.color = new Color(1f, 1f, 1f, 0.9f * percentVisible);

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
			
			GUI.color = new Color(1f, 1f, 1f, 1f * percentVisible);
			customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);


			if (popupPanelPage == 5) {

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

			if (levelManager.GetPumaHealth(pumaNum) >= 1f)
				GUI.color = new Color(0.1f, 1f, 0f, (brightFlag ? 0.7f : 0.55f) * guiOpacity);
			else if (levelManager.GetPumaHealth(pumaNum) <= 0f)
				GUI.color = new Color(0.4f, 0.4f, 0.4f, (brightFlag ? 0.5f : 0.4f) * guiOpacity);

			DrawRect(new Rect(refX + refWidth * 0.54f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.018f - overlayRect.height * 0.002f, refWidth * 0.34f, overlayRect.height * 0.0048f), new Color(0.42f, 0.404f, 0.533f, 1f));	
			DrawRect(new Rect(refX + refWidth * 0.54f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.040f - overlayRect.height * 0.002f, refWidth * 0.34f, overlayRect.height * 0.0048f), new Color(0.5f, 0.5f, 0.5f, 1f));	
			DrawRect(new Rect(refX + refWidth * 0.54f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.062f - overlayRect.height * 0.002f, refWidth * 0.34f, overlayRect.height * 0.0048f), new Color(0.5f, 0.5f, 0.5f, 1f));	
			DrawRect(new Rect(refX + refWidth * 0.54f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.084f - overlayRect.height * 0.002f, refWidth * 0.34f, overlayRect.height * 0.0048f), new Color(0.5f, 0.5f, 0.5f, 1f));	

			if (levelManager.GetPumaHealth(pumaNum) > 0f && levelManager.GetPumaHealth(pumaNum) < 1f) {
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
		if (pumaNum < 6 && levelManager.GetPumaHealth(pumaNum) <= 0f)
			GUI.color = new Color(0.6f, 0.1f, 0.1f, 0.4f * guiOpacity);
		else if (pumaNum < 6 && levelManager.GetPumaHealth(pumaNum) >= 1f)
			//GUI.color = new Color(0.1f, 0.75f, 0.1f, 0.4f * guiOpacity);
			GUI.color = new Color(0.1f, 0.50f, 0f, 0.5f * guiOpacity);

		Color upperColor = new Color(0f, 0.75f, 0f, 1f);
		Color upperMiddleColor = new Color(0.5f * 1.04f, 0.7f * 1.04f, 0f, 1f);
		Color middleColor = new Color(0.85f * 0.99f, 0.85f * 0.99f, 0f, 1f);
		Color lowerMiddleColor = new Color(0.99f, 0.40f, 0f, 1f);
		Color lowerColor = new Color(0.86f, 0f, 0f, 1f);
		
		float buckCalories = levelManager.GetBuckCalories(pumaNum);
		float doeCalories  = levelManager.GetDoeCalories(pumaNum);
		float fawnCalories = levelManager.GetFawnCalories(pumaNum);
		
		if (buckCalories > 0f) {
			float buckExpenditures = levelManager.GetBuckExpenditures(pumaNum);		
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
			float doeExpenditures = levelManager.GetDoeExpenditures(pumaNum);		
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
			float fawnExpenditures = levelManager.GetFawnExpenditures(pumaNum);		
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
		if (levelManager.GetPumaHealth(pumaNum) <= 0f)
			return false;
			
		if (levelManager.GetPumaHealth(pumaNum) >= 1f)
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
