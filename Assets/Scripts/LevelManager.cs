using UnityEngine;
using System.Collections;

/// Main handling of all level-based play
/// 

public class LevelManager : MonoBehaviour 
{
	// DEBUG & DEV
	private bool goStraightToFeeding = false;
	public float speedOverdrive = 1.0f;
	public float displayVar1;
	public float displayVar2;
	public float displayVar3;
	private int frameCount = 0;
	private int frameFirstTime;
	private int framePrevTime;
	public int frameCurrentDuration;
	public int frameAverageDuration;

	//===================================
	//===================================
	//		MODULE VARIABLES
	//===================================
	//===================================

	// STATES

	public string gameState;
	private string gameSubState;
	private float stateStartTime;
	private bool stateInitFlag;
	private int currentLevel;

	// GROUND PLANES

	public Terrain terrain1A;
	public Terrain terrain1B;
	public Terrain terrain1C;
	public Terrain terrain1D;
	public Terrain terrain2A;
	public Terrain terrain2B;
	public Terrain terrain2C;
	public Terrain terrain2D;
	public Terrain terrain3A;
	public Terrain terrain3B;
	public Terrain terrain3C;
	public Terrain terrain3D;
	public Terrain terrain4A;
	public Terrain terrain4B;
	public Terrain terrain4C;
	public Terrain terrain4D;
	public Terrain terrain5A;
	public Terrain terrain5B;
	public Terrain terrain5C;
	public Terrain terrain5D;
	private Terrain[] terrainArray;

	// PUMA

	public GameObject pumaObj;
	private int selectedPuma = -1;
	public float mainHeading;
	private float pumaX;
	private float pumaY;
	private float pumaZ;
	private float pumaHeading = 0f;
	public float pumaHeadingOffset = 0f;   			// NOTE: is currently changed from InputControls....probably shouldn't be
	private float pumaStalkingSpeed = 22f * 0.66f;
	private float pumaChasingSpeed = 32f * 0.66f;
	private float defaultPumaChasingSpeed = 32f * 0.66f;
	private float chaseTriggerDistance = 40f * 0.66f;
	private float defaultChaseTriggerDistance = 40f * 0.66f;
	private float deerCaughtFinalOffsetFactor0 = 1f * 0.66f;
	private float deerCaughtFinalOffsetFactor90 = 1f;

	// PUMA CHARACTERISTICS

	private float[] powerArray = new float[] {0.6f, 0.4f, 0.9f, 0.7f, 0.7f, 0.5f};
	private float[] speedArray = new float[] {0.90f, 0.80f, 0.55f, 0.45f, 0.20f, 0.10f};
	private float[] enduranceArray = new float[] {0.6f, 0.4f, 0.9f, 0.8f, 0.6f, 0.4f};
	private float[] stealthinessArray = new float[] {0.10f, 0.20f, 0.45f, 0.55f, 0.80f, 0.90f};
	
	// DEER

	public class DeerClass {
		public string type;
		public GameObject gameObj;
		public float heading = 0f;
		public float targetHeading = 0f;
		public float nextTurnTime = 0f;
		public float forwardRate = 0f;
		public float turnRate = 0f;
		public float baseY;
	}
	
	public DeerClass buck;
	public DeerClass doe;
	public DeerClass fawn;

	private bool newChaseFlag = false;

	private float buckDefaultForwardRate = 30f * 0.66f;
	private float buckDefaultTurnRate = 22.5f * 0.66f;
	private float doeDefaultForwardRate = 29f * 0.66f;
	private float doeDefaultTurnRate = 22.5f * 0.66f;
	private float fawnDefaultForwardRate = 28f * 0.66f;
	private float fawnDefaultTurnRate = 22.5f * 0.66f;

	// THE CAUGHT DEER
	
	private DeerClass caughtDeer;
	private float deerCaughtHeading;
	private float deerCaughtFinalHeading;
	private bool deerCaughtHeadingLeft = false;
	private float deerCaughtOffsetX;
	private float deerCaughtFinalOffsetX;
	private float deerCaughtOffsetZ;
	private float deerCaughtFinalOffsetZ;
	private float deerCaughtmainHeading;
	private float deerCaughtNextFrameTime;
	public int deerCaughtEfficiency = 0;

	// ANIMATORS
	
	public Animator pumaAnimator;
	public Animator buckAnimator;
	public Animator doeAnimator;
	public Animator fawnAnimator;
	
	// EXTERNAL MODULES
	private ScoringSystem scoringSystem;
	private InputControls inputControls;
	private CameraController cameraController;
	private SwitchLevels switchLevels;

	//===================================
	//===================================
	//		INITIALIZATION
	//===================================
	//===================================

    void Awake()
	{
        Application.targetFrameRate = 120;
    }

	void Start() 
	{	
		// connect to external modules
		scoringSystem = GetComponent<ScoringSystem>();
		inputControls = GetComponent<InputControls>();
		cameraController = GetComponent<CameraController>();
		GameObject levelSwitcher = GameObject.Find("SwitchLevels");	
		switchLevels = levelSwitcher.GetComponent<SwitchLevels>();

		// puma
		pumaObj = GameObject.Find("_Puma-thin");	
				
		// deer
		buck = new DeerClass();
		buck.type = "Buck";
		buck.forwardRate = 0; //30f;
		buck.turnRate = 0; //22.5f;
		buck.baseY = 0f;

		doe = new DeerClass();
		doe.type = "Doe";
		doe.forwardRate = 0; //30f;
		doe.turnRate = 0; //22.5f;
		doe.baseY = 0f;

		fawn = new DeerClass();
		fawn.type = "Fawn";
		fawn.forwardRate = 0; //30f;
		fawn.turnRate = 0; //22.5f;
		fawn.baseY = 0f;

		//buck.gameObj = new GameObject();
		//doe.gameObj = new GameObject();
		//fawn.gameObj  = new GameObject();

		buck.gameObj = GameObject.Find("_Buck");
		doe.gameObj = GameObject.Find("_Doe");
		fawn.gameObj = GameObject.Find("_Fawn");
		
			
		// create array of ground planes	
		terrainArray = new Terrain[20];
		terrainArray[0] = terrain1A;		
		terrainArray[1] = terrain1B;		
		terrainArray[2] = terrain1C;		
		terrainArray[3] = terrain1D;		
		terrainArray[4] = terrain2A;		
		terrainArray[5] = terrain2B;		
		terrainArray[6] = terrain2C;		
		terrainArray[7] = terrain2D;		
		terrainArray[8] = terrain3A;		
		terrainArray[9] = terrain3B;		
		terrainArray[10] = terrain3C;		
		terrainArray[11] = terrain3D;		
		terrainArray[12] = terrain4A;		
		terrainArray[13] = terrain4B;		
		terrainArray[14] = terrain4C;		
		terrainArray[15] = terrain4D;		
		terrainArray[16] = terrain5A;		
		terrainArray[17] = terrain5B;		
		terrainArray[18] = terrain5C;		
		terrainArray[19] = terrain5D;		
		
		InitLevel(3);
	}
	
	public void InitLevel(int level)
	{
		currentLevel = level;
		switchLevels.SwitchLevel(level, false);

		gameState = "gameStateGui";
		stateStartTime = Time.time;
		mainHeading = Random.Range(0f, 360f);

		pumaX = 0f;
		pumaY = 0f;
		pumaZ = 0f;			
		pumaObj.transform.position = new Vector3(pumaX, pumaY, pumaZ);		
				
		//================================
		// Reset the Ground Planes
		//================================
						
		for (int i = 0; i < terrainArray.Length; i++) {
			float terrainX = terrainArray[i].transform.position.x;
			float terrainZ = terrainArray[i].transform.position.z;
			
			while (pumaX - terrainX > 3000) {
				terrainX += 4000;
			}
			
			while (terrainX - pumaX > 1000) {
				terrainX -= 4000;
			}

			while (pumaZ - terrainZ > 3000) {
				terrainZ += 4000;
			}
			
			while (terrainZ - pumaZ > 1000) {
				terrainZ -= 4000;
			}
			
			terrainArray[i].transform.position = new Vector3 (terrainX, 0, terrainZ);
		}
	}
	
	public int GetCurrentLevel()
	{
		return currentLevel;
	}
	
	//===================================
	//===================================
	//		PUBLICS & UTILS
	//===================================
	//===================================

	public void SetGameState(string newGameState)
	{
		gameState = newGameState;
		gameSubState = "gameSubStateNull";
		stateStartTime = Time.time;
		stateInitFlag = false;
	}

	public bool IsCaughtState()
	{
		return (gameState == "gameStateFeeding1") ? true : false;
	}

	public void SetSelectedPuma(int selection)
	{
		selectedPuma = selection;
		
		pumaChasingSpeed = defaultPumaChasingSpeed + (10f * speedArray[selectedPuma]);
		chaseTriggerDistance = defaultChaseTriggerDistance - (20f * stealthinessArray[selectedPuma]);
	}

	void SelectCameraPosition(string targetPositionLabel, float targetRotOffsetY, float fadeTime, string mainCurve, string rotXCurve)
	{
		if (stateInitFlag == false) {
			cameraController.SelectTargetPosition(targetPositionLabel, targetRotOffsetY, fadeTime, mainCurve, rotXCurve);
			stateInitFlag = true;
		}
	}

	//===================================
	//===================================
	//		PERIODIC UPDATE
	//===================================
	//===================================

	void Update() 
	{	
		float fadeTime;
		float guiFlybySpeed = 0f;

		//pumaAnimator.SetLayerWeight(1, 1f);
	
		if (pumaObj == null || buck == null || doe == null || fawn == null)
			return;
			
		CalculateFrameRate();
			
		inputControls.ProcessControls(gameState);

		//=================================
		// Get distances from puma to deer
		//=================================

		float pumaDeerDistance1 = Vector3.Distance(pumaObj.transform.position, buck.gameObj.transform.position);
		float pumaDeerDistance2 = Vector3.Distance(pumaObj.transform.position, doe.gameObj.transform.position);
		float pumaDeerDistance3 = Vector3.Distance(pumaObj.transform.position, fawn.gameObj.transform.position);		
		
		//=================================
		// Check for Skip Ahead
		//=================================

		if (goStraightToFeeding == true && gameState == "gameStateStalking") {
			SetGameState("gameStateChasing");
			pumaDeerDistance1 = 0;
		}
			
		
		//===========================
		// Update Game-State Logic
		//===========================
			
		switch (gameState) {
		
		//------------------------------
		// GUI States
		//
		// user interface states
		// main panel showing
		//------------------------------

		case "gameStateGui":
			// high in air, overlay panel showing
			guiFlybySpeed = 1f;
			SelectCameraPosition("cameraPosGui", -120f, 0f, null, null);
			break;
	
		case "gameStateLeavingGui":
			// zoom down into close up
			fadeTime = 2.5f;
			guiFlybySpeed = 1f - (Time.time - stateStartTime) / fadeTime;		
			if (stateInitFlag == false) {
				// init the level before zooming down
				PlaceDeerPositions();
				ResetAnimations();
				// stateInitFlag set to TRUE in the next function
			}
			SelectCameraPosition("cameraPosCloseup", 1000000f, fadeTime, "mainCurveSForward", "curveRotXLogarithmicSecondHalf"); // 1000000 signifies no change for cameraRotOffsetY
			if (Time.time >= stateStartTime + fadeTime) {
				guiFlybySpeed = 0f;
				SetGameState("gameStateEnteringGameplay1");
			}
			break;
	
		//------------------------------
		// Gameplay States
		//
		// entering, leaving,
		// stalking and chasing
		//------------------------------

		case "gameStateEnteringGameplay1":
			// brief pause on close up
			fadeTime = 0.1f;
			if (Time.time >= stateStartTime + fadeTime) {
				SetGameState("gameStateEnteringGameplay2");
			}
			break;	
	
		case "gameStateEnteringGameplay2":
			// swing around to behind view
			fadeTime = 1.7f;
			SelectCameraPosition("cameraPosHigh", 0f, fadeTime, "mainCurveSBackward", "curveRotXLinearBackwardsSecondHalf"); 
			if (Time.time >= stateStartTime + fadeTime) {
				inputControls.ResetControls();		
				SetGameState("gameStateStalking");
			}
			break;	
	
		case "gameStateStalking":
			float lookingDistance = chaseTriggerDistance * 2f;
			float chasingDistance = chaseTriggerDistance;

			if (pumaDeerDistance1 < lookingDistance || pumaDeerDistance2 < lookingDistance || pumaDeerDistance3 < lookingDistance) {
				buckAnimator.SetBool("Looking", true);
				doeAnimator.SetBool("Looking", true);
				fawnAnimator.SetBool("Looking", true);
			}
			else {
				buckAnimator.SetBool("Looking", false);
				doeAnimator.SetBool("Looking", false);
				fawnAnimator.SetBool("Looking", false);
			}

			if (pumaDeerDistance1 < chasingDistance || pumaDeerDistance2 < chasingDistance || pumaDeerDistance3 < chasingDistance) {
				SetGameState("gameStateChasing");
				pumaAnimator.SetBool("Chasing", true);
				buckAnimator.SetBool("Running", true);
				doeAnimator.SetBool("Running", true);
				fawnAnimator.SetBool("Running", true);
				newChaseFlag = true;
				pumaHeadingOffset = 0;  // instantly disable diagonal movement (TEMP - really should swing camera around)
			}
			buck.forwardRate = 0f;
			buck.turnRate = 0f;
			doe.forwardRate = 0f;
			doe.turnRate = 0f;
			fawn.forwardRate = 0f;
			fawn.turnRate = 0f;
			break;
	
		case "gameStateChasing":
			// main chasing state - with a couple of quick initial camera moves handled via sub-states
			if (stateInitFlag == false) {
				gameSubState = "chasingSubState1";
				cameraController.SelectTargetPosition("cameraPosMedium", 1000000f, 0.75f, "mainCurveLinear", "curveRotXLinear");  // 1000000 signifies no change for cameraRotOffsetY
				stateInitFlag = true;
			}
			else if (gameSubState == "chasingSubState1" && (Time.time >= stateStartTime + 0.75f)) {
				gameSubState = "chasingSubState2";
				cameraController.SelectTargetPosition("cameraPosLow", 1000000f, 0.25f, "mainCurveLinear", "curveRotXLinear");  // 1000000 signifies no change for cameraRotOffsetY
			}

			buck.forwardRate = buckDefaultForwardRate * Random.Range(0.9f, 1.1f);		// ??? should these really be changed every frame ???
			buck.turnRate = buckDefaultTurnRate * Random.Range(0.9f, 1.1f);
			doe.forwardRate = doeDefaultForwardRate * Random.Range(0.9f, 1.1f);
			doe.turnRate = doeDefaultTurnRate * Random.Range(0.9f, 1.1f);
			fawn.forwardRate = fawnDefaultForwardRate * Random.Range(0.9f, 1.1f);
			fawn.turnRate = fawnDefaultTurnRate * Random.Range(0.9f, 1.1f);

			//buck.forwardRate = 0f;
			//doe.forwardRate = 0f;
			//fawn.forwardRate = 0f;
			//buck.turnRate = 0f;
			//doe.turnRate = 0f;
			//fawn.turnRate = 0f;

			if (pumaDeerDistance1 < 2.5f || pumaDeerDistance2 < 2.5f || pumaDeerDistance3 < 2.5f) {
			
				// DEER IS CAUGHT !!!
			
				if (pumaDeerDistance1 < 2.5f) {
					buck.forwardRate = 0;
					buck.turnRate = 0f;
					buckAnimator.SetBool("Die", true);
					caughtDeer = buck;
					scoringSystem.DeerCaught(selectedPuma, "Buck");
				}
				else if (pumaDeerDistance2 < 2.5f) {
					doe.forwardRate = 0f;
					doe.turnRate = 0f;
					doeAnimator.SetBool("Die", true);
					caughtDeer = doe;
					scoringSystem.DeerCaught(selectedPuma, "Doe");
				}
				else {
					fawn.forwardRate = 0f;
					fawn.turnRate = 0f;
					fawnAnimator.SetBool("Die", true);
					caughtDeer = fawn;
					scoringSystem.DeerCaught(selectedPuma, "Fawn");
				}

				// prepare caughtDeer obj for slide
				deerCaughtHeading = caughtDeer.heading;
				if (mainHeading >= deerCaughtHeading) {
					deerCaughtHeadingLeft = (mainHeading - deerCaughtHeading <= 180) ? false : true;
				}
				else {
					deerCaughtHeadingLeft = (deerCaughtHeading - mainHeading <= 180) ? true : false;
				}
				if (deerCaughtHeadingLeft == true) {
					deerCaughtFinalHeading = mainHeading + 90;
				}
				else {
					//deerCaughtFinalHeading = mainHeading - 90;
					deerCaughtFinalHeading = mainHeading + 90;
				}
				if (deerCaughtFinalHeading < 0)
					deerCaughtFinalHeading += 360;
				if (deerCaughtFinalHeading >= 360)
					deerCaughtFinalHeading -= 360;
				deerCaughtmainHeading = mainHeading;
				deerCaughtOffsetX = caughtDeer.gameObj.transform.position.x - pumaX;
				deerCaughtOffsetZ = caughtDeer.gameObj.transform.position.z - pumaZ;
				deerCaughtFinalOffsetX = (Mathf.Sin(mainHeading*Mathf.PI/180) * deerCaughtFinalOffsetFactor0);
				deerCaughtFinalOffsetZ = (Mathf.Cos(mainHeading*Mathf.PI/180) * deerCaughtFinalOffsetFactor0);
				deerCaughtFinalOffsetX += (Mathf.Sin((mainHeading-90f)*Mathf.PI/180) * deerCaughtFinalOffsetFactor90);
				deerCaughtFinalOffsetZ += (Mathf.Cos((mainHeading-90f)*Mathf.PI/180) * deerCaughtFinalOffsetFactor90);
				deerCaughtNextFrameTime = 0;
				
				if (Time.time - stateStartTime < 5f)
					deerCaughtEfficiency = 3;
				else if (Time.time - stateStartTime < 10f)
					deerCaughtEfficiency = 2;
				else if (Time.time - stateStartTime < 16f)
					deerCaughtEfficiency = 1;
				else
					deerCaughtEfficiency = 0;
					
				pumaAnimator.SetBool("DeerKill", true);
				SetGameState("gameStateFeeding1");
			}
			break;

		case "gameStateLeavingGameplay":
			// zoom up to high in the air
			fadeTime = 2f;
			guiFlybySpeed = (Time.time - stateStartTime) / fadeTime;
			SelectCameraPosition("cameraPosGui", -120f, fadeTime, "mainCurveSBackward", "curveRotXLogarithmicBackwardsSecondHalf"); 
			if (Time.time >= stateStartTime + fadeTime) {
				guiFlybySpeed = 1f;
				ResetAnimations();
				SetGameState("gameStateGui");
			}
			break;	
	
		//------------------------------
		// Feeding States
		//
		// puma has caught a deer
		// kills it and feeds on it
		//------------------------------

		case "gameStateFeeding1":
			// deer and puma slide to a stop as camera swings around to front
			fadeTime = 1.3f;
			SelectCameraPosition("cameraPosCloseup", -160f, fadeTime, "mainCurveSBackward", "curveRotXLogarithmic"); 
			
			if (Time.time < stateStartTime + fadeTime) {
				// puma and deer slide to a stop
				float percentDone = 1f - ((Time.time - stateStartTime) / fadeTime);
				float pumaMoveDistance = 1f * Time.deltaTime * pumaChasingSpeed * percentDone * 1.1f;
				pumaX += (Mathf.Sin(deerCaughtmainHeading*Mathf.PI/180) * pumaMoveDistance);
				pumaZ += (Mathf.Cos(deerCaughtmainHeading*Mathf.PI/180) * pumaMoveDistance);
				// during slide move deer to correct position 
				percentDone = ((Time.time - stateStartTime) / fadeTime);
				if ((deerCaughtFinalHeading > deerCaughtHeading) && (deerCaughtFinalHeading - deerCaughtHeading > 180))
					deerCaughtHeading += 360;
				else if ((deerCaughtHeading > deerCaughtFinalHeading) && (deerCaughtHeading - deerCaughtFinalHeading > 180))
					deerCaughtFinalHeading += 360;
				if (deerCaughtFinalHeading > deerCaughtHeading)
					caughtDeer.heading = deerCaughtHeading + ((deerCaughtFinalHeading - deerCaughtHeading) * percentDone);
				else
					caughtDeer.heading = deerCaughtHeading - ((deerCaughtHeading - deerCaughtFinalHeading) * percentDone);
				if (caughtDeer.heading < 0)
					caughtDeer.heading += 360;
				if (caughtDeer.heading >= 360)
					caughtDeer.heading -= 360;
				float deerX = pumaX + (deerCaughtOffsetX * (1f - percentDone)) + (deerCaughtFinalOffsetX * percentDone);
				float deerY = caughtDeer.gameObj.transform.position.y;
				float deerZ = pumaZ + (deerCaughtOffsetZ * (1f - percentDone)) + (deerCaughtFinalOffsetZ * percentDone);
				caughtDeer.gameObj.transform.rotation = Quaternion.Euler(0, caughtDeer.heading, 0);
				//System.Console.WriteLine("update heading: " + caughtDeer.heading.ToString());	
				caughtDeer.gameObj.transform.position = new Vector3(deerX, deerY, deerZ);
			}
			else {
				float deerX = pumaX + deerCaughtFinalOffsetX;
				float deerY = caughtDeer.gameObj.transform.position.y;
				float deerZ = pumaZ + deerCaughtFinalOffsetZ;
				caughtDeer.gameObj.transform.rotation = Quaternion.Euler(0, deerCaughtFinalHeading, 0);
				caughtDeer.gameObj.transform.position = new Vector3(deerX, deerY, deerZ);
				SetGameState("gameStateFeeding2");
			}
			break;

		case "gameStateFeeding2":
			// brief pause
			fadeTime = 1.3f;
			if (Time.time >= stateStartTime + fadeTime) {
				SetGameState("gameStateFeeding3");
			}
			break;

		case "gameStateFeeding3":
			// camera slowly lifts as puma feeds on deer
			fadeTime = 5f;
			SelectCameraPosition("cameraPosEating", 1000000f, fadeTime, "mainCurveSBackward", "curveRotXLinear"); 
			if (Time.time >= stateStartTime + fadeTime) {
				SetGameState("gameStateFeeding4");
			}
			break;
			
		case "gameStateFeeding4":
			// camera spins slowly around puma as it feeds
			if (Time.time >= stateStartTime + 0.1f) {
				float spinningRotOffsetY = cameraController.GetCurrentRotOffsetY() - (Time.deltaTime + 0.03f);
				if (spinningRotOffsetY < -180f)
					spinningRotOffsetY += 360f;
				cameraController.SelectTargetPosition("cameraPosEating", spinningRotOffsetY, 0f, "mainCurveNull", "curveRotXNull"); 
			}
			break;
					
		case "gameStateFeeding5":
			// camera swings back into position for stalking
			fadeTime = 2.4f;
			pumaAnimator.SetBool("DeerKill", false);
			SelectCameraPosition("cameraPosHigh", 0f, fadeTime, "mainCurveSBackward", "curveRotXLogarithmic"); 
			if (Time.time >= stateStartTime + fadeTime) {
				PlaceDeerPositions();
				ResetAnimations();
				scoringSystem.ClearLastKillInfo(selectedPuma);		
				inputControls.ResetControls();		
				SetGameState("gameStateStalking");
			}
			break;	
	
		//------------------------------
		// Died States
		//
		// puma has died
		// returns to overlay display
		//------------------------------

		case "gameStateDied1":
			// camera swings around to front of puma
			fadeTime = 3f;
			SelectCameraPosition("cameraPosCloseup", -160f, fadeTime, "mainCurveSBackward", "curveRotXLogarithmic"); 
			if (Time.time >= stateStartTime + fadeTime) {
				SetGameState("gameStateDied2");
			}
			break;

		case "gameStateDied2":
			// brief pause
			fadeTime = 0.2f;
			if (Time.time >= stateStartTime + fadeTime) {
				SetGameState("gameStateDied3");
			}
			break;

		case "gameStateDied3":
			// camera lifts slowly away from puma
			fadeTime = 5f;
			SelectCameraPosition("cameraPosEating", 1000000f, fadeTime, "mainCurveSBackward", "curveRotXLinear"); 
			if (Time.time >= stateStartTime + fadeTime) {
				SetGameState("gameStateDied4");
			}
			break;
			
		case "gameStateDied4":
			// camera spins slowly around puma
			if (Time.time >= stateStartTime + 0.1f) {
				float spinningRotOffsetY = cameraController.GetCurrentRotOffsetY() - (Time.deltaTime + 0.03f);
				if (spinningRotOffsetY < -180f)
					spinningRotOffsetY += 360f;
				cameraController.SelectTargetPosition("cameraPosEating", spinningRotOffsetY, 0f, "mainCurveNull", "curveRotXNull"); 
			}
			break;
					

		//------------------
		// Error Check
		//------------------
			
		default:
			Debug.Log("ERROR - LevelManager.Update() got bad state: " + gameState);
			break;
		}		
		
		//===============
		// Update Puma
		//===============
			
		float distance = 0f;
		pumaHeading = mainHeading;
	
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftControl)) {
			// filter out the input when manual camera moves are in progress - DEV ONLY
		}		
		else if (gameState == "gameStateGui" || gameState == "gameStateLeavingGameplay" || gameState == "gameStateLeavingGui") {
			// process automatic puma walking during GUI state
			if ((gameState != "gameStateLeavingGui") || (Time.time - stateStartTime < 1.8f))
				pumaAnimator.SetBool("GuiMode", true);
			distance = guiFlybySpeed * Time.deltaTime  * 12f * speedOverdrive;
			pumaX += (Mathf.Sin(mainHeading*Mathf.PI/180) * distance);
			pumaZ += (Mathf.Cos(mainHeading*Mathf.PI/180) * distance);
		}	
		else if (gameState == "gameStateStalking") {	
			// main stalking state
			float rotationSpeed = 100f;
			distance = inputControls.GetInputVert() * Time.deltaTime  * pumaStalkingSpeed * speedOverdrive;
			mainHeading += inputControls.GetInputHorz() * Time.deltaTime * rotationSpeed;
			pumaHeading = mainHeading + pumaHeadingOffset;
			pumaX += (Mathf.Sin(pumaHeading*Mathf.PI/180) * distance);
			pumaZ += (Mathf.Cos(pumaHeading*Mathf.PI/180) * distance);
			scoringSystem.PumaHasWalked(selectedPuma, distance);
			if (scoringSystem.GetPumaHealth(selectedPuma) == 0f)
				SetGameState("gameStateDied1");			
		}
		else if (gameState == "gameStateChasing") {
			// main chasing state
			float rotationSpeed = 150f;
			distance = inputControls.GetInputVert() * Time.deltaTime  * pumaChasingSpeed * speedOverdrive;
			float travelledDistance = (scoringSystem.GetPumaHealth(selectedPuma) > 0.05f) ? distance : distance * (scoringSystem.GetPumaHealth(selectedPuma) / 0.05f);
			mainHeading += inputControls.GetInputHorz() * Time.deltaTime * rotationSpeed;
			pumaHeading = mainHeading + pumaHeadingOffset;
			pumaX += (Mathf.Sin(pumaHeading*Mathf.PI/180) * travelledDistance);
			pumaZ += (Mathf.Cos(pumaHeading*Mathf.PI/180) * travelledDistance);
			scoringSystem.PumaHasRun(selectedPuma, distance);
			if (scoringSystem.GetPumaHealth(selectedPuma) == 0f)
				SetGameState("gameStateDied1");			
		}
		
		pumaAnimator.SetBool("GuiMode", false);
		pumaAnimator.SetFloat("Distance", distance);

		// calculate puma rotX based on terrain in front and behind
		float pumaRotX;
		float offsetDistance = 1f;
		float pumaAheadX = pumaX + (Mathf.Sin(pumaHeading*Mathf.PI/180) * offsetDistance * 1f);
		float pumaAheadZ = pumaZ + (Mathf.Cos(pumaHeading*Mathf.PI/180) * offsetDistance * 1f);
		float pumaBehindX = pumaX + (Mathf.Sin(pumaHeading*Mathf.PI/180) * offsetDistance * -1f);
		float pumaBehindZ = pumaZ + (Mathf.Cos(pumaHeading*Mathf.PI/180) * offsetDistance * -1f);
		pumaRotX = GetAngleFromOffset(0, GetTerrainHeight(pumaAheadX, pumaAheadZ), offsetDistance * 2f, GetTerrainHeight(pumaBehindX, pumaBehindZ)) - 90f;
			
		// update puma obj
		pumaY = GetTerrainHeight(pumaX, pumaZ);
		pumaObj.transform.position = new Vector3(pumaX, pumaY, pumaZ);			
		pumaObj.transform.rotation = Quaternion.Euler(pumaRotX, (pumaHeading - 180f), 0);
	
		//================
		// Update Camera
		//================
			
		cameraController.UpdateCameraPosition(pumaX, pumaY, pumaZ, mainHeading);
			
		//================
		// Update Deer
		//================

		UpdateDeerHeading(buck);
		UpdateDeerHeading(doe);
		UpdateDeerHeading(fawn);
		UpdateDeerPosition(buck);
		UpdateDeerPosition(doe);
		UpdateDeerPosition(fawn);
		
		//================================
		// Leap-Frog the Ground Planes
		//================================
						
		for (int i = 0; i < terrainArray.Length; i++) {
			float terrainX = terrainArray[i].transform.position.x;
			float terrainZ = terrainArray[i].transform.position.z;
			
			if (pumaX - terrainX > 3000) {
				terrainX += 4000;
			}
			else if (terrainX - pumaX > 1000) {
				terrainX -= 4000;
			}

			if (pumaZ - terrainZ > 3000) {
				terrainZ += 4000;
			}
			else if (terrainZ - pumaZ > 1000) {
				terrainZ -= 4000;
			}
			
			terrainArray[i].transform.position = new Vector3 (terrainX, 0, terrainZ);
		}
		
	}

	//===================================
	//===================================
	//		DEER HANDLING
	//===================================
	//===================================
	
	void UpdateDeerHeading(DeerClass deer)
	{
		if (deer.turnRate == 0 && deer.forwardRate == 0)
			return;
		
		if (Time.time > deer.nextTurnTime && deer.turnRate != 0f) {
			
			float randVal = Random.Range(0, 3);

			if (randVal < 1.0f)
				deer.targetHeading -= deer.turnRate * Random.Range(0.5f, 1f);
			else if (randVal < 2.0f)
				deer.targetHeading += 0;
			else
				deer.targetHeading += deer.turnRate * Random.Range(0.5f, 1f);				
					
			if (deer.targetHeading < 0)
				deer.targetHeading += 360;
			if (deer.targetHeading >= 360)
				deer.targetHeading -= 360;
			
			// limit to running away from puma

			float pumaDeerAngle = GetAngleFromOffset(pumaObj.transform.position.x, pumaObj.transform.position.z, deer.gameObj.transform.position.x, deer.gameObj.transform.position.z);

			if (pumaDeerAngle < 0)
				pumaDeerAngle += 360;
			if (pumaDeerAngle >= 360)
				pumaDeerAngle -= 360;
			
			if (pumaDeerAngle > deer.targetHeading) {
				if ((pumaDeerAngle - deer.targetHeading > 70f) && (pumaDeerAngle - deer.targetHeading <= 180f)) {
					deer.targetHeading = pumaDeerAngle - 70f;
				}
				else if ((pumaDeerAngle - deer.targetHeading >= 180f) && (pumaDeerAngle - deer.targetHeading <= 290f)) {
					deer.targetHeading = pumaDeerAngle - 290f;
					if (deer.targetHeading < 0)
						deer.targetHeading += 360;
				}
			}	
			else if (deer.targetHeading > pumaDeerAngle) {
				if ((deer.targetHeading - pumaDeerAngle > 70f) && (deer.targetHeading - pumaDeerAngle <= 180f)) {
					deer.targetHeading = pumaDeerAngle + 70f;
				}
				else if ((deer.targetHeading - pumaDeerAngle >= 180f) && (deer.targetHeading - pumaDeerAngle <= 290f)) {
					deer.targetHeading = pumaDeerAngle + 290f;
					if (pumaDeerAngle >= 360)
						pumaDeerAngle -= 360;
				}
			}
			
			deer.nextTurnTime = Time.time + Random.Range(0.2f, 0.4f);			

		}
		
		// slew the change in heading
		
		float slewRate = 100f * Time.deltaTime;
		
		if (newChaseFlag == true) {
			slewRate *= 3;
			if (Time.time - stateStartTime > 0.3f)	
				newChaseFlag = false;
		}
			
		if (deer.heading > deer.targetHeading) {
			if ((deer.heading - deer.targetHeading) < 180)
				deer.heading -= (deer.heading - deer.targetHeading > slewRate) ? slewRate : deer.heading - deer.targetHeading;
			else
				deer.heading += slewRate;
		}
		else if (deer.heading < deer.targetHeading) {
			if ((deer.targetHeading - deer.heading) < 180)
				deer.heading += (deer.targetHeading - deer.heading > slewRate) ? slewRate : deer.targetHeading - deer.heading;
			else
				deer.heading -= slewRate;
		}

		if (deer.heading < 0)
			deer.heading += 360;
		if (deer.heading >= 360)
			deer.heading -= 360;
					
		deer.gameObj.transform.rotation = Quaternion.Euler(0, deer.heading, 0);
		
		//System.Console.WriteLine("DEER HEADING: " + deer.heading.ToString());	
	}

	void UpdateDeerPosition(DeerClass deer)
	{
		//if (deer.type == "Buck")
			//offsetY = deer.gameObj.GetComponent<BuckRunScript>().GetOffsetY();
		//else if (deer.type == "Doe")	
			//offsetY = deer.gameObj.GetComponent<DoeRunScript>().GetOffsetY();
		//else if (deer.type == "Fawn")	
			//offsetY = deer.gameObj.GetComponent<FawnRunScript>().GetOffsetY();

		float forwardRate = deer.forwardRate;
		
		if (newChaseFlag) 
			forwardRate = deer.forwardRate * ((Time.time - stateStartTime) / 0.3f);
		
		float deerX = deer.gameObj.transform.position.x + (Mathf.Sin(deer.heading*Mathf.PI/180) * Time.deltaTime  * forwardRate);
		float deerZ = deer.gameObj.transform.position.z + (Mathf.Cos(deer.heading*Mathf.PI/180) * Time.deltaTime  * forwardRate);
		float deerY = deer.baseY + GetTerrainHeight(deerX, deerZ);

		deer.gameObj.transform.position = new Vector3(deerX, deerY, deerZ);
	}

	void PlaceDeerPositions()
	{
		buck.heading = buck.targetHeading = Random.Range(0f,360f);
		buck.gameObj.transform.rotation = Quaternion.Euler(0, buck.heading, 0);
		doe.heading = doe.targetHeading = Random.Range(0f,360f);		
		doe.gameObj.transform.rotation = Quaternion.Euler(0, doe.heading, 0);
		fawn.heading = fawn.targetHeading = Random.Range(0f,360f);	
		fawn.gameObj.transform.rotation = Quaternion.Euler(0, fawn.heading, 0);

		float randomDirection = Random.Range(0f,360f);	
		float deerDistance = Random.Range(80f,120f);  //Random.Range(100f,250f); //Random.Range(200f,350f);
		float positionVariance = 10f;
		float newX = pumaX + (Mathf.Sin(randomDirection*Mathf.PI/180) * deerDistance);
		float newZ = pumaZ + (Mathf.Cos(randomDirection*Mathf.PI/180) * deerDistance);

		float deerX;
		float deerZ;
		float deerY;
		
		deerX = newX + Random.Range(-positionVariance, positionVariance);
		deerZ = newZ + Random.Range(-positionVariance, positionVariance);
		deerY = buck.baseY + GetTerrainHeight(deerX, deerZ);
		buck.gameObj.transform.position = new Vector3(deerX, deerY, deerZ);

		deerX = newX + Random.Range(-positionVariance, positionVariance);
		deerZ = newZ + Random.Range(-positionVariance, positionVariance);
		deerY = doe.baseY + GetTerrainHeight(deerX, deerZ);
		doe.gameObj.transform.position = new Vector3(deerX, deerY, deerZ);

		deerX = newX + Random.Range(-positionVariance, positionVariance);
		deerZ = newZ + Random.Range(-positionVariance, positionVariance);
		deerY = fawn.baseY + GetTerrainHeight(deerX, deerZ);
		fawn.gameObj.transform.position = new Vector3(deerX, deerY, deerZ);

		//System.Console.WriteLine("PLACE DEER POSITIONS");	
		//System.Console.WriteLine("positive variance: " + positionVariance.ToString());	
		//System.Console.WriteLine("buck X: " + buck.gameObj.transform.position.x.ToString());	

	}

	//===================================
	//===================================
	//		UTILITIES
	//===================================
	//===================================
	
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
	
	void CalculateFrameRate()	// for frame rate display
	{
		int currentMsec = (int)(Time.time * 1000);

		if (frameCount == 0 || currentMsec - frameFirstTime > 1500) {
			frameCount = 1;
			frameFirstTime = framePrevTime = currentMsec;
		}
		else {
			frameCurrentDuration = currentMsec - framePrevTime;
			frameAverageDuration =  (currentMsec - frameFirstTime) / frameCount;
			framePrevTime = currentMsec;
			frameCount++;
		}
	}

	public float GetTerrainHeight(float x, float z)
	{
		float terrainX;
		float terrainZ;
	
		terrainX = terrain1A.transform.position.x;
		terrainZ = terrain1A.transform.position.z;		
		if (x >= terrainX && x < terrainX + 2000 && z >= terrainZ && z < terrainZ + 2000) {
			return  terrain1A.SampleHeight(new Vector3(x, 0, z));
		}
			
		terrainX = terrain1B.transform.position.x;
		terrainZ = terrain1B.transform.position.z;		
		if (x >= terrainX && x < terrainX + 2000 && z >= terrainZ && z < terrainZ + 2000) {
			return  terrain1B.SampleHeight(new Vector3(x, 0, z));
		}
			
		terrainX = terrain1C.transform.position.x;
		terrainZ = terrain1C.transform.position.z;		
		if (x >= terrainX && x < terrainX + 2000 && z >= terrainZ && z < terrainZ + 2000) {
			return  terrain1C.SampleHeight(new Vector3(x, 0, z));
		}
			
		terrainX = terrain1D.transform.position.x;
		terrainZ = terrain1D.transform.position.z;		
		if (x >= terrainX && x < terrainX + 2000 && z >= terrainZ && z < terrainZ + 2000) {
			return  terrain1D.SampleHeight(new Vector3(x, 0, z));
		}
			
		return 0f;
	}
	
	public float GetStartingTerrainX(int terrainNum)
	{
		switch (terrainNum) {
		case 0:
			return -2000f;
		case 1:
			return 0f;
		case 2:
			return -2000f;
		case 3:
			return 0f;
		}	
		return 0f;
	}
	
	public float GetStartingTerrainZ(int terrainNum)
	{
		switch (terrainNum) {
		case 0:
			return 0f;
		case 1:
			return 0f;
		case 2:
			return -2000f;
		case 3:
			return -2000f;
		}	
		return 0f;
	}
	
	public float GetTerrainMinX()
	{
		float terrainMinX = (terrain1A.transform.position.x < terrain1B.transform.position.x) ? terrain1A.transform.position.x : terrain1B.transform.position.x;
		terrainMinX = (terrainMinX < terrain1C.transform.position.x) ? terrainMinX : terrain1C.transform.position.x;
		terrainMinX = (terrainMinX < terrain1D.transform.position.x) ? terrainMinX : terrain1D.transform.position.x;
		return terrainMinX;
	}

	public float GetTerrainMinZ()
	{
		float terrainMinZ = (terrain1A.transform.position.z < terrain1B.transform.position.z) ? terrain1A.transform.position.z : terrain1B.transform.position.z;
		terrainMinZ = (terrainMinZ < terrain1C.transform.position.z) ? terrainMinZ : terrain1C.transform.position.z;
		terrainMinZ = (terrainMinZ < terrain1D.transform.position.z) ? terrainMinZ : terrain1D.transform.position.z;
		return terrainMinZ;
	}

	public float GetTerrainMaxX()
	{
		float terrainMaxX = (terrain1A.transform.position.x > terrain1B.transform.position.x) ? terrain1A.transform.position.x : terrain1B.transform.position.x;
		terrainMaxX = (terrainMaxX > terrain1C.transform.position.x) ? terrainMaxX : terrain1C.transform.position.x;
		terrainMaxX = (terrainMaxX > terrain1D.transform.position.x) ? terrainMaxX : terrain1D.transform.position.x;
		return terrainMaxX + 2000f;
	}

	public float GetTerrainMaxZ()
	{
		float terrainMaxZ = (terrain1A.transform.position.z > terrain1B.transform.position.z) ? terrain1A.transform.position.z : terrain1B.transform.position.z;
		terrainMaxZ = (terrainMaxZ > terrain1C.transform.position.z) ? terrainMaxZ : terrain1C.transform.position.z;
		terrainMaxZ = (terrainMaxZ > terrain1D.transform.position.z) ? terrainMaxZ : terrain1D.transform.position.z;
		return terrainMaxZ + 2000f;
	}

	void ResetAnimations()
	{
		buckAnimator.SetBool("Looking", false);
		buckAnimator.SetBool("Running", false);
		buckAnimator.SetBool("Die", false);
		
		doeAnimator.SetBool("Looking", false);
		doeAnimator.SetBool("Running", false);
		doeAnimator.SetBool("Die", false);
		
		fawnAnimator.SetBool("Looking", false);
		fawnAnimator.SetBool("Running", false);
		fawnAnimator.SetBool("Die", false);
	
		pumaAnimator.SetBool("Chasing", false);
		pumaAnimator.SetBool("DeerKill", false);
	}			
	

}














