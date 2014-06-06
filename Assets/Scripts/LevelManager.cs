using UnityEngine;
using System.Collections;

/// Main handling of all level-based play
/// 

public class LevelManager : MonoBehaviour 
{
	private float speedOverdrive = 1.0f;
	
	public float displayVar1;
	public float displayVar2;
	public float displayVar3;
	
	// PUMA CHARACTERISTICS

	private float[] powerArray = new float[] {0.6f, 0.4f, 0.9f, 0.7f, 0.7f, 0.5f};
	private float[] speedArray = new float[] {0.90f, 0.80f, 0.55f, 0.45f, 0.20f, 0.10f};
	private float[] enduranceArray = new float[] {0.6f, 0.4f, 0.9f, 0.8f, 0.6f, 0.4f};
	private float[] stealthinessArray = new float[] {0.10f, 0.20f, 0.45f, 0.55f, 0.80f, 0.90f};
	
	private int selectedPuma = -1;

	// SCORING INFO
	
	private int[] bucksKilled = new int[] {2, 5, 0, 0, 1, 0};
	private int[] doesKilled = new int[] {3, 0, 2, 2, 0, 1};
	private int[] fawnsKilled = new int[] {1, 0, 3, 0, 5, 2};
	//private int[] bucksKilled = new int[] {0, 0, 0, 0, 0, 0};
	//private int[] doesKilled = new int[] {0, 0, 0, 0, 0, 0};
	//private int[] fawnsKilled = new int[] {0, 0, 0, 0, 0, 0};

	public float meatTotalEaten;
	public float meatMaxLevel;
	public float meatJustEaten;
	
	private float caloriesPerMeterChasing = 75f;
	private float caloriesPerMeterStalking = 75f * 0.25f;

	private float pumaMaxCalories = 175000f;
	private float[] pumaPoints = new float[] {175000f*0.5f, 175000f*0.5f, 1f  *  175000f*0.5f, 1f    *     175000f*0.5f, 175000f*0.5f, 175000f*0.5f};

	// for each prey type, totals for energy spent and calories eaten by pumas 0-5; TOTAL for all pumas uses index 6
	private float[] buckExpenditures = new float[] {5f, 4f, 6f, 4f, 6f, 3f, 5f};
	private float[] buckCalories = new float[] {4f, 5f, 3f, 6f, 4f, 5f, 3f};
	private float[] doeExpenditures = new float[] {4f, 6f, 3f, 4f, 6f, 4f, 5f};
	private float[] doeCalories = new float[] {4f, 5f, 4f, 6f, 4f, 5f, 3f};
	private float[] fawnExpenditures = new float[] {5f, 4f, 6f, 3f, 5f, 4f, 5f};
	private float[] fawnCalories = new float[] {4f, 5f, 3f, 5f, 4f, 6f, 3f};

	//private float[] buckExpenditures = new float[] {0f, 0f, 0f, 0f, 0f, 0f, 0f};
	//private float[] buckCalories = new float[] {0f, 0f, 0f, 0f, 0f, 0f, 0f};
	//private float[] doeExpenditures = new float[] {0f, 0f, 0f, 0f, 0f, 0f, 0f};
	//private float[] doeCalories = new float[] {0f, 0f, 0f, 0f, 0f, 0f, 0f};
	//private float[] fawnExpenditures = new float[] {0f, 0f, 0f, 0f, 0f, 0f, 0f};
	//private float[] fawnCalories = new float[] {0f, 0f, 0f, 0f, 0f, 0f, 0f};

	private float[] recentCalories = new float[] {0f, 0f, 0f, 0f, 0f, 0f};
	
	private float previousMeatLevel;
	private float previousPumaHealth;
	private float caloriesGained;
	private int lastCaughtDeerType;
	
	// FRAMERATE INFO

	private int frameCount = 0;
	private int frameFirstTime;
	private int framePrevTime;
	public int frameCurrentDuration;
	public int frameAverageDuration;

	// GROUND PLANES

	public Terrain terrain1;
	public Terrain terrain2;
	public Terrain terrain3;
	public Terrain terrain4;
	private Terrain[] terrainArray;

	// PUMA

	public GameObject pumaObj;
	private float pumaX;
	private float pumaY;
	private float pumaZ;
	private float pumaHeading = 0f;
	private float pumaHeadingOffset = 0f;
	private float pumaStalkingSpeed = 22f * 0.66f;
	private float pumaChasingSpeed = 32f * 0.66f;
	private float defaultPumaChasingSpeed = 32f * 0.66f;
	private float chaseTriggerDistance = 40f * 0.66f;
	private float defaultChaseTriggerDistance = 40f * 0.66f;
	private float deerCaughtFinalOffsetFactor0 = 1f * 0.66f;
	private float deerCaughtFinalOffsetFactor90 = 1f;
	private float inputVert = 0f;
	private float inputHorz = 0f;
	
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
	
	private float buckDefaultForwardRate = 30f * 0.66f;
	private float buckDefaultTurnRate = 22.5f * 0.66f;
	private float doeDefaultForwardRate = 29f * 0.66f;
	private float doeDefaultTurnRate = 22.5f * 0.66f;
	private float fawnDefaultForwardRate = 28f * 0.66f;
	private float fawnDefaultTurnRate = 22.5f * 0.66f;

	public DeerClass buck;
	public DeerClass doe;
	public DeerClass fawn;

	// CAMERA

	private float cameraX;
	public float cameraY;
	private float cameraZ;
	public float cameraRotX;
	public float cameraRotY;
	private float cameraRotZ;
	public float cameraDistance;
	private float cameraRotOffsetY;
	private float previousCameraRotOffsetY;
	
	private float highCameraY = 5.7f;
	private float highCameraRotX = 12.8f;
	private float highCameraDistance = 8.6f;
	
	private float medCameraY = 4f;
	private float medCameraRotX = 4f;
	private float medCameraDistance = 7.5f;
	
	private float lowCameraY = 3f;
	private float lowCameraRotX = -2f;
	private float lowCameraDistance = 7f;

	private float closeupCameraY = 2.75f;
	private float closeupCameraRotX = 2.75f;
	private float closeupCameraDistance = 6.5f;

	private float eatingCameraY = 9f;
	private float eatingCameraRotX = 30f;
	private float eatingCameraDistance = 9f;

	private float guiCameraY = 90f;
	private float guiCameraRotX = 33f; //45f;
	private float guiCameraDistance = 48f;

	private float previousCameraY = 0f;
	private float previousCameraRotX = 0f;
	private float previousCameraDistance = 0f;

	// STATES

	public string gameState;
	private float stateStartTime;
	private bool zoomInProgress = false;

	private bool newChaseFlag = false;

	private DeerClass caughtDeer;
	private float deerCaughtHeading;
	private float deerCaughtFinalHeading;
	private bool deerCaughtHeadingLeft = false;
	private float deerCaughtOffsetX;
	private float deerCaughtFinalOffsetX;
	private float deerCaughtOffsetZ;
	private float deerCaughtFinalOffsetZ;
	private float deerCaughtCameraRotY;
	private float deerCaughtNextFrameTime;
	public int deerCaughtEfficiency = 0;
	
	// NAVIGATION CONTROLS

	enum Nav {Off, Inc, Full, Dec};
	
	private Nav navStateLeft = Nav.Off;
	private Nav navStateRight = Nav.Off;
	private Nav navStateForward = Nav.Off;
	private Nav navStateBack = Nav.Off;

	private Nav navStateForwardLeft = Nav.Off;
	private Nav navStateForwardRight = Nav.Off;
	private Nav navStateBackLeft = Nav.Off;
	private Nav navStateBackRight = Nav.Off;
	
	private float navValLeft = 0;
	private float navValRight = 0;
	private float navValForward = 0;
	private float navValBack = 0;

	private float navValForwardLeft = 0;
	private float navValForwardRight = 0;
	private float navValBackLeft = 0;
	private float navValBackRight = 0;
	
	private bool leftKey = false;
	private bool rightKey = false;
	private bool forwardKey = false;
	private bool backKey = false;

	private Nav newNavState;
	private float newNavVal;
	
	public bool forwardClicked = false;
	public bool backClicked = false;
	public bool sideLeftClicked = false;
	public bool sideRightClicked = false;
	public bool diagLeftClicked = false;
	public bool diagRightClicked = false;
	
	public bool leftArrowMouseEvent = false;
	public bool rightArrowMouseEvent = false;

	// ANIMATORS
	
	public Animator pumaAnimator;
	public Animator buckAnimator;
	public Animator doeAnimator;
	public Animator fawnAnimator;
	

		
	//=======================================================
	//
	//	INITIALIZATION
	//
	//=======================================================
	
    void Awake() {
        Application.targetFrameRate = 120;
    }

	void Start () 
	{	
		// scoring system
		meatTotalEaten = 0f;
		meatMaxLevel = 1000f;
		caloriesGained = 0f;

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
		terrainArray = new Terrain[4];
		terrainArray[0] = terrain1;		
		terrainArray[1] = terrain2;		
		terrainArray[2] = terrain3;		
		terrainArray[3] = terrain4;		
		
		InitLevel();
	}
	
	void InitLevel()
	{
		gameState = "gameStateGui";
		stateStartTime = Time.time;

		cameraY = guiCameraY;
		cameraRotX = guiCameraRotX;
		cameraRotOffsetY = -120f;
		cameraDistance = guiCameraDistance;
	
		pumaX = 0f;
		pumaY = 0f;
		pumaZ = 100f;			
		cameraX = 0f;
		cameraZ = 0f;	
		cameraRotY = Random.Range(0f, 360f);
		cameraRotZ = 0f;
		
		cameraX = pumaX - (Mathf.Sin(cameraRotY*Mathf.PI/180) * cameraDistance);
		cameraZ = pumaZ - (Mathf.Cos(cameraRotY*Mathf.PI/180) * cameraDistance);

		pumaObj.transform.position = new Vector3(pumaX, pumaY, pumaZ);
		Camera.main.transform.position = new Vector3(cameraX, cameraY, cameraZ);
		Camera.main.transform.rotation = Quaternion.Euler(cameraRotX, cameraRotY, cameraRotZ);
		
	}
	
	
	public void SetGameState(string newGameState)
	{
		gameState = newGameState;
		stateStartTime = Time.time;
		
		if (newGameState == "gameStateLeavingGui") {
			PlaceDeerPositions();
			ResetAnimations();
		}
		
		previousCameraY = cameraY;
		previousCameraRotX = cameraRotX;
		previousCameraDistance = cameraDistance;
	}

	public bool IsStalkingState()
	{
		return (gameState == "gameStateStalking") ? true : false;
	}

	public bool IsChasingState()
	{
		return (gameState == "gameStateChasing") ? true : false;
	}

	public bool IsCaughtState()
	{
		return (gameState == "gameStateCaught1") ? true : false;
	}

	public float GetCaloriesExpended()
	{
		return recentCalories[selectedPuma];
	}
	
	public float GetCaloriesGained()
	{
		return caloriesGained;
	}
	
	public float GetMeatLevel()
	{
		return meatTotalEaten / meatMaxLevel;
	}
		
	public float GetPreviousMeatLevel()
	{
		return previousMeatLevel;
	}
		
	public float GetMeatJustEaten()
	{
		return meatJustEaten;
	}
		
	public void SetSelectedPuma(int selection)
	{
		selectedPuma = selection;
		
		pumaChasingSpeed = defaultPumaChasingSpeed + (10f * speedArray[selectedPuma]);
		chaseTriggerDistance = defaultChaseTriggerDistance - (20f * stealthinessArray[selectedPuma]);
	}
	
	public int GetDeerType()
	{
		return lastCaughtDeerType;
	}
		
	public float GetPumaHealth(int pumaNum)
	{
		if (pumaNum == -1)
			return -1f;

		return pumaPoints[pumaNum] / pumaMaxCalories;
	}
	
	public float GetPreviousPumaHealth()
	{
		return previousPumaHealth;
	}
	
	public void PumaAddCalories(float caloriesToAdd)
	{
		if (selectedPuma == -1)
			return;
	
		pumaPoints[selectedPuma] += caloriesToAdd;

		if (pumaPoints[selectedPuma] > pumaMaxCalories)
			pumaPoints[selectedPuma] = pumaMaxCalories;
		if (pumaPoints[selectedPuma] < 0) {
			pumaPoints[selectedPuma] = 0;
			SetGameState("gameStateDied1");
		}
	}
	
	
	public int GetBucksKilled(int pumaNum)
	{
		return bucksKilled[pumaNum];
	}
	
	public int GetDoesKilled(int pumaNum)
	{
		return doesKilled[pumaNum];
	}
	
	public int GetFawnsKilled(int pumaNum)
	{
		return fawnsKilled[pumaNum];
	}
	
	public float GetBuckExpenditures(int pumaNum)
	{
		return buckExpenditures[pumaNum];
	}
	
	public float GetDoeExpenditures(int pumaNum)
	{
		return doeExpenditures[pumaNum];
	}
	
	public float GetFawnExpenditures(int pumaNum)
	{
		return fawnExpenditures[pumaNum];
	}
	
	public float GetBuckCalories(int pumaNum)
	{
		return buckCalories[pumaNum];
	}
	
	public float GetDoeCalories(int pumaNum)
	{
		return doeCalories[pumaNum];
	}
	
	public float GetFawnCalories(int pumaNum)
	{
		return fawnCalories[pumaNum];
	}
	
	
	
	
	//=======================================================
	//
	//	PERIODIC UPDATE
	//
	//=======================================================
	
	void Update() 
	{	
		//pumaAnimator.SetLayerWeight(1, 1f);
	
		if (pumaObj == null || buck == null || doe == null || fawn == null)
			return;
			
		CalculateFrameRate();
			
		ProcessNavControls();

		//===========================
		// Update Game-State Logic
		//===========================
			
		float pumaDeerDistance1 = Vector3.Distance(pumaObj.transform.position, buck.gameObj.transform.position);
		float pumaDeerDistance2 = Vector3.Distance(pumaObj.transform.position, doe.gameObj.transform.position);
		float pumaDeerDistance3 = Vector3.Distance(pumaObj.transform.position, fawn.gameObj.transform.position);		
	
		float fadeTime;
		float fadePercentComplete;
		float cameraRotPercentDone;
		float guiFlybySpeed = 0f;

		switch (gameState) {
		
		case "gameStateGui":
			guiFlybySpeed = 1f;
			break;
	
		case "gameStateEnteringGui":
			cameraY = guiCameraY;
			cameraRotX = guiCameraRotX;
			cameraDistance = guiCameraDistance;
			SetGameState("gameStateGui");
			break;	
	
		case "gameStateLeavingGui":
			fadeTime = 2.5f;
			if (Time.time - stateStartTime < (fadeTime * 0.5f)) {
				// 1st half
				guiFlybySpeed = 1f - (Time.time - stateStartTime) / fadeTime;
				fadePercentComplete = (Time.time - stateStartTime) / (fadeTime * 0.5f);
				fadePercentComplete = fadePercentComplete * fadePercentComplete;
				cameraY = previousCameraY + fadePercentComplete * ((closeupCameraY - previousCameraY) * 0.5f);
				//cameraRotX = previousCameraRotX + fadePercentComplete * ((highCameraRotX - previousCameraRotX) * 0.5f);
				cameraRotX = previousCameraRotX;
				cameraDistance = previousCameraDistance + fadePercentComplete * ((closeupCameraDistance - previousCameraDistance) * 0.5f);
			}
			else if (Time.time - stateStartTime < fadeTime) {
				// 2nd half
				guiFlybySpeed = 1f - (Time.time - stateStartTime) / fadeTime;
				fadePercentComplete = ((Time.time - stateStartTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);				
				fadePercentComplete = fadePercentComplete + (fadePercentComplete - (fadePercentComplete * fadePercentComplete));
				cameraY = previousCameraY + ((closeupCameraY - previousCameraY) * 0.5f) + fadePercentComplete * ((closeupCameraY - previousCameraY) * 0.5f);
				//cameraRotX = previousCameraRotX + ((highCameraRotX - previousCameraRotX) * 0.5f) + fadePercentComplete * ((highCameraRotX - previousCameraRotX) * 0.5f);
				cameraRotPercentDone = (float)((float)(Time.time - stateStartTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);
				cameraRotX = previousCameraRotX + ((closeupCameraRotX - previousCameraRotX) * cameraRotPercentDone * cameraRotPercentDone);
				cameraDistance = previousCameraDistance + ((closeupCameraDistance - previousCameraDistance) * 0.5f) + fadePercentComplete * ((closeupCameraDistance - previousCameraDistance) * 0.5f);
			}
			else {
				cameraY = closeupCameraY;
				cameraRotX = closeupCameraRotX;
				cameraDistance = closeupCameraDistance;
				SetGameState("gameStateCloseup");
			}
			break;
	
		case "gameStateCloseup":
			fadeTime = 0.1f;
			if (Time.time - stateStartTime < fadeTime) {
				// pause
			}
			else {
				forwardKey = false;
				backKey = false;		
				SetGameState("gameStateEnteringGameplay");
				//SetGameState("gameStateStalking");
			}
			break;	
	
		case "gameStateEnteringGameplay":
			fadeTime = 1.7f;

			if (Time.time - stateStartTime < fadeTime) { // implements trans as a reverse of leavingGui trans
				float backwardsTime = (stateStartTime + fadeTime) - (Time.time - stateStartTime);	
				if (backwardsTime - stateStartTime < (fadeTime * 0.5f)) {
					// 1st half
					fadePercentComplete = (backwardsTime - stateStartTime) / (fadeTime * 0.5f);
					fadePercentComplete = fadePercentComplete * fadePercentComplete;
					cameraY = highCameraY + fadePercentComplete * ((previousCameraY - highCameraY) * 0.5f);
					cameraRotOffsetY = 0f + fadePercentComplete * ((-120f - 0f) * 0.5f);
					cameraRotX = highCameraRotX;
					cameraDistance = highCameraDistance + fadePercentComplete * ((previousCameraDistance - highCameraDistance) * 0.5f);
				}
				else if (backwardsTime - stateStartTime < fadeTime) {
					// 2nd half
					fadePercentComplete = ((backwardsTime - stateStartTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);				
					fadePercentComplete = fadePercentComplete + (fadePercentComplete - (fadePercentComplete * fadePercentComplete));
					cameraY = highCameraY + ((previousCameraY - highCameraY) * 0.5f) + fadePercentComplete * ((previousCameraY - highCameraY) * 0.5f);
					cameraRotOffsetY = 0f + ((-120f - 0f) * 0.5f) + fadePercentComplete * ((-120f - 0f) * 0.5f);
					cameraRotPercentDone = (float)((float)(backwardsTime - stateStartTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);
					cameraRotX = highCameraRotX + ((previousCameraRotX - highCameraRotX) * cameraRotPercentDone);
					cameraDistance = highCameraDistance + ((previousCameraDistance - highCameraDistance) * 0.5f) + fadePercentComplete * ((previousCameraDistance - highCameraDistance) * 0.5f);
				}
				
			}

			else {
				cameraY = highCameraY;
				cameraRotX = highCameraRotX;
				cameraDistance = highCameraDistance;
				cameraRotOffsetY = 0;
				SetGameState("gameStateStalking");
				previousMeatLevel = GetMeatLevel();
				previousPumaHealth = GetPumaHealth(selectedPuma);
			}
			break;	
	
		case "gameStateLeavingGameplay":
			fadeTime = 2f;
			if (Time.time - stateStartTime < fadeTime) { // implements trans as a reverse of leavingGui trans
				float backwardsTime = (stateStartTime + fadeTime) - (Time.time - stateStartTime);	
				if (backwardsTime - stateStartTime < (fadeTime * 0.5f)) {
					// 1st half
					guiFlybySpeed = 1f - (backwardsTime - stateStartTime) / fadeTime;
					fadePercentComplete = (backwardsTime - stateStartTime) / (fadeTime * 0.5f);
					fadePercentComplete = fadePercentComplete * fadePercentComplete;
					cameraY = guiCameraY + fadePercentComplete * ((previousCameraY - guiCameraY) * 0.5f);
					if (previousCameraRotOffsetY > 60f) // constrain to within 180 degrees of -120 degrees (-120 is dest)
						previousCameraRotOffsetY -= 360f;
					if (previousCameraRotOffsetY < -300f)
						previousCameraRotOffsetY += 360f;
					cameraRotOffsetY = -120f + fadePercentComplete * ((previousCameraRotOffsetY - -120f) * 0.5f);
					cameraRotX = guiCameraRotX;
					cameraDistance = guiCameraDistance + fadePercentComplete * ((previousCameraDistance - guiCameraDistance) * 0.5f);
				}
				else if (backwardsTime - stateStartTime < fadeTime) {
					// 2nd half
					guiFlybySpeed = 1f - (backwardsTime - stateStartTime) / fadeTime;
					fadePercentComplete = ((backwardsTime - stateStartTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);				
					fadePercentComplete = fadePercentComplete + (fadePercentComplete - (fadePercentComplete * fadePercentComplete));
					cameraY = guiCameraY + ((previousCameraY - guiCameraY) * 0.5f) + fadePercentComplete * ((previousCameraY - guiCameraY) * 0.5f);
					if (previousCameraRotOffsetY > 60f) // constrain to within 180 degrees of -120 degrees (-120 is dest)
						previousCameraRotOffsetY -= 360f;
					if (previousCameraRotOffsetY < -300f)
						previousCameraRotOffsetY += 360f;
					cameraRotOffsetY = -120f + ((previousCameraRotOffsetY - -120f) * 0.5f) + fadePercentComplete * ((previousCameraRotOffsetY - -120f) * 0.5f);
					cameraRotPercentDone = (float)((float)(backwardsTime - stateStartTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);
					cameraRotX = guiCameraRotX + ((previousCameraRotX - guiCameraRotX) * cameraRotPercentDone * cameraRotPercentDone);
					cameraDistance = guiCameraDistance + ((previousCameraDistance - guiCameraDistance) * 0.5f) + fadePercentComplete * ((previousCameraDistance - guiCameraDistance) * 0.5f);
				}
				
			}
/*

			float overshootCameraRotX = previousCameraRotX + ((guiCameraRotX - previousCameraRotX) * 1.1f);
			if (Time.time - stateStartTime < (fadeTime * 0.5f)) {
				// 1st half
				guiFlybySpeed = 0f + (Time.time - stateStartTime) / fadeTime;
				fadePercentComplete = (Time.time - stateStartTime) / (fadeTime * 0.5f);
				fadePercentComplete = fadePercentComplete * fadePercentComplete;
				cameraY = previousCameraY + fadePercentComplete * ((guiCameraY - previousCameraY) * 0.5f);
				//cameraRotX = previousCameraRotX + fadePercentComplete * ((guiCameraRotX - previousCameraRotX) * 0.5f);
				float cameraRotPercentDone = (float)((float)(Time.time - stateStartTime)) / (fadeTime * 0.5f);
				cameraRotX = previousCameraRotX + ((overshootCameraRotX - previousCameraRotX) * cameraRotPercentDone);
				cameraDistance = previousCameraDistance + fadePercentComplete * ((guiCameraDistance - previousCameraDistance) * 0.5f);
			}
			else if (Time.time - stateStartTime < fadeTime) {
				// 2nd half
				guiFlybySpeed = 0f + (Time.time - stateStartTime) / fadeTime;
				fadePercentComplete = ((Time.time - stateStartTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);				
				fadePercentComplete = fadePercentComplete + (fadePercentComplete - (fadePercentComplete * fadePercentComplete));
				cameraY = previousCameraY + ((guiCameraY - previousCameraY) * 0.5f) + fadePercentComplete * ((guiCameraY - previousCameraY) * 0.5f);
				//cameraRotX = previousCameraRotX + ((guiCameraRotX - previousCameraRotX) * 0.5f) + fadePercentComplete * ((guiCameraRotX - previousCameraRotX) * 0.5f);
				float cameraRotPercentDone = (float)((float)(Time.time - stateStartTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);
				cameraRotX = overshootCameraRotX + ((guiCameraRotX - overshootCameraRotX) * cameraRotPercentDone);
				cameraDistance = previousCameraDistance + ((guiCameraDistance - previousCameraDistance) * 0.5f) + fadePercentComplete * ((guiCameraDistance - previousCameraDistance) * 0.5f);
			}
			
			*/
			
			
			else {
				cameraY = guiCameraY;
				cameraRotX = guiCameraRotX;
				cameraDistance = guiCameraDistance;
				ResetAnimations();
				SetGameState("gameStateGui");
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
				pumaHeadingOffset = 0;  // TEMP -- really should swing camera around
			}
			buck.forwardRate = 0f;
			buck.turnRate = 0f;
			doe.forwardRate = 0f;
			doe.turnRate = 0f;
			fawn.forwardRate = 0f;
			fawn.turnRate = 0f;
			break;
	
		case "gameStateChasing":
			float zoomTime = 1f;
			float zoomPercentComplete;
			if (Time.time - stateStartTime < zoomTime) {
				zoomInProgress = true;
				if (Time.time - stateStartTime < zoomTime*0.75f) {
					zoomPercentComplete = (Time.time - stateStartTime) / (zoomTime*0.75f);
					cameraY = highCameraY - ((highCameraY - medCameraY) * zoomPercentComplete);
					cameraRotX = highCameraRotX - ((highCameraRotX - medCameraRotX) * zoomPercentComplete);
					cameraDistance = highCameraDistance - ((highCameraDistance - medCameraDistance) * zoomPercentComplete);
				}
				else {
					zoomPercentComplete = (Time.time - stateStartTime - (zoomTime*0.75f)) / (zoomTime*0.25f);
					cameraY = medCameraY - ((medCameraY - lowCameraY) * zoomPercentComplete);
					cameraRotX = medCameraRotX - ((medCameraRotX - lowCameraRotX) * zoomPercentComplete);
					cameraDistance = medCameraDistance - ((medCameraDistance - lowCameraDistance) * zoomPercentComplete);
				}
			}
			else if (zoomInProgress == true) {
				cameraY = lowCameraY;
				cameraRotX = lowCameraRotX;
				cameraDistance = lowCameraDistance;
				zoomInProgress = false;
			}
						
			buck.forwardRate = buckDefaultForwardRate * Random.Range(0.9f, 1.1f);
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

			//buck.forwardRate = 15f;
			//buck.turnRate = 10f;
			//doe.forwardRate = 15f;
			//doe.turnRate = 10f;
			//fawn.forwardRate = 15f;
			//fawn.turnRate = 10f;

			

			if (pumaDeerDistance1 < 2.5f || pumaDeerDistance2 < 2.5f || pumaDeerDistance3 < 2.5f) {
			
				// DEER IS CAUGHT !!!
			
				if (pumaDeerDistance1 < 2.5f) {
					buck.forwardRate = 0;
					buck.turnRate = 0f;
					buckAnimator.SetBool("Die", true);
					caughtDeer = buck;
					meatJustEaten = Random.Range(35f, 43f);
					meatTotalEaten += meatJustEaten;
					caloriesGained = meatJustEaten * Random.Range(900f, 1100f);
					lastCaughtDeerType = 0;
					bucksKilled[selectedPuma] += 1;
					buckExpenditures[selectedPuma] += recentCalories[selectedPuma];
					buckCalories[selectedPuma] += caloriesGained;
					buckExpenditures[6] += recentCalories[selectedPuma]; // population total
					buckCalories[6] += caloriesGained; // population total
					PumaAddCalories(caloriesGained);
				}
				else if (pumaDeerDistance2 < 2.5f) {
					doe.forwardRate = 0f;
					doe.turnRate = 0f;
					doeAnimator.SetBool("Die", true);
					caughtDeer = doe;
					meatJustEaten = Random.Range(25f, 32f)      *        6f;  //////////////TEMP
					meatTotalEaten += meatJustEaten;
					caloriesGained = meatJustEaten * Random.Range(900f, 1100f);
					lastCaughtDeerType = 1;
					doesKilled[selectedPuma] += 1;
					doeExpenditures[selectedPuma] += recentCalories[selectedPuma];
					doeCalories[selectedPuma] += caloriesGained;
					doeExpenditures[6] += recentCalories[selectedPuma]; // population total
					doeCalories[6] += caloriesGained; // population total
					PumaAddCalories(caloriesGained);
				}
				else {
					fawn.forwardRate = 0f;
					fawn.turnRate = 0f;
					fawnAnimator.SetBool("Die", true);
					caughtDeer = fawn;
					meatJustEaten = Random.Range(17f, 23f);
					meatTotalEaten += meatJustEaten;
					caloriesGained = meatJustEaten * Random.Range(900f, 1100f);
					lastCaughtDeerType = 2;
					fawnsKilled[selectedPuma] += 1;
					fawnExpenditures[selectedPuma] += recentCalories[selectedPuma];
					fawnCalories[selectedPuma] += caloriesGained;
					fawnExpenditures[6] += recentCalories[selectedPuma]; // population total
					fawnCalories[6] += caloriesGained; // population total
					PumaAddCalories(caloriesGained);
				}

				// prepare caughtDeer obj for slide
				deerCaughtHeading = caughtDeer.heading;
				if (cameraRotY >= deerCaughtHeading) {
					deerCaughtHeadingLeft = (cameraRotY - deerCaughtHeading <= 180) ? false : true;
				}
				else {
					deerCaughtHeadingLeft = (deerCaughtHeading - cameraRotY <= 180) ? true : false;
				}
				if (deerCaughtHeadingLeft == true) {
					deerCaughtFinalHeading = cameraRotY + 90;
				}
				else {
					//deerCaughtFinalHeading = cameraRotY - 90;
					deerCaughtFinalHeading = cameraRotY + 90;
				}
				//System.Console.WriteLine("cameraRotY: " + cameraRotY.ToString() + "  deerCaughtHeading: " + deerCaughtHeading.ToString() + "  deerCaughtFinalHeading: " + deerCaughtFinalHeading.ToString());	
				if (deerCaughtFinalHeading < 0)
					deerCaughtFinalHeading += 360;
				if (deerCaughtFinalHeading >= 360)
					deerCaughtFinalHeading -= 360;
				deerCaughtCameraRotY = cameraRotY;
				deerCaughtOffsetX = caughtDeer.gameObj.transform.position.x - pumaX;
				deerCaughtOffsetZ = caughtDeer.gameObj.transform.position.z - pumaZ;
				deerCaughtFinalOffsetX = (Mathf.Sin(cameraRotY*Mathf.PI/180) * deerCaughtFinalOffsetFactor0);
				deerCaughtFinalOffsetZ = (Mathf.Cos(cameraRotY*Mathf.PI/180) * deerCaughtFinalOffsetFactor0);
				deerCaughtFinalOffsetX += (Mathf.Sin((cameraRotY-90f)*Mathf.PI/180) * deerCaughtFinalOffsetFactor90);
				deerCaughtFinalOffsetZ += (Mathf.Cos((cameraRotY-90f)*Mathf.PI/180) * deerCaughtFinalOffsetFactor90);
				//deerCaughtFinalOffsetX += (Mathf.Sin(deerCaughtFinalHeading*Mathf.PI/180) * 9f);
				//deerCaughtFinalOffsetZ += (Mathf.Cos(deerCaughtFinalHeading*Mathf.PI/180) * 9f);
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
				SetGameState("gameStateCaught1");
			}
			break;

		case "gameStateCaught1":
			fadeTime = 1.3f;
			forwardKey = false;
			backKey = false;
			if (Time.time - stateStartTime < fadeTime) { // implements trans as a reverse of leavingGui trans
	
				cameraRotPercentDone = ((Time.time - stateStartTime) / fadeTime);
				cameraRotPercentDone = cameraRotPercentDone * cameraRotPercentDone;
				cameraRotX = previousCameraRotX + (closeupCameraRotX - previousCameraRotX) * cameraRotPercentDone;

				float backwardsTime = (stateStartTime + fadeTime) - (Time.time - stateStartTime);	
				if (backwardsTime - stateStartTime < (fadeTime * 0.5f)) {
					// 1st half
					fadePercentComplete = (backwardsTime - stateStartTime) / (fadeTime * 0.5f);
					fadePercentComplete = fadePercentComplete * fadePercentComplete;
					cameraY = closeupCameraY + fadePercentComplete * ((previousCameraY - closeupCameraY) * 0.5f);
					cameraRotOffsetY = -160f + fadePercentComplete * ((0f - -160f) * 0.5f);
					cameraDistance = closeupCameraDistance + fadePercentComplete * ((previousCameraDistance - closeupCameraDistance) * 0.5f);
				}
				else if (backwardsTime - stateStartTime < fadeTime) {
					// 2nd half
					fadePercentComplete = ((backwardsTime - stateStartTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);				
					fadePercentComplete = fadePercentComplete + (fadePercentComplete - (fadePercentComplete * fadePercentComplete));
					cameraY = closeupCameraY + ((previousCameraY - closeupCameraY) * 0.5f) + fadePercentComplete * ((previousCameraY - closeupCameraY) * 0.5f);
					cameraRotOffsetY = -160f + ((0f - -160f) * 0.5f) + fadePercentComplete * ((0f - -160f) * 0.5f);
					cameraDistance = closeupCameraDistance + ((previousCameraDistance - closeupCameraDistance) * 0.5f) + fadePercentComplete * ((previousCameraDistance - closeupCameraDistance) * 0.5f);
				}

				// puma and deer slide to a stop
				float percentDone = 1f - ((Time.time - stateStartTime) / fadeTime);
				float pumaMoveDistance = 1f * Time.deltaTime * pumaChasingSpeed * percentDone * 1.1f;
				pumaX += (Mathf.Sin(deerCaughtCameraRotY*Mathf.PI/180) * pumaMoveDistance);
				pumaZ += (Mathf.Cos(deerCaughtCameraRotY*Mathf.PI/180) * pumaMoveDistance);
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
				cameraY = closeupCameraY;
				cameraRotX = closeupCameraRotX;
				cameraDistance = closeupCameraDistance;
				cameraRotOffsetY = -160f;

				float deerX = pumaX + deerCaughtFinalOffsetX;
				float deerY = caughtDeer.gameObj.transform.position.y;
				float deerZ = pumaZ + deerCaughtFinalOffsetZ;
				caughtDeer.gameObj.transform.rotation = Quaternion.Euler(0, deerCaughtFinalHeading, 0);
				//System.Console.WriteLine("final heading: " + deerCaughtFinalHeading.ToString());	
				caughtDeer.gameObj.transform.position = new Vector3(deerX, deerY, deerZ);

				SetGameState("gameStateCaught2");
			}
			break;

		case "gameStateCaught2":
			forwardKey = false;
			backKey = false;
			if (Time.time - stateStartTime > 0.2f) {
				SetGameState("gameStateCaught3");
			}
			break;

		case "gameStateCaught3":
			fadeTime = 5f;
			forwardKey = false;
			backKey = false;
			if (Time.time - stateStartTime < fadeTime) { // implements trans as a reverse of leavingGui trans
				float backwardsTime = (stateStartTime + fadeTime) - (Time.time - stateStartTime);	
				if (backwardsTime - stateStartTime < (fadeTime * 0.5f)) {
					// 1st half
					//guiFlybySpeed = 1f - (backwardsTime - stateStartTime) / fadeTime;
					fadePercentComplete = (backwardsTime - stateStartTime) / (fadeTime * 0.5f);
					fadePercentComplete = fadePercentComplete * fadePercentComplete;
					cameraY = eatingCameraY + fadePercentComplete * ((previousCameraY - eatingCameraY) * 0.5f);
					//cameraRotOffsetY = -120f + fadePercentComplete * ((0f - -120f) * 0.5f);
					//cameraRotX = eatingCameraRotX + fadePercentComplete * ((previousCameraRotX - eatingCameraRotX) * 0.5f);
					cameraRotX = previousCameraRotX + (eatingCameraRotX - previousCameraRotX) * ((Time.time - stateStartTime) / fadeTime);
					cameraDistance = eatingCameraDistance + fadePercentComplete * ((previousCameraDistance - eatingCameraDistance) * 0.5f);
				}
				else if (backwardsTime - stateStartTime < fadeTime) {
					// 2nd half
					//guiFlybySpeed = 1f - (backwardsTime - stateStartTime) / fadeTime;
					fadePercentComplete = ((backwardsTime - stateStartTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);				
					fadePercentComplete = fadePercentComplete + (fadePercentComplete - (fadePercentComplete * fadePercentComplete));
					cameraY = eatingCameraY + ((previousCameraY - eatingCameraY) * 0.5f) + fadePercentComplete * ((previousCameraY - eatingCameraY) * 0.5f);
					//cameraRotOffsetY = -120f + ((0f - -120f) * 0.5f) + fadePercentComplete * ((0f - -120f) * 0.5f);
					cameraRotPercentDone = (float)((float)(backwardsTime - stateStartTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);
					cameraRotX = previousCameraRotX + (eatingCameraRotX - previousCameraRotX) * ((Time.time - stateStartTime) / fadeTime);
					cameraDistance = eatingCameraDistance + ((previousCameraDistance - eatingCameraDistance) * 0.5f) + fadePercentComplete * ((previousCameraDistance - eatingCameraDistance) * 0.5f);
				}
			}
			else {
				cameraY = eatingCameraY;
				cameraRotX = eatingCameraRotX;
				cameraDistance = eatingCameraDistance;	
				SetGameState("gameStateCaught4");
			}
			previousCameraRotOffsetY = cameraRotOffsetY; // just in case we go straight from here to gameStateCaught5
			break;
			
		case "gameStateCaught4":
			forwardKey = false;
			backKey = false;
			if (Time.time - stateStartTime > 0.1f) {
				cameraRotOffsetY -= Time.deltaTime + 0.03f;
				if (cameraRotOffsetY < -180f)
					cameraRotOffsetY += 360f;
				previousCameraRotOffsetY = cameraRotOffsetY;
				//InitLevel();
				//SetGameState("gameStateCaught5");
			}
			break;
					
		case "gameStateCaught5":
			pumaAnimator.SetBool("DeerKill", false);

			fadeTime = 2.4f;

			cameraRotPercentDone = ((Time.time - stateStartTime) / fadeTime);
			cameraRotPercentDone = cameraRotPercentDone * cameraRotPercentDone;
			cameraRotX = previousCameraRotX + (highCameraRotX - previousCameraRotX) * cameraRotPercentDone;

			if (Time.time - stateStartTime < fadeTime) { // implements trans as a reverse of leavingGui trans
				float backwardsTime = (stateStartTime + fadeTime) - (Time.time - stateStartTime);	
				if (backwardsTime - stateStartTime < (fadeTime * 0.5f)) {
					// 1st half
					//guiFlybySpeed = 1f - (backwardsTime - stateStartTime) / fadeTime;
					fadePercentComplete = (backwardsTime - stateStartTime) / (fadeTime * 0.5f);
					fadePercentComplete = fadePercentComplete * fadePercentComplete;
					cameraY = highCameraY + fadePercentComplete * ((previousCameraY - highCameraY) * 0.5f);
					cameraRotOffsetY = 0f + fadePercentComplete * ((previousCameraRotOffsetY - 0f) * 0.5f);
					//cameraRotX = highCameraRotX + fadePercentComplete * ((previousCameraRotX - highCameraRotX) * 0.5f);
					//cameraRotX = previousCameraRotX + (highCameraRotX - previousCameraRotX) * ((Time.time - stateStartTime) / fadeTime);
					cameraDistance = highCameraDistance + fadePercentComplete * ((previousCameraDistance - highCameraDistance) * 0.5f);
				}
				else if (backwardsTime - stateStartTime < fadeTime) {
					// 2nd half
					//guiFlybySpeed = 1f - (backwardsTime - stateStartTime) / fadeTime;
					fadePercentComplete = ((backwardsTime - stateStartTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);				
					fadePercentComplete = fadePercentComplete + (fadePercentComplete - (fadePercentComplete * fadePercentComplete));
					cameraY = highCameraY + ((previousCameraY - highCameraY) * 0.5f) + fadePercentComplete * ((previousCameraY - highCameraY) * 0.5f);
					cameraRotOffsetY = 0f + ((previousCameraRotOffsetY - 0f) * 0.5f) + fadePercentComplete * ((previousCameraRotOffsetY - 0f) * 0.5f);
					cameraRotPercentDone = (float)((float)(backwardsTime - stateStartTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);
					//cameraRotX = previousCameraRotX + (highCameraRotX - previousCameraRotX) * ((Time.time - stateStartTime) / fadeTime);
					cameraDistance = highCameraDistance + ((previousCameraDistance - highCameraDistance) * 0.5f) + fadePercentComplete * ((previousCameraDistance - highCameraDistance) * 0.5f);
				}
			}
			else {
				cameraY = highCameraY;
				cameraRotX = highCameraRotX;
				cameraDistance = highCameraDistance;
				previousCameraRotOffsetY = cameraRotOffsetY = 0;
							
				PlaceDeerPositions();
				ResetAnimations();
				
				SetGameState("gameStateStalking");
							
				previousMeatLevel = GetMeatLevel();
				previousPumaHealth = GetPumaHealth(selectedPuma);
				caloriesGained = 0f;
				recentCalories[selectedPuma] = 0f;
			}
			break;	

		case "gameStateDied1":
			//fadeTime = 1.3f;
			fadeTime = 3f;
			forwardKey = false;
			backKey = false;
			if (Time.time - stateStartTime < fadeTime) { // implements trans as a reverse of leavingGui trans
	
				cameraRotPercentDone = ((Time.time - stateStartTime) / fadeTime);
				cameraRotPercentDone = cameraRotPercentDone * cameraRotPercentDone;
				cameraRotX = previousCameraRotX + (closeupCameraRotX - previousCameraRotX) * cameraRotPercentDone;

				float backwardsTime = (stateStartTime + fadeTime) - (Time.time - stateStartTime);	
				if (backwardsTime - stateStartTime < (fadeTime * 0.5f)) {
					// 1st half
					fadePercentComplete = (backwardsTime - stateStartTime) / (fadeTime * 0.5f);
					fadePercentComplete = fadePercentComplete * fadePercentComplete;
					cameraY = closeupCameraY + fadePercentComplete * ((previousCameraY - closeupCameraY) * 0.5f);
					cameraRotOffsetY = -160f + fadePercentComplete * ((0f - -160f) * 0.5f);
					cameraDistance = closeupCameraDistance + fadePercentComplete * ((previousCameraDistance - closeupCameraDistance) * 0.5f);
				}
				else if (backwardsTime - stateStartTime < fadeTime) {
					// 2nd half
					fadePercentComplete = ((backwardsTime - stateStartTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);				
					fadePercentComplete = fadePercentComplete + (fadePercentComplete - (fadePercentComplete * fadePercentComplete));
					cameraY = closeupCameraY + ((previousCameraY - closeupCameraY) * 0.5f) + fadePercentComplete * ((previousCameraY - closeupCameraY) * 0.5f);
					cameraRotOffsetY = -160f + ((0f - -160f) * 0.5f) + fadePercentComplete * ((0f - -160f) * 0.5f);
					cameraDistance = closeupCameraDistance + ((previousCameraDistance - closeupCameraDistance) * 0.5f) + fadePercentComplete * ((previousCameraDistance - closeupCameraDistance) * 0.5f);
				}
			}
			else {
				cameraY = closeupCameraY;
				cameraRotX = closeupCameraRotX;
				cameraDistance = closeupCameraDistance;
				cameraRotOffsetY = -160f;
				SetGameState("gameStateDied2");
			}
			break;

		case "gameStateDied2":
			forwardKey = false;
			backKey = false;
			if (Time.time - stateStartTime > 0.2f) {
				SetGameState("gameStateDied3");
			}
			break;

		case "gameStateDied3":
			fadeTime = 5f;
			forwardKey = false;
			backKey = false;
			if (Time.time - stateStartTime < fadeTime) { // implements trans as a reverse of leavingGui trans
				float backwardsTime = (stateStartTime + fadeTime) - (Time.time - stateStartTime);	
				if (backwardsTime - stateStartTime < (fadeTime * 0.5f)) {
					// 1st half
					//guiFlybySpeed = 1f - (backwardsTime - stateStartTime) / fadeTime;
					fadePercentComplete = (backwardsTime - stateStartTime) / (fadeTime * 0.5f);
					fadePercentComplete = fadePercentComplete * fadePercentComplete;
					cameraY = eatingCameraY + fadePercentComplete * ((previousCameraY - eatingCameraY) * 0.5f);
					//cameraRotOffsetY = -120f + fadePercentComplete * ((0f - -120f) * 0.5f);
					//cameraRotX = eatingCameraRotX + fadePercentComplete * ((previousCameraRotX - eatingCameraRotX) * 0.5f);
					cameraRotX = previousCameraRotX + (eatingCameraRotX - previousCameraRotX) * ((Time.time - stateStartTime) / fadeTime);
					cameraDistance = eatingCameraDistance + fadePercentComplete * ((previousCameraDistance - eatingCameraDistance) * 0.5f);
				}
				else if (backwardsTime - stateStartTime < fadeTime) {
					// 2nd half
					//guiFlybySpeed = 1f - (backwardsTime - stateStartTime) / fadeTime;
					fadePercentComplete = ((backwardsTime - stateStartTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);				
					fadePercentComplete = fadePercentComplete + (fadePercentComplete - (fadePercentComplete * fadePercentComplete));
					cameraY = eatingCameraY + ((previousCameraY - eatingCameraY) * 0.5f) + fadePercentComplete * ((previousCameraY - eatingCameraY) * 0.5f);
					//cameraRotOffsetY = -120f + ((0f - -120f) * 0.5f) + fadePercentComplete * ((0f - -120f) * 0.5f);
					cameraRotPercentDone = (float)((float)(backwardsTime - stateStartTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);
					cameraRotX = previousCameraRotX + (eatingCameraRotX - previousCameraRotX) * ((Time.time - stateStartTime) / fadeTime);
					cameraDistance = eatingCameraDistance + ((previousCameraDistance - eatingCameraDistance) * 0.5f) + fadePercentComplete * ((previousCameraDistance - eatingCameraDistance) * 0.5f);
				}
			}
			else {
				cameraY = eatingCameraY;
				cameraRotX = eatingCameraRotX;
				cameraDistance = eatingCameraDistance;	
				SetGameState("gameStateDied4");
			}
			previousCameraRotOffsetY = cameraRotOffsetY; // just in case we go straight from here to gameStateDied5
			break;
			
		case "gameStateDied4":
			forwardKey = false;
			backKey = false;
			if (Time.time - stateStartTime > 0.1f) {
				cameraRotOffsetY -= Time.deltaTime + 0.03f;
				if (cameraRotOffsetY < -180f)
					cameraRotOffsetY += 360f;
				previousCameraRotOffsetY = cameraRotOffsetY;
				//InitLevel();
				//SetGameState("gameStateDied5");
			}
			break;
					
		case "gameStateDied5":
			pumaAnimator.SetBool("DeerKill", false);

			fadeTime = 2.4f;

			cameraRotPercentDone = ((Time.time - stateStartTime) / fadeTime);
			cameraRotPercentDone = cameraRotPercentDone * cameraRotPercentDone;
			cameraRotX = previousCameraRotX + (highCameraRotX - previousCameraRotX) * cameraRotPercentDone;

			if (Time.time - stateStartTime < fadeTime) { // implements trans as a reverse of leavingGui trans
				float backwardsTime = (stateStartTime + fadeTime) - (Time.time - stateStartTime);	
				if (backwardsTime - stateStartTime < (fadeTime * 0.5f)) {
					// 1st half
					//guiFlybySpeed = 1f - (backwardsTime - stateStartTime) / fadeTime;
					fadePercentComplete = (backwardsTime - stateStartTime) / (fadeTime * 0.5f);
					fadePercentComplete = fadePercentComplete * fadePercentComplete;
					cameraY = highCameraY + fadePercentComplete * ((previousCameraY - highCameraY) * 0.5f);
					cameraRotOffsetY = 0f + fadePercentComplete * ((previousCameraRotOffsetY - 0f) * 0.5f);
					//cameraRotX = highCameraRotX + fadePercentComplete * ((previousCameraRotX - highCameraRotX) * 0.5f);
					//cameraRotX = previousCameraRotX + (highCameraRotX - previousCameraRotX) * ((Time.time - stateStartTime) / fadeTime);
					cameraDistance = highCameraDistance + fadePercentComplete * ((previousCameraDistance - highCameraDistance) * 0.5f);
				}
				else if (backwardsTime - stateStartTime < fadeTime) {
					// 2nd half
					//guiFlybySpeed = 1f - (backwardsTime - stateStartTime) / fadeTime;
					fadePercentComplete = ((backwardsTime - stateStartTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);				
					fadePercentComplete = fadePercentComplete + (fadePercentComplete - (fadePercentComplete * fadePercentComplete));
					cameraY = highCameraY + ((previousCameraY - highCameraY) * 0.5f) + fadePercentComplete * ((previousCameraY - highCameraY) * 0.5f);
					cameraRotOffsetY = 0f + ((previousCameraRotOffsetY - 0f) * 0.5f) + fadePercentComplete * ((previousCameraRotOffsetY - 0f) * 0.5f);
					cameraRotPercentDone = (float)((float)(backwardsTime - stateStartTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);
					//cameraRotX = previousCameraRotX + (highCameraRotX - previousCameraRotX) * ((Time.time - stateStartTime) / fadeTime);
					cameraDistance = highCameraDistance + ((previousCameraDistance - highCameraDistance) * 0.5f) + fadePercentComplete * ((previousCameraDistance - highCameraDistance) * 0.5f);
				}
			}
			else {
				cameraY = highCameraY;
				cameraRotX = highCameraRotX;
				cameraDistance = highCameraDistance;
				cameraRotOffsetY = 0;
							
				PlaceDeerPositions();
				ResetAnimations();
				
				SetGameState("gameStateStalking");
							
				previousMeatLevel = GetMeatLevel();
				previousPumaHealth = GetPumaHealth(selectedPuma);
				caloriesGained = 0f;
				recentCalories[selectedPuma] = 0f;
			}
			break;	

		}		
		


		//=======================
		// Update Positions
		//=======================
			
		/*
		int displayInt;
		System.Console.WriteLine("=================================");	
		displayInt = frameCurrentDuration;
		System.Console.WriteLine("Frame time: " + displayInt.ToString());	
		displayInt = (int)(Time.deltaTime * 1000);
		System.Console.WriteLine("Delta time: " + displayInt.ToString());		
		System.Console.WriteLine("-----------");	
		displayInt = (int)(inputVert * 1000);
		System.Console.WriteLine("Input vert: " + displayInt.ToString());		
		displayInt = (int)(inputHorz * 1000);
		System.Console.WriteLine("Input horz: " + displayInt.ToString());		
		System.Console.WriteLine("=================================");	
		*/

		float distance = 0f;

		pumaAnimator.SetBool("GuiMode", false);

		if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftControl)) {
			// dev only: camera distance and angle
			cameraDistance -= inputVert * Time.deltaTime * 20 * 5 * speedOverdrive;
			cameraRotOffsetY += inputHorz * Time.deltaTime * 40 * 5 * speedOverdrive;
			pumaHeading = cameraRotY;
			ResetNavState();
			inputVert = 0;
			inputHorz = 0;
		}
		
		else if (Input.GetKey(KeyCode.LeftShift)) {
			// dev only: camera height
			cameraY += inputVert * Time.deltaTime  * 20 * 5 * speedOverdrive;
			pumaHeading = cameraRotY;
			ResetNavState();
			inputVert = 0;
			inputHorz = 0;
		}
		
		else if (Input.GetKey(KeyCode.LeftControl)) {
			// dev only: camera pitch
			cameraRotX += inputVert * Time.deltaTime  * 50 * 5 * speedOverdrive;
			pumaHeading = cameraRotY;
			ResetNavState();
			inputVert = 0;
			inputHorz = 0;
		}
		
		else if (gameState == "gameStateGui" || gameState == "gameStateLeavingGameplay" || gameState == "gameStateLeavingGui") {
			if ((gameState != "gameStateLeavingGui") || (Time.time - stateStartTime < 1.8f))
				pumaAnimator.SetBool("GuiMode", true);
			distance = guiFlybySpeed * Time.deltaTime  * 12f * speedOverdrive;
			pumaHeading = cameraRotY;
			pumaX += (Mathf.Sin(cameraRotY*Mathf.PI/180) * distance);
			pumaZ += (Mathf.Cos(cameraRotY*Mathf.PI/180) * distance);
		}
		
		else if (gameState == "gameStateCloseup" || gameState == "gameStateEnteringGameplay" ||
		         gameState == "gameStateCaught1" || gameState == "gameStateCaught2" || gameState == "gameStateCaught3" || gameState == "gameStateCaught4" || gameState == "gameStateCaught5" ||
				 gameState == "gameStateDied1" || gameState == "gameStateDied2" || gameState == "gameStateDied3" || gameState == "gameStateDied4" || gameState == "gameStateDied5")
		{
			distance = 0f;
			pumaHeading = cameraRotY;
			pumaX += (Mathf.Sin(cameraRotY*Mathf.PI/180) * distance);
			pumaZ += (Mathf.Cos(cameraRotY*Mathf.PI/180) * distance);
		}
		
		else if (gameState == "gameStateChasing") {
			float rotationSpeed = 150f;
			distance = inputVert * Time.deltaTime  * pumaChasingSpeed * speedOverdrive;
			float travelledDistance = (GetPumaHealth(selectedPuma) > 0.05f) ? distance : distance * (GetPumaHealth(selectedPuma) / 0.05f);
			cameraRotY += inputHorz * Time.deltaTime * rotationSpeed;
			pumaHeading = cameraRotY + pumaHeadingOffset;
			pumaX += (Mathf.Sin(pumaHeading*Mathf.PI/180) * travelledDistance);
			pumaZ += (Mathf.Cos(pumaHeading*Mathf.PI/180) * travelledDistance);
			
			float newCalories = distance * caloriesPerMeterChasing;
			
newCalories = (GetPumaHealth(selectedPuma) < 0.1f) ? newCalories : newCalories * 5f;  // TEMP!!
					
			recentCalories[selectedPuma] += newCalories;
			PumaAddCalories(-newCalories);
		}
		
		else if (gameState == "gameStateStalking") {		
			float rotationSpeed = 100f;
			distance = inputVert * Time.deltaTime  * pumaStalkingSpeed * speedOverdrive;
			cameraRotY += inputHorz * Time.deltaTime * rotationSpeed;
			pumaHeading = cameraRotY + pumaHeadingOffset;
			pumaX += (Mathf.Sin(pumaHeading*Mathf.PI/180) * distance);
			pumaZ += (Mathf.Cos(pumaHeading*Mathf.PI/180) * distance);
					
			float newCalories = distance * caloriesPerMeterStalking;
			
newCalories = (GetPumaHealth(selectedPuma) < 0.1f) ? newCalories : newCalories * 8f;  // TEMP!!
					
			recentCalories[selectedPuma] += newCalories;
			PumaAddCalories(-newCalories);					
		}

		pumaAnimator.SetFloat("Distance", distance);

		float oldCameraRotY = cameraRotY;
		cameraRotY += cameraRotOffsetY;
		
		cameraX = pumaX - (Mathf.Sin(cameraRotY*Mathf.PI/180) * cameraDistance);
		cameraZ = pumaZ - (Mathf.Cos(cameraRotY*Mathf.PI/180) * cameraDistance);	
	
		// calculate puma rotX based on terrain in front and behind
		float pumaRotX;
		distance = 1f;
		float pumaAheadX = pumaX + (Mathf.Sin(pumaHeading*Mathf.PI/180) * distance * 1f);
		float pumaAheadZ = pumaZ + (Mathf.Cos(pumaHeading*Mathf.PI/180) * distance * 1f);
		float pumaBehindX = pumaX + (Mathf.Sin(pumaHeading*Mathf.PI/180) * distance * -1f);
		float pumaBehindZ = pumaZ + (Mathf.Cos(pumaHeading*Mathf.PI/180) * distance * -1f);
		pumaRotX = GetAngleFromOffset(0, GetTerrainHeight(pumaAheadX, pumaAheadZ), distance * 2f, GetTerrainHeight(pumaBehindX, pumaBehindZ)) - 90f;
			
		// update puma obj
		pumaY = GetTerrainHeight(pumaX, pumaZ);
		pumaObj.transform.position = new Vector3(pumaX, pumaY, pumaZ);			
		pumaObj.transform.rotation = Quaternion.Euler(pumaRotX, (pumaHeading - 180f), 0);
	
		// calculate camera adjustments based on terrain
		//-----------------------------------------------
			// initially camera goes to 'cameraY' units above terrain
			// that screws up the distance to the puma in extreme slope terrain
			// the camera is then moved to the 'correct' distance along the vector from puma to camera
			// that screws up the viewing angle, putting the puma too high or low in field of view
			// lastly we calculate an angle offset for new position, and factor in some fudge to account for viewing angle problem
		System.Console.WriteLine("=============================");	
		System.Console.WriteLine("pumaY: " + pumaY.ToString());	
		System.Console.WriteLine("cameraX: " + cameraX.ToString());	
		System.Console.WriteLine("cameraY: " + cameraY.ToString());	
		System.Console.WriteLine("cameraZ: " + cameraZ.ToString());	
		System.Console.WriteLine("cameraRotX: " + cameraRotX.ToString());	
		System.Console.WriteLine("cameraDistance: " + cameraDistance.ToString());	
		System.Console.WriteLine("----------");	 // camera moves up
		float adjustedCameraX = cameraX;
		float adjustedCameraY = cameraY + GetTerrainHeight(cameraX, cameraZ);
		float adjustedCameraZ = cameraZ;	
		System.Console.WriteLine("adjustedCameraX: " + adjustedCameraX.ToString());	
		System.Console.WriteLine("adjustedCameraY: " + adjustedCameraY.ToString());	
		System.Console.WriteLine("adjustedCameraZ: " + adjustedCameraZ.ToString());	
		System.Console.WriteLine("----------");	  // slides along vector
		float idealVisualDistance = Vector3.Distance(new Vector3(0, 0, 0), new Vector3(cameraDistance, cameraY, 0));
		System.Console.WriteLine("idealVisualDistance: " + idealVisualDistance.ToString());	
		float currentVisualAngle = GetAngleFromOffset(0, pumaY, cameraDistance, adjustedCameraY);
		System.Console.WriteLine("currentVisualAngle: " + currentVisualAngle.ToString());	
		float adjustedCameraDistance = Mathf.Sin(currentVisualAngle*Mathf.PI/180) * idealVisualDistance;
		System.Console.WriteLine("adjustedCameraDistance: " + adjustedCameraDistance.ToString());	
		adjustedCameraY = pumaY + Mathf.Cos(currentVisualAngle*Mathf.PI/180) * idealVisualDistance;
		adjustedCameraX = pumaX - (Mathf.Sin(cameraRotY*Mathf.PI/180) * adjustedCameraDistance);
		adjustedCameraZ = pumaZ - (Mathf.Cos(cameraRotY*Mathf.PI/180) * adjustedCameraDistance);	
		System.Console.WriteLine("adjustedCameraX: " + adjustedCameraX.ToString());	
		System.Console.WriteLine("adjustedCameraY: " + adjustedCameraY.ToString());	
		System.Console.WriteLine("adjustedCameraZ: " + adjustedCameraZ.ToString());	
		System.Console.WriteLine("----------");	  // adjust angle
		float cameraRotXAdjustment = -1f * (GetAngleFromOffset(0, pumaY, cameraDistance, GetTerrainHeight(cameraX, cameraZ)) - 90f);
		cameraRotXAdjustment *= (cameraRotXAdjustment > 0) ? 0.65f : 0.8f;
		float adjustedCameraRotX = cameraRotX + cameraRotXAdjustment;
		System.Console.WriteLine("adjustedCameraRotX: " + adjustedCameraRotX.ToString());	
		displayVar1 = cameraRotX;
		displayVar2 = cameraRotXAdjustment;
		displayVar3 = adjustedCameraRotX;
		
		// update camera obj
		Camera.main.transform.position = new Vector3(adjustedCameraX, adjustedCameraY, adjustedCameraZ);
		Camera.main.transform.rotation = Quaternion.Euler(adjustedCameraRotX, cameraRotY, cameraRotZ);
		
		cameraRotY = oldCameraRotY;

		UpdateDeerHeading(buck);
		UpdateDeerHeading(doe);
		UpdateDeerHeading(fawn);

		UpdateDeerPosition(buck);
		UpdateDeerPosition(doe);
		UpdateDeerPosition(fawn);
		
		//////////////////////////////////
		/// leap-frog the ground planes
		//////////////////////////////////
	
		for (int i = 0; i < 4; i++) {
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
	

	float GetTerrainHeight(float x, float z)
	{
		float terrainX;
		float terrainZ;
	
		//System.Console.WriteLine("Entering GetTerrainHeight:   x: " + x.ToString()  + "  z: " + z.ToString());	
		//System.Console.WriteLine("terrain 1:   x: " + terrain1.transform.position.x.ToString()  + "  z: " + terrain1.transform.position.z.ToString());	
		//System.Console.WriteLine("terrain 2:   x: " + terrain2.transform.position.x.ToString()  + "  z: " + terrain2.transform.position.z.ToString());	
		//System.Console.WriteLine("terrain 3:   x: " + terrain3.transform.position.x.ToString()  + "  z: " + terrain3.transform.position.z.ToString());	
		//System.Console.WriteLine("terrain 4:   x: " + terrain4.transform.position.x.ToString()  + "  z: " + terrain4.transform.position.z.ToString());	

		terrainX = terrain1.transform.position.x;
		terrainZ = terrain1.transform.position.z;		
		if (x >= terrainX && x < terrainX + 2000 && z >= terrainZ && z < terrainZ + 2000) {
			//int height = (int)(terrain1.SampleHeight(new Vector3(x, 0, z)));
			//System.Console.WriteLine("terrain 1: " + height.ToString());	
			return  terrain1.SampleHeight(new Vector3(x, 0, z));
		}
			
		terrainX = terrain2.transform.position.x;
		terrainZ = terrain2.transform.position.z;		
		if (x >= terrainX && x < terrainX + 2000 && z >= terrainZ && z < terrainZ + 2000) {
			//int height = (int)(terrain2.SampleHeight(new Vector3(x, 0, z)));
			//System.Console.WriteLine("terrain 2: " + height.ToString());	
			return  terrain2.SampleHeight(new Vector3(x, 0, z));
		}
			
		terrainX = terrain3.transform.position.x;
		terrainZ = terrain3.transform.position.z;		
		if (x >= terrainX && x < terrainX + 2000 && z >= terrainZ && z < terrainZ + 2000) {
			//int height = (int)(terrain3.SampleHeight(new Vector3(x, 0, z)));
			//System.Console.WriteLine("terrain 3: " + height.ToString());	
			return  terrain3.SampleHeight(new Vector3(x, 0, z));
		}
			
		terrainX = terrain4.transform.position.x;
		terrainZ = terrain4.transform.position.z;		
		if (x >= terrainX && x < terrainX + 2000 && z >= terrainZ && z < terrainZ + 2000) {
			//int height = (int)(terrain4.SampleHeight(new Vector3(x, 0, z)));
			//System.Console.WriteLine("terrain 4: " + height.ToString());	
			return  terrain4.SampleHeight(new Vector3(x, 0, z));
		}
			
		return 0f;
	}

	
	
	//=======================================================
	//
	//	FRAME-BASED PROCESSING
	//
	//=======================================================
	
	void CalculateFrameRate()
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
	
	void ProcessNavControls()
	{
		bool leftKeyState = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.J) || leftArrowMouseEvent == true;
		bool rightKeyState = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.L) || rightArrowMouseEvent == true;
		bool forwardKeyState = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.I) || forwardClicked == true;
		bool backKeyState = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.K) || backClicked == true;
		
		bool sideLeftState = Input.GetKey(KeyCode.U) || sideLeftClicked == true;
		bool sideRightState = Input.GetKey(KeyCode.O) || sideRightClicked == true;
		bool diagLeftState = Input.GetKey(KeyCode.Alpha8) || diagLeftClicked == true;
		bool diagRightState = Input.GetKey(KeyCode.Alpha9) || diagRightClicked == true;
		
		forwardClicked = backClicked = sideLeftClicked = sideRightClicked = diagLeftClicked = diagRightClicked = false;
	
		if (forwardKeyState == true)
			pumaHeadingOffset = 0f;

		if (sideLeftState == true) {
			pumaHeadingOffset = -60f;
			forwardKeyState = true;
		}
		else if (sideRightState == true) {
			pumaHeadingOffset = 60f;
			forwardKeyState = true;
		}
		else if (diagLeftState == true) {
			pumaHeadingOffset = -30f;
			forwardKeyState = true;
		}
		else if (diagRightState == true) {
			pumaHeadingOffset = 30f;
			forwardKeyState = true;
		}
	
		leftKey = leftKeyState;
		rightKey = rightKeyState;
		//forwardKey = Input.GetKey(KeyCode.UpArrow);
		//backKey = Input.GetKey(KeyCode.DownArrow);


		if (inputVert == 0) {
			if (forwardKey == false)
				forwardKey = forwardKeyState;
			if (forwardKey == false)
				backKey = backKeyState;	
		}
		else if (inputVert > 0) {
			if (forwardKey == false)
				forwardKey = forwardKeyState;
			else if (backKeyState == true)
				forwardKey = false;
		}
		else {
			if (backKey == false)
				backKey = backKeyState;
			else if (forwardKeyState == true)
				backKey = false;
		}

		
		// basic input processing
	
		UpdateNav(navStateForward, navValForward, forwardKey, Time.deltaTime * 3, Time.deltaTime * 3);
		navStateForward = newNavState;
		navValForward = newNavVal;

		UpdateNav(navStateBack, -navValBack * 2, backKey, Time.deltaTime * 3, Time.deltaTime * 3);
		navStateBack = newNavState;
		navValBack = -newNavVal / 2;

		UpdateNav(navStateLeft, -navValLeft, leftKey, Time.deltaTime * ((gameState == "gameStateStalking") ? 3f : 4.4f), Time.deltaTime * 3);
		navStateLeft = newNavState;
		navValLeft = -newNavVal;

		UpdateNav(navStateRight, navValRight, rightKey, Time.deltaTime * ((gameState == "gameStateStalking") ? 3f : 4.4f), Time.deltaTime * 3);
		navStateRight = newNavState;
		navValRight = newNavVal;
		
		//inputVert = navValForward; // + navValBack;	 // disable down motion
		inputVert = navValForward + navValBack;		 // enable down motion
		inputHorz = navValRight + navValLeft;
		
		if (inputVert == 0)
			pumaHeadingOffset = 0;
		
	}
	
	
	
	
	void UpdateNav(Nav previousNavState, float previousNavVal, bool keyPressed, float incStep, float decStep)
	{
		newNavState = previousNavState;
		newNavVal = previousNavVal;
	
		switch (previousNavState) {

		case Nav.Off:
			if (keyPressed) {
				newNavState = Nav.Inc;
				newNavVal = previousNavVal + incStep;
				if (newNavVal >= 1f) {
					newNavState = Nav.Full;
					newNavVal = 1f;
				}
			}
			else {
				newNavVal = 0f;
			}
			break;

		case Nav.Inc:
			if (keyPressed) {
				newNavVal = previousNavVal + incStep;
				if (newNavVal >= 1f) {
					newNavState = Nav.Full;
					newNavVal = 1f;
				}
			}
			else {
				newNavState = Nav.Dec;
				newNavVal = previousNavVal - decStep;
				if (newNavVal <= 0f) {
					newNavState = Nav.Off;
					newNavVal = 0f;
				}
			}
			break;
			
		case Nav.Full:
			if (keyPressed) {
				newNavVal = 1f;
			}
			else {
				newNavState = Nav.Dec;
				newNavVal = previousNavVal - decStep;
				if (newNavVal <= 0f) {
					newNavState = Nav.Off;
					newNavVal = 0f;
				}
			}
			break;
			
		case Nav.Dec:
			if (keyPressed) {
				newNavState = Nav.Inc;
				newNavVal = previousNavVal + incStep;
				if (newNavVal >= 1f) {
					newNavState = Nav.Full;
					newNavVal = 1f;
				}
			}
			else {
				newNavVal = previousNavVal - decStep;
				if (newNavVal <= 0f) {
					newNavState = Nav.Off;
					newNavVal = 0f;
				}
			}
			break;
		}
	}
	
	void ResetNavState()
	{
		navStateLeft = Nav.Off;
		navStateRight = Nav.Off;
		navStateForward = Nav.Off;
		navStateBack = Nav.Off;

		navStateForwardLeft = Nav.Off;
		navStateForwardRight = Nav.Off;
		navStateBackLeft = Nav.Off;
		navStateBackRight = Nav.Off;
		
		navValLeft = 0;
		navValRight = 0;
		navValForward = 0;
		navValBack = 0;

		navValForwardLeft = 0;
		navValForwardRight = 0;
		navValBackLeft = 0;
		navValBackRight = 0;
		
		leftKey = false;
		rightKey = false;
		forwardKey = false;
		backKey = false;
		
		forwardClicked = false;
		backClicked = false;
		sideLeftClicked = false;
		sideRightClicked = false;
		diagLeftClicked = false;
		diagRightClicked = false;
		
		leftArrowMouseEvent = false;
		rightArrowMouseEvent = false;
	}
}



