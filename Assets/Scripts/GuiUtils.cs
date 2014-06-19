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


}