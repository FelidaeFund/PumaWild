using UnityEngine;
using System.Collections;

/// GuiUtils
/// Low-level GUI drawing utilities

public class GuiUtils : MonoBehaviour
{
	//===================================
	//===================================
	//		MODULE VARIABLES
	//===================================
	//===================================

	private Texture2D rectTexture;
	private GUIStyle rectStyle;

	//===================================
	//===================================
	//		INITIALIZATION
	//===================================
	//===================================

	void Start() 
	{	
		// basic rect
		rectTexture = new Texture2D(2,2);
		rectStyle = new GUIStyle();
	}

	//===================================
	//===================================
	//		UTILS
	//===================================
	//===================================

	public void DrawRect(Rect position, Color color)
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

	public float GetAngleFromOffset(float x1, float y1, float x2, float y2)
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