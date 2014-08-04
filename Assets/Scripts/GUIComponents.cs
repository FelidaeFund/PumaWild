using UnityEngine;
using System.Collections;

/// GuiComponents
/// Draws various components used throughout GUI

public class GuiComponents : MonoBehaviour
{
	//===================================
	//===================================
	//		MODULE VARIABLES
	//===================================
	//===================================

	// textures based on bitmap files
	private Texture2D pumaIconTexture;
	private Texture2D pumaIconShadowYellowTexture;
	private Texture2D greenCheckTexture;	
	private Texture2D greenHeartTexture;	
	private Texture2D pumaCrossbonesRedTexture;	
	private Texture2D pumaCrossbonesDarkRedTexture;	
	private Texture2D closeup1Texture;
	private Texture2D closeup2Texture;
	private Texture2D closeup3Texture;
	private Texture2D closeup4Texture;
	private Texture2D closeup5Texture;
	private Texture2D closeup6Texture;
	
	// placeholder for level selector; used only in display
	private int currentLevel = 1;

	// external modules
	private GuiManager guiManager;
	private GuiUtils guiUtils;
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
		guiUtils = GetComponent<GuiUtils>();
		scoringSystem = GetComponent<ScoringSystem>();
		
		// texture references from GuiManager
		pumaIconTexture = guiManager.pumaIconTexture;
		pumaIconShadowYellowTexture = guiManager.pumaIconShadowYellowTexture;	
		greenCheckTexture = guiManager.greenCheckTexture;
		greenHeartTexture = guiManager.greenHeartTexture;
		pumaCrossbonesRedTexture = guiManager.pumaCrossbonesRedTexture;
		pumaCrossbonesDarkRedTexture = guiManager.pumaCrossbonesDarkRedTexture;
		closeup1Texture = guiManager.closeup1Texture;
		closeup2Texture = guiManager.closeup2Texture;
		closeup3Texture = guiManager.closeup3Texture;
		closeup4Texture = guiManager.closeup4Texture;
		closeup5Texture = guiManager.closeup5Texture;
		closeup6Texture = guiManager.closeup6Texture;
	}

	//===================================
	//===================================
	//		DRAW THE LEVEL PANEL
	//===================================
	//===================================

	public void DrawLevelPanel(float levelPanelOpacity, float levelPanelX, float levelPanelY, float levelPanelWidth, float levelPanelHeight, bool bareBonesFlag = false) 
	{ 
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		
		if (bareBonesFlag == false) {
			GUI.color = new Color(1f, 1f, 1f, 0.8f * levelPanelOpacity);
			GUI.Box(new Rect(levelPanelX, levelPanelY, levelPanelWidth, levelPanelHeight), "");
			GUI.color = new Color(1f, 1f, 1f, 0.2f * levelPanelOpacity);
			GUI.Box(new Rect(levelPanelX, levelPanelY, levelPanelWidth, levelPanelHeight), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * levelPanelOpacity);
			//guiUtils.DrawRect(new Rect(levelPanelX + levelPanelWidth * 0.33f, levelPanelY, levelPanelWidth * 0.005f, levelPanelHeight), new Color(0f, 0f, 0f, 0.2f));	
			//guiUtils.DrawRect(new Rect(levelPanelX + levelPanelWidth * 0.335f, levelPanelY, levelPanelWidth * 0.005f, levelPanelHeight), new Color(1f, 1f, 1f, 0.2f));	
		}
		
		float fontRef = levelPanelWidth * 1000f / 320f;
		string levelNumber = "0:";
		string levelLabel = "unknown";
		float textureX;
		float textureY;
		float textureWidth;
		float textureHeight;

		// level icon

		if (bareBonesFlag == false) {
		
			if (currentLevel == 1) {
				// Natural Wilderness
				textureX = levelPanelX + levelPanelWidth * 0.74f;
				textureY = levelPanelY + levelPanelHeight * 0.05f;
				textureWidth = levelPanelWidth * 0.20f;
				textureHeight = guiManager.buckHeadTexture.height * (textureWidth / guiManager.buckHeadTexture.width);
				GUI.color = new Color(1f, 1f, 1f, 0.9f * levelPanelOpacity);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), guiManager.buckHeadTexture);
				levelNumber = "1:";
				levelLabel = "Natural Wilderness";
				style.normal.textColor = new Color(0.99f, 0.70f, 0f, 0.96f);
			}

			if (currentLevel == 2) {
				// Human Presence
				textureX = levelPanelX + levelPanelWidth * 0.80f;
				textureY = levelPanelY + levelPanelHeight * 0.12f;
				textureWidth = levelPanelWidth * 0.15f;
				textureHeight = guiManager.hunterTexture.height * (textureWidth / guiManager.hunterTexture.width);
				GUI.color = new Color(1f, 1f, 1f, 0.94f * levelPanelOpacity);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), guiManager.hunterTexture);
				levelNumber = "2:";
				levelLabel = "Human Presence";
				style.normal.textColor = new Color(0.99f, 0.66f, 0f, 0.97f);
			}

			if (currentLevel == 3) {
				// Human Expansion
				textureX = levelPanelX + levelPanelWidth * 0.75f;
				textureY = levelPanelY + levelPanelHeight * 0.26f;
				textureWidth = levelPanelWidth * 0.21f;
				textureHeight = guiManager.vehicleTexture.height * (textureWidth / guiManager.vehicleTexture.width);
				GUI.color = new Color(1f, 1f, 1f, 0.93f * levelPanelOpacity);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), guiManager.vehicleTexture);
				levelNumber = "3:";
				levelLabel = "Human Expansion";
				style.normal.textColor = new Color(0.99f, 0.64f, 0f, 0.98f);
			}

			if (currentLevel == 4) {
				// Fragmented Habitat
				textureX = levelPanelX + levelPanelWidth * 0.75f;
				textureY = levelPanelY + levelPanelHeight * 0.26f;
				textureWidth = levelPanelWidth * 0.21f;
				textureHeight = guiManager.highwayTexture.height * (textureWidth / guiManager.highwayTexture.width);
				GUI.color = new Color(1f, 1f, 1f, 0.93f * levelPanelOpacity);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), guiManager.highwayTexture);
				levelNumber = "4:";
				levelLabel = "Fragmented Habitat";
				style.normal.textColor = new Color(0.99f, 0.62f, 0f, 0.99f);
			}

			if (currentLevel == 5) {
				// Connected Habitat
				textureX = levelPanelX + levelPanelWidth * 0.75f;
				textureY = levelPanelY + levelPanelHeight * 0.26f;
				textureWidth = levelPanelWidth * 0.21f;
				textureHeight = guiManager.overpassTexture.height * (textureWidth / guiManager.overpassTexture.width);
				GUI.color = new Color(1f, 1f, 1f, 0.93f * levelPanelOpacity);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), guiManager.overpassTexture);
				levelNumber = "5:";
				levelLabel = "Connected Habitat";
				style.normal.textColor = new Color(0.99f, 0.70f, 0f, 1f);
			}

		}
			
		// level label
		
		//style.fontSize = (int)(fontRef * 0.020f);
		//style.normal.textColor = new Color(0f, 0.25f, 0f, 1f);
		//style.normal.textColor = new Color(0.99f, 0.63f, 0f, 0.95f);
		//style.normal.textColor = new Color(0.99f, 0.7f, 0f, 0.95f);
		//style.fontStyle = FontStyle.Bold;
		//style.fontStyle = FontStyle.BoldAndItalic;
		//GUI.Button(new Rect(levelPanelX - levelPanelWidth * 0.053f, levelPanelY + levelPanelHeight * 0.29f, levelPanelWidth * 0.3f, levelPanelHeight * 0.03f), (currentLevel == 0) ? " " : ((currentLevel == 1) ? " " : " "), style);
		if (bareBonesFlag == false) {
			style.fontSize = (int)(fontRef * 0.0185f);
			//style.normal.textColor = new Color(0.99f, 0.7f, 0f, 0.92f);
			style.fontStyle = FontStyle.BoldAndItalic;
			style.alignment = TextAnchor.MiddleLeft;
			GUI.Button(new Rect(levelPanelX + levelPanelWidth * 0.09f, levelPanelY + levelPanelHeight * 0.26f, levelPanelWidth * 0.64f, levelPanelHeight * 0.03f), levelLabel, style);
			//style.normal.textColor = new Color(0f, 0f, 0f, 0.5f);
			//style.fontSize = (int)(fontRef * 0.02f);
			//GUI.Button(new Rect(levelPanelX + levelPanelWidth * 0.09f, levelPanelY + levelPanelHeight * 0.26f, levelPanelWidth * 0.64f, levelPanelHeight * 0.03f), levelNumber, style);
			style.alignment = TextAnchor.MiddleCenter;
		}

		// meat bar
		if (bareBonesFlag == true) {
			GUI.color = new Color(1f, 1f, 1f, 0.5f * levelPanelOpacity);
			GUI.Box(new Rect(levelPanelX + levelPanelWidth * 0.07f, levelPanelY + levelPanelHeight * 0.525f, levelPanelWidth * 0.64f, levelPanelHeight * 0.35f), "");
		}
		DrawMeatBar(levelPanelOpacity, levelPanelX + levelPanelWidth * 0.07f, levelPanelY + levelPanelHeight * 0.525f, levelPanelWidth * 0.64f, levelPanelHeight * 0.35f);


		// invisible button
		if (GUI.Button(new Rect(levelPanelX + levelPanelWidth * 0.7f, levelPanelY, levelPanelWidth * 0.3f, levelPanelHeight), "", style)) {
			currentLevel += 1;
			if (currentLevel > 5)
				currentLevel = 1;
		}
		//GUI.Box(new Rect(levelPanelX + levelPanelWidth * 0.7f, levelPanelY, levelPanelWidth * 0.3f, levelPanelHeight), "");



	}

	//===================================
	//===================================
	//		DRAW THE STATUS PANEL
	//===================================
	//===================================
	
	public void DrawStatusPanel(float statusPanelOpacity, float statusPanelX, float statusPanelY, float statusPanelWidth, float statusPanelHeight, bool bareBonesFlag = false) 
	{ 
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		
		if (bareBonesFlag == false) {
			GUI.color = new Color(1f, 1f, 1f, 0.8f * statusPanelOpacity);
			GUI.Box(new Rect(statusPanelX, statusPanelY, statusPanelWidth, statusPanelHeight), "");
			GUI.color = new Color(1f, 1f, 1f, 0.4f * statusPanelOpacity);
			GUI.Box(new Rect(statusPanelX, statusPanelY, statusPanelWidth, statusPanelHeight), "");
			GUI.color = new Color(1f, 1f, 1f, 1f * statusPanelOpacity);
			//guiUtils.DrawRect(new Rect(statusPanelX + statusPanelWidth * 0.33f, statusPanelY, statusPanelWidth * 0.005f, statusPanelHeight), new Color(0f, 0f, 0f, 0.2f));	
			//guiUtils.DrawRect(new Rect(statusPanelX + statusPanelWidth * 0.335f, statusPanelY, statusPanelWidth * 0.005f, statusPanelHeight), new Color(1f, 1f, 1f, 0.2f));	
			//guiUtils.DrawRect(new Rect(statusPanelX + statusPanelWidth * 0f, statusPanelY + statusPanelHeight * 0.50f, statusPanelWidth * 1f, statusPanelWidth * 0.005f), new Color(0f, 0f, 0f, 0.3f));	
			//guiUtils.DrawRect(new Rect(statusPanelX + statusPanelWidth * 0f, statusPanelY + statusPanelHeight * 0.50f + statusPanelWidth * 0.005f, statusPanelWidth * 1f, statusPanelWidth * 0.005f), new Color(1f, 1f, 1f, 0.2f));	
		}
		
		float fontRef = statusPanelWidth * 1000f / 320f;
				
		float textureX;
		float textureY;
		float textureHeight;
		float textureWidth;

		if (guiManager.selectedPuma != -1) {

			// puma identity
			Texture2D headshotTexture = closeup1Texture;
			string pumaName = "no name";
			switch (guiManager.selectedPuma) {
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

			GUI.color = new Color(1f, 1f, 1f, 1f * statusPanelOpacity);

			// puma head
			if (bareBonesFlag == false) {
				textureX = statusPanelX + statusPanelWidth * 0.05f;
				textureY = statusPanelY + statusPanelHeight * 0.20f;
				textureHeight = statusPanelHeight * 0.62f;
				textureWidth = headshotTexture.width * (textureHeight / headshotTexture.height);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), headshotTexture);
			}

			// puma name
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

		}
		else {
			// no puma selected
			style.normal.textColor = new Color(0.99f, 0.7f, 0f, 0.92f);
			style.normal.textColor = new Color(0.99f, 0.62f, 0f, 0.95f);
			style.normal.textColor = new Color(0.99f, 0.66f, 0f, 0.935f);
			style.fontSize = (int)(fontRef * 0.016f);
			style.alignment = TextAnchor.UpperCenter;
			GUI.Button(new Rect(statusPanelX + statusPanelWidth * 0.045f, statusPanelY + statusPanelHeight * 0.2f, statusPanelWidth * 0.2f, statusPanelWidth * 0.3f), "No", style);
			GUI.Button(new Rect(statusPanelX + statusPanelWidth * 0.045f, statusPanelY + statusPanelHeight * 0.4f, statusPanelWidth * 0.2f, statusPanelWidth * 0.3f), "Puma", style);
			GUI.Button(new Rect(statusPanelX + statusPanelWidth * 0.045f, statusPanelY + statusPanelHeight * 0.6f, statusPanelWidth * 0.2f, statusPanelWidth * 0.3f), "Selected", style);
			style.alignment = TextAnchor.MiddleCenter;
		
		}

		// six puma icons
		
		if (bareBonesFlag == false) {
		
			Color pumaAliveColor = new Color(1f, 1f, 1f, 0.9f * statusPanelOpacity);
			//Color pumaFullHealthColor = new Color(0.32f, 0.99f, 0f, 0.99f * statusPanelOpacity);
			Color pumaFullHealthColor = new Color(0.84f, 0.99f, 0.0f, 0.72f * statusPanelOpacity);
			//Color pumaDeadColor = new Color(0.76f, 0.0f, 0f, 0.47f * statusPanelOpacity);
			Color pumaDeadColor = new Color(0.1f, 0.1f, 0.1f, 0.6f * statusPanelOpacity);


			textureX = statusPanelX + statusPanelWidth * 0.280f;
			textureY = statusPanelY + statusPanelHeight * 0.1f;
			textureWidth = statusPanelWidth * 0.12f;
			textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
			if (scoringSystem.GetPumaHealth(0) >= 1f) {
				GUI.color = new Color(1f, 1f, 1f, 0.7f * statusPanelOpacity);
				GUI.DrawTexture(new Rect(textureX - (textureWidth * 0.1f), textureY - (textureHeight * 0.11f), textureWidth * 1.2f, textureHeight * 1.2f), pumaIconShadowYellowTexture);
				//GUI.color = new Color(1f, 1f, 1f, 1f * statusPanelOpacity);
				//GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}
			//else {
				GUI.color = (scoringSystem.GetPumaHealth(0) <= 0f) ? pumaDeadColor : ((scoringSystem.GetPumaHealth(0) >= 1f) ? pumaFullHealthColor : pumaAliveColor);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			//}
			
			textureX = statusPanelX + statusPanelWidth * 0.388f;
			textureY = statusPanelY + statusPanelHeight * 0.1f;
			textureWidth = statusPanelWidth * 0.12f;
			textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
			if (scoringSystem.GetPumaHealth(1) >= 1f) {
				GUI.color = new Color(1f, 1f, 1f, 0.7f * statusPanelOpacity);
				GUI.DrawTexture(new Rect(textureX - (textureWidth * 0.1f), textureY - (textureHeight * 0.11f), textureWidth * 1.2f, textureHeight * 1.2f), pumaIconShadowYellowTexture);
				//GUI.color = new Color(1f, 1f, 1f, 1f * statusPanelOpacity);
				//GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}
			//else {
				GUI.color = (scoringSystem.GetPumaHealth(1) <= 0f) ? pumaDeadColor : ((scoringSystem.GetPumaHealth(1) >= 1f) ? pumaFullHealthColor : pumaAliveColor);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			//}

			textureX = statusPanelX + statusPanelWidth * 0.496f;
			textureY = statusPanelY + statusPanelHeight * 0.1f;
			textureWidth = statusPanelWidth * 0.12f;
			textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
			if (scoringSystem.GetPumaHealth(2) >= 1f) {
				GUI.color = new Color(1f, 1f, 1f, 0.7f * statusPanelOpacity);
				GUI.DrawTexture(new Rect(textureX - (textureWidth * 0.1f), textureY - (textureHeight * 0.11f), textureWidth * 1.2f, textureHeight * 1.2f), pumaIconShadowYellowTexture);
				//GUI.color = new Color(1f, 1f, 1f, 1f * statusPanelOpacity);
				//GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}
			//else {
				GUI.color = (scoringSystem.GetPumaHealth(2) <= 0f) ? pumaDeadColor : ((scoringSystem.GetPumaHealth(2) >= 1f) ? pumaFullHealthColor : pumaAliveColor);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			//}

			textureX = statusPanelX + statusPanelWidth * 0.604f;
			textureY = statusPanelY + statusPanelHeight * 0.1f;
			textureWidth = statusPanelWidth * 0.12f;
			textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
			if (scoringSystem.GetPumaHealth(3) >= 1f) {
				GUI.color = new Color(1f, 1f, 1f, 0.7f * statusPanelOpacity);
				GUI.DrawTexture(new Rect(textureX - (textureWidth * 0.1f), textureY - (textureHeight * 0.11f), textureWidth * 1.2f, textureHeight * 1.2f), pumaIconShadowYellowTexture);
				//GUI.color = new Color(1f, 1f, 1f, 1f * statusPanelOpacity);
				//GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}
			//else {
				GUI.color = (scoringSystem.GetPumaHealth(3) <= 0f) ? pumaDeadColor : ((scoringSystem.GetPumaHealth(3) >= 1f) ? pumaFullHealthColor : pumaAliveColor);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			//}

			textureX = statusPanelX + statusPanelWidth * 0.712f;
			textureY = statusPanelY + statusPanelHeight * 0.1f;
			textureWidth = statusPanelWidth * 0.12f;
			textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
			if (scoringSystem.GetPumaHealth(4) >= 1f) {
				GUI.color = new Color(1f, 1f, 1f, 0.7f * statusPanelOpacity);
				GUI.DrawTexture(new Rect(textureX - (textureWidth * 0.1f), textureY - (textureHeight * 0.11f), textureWidth * 1.2f, textureHeight * 1.2f), pumaIconShadowYellowTexture);
				//GUI.color = new Color(1f, 1f, 1f, 1f * statusPanelOpacity);
				//GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}
			//else {
				GUI.color = (scoringSystem.GetPumaHealth(4) <= 0f) ? pumaDeadColor : ((scoringSystem.GetPumaHealth(4) >= 1f) ? pumaFullHealthColor : pumaAliveColor);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			//}

			textureX = statusPanelX + statusPanelWidth * 0.820f;
			textureY = statusPanelY + statusPanelHeight * 0.1f;
			textureWidth = statusPanelWidth * 0.12f;
			textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
			if (scoringSystem.GetPumaHealth(0) >= 1f) {
				GUI.color = new Color(1f, 1f, 1f, 0.7f * statusPanelOpacity);
				GUI.DrawTexture(new Rect(textureX - (textureWidth * 0.1f), textureY - (textureHeight * 0.11f), textureWidth * 1.2f, textureHeight * 1.2f), pumaIconShadowYellowTexture);
				//GUI.color = new Color(1f, 1f, 1f, 1f * statusPanelOpacity);
				//GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}
			//else {
				GUI.color = (scoringSystem.GetPumaHealth(5) <= 0f) ? pumaDeadColor : ((scoringSystem.GetPumaHealth(5) >= 1f) ? pumaFullHealthColor : pumaAliveColor);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			//}
		}
		
		// health bar
		if (bareBonesFlag == true) {
			GUI.color = new Color(1f, 1f, 1f, 0.5f * statusPanelOpacity);
			GUI.Box(new Rect(statusPanelX + statusPanelWidth * 0.29f, statusPanelY + statusPanelHeight * 0.59f, statusPanelWidth * 0.64f, statusPanelHeight * 0.3f), "");
		}
		DrawPumaHealthBar(guiManager.selectedPuma, statusPanelOpacity, statusPanelX + statusPanelWidth * 0.29f, statusPanelY + statusPanelHeight * 0.59f, statusPanelWidth * 0.64f, statusPanelHeight * 0.3f);

	}

	//===================================
	//===================================
	//		DRAW THE MEAT BAR
	//===================================
	//===================================

	public void DrawMeatBar(float meatBarOpacity, float meatBarX, float meatBarY, float meatBarWidth, float meatBarHeight) 
	{ 
		float meatLevel = scoringSystem.GetMeatLevel();
		if (meatLevel > 1f)
			meatLevel = 1f;
			
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		
		// panel background
		GUI.color = new Color(1f, 1f, 1f, 0.8f * meatBarOpacity);
		GUI.Box(new Rect(meatBarX, meatBarY, meatBarWidth, meatBarHeight), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * meatBarOpacity);
	
		// meter label
		float fontRef = meatBarHeight * 2f;
		style.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);
		style.fontSize = (int)(fontRef * 0.22f);
		style.fontStyle = FontStyle.Bold;
		GUI.Button(new Rect(meatBarX + meatBarWidth * 0.058f, meatBarY + meatBarHeight * 0.46f, meatBarWidth * 0.1f, meatBarHeight * 0.03f), "MEAT", style);
		style.fontSize = (int)(fontRef * 0.2f);
		//GUI.Button(new Rect(meatBarX + meatBarWidth * 0.055f, meatBarY + meatBarHeight * 0.65f, meatBarWidth * 0.1f, meatBarHeight * 0.03f), "Eaten", style);


		// feeding meter
		float meterLeft = 0.2075f;
		float meterRight = 0.2075f;
		float meterTop = 0.27f;		
		GUI.color = new Color(1f, 1f, 1f, 1f * meatBarOpacity);
		GUI.Box(new Rect(meatBarX + meatBarWidth * meterLeft, meatBarY + meatBarHeight * meterTop, meatBarWidth - meatBarWidth * (meterLeft + meterRight), meatBarHeight - meatBarHeight * meterTop * 2), "");
		GUI.Box(new Rect(meatBarX + meatBarWidth * meterLeft, meatBarY + meatBarHeight * meterTop, meatBarWidth - meatBarWidth * (meterLeft + meterRight), meatBarHeight - meatBarHeight * meterTop * 2), "");

		meterLeft += 0.017f;
		meterRight += 0.017f;
		meterTop += 0.12f;		

		float meterWidth = meatBarWidth - meatBarWidth * (meterLeft + meterRight);
		float meterStatWidth = meterWidth * (meatLevel >= 0.1f ? 0.24f : 0.18f);
		Color feedingColor = new Color(0.99f, 0.6f, 0f, 1f);
		guiUtils.DrawRect(new Rect(meatBarX + meatBarWidth * meterLeft, meatBarY + meatBarHeight * meterTop, meatBarWidth - meatBarWidth * (meterLeft + meterRight), meatBarHeight - meatBarHeight * meterTop * 2), new Color(0.47f, 0.5f, 0.45f, 0.5f));	
		guiUtils.DrawRect(new Rect(meatBarX + meatBarWidth * meterLeft, meatBarY + meatBarHeight * meterTop, ((meatBarWidth - meatBarWidth * (meterLeft + meterRight)) - meterStatWidth) * meatLevel, meatBarHeight - meatBarHeight * meterTop * 2), feedingColor);			


		if (meatLevel > 0f) {
			// current value display
			meterTop -= 0.12f;		
			float meterX = meatBarX + meatBarWidth * meterLeft;
			float meterY = meatBarY + meatBarHeight * meterTop;
			float meterHeight = meatBarHeight - meatBarHeight * meterTop * 2;
			int meatLevelInt = (int)(meatLevel * 1000f);
			float meterStatX = meterX + (meterWidth - meterStatWidth) * meatLevel;
			GUI.Box(new Rect(meterStatX, meterY - meterHeight * 0.15f, meterStatWidth, meterHeight + meterHeight * 0.3f), "");
			//guiUtils.DrawRect(new Rect(meterStatX, meterY - meterHeight * 0.15f, meterStatWidth, meterHeight + meterHeight * 0.3f), new Color(0f, 0f, 0f, 1f));	
			string displayString = meatLevelInt.ToString();
			style.fontSize = (int)(fontRef * 0.235f);
			style.alignment = TextAnchor.MiddleCenter;
			style.normal.textColor = feedingColor;
			style.fontStyle = FontStyle.Bold;
			GUI.Button(new Rect(meterStatX - (meterStatWidth * (meatLevel == 1f ? -0.0f : (meatLevel >= 0.1f ? 0.06f : 0.046f))), meterY, meterStatWidth, meterHeight), displayString, style);
		}


		// meter label 2

		float textureX = meatBarX + meatBarWidth * 0.91f;
		float textureY = meatBarY + meatBarHeight * 0.1f;
		float textureWidth = meatBarHeight * 0.67f;
		float textureHeight = greenCheckTexture.height * (textureWidth / greenCheckTexture.width);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), greenCheckTexture);


		style.normal.textColor = new Color(0f, 0.70f, 0f, 1f);
		style.fontSize = (int)(fontRef * 0.2f);
		style.fontStyle = FontStyle.BoldAndItalic;
		GUI.Button(new Rect(meatBarX + meatBarWidth * 0.8f, meatBarY + meatBarHeight * 0.30f, meatBarWidth * 0.1f, meatBarHeight * 0.03f), "1000", style);
		style.fontSize = (int)(fontRef * 0.18f);
		GUI.Button(new Rect(meatBarX + meatBarWidth * 0.805f, meatBarY + meatBarHeight * 0.65f, meatBarWidth * 0.1f, meatBarHeight * 0.03f), "Lbs", style);


	}

	//===================================
	//===================================
	//	   DRAW THE PUMA HEALTH BAR
	//===================================
	//===================================

	public void DrawPumaHealthBar(int pumaNum, float healthBarOpacity, float healthBarX, float healthBarY, float healthBarWidth, float healthBarHeight, bool hideStatFlag = false, bool shiftStatFlag = false) 
	{ 
		float health = scoringSystem.GetPumaHealth(pumaNum); 
		float textureX;
		float textureY;
		float textureWidth;
		float textureHeight;

		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		
		// panel background
		GUI.color = new Color(1f, 1f, 1f, 0.8f * healthBarOpacity);
		GUI.Box(new Rect(healthBarX, healthBarY, healthBarWidth, healthBarHeight), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * healthBarOpacity);
	
		// puma crossbones
		if (health < 1f) {
			Texture2D crossboneTexture = (health > 0.66f || health < 0f) ? pumaCrossbonesDarkRedTexture : (health > 0.33 ? pumaCrossbonesDarkRedTexture : pumaCrossbonesRedTexture);
			textureX = healthBarX + healthBarWidth * 0.053f;
			textureY = healthBarY + healthBarHeight * 0.15f;
			textureWidth = healthBarHeight * .8f;
			textureHeight = crossboneTexture.height * (textureWidth / crossboneTexture.width) * 1f;
			GUI.color = new Color(1f, 1f, 1f, ((health > 0.66f || health < 0f) ? 0.9f : (health > 0.33f ? 0.975f : 1f)) * healthBarOpacity);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), crossboneTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * healthBarOpacity);
		}

		// health meter

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
		GUI.color = new Color(1f, 1f, 1f, 1f * healthBarOpacity);
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
			guiUtils.DrawRect(new Rect(meterX, meterY, meterWidth, meterHeight), new Color(0.47f, 0.5f, 0.45f, 0.5f));	
			guiUtils.DrawRect(new Rect(meterX, meterY, (meterWidth - meterStatWidth) * health, meterHeight), barColor);		
		}
		else {
			guiUtils.DrawRect(new Rect(meterX, meterY, meterWidth, meterHeight), new Color(0.47f, 0.5f, 0.45f, 0.25f));	
		}

		if (hideStatFlag == false && health >= 0f) {
			// display current value 
			meterTop -= 0.12f;		
			meterY = healthBarY + healthBarHeight * meterTop;
			meterHeight = healthBarHeight - healthBarHeight * meterTop * 2;
			int healthPercent = (int)(health * 100f);
			float meterStatX = meterX + (meterWidth - meterStatWidth) * health;
			GUI.Box(new Rect(meterStatX, meterY - meterHeight * 0.3f, meterStatWidth, meterHeight + meterHeight * 0.6f), "");
			//guiUtils.DrawRect(new Rect(meterStatX, meterY - meterHeight * 0.3f, meterStatWidth, meterHeight + meterHeight * 0.6f), new Color(0f, 0f, 0f, 1f));	
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
			//guiUtils.DrawRect(new Rect(meterStatX, meterY - meterHeight * 0.3f, meterStatWidth, meterHeight + meterHeight * 0.6f), new Color(0f, 0f, 0f, 1f));	

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
				GUI.color = new Color(1f, 1f, 1f, 0.75f * healthBarOpacity);
				GUI.DrawTexture(new Rect(checkMarkX, checkMarkY, checkMarkWidth, checkMarkHeight), greenCheckTexture);
				GUI.color = new Color(1f, 1f, 1f, 1f * healthBarOpacity);
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
			GUI.color = new Color(1f, 1f, 1f, (health > 0.66f ? 0.8f : (health > 0.33f ? 0.68f : 0.5f)) * healthBarOpacity);
			if (health >= 1f)
				GUI.color = new Color(1f, 1f, 1f, 0.9f * healthBarOpacity);
			GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), greenHeartTexture);
			GUI.color = new Color(1f, 1f, 1f, 1f * healthBarOpacity);
		}
	}
	
	//===================================
	//===================================
	//	DRAW THE POPULATION HEALTH BAR
	//===================================
	//===================================

	public void DrawPopulationHealthBar(float healthBarOpacity, float healthBarX, float healthBarY, float healthBarWidth, float healthBarHeight, bool showPumaIcons, bool centerLabels) 
	{ 
		float health = 0; 
		health += scoringSystem.GetPumaHealth(0); 
		health += scoringSystem.GetPumaHealth(1); 
		health += scoringSystem.GetPumaHealth(2); 
		health += scoringSystem.GetPumaHealth(3); 
		health += scoringSystem.GetPumaHealth(4); 
		health += scoringSystem.GetPumaHealth(5); 
		health /= 6;
		
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;

		// panel background
		GUI.color = new Color(1f, 1f, 1f, 0.8f * healthBarOpacity);
		GUI.Box(new Rect(healthBarX, healthBarY, healthBarWidth, healthBarHeight), "");
		GUI.Box(new Rect(healthBarX, healthBarY, healthBarWidth, healthBarHeight), "");
		GUI.color = new Color(1f, 1f, 1f, 1f * healthBarOpacity);
	

		float textureX;
		float textureY;
		float textureWidth;
		float textureHeight;


		if (showPumaIcons == true) {

			// six puma icons
			
			float pumaIconWidth = healthBarHeight * 0.85f;
			float pumaIncrementX = healthBarWidth * 0.038f;
			float pumaIconY = healthBarY - healthBarHeight * 1.32f;
			
			if (centerLabels == true)
				pumaIconY += healthBarHeight * 0.4f;
			
			//Color pumaAliveColor = new Color(1f, 1f, 1f, 0.85f * healthBarOpacity);
			//Color pumaDeadColor = new Color(0.5f, 0.05f, 0f, 0.9f * healthBarOpacity);
		

			Color pumaAliveColor = new Color(1f, 1f, 1f, 0.9f * healthBarOpacity);
			Color pumaFullHealthColor = new Color(0.32f, 0.99f, 0f, 0.9f * healthBarOpacity);
			Color pumaDeadColor = new Color(0.6f, 0.05f, 0f, 0.8f * healthBarOpacity);


			textureX = healthBarX + healthBarWidth * 0.383f;
			textureY = pumaIconY;
			textureWidth = pumaIconWidth;
			textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
			if (guiManager.selectedPuma == -1) {
				GUI.color = new Color(1f, 1f, 1f, 0.8f * healthBarOpacity);
				GUI.DrawTexture(new Rect(textureX - (textureWidth * 0.1f), textureY - (textureHeight * 0.11f), textureWidth * 1.2f, textureHeight * 1.2f), pumaIconShadowYellowTexture);
				GUI.color = new Color(1f, 1f, 1f, 1f * healthBarOpacity);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}
			else {
				GUI.color = (scoringSystem.GetPumaHealth(0) <= 0f) ? pumaDeadColor : ((scoringSystem.GetPumaHealth(0) >= 1f) ? pumaFullHealthColor : pumaAliveColor);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}
			
			textureX += pumaIncrementX;
			textureY = pumaIconY;
			textureWidth = pumaIconWidth;
			textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
			if (guiManager.selectedPuma == -1) {
				GUI.color = new Color(1f, 1f, 1f, 0.8f * healthBarOpacity);
				GUI.DrawTexture(new Rect(textureX - (textureWidth * 0.1f), textureY - (textureHeight * 0.11f), textureWidth * 1.2f, textureHeight * 1.2f), pumaIconShadowYellowTexture);
				GUI.color = new Color(1f, 1f, 1f, 1f * healthBarOpacity);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}
			else {
				GUI.color = (scoringSystem.GetPumaHealth(1) <= 0f) ? pumaDeadColor : ((scoringSystem.GetPumaHealth(1) >= 1f) ? pumaFullHealthColor : pumaAliveColor);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}

			textureX += pumaIncrementX;
			textureY = pumaIconY;
			textureWidth = pumaIconWidth;
			textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
			if (guiManager.selectedPuma == -1) {
				GUI.color = new Color(1f, 1f, 1f, 0.8f * healthBarOpacity);
				GUI.DrawTexture(new Rect(textureX - (textureWidth * 0.1f), textureY - (textureHeight * 0.11f), textureWidth * 1.2f, textureHeight * 1.2f), pumaIconShadowYellowTexture);
				GUI.color = new Color(1f, 1f, 1f, 1f * healthBarOpacity);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}
			else {
				GUI.color = (scoringSystem.GetPumaHealth(2) <= 0f) ? pumaDeadColor : ((scoringSystem.GetPumaHealth(2) >= 1f) ? pumaFullHealthColor : pumaAliveColor);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}

			textureX += pumaIncrementX;
			textureY = pumaIconY;
			textureWidth = pumaIconWidth;
			textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
			if (guiManager.selectedPuma == -1) {
				GUI.color = new Color(1f, 1f, 1f, 0.8f * healthBarOpacity);
				GUI.DrawTexture(new Rect(textureX - (textureWidth * 0.1f), textureY - (textureHeight * 0.11f), textureWidth * 1.2f, textureHeight * 1.2f), pumaIconShadowYellowTexture);
				GUI.color = new Color(1f, 1f, 1f, 1f * healthBarOpacity);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}
			else {
				GUI.color = (scoringSystem.GetPumaHealth(3) <= 0f) ? pumaDeadColor : ((scoringSystem.GetPumaHealth(3) >= 1f) ? pumaFullHealthColor : pumaAliveColor);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}

			textureX += pumaIncrementX;
			textureY = pumaIconY;
			textureWidth = pumaIconWidth;
			textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
			if (guiManager.selectedPuma == -1) {
				GUI.color = new Color(1f, 1f, 1f, 0.8f * healthBarOpacity);
				GUI.DrawTexture(new Rect(textureX - (textureWidth * 0.1f), textureY - (textureHeight * 0.11f), textureWidth * 1.2f, textureHeight * 1.2f), pumaIconShadowYellowTexture);
				GUI.color = new Color(1f, 1f, 1f, 1f * healthBarOpacity);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}
			else {
				GUI.color = (scoringSystem.GetPumaHealth(4) <= 0f) ? pumaDeadColor : ((scoringSystem.GetPumaHealth(4) >= 1f) ? pumaFullHealthColor : pumaAliveColor);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}

			textureX += pumaIncrementX;
			textureY = pumaIconY;
			textureWidth = pumaIconWidth;
			textureHeight = pumaIconTexture.height * (textureWidth / pumaIconTexture.width);
			if (guiManager.selectedPuma == -1) {
				GUI.color = new Color(1f, 1f, 1f, 0.8f * healthBarOpacity);
				GUI.DrawTexture(new Rect(textureX - (textureWidth * 0.1f), textureY - (textureHeight * 0.11f), textureWidth * 1.2f, textureHeight * 1.2f), pumaIconShadowYellowTexture);
				GUI.color = new Color(1f, 1f, 1f, 1f * healthBarOpacity);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}
			else {
				GUI.color = (scoringSystem.GetPumaHealth(5) <= 0f) ? pumaDeadColor : ((scoringSystem.GetPumaHealth(5) >= 1f) ? pumaFullHealthColor : pumaAliveColor);
				GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), pumaIconTexture);
			}
		}

		GUI.color = new Color(1f, 1f, 1f, 1f * healthBarOpacity);

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
			float labelAdjustY = 0f;
			if (centerLabels == true)
				labelAdjustY = healthBarHeight * 0.4f;		
			style.fontStyle = FontStyle.Bold;
			style.alignment = TextAnchor.MiddleLeft;
			style.fontSize = (int)(fontRef * 0.20f);
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			GUI.Button(new Rect(xOffset + healthBarX + healthBarWidth * 0.383f, healthBarY - healthBarHeight * 1.85f + labelAdjustY, healthBarWidth * 0.3f, healthBarHeight * 0.03f), "POPULATION:", style);
			style.fontSize = (int)(fontRef * 0.24f);
			style.normal.textColor = labelColor;
			GUI.Button(new Rect(xOffset + healthBarX + healthBarWidth * 0.505f, healthBarY - healthBarHeight * 1.85f + labelAdjustY, healthBarWidth * 0.3f, healthBarHeight * 0.03f), displayString, style);
		}
		else if (centerLabels == false) {
			// labels go to sides of bar
			style.fontStyle = FontStyle.Bold;
			style.alignment = TextAnchor.MiddleLeft;
			style.fontSize = (int)(fontRef * 0.21f);
			style.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			GUI.Button(new Rect(xOffset2 + healthBarX - healthBarWidth * 0.15f, healthBarY + healthBarHeight * 0.01f, healthBarWidth * 0.3f, healthBarHeight * 1f), "POPULATION", style);
			style.fontSize = (int)(fontRef * 0.26f);
			style.normal.textColor = labelColor;
			GUI.Button(new Rect(xOffset2 + healthBarX + healthBarWidth * 1.027f, healthBarY - healthBarHeight * 0.0255f, healthBarWidth * 0.3f, healthBarHeight * 1f), displayString, style);
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
		GUI.color = new Color(1f, 1f, 1f, ((health > 0.66f || health < 0f) ? 0.9f : (health > 0.33f ? 0.975f : 1f)) * healthBarOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), crossboneTexture);
		GUI.color = new Color(1f, 1f, 1f, 1f * healthBarOpacity);


		// health meter
		float meterLeft = 0.065f;
		float meterRight = 0.065f;
		float meterTop = 0.287f;		
		float meterX = healthBarX + healthBarWidth * meterLeft;
		float meterWidth = healthBarWidth - healthBarWidth * (meterLeft + meterRight);
		float meterY = healthBarY + healthBarHeight * meterTop;
		float meterHeight = healthBarHeight - healthBarHeight * meterTop * 2;
		GUI.color = new Color(1f, 1f, 1f, 1f * healthBarOpacity);
		GUI.Box(new Rect(meterX, meterY, meterWidth, meterHeight), "");

		meterLeft += 0.007f;
		meterRight += 0.007f;
		meterTop += 0.12f;		
		meterX = healthBarX + healthBarWidth * meterLeft;
		meterWidth = healthBarWidth - healthBarWidth * (meterLeft + meterRight);
		float meterStatWidth = meterWidth * 0.065f;
		meterY = healthBarY + healthBarHeight * meterTop;
		meterHeight = healthBarHeight - healthBarHeight * meterTop * 2;
		guiUtils.DrawRect(new Rect(meterX, meterY, meterWidth, meterHeight), new Color(0.47f, 0.5f, 0.45f, 0.5f));	
		if (health >= 0)
			guiUtils.DrawRect(new Rect(meterX, meterY, (meterWidth - meterStatWidth) * health, meterHeight), barColor);			

			
		// display current value
		meterTop -= 0.12f;		
		meterY = healthBarY + healthBarHeight * meterTop;
		meterHeight = healthBarHeight - healthBarHeight * meterTop * 2;
		int healthPercent = (int)(health * 100f);
		float meterStatX = meterX + (meterWidth - meterStatWidth) * health;
		GUI.Box(new Rect(meterStatX, meterY - meterHeight * 0.25f, meterStatWidth, meterHeight + meterHeight * 0.5f), "");
		//guiUtils.DrawRect(new Rect(meterStatX, meterY - meterHeight * 0.25f, meterStatWidth, meterHeight + meterHeight * 0.5f), new Color(0f, 0f, 0f, 1f));	
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
		GUI.color = new Color(1f, 1f, 1f, (health > 0.66f ? 0.8f : (health > 0.33f ? 0.65f : 0.5f)) * healthBarOpacity);
		GUI.DrawTexture(new Rect(textureX, textureY, textureWidth, textureHeight), greenHeartTexture);
		GUI.color = new Color(1f, 1f, 1f, 1f * healthBarOpacity);
	}

	
}