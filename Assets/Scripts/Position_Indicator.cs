using UnityEngine;
using System.Collections;

/// Positions a deer head on the border of the screen to help 
/// the player track deer

public class Position_Indicator : MonoBehaviour
{
	public Texture2D indicatorBuck; 
	public Texture2D indicatorDoe; 
	public Texture2D indicatorFawn; 
	public Texture2D indicatorBkgnd; 

    void Start()
    {
	}

    public void DrawIndicator(float cameraRotY, GameObject pumaObj, GameObject deerObj, string type, int borderThickness, float guiOpacity)
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

		while (cameraRotY < 0)
			cameraRotY += 360;
		while (cameraRotY > 360)
			cameraRotY -= 360;
		
		// angle based on midpoint between camera and puma
		float refX = (pumaObj.transform.position.x + Camera.main.transform.position.x) / 2;
		float refY = (pumaObj.transform.position.z + Camera.main.transform.position.z) / 2;

		deerToPumaAngle = GetAngleFromOffset(refX, refY, deerObj.transform.position.x, deerObj.transform.position.z);
		deerToPumaDistance = Vector3.Distance(pumaObj.transform.position, deerObj.transform.position);

		xOffset = -(Mathf.Sin((cameraRotY - deerToPumaAngle) * Mathf.PI / 180) * rangeY);
		yOffset = (Mathf.Cos((cameraRotY - deerToPumaAngle) * Mathf.PI / 180) * rangeY);
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

	
	float GetAngleFromOffset(float x1, float y1, float x2, float y2)
	{
        float deltaX = x2 - x1;
        float deltaY = y2 - y1;
        float angle = Mathf.Atan2(deltaY, -deltaX) * (180f / Mathf.PI);
        angle -= 90f;
        if (angle < 0f)
			angle += 360f;
		if (angle >= 360f)
			angle -= 360f;
		return angle;
	}	

}