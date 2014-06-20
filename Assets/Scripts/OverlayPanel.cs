using UnityEngine;
using System.Collections;

/// OverlayPanel
/// Draw the GUI panel that provides the main user interface

public class OverlayPanel : MonoBehaviour
{
	//===================================
	//===================================
	//		MODULE VARIABLES
	//===================================
	//===================================

	private Rect overlayRect;
	private int currentScreen;
	private int difficultyLevel;
	private int soundEnable;
	private float soundVolume;
	private float pawRightFlag;
	
	// button styling
	private GUIStyle buttonStyle;	
	private GUIStyle buttonDownStyle;	
	private GUIStyle buttonDisabledStyle;	
	private GUIStyle bigButtonStyle;	
	private GUIStyle bigButtonDisabledStyle;
	private GUIStyle swapButtonStyle;

	// slider styling
	private GUISkin customSkin;
	private GUIStyle sliderBarStyle;
	private GUIStyle sliderThumbStyle;	

	// textures based on bitmap files
	private Texture2D logoTexture;
	private Texture2D backgroundTexture;
	private Texture2D pumaIconTexture;
	private Texture2D pumaIconShadowTexture;
	private Texture2D greenCheckTexture;	
	private Texture2D radioButtonTexture;
	private Texture2D radioSelectTexture;
	private Texture2D arrowTrayTexture;
	private Texture2D arrowTrayTopTexture;
	private Texture2D swapButtonTexture;
	private Texture2D swapButtonHoverTexture;
	private Texture2D sliderBarTexture;
	private Texture2D sliderThumbTexture;
	private Texture2D buckHeadTexture;
	private Texture2D doeHeadTexture;
	private Texture2D fawnHeadTexture;
	private Texture2D headshot1Texture;
	private Texture2D headshot2Texture;
	private Texture2D headshot3Texture;
	private Texture2D headshot4Texture;
	private Texture2D headshot5Texture;
	private Texture2D headshot6Texture;
	private Texture2D closeup1Texture;
	private Texture2D closeup2Texture;
	private Texture2D closeup3Texture;
	private Texture2D closeup4Texture;
	private Texture2D closeup5Texture;
	private Texture2D closeup6Texture;
	private Texture2D closeupBackgroundTexture;

	// external modules
	private GuiManager guiManager;
	private GuiComponents guiComponents;
	private GuiUtils guiUtils;
	private LevelManager levelManager;
	//private InputControls inputControls;
	private ScoringSystem scoringSystem;
	

	//===================================
	//===================================
	//		INITIALIZATION
	//===================================
	//===================================

	void Start() 
	{	
		// connect to external modules
		guiManager = GetComponent<GuiManager>();
		guiComponents = GetComponent<GuiComponents>();
		guiUtils = GetComponent<GuiUtils>();
		levelManager = GetComponent<LevelManager>();
		//inputControls = GetComponent<InputControls>();
		scoringSystem = GetComponent<ScoringSystem>();
		
		// texture references from GuiManager
		logoTexture = guiManager.logoTexture;
		backgroundTexture = guiManager.backgroundTexture;
		pumaIconTexture = guiManager.pumaIconTexture;
		pumaIconShadowTexture = guiManager.pumaIconShadowTexture;
		greenCheckTexture = guiManager.greenCheckTexture;
		radioButtonTexture = guiManager.radioButtonTexture;
		radioSelectTexture = guiManager.radioSelectTexture;
		arrowTrayTexture = guiManager.arrowTrayTexture;
		arrowTrayTopTexture = guiManager.arrowTrayTopTexture;
		swapButtonTexture = guiManager.swapButtonTexture;
		swapButtonHoverTexture = guiManager.swapButtonHoverTexture;
		sliderBarTexture = guiManager.sliderBarTexture;
		sliderThumbTexture = guiManager.sliderThumbTexture;
		buckHeadTexture = guiManager.buckHeadTexture;
		doeHeadTexture = guiManager.doeHeadTexture;
		fawnHeadTexture = guiManager.fawnHeadTexture;
		headshot1Texture = guiManager.headshot1Texture;
		headshot2Texture = guiManager.headshot2Texture;
		headshot3Texture = guiManager.headshot3Texture;
		headshot4Texture = guiManager.headshot4Texture;
		headshot5Texture = guiManager.headshot5Texture;
		headshot6Texture = guiManager.headshot6Texture;
		closeup1Texture = guiManager.closeup1Texture;
		closeup2Texture = guiManager.closeup2Texture;
		closeup3Texture = guiManager.closeup3Texture;
		closeup4Texture = guiManager.closeup4Texture;
		closeup5Texture = guiManager.closeup5Texture;
		closeup6Texture = guiManager.closeup6Texture;
		closeupBackgroundTexture = guiManager.closeupBackgroundTexture;
		
		// custom button styling
		buttonStyle = new GUIStyle();
		buttonStyle.normal.textColor = new Color(0.99f, 0.7f, 0.2f, 1f);
		buttonStyle.hover.textColor = new Color(0.99f, 0.8f, 0.4f, 1f);
		buttonStyle.alignment = TextAnchor.MiddleCenter;
		buttonDownStyle = new GUIStyle();
		buttonDownStyle.normal.textColor = new Color(0.99f, 0.88f, 0.6f, 1f);
		buttonDownStyle.hover.textColor = new Color(0.99f, 0.88f, 0.6f, 1f);
		buttonDownStyle.alignment = TextAnchor.MiddleCenter;
		buttonDisabledStyle = new GUIStyle();
		buttonDisabledStyle.normal.textColor = new Color(0.3f, 0.3f, 0.3f, 1f);
		buttonDisabledStyle.hover.textColor = new Color(0.3f, 0.3f, 0.3f, 1f);
		buttonDisabledStyle.alignment = TextAnchor.MiddleCenter;
		bigButtonStyle = new GUIStyle();
		bigButtonStyle.normal.textColor = new Color(0.99f, 0.7f, 0.2f, 1f);
		bigButtonStyle.hover.textColor = new Color(0.99f, 0.8f, 0.4f, 1f);
		bigButtonStyle.alignment = TextAnchor.MiddleCenter;
		bigButtonDisabledStyle = new GUIStyle();
		bigButtonDisabledStyle.normal.textColor = new Color(0.4f, 0.4f, 0.4f, 1f);
		bigButtonDisabledStyle.hover.textColor = new Color(0.4f, 0.4f, 0.4f, 1f);
		bigButtonDisabledStyle.alignment = TextAnchor.MiddleCenter;
		swapButtonStyle = new GUIStyle();
		swapButtonStyle.normal.textColor = Color.white;
		swapButtonStyle.normal.background = swapButtonTexture;
		swapButtonStyle.hover.textColor = Color.white;
		swapButtonStyle.hover.background = swapButtonHoverTexture;
		swapButtonStyle.alignment = TextAnchor.MiddleCenter;

		// custom slider styling
		sliderBarStyle = new GUIStyle();
		sliderThumbStyle = new GUIStyle();
		sliderThumbStyle.normal.background = sliderThumbTexture;
		sliderThumbStyle.padding = new RectOffset(10,10,10,10);
		customSkin = (GUISkin)ScriptableObject.CreateInstance("GUISkin");
		customSkin.horizontalSlider = sliderBarStyle;
		customSkin.horizontalSliderThumb = sliderThumbStyle;	

		// additional initialization
		currentScreen = 0;
		difficultyLevel = 1;
		soundEnable = 1;
		soundVolume = 0.5f;
		pawRightFlag = 1;
	}
	
	//===================================
	//===================================
	//	 PUBLIC ACCESS TO MODULE VARS
	//===================================
	//===================================
	
	public int GetCurrentScreen()
	{
		return currentScreen;
	}

	public void SetCurrentScreen(int newVal)
	{
		currentScreen = newVal;
	}

	//===================================
	//===================================
	//	  DRAW THE OVERLAY PANEL
	//===================================
	//===================================
	
	public void Draw(float overlayPanelOpacity) 
	{ 
		overlayRect = guiManager.GetOverlayRect();
	
		// background panel
		GUI.color = new Color(0f, 0f, 0f, 1f * overlayPanelOpacity);
		GUI.Box(new Rect(overlayRect.x, overlayRect.y, overlayRect.width, overlayRect.height), "");
		//GUI.color = new Color(0f, 0f, 0f, 0.3f * overlayPanelOpacity);
		//GUI.Box(new Rect(overlayRect.x, overlayRect.y, overlayRect.width, overlayRect.height), "");
		
		//background image
		GUI.color = new Color(1f, 1f, 1f, 0.75f * overlayPanelOpacity);
		GUI.DrawTexture(new Rect(overlayRect.x + 4, overlayRect.y + 4, overlayRect.width-8, overlayRect.height-8), backgroundTexture);
		GUI.color = new Color(1f, 1f, 1f, 1f * overlayPanelOpacity);
			
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		
		// MAIN TTTLE
		
		float upperItemsYShift = overlayRect.height * 0.01f;

		if (true) {
			// graphical logo
			float logoX = overlayRect.x + overlayRect.width * 0.36f;
			float logoY = overlayRect.y - overlayRect.height * 0.02f + upperItemsYShift;
			float logoWidth = overlayRect.width * 0.28f;
			float logoHeight = logoTexture.height * (logoWidth / logoTexture.width);
			//GUI.color = new Color(1f, 1f, 1f, 0.75f * overlayPanelOpacity);
			GUI.DrawTexture(new Rect(logoX, logoY, logoWidth, logoHeight), logoTexture);
			//GUI.color = new Color(1f, 1f, 1f, 1f * overlayPanelOpacity);
		}
		else {
			// text-based logo -- for reference only		
			GUI.color = new Color(1f, 1f, 1f, 0.93f * overlayPanelOpacity);
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
			GUI.color = new Color(1f, 1f, 1f, 1f * overlayPanelOpacity);
		}

		// LEVEL and STATUS DISPLAYS

		float levelPanelX = overlayRect.x + overlayRect.width * 0.04f;
		float levelPanelY = overlayRect.y + overlayRect.width * 0.016f + upperItemsYShift;
		float levelPanelWidth = overlayRect.width * 0.30f;
		float levelPanelHeight = overlayRect.width * 0.076f;
		guiComponents.DrawLevelPanel(overlayPanelOpacity, levelPanelX, levelPanelY, levelPanelWidth, levelPanelHeight, false);
					
		float statusPanelX = overlayRect.x + overlayRect.width * 0.66f;
		float statusPanelY = overlayRect.y + overlayRect.width * 0.016f + upperItemsYShift;
		float statusPanelWidth = overlayRect.width * 0.30f;
		float statusPanelHeight = overlayRect.width * 0.076f;
		guiComponents.DrawStatusPanel(overlayPanelOpacity, statusPanelX, statusPanelY, statusPanelWidth, statusPanelHeight, false);

		
		
		//=====================================
		// ADD BUTTONS
		//=====================================
		
		// background rectangle
		GUI.color = new Color(0f, 0f, 0f, 1f * overlayPanelOpacity);
		GUI.Box(new Rect(overlayRect.x + overlayRect.width * 0f, overlayRect.y + overlayRect.height * 0.926f, overlayRect.width * 1f, overlayRect.height * 0.074f), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * overlayPanelOpacity);

		
		// 'help' button
		// background rectangle
		float helpButtonX = overlayRect.x + overlayRect.width * 0.59f;
		float helpButtonY = overlayRect.y + overlayRect.height * 0.937f;
		float helpButtonWidth = overlayRect.width * 0.1f;
		float helpButtonHeight = overlayRect.height * 0.05f;
		GUI.color = new Color(1f, 1f, 1f, 1f * overlayPanelOpacity);
		GUI.skin = guiManager.customGUISkin;
		guiManager.customGUISkin.button.fontSize = (int)(overlayRect.width * 0.02);
		guiManager.customGUISkin.button.fontStyle = FontStyle.Normal;
		GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
		GUI.color = new Color(1f, 1f, 1f, 0.5f * overlayPanelOpacity);
		if (GUI.Button(new Rect(helpButtonX, helpButtonY, helpButtonWidth, helpButtonHeight), "")) {
			guiManager.OpenInfoPanel(-1); // opens to current page
		}
		GUI.color = new Color(1f, 1f, 1f, 0.9f * overlayPanelOpacity);
		if (GUI.Button(new Rect(helpButtonX, helpButtonY, helpButtonWidth, helpButtonHeight), "Info...")) {
			guiManager.OpenInfoPanel(-1); // opens to current page
		}
		GUI.color = new Color(1f, 1f, 1f, 1f * overlayPanelOpacity);

		
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
		guiUtils.DrawRect(new Rect(buttonMargin + buttonWidth*buttonIndex + buttonGap*buttonIndex + buttonWidth*0.05f - backgroundRectWidthAdjust*0.5f, buttonY + buttonheight * 0.15f, buttonWidth - buttonWidth*0.1f + backgroundRectWidthAdjust, buttonheight - buttonheight * 0.29f), new Color(0f, 0f, 0f, 0.5f));	


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
		GUI.color = (guiManager.selectedPuma != -1) ? new Color(1f, 1f, 1f, 1f * overlayPanelOpacity) : new Color(1f, 1f, 1f, 0.5f * overlayPanelOpacity);
		bigButtonStyle.fontSize = (int)(overlayRect.width * 0.032);;
		bigButtonDisabledStyle.fontSize = (int)(overlayRect.width * 0.03);;
		GUI.skin = guiManager.customGUISkin;
		guiManager.customGUISkin.button.fontSize = (int)(overlayRect.width * 0.028);
		guiManager.customGUISkin.button.fontStyle = FontStyle.Normal;
		GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
		if (guiManager.selectedPuma != -1) {
			if (GUI.Button(new Rect(startButtonX, startButtonY, startButtonWidth, startButtonHeight), "")) {
				guiManager.SetGuiState("guiStateLeavingOverlay");
				levelManager.SetGameState("gameStateLeavingGui");
			}
			if (GUI.Button(new Rect(startButtonX, startButtonY, startButtonWidth, startButtonHeight), "Start")) {
			//if (GUI.Button(new Rect(startButtonX, startButtonY, startButtonWidth, startButtonHeight), "Start", (guiManager.selectedPuma != -1) ? bigButtonStyle : bigButtonDisabledStyle) && (guiManager.selectedPuma != -1)) {
				guiManager.SetGuiState("guiStateLeavingOverlay");
				levelManager.SetGameState("gameStateLeavingGui");
			}
		}
		else {
			GUI.Button(new Rect(startButtonX, startButtonY, startButtonWidth, startButtonHeight), "Start", bigButtonDisabledStyle);
		}
		
		GUI.color = new Color(1f, 1f, 1f, 1f * overlayPanelOpacity);
		
		
		// add selected screen
		switch (currentScreen) {
		case 0:
			DrawSelectScreen(overlayPanelOpacity);
			break;		
		case 1:
			DrawOptionsScreen(overlayPanelOpacity);
			break;
		case 2:
			DrawStatsScreen(overlayPanelOpacity);
			break;
		case 3:
			DrawQuitScreen(overlayPanelOpacity);
			break;
		}
		
	}


	

	void DrawSelectScreen(float selectScreenOpacity) 
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

		GUI.color = new Color(0f, 0f, 0f, 1f * selectScreenOpacity);
		GUI.Box(new Rect(backRectX - overlayRect.width * 0.02f, backRectY - overlayRect.height * 0.025f, backRectWidth + overlayRect.width * 0.04f, backRectHeight + overlayRect.height * 0.05f), "");
		GUI.color = new Color(0f, 0f, 0f, 0.5f * selectScreenOpacity);
		GUI.Box(new Rect(backRectX, backRectY, backRectWidth, backRectHeight), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);


		// select screen prompt

		if (guiManager.selectedPuma == -1) {
		
			float yOffset = overlayRect.height * -0.03f;
		
			//guiUtils.DrawRect(new Rect(overlayRect.width * 0.32f, overlayRect.height * 0.32f, overlayRect.width * 0.36f, overlayRect.height * 0.12f), new Color(1f, 1f, 1f, 0.4f));	
			//guiUtils.DrawRect(new Rect(overlayRect.width * 0.30f, overlayRect.height * 0.26f, overlayRect.width * 0.40f, overlayRect.height * 0.16f), new Color(1f, 1f, 1f, 0.6f));	
			GUI.color = new Color(0f, 0f, 0f, 1f * selectScreenOpacity);
			GUI.Box(new Rect(overlayRect.x + overlayRect.width * 0.37f, yOffset + overlayRect.y + overlayRect.height * 0.195f, overlayRect.width * 0.26f, overlayRect.height * 0.064f), "");
			//GUI.color = new Color(0f, 0f, 0f, 0.5f * selectScreenOpacity);
			//GUI.Box(new Rect(overlayRect.width * 0.32f, overlayRect.height * 0.31f, overlayRect.width * 0.36f, overlayRect.height * 0.16f), "");

			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
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
		float actualselectScreenOpacity = selectScreenOpacity;
		selectScreenOpacity = selectScreenOpacity * 0.9f;
		GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);		
		
		float healthBarX = overlayRect.x + overlayRect.width * 0.04f;
		float healthBarY = overlayRect.y + overlayRect.height * 0.844f + yOffsetForAddingPopulationBar;
		float healthBarWidth = overlayRect.width * 0.92f;
		float healthBarHeight = overlayRect.height * 0.048f;	
		float healthBarLabelWidth = healthBarWidth * 0.13f;

		GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
		GUI.Box(new Rect(healthBarX, healthBarY, healthBarLabelWidth * 0.985f, healthBarHeight), "");
		GUI.Box(new Rect(healthBarX, healthBarY, healthBarLabelWidth * 0.985f, healthBarHeight), "");
		GUI.Box(new Rect(healthBarX + healthBarWidth - healthBarLabelWidth * 0.985f, healthBarY, healthBarLabelWidth * 0.985f, healthBarHeight), "");
		GUI.Box(new Rect(healthBarX + healthBarWidth - healthBarLabelWidth * 0.985f, healthBarY, healthBarLabelWidth * 0.985f, healthBarHeight), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);

		guiComponents.DrawPopulationHealthBar(selectScreenOpacity, healthBarX + healthBarLabelWidth, healthBarY, healthBarWidth - healthBarLabelWidth * 2f, healthBarHeight, false, false);

		selectScreenOpacity = actualselectScreenOpacity;
		GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);		
		
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

		textUpShift = 0f;
		barsDownShift = 0f;
		iconDownShift = 0f;
		headshotBackgroundHeightAddition = 0f;


		//Color pumaFullHealthColor = new Color(0.84f, 0.99f, 0.0f, 0.72f * selectScreenOpacity);
		//Color pumaDeadColor = new Color(0.76f, 0.0f, 0f, 0.47f * selectScreenOpacity);
		//Color pumaDeadColor = new Color(0.1f, 0.1f, 0.1f, 0.6f * selectScreenOpacity);



		//Color fullHealthPumaHeadshotColor = new Color(0.2f, 1f, 0.05f, 1f * selectScreenOpacity);
		//Color fullHealthPumaIconColor = new Color(0.1f, 1f, 0f, 0.65f * selectScreenOpacity);
		Color fullHealthPumaHeadshotColor = new Color(0.99f, 0.92f, 0f, 0.62f * selectScreenOpacity);
		Color fullHealthPumaIconColor = new Color(0.99f, 0.92f, 0f, 0.27f * selectScreenOpacity);
		//Color fullHealthPumaTextColor = new Color(0.1f, 0.5f, 0f, 0.8f * selectScreenOpacity);
		Color fullHealthPumaTextColor = new Color(0.32f, 0.32f, 0.22f, 0.8f * selectScreenOpacity);
		Color fullHealthPumaAnnounceColor = new Color(0.01f, 0.85f, 0f, 0.8f * selectScreenOpacity);

		//Color deadPumaHeadshotColor = new Color(0.8f, 0.1f, 0f, 0.55f * selectScreenOpacity);
		Color deadPumaHeadshotColor = new Color(0.12f, 0.12f, 0.12f, 0.7f * selectScreenOpacity);
		//Color deadPumaIconColor = new Color(0.8f, 0f, 0f, 0.3f * selectScreenOpacity);
		Color deadPumaIconColor = new Color(0.08f, 0.08f, 0.08f, 0.99f * selectScreenOpacity);
		//Color deadPumaTextColor = new Color(0.05f, 0.05f, 0.02f, 1f * selectScreenOpacity);
		Color deadPumaTextColor = new Color(0.32f, 0.32f, 0.22f, 0.8f * selectScreenOpacity);
		Color deadPumaAnnounceColor = new Color(0.65f, 0.05f, 0f, 1f * selectScreenOpacity);

		float endingLabelDownshift = overlayRect.height * 0.036f;
		
		// young male
		textureX = overlayRect.x + overlayRect.width * ((guiManager.selectedPuma == -1) ? 0.102f : 0.089f);
		textureX += overlayRect.width * ((guiManager.selectedPuma == 0) ? -0.002f : 0f);
		textureY = overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 0) ? 0.572f : 0.585f) + yOffsetForAddingPopulationBar;
		textureWidth = overlayRect.width * ((guiManager.selectedPuma == 0) ? 0.16f : 0.115f);
		textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
		if (guiManager.selectedPuma != 0) {
			// background panel and puma head
			headshotY = overlayRect.y + overlayRect.height * 0.355f + yOffsetForAddingPopulationBar + headUpShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f + headshotBackgroundHeightAddition), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
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
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			// health bar
			if (scoringSystem.GetPumaHealth(0) > 0f && scoringSystem.GetPumaHealth(0) < 1f)
				guiComponents.DrawPumaHealthBar(0, selectScreenOpacity, textureX, headshotY - headshotHeight * 0.36f + healthDownShift, textureWidth, headshotHeight * 0.26f, true);
			// background panel for puma middle
			headshotY = overlayRect.y + overlayRect.height * 0.47f + yOffsetForAddingPopulationBar + barsDownShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			// display bars in puma middle
			DrawDisplayBars(0, selectScreenOpacity, headshotX, headshotY, headshotWidth, headshotHeight);
			// final ending label at very top
			if (scoringSystem.GetPumaHealth(0) <= 0f) {
				// puma is dead
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
				style.normal.textColor = deadPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), "STARVED", style);
			}
			else if (scoringSystem.GetPumaHealth(0) >= 1f) {
				// puma at full health
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
				style.normal.textColor = fullHealthPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), "Full Health", style);
				float checkMarkWidth = overlayRect.width * 0.021f;
				float checkMarkHeight = greenCheckTexture.height * (checkMarkWidth / greenCheckTexture.width);
				float checkMarkX = textureX + textureWidth * 0.84f;
				float checkMarkY = overlayRect.y + overlayRect.height * 0.282f + yOffsetForAddingPopulationBar + textUpShift;
				GUI.color = new Color(1f, 1f, 1f, 0.8f * selectScreenOpacity);
				GUI.DrawTexture(new Rect(checkMarkX, checkMarkY + endingLabelDownshift, checkMarkWidth, checkMarkHeight), greenCheckTexture);
				GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			}
		}
		// background panel for text or puma icon
		if (guiManager.selectedPuma == 0) {
			GUI.color = new Color(0f, 0f, 0f, 0.4f * selectScreenOpacity);
			//GUI.Box(new Rect(textureX + overlayRect.width * .015f, overlayRect.y + overlayRect.height * 0.73f + yOffsetForAddingPopulationBar, textureWidth - overlayRect.width * .03f, overlayRect.height * 0.065f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		else {
			// not selected
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX + overlayRect.width * .0f, overlayRect.y + overlayRect.height * 0.585f + yOffsetForAddingPopulationBar + iconDownShift, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.14f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		// puma icon
		GUI.color = new Color(1f, 1f, 1f, 0f * selectScreenOpacity);
		if (GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.315f, textureWidth, overlayRect.height * 0.47f), "") && PumaIsSelectable(0)) {
			guiManager.selectedPuma = 0;
			levelManager.SetSelectedPuma(0);
		}
		GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		if (guiManager.selectedPuma == 0) {
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
				GUI.color = new Color(1f, 1f, 1f, 0.7f * selectScreenOpacity);
			GUI.DrawTexture(new Rect(textureX + textureWidth * 0.05f, textureY + textureHeight * 0.12f + iconDownShift, textureWidth * 0.9f, textureHeight * 0.9f), pumaIconTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		if (guiManager.selectedPuma == 0)
			DrawSelectScreenDetailsPanel(selectScreenOpacity, style, textureX, textureWidth, headshot1Texture);
		// text
		if (guiManager.selectedPuma == 0)
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
		else if (scoringSystem.GetPumaHealth(0) <= 0f)
			style.normal.textColor = deadPumaTextColor;
		else if (scoringSystem.GetPumaHealth(0) >= 1f)
			style.normal.textColor = fullHealthPumaTextColor;
		else
			style.normal.textColor = unselectedTextColor;
		style.fontSize = (int)(overlayRect.width * ((guiManager.selectedPuma == 0) ? 0.021f : 0.019f));
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 0) ? 0.71f : 0.71f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "Eric", style);
		style.fontSize = (int)(overlayRect.width * ((guiManager.selectedPuma == 0) ? 0.015f : 0.014f));
		//style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 0) ? 0.735f : 0.735f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "2 years - male", style);
		style.normal.textColor = Color.white;
		


		// young female
		textureX += overlayRect.width * ((guiManager.selectedPuma == 0) ? 0.18f : 0.135f);
		textureX += overlayRect.width * ((guiManager.selectedPuma == 1) ? -0.002f : 0f);
		textureY = overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 1) ? 0.572f : 0.585f) + yOffsetForAddingPopulationBar;
		textureWidth = overlayRect.width * ((guiManager.selectedPuma == 1) ? 0.16f : 0.115f);
		textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
		if (guiManager.selectedPuma != 1) {
			// background panel for puma head
			headshotY = overlayRect.y + overlayRect.height * 0.355f + yOffsetForAddingPopulationBar + headUpShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f + headshotBackgroundHeightAddition), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
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
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			// health bar
			if (scoringSystem.GetPumaHealth(1) > 0f && scoringSystem.GetPumaHealth(1) < 1f)
				guiComponents.DrawPumaHealthBar(1, selectScreenOpacity, textureX, headshotY - headshotHeight * 0.36f + healthDownShift, textureWidth, headshotHeight * 0.26f, true);
			// background panel for puma middle
			headshotY = overlayRect.y + overlayRect.height * 0.47f + yOffsetForAddingPopulationBar + barsDownShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			// display bars in puma middle
			DrawDisplayBars(1, selectScreenOpacity, headshotX, headshotY, headshotWidth, headshotHeight);
			// final ending label at very top
			if (scoringSystem.GetPumaHealth(1) <= 0f) {
				// puma is dead
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
				style.normal.textColor = deadPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), "STARVED !", style);
			}
			else if (scoringSystem.GetPumaHealth(1) >= 1f) {
				// puma at full health
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
				style.normal.textColor = fullHealthPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), "Full Health", style);
				float checkMarkWidth = overlayRect.width * 0.021f;
				float checkMarkHeight = greenCheckTexture.height * (checkMarkWidth / greenCheckTexture.width);
				float checkMarkX = textureX + textureWidth * 0.84f;
				float checkMarkY = overlayRect.y + overlayRect.height * 0.282f + yOffsetForAddingPopulationBar + textUpShift;
				GUI.color = new Color(1f, 1f, 1f, 0.8f * selectScreenOpacity);
				GUI.DrawTexture(new Rect(checkMarkX, checkMarkY + endingLabelDownshift, checkMarkWidth, checkMarkHeight), greenCheckTexture);
				GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			}
		}
		// background panel for text or puma icon
		if (guiManager.selectedPuma == 1) {
			GUI.color = new Color(0f, 0f, 0f, 0.4f * selectScreenOpacity);
			//GUI.Box(new Rect(textureX + overlayRect.width * .015f, overlayRect.y + overlayRect.height * 0.73f + yOffsetForAddingPopulationBar, textureWidth - overlayRect.width * .03f, overlayRect.height * 0.065f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		else {
			// not selected
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX + overlayRect.width * .0f, overlayRect.y + overlayRect.height * 0.585f + yOffsetForAddingPopulationBar + iconDownShift, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.14f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		// puma icon
		GUI.color = new Color(1f, 1f, 1f, 0f * selectScreenOpacity);
		if (GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.315f, textureWidth, overlayRect.height * 0.47f), "") && PumaIsSelectable(1)) {
			guiManager.selectedPuma = 1;
			levelManager.SetSelectedPuma(1);
		}
		GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		if (guiManager.selectedPuma == 1) {
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
				GUI.color = new Color(1f, 1f, 1f, 0.7f * selectScreenOpacity);
			GUI.DrawTexture(new Rect(textureX + textureWidth * 0.05f, textureY + textureHeight * 0.12f + iconDownShift, textureWidth * 0.9f, textureHeight * 0.9f), pumaIconTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		if (guiManager.selectedPuma == 1)
			DrawSelectScreenDetailsPanel(selectScreenOpacity, style, textureX, textureWidth, headshot2Texture);
		// text
		if (guiManager.selectedPuma == 1)
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
		else if (scoringSystem.GetPumaHealth(1) <= 0f)
			style.normal.textColor = deadPumaTextColor;
		else if (scoringSystem.GetPumaHealth(1) >= 1f)
			style.normal.textColor = fullHealthPumaTextColor;
		else
			style.normal.textColor = unselectedTextColor;
		style.fontSize = (int)(overlayRect.width * ((guiManager.selectedPuma == 1) ? 0.021f : 0.019f));
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 1) ? 0.71f : 0.71f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "Palo", style);
		style.fontSize = (int)(overlayRect.width * ((guiManager.selectedPuma == 1) ? 0.015f : 0.014f));
		//style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 1) ? 0.735f : 0.735f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "2 years - female", style);
		style.normal.textColor = Color.white;

		// adult male
		textureX += overlayRect.width * ((guiManager.selectedPuma == 1) ? 0.18f : 0.135f);
		textureX += overlayRect.width * ((guiManager.selectedPuma == 2) ? -0.002f : 0f);
		textureY = overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 2) ? 0.572f : 0.585f) + yOffsetForAddingPopulationBar;
		textureWidth = overlayRect.width * ((guiManager.selectedPuma == 2) ? 0.16f : 0.115f);
		textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
		if (guiManager.selectedPuma != 2) {
			// background panel for puma head
			headshotY = overlayRect.y + overlayRect.height * 0.355f + yOffsetForAddingPopulationBar + headUpShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f + headshotBackgroundHeightAddition), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
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
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			// health bar
			if (scoringSystem.GetPumaHealth(2) > 0f && scoringSystem.GetPumaHealth(2) < 1f)
				guiComponents.DrawPumaHealthBar(2, selectScreenOpacity, textureX, headshotY - headshotHeight * 0.36f + healthDownShift, textureWidth, headshotHeight * 0.26f, true);
			// background panel for puma middle
			headshotY = overlayRect.y + overlayRect.height * 0.47f + yOffsetForAddingPopulationBar + barsDownShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			// display bars in puma middle
			DrawDisplayBars(2, selectScreenOpacity, headshotX, headshotY, headshotWidth, headshotHeight);
			// final ending label at very top
			if (scoringSystem.GetPumaHealth(2) <= 0f) {
				// puma is dead
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
				style.normal.textColor = deadPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), "STARVED !", style);
			}
			else if (scoringSystem.GetPumaHealth(2) >= 1f) {
				// puma at full health
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
				style.normal.textColor = fullHealthPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), "Full Health", style);
				float checkMarkWidth = overlayRect.width * 0.021f;
				float checkMarkHeight = greenCheckTexture.height * (checkMarkWidth / greenCheckTexture.width);
				float checkMarkX = textureX + textureWidth * 0.84f;
				float checkMarkY = overlayRect.y + overlayRect.height * 0.282f + yOffsetForAddingPopulationBar + textUpShift;
				GUI.color = new Color(1f, 1f, 1f, 0.8f * selectScreenOpacity);
				GUI.DrawTexture(new Rect(checkMarkX, checkMarkY + endingLabelDownshift, checkMarkWidth, checkMarkHeight), greenCheckTexture);
				GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			}
		}
		// background panel for text or puma icon
		if (guiManager.selectedPuma == 2) {
			GUI.color = new Color(0f, 0f, 0f, 0.4f * selectScreenOpacity);
			//GUI.Box(new Rect(textureX + overlayRect.width * .015f, overlayRect.y + overlayRect.height * 0.73f + yOffsetForAddingPopulationBar, textureWidth - overlayRect.width * .03f, overlayRect.height * 0.065f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		else {
			// not selected
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX + overlayRect.width * .0f, overlayRect.y + overlayRect.height * 0.585f + yOffsetForAddingPopulationBar + iconDownShift, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.14f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		// puma icon
		GUI.color = new Color(1f, 1f, 1f, 0f * selectScreenOpacity);
		if (GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.315f, textureWidth, overlayRect.height * 0.47f), "") && PumaIsSelectable(2)) {
			guiManager.selectedPuma = 2;
			levelManager.SetSelectedPuma(2);
		}
		GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		if (guiManager.selectedPuma == 2) {
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
				GUI.color = new Color(1f, 1f, 1f, 0.7f * selectScreenOpacity);
			GUI.DrawTexture(new Rect(textureX + textureWidth * 0.05f, textureY + textureHeight * 0.12f + iconDownShift, textureWidth * 0.9f, textureHeight * 0.9f), pumaIconTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		if (guiManager.selectedPuma == 2)
			DrawSelectScreenDetailsPanel(selectScreenOpacity, style, textureX, textureWidth, headshot3Texture);
		// text
		if (guiManager.selectedPuma == 2)
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
		else if (scoringSystem.GetPumaHealth(2) <= 0f)
			style.normal.textColor = deadPumaTextColor;
		else if (scoringSystem.GetPumaHealth(2) >= 1f)
			style.normal.textColor = fullHealthPumaTextColor;
		else
			style.normal.textColor = unselectedTextColor;
		style.fontSize = (int)(overlayRect.width * ((guiManager.selectedPuma == 2) ? 0.021f : 0.019f));
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 2) ? 0.71f : 0.71f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "Mitch", style);
		style.fontSize = (int)(overlayRect.width * ((guiManager.selectedPuma == 2) ? 0.015f : 0.014f));
		//style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 2) ? 0.735f : 0.735f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "5 years - male", style);
		style.normal.textColor = Color.white;

		// adult female
		textureX += overlayRect.width * ((guiManager.selectedPuma == 2) ? 0.18f : 0.135f);
		textureX += overlayRect.width * ((guiManager.selectedPuma == 3) ? -0.002f : 0f);
		textureY = overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 3) ? 0.572f : 0.585f) + yOffsetForAddingPopulationBar;
		textureWidth = overlayRect.width * ((guiManager.selectedPuma == 3) ? 0.16f : 0.115f);
		textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
		if (guiManager.selectedPuma != 3) {
			// background panel for puma head
			headshotY = overlayRect.y + overlayRect.height * 0.355f + yOffsetForAddingPopulationBar + headUpShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f + headshotBackgroundHeightAddition), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
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
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			// health bar
			if (scoringSystem.GetPumaHealth(3) > 0f && scoringSystem.GetPumaHealth(3) < 1f)
				guiComponents.DrawPumaHealthBar(3, selectScreenOpacity, textureX, headshotY - headshotHeight * 0.36f + healthDownShift, textureWidth, headshotHeight * 0.26f, true);
			// background panel for puma middle
			headshotY = overlayRect.y + overlayRect.height * 0.47f + yOffsetForAddingPopulationBar + barsDownShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			// display bars in puma middle
			DrawDisplayBars(3, selectScreenOpacity, headshotX, headshotY, headshotWidth, headshotHeight);
			// final ending label at very top
			if (scoringSystem.GetPumaHealth(3) <= 0f) {
				// puma is dead
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
				style.normal.textColor = deadPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), "STARVED !", style);
			}
			else if (scoringSystem.GetPumaHealth(3) >= 1f) {
				// puma at full health
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
				style.normal.textColor = fullHealthPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), "Full Health", style);
				float checkMarkWidth = overlayRect.width * 0.021f;
				float checkMarkHeight = greenCheckTexture.height * (checkMarkWidth / greenCheckTexture.width);
				float checkMarkX = textureX + textureWidth * 0.84f;
				float checkMarkY = overlayRect.y + overlayRect.height * 0.282f + yOffsetForAddingPopulationBar + textUpShift;
				GUI.color = new Color(1f, 1f, 1f, 0.8f * selectScreenOpacity);
				GUI.DrawTexture(new Rect(checkMarkX, checkMarkY + endingLabelDownshift, checkMarkWidth, checkMarkHeight), greenCheckTexture);
				GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			}
		}
		// background panel for text or puma icon
		if (guiManager.selectedPuma == 3) {
			GUI.color = new Color(0f, 0f, 0f, 0.4f * selectScreenOpacity);
			//GUI.Box(new Rect(textureX + overlayRect.width * .015f, overlayRect.y + overlayRect.height * 0.73f + yOffsetForAddingPopulationBar, textureWidth - overlayRect.width * .03f, overlayRect.height * 0.065f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		else {
			// not selected
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX + overlayRect.width * .0f, overlayRect.y + overlayRect.height * 0.585f + yOffsetForAddingPopulationBar + iconDownShift, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.14f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		// puma icon
		GUI.color = new Color(1f, 1f, 1f, 0f * selectScreenOpacity);
		if (GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.315f, textureWidth, overlayRect.height * 0.47f), "") && PumaIsSelectable(3)) {
			guiManager.selectedPuma = 3;
			levelManager.SetSelectedPuma(3);
		}
		GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		if (guiManager.selectedPuma == 3) {
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
				GUI.color = new Color(1f, 1f, 1f, 0.7f * selectScreenOpacity);
			GUI.DrawTexture(new Rect(textureX + textureWidth * 0.05f, textureY + textureHeight * 0.12f + iconDownShift, textureWidth * 0.9f, textureHeight * 0.9f), pumaIconTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		if (guiManager.selectedPuma == 3)
			DrawSelectScreenDetailsPanel(selectScreenOpacity, style, textureX, textureWidth, headshot4Texture);
		// text
		if (guiManager.selectedPuma == 3)
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
		else if (scoringSystem.GetPumaHealth(3) <= 0f)
			style.normal.textColor = deadPumaTextColor;
		else if (scoringSystem.GetPumaHealth(3) >= 1f)
			style.normal.textColor = fullHealthPumaTextColor;
		else
			style.normal.textColor = unselectedTextColor;
		style.fontSize = (int)(overlayRect.width * ((guiManager.selectedPuma == 3) ? 0.021f : 0.019f));
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 3) ? 0.71f : 0.71f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "Trish", style);
		style.fontSize = (int)(overlayRect.width * ((guiManager.selectedPuma == 3) ? 0.015f : 0.014f));
		//style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 3) ? 0.735f : 0.735f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "5 years - female", style);
		style.normal.textColor = Color.white;

		// old male
		textureX += overlayRect.width * ((guiManager.selectedPuma == 3) ? 0.18f : 0.135f);
		textureX += overlayRect.width * ((guiManager.selectedPuma == 4) ? -0.002f : 0f);
		textureY = overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 4) ? 0.572f : 0.585f) + yOffsetForAddingPopulationBar;
		textureWidth = overlayRect.width * ((guiManager.selectedPuma == 4) ? 0.16f : 0.115f);
		textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
		if (guiManager.selectedPuma != 4) {
			// background panel for puma head
			headshotY = overlayRect.y + overlayRect.height * 0.355f + yOffsetForAddingPopulationBar + headUpShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f + headshotBackgroundHeightAddition), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
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
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			// health bar
			if (scoringSystem.GetPumaHealth(4) > 0f && scoringSystem.GetPumaHealth(4) < 1f)
				guiComponents.DrawPumaHealthBar(4, selectScreenOpacity, textureX, headshotY - headshotHeight * 0.36f + healthDownShift, textureWidth, headshotHeight * 0.26f, true);
			// background panel for puma middle
			headshotY = overlayRect.y + overlayRect.height * 0.47f + yOffsetForAddingPopulationBar + barsDownShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			// display bars in puma middle
			DrawDisplayBars(4, selectScreenOpacity, headshotX, headshotY, headshotWidth, headshotHeight);
			// final ending label at very top
			if (scoringSystem.GetPumaHealth(4) <= 0f) {
				// puma is dead
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
				style.normal.textColor = deadPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), "STARVED !", style);
			}
			else if (scoringSystem.GetPumaHealth(4) >= 1f) {
				// puma at full health
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
				style.normal.textColor = fullHealthPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), "Full Health", style);
				float checkMarkWidth = overlayRect.width * 0.021f;
				float checkMarkHeight = greenCheckTexture.height * (checkMarkWidth / greenCheckTexture.width);
				float checkMarkX = textureX + textureWidth * 0.84f;
				float checkMarkY = overlayRect.y + overlayRect.height * 0.282f + yOffsetForAddingPopulationBar + textUpShift;
				GUI.color = new Color(1f, 1f, 1f, 0.8f * selectScreenOpacity);
				GUI.DrawTexture(new Rect(checkMarkX, checkMarkY + endingLabelDownshift, checkMarkWidth, checkMarkHeight), greenCheckTexture);
				GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			}
		}
		// background panel for text or puma icon
		if (guiManager.selectedPuma == 4) {
			GUI.color = new Color(0f, 0f, 0f, 0.4f * selectScreenOpacity);
			//GUI.Box(new Rect(textureX + overlayRect.width * .015f, overlayRect.y + overlayRect.height * 0.73f + yOffsetForAddingPopulationBar, textureWidth - overlayRect.width * .03f, overlayRect.height * 0.065f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		else {
			// not selected
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX + overlayRect.width * .0f, overlayRect.y + overlayRect.height * 0.585f + yOffsetForAddingPopulationBar + iconDownShift, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.14f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		// puma icon
		GUI.color = new Color(1f, 1f, 1f, 0f * selectScreenOpacity);
		if (GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.315f, textureWidth, overlayRect.height * 0.47f), "") && PumaIsSelectable(4)) {
			guiManager.selectedPuma = 4;
			levelManager.SetSelectedPuma(4);
		}
		GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		if (guiManager.selectedPuma == 4) {
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
				GUI.color = new Color(1f, 1f, 1f, 0.7f * selectScreenOpacity);
			GUI.DrawTexture(new Rect(textureX + textureWidth * 0.05f, textureY + textureHeight * 0.12f + iconDownShift, textureWidth * 0.9f, textureHeight * 0.9f), pumaIconTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		if (guiManager.selectedPuma == 4)
			DrawSelectScreenDetailsPanel(selectScreenOpacity, style, textureX, textureWidth, headshot5Texture);
		// text
		if (guiManager.selectedPuma == 4)
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
		else if (scoringSystem.GetPumaHealth(4) <= 0f)
			style.normal.textColor = deadPumaTextColor;
		else if (scoringSystem.GetPumaHealth(4) >= 1f)
			style.normal.textColor = fullHealthPumaTextColor;
		else
			style.normal.textColor = unselectedTextColor;
		style.fontSize = (int)(overlayRect.width * ((guiManager.selectedPuma == 4) ? 0.021f : 0.019f));
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 4) ? 0.71f : 0.71f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "Liam", style);
		style.fontSize = (int)(overlayRect.width * ((guiManager.selectedPuma == 4) ? 0.015f : 0.014f));
		//style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 4) ? 0.735f : 0.735f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "8 years - male", style);
		style.normal.textColor = Color.white;

		// old female
		textureX += overlayRect.width * ((guiManager.selectedPuma == 4) ? 0.18f : 0.135f);
		textureX += overlayRect.width * ((guiManager.selectedPuma == 5) ? -0.002f : 0f);
		textureY = overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 5) ? 0.572f : 0.585f) + yOffsetForAddingPopulationBar;
		textureWidth = overlayRect.width * ((guiManager.selectedPuma == 5) ? 0.16f : 0.115f);
		textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
		if (guiManager.selectedPuma != 5) {
			// background panel for puma head
			headshotY = overlayRect.y + overlayRect.height * 0.355f + yOffsetForAddingPopulationBar + headUpShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f + headshotBackgroundHeightAddition), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
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
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			// health bar
			if (scoringSystem.GetPumaHealth(5) > 0f && scoringSystem.GetPumaHealth(5) < 1f)
				guiComponents.DrawPumaHealthBar(5, selectScreenOpacity, textureX, headshotY - headshotHeight * 0.36f + healthDownShift, textureWidth, headshotHeight * 0.26f, true);
			// background panel for puma middle
			headshotY = overlayRect.y + overlayRect.height * 0.47f + yOffsetForAddingPopulationBar + barsDownShift;
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.1f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			// display bars in puma middle
			DrawDisplayBars(5, selectScreenOpacity, headshotX, headshotY, headshotWidth, headshotHeight);
			// final ending label at very top
			if (scoringSystem.GetPumaHealth(5) <= 0f) {
				// puma is dead
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
				style.normal.textColor = deadPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), "STARVED !", style);
			}
			else if (scoringSystem.GetPumaHealth(5) >= 1f) {
				// puma at full health
				headshotY = overlayRect.y + overlayRect.height * 0.2885f + yOffsetForAddingPopulationBar + headUpShift;
				GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
				GUI.Box(new Rect(textureX - overlayRect.width * .0f, headshotY + endingLabelDownshift, textureWidth - overlayRect.width * .0f, headshotHeight * 0.3f), "");
				GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
				style.normal.textColor = fullHealthPumaAnnounceColor;
				style.fontSize = (int)(overlayRect.width * 0.012f);
				GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.26f + yOffsetForAddingPopulationBar + textUpShift + endingLabelDownshift, textureWidth, overlayRect.height * 0.08f), "Full Health", style);
				float checkMarkWidth = overlayRect.width * 0.021f;
				float checkMarkHeight = greenCheckTexture.height * (checkMarkWidth / greenCheckTexture.width);
				float checkMarkX = textureX + textureWidth * 0.84f;
				float checkMarkY = overlayRect.y + overlayRect.height * 0.282f + yOffsetForAddingPopulationBar + textUpShift;
				GUI.color = new Color(1f, 1f, 1f, 0.8f * selectScreenOpacity);
				GUI.DrawTexture(new Rect(checkMarkX, checkMarkY + endingLabelDownshift, checkMarkWidth, checkMarkHeight), greenCheckTexture);
				GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
			}
		}
		// background panel for text or puma icon
		if (guiManager.selectedPuma == 5) {
			GUI.color = new Color(0f, 0f, 0f, 0.4f * selectScreenOpacity);
			//GUI.Box(new Rect(textureX + overlayRect.width * .015f, overlayRect.y + overlayRect.height * 0.73f + yOffsetForAddingPopulationBar, textureWidth - overlayRect.width * .03f, overlayRect.height * 0.065f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		else {
			// not selected
			GUI.color = new Color(0f, 0f, 0f, 0.8f * selectScreenOpacity);
			GUI.Box(new Rect(textureX + overlayRect.width * .0f, overlayRect.y + overlayRect.height * 0.585f + yOffsetForAddingPopulationBar + iconDownShift, textureWidth - overlayRect.width * .0f, overlayRect.height * 0.14f), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		// puma icon
		GUI.color = new Color(1f, 1f, 1f, 0f * selectScreenOpacity);
		if (GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * 0.315f, textureWidth, overlayRect.height * 0.47f), "") && PumaIsSelectable(5)) {
			guiManager.selectedPuma = 5;
			levelManager.SetSelectedPuma(5);
		}
		GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		if (guiManager.selectedPuma == 5) {
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
				GUI.color = new Color(1f, 1f, 1f, 0.7f * selectScreenOpacity);
			GUI.DrawTexture(new Rect(textureX + textureWidth * 0.05f, textureY + textureHeight * 0.12f + iconDownShift, textureWidth * 0.9f, textureHeight * 0.9f), pumaIconTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenOpacity);
		}
		if (guiManager.selectedPuma == 5)
			DrawSelectScreenDetailsPanel(selectScreenOpacity, style, textureX, textureWidth, headshot6Texture);
		// text
		if (guiManager.selectedPuma == 5)
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
		else if (scoringSystem.GetPumaHealth(5) <= 0f)
			style.normal.textColor = deadPumaTextColor;
		else if (scoringSystem.GetPumaHealth(5) >= 1f)
			style.normal.textColor = fullHealthPumaTextColor;
		else
			style.normal.textColor = unselectedTextColor;
		style.fontSize = (int)(overlayRect.width * ((guiManager.selectedPuma == 5) ? 0.021f : 0.019f));
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 5) ? 0.71f : 0.71f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "Barb", style);
		style.fontSize = (int)(overlayRect.width * ((guiManager.selectedPuma == 5) ? 0.015f : 0.014f));
		//style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);
		GUI.Button(new Rect(textureX, overlayRect.y + overlayRect.height * ((guiManager.selectedPuma == 5) ? 0.735f : 0.735f) + yOffsetForAddingPopulationBar + textUpShift, textureWidth, overlayRect.height * 0.08f), "8 years - female", style);
		style.normal.textColor = Color.white;
		
	}	
	

	void DrawSelectScreenDetailsPanel(float selectScreenDetailsOpacity, GUIStyle style, float textureX, float textureWidth, Texture2D headshotTexture) 
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


		textUpShift = 0f;
		barsDownShift = 0f;
		headshotBackgroundHeightAddition = 0f;

		
		// background panel
		GUI.color = new Color(0f, 0f, 0f, 1f * selectScreenDetailsOpacity);
		GUI.Box(new Rect(detailsPanelX + detailsPanelWidth * 0.03f, detailsPanelY + (detailsPanelHeight * 0.113f) + headUpShift, detailsPanelWidth - detailsPanelWidth * 0.06f, detailsPanelHeight * 0.526f + headshotBackgroundHeightAddition), "");
		GUI.Box(new Rect(headshotX, detailsPanelY + detailsPanelHeight * 0.68f + barsDownShift, headshotWidth, detailsPanelHeight * 0.285f), "");
		if (headshotTexture != headshot1Texture && headshotTexture != headshot2Texture && headshotTexture != headshot3Texture) {
			GUI.color = new Color(0f, 0f, 0f, 1f * selectScreenDetailsOpacity);
			GUI.Box(new Rect(detailsPanelX + detailsPanelWidth * 0.03f, detailsPanelY + (detailsPanelHeight * 0.113f) + headUpShift, detailsPanelWidth - detailsPanelWidth * 0.06f, detailsPanelHeight * 0.526f + headshotBackgroundHeightAddition), "");
			GUI.Box(new Rect(headshotX, detailsPanelY + detailsPanelHeight * 0.68f + barsDownShift, headshotWidth, detailsPanelHeight * 0.285f), "");
		}
		//GUI.color = new Color(1f, 1f, 1f, 0.8f);
		//GUI.Box(new Rect(detailsPanelX, detailsPanelY, detailsPanelWidth, detailsPanelHeight * 0.85f), "");

		float origHeadshotYPos = headshotY + (headshotHeight * upperPanelShrinkFactor) - headshotHeight * 0.03f;
		float origHealthbarYPos = headshotY + headshotHeight + headshotHeight * 0.025f;
		
		// puma closeup headshot
		GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenDetailsOpacity);
		GUI.DrawTexture(new Rect(headshotX + (headshotWidth * upperPanelShrinkFactor * 0.5f), origHeadshotYPos + headshotHeight * 0.19f + headUpShift, headshotWidth * (1f - upperPanelShrinkFactor), headshotHeight * (1f - upperPanelShrinkFactor)), headshotTexture);

		// health bar
		GUI.color = new Color(0f, 0f, 0f, 1f * selectScreenDetailsOpacity);
		GUI.Box(new Rect(detailsPanelX + detailsPanelWidth * 0.03f, origHeadshotYPos - headshotHeight * 0.091f + healthDownShift, detailsPanelWidth - detailsPanelWidth * 0.06f, headshotHeight * 0.17f), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * selectScreenDetailsOpacity);
		guiComponents.DrawPumaHealthBar(guiManager.selectedPuma, selectScreenDetailsOpacity, detailsPanelX + detailsPanelWidth * 0.03f, origHeadshotYPos - headshotHeight * 0.091f + healthDownShift, detailsPanelWidth - detailsPanelWidth * 0.06f, headshotHeight * 0.17f);

		
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
		DrawDisplayBars(guiManager.selectedPuma, selectScreenDetailsOpacity, headshotX + displayBarsRightShift - overlayRect.height * -0.1265f, headshotY + overlayRect.height * 0.2295f + barsDownShift, headshotWidth, headshotWidth / 3f);

	}
	
	
	//======================================================================
	//======================================================================
	//======================================================================
	//								OPTIONS
	//======================================================================
	//======================================================================
	//======================================================================
	
	void DrawOptionsScreen(float optionsScreenOpacity) 
	{ 
		float optionsScreenX = overlayRect.x + overlayRect.width * 0.06f;
		float optionsScreenY = overlayRect.y + overlayRect.height * 0.205f;
		float optionsScreenWidth = overlayRect.width * 0.88f;
		float optionsScreenHeight = overlayRect.height * 0.578f;
		float fontScale = 0.8f;

		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;

		// background rectangle
		GUI.color = new Color(0f, 0f, 0f, 1f * optionsScreenOpacity);
		GUI.Box(new Rect(optionsScreenX - overlayRect.width * 0.02f, optionsScreenY - overlayRect.height * 0.025f, optionsScreenWidth + overlayRect.width * 0.04f, optionsScreenHeight + overlayRect.height * 0.05f), "");
		GUI.color = new Color(0f, 0f, 0f, 0.5f * optionsScreenOpacity);
		GUI.Box(new Rect(optionsScreenX, optionsScreenY, optionsScreenWidth, optionsScreenHeight), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * optionsScreenOpacity);


		// add population bar
		
		float yOffsetForAddingPopulationBar = overlayRect.height * -0.012f;
		float actualoptionsScreenOpacity = optionsScreenOpacity;
		optionsScreenOpacity = optionsScreenOpacity * 0.9f;
		GUI.color = new Color(1f, 1f, 1f, 1f * optionsScreenOpacity);		
		
		float healthBarX = overlayRect.x + overlayRect.width * 0.04f;
		float healthBarY = overlayRect.y + overlayRect.height * 0.844f + yOffsetForAddingPopulationBar;
		float healthBarWidth = overlayRect.width * 0.92f;
		float healthBarHeight = overlayRect.height * 0.048f;	
		float healthBarLabelWidth = healthBarWidth * 0.13f;

		GUI.color = new Color(0f, 0f, 0f, 0.8f * optionsScreenOpacity);
		GUI.Box(new Rect(healthBarX, healthBarY, healthBarLabelWidth * 0.985f, healthBarHeight), "");
		GUI.Box(new Rect(healthBarX, healthBarY, healthBarLabelWidth * 0.985f, healthBarHeight), "");
		GUI.Box(new Rect(healthBarX + healthBarWidth - healthBarLabelWidth * 0.985f, healthBarY, healthBarLabelWidth * 0.985f, healthBarHeight), "");
		GUI.Box(new Rect(healthBarX + healthBarWidth - healthBarLabelWidth * 0.985f, healthBarY, healthBarLabelWidth * 0.985f, healthBarHeight), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * optionsScreenOpacity);

		guiComponents.DrawPopulationHealthBar(optionsScreenOpacity, healthBarX + healthBarLabelWidth, healthBarY, healthBarWidth - healthBarLabelWidth * 2f, healthBarHeight, false, false);

		optionsScreenOpacity = actualoptionsScreenOpacity;
		GUI.color = new Color(1f, 1f, 1f, 1f * optionsScreenOpacity);		




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
		guiUtils.DrawRect(new Rect(optionsScreenX, optionsScreenY, optionsScreenWidth, optionsScreenHeight), new Color(1f, 1f, 1f, 0.665f));	

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
		guiUtils.DrawRect(new Rect(optionsScreenX, optionsScreenY + overlayRect.height * 0.113f, optionsScreenWidth, overlayRect.height * 0.005f), new Color(0f, 0f, 0f, 0.2f));	
		guiUtils.DrawRect(new Rect(optionsScreenX, optionsScreenY + overlayRect.height * 0.118f, optionsScreenWidth, overlayRect.height * 0.005f), new Color(1f, 1f, 1f, 0.5f));	
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
		guiUtils.DrawRect(new Rect(optionsScreenX, optionsScreenY + overlayRect.height * 0.226f, optionsScreenWidth, overlayRect.height * 0.005f), new Color(0f, 0f, 0f, 0.2f));	
		guiUtils.DrawRect(new Rect(optionsScreenX, optionsScreenY + overlayRect.height * 0.231f, optionsScreenWidth, overlayRect.height * 0.005f), new Color(1f, 1f, 1f, 0.5f));	
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
		GUI.skin = guiManager.customGUISkin;
		guiManager.customGUISkin.button.fontSize = (int)(overlayRect.width * 0.0165);
		guiManager.customGUISkin.button.fontStyle = FontStyle.Normal;
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
		guiUtils.DrawRect(new Rect(meterBoxX + meterBoxWidth * 0.05f,  meterBoxY + meterBoxHeight * 0.1f, meterBoxWidth * 0.9f, meterBoxHeight * 0.25f), new Color(0.61f, 0.64f, 0.66f, 1f));	
		guiUtils.DrawRect(new Rect(meterBoxX + meterBoxWidth * 0.07f,  meterBoxY + meterBoxHeight * 0.11f, (meterBoxWidth * 0.85f) * (health / 1.0f), meterBoxHeight * 0.23f), healthColor);			
		// 'pause' button
		GUI.color = new Color(1f, 1f, 1f, 0.75f);
		GUI.skin = guiManager.customGUISkin;
		guiManager.customGUISkin.button.fontSize = (int)(overlayRect.width * 0.009);;
		GUI.Button(new Rect(meterBoxX + meterBoxWidth * 0.05f,  meterBoxY + meterBoxHeight * 0.47f, meterBoxWidth * 0.9f, meterBoxHeight * 0.41f), "EXIT");
		GUI.color = new Color(1f, 1f, 1f, 1f);


		// ======================================	
		// DIVIDER
		guiUtils.DrawRect(new Rect(optionsScreenX, optionsScreenY + overlayRect.height * 0.339f, optionsScreenWidth, overlayRect.height * 0.005f), new Color(0f, 0f, 0f, 0.2f));	
		guiUtils.DrawRect(new Rect(optionsScreenX, optionsScreenY + overlayRect.height * 0.344f, optionsScreenWidth, overlayRect.height * 0.005f), new Color(1f, 1f, 1f, 0.5f));	
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
		//guiUtils.DrawRect(new Rect(optionsScreenX + overlayRect.width * 0.025f + overlayRect.width * 0.292f, titleY + overlayRect.height * 0.113f * 3f - overlayRect.height * 0.006f, overlayRect.width * 0.25f, overlayRect.height * 0.0585f), new Color(0f, 0f, 0f, 1f));	
		GUI.skin = guiManager.customGUISkin;
		guiManager.customGUISkin.button.fontSize = (int)(overlayRect.width * 0.0185);
		guiManager.customGUISkin.button.fontStyle = FontStyle.Normal;
		GUI.color = new Color(1f, 1f, 1f, 1f);
		GUI.backgroundColor = new Color(1f, 0f, 0f, 1f);
		if (GUI.Button(new Rect(overlayRect.width * -0.003f + optionsScreenX + overlayRect.width * 0.025f + overlayRect.width * 0.292f, overlayRect.width * -0.0025f + titleY + overlayRect.height * 0.113f * 3f - overlayRect.height * 0.006f, overlayRect.width * 0.006f + overlayRect.width * 0.25f, overlayRect.width * 0.005f + overlayRect.height * 0.0585f), "")) {
			guiManager.OpenInfoPanel(0);
		}
		GUI.backgroundColor = new Color(0.5f, 0.5f, 0.9f, 1f);
		if (GUI.Button(new Rect(optionsScreenX + overlayRect.width * 0.025f + overlayRect.width * 0.292f, titleY + overlayRect.height * 0.113f * 3f - overlayRect.height * 0.006f, overlayRect.width * 0.25f, overlayRect.height * 0.0585f), "")) {
			guiManager.OpenInfoPanel(0);
		}
		if (GUI.Button(new Rect(optionsScreenX + overlayRect.width * 0.025f + overlayRect.width * 0.292f, titleY + overlayRect.height * 0.113f * 3f - overlayRect.height * 0.006f, overlayRect.width * 0.25f, overlayRect.height * 0.0585f), "Play Introduction....")) {
			guiManager.OpenInfoPanel(0);
		}
		GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
		GUI.color = new Color(1f, 1f, 1f, 1f);

		
		// ======================================	
		// DIVIDER
		guiUtils.DrawRect(new Rect(optionsScreenX, optionsScreenY + overlayRect.height * 0.452f, optionsScreenWidth, overlayRect.height * 0.005f), new Color(0f, 0f, 0f, 0.2f));	
		guiUtils.DrawRect(new Rect(optionsScreenX, optionsScreenY + overlayRect.height * 0.457f, optionsScreenWidth, overlayRect.height * 0.005f), new Color(1f, 1f, 1f, 0.5f));	
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
		GUI.skin = guiManager.customGUISkin;
		guiManager.customGUISkin.button.fontSize = (int)(overlayRect.width * 0.0185);
		guiManager.customGUISkin.button.fontStyle = FontStyle.Normal;
		GUI.color = new Color(1f, 1f, 1f, 1f);
		GUI.backgroundColor = new Color(1f, 0f, 0f, 1f);
		if (GUI.Button(new Rect(overlayRect.width * -0.003f + optionsScreenX + overlayRect.width * 0.025f + overlayRect.width * 0.292f, overlayRect.width * -0.0025f + titleY + overlayRect.height * 0.113f * 4f - overlayRect.height * 0.006f, overlayRect.width * 0.006f + overlayRect.width * 0.25f, overlayRect.width * 0.005f + overlayRect.height * 0.0585f), "")) {
			guiManager.OpenInfoPanel(5);
		}
		GUI.backgroundColor = new Color(0.5f, 0.5f, 0.9f, 1f);
		if (GUI.Button(new Rect(optionsScreenX + overlayRect.width * 0.025f + overlayRect.width * 0.292f, titleY + overlayRect.height * 0.113f * 4f - overlayRect.height * 0.006f, overlayRect.width * 0.25f, overlayRect.height * 0.0585f), "")) {
			guiManager.OpenInfoPanel(5);
		}
		if (GUI.Button(new Rect(optionsScreenX + overlayRect.width * 0.025f + overlayRect.width * 0.292f, titleY + overlayRect.height * 0.113f * 4f - overlayRect.height * 0.006f, overlayRect.width * 0.25f, overlayRect.height * 0.0585f), "Help Protect Pumas....")) {
			guiManager.OpenInfoPanel(5);
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
	
	void DrawStatsScreen(float statsScreenOpacity) 
	{ 
		float statsX = overlayRect.x + overlayRect.width * 0.06f;
		float statsY = overlayRect.y + overlayRect.height * 0.205f;
		float statsWidth = overlayRect.width * 0.88f;
		float statsHeight = overlayRect.height * 0.578f;
		float fontScale = 1f;

		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		
		Color fullHealthPumaHeadshotColor = new Color(0.99f, 0.92f, 0f, 0.7f * statsScreenOpacity);
		Color fullHealthPumaDeerIconColor = new Color(0.99f, 0.92f, 0f, 0.4f * statsScreenOpacity);
		Color fullHealthPumaTextColor = new Color(0.32f, 0.32f, 0.22f, 0.8f * statsScreenOpacity);

		Color deadPumaHeadshotColor = new Color(0.8f, 0.1f, 0f, 0.55f * statsScreenOpacity);
		Color deadPumaDeerIconColor = new Color(0.5f, 0.02f, 0f, 0.8f * statsScreenOpacity);
		Color deadPumaTextColor = new Color(0.32f, 0.32f, 0.22f, 0.8f * statsScreenOpacity);

		
		// background rectangles
		GUI.color = new Color(0f, 0f, 0f, 1f * statsScreenOpacity);
		GUI.Box(new Rect(statsX - overlayRect.width * 0.02f, statsY - overlayRect.height * 0.025f, statsWidth + overlayRect.width * 0.04f, statsHeight + overlayRect.height * 0.05f), "");
		GUI.color = new Color(0f, 0f, 0f, 0.2f * statsScreenOpacity);
		GUI.Box(new Rect(statsX, statsY, statsWidth, statsHeight), "");

		float columnCount = 7f;
		float columnGap = statsWidth * 0.02f;
		float midColumnSizeIncrease = columnGap * 2f;
		float columnWidth = (statsWidth - (columnGap * (columnCount-1)) - midColumnSizeIncrease) / columnCount;
		GUI.color = new Color(0f, 0f, 0f, 0.8f * statsScreenOpacity);
		GUI.Box(new Rect(statsX + columnWidth*0 + columnGap*0, statsY, columnWidth, statsHeight), "");
		GUI.Box(new Rect(statsX + columnWidth*1 + columnGap*1, statsY, columnWidth, statsHeight), "");
		GUI.Box(new Rect(statsX + columnWidth*2 + columnGap*2, statsY, columnWidth, statsHeight), "");
		GUI.Box(new Rect(statsX + columnWidth*3 + columnGap*3, statsY, columnWidth + midColumnSizeIncrease, statsHeight), "");
		GUI.Box(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease, statsY, columnWidth, statsHeight), "");
		GUI.Box(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease, statsY, columnWidth, statsHeight), "");
		GUI.Box(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease, statsY, columnWidth, statsHeight), "");
		GUI.color = new Color(0f, 0f, 0f, 0.7f * statsScreenOpacity);
		GUI.Box(new Rect(statsX + columnWidth*0 + columnGap*0, statsY, columnWidth, statsHeight), "");
		GUI.Box(new Rect(statsX + columnWidth*1 + columnGap*1, statsY, columnWidth, statsHeight), "");
		GUI.Box(new Rect(statsX + columnWidth*2 + columnGap*2, statsY, columnWidth, statsHeight), "");
		GUI.color = new Color(0f, 0f, 0f, 0.9f * statsScreenOpacity);
		GUI.Box(new Rect(statsX + columnWidth*3 + columnGap*3, statsY, columnWidth + midColumnSizeIncrease, statsHeight), "");
		GUI.color = new Color(0f, 0f, 0f, 0.8f * statsScreenOpacity);
		GUI.Box(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease, statsY, columnWidth, statsHeight), "");
		GUI.color = new Color(0f, 0f, 0f, 0.85f * statsScreenOpacity);
		GUI.Box(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease, statsY, columnWidth, statsHeight), "");
		GUI.color = new Color(0f, 0f, 0f, 0.9f * statsScreenOpacity);
		GUI.Box(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease, statsY, columnWidth, statsHeight), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * statsScreenOpacity);
	

		// clickable invisible buttons to select puma
		GUI.color = new Color(1f, 1f, 1f, 0f * statsScreenOpacity);

		if (GUI.Button(new Rect(statsX + columnWidth*0 + columnGap*0, statsY, columnWidth, statsHeight), "") && PumaIsSelectable(0)) {
			guiManager.selectedPuma = 0;
			levelManager.SetSelectedPuma(0);
		}
		if (GUI.Button(new Rect(statsX + columnWidth*1 + columnGap*1, statsY, columnWidth, statsHeight), "") && PumaIsSelectable(1)) {
			guiManager.selectedPuma = 1;
			levelManager.SetSelectedPuma(1);
		}
		if (GUI.Button(new Rect(statsX + columnWidth*2 + columnGap*2, statsY, columnWidth, statsHeight), "") && PumaIsSelectable(2)) {
			guiManager.selectedPuma = 2;
			levelManager.SetSelectedPuma(2);
		}
		if (GUI.Button(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease, statsY, columnWidth, statsHeight), "") && PumaIsSelectable(3)) {
			guiManager.selectedPuma = 3;
			levelManager.SetSelectedPuma(3);
		}
		if (GUI.Button(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease, statsY, columnWidth, statsHeight), "") && PumaIsSelectable(4)) {
			guiManager.selectedPuma = 4;
			levelManager.SetSelectedPuma(4);
		}
		if (GUI.Button(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease, statsY, columnWidth, statsHeight), "") && PumaIsSelectable(5)) {
			guiManager.selectedPuma = 5;
			levelManager.SetSelectedPuma(5);
		}

		GUI.color = new Color(1f, 1f, 1f, 1f * statsScreenOpacity);
		
		
		
		
		// population bar
		
		float yOffsetForAddingPopulationBar = overlayRect.height * -0.012f;
		float actualstatsScreenOpacity = statsScreenOpacity;
		statsScreenOpacity = statsScreenOpacity * 0.9f;
		GUI.color = new Color(1f, 1f, 1f, 1f * statsScreenOpacity);		
		
		float healthBarX = overlayRect.x + overlayRect.width * 0.04f;
		float healthBarY = overlayRect.y + overlayRect.height * 0.844f + yOffsetForAddingPopulationBar;
		float healthBarWidth = overlayRect.width * 0.92f;
		float healthBarHeight = overlayRect.height * 0.048f;	
		float healthBarLabelWidth = 0f; //healthBarWidth * 0.13f;

		if (healthBarLabelWidth > 0f) {
			GUI.color = new Color(0f, 0f, 0f, 0.8f * statsScreenOpacity);
			GUI.Box(new Rect(healthBarX, healthBarY, healthBarLabelWidth * 0.985f, healthBarHeight), "");
			GUI.Box(new Rect(healthBarX, healthBarY, healthBarLabelWidth * 0.985f, healthBarHeight), "");
			GUI.Box(new Rect(healthBarX + healthBarWidth - healthBarLabelWidth * 0.985f, healthBarY, healthBarLabelWidth * 0.985f, healthBarHeight), "");
			GUI.Box(new Rect(healthBarX + healthBarWidth - healthBarLabelWidth * 0.985f, healthBarY, healthBarLabelWidth * 0.985f, healthBarHeight), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * statsScreenOpacity);
		}

		guiComponents.DrawPopulationHealthBar(statsScreenOpacity, healthBarX + healthBarLabelWidth, healthBarY, healthBarWidth - healthBarLabelWidth * 2f, healthBarHeight, false, true);

		statsScreenOpacity = actualstatsScreenOpacity;
		GUI.color = new Color(1f, 1f, 1f, 1f * statsScreenOpacity);		


		// puma heads at tops of columns
		
		float textureX;
		float textureY;
		float textureHeight;
		float textureWidth;
		Texture2D headshotTexture;
		
		GUI.color = new Color(1f, 1f, 1f, 0.85f * statsScreenOpacity);
		
		float rightShift = columnWidth * 0.18f;
		float backgroundInset = columnWidth * 0.05f;
		float headSize = 0.135f;
		
		// background texture
		if (guiManager.selectedPuma != -1) {
			int columnMultiplier = (guiManager.selectedPuma < 3) ? guiManager.selectedPuma : guiManager.selectedPuma + 1;
			headshotTexture = closeupBackgroundTexture;
			textureX = statsX + columnWidth*columnMultiplier + columnGap*columnMultiplier + backgroundInset;
			textureX +=  (guiManager.selectedPuma < 3) ? 0 : midColumnSizeIncrease;
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
			GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
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
			GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
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
			GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
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
			GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
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
			GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
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
			GUI.color = new Color(1f, 1f, 1f, headshotOpacity * statsScreenOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
		GUI.color = new Color(1f, 1f, 1f, 1f * statsScreenOpacity);

	
		

		// puma names

		GUI.color = new Color(1f, 1f, 1f, 1f * statsScreenOpacity);
		
		float bigTextFont = 0.016f;
		float textY = statsY + statsHeight * 0.1705f;		
		textureHeight = statsHeight * headSize;
		
		//style.normal.textColor = new Color(0.99f, 0.62f, 0f, 0.95f);
		//style.normal.textColor = new Color(0.99f, 0.75f, 0.3f, 0.95f);
		Color unselectedTextColor = new Color(0.85f, 0.74f, 0.5f, 0.85f);	
		Color selectedTextColor = new Color(0.88f, 0.55f, 0f, 1f);
		selectedTextColor = unselectedTextColor;
		
		style.normal.textColor = (guiManager.selectedPuma == 0) ? selectedTextColor : ((scoringSystem.GetPumaHealth(0) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(0) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * bigTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*0 + columnGap*0;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "Eric", style);
		style.alignment = TextAnchor.MiddleCenter;
		
		style.normal.textColor = (guiManager.selectedPuma == 1) ? selectedTextColor : ((scoringSystem.GetPumaHealth(1) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(1) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * bigTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*1 + columnGap*1;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "Palo", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (guiManager.selectedPuma == 2) ? selectedTextColor : ((scoringSystem.GetPumaHealth(2) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(2) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * bigTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*2 + columnGap*2;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "Mitch", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (guiManager.selectedPuma == 3) ? selectedTextColor : ((scoringSystem.GetPumaHealth(3) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(3) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * bigTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "Trish", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (guiManager.selectedPuma == 4) ? selectedTextColor : ((scoringSystem.GetPumaHealth(4) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(4) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * bigTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "Liam", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (guiManager.selectedPuma == 5) ? selectedTextColor : ((scoringSystem.GetPumaHealth(5) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(5) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * bigTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "Barb", style);
		style.alignment = TextAnchor.MiddleCenter;

		
		
		// puma labels
		
		float smallTextFont = 0.013f;
		textY = statsY + statsHeight * 0.212f;	

		style.normal.textColor = (guiManager.selectedPuma == 0) ? selectedTextColor : ((scoringSystem.GetPumaHealth(0) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(0) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*0 + columnGap*0;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "2 years - male", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (guiManager.selectedPuma == 1) ? selectedTextColor : ((scoringSystem.GetPumaHealth(1) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(1) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*1 + columnGap*1;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "2 years - female", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (guiManager.selectedPuma == 2) ? selectedTextColor : ((scoringSystem.GetPumaHealth(2) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(2) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*2 + columnGap*2;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "5 years - male", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (guiManager.selectedPuma == 3) ? selectedTextColor : ((scoringSystem.GetPumaHealth(3) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(3) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "5 years - female", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (guiManager.selectedPuma == 4) ? selectedTextColor : ((scoringSystem.GetPumaHealth(4) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(4) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease;
		GUI.Button(new Rect(textureX, textY, columnWidth, textureHeight), "8 years - male", style);
		style.alignment = TextAnchor.MiddleCenter;

		style.normal.textColor = (guiManager.selectedPuma == 5) ? selectedTextColor : ((scoringSystem.GetPumaHealth(5) <= 0f) ? deadPumaTextColor : ((scoringSystem.GetPumaHealth(5) >= 1f) ? fullHealthPumaTextColor : unselectedTextColor));
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
		style.fontSize = (int)(overlayRect.width * smallTextFont * fontScale * 1f);
		style.fontStyle = FontStyle.BoldAndItalic;
		style.alignment = TextAnchor.UpperCenter;
		textureX = statsX + columnWidth*3 + columnGap*3;
		GUI.Button(new Rect(textureX, statsY + statsHeight * 0.068f, columnWidth + midColumnSizeIncrease, textureHeight), "overall results", style);
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
			
			GUI.color = new Color(1f, 1f, 1f, 1f * statsScreenOpacity);
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
		DrawDisplayBarsVert(0, statsScreenOpacity, statsX + columnWidth*0 + columnGap*0 + columnWidth*0.15f, verticalBarsY, columnWidth * 0.7f, statsHeight * 0.13f);
		DrawDisplayBarsVert(1, statsScreenOpacity, statsX + columnWidth*1 + columnGap*1 + columnWidth*0.15f, verticalBarsY, columnWidth * 0.7f, statsHeight * 0.13f);
		DrawDisplayBarsVert(2, statsScreenOpacity, statsX + columnWidth*2 + columnGap*2 + columnWidth*0.15f, verticalBarsY, columnWidth * 0.7f, statsHeight * 0.13f);
		DrawDisplayBarsVert(3, statsScreenOpacity, statsX + columnWidth*4 + columnGap*4 + columnWidth*0.15f + midColumnSizeIncrease, verticalBarsY, columnWidth * 0.7f, statsHeight * 0.13f);
		DrawDisplayBarsVert(4, statsScreenOpacity, statsX + columnWidth*5 + columnGap*5 + columnWidth*0.15f + midColumnSizeIncrease, verticalBarsY, columnWidth * 0.7f, statsHeight * 0.13f);
		DrawDisplayBarsVert(5, statsScreenOpacity, statsX + columnWidth*6 + columnGap*6 + columnWidth*0.15f + midColumnSizeIncrease, verticalBarsY, columnWidth * 0.7f, statsHeight * 0.13f);
		DrawDisplayBarsVert(6, statsScreenOpacity, statsX + columnWidth*3 + columnGap*3 + columnWidth*0.16f, verticalBarsY, columnWidth * 0.68f + midColumnSizeIncrease, statsHeight * 0.13f);

		
		// health bar
		
		float healthBarYFactor = 0.94f;

		guiComponents.DrawPumaHealthBar(0, statsScreenOpacity, statsX + columnWidth*0 + columnGap*0, statsY + statsHeight * healthBarYFactor, columnWidth, statsHeight * 0.04f, true, true);
		guiComponents.DrawPumaHealthBar(1, statsScreenOpacity, statsX + columnWidth*1 + columnGap*1, statsY + statsHeight * healthBarYFactor, columnWidth, statsHeight * 0.04f, true, true);
		guiComponents.DrawPumaHealthBar(2, statsScreenOpacity, statsX + columnWidth*2 + columnGap*2, statsY + statsHeight * healthBarYFactor, columnWidth, statsHeight * 0.04f, true, true);
		guiComponents.DrawPumaHealthBar(3, statsScreenOpacity, statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease, statsY + statsHeight * healthBarYFactor, columnWidth, statsHeight * 0.04f, true, true);
		guiComponents.DrawPumaHealthBar(4, statsScreenOpacity, statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease, statsY + statsHeight * healthBarYFactor, columnWidth, statsHeight * 0.04f, true, true);
		guiComponents.DrawPumaHealthBar(5, statsScreenOpacity, statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease, statsY + statsHeight * healthBarYFactor, columnWidth, statsHeight * 0.04f, true, true);


		
		// ======================================
		
		// DIVIDERS

		float dividerY;
		Color upperColor;
		Color lowerColor;
		

		// horizontal - upper	
		dividerY = statsY + statsHeight * 0.28f;
		upperColor = new Color(0f, 0f, 0f, 0.55f);
		lowerColor = new Color(0.5f, 0.49f, 0.47f, 0.3f);
		guiUtils.DrawRect(new Rect(statsX + columnWidth*0 + columnGap*0, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*0 + columnGap*0, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*1 + columnGap*1, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*1 + columnGap*1, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*2 + columnGap*2, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*2 + columnGap*2, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*3 + columnGap*3, dividerY - overlayRect.height * 0.005f + centerColumnOffsetY, columnWidth + midColumnSizeIncrease, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*3 + columnGap*3, dividerY + centerColumnOffsetY, columnWidth + midColumnSizeIncrease, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	

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
		guiUtils.DrawRect(new Rect(statsX + columnWidth*0 + columnGap*0 + columnWidth * leftPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*0 + columnGap*0 + columnWidth * leftPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*0 + columnGap*0 + columnWidth * rightPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*0 + columnGap*0 + columnWidth * rightPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*1 + columnGap*1 + columnWidth * leftPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*1 + columnGap*1 + columnWidth * leftPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*1 + columnGap*1 + columnWidth * rightPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*1 + columnGap*1 + columnWidth * rightPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*2 + columnGap*2 + columnWidth * leftPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*2 + columnGap*2 + columnWidth * leftPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*2 + columnGap*2 + columnWidth * rightPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*2 + columnGap*2 + columnWidth * rightPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		rightPercent = 0.69f;
		guiUtils.DrawRect(new Rect(statsX + columnWidth*3 + columnGap*3 + columnWideWidth * leftPercent - leftWidth, dividerY + overlayRect.height * 0.003f + centerColumnOffsetY, leftWidth, barsHeight - centerColumnOffsetY), leftColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*3 + columnGap*3 + columnWideWidth * leftPercent, dividerY + overlayRect.height * 0.003f + centerColumnOffsetY, rightWidth, barsHeight - centerColumnOffsetY), rightColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*3 + columnGap*3 + columnWideWidth * rightPercent - leftWidth, dividerY + overlayRect.height * 0.003f + centerColumnOffsetY, leftWidth, barsHeight - centerColumnOffsetY), leftColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*3 + columnGap*3 + columnWideWidth * rightPercent, dividerY + overlayRect.height * 0.003f + centerColumnOffsetY, rightWidth, barsHeight - centerColumnOffsetY), rightColor);	
		rightPercent = 0.67f;
		guiUtils.DrawRect(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease+ columnWidth * leftPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease+ columnWidth * leftPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease+ columnWidth * rightPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease+ columnWidth * rightPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease+ columnWidth * leftPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease+ columnWidth * leftPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease+ columnWidth * rightPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease+ columnWidth * rightPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease+ columnWidth * leftPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease+ columnWidth * leftPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease+ columnWidth * rightPercent - leftWidth, dividerY + overlayRect.height * 0.003f, leftWidth, barsHeight), leftColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease+ columnWidth * rightPercent, dividerY + overlayRect.height * 0.003f, rightWidth, barsHeight), rightColor);	

/*
		// horizontal - middle	
		dividerY = statsY + statsHeight * 0.68f;
		upperColor = new Color(0f, 0f, 0f, 0.55f);
		lowerColor = new Color(0.4f, 0.4f, 0.4f, 0.3f);
		guiUtils.DrawRect(new Rect(statsX + columnWidth*0 + columnGap*0, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*0 + columnGap*0, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*1 + columnGap*1, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*1 + columnGap*1, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*2 + columnGap*2, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*2 + columnGap*2, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*3 + columnGap*3, dividerY - overlayRect.height * 0.005f, columnWidth + midColumnSizeIncrease, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*3 + columnGap*3, dividerY, columnWidth + midColumnSizeIncrease, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
*/
		// horizontal - lower	
		dividerY = statsY + statsHeight * 0.86f;
		upperColor = new Color(0f, 0f, 0f, 0.55f);
		lowerColor = new Color(0.5f, 0.49f, 0.47f, 0.3f);
		guiUtils.DrawRect(new Rect(statsX + columnWidth*0 + columnGap*0, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*0 + columnGap*0, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*1 + columnGap*1, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*1 + columnGap*1, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*2 + columnGap*2, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*2 + columnGap*2, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*3 + columnGap*3, dividerY - overlayRect.height * 0.005f, columnWidth + midColumnSizeIncrease, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*3 + columnGap*3, dividerY, columnWidth + midColumnSizeIncrease, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*4 + columnGap*4 + midColumnSizeIncrease, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*5 + columnGap*5 + midColumnSizeIncrease, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease, dividerY - overlayRect.height * 0.005f, columnWidth, overlayRect.height * 0.005f), upperColor);	
		guiUtils.DrawRect(new Rect(statsX + columnWidth*6 + columnGap*6 + midColumnSizeIncrease, dividerY, columnWidth, overlayRect.height * 0.003f), lowerColor);	

		// ======================================
		
		
		
		
		
		
		return;

		// ======================================
		
		// DIVIDER

		guiUtils.DrawRect(new Rect(statsX, statsY + (statsHeight * 0.375f) - overlayRect.height * 0.005f, statsWidth, overlayRect.height * 0.005f), new Color(0f, 0f, 0f, 0.35f));	
		guiUtils.DrawRect(new Rect(statsX, statsY + (statsHeight * 0.375f), statsWidth, overlayRect.height * 0.005f), new Color(1f, 1f, 1f, 0.5f));	

		guiUtils.DrawRect(new Rect(statsX, statsY + (statsHeight/2) - overlayRect.height * 0.005f, statsWidth, overlayRect.height * 0.005f), new Color(0f, 0f, 0f, 0.35f));	
		guiUtils.DrawRect(new Rect(statsX, statsY + (statsHeight/2), statsWidth, overlayRect.height * 0.005f), new Color(1f, 1f, 1f, 0.5f));	

		guiUtils.DrawRect(new Rect(statsX, statsY + (statsHeight * 0.875f) - overlayRect.height * 0.005f, statsWidth, overlayRect.height * 0.005f), new Color(0f, 0f, 0f, 0.35f));	
		guiUtils.DrawRect(new Rect(statsX, statsY + (statsHeight * 0.875f), statsWidth, overlayRect.height * 0.005f), new Color(1f, 1f, 1f, 0.5f));	

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
		//guiUtils.DrawRect(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.14f, statsWidth * 0.9f, statsHeight * 0.007f), new Color(1f, 1f, 1f, 0.5f));	
		//guiUtils.DrawRect(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.38f, statsWidth * 0.9f, statsHeight * 0.007f), new Color(1f, 1f, 1f, 0.5f));	

		// button
		GUI.skin = guiManager.customGUISkin;
		guiManager.customGUISkin.button.fontSize = (int)(overlayRect.width * 0.0165);
		guiManager.customGUISkin.button.fontStyle = FontStyle.Normal;
		GUI.color = new Color(1f, 1f, 1f, 1f);
		GUI.backgroundColor = new Color(1f, 0f, 0f, 1f);
		if (GUI.Button(new Rect(overlayRect.width * -0.003f + statsX + statsWidth * 0.24f, overlayRect.width * -0.0025f + statsY + statsHeight * 0.56f, overlayRect.width * 0.006f + statsWidth * 0.3f, overlayRect.width * 0.005f + statsHeight * 0.098f), "")) {
			guiManager.OpenInfoPanel(2);
		}
		GUI.backgroundColor = new Color(0.5f, 0.5f, 0.9f, 1f);
		if (GUI.Button(new Rect(statsX + statsWidth * 0.24f, statsY + statsHeight * 0.56f, statsWidth * 0.3f, statsHeight * 0.098f), "")) {
			guiManager.OpenInfoPanel(2);
		}
		if (GUI.Button(new Rect(statsX + statsWidth * 0.24f, statsY + statsHeight * 0.56f, statsWidth * 0.3f, statsHeight * 0.098f), "How Pumas Hunt....")) {
			guiManager.OpenInfoPanel(2);
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
		//guiUtils.DrawRect(new Rect(statsX + statsWidth * 0.365f, statsY + statsHeight * 0.245f, statsWidth * 0.03f, statsHeight * 0.04f), new Color(0.06f, 0.06f, 0.16f, 0.17f));	
		//GUI.Button(new Rect(statsX + statsWidth * 0.30f, statsY + statsHeight * 0.29f, statsWidth * 0.16f, statsHeight * 0.1f), "--", style);
		//guiUtils.DrawRect(new Rect(statsX + statsWidth * 0.365f, statsY + statsHeight * 0.32f, statsWidth * 0.03f, statsHeight * 0.04f), new Color(1f, 1f, 1f, 0.0f));	
		style.normal.textColor = new Color(0.063f, 0.059f, 0.161f, 1f);
		GUI.Button(new Rect(statsX + statsWidth * 0.30f, statsY + statsHeight * 0.38f, statsWidth * 0.16f, statsHeight * 0.1f), "6", style);
		style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);

		GUI.Button(new Rect(statsX + statsWidth * 0.46f, statsY + statsHeight * 0.14f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		GUI.Button(new Rect(statsX + statsWidth * 0.46f, statsY + statsHeight * 0.215f, statsWidth * 0.16f, statsHeight * 0.1f), "0", style);
		//GUI.Button(new Rect(statsX + statsWidth * 0.46f, statsY + statsHeight * 0.29f, statsWidth * 0.16f, statsHeight * 0.1f), "--", style);
		//guiUtils.DrawRect(new Rect(statsX + statsWidth * 0.525f, statsY + statsHeight * 0.32f, statsWidth * 0.03f, statsHeight * 0.04f), new Color(1f, 1f, 1f, 0.0f));	
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
		//guiUtils.DrawRect(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.14f, statsWidth * 0.9f, statsHeight * 0.007f), new Color(1f, 1f, 1f, 0.5f));	
		//guiUtils.DrawRect(new Rect(statsX + statsWidth * 0.05f, statsY + statsHeight * 0.38f, statsWidth * 0.9f, statsHeight * 0.007f), new Color(1f, 1f, 1f, 0.5f));	
		
		// button
		GUI.skin = guiManager.customGUISkin;
		guiManager.customGUISkin.button.fontSize = (int)(overlayRect.width * 0.0165);
		guiManager.customGUISkin.button.fontStyle = FontStyle.Normal;
		GUI.color = new Color(1f, 1f, 1f, 1f);
		GUI.backgroundColor = new Color(1f, 0f, 0f, 1f);
		if (GUI.Button(new Rect(overlayRect.width * -0.003f + statsX + statsWidth * 0.24f, overlayRect.width * -0.0025f + statsY + statsHeight * 0.56f, overlayRect.width * 0.006f + statsWidth * 0.3f, overlayRect.width * 0.005f + statsHeight * 0.098f), "")) {
			guiManager.OpenInfoPanel(4);
		}
		GUI.backgroundColor = new Color(0.5f, 0.5f, 0.9f, 1f);
		if (GUI.Button(new Rect(statsX + statsWidth * 0.24f, statsY + statsHeight * 0.56f, statsWidth * 0.3f, statsHeight * 0.098f), "")) {
			guiManager.OpenInfoPanel(4);
		}
		if (GUI.Button(new Rect(statsX + statsWidth * 0.24f, statsY + statsHeight * 0.56f, statsWidth * 0.3f, statsHeight * 0.098f), "Threats to Pumas....")) {
			guiManager.OpenInfoPanel(4);
		}
		GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
		GUI.color = new Color(1f, 1f, 1f, 1f);

		//buttonStyle.fontSize = buttonDownStyle.fontSize = (int)(overlayRect.width * 0.0165);
		//GUI.color = new Color(1f, 1f, 1f, 0.89f);
		//if (GUI.Button(new Rect(statsX + statsWidth * 0.24f, statsY + statsHeight * 0.545f, statsWidth * 0.52f, statsHeight * 0.11f), "THE THREATS REAL PUMAS FACE...", buttonStyle))
			//currentScreen = currentScreen;
		//GUI.color = new Color(1f, 1f, 1f, 1f);

	}


	void DrawQuitScreen(float quitScreenOpacity) 
	{ 
		float quitScreenY = overlayRect.y + overlayRect.height * 0.37f;

		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;

		// background rectangle
		//guiUtils.DrawRect(new Rect(overlayRect.width * 0.315f, overlayRect.height * 0.3f, overlayRect.width * 0.37f, overlayRect.height * 0.3f), new Color(1f, 1f, 1f, 0.6f));	
		GUI.color = new Color(0f, 0f, 0f, 1f * quitScreenOpacity);
		GUI.Box(new Rect(overlayRect.x + overlayRect.width * 0.315f, quitScreenY, overlayRect.width * 0.37f, overlayRect.height * 0.3f), "");
		GUI.color = new Color(0f, 0f, 0f, 0.55f * quitScreenOpacity);
		GUI.Box(new Rect(overlayRect.x + overlayRect.width * 0.315f, quitScreenY, overlayRect.width * 0.37f, overlayRect.height * 0.3f), "");
		GUI.color = new Color(0f, 0f, 0f, 1f * quitScreenOpacity);

		GUI.color = new Color(1f, 1f, 1f, 1f * quitScreenOpacity);
		style.fontSize = (int)(overlayRect.width * 0.036f);
		style.fontStyle = FontStyle.BoldAndItalic;
		//style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);
		//style.normal.textColor = new Color(1f, 1f, 1f, 1f);
			
		// quit screen prompt
		//guiUtils.DrawRect(new Rect(overlayRect.width * 0.32f, overlayRect.height * 0.34f, overlayRect.width * 0.36f, overlayRect.height * 0.08f), new Color(1f, 1f, 1f, 0.7f));	
		style.fontSize = (int)(overlayRect.width * 0.036f);
		style.fontStyle = FontStyle.BoldAndItalic;
		//style.normal.textColor = new Color(0.392f, 0.0588f, 0.0588f, 1f);
		style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f * quitScreenOpacity);
		GUI.Button(new Rect(overlayRect.x + overlayRect.width * 0.3f, quitScreenY + overlayRect.height * 0.03f, overlayRect.width * 0.4f, overlayRect.height * 0.1f), "Really Quit?", style);
		style.normal.textColor = Color.white;

		// quit button
		GUI.color = new Color(1f, 1f, 1f, 0.15f * quitScreenOpacity);
		guiUtils.DrawRect(new Rect(overlayRect.x + overlayRect.width * 0.415f, quitScreenY + overlayRect.height * 0.150f, overlayRect.width * 0.17f, overlayRect.height * 0.09f), new Color(1f, 1f, 1f, 1f));	
		GUI.skin = guiManager.customGUISkin;
		guiManager.customGUISkin.button.fontSize = (int)(overlayRect.width * 0.026);
		guiManager.customGUISkin.button.fontStyle = FontStyle.Normal;
		GUI.color = new Color(1f, 1f, 1f, 1f * quitScreenOpacity);
		GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
		if (GUI.Button(new Rect(overlayRect.x + overlayRect.width * 0.42f, quitScreenY + overlayRect.height * 0.155f, overlayRect.width * 0.16f, overlayRect.height * 0.08f), "")) {
			Application.Quit();
		}
		if (GUI.Button(new Rect(overlayRect.x + overlayRect.width * 0.42f, quitScreenY + overlayRect.height * 0.155f, overlayRect.width * 0.16f, overlayRect.height * 0.08f), "Quit")) {
			Application.Quit();
		}
		GUI.color = new Color(1f, 1f, 1f, 1f * quitScreenOpacity);
		GUI.backgroundColor = new Color(0f, 0f, 0f, 0f);
	}
	
	
	
	
	
	
	void DrawDisplayBars(int pumaNum, float displayBarsOpacity, float refX, float refY, float refWidth, float refHeight, bool brightFlag = false) 
	{ 
		float yOffset = overlayRect.height * -0.085f;
		float displayBarsRightShift = overlayRect.height * -0.1265f;
		
		refWidth = refHeight * 3f;
	
		// display bars for characteristics: backgrounds
		GUI.color = new Color(0f, 0f, 0f, (brightFlag ? 0.9f : 0.75f) * displayBarsOpacity);
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
		GUI.color = new Color(1f, 1f, 1f, (brightFlag ? 0.7f : 0.55f) * displayBarsOpacity);


		
		if (true) {

			if (scoringSystem.GetPumaHealth(pumaNum) >= 1f)
				GUI.color = new Color(0.1f, 1f, 0f, (brightFlag ? 0.7f : 0.55f) * displayBarsOpacity);
			else if (scoringSystem.GetPumaHealth(pumaNum) <= 0f)
				GUI.color = new Color(0.4f, 0.4f, 0.4f, (brightFlag ? 0.5f : 0.4f) * displayBarsOpacity);

			guiUtils.DrawRect(new Rect(refX + refWidth * 0.54f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.018f - overlayRect.height * 0.002f, refWidth * 0.34f, overlayRect.height * 0.0048f), new Color(0.42f, 0.404f, 0.533f, 1f));	
			guiUtils.DrawRect(new Rect(refX + refWidth * 0.54f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.040f - overlayRect.height * 0.002f, refWidth * 0.34f, overlayRect.height * 0.0048f), new Color(0.5f, 0.5f, 0.5f, 1f));	
			guiUtils.DrawRect(new Rect(refX + refWidth * 0.54f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.062f - overlayRect.height * 0.002f, refWidth * 0.34f, overlayRect.height * 0.0048f), new Color(0.5f, 0.5f, 0.5f, 1f));	
			guiUtils.DrawRect(new Rect(refX + refWidth * 0.54f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.084f - overlayRect.height * 0.002f, refWidth * 0.34f, overlayRect.height * 0.0048f), new Color(0.5f, 0.5f, 0.5f, 1f));	

			if (scoringSystem.GetPumaHealth(pumaNum) > 0f && scoringSystem.GetPumaHealth(pumaNum) < 1f) {
				float speed = guiManager.GetPumaSpeed(pumaNum);
				Color speedColor = (speed > 0.66f) ? new Color(0f, 1f, 0f, 0.8f) : ((speed > 0.33f) ? new Color(1f, 1f, 0f, 0.85f) : new Color(1f, 0f, 0f, 1f));
				guiUtils.DrawRect(new Rect(refX + refWidth * 0.54f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.018f - overlayRect.height * 0.002f, refWidth * 0.34f * speed, overlayRect.height * 0.0048f), speedColor);	

				float stealth = guiManager.GetPumaStealth(pumaNum);
				Color stealthColor = (stealth > 0.66f) ? new Color(0f, 1f, 0f,  0.8f) : ((stealth > 0.33f) ? new Color(1f, 1f, 0f, 0.85f) : new Color(1f, 0f, 0f, 1f));
				guiUtils.DrawRect(new Rect(refX + refWidth * 0.54f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.040f - overlayRect.height * 0.002f, refWidth * 0.34f * stealth, overlayRect.height * 0.0048f), stealthColor);	

				float endurance = guiManager.GetPumaEndurance(pumaNum);
				Color enduranceColor = (endurance > 0.66f) ? new Color(0f, 1f, 0f,  0.8f) : ((endurance > 0.33f) ? new Color(1f, 1f, 0f, 0.85f) : new Color(1f, 0f, 0f, 1f));
				guiUtils.DrawRect(new Rect(refX + refWidth * 0.54f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.062f - overlayRect.height * 0.002f, refWidth * 0.34f * endurance, overlayRect.height * 0.0048f), enduranceColor);	

				float power = guiManager.GetPumaPower(pumaNum);
				Color powerColor = (power > 0.66f) ? new Color(0f, 1f, 0f,  0.8f) : ((power > 0.33f) ? new Color(1f, 1f, 0f, 0.85f) : new Color(1f, 0f, 0f, 1f));
				guiUtils.DrawRect(new Rect(refX + refWidth * 0.54f + displayBarsRightShift,  yOffset + refY + refHeight + overlayRect.height * 0.084f - overlayRect.height * 0.002f, refWidth * 0.34f * power, overlayRect.height * 0.0048f), powerColor);	
			}
		}
				
		GUI.color = new Color(1f, 1f, 1f, 1f * displayBarsOpacity);
	}
	
	
	void DrawDisplayBarsVert(int pumaNum, float displayBarsOpacity, float barsX, float barsY, float barsWidth, float barsHeight) 
	{ 
		float rectWidth = barsWidth * 0.20f;
		float rect1X = barsX;
		float rect2X = barsX + barsWidth * 0.4f;
		float rect3X = barsX + barsWidth * 0.8f;
		float margin = rectWidth * 0.3f;
		
		// display bars for characteristics: backgrounds
		GUI.color = new Color(0f, 0f, 0f, 0.9f * displayBarsOpacity);
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
		guiUtils.DrawRect(new Rect(rect1X+margin,  barsY+margin, rectWidth-margin*2f, barsHeight-margin*2f), barBackgroundColor);	
		guiUtils.DrawRect(new Rect(rect2X+margin,  barsY+margin, rectWidth-margin*2f, barsHeight-margin*2f), barBackgroundColor);	
		guiUtils.DrawRect(new Rect(rect3X+margin,  barsY+margin, rectWidth-margin*2f, barsHeight-margin*2f), barBackgroundColor);	
		
		// display bars for characteristics: fillers
		GUI.color = new Color(1f, 1f, 1f, 0.6f * displayBarsOpacity);
		if (pumaNum < 6 && scoringSystem.GetPumaHealth(pumaNum) <= 0f)
			GUI.color = new Color(0.6f, 0.1f, 0.1f, 0.4f * displayBarsOpacity);
		else if (pumaNum < 6 && scoringSystem.GetPumaHealth(pumaNum) >= 1f)
			//GUI.color = new Color(0.1f, 0.75f, 0.1f, 0.4f * displayBarsOpacity);
			GUI.color = new Color(0.1f, 0.50f, 0f, 0.5f * displayBarsOpacity);

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
			guiUtils.DrawRect(new Rect(rect1X+margin,  barsY+margin + (1f - buckSuccess) * (barsHeight-margin*2f), rectWidth-margin*2f, (barsHeight-margin*2f) * buckSuccess), buckColor);	
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
			guiUtils.DrawRect(new Rect(rect2X+margin,  barsY+margin + (1f - doeSuccess) * (barsHeight-margin*2f), rectWidth-margin*2f, (barsHeight-margin*2f) * doeSuccess), doeColor);	
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
			guiUtils.DrawRect(new Rect(rect3X+margin,  barsY+margin + (1f - fawnSuccess) * (barsHeight-margin*2f), rectWidth-margin*2f, (barsHeight-margin*2f) * fawnSuccess), fawnColor);	
		}
		
		GUI.color = new Color(1f, 1f, 1f, 1f * displayBarsOpacity);
	}
	
		
	
	bool PumaIsSelectable(int pumaNum)
	{	
		if (scoringSystem.GetPumaHealth(pumaNum) <= 0f)
			return false;
			
		if (scoringSystem.GetPumaHealth(pumaNum) >= 1f)
			return false;
			
		return true;
	}
	
	
	
	
	

	
}