using UnityEngine;
using System.Collections;

/// GameplayDisplay
/// Draw the heads up display that overlays on top of gameplay

public class GameplayDisplay : MonoBehaviour
{



    void Start()
    {

	}

	
	
    public void Draw()
    {




/*





		float actualGuiOpacity = guiOpacity;
		float prevGuiOpacity;
		
		if (guiState == "guiStateEnteringGameplay2" || guiState == "guiStateEnteringGameplay3" || guiState == "guiStateEnteringGameplay6" || guiState == "guiStateEnteringGameplay7" || guiState == "guiStateEnteringGameplay8" || guiState == "guiStateCaught6" || guiState == "guiStateCaught7" || guiState == "guiStateCaught8" || guiState == "guiStateCaught9")
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



		prevGuiOpacity = guiOpacity;
		if (guiState == "guiStateEnteringGameplay2" || guiState == "guiStateEnteringGameplay3" || guiState == "guiStateCaught6")
			guiOpacity = actualGuiOpacity;
		else if (guiState == "guiStateEnteringGameplay6" || guiState == "guiStateEnteringGameplay7" || guiState == "guiStateEnteringGameplay8" || guiState == "guiStateCaught7" || guiState == "guiStateCaught8" || guiState == "guiStateCaught9")
			guiOpacity = 1f;



			
			
		
		
		// movement controls tray
		

			
		// lower right paw

		float trayScaleFactor = (8.5f/7f);

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
		inputControls.SetRectTurnRight(new Rect(rightBoxX, rightBoxY, rightBoxWidth, rightBoxHeight));
		
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
		inputControls.SetRectTurnLeft(new Rect(leftBoxX, leftBoxY, leftBoxWidth, leftBoxHeight));







		
		guiOpacity = prevGuiOpacity;


		if (guiState == "guiStateEnteringGameplay2" || guiState == "guiStateEnteringGameplay3" || guiState == "guiStateCaught6")
			guiOpacity = actualGuiOpacity;
		else if (guiState == "guiStateEnteringGameplay6" || guiState == "guiStateEnteringGameplay7" || guiState == "guiStateEnteringGameplay8" || guiState == "guiStateCaught7" || guiState == "guiStateCaught8" || guiState == "guiStateCaught9")
			guiOpacity = 1f;


			
			
			
			
			
		float oldBoxHeight = boxHeight;
		float bottomBoxMargin = boxMargin * 1.05f;
		boxHeight = boxHeight * 0.865f;
		//boxMargin = boxMargin * 1.2f;


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
*/		
		
	}
}