using UnityEngine;
using System.Collections;

/// InfoPanel
/// Popup panel for scientific and conservation info

public class InfoPanel : MonoBehaviour
{
	//===================================
	//===================================
	//		MODULE VARIABLES
	//===================================
	//===================================

	private int infoPanelPage;
	private bool infoPanelIntroFlag;
	private Rect overlayRect;
	
	// textures based on bitmap files
	private Texture2D buckHeadTexture;
	private Texture2D doeHeadTexture;
	private Texture2D fawnHeadTexture;
	private Texture2D forestTexture; 
	private Texture2D closeup1Texture;
	private Texture2D closeup2Texture;
	private Texture2D closeup3Texture;
	private Texture2D closeup4Texture;
	private Texture2D closeup5Texture;
	private Texture2D closeup6Texture;
	private Texture2D closeupBackgroundTexture;
	private Texture2D iconFacebookTexture; 
	private Texture2D iconTwitterTexture; 
	private Texture2D iconGoogleTexture; 
	private Texture2D iconPinterestTexture; 
	private Texture2D iconYouTubeTexture; 
	private Texture2D iconLinkedInTexture; 

	// external modules
	private GuiManager guiManager;
	//private GuiComponents guiComponents;
	private GuiUtils guiUtils;
	//private LevelManager levelManager;
	//private ScoringSystem scoringSystem;

	//===================================
	//===================================
	//		INITIALIZATION
	//===================================
	//===================================

	void Start() 
	{	
		// connect to external modules
		guiManager = GetComponent<GuiManager>();
		//guiComponents = GetComponent<GuiComponents>();
		guiUtils = GetComponent<GuiUtils>();
		//levelManager = GetComponent<LevelManager>();
		//scoringSystem = GetComponent<ScoringSystem>();

		// texture references from GuiManager
		buckHeadTexture = guiManager.buckHeadTexture;
		doeHeadTexture = guiManager.doeHeadTexture;
		fawnHeadTexture = guiManager.fawnHeadTexture;
		forestTexture = guiManager.forestTexture;
		closeup1Texture = guiManager.closeup1Texture;
		closeup2Texture = guiManager.closeup2Texture;
		closeup3Texture = guiManager.closeup3Texture;
		closeup4Texture = guiManager.closeup4Texture;
		closeup5Texture = guiManager.closeup5Texture;
		closeup6Texture = guiManager.closeup6Texture;
		closeupBackgroundTexture = guiManager.closeupBackgroundTexture;
		iconFacebookTexture = guiManager.iconFacebookTexture;
		iconTwitterTexture = guiManager.iconTwitterTexture;
		iconGoogleTexture = guiManager.iconGoogleTexture;
		iconPinterestTexture = guiManager.iconPinterestTexture;
		iconYouTubeTexture = guiManager.iconYouTubeTexture;
		iconLinkedInTexture = guiManager.iconLinkedInTexture;
		
		// additional initialization
		infoPanelPage = 0;
		infoPanelIntroFlag = true;
	}
	
	//===================================
	//===================================
	//		UTILITY FUNCTIONS
	//===================================
	//===================================

	public int GetCurrentPage()
	{
		return infoPanelPage;
	}
	
	public void SetCurrentPage(int newPageNum)
	{
		infoPanelPage = newPageNum;
	}
	
	public void ClearIntroFlag()
	{
		infoPanelIntroFlag = false;
	}
	
	//===================================
	//===================================
	//		DRAW THE INFO PANEL
	//===================================
	//===================================

	public void Draw(float infoPanelOpacity) 
	{ 
		overlayRect = guiManager.GetOverlayRect();
	
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
		//guiUtils.DrawRect(new Rect(infoPanelX + infoPanelWidth * 0.01f, infoPanelY + infoPanelWidth * 0.01f, infoPanelWidth * 0.98f, infoPanelHeight - infoPanelWidth * 0.02f), new Color(0.22f, 0.21f, 0.20f, 1f));	
		guiUtils.DrawRect(new Rect(popupInnerRectX, popupInnerRectY, popupInnerRectWidth, popupInnerRectHeight * 0.07f), new Color(0.205f, 0.21f, 0.19f, 1f));	
		guiUtils.DrawRect(new Rect(popupInnerRectX, popupInnerRectY + popupInnerRectHeight * 0.11f, popupInnerRectWidth, popupInnerRectHeight - popupInnerRectHeight * 0.11f - popupInnerRectHeight * 0.09f), new Color(0.205f, 0.21f, 0.19f, 1f));	
				
				
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
		
			guiManager.customGUISkin.button.fontSize = (int)(overlayRect.width * 0.0196);
		
			// introduction
			
			GUI.color = new Color(1f, 1f, 1f, 0.15f * infoPanelOpacity);
			if (infoPanelPage != 0)
				guiUtils.DrawRect(new Rect(buttonX - buttonBorderWidth, buttonY - buttonBorderWidth, buttonWidth + buttonBorderWidth * 2f, buttonHeight + buttonBorderWidth * 2f), new Color(1f, 1f, 1f, 1f));	
			GUI.skin = guiManager.customGUISkin;
			guiManager.customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
			if (infoPanelPage != 0)
				guiManager.customGUISkin.button.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1f);
			else
				guiManager.customGUISkin.button.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "")) {
				infoPanelPage = 0;
			}
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "Overview")) {
				infoPanelPage = 0;
			}
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			guiManager.customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);

			// puma biology
			
			buttonX += buttonWidth + buttonGap;
			
			GUI.color = new Color(1f, 1f, 1f, 0.15f * infoPanelOpacity);
			if (infoPanelPage != 1)
				guiUtils.DrawRect(new Rect(buttonX - buttonBorderWidth, buttonY - buttonBorderWidth, buttonWidth + buttonBorderWidth * 2f, buttonHeight + buttonBorderWidth * 2f), new Color(1f, 1f, 1f, 1f));	
			GUI.skin = guiManager.customGUISkin;
			guiManager.customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
			if (infoPanelPage != 1)
				guiManager.customGUISkin.button.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1f);
			else
				guiManager.customGUISkin.button.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "")) {
				infoPanelPage = 1;
			}
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "Biology")) {
				infoPanelPage = 1;
			}
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			guiManager.customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);

			// puma ecology
			
			buttonX += buttonWidth + buttonGap;
			
			GUI.color = new Color(1f, 1f, 1f, 0.15f * infoPanelOpacity);
			if (infoPanelPage != 2)
				guiUtils.DrawRect(new Rect(buttonX - buttonBorderWidth, buttonY - buttonBorderWidth, buttonWidth + buttonBorderWidth * 2f, buttonHeight + buttonBorderWidth * 2f), new Color(1f, 1f, 1f, 1f));	
			GUI.skin = guiManager.customGUISkin;
			guiManager.customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
			if (infoPanelPage != 2)
				guiManager.customGUISkin.button.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1f);
			else
				guiManager.customGUISkin.button.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "")) {
				infoPanelPage = 2;
			}
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "Ecology")) {
				infoPanelPage = 2;
			}
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			guiManager.customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);

			// catching prey
			
			buttonX += buttonWidth + buttonGap;
			
			GUI.color = new Color(1f, 1f, 1f, 0.15f * infoPanelOpacity);
			if (infoPanelPage != 3)
				guiUtils.DrawRect(new Rect(buttonX - buttonBorderWidth, buttonY - buttonBorderWidth, buttonWidth + buttonBorderWidth * 2f, buttonHeight + buttonBorderWidth * 2f), new Color(1f, 1f, 1f, 1f));	
			GUI.skin = guiManager.customGUISkin;
			guiManager.customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
			if (infoPanelPage != 3)
				guiManager.customGUISkin.button.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1f);
			else
				guiManager.customGUISkin.button.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "")) {
				infoPanelPage = 3;
			}
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "Predation")) {
				infoPanelPage = 3;
			}
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			guiManager.customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);

			// survival threats
			
			buttonX += buttonWidth + buttonGap;
			
			GUI.color = new Color(1f, 1f, 1f, 0.15f * infoPanelOpacity);
			if (infoPanelPage != 4)
				guiUtils.DrawRect(new Rect(buttonX - buttonBorderWidth, buttonY - buttonBorderWidth, buttonWidth + buttonBorderWidth * 2f, buttonHeight + buttonBorderWidth * 2f), new Color(1f, 1f, 1f, 1f));	
			GUI.skin = guiManager.customGUISkin;
			guiManager.customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
			if (infoPanelPage != 4)
				guiManager.customGUISkin.button.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1f);
			else
				guiManager.customGUISkin.button.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "")) {
				infoPanelPage = 4;
			}
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "Mortality")) {
				infoPanelPage = 4;
			}
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			guiManager.customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);

			// help pumas
			
			buttonX += buttonWidth + buttonGap;
			
			GUI.color = new Color(1f, 1f, 1f, 0.15f * infoPanelOpacity);
			if (infoPanelPage != 5)
				guiUtils.DrawRect(new Rect(buttonX - buttonBorderWidth, buttonY - buttonBorderWidth, buttonWidth + buttonBorderWidth * 2f, buttonHeight + buttonBorderWidth * 2f), new Color(1f, 1f, 1f, 1f));	
			GUI.skin = guiManager.customGUISkin;
			guiManager.customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
			if (infoPanelPage != 5)
				guiManager.customGUISkin.button.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1f);
			else
				guiManager.customGUISkin.button.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "")) {
				infoPanelPage = 5;
			}
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "How to Help")) {
				infoPanelPage = 5;
			}
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			guiManager.customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);
			
			// DONE
			
			buttonX += buttonWidth + buttonGap;
			
			GUI.color = new Color(1f, 1f, 1f, 0.15f * infoPanelOpacity);
			if (infoPanelPage != 6)
				guiUtils.DrawRect(new Rect(buttonX - buttonBorderWidth, buttonY - buttonBorderWidth, buttonWidth + buttonBorderWidth * 2f, buttonHeight + buttonBorderWidth * 2f), new Color(1f, 1f, 1f, 1f));	
			GUI.skin = guiManager.customGUISkin;
			guiManager.customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
			if (infoPanelPage != 6)
				guiManager.customGUISkin.button.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1f);
			else
				guiManager.customGUISkin.button.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "")) {
				guiManager.CloseInfoPanel(true);
			}
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "CLOSE   X")) {
				guiManager.CloseInfoPanel(true);
			}
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			guiManager.customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);
			
		}
		else {
		
			// DRAW 'OK' BUTTON FOR WELCOME SCREEN
			
			buttonY -= buttonHeight * 0.15f;
			buttonHeight *= 1.3f;

			buttonWidth *= 1.2f;
			buttonX = popupInnerRectX + popupInnerRectWidth * 0.5f - buttonWidth * 0.5f;

			GUI.color = new Color(1f, 1f, 1f, 0.15f * infoPanelOpacity);
			if (infoPanelPage != 0)
				guiUtils.DrawRect(new Rect(buttonX - buttonBorderWidth, buttonY - buttonBorderWidth, buttonWidth + buttonBorderWidth * 2f, buttonHeight + buttonBorderWidth * 2f), new Color(1f, 1f, 1f, 1f));	
			GUI.skin = guiManager.customGUISkin;
			guiManager.customGUISkin.button.fontSize = (int)(overlayRect.width * 0.026);
			guiManager.customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			GUI.backgroundColor = new Color(1f, 1f, 1f, 1f);
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "")) {
				guiManager.CloseInfoPanel(true);
				guiManager.SetGuiState("guiStateStartApp2");
			}
			if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "GO")) {
				guiManager.CloseInfoPanel(true);
				guiManager.SetGuiState("guiStateStartApp2");
			}
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
		}
		
		
		if (infoPanelIntroFlag == true || infoPanelPage == 0) {
		
		
		
		}
		else if (infoPanelPage != 5) {
		
			// vertical divider
			//guiUtils.DrawRect(new Rect(popupInnerRectX + popupInnerRectWidth * 0.4965f, popupInnerRectY + popupInnerRectHeight * 0.19f, popupInnerRectWidth * 0.0035f, popupInnerRectHeight * 0.69f), new Color(0.31f, 0.305f, 0.30f, 1f));	
			//guiUtils.DrawRect(new Rect(popupInnerRectX + popupInnerRectWidth * 0.500f, popupInnerRectY + popupInnerRectHeight * 0.19f, popupInnerRectWidth * 0.005f, popupInnerRectHeight * 0.69f), new Color(0.13f, 0.125f, 0.12f, 1f));	
			
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
			guiManager.customGUISkin.button.fontSize = (int)(overlayRect.width * 0.032);
			guiManager.customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			guiManager.customGUISkin.button.normal.textColor = new Color(0.8f, 0.1f, 0f, 1f);
			Color defaultHoverColor = guiManager.customGUISkin.button.hover.textColor;
			guiManager.customGUISkin.button.hover.textColor = new Color(0.9f, 0.12f, 0f, 1f);
			guiUtils.DrawRect(new Rect(popupInnerRectX + popupInnerRectWidth * 0.35f + popupInnerRectHeight * 0.01f, popupInnerRectY + popupInnerRectHeight * 0.423f + popupInnerRectHeight * 0.01f, popupInnerRectWidth * 0.3f - popupInnerRectHeight * 0.02f, popupInnerRectHeight * 0.15f - popupInnerRectHeight * 0.023f), new Color(1f, 1f, 1f, 0.15f));	
			if (GUI.Button(new Rect(popupInnerRectX + popupInnerRectWidth * 0.35f, popupInnerRectY + popupInnerRectHeight * 0.42f, popupInnerRectWidth * 0.3f, popupInnerRectHeight * 0.15f), "Make a Donation")) {
				Application.OpenURL("http://www.felidaefund.org/donate");
			}
			guiManager.customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);
			guiManager.customGUISkin.button.hover.textColor = defaultHoverColor;
		}

		// "learn more" section

		if (infoPanelIntroFlag == false && infoPanelPage != 0) {
		
			float yOffsetForPage5 = infoPanelPage == 5 ? (popupInnerRectHeight * -0.12f) : 0f;
		
			if (infoPanelPage == 5)
				guiManager.customGUISkin.button.normal.textColor = new Color(0.88f, 0.55f, 0f, 1f);
			else
				guiManager.customGUISkin.button.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1f);

			style.fontStyle = FontStyle.BoldAndItalic;
			style.alignment = TextAnchor.MiddleCenter;
			style.fontSize = (int)(popupInnerRectWidth * 0.014f);
			style.normal.textColor = new Color(0.6f, 0.6f, 0.6f, 1f);
			if (infoPanelPage == 5)
				GUI.Button(new Rect(popupInnerRectX + popupInnerRectWidth * 0.4f, popupInnerRectY + popupInnerRectHeight * 0.765f + yOffsetForPage5, popupInnerRectWidth * 0.2f, popupInnerRectHeight * 0.06f), "Get involved...", style);
			else
				GUI.Button(new Rect(popupInnerRectX + popupInnerRectWidth * 0.4f, popupInnerRectY + popupInnerRectHeight * 0.765f, popupInnerRectWidth * 0.2f, popupInnerRectHeight * 0.06f), "Learn more at...", style);

			GUI.color = new Color(1f, 1f, 1f, 0.9f * infoPanelOpacity);

			guiManager.customGUISkin.button.fontSize = (int)(overlayRect.width * 0.016);
			guiManager.customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			if (GUI.Button(new Rect(popupInnerRectX + popupInnerRectWidth * 0.312f, popupInnerRectY + popupInnerRectHeight * 0.835f + yOffsetForPage5, popupInnerRectWidth * 0.17f, popupInnerRectHeight * 0.045f), "Felidae Fund")) {
				Application.OpenURL("http://www.felidaefund.org");
			}
		
			guiManager.customGUISkin.button.fontSize = (int)(overlayRect.width * 0.016);
			guiManager.customGUISkin.button.fontStyle = FontStyle.BoldAndItalic;
			if (GUI.Button(new Rect(popupInnerRectX + popupInnerRectWidth * 0.518f, popupInnerRectY + popupInnerRectHeight * 0.835f + yOffsetForPage5, popupInnerRectWidth * 0.17f, popupInnerRectHeight * 0.045f), "Bay Area Puma Project")) {
				Application.OpenURL("http://www.bapp.org");
			}	
			
			GUI.color = new Color(1f, 1f, 1f, 1f * infoPanelOpacity);
			guiManager.customGUISkin.button.normal.textColor = new Color(0.90f, 0.65f, 0f, 1f);


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
	
}