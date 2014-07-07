using UnityEngine;
using System.Collections;

public class SwitchLevels : MonoBehaviour {

	//-----------------------
	// Module Variables
	//-----------------------
	private int currentLevel = 0; //TODO: Implement serializable, so we start retrieving the saved level.

	private bool initFlag = false;

	// Terrains GameObjects
	private GameObject terrainA;
	private GameObject terrainB;
	private GameObject terrainC;
	private GameObject terrainD;

	private TerrainData targetLevel;

	public void switchLevel(int levelNum)
	{
		terrain1A = GameObject.Find("Terrain1A");
		terrain1B = GameObject.Find("Terrain1B");
		terrain1C = GameObject.Find("Terrain1C");
		terrain1D = GameObject.Find("Terrain1D");

		terrain2A = GameObject.Find("Terrain2A");
		terrain2B = GameObject.Find("Terrain2B");
		terrain2C = GameObject.Find("Terrain2C");
		terrain2D = GameObject.Find("Terrain2D");

		if (levelNum == 



		switch (levelId)
		{
			case 1:
				targetLevel = GameObject.Find("Level1").GetComponent<Terrain>().terrainData;
			break;
			
			case 2:
				targetLevel = GameObject.Find("Level2").GetComponent<Terrain>().terrainData;
			break;

			case 3:
				targetLevel = GameObject.Find("Level3").GetComponent<Terrain>().terrainData;
			break;
			
			case 4:
				targetLevel = GameObject.Find("Level4").GetComponent<Terrain>().terrainData;
			break;
			
			case 5:
				targetLevel = GameObject.Find("Level5").GetComponent<Terrain>().terrainData;
			break;

			terrainA.GetComponent<Terrain>().terrainData = targetLevel;
			terrainB.GetComponent<Terrain>().terrainData = targetLevel;
			terrainC.GetComponent<Terrain>().terrainData = targetLevel;
			terrainD.GetComponent<Terrain>().terrainData = targetLevel;
		}
	}
}
