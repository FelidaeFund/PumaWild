using UnityEngine;
using System.Collections;

/// GameplayDisplay
/// Draw the heads up display that overlays on top of gameplay

public class GameplayDisplay : MonoBehaviour
{
	//===================================
	//===================================
	//		MODULE VARIABLES
	//===================================
	//===================================

	// textures based on bitmap files
	private Texture2D arrowTrayTexture;
	private Texture2D arrowTrayTopTexture;
	private Texture2D arrowUpTexture;
	private Texture2D arrowDownTexture;
	private Texture2D arrowLeftTexture;
	private Texture2D arrowRightTexture;
	private Texture2D arrowTurnLeftTexture;
	private Texture2D arrowTurnRightTexture;
	private Texture2D indicatorBuck; 
	private Texture2D indicatorDoe; 
	private Texture2D indicatorFawn; 
	private Texture2D indicatorBkgnd; 

	// external modules
	private GuiManager guiManager;
	private GuiComponents guiComponents;
	private GuiUtils guiUtils;
	private LevelManager levelManager;
	private InputControls inputControls;

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
		inputControls = GetComponent<InputControls>();
		
		// texture references from GuiManager
		arrowTrayTexture = guiManager.arrowTrayTexture;
		arrowTrayTopTexture = guiManager.arrowTrayTopTexture;
		arrowUpTexture = guiManager.arrowUpTexture;
		arrowDownTexture = guiManager.arrowDownTexture;
		arrowLeftTexture = guiManager.arrowLeftTexture;
		arrowRightTexture = guiManager.arrowRightTexture;
		arrowTurnLeftTexture = guiManager.arrowTurnLeftTexture;
		arrowTurnRightTexture = guiManager.arrowTurnRightTexture;
		indicatorBuck = guiManager.indicatorBuck;
		indicatorDoe = guiManager.indicatorDoe;
		indicatorFawn = guiManager.indicatorFawn;
		indicatorBkgnd = guiManager.indicatorBkgnd;
	}

	//===================================
	//===================================
	//	  DRAW THE GAMEPLAY DISPLAY
	//===================================
	//===================================
	
	public void Draw(float movementControlsOpacity, float positionIndicatorOpacity, float statusDisplayOpacity) 
	{ 
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

		//----------------------
		// MOVEMENT CONTROLS
		//----------------------
		
		float trayScaleFactor = (8.5f/7f);
			
		// lower right paw

		float textureWidth = boxWidth * 0.7f * trayScaleFactor;
		float textureX = Screen.width - textureWidth*1.2f - boxMargin;
		float textureHeight = arrowTrayTexture.height * (textureWidth / arrowTrayTopTexture.width);
		float textureY = Screen.height - boxHeight*0.8f - boxMargin;			

		GUI.color = new Color(1f, 1f, 1f, 0.55f * movementControlsOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY + textureHeight * 0.05f, textureWidth, textureHeight * 0.9f), arrowTrayTopTexture);
		GUI.color = new Color(1f, 1f, 1f, 0.7f * movementControlsOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), arrowTrayTexture);
		GUI.color = new Color(1f, 1f, 1f, 1f * movementControlsOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), arrowTurnRightTexture);
		float rightBoxX = textureX + textureWidth * 0.1f;
		float rightBoxY = textureY + textureHeight * 0.4f;
		float rightBoxWidth = textureWidth * 0.8f;
		float rightBoxHeight = textureHeight * 0.6f;
		inputControls.SetRectTurnRight(new Rect(rightBoxX, rightBoxY, rightBoxWidth, rightBoxHeight));
		
		// upper right paw
	
		textureX = Screen.width - boxWidth * trayScaleFactor - boxMargin;
		textureWidth = boxWidth * trayScaleFactor;
		textureHeight = arrowTrayTexture.height * (textureWidth / arrowTrayTopTexture.width);
		textureY = Screen.height - boxHeight*0.8f - boxMargin;			
		textureY -= textureHeight * 1f;
		
		GUI.color = new Color(1f, 1f, 1f, 0.55f * movementControlsOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY + textureHeight * 0.05f, textureWidth, textureHeight * 0.9f), arrowTrayTopTexture);
		GUI.color = new Color(1f, 1f, 1f, 0.7f * movementControlsOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), arrowTrayTexture);
		GUI.color = new Color(1f, 1f, 1f, 1f * movementControlsOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), arrowLeftTexture);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), arrowRightTexture);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), arrowUpTexture);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), arrowDownTexture);
		inputControls.SetRectForward(   new Rect(textureX + textureWidth * 0.37f, textureY + textureHeight * 0.45f, textureWidth * 0.24f, textureHeight * 0.24f));
		inputControls.SetRectBack(      new Rect(textureX + textureWidth * 0.37f, textureY + textureHeight * 0.69f, textureWidth * 0.24f, textureHeight * 0.24f));
		inputControls.SetRectDiagLeft(  new Rect(textureX + textureWidth * 0.11f, textureY + textureHeight * 0.63f, textureWidth * 0.26f, textureHeight * 0.3f));
		inputControls.SetRectDiagRight( new Rect(textureX + textureWidth * 0.61f, textureY + textureHeight * 0.63f, textureWidth * 0.26f, textureHeight * 0.3f));
			
		// upper left paw
	
		textureX = Screen.width - boxWidth - boxMargin;
		textureWidth = boxWidth * trayScaleFactor;
		textureHeight = arrowTrayTexture.height * (textureWidth / arrowTrayTopTexture.width);
		textureY = Screen.height - boxHeight - boxMargin;			
		textureY -= textureHeight * 1f;
		textureX = boxMargin;
	
		//GUI.color = new Color(1f, 1f, 1f, 0.55f * movementControlsOpacity);
		//GUI.DrawTexture(new Rect(textureX, textureY + textureHeight * 0.05f, textureWidth, textureHeight * 0.9f), arrowTrayTopTexture);
		//GUI.color = new Color(1f, 1f, 1f, 0.7f * movementControlsOpacity);
		//GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), arrowTrayTexture);
		//GUI.color = new Color(1f, 1f, 1f, 1f * movementControlsOpacity);
		//GUI.color = new Color(1f, 1f, 1f, 0f);
		//GUI.color = new Color(1f, 1f, 1f, 1f * movementControlsOpacity);
		
		// lower left paw

		textureWidth = boxWidth * 0.7f * trayScaleFactor;
		textureHeight = arrowTrayTexture.height * (textureWidth / arrowTrayTopTexture.width);
		textureX = boxMargin + textureWidth*0.2f;
		textureY = Screen.height - boxHeight*0.8f - boxMargin;

		GUI.color = new Color(1f, 1f, 1f, 0.55f * movementControlsOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY + textureHeight * 0.05f, textureWidth, textureHeight * 0.9f), arrowTrayTopTexture);
		GUI.color = new Color(1f, 1f, 1f, 0.7f * movementControlsOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), arrowTrayTexture);
		GUI.color = new Color(1f, 1f, 1f, 1f * movementControlsOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), arrowTurnLeftTexture);
		float leftBoxX = textureX + textureWidth * 0.1f;
		float leftBoxY = textureY + textureHeight * 0.4f;
		float leftBoxWidth = textureWidth * 0.8f;
		float leftBoxHeight = textureHeight * 0.6f;
		inputControls.SetRectTurnLeft(new Rect(leftBoxX, leftBoxY, leftBoxWidth, leftBoxHeight));	
			
		//----------------------
		// POSITION INDICATORS
		//----------------------
						
		// outer edge display
		GUI.color = new Color(1f, 1f, 1f, 0.9f * positionIndicatorOpacity);
		int borderThickness = (int)(boxMargin * 0.8f);//(int)(Screen.width * 0.026f);
		Color edgeColor = new Color(0f, 0f, 0f, 0.35f);	
		guiUtils.DrawRect(new Rect(0, 0, Screen.width, borderThickness), edgeColor);
		guiUtils.DrawRect(new Rect(0, Screen.height-borderThickness, Screen.width, borderThickness), edgeColor);
		guiUtils.DrawRect(new Rect(0, borderThickness, borderThickness, Screen.height-(borderThickness*2)), edgeColor);
		guiUtils.DrawRect(new Rect(Screen.width-borderThickness, borderThickness, borderThickness, Screen.height-(borderThickness*2)), edgeColor);

		// deer head indicators
		if (levelManager.buck != null && levelManager.doe != null && levelManager.fawn != null ) {
			DrawIndicator(levelManager.mainHeading, levelManager.pumaObj, levelManager.buck.gameObj, levelManager.buck.type, borderThickness, positionIndicatorOpacity);
			DrawIndicator(levelManager.mainHeading, levelManager.pumaObj, levelManager.doe.gameObj, levelManager.doe.type, borderThickness, positionIndicatorOpacity);
			DrawIndicator(levelManager.mainHeading, levelManager.pumaObj, levelManager.fawn.gameObj, levelManager.fawn.type, borderThickness, positionIndicatorOpacity);
		}
		
		//----------------------
		// STATUS DISPLAYS
		//----------------------
		
		float levelPanelX = boxMargin * 0.2f;
		float levelPanelY = Screen.height - boxMargin * 0.82f - boxMargin * 0.78f;
		float levelPanelWidth = boxWidth * 1.5f;
		float levelPanelHeight = boxHeight / 3.03f;
		guiComponents.DrawLevelPanel(statusDisplayOpacity * 0.9f, levelPanelX, levelPanelY, levelPanelWidth, levelPanelHeight, true);
					
		float statusPanelX = Screen.width - (boxMargin * 0.2f) - boxWidth * 1.5f;
		float statusPanelY = Screen.height - boxMargin * 1.06f - boxMargin * 0.78f;
		float statusPanelWidth = boxWidth * 1.5f;
		float statusPanelHeight = boxHeight / 2.7f;
		guiComponents.DrawStatusPanel(statusDisplayOpacity * 0.9f, statusPanelX, statusPanelY, statusPanelWidth, statusPanelHeight, true);
		
		// 'exit' button
		GUI.color = new Color(0f, 0f, 0f, 1f * statusDisplayOpacity);
		GUI.Box(new Rect(Screen.width * 0.5f - boxWidth * 0.35f,  Screen.height - (boxMargin * 0.0f) - (boxHeight * 0.15f), boxWidth * 0.7f, boxHeight * 0.15f), "");
		//GUI.color = new Color(0f, 0f, 0f, 0.3f * statusDisplayOpacity);
		//GUI.Box(new Rect(Screen.width * 0.5f - boxWidth * 0.35f,  Screen.height - (boxMargin * 0.0f) - (boxHeight * 0.15f), boxWidth * 0.7f, boxHeight * 0.15f), "");
		GUI.color = new Color(1f, 1f, 1f, 0.8f * statusDisplayOpacity);
		GUI.skin = guiManager.customGUISkin;
		guiManager.customGUISkin.button.fontSize = (int)(boxWidth * 0.0675);
		guiManager.customGUISkin.button.fontStyle = FontStyle.Bold;	
		float exitButtonX = Screen.width * 0.5f - boxWidth * 0.3f;
		float exitButtonY = Screen.height - (boxMargin * 0.0f + boxHeight * 0.03f) - (boxHeight * 0.096f);
		float exitButtonWidth = boxWidth * 0.6f;
		float exitButtonHeight = boxHeight * 0.105f;
		if (GUI.Button(new Rect(exitButtonX,  exitButtonY, exitButtonWidth, exitButtonHeight), "")) {
			guiManager.SetGuiState("guiStateLeavingGameplay");
			//guiManager.SetGuiState("guiStateOverlay");
			levelManager.SetGameState("gameStateLeavingGameplay");
		}
		else if (GUI.Button(new Rect(exitButtonX,  exitButtonY, exitButtonWidth, exitButtonHeight), "")) {
			guiManager.SetGuiState("guiStateLeavingGameplay");
			//guiManager.SetGuiState("guiStateOverlay");
			levelManager.SetGameState("gameStateLeavingGameplay");
		}
		else if (GUI.Button(new Rect(exitButtonX,  exitButtonY, exitButtonWidth, exitButtonHeight), "Main Menu")) {
			guiManager.SetGuiState("guiStateLeavingGameplay");
			//guiManager.SetGuiState("guiStateOverlay");
			levelManager.SetGameState("gameStateLeavingGameplay");
		}
	}
	
	//===================================
	//===================================
	//	  POSITION INDICATORS
	//===================================
	//===================================

	void DrawIndicator(float mainHeading, GameObject pumaObj, GameObject deerObj, string type, int borderThickness, float guiOpacity)
    {
		float xPos = 0;
		float yPos = 0;
		float xOffset = 0;
		float yOffset = 0;
		float rangeX = Screen.width - borderThickness;
		float rangeY = Screen.height - borderThickness;
		float deerToPumaAngle = 0;
		float deerToPumaDistance = 0;

		//------------------------------------------
		// Determine onscreen position of indicator
		//------------------------------------------

		while (mainHeading < 0)
			mainHeading += 360;
		while (mainHeading > 360)
			mainHeading -= 360;
		
		// angle based on midpoint between camera and puma
		float refX = (pumaObj.transform.position.x + Camera.main.transform.position.x) / 2;
		float refY = (pumaObj.transform.position.z + Camera.main.transform.position.z) / 2;

		deerToPumaAngle = guiUtils.GetAngleFromOffset(refX, refY, deerObj.transform.position.x, deerObj.transform.position.z);
		deerToPumaDistance = Vector3.Distance(pumaObj.transform.position, deerObj.transform.position);

		xOffset = -(Mathf.Sin((mainHeading - deerToPumaAngle) * Mathf.PI / 180) * rangeY);
		yOffset = (Mathf.Cos((mainHeading - deerToPumaAngle) * Mathf.PI / 180) * rangeY);
		xOffset *= (rangeX/2) / rangeY; // adjust x offset to fit screen width			
			
		if (yOffset > 0) {
			// stretch out offsets to place deer head along screen edge
			float scaleVal;
			if (Mathf.Abs(xOffset / yOffset) > Mathf.Abs((rangeX/2) / rangeY)) {
				scaleVal = (rangeX/2) / Mathf.Abs(xOffset); // scale by X dimension
			}
			else {
				scaleVal = (rangeY) / Mathf.Abs(yOffset); // scale by Y dimension
			}
			xOffset *= scaleVal;
			yOffset *= scaleVal;
		}
		else {
			// clip y to keep deer head along lower edge
			yOffset = 0;
		}
					
		xPos = (Screen.width - borderThickness) / 2 + xOffset;
		yPos = Screen.height - borderThickness - yOffset;

		//--------------------------------------------
		// Determine settings for distance indication
		//--------------------------------------------

		Color backdropColor;
		float fullRedDistance = 20f;
		float fullYellowDistance = 75f;
		float startYellowDistance = 175f;
		float transPercent;
				
		if (deerToPumaDistance < fullRedDistance) {
			backdropColor = new Color(1f, 0f, 0f, 1f * guiOpacity);
		}
		else if (deerToPumaDistance < fullYellowDistance) {
			transPercent = (fullYellowDistance - deerToPumaDistance) / (fullYellowDistance - fullRedDistance);
			backdropColor = new Color(1f, 0.5f - (transPercent/2), 0f, 1f * guiOpacity);
		}
		else if (deerToPumaDistance < startYellowDistance) {
			transPercent = (startYellowDistance - deerToPumaDistance) / (startYellowDistance - fullYellowDistance);
			backdropColor = new Color(1f, 0.5f, 0f, transPercent * guiOpacity);
		}
		else {
			backdropColor = new Color(0f, 0f, 0f, 0f * guiOpacity);
		}
			
		Color headColor;
		float fullHeadAlphaDistance = 150f;
		float startHeadAlphaDistance = 300f;

		if (deerToPumaDistance < fullHeadAlphaDistance) {
			headColor = new Color(1f, 1f, 1f, 1f * guiOpacity);
		}
		else if (deerToPumaDistance < startHeadAlphaDistance) {
			headColor = new Color(1f, 1f, 1f, (0.5f + ((startHeadAlphaDistance - deerToPumaDistance) / (startHeadAlphaDistance - fullHeadAlphaDistance) * 0.5f)) * guiOpacity);
		}
		else {
			headColor = new Color(1f, 1f, 1f, 0.5f * guiOpacity);
		}

		//--------------------------
		// Draw the indicator
		//--------------------------

		Texture2D headTexture = indicatorBkgnd;

		switch (type) {

		case "Buck":
			headTexture = indicatorBuck;
			break;

		case "Doe":
			headTexture = indicatorDoe;
			break;

		case "Fawn":
			headTexture = indicatorFawn;
			break;
		}

		GUI.color = backdropColor;
		GUI.DrawTexture(new Rect(xPos, yPos, borderThickness, borderThickness), indicatorBkgnd);
		GUI.color = headColor;
		GUI.DrawTexture(new Rect(xPos, yPos, borderThickness, borderThickness), headTexture);
	}

}