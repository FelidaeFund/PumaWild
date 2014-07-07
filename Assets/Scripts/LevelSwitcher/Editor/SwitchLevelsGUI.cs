using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SwitchLevels))]

public class SwitchLevelsGUI : Editor 
{
	//---------------------------
	// Levels Toolbar variables
	//---------------------------
	//Keeps track of button changes
	private int selectedLevel = 0; //TODO: Implement serialization, and start with the value that is saved.
	private int toolbarSelected = 0; //TODO: Implement serialization, and start with the value that is saved.
	private string[] toolbarLabels = new string[] {"Lv1", "Lv2", "Lv3", "Lv4", "Lv5"}; //Labels

	
	
	
	//---------------------------
	// Paint Options variables
	//---------------------------
	// booleans indicate checked boxes
	private bool togglePaint = false;


	public override void OnInspectorGUI()
	{
		//------------------
		// Levels Toolbar
		//------------------
		GUILayout.Label ("Current Level:", EditorStyles.boldLabel);
		toolbarSelected = GUILayout.Toolbar (toolbarSelected, toolbarLabels, GUILayout.Width(200) , GUILayout.Height(30));
		//GUILayout.Button ("Apply", GUILayout.Width(70), GUILayout.Height(30));

		//Tests which button is pressed
		switch (toolbarSelected)
		{
			//Level 1
			case 0:
				if (toolbarSelected!=selectedLevel){
					Debug.Log("Level 1 pressed");
				}
				selectedLevel = toolbarSelected;
			break;
			//Level 2
			case 1:
				if (toolbarSelected!=selectedLevel){
					Debug.Log("Level 2 pressed");
				}
				selectedLevel = toolbarSelected;
			break;
			//Level 3
			case 2:
			if (toolbarSelected!=selectedLevel){
					Debug.Log("Level 3 pressed");
				}
				selectedLevel = toolbarSelected;
			break;
			//Level 4
			case 3:
				if (toolbarSelected!=selectedLevel){
					Debug.Log("Level 4 pressed");
				}
				selectedLevel = toolbarSelected;
			break;
			//Level 5
			case 4:
				if (toolbarSelected!=selectedLevel){
					Debug.Log("Level 5 pressed");
				}
				selectedLevel = toolbarSelected;
			break;
		}
		
		
		//------------------
		// Paint Enable
		//------------------
		GUILayout.Label ("Paint Visible:", EditorStyles.boldLabel);
		GUILayout.BeginHorizontal("box", GUILayout.Width(200));
			togglePaint = GUILayout.Toggle(togglePaint, " show painted terrains");
		GUILayout.EndHorizontal();
		
		
		
		changeLevel(selectedLevel+1);

		
/*		
		//------------------
		// Paint Options
		//------------------
		GUILayout.Label ("Paint Options:", EditorStyles.boldLabel);
		for(int i=0; i<10; i++){
			GUILayout.BeginHorizontal("box", GUILayout.Width(200));
				togglePaint[i] = GUILayout.Toggle(togglePaint[i], "Layer " + (i+1) );
				GUILayout.Button ("Reset", GUILayout.Width(45), GUILayout.Height(15));
			GUILayout.EndHorizontal();
		}
		GUILayout.BeginHorizontal (GUILayout.Width(200));
			GUILayout.Button ("All ON", GUILayout.Width(97), GUILayout.Height(20));
			GUILayout.Button ("All OFF", GUILayout.Width(97), GUILayout.Height(20));
		GUILayout.EndHorizontal();

		//------------------
		// Eraser Options
		//------------------
		GUILayout.Label ("Eraser Options:", EditorStyles.boldLabel);
		GUILayout.Space (-5);
		GUILayout.Label ("This command subtracts the area\npainted with 'ColorX' from all visible\nPaint layers. ");

		GUILayout.BeginHorizontal (GUILayout.Width(200));
			GUILayout.Button ("Erase", GUILayout.Width(97), GUILayout.Height(20));
			GUILayout.Button ("Reset Eraser", GUILayout.Width(97), GUILayout.Height(20));
		GUILayout.EndHorizontal();
		GUILayout.Space (15);
*/
		
		//------------------
		// Default Inspector
		//------------------
		GUILayout.Space(20);
		DrawDefaultInspector();
		GUILayout.Space(10);

	}

	public void changeLevel(int levelId)
	{
		SwitchLevels myTarget = (SwitchLevels)target;
		myTarget.switchLevel (levelId, togglePaint);
	}

}








