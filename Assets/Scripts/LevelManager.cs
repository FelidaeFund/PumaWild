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
	private float cameraRotY;
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
	private bool stateInitFlag;
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

	//===================================
	//===================================
	//
	//		INITIALIZATION
	//
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
		mainHeading = Random.Range(0f, 360f);
		cameraRotZ = 0f;
		
		cameraX = pumaX - (Mathf.Sin(mainHeading*Mathf.PI/180) * cameraDistance);
		cameraZ = pumaZ - (Mathf.Cos(mainHeading*Mathf.PI/180) * cameraDistance);

		pumaObj.transform.position = new Vector3(pumaX, pumaY, pumaZ);
		Camera.main.transform.position = new Vector3(cameraX, cameraY, cameraZ);
		Camera.main.transform.rotation = Quaternion.Euler(cameraRotX, mainHeading, cameraRotZ);
		
	}
	
	
	public void SetGameState(string newGameState)
	{
		gameState = newGameState;
		stateStartTime = Time.time;
		stateInitFlag = false;
		
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

	public void SetSelectedPuma(int selection)
	{
		selectedPuma = selection;
		
		pumaChasingSpeed = defaultPumaChasingSpeed + (10f * speedArray[selectedPuma]);
		chaseTriggerDistance = defaultChaseTriggerDistance - (20f * stealthinessArray[selectedPuma]);
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
			
		inputControls.ProcessControls(gameState);

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
			SelectCameraPosition("cameraPosGui", -120f, 0f, null, null);
			break;
	
		case "gameStateLeavingGui":
			fadeTime = 2.5f;
			guiFlybySpeed = 1f - (Time.time - stateStartTime) / fadeTime;

			
			SelectCameraPosition("cameraPosCloseup", 1000000f, fadeTime, "mainCurveSForward", "curveRotXLogarithmicSecondHalf"); // 1000000 signifies no change for cameraRotOffsetY
			
			
			
			if (Time.time - stateStartTime < (fadeTime * 0.5f)) {
				// 1st half
				cameraRotX = previousCameraRotX;
				fadePercentComplete = (Time.time - stateStartTime) / (fadeTime * 0.5f);
				fadePercentComplete = fadePercentComplete * fadePercentComplete;
				cameraY = previousCameraY + fadePercentComplete * ((closeupCameraY - previousCameraY) * 0.5f);
				cameraDistance = previousCameraDistance + fadePercentComplete * ((closeupCameraDistance - previousCameraDistance) * 0.5f);
			}
			else if (Time.time - stateStartTime < fadeTime) {
				// 2nd half
				cameraRotPercentDone = (float)((float)(Time.time - stateStartTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);
				cameraRotX = previousCameraRotX + ((closeupCameraRotX - previousCameraRotX) * cameraRotPercentDone * cameraRotPercentDone);
				fadePercentComplete = ((Time.time - stateStartTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);				
				fadePercentComplete = fadePercentComplete + (fadePercentComplete - (fadePercentComplete * fadePercentComplete));
				cameraY = previousCameraY + ((closeupCameraY - previousCameraY) * 0.5f) + fadePercentComplete * ((closeupCameraY - previousCameraY) * 0.5f);
				cameraDistance = previousCameraDistance + ((closeupCameraDistance - previousCameraDistance) * 0.5f) + fadePercentComplete * ((closeupCameraDistance - previousCameraDistance) * 0.5f);
			}
			else {
				cameraY = closeupCameraY;
				cameraRotX = closeupCameraRotX;
				cameraDistance = closeupCameraDistance;
			}

			
			
			

			if (Time.time >= stateStartTime + fadeTime) {
				guiFlybySpeed = 0f;
				SetGameState("gameStateCloseup");
			}
			break;
	
		case "gameStateCloseup":
			fadeTime = 0.1f;
			if (Time.time - stateStartTime < fadeTime) {
				// pause
			}
			else {
				inputControls.ResetControls();		
				SetGameState("gameStateEnteringGameplay");
			}
			break;	
	
		case "gameStateEnteringGameplay":
			fadeTime = 1.7f;

			
			if (Time.time - stateStartTime < fadeTime) { // implements trans as a reverse of leavingGui trans
				float backwardsTime = (stateStartTime + fadeTime) - (Time.time - stateStartTime);	
				if (backwardsTime - stateStartTime < (fadeTime * 0.5f)) {
					// 1st half

					cameraRotX = highCameraRotX;

					fadePercentComplete = (backwardsTime - stateStartTime) / (fadeTime * 0.5f);
					fadePercentComplete = fadePercentComplete * fadePercentComplete;
					cameraY = highCameraY + fadePercentComplete * ((previousCameraY - highCameraY) * 0.5f);
					cameraDistance = highCameraDistance + fadePercentComplete * ((previousCameraDistance - highCameraDistance) * 0.5f);

					cameraRotOffsetY = 0f + fadePercentComplete * ((-120f - 0f) * 0.5f);
				}
				else if (backwardsTime - stateStartTime < fadeTime) {
					// 2nd half

					cameraRotPercentDone = (float)((float)(backwardsTime - stateStartTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);
					cameraRotX = highCameraRotX + ((previousCameraRotX - highCameraRotX) * cameraRotPercentDone);

					fadePercentComplete = ((backwardsTime - stateStartTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);				
					fadePercentComplete = fadePercentComplete + (fadePercentComplete - (fadePercentComplete * fadePercentComplete));
					cameraY = highCameraY + ((previousCameraY - highCameraY) * 0.5f) + fadePercentComplete * ((previousCameraY - highCameraY) * 0.5f);
					cameraDistance = highCameraDistance + ((previousCameraDistance - highCameraDistance) * 0.5f) + fadePercentComplete * ((previousCameraDistance - highCameraDistance) * 0.5f);

					cameraRotOffsetY = 0f + ((-120f - 0f) * 0.5f) + fadePercentComplete * ((-120f - 0f) * 0.5f);
				}		
			}
			else {
				cameraY = highCameraY;
				cameraRotX = highCameraRotX;
				cameraDistance = highCameraDistance;
				cameraRotOffsetY = 0;
				SetGameState("gameStateStalking");
			}


			break;	
	
		case "gameStateLeavingGameplay":
			fadeTime = 2f;
			guiFlybySpeed = (Time.time - stateStartTime) / fadeTime;


			if (previousCameraRotOffsetY > 60f) // constrain to within 180 degrees of -120 degrees (-120 is dest)
				previousCameraRotOffsetY -= 360f;
			if (previousCameraRotOffsetY < -300f)
				previousCameraRotOffsetY += 360f;


			if (Time.time - stateStartTime < fadeTime) { // implements trans as a reverse of leavingGui trans


				float backwardsTime = (stateStartTime + fadeTime) - (Time.time - stateStartTime);	
				if (backwardsTime - stateStartTime < (fadeTime * 0.5f)) {
					// 1st half

					cameraRotX = guiCameraRotX;

					fadePercentComplete = (backwardsTime - stateStartTime) / (fadeTime * 0.5f);
					fadePercentComplete = fadePercentComplete * fadePercentComplete;
					cameraY = guiCameraY + fadePercentComplete * ((previousCameraY - guiCameraY) * 0.5f);
					cameraDistance = guiCameraDistance + fadePercentComplete * ((previousCameraDistance - guiCameraDistance) * 0.5f);

					cameraRotOffsetY = -120f + fadePercentComplete * ((previousCameraRotOffsetY - -120f) * 0.5f);
				}
				else if (backwardsTime - stateStartTime < fadeTime) {
					// 2nd half

					cameraRotPercentDone = (float)((float)(backwardsTime - stateStartTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);
					cameraRotX = guiCameraRotX + ((previousCameraRotX - guiCameraRotX) * cameraRotPercentDone * cameraRotPercentDone);

					fadePercentComplete = ((backwardsTime - stateStartTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);				
					fadePercentComplete = fadePercentComplete + (fadePercentComplete - (fadePercentComplete * fadePercentComplete));
					cameraY = guiCameraY + ((previousCameraY - guiCameraY) * 0.5f) + fadePercentComplete * ((previousCameraY - guiCameraY) * 0.5f);
					cameraDistance = guiCameraDistance + ((previousCameraDistance - guiCameraDistance) * 0.5f) + fadePercentComplete * ((previousCameraDistance - guiCameraDistance) * 0.5f);

					cameraRotOffsetY = -120f + ((previousCameraRotOffsetY - -120f) * 0.5f) + fadePercentComplete * ((previousCameraRotOffsetY - -120f) * 0.5f);
				}	
			}			
			else {
				guiFlybySpeed = 1f;
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
					// first 3/4
					zoomPercentComplete = (Time.time - stateStartTime) / (zoomTime*0.75f);
					cameraY = highCameraY - ((highCameraY - medCameraY) * zoomPercentComplete);
					cameraRotX = highCameraRotX - ((highCameraRotX - medCameraRotX) * zoomPercentComplete);
					cameraDistance = highCameraDistance - ((highCameraDistance - medCameraDistance) * zoomPercentComplete);
				}
				else {
					// last 1/4	
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
				//System.Console.WriteLine("mainHeading: " + mainHeading.ToString() + "  deerCaughtHeading: " + deerCaughtHeading.ToString() + "  deerCaughtFinalHeading: " + deerCaughtFinalHeading.ToString());	
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
			inputControls.ResetControls();



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
					cameraDistance = closeupCameraDistance + fadePercentComplete * ((previousCameraDistance - closeupCameraDistance) * 0.5f);

					cameraRotOffsetY = -160f + fadePercentComplete * ((0f - -160f) * 0.5f);
				}
				else if (backwardsTime - stateStartTime < fadeTime) {
					// 2nd half

					fadePercentComplete = ((backwardsTime - stateStartTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);				
					fadePercentComplete = fadePercentComplete + (fadePercentComplete - (fadePercentComplete * fadePercentComplete));
					cameraY = closeupCameraY + ((previousCameraY - closeupCameraY) * 0.5f) + fadePercentComplete * ((previousCameraY - closeupCameraY) * 0.5f);
					cameraDistance = closeupCameraDistance + ((previousCameraDistance - closeupCameraDistance) * 0.5f) + fadePercentComplete * ((previousCameraDistance - closeupCameraDistance) * 0.5f);

					cameraRotOffsetY = -160f + ((0f - -160f) * 0.5f) + fadePercentComplete * ((0f - -160f) * 0.5f);
				}

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
			inputControls.ResetControls();
			if (Time.time - stateStartTime > 0.2f) {
				SetGameState("gameStateCaught3");
			}
			break;

		case "gameStateCaught3":
			fadeTime = 5f;
			inputControls.ResetControls();



			if (Time.time - stateStartTime < fadeTime) { // implements trans as a reverse of leavingGui trans
				float backwardsTime = (stateStartTime + fadeTime) - (Time.time - stateStartTime);	

				cameraRotX = previousCameraRotX + (eatingCameraRotX - previousCameraRotX) * ((Time.time - stateStartTime) / fadeTime);

				if (backwardsTime - stateStartTime < (fadeTime * 0.5f)) {
					// 1st half
	
					fadePercentComplete = (backwardsTime - stateStartTime) / (fadeTime * 0.5f);
					fadePercentComplete = fadePercentComplete * fadePercentComplete;
					cameraY = eatingCameraY + fadePercentComplete * ((previousCameraY - eatingCameraY) * 0.5f);
					cameraDistance = eatingCameraDistance + fadePercentComplete * ((previousCameraDistance - eatingCameraDistance) * 0.5f);
				}
				else if (backwardsTime - stateStartTime < fadeTime) {
					// 2nd half

					fadePercentComplete = ((backwardsTime - stateStartTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);				
					fadePercentComplete = fadePercentComplete + (fadePercentComplete - (fadePercentComplete * fadePercentComplete));
					cameraY = eatingCameraY + ((previousCameraY - eatingCameraY) * 0.5f) + fadePercentComplete * ((previousCameraY - eatingCameraY) * 0.5f);
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
			inputControls.ResetControls();



			if (Time.time - stateStartTime > 0.1f) {
				cameraRotOffsetY -= Time.deltaTime + 0.03f;
				if (cameraRotOffsetY < -180f)
					cameraRotOffsetY += 360f;
				previousCameraRotOffsetY = cameraRotOffsetY;
			}



			break;
					
		case "gameStateCaught5":
			fadeTime = 2.4f;
			pumaAnimator.SetBool("DeerKill", false);



			if (Time.time - stateStartTime < fadeTime) { // implements trans as a reverse of leavingGui trans

				cameraRotPercentDone = ((Time.time - stateStartTime) / fadeTime);
				cameraRotPercentDone = cameraRotPercentDone * cameraRotPercentDone;
				cameraRotX = previousCameraRotX + (highCameraRotX - previousCameraRotX) * cameraRotPercentDone;

				float backwardsTime = (stateStartTime + fadeTime) - (Time.time - stateStartTime);	
				if (backwardsTime - stateStartTime < (fadeTime * 0.5f)) {
					// 1st half

					fadePercentComplete = (backwardsTime - stateStartTime) / (fadeTime * 0.5f);
					fadePercentComplete = fadePercentComplete * fadePercentComplete;
					cameraY = highCameraY + fadePercentComplete * ((previousCameraY - highCameraY) * 0.5f);
					cameraDistance = highCameraDistance + fadePercentComplete * ((previousCameraDistance - highCameraDistance) * 0.5f);

					cameraRotOffsetY = 0f + fadePercentComplete * ((previousCameraRotOffsetY - 0f) * 0.5f);
				}
				else if (backwardsTime - stateStartTime < fadeTime) {
					// 2nd half

					fadePercentComplete = ((backwardsTime - stateStartTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);				
					fadePercentComplete = fadePercentComplete + (fadePercentComplete - (fadePercentComplete * fadePercentComplete));
					cameraY = highCameraY + ((previousCameraY - highCameraY) * 0.5f) + fadePercentComplete * ((previousCameraY - highCameraY) * 0.5f);
					cameraDistance = highCameraDistance + ((previousCameraDistance - highCameraDistance) * 0.5f) + fadePercentComplete * ((previousCameraDistance - highCameraDistance) * 0.5f);

					cameraRotOffsetY = 0f + ((previousCameraRotOffsetY - 0f) * 0.5f) + fadePercentComplete * ((previousCameraRotOffsetY - 0f) * 0.5f);
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
				scoringSystem.ClearLastKillInfo(selectedPuma);
			}





			break;	
	
		case "gameStateDied1":
			fadeTime = 3f;
			inputControls.ResetControls();



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
					cameraDistance = closeupCameraDistance + fadePercentComplete * ((previousCameraDistance - closeupCameraDistance) * 0.5f);

					cameraRotOffsetY = -160f + fadePercentComplete * ((0f - -160f) * 0.5f);
				}
				else if (backwardsTime - stateStartTime < fadeTime) {
					// 2nd half

					fadePercentComplete = ((backwardsTime - stateStartTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);				
					fadePercentComplete = fadePercentComplete + (fadePercentComplete - (fadePercentComplete * fadePercentComplete));
					cameraY = closeupCameraY + ((previousCameraY - closeupCameraY) * 0.5f) + fadePercentComplete * ((previousCameraY - closeupCameraY) * 0.5f);
					cameraDistance = closeupCameraDistance + ((previousCameraDistance - closeupCameraDistance) * 0.5f) + fadePercentComplete * ((previousCameraDistance - closeupCameraDistance) * 0.5f);

					cameraRotOffsetY = -160f + ((0f - -160f) * 0.5f) + fadePercentComplete * ((0f - -160f) * 0.5f);
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
			inputControls.ResetControls();
			if (Time.time - stateStartTime > 0.2f) {
				SetGameState("gameStateDied3");
			}
			break;

		case "gameStateDied3":
			fadeTime = 5f;
			inputControls.ResetControls();



			if (Time.time - stateStartTime < fadeTime) { // implements trans as a reverse of leavingGui trans

				cameraRotX = previousCameraRotX + (eatingCameraRotX - previousCameraRotX) * ((Time.time - stateStartTime) / fadeTime);

				float backwardsTime = (stateStartTime + fadeTime) - (Time.time - stateStartTime);	
				if (backwardsTime - stateStartTime < (fadeTime * 0.5f)) {
					// 1st half

					fadePercentComplete = (backwardsTime - stateStartTime) / (fadeTime * 0.5f);
					fadePercentComplete = fadePercentComplete * fadePercentComplete;
					cameraY = eatingCameraY + fadePercentComplete * ((previousCameraY - eatingCameraY) * 0.5f);
					cameraDistance = eatingCameraDistance + fadePercentComplete * ((previousCameraDistance - eatingCameraDistance) * 0.5f);
				}
				else if (backwardsTime - stateStartTime < fadeTime) {
					// 2nd half

					fadePercentComplete = ((backwardsTime - stateStartTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);				
					fadePercentComplete = fadePercentComplete + (fadePercentComplete - (fadePercentComplete * fadePercentComplete));
					cameraY = eatingCameraY + ((previousCameraY - eatingCameraY) * 0.5f) + fadePercentComplete * ((previousCameraY - eatingCameraY) * 0.5f);
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
			inputControls.ResetControls();


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
			fadeTime = 2.4f;
			pumaAnimator.SetBool("DeerKill", false);


			cameraRotPercentDone = ((Time.time - stateStartTime) / fadeTime);
			cameraRotPercentDone = cameraRotPercentDone * cameraRotPercentDone;
			cameraRotX = previousCameraRotX + (highCameraRotX - previousCameraRotX) * cameraRotPercentDone;

			if (Time.time - stateStartTime < fadeTime) { // implements trans as a reverse of leavingGui trans
				float backwardsTime = (stateStartTime + fadeTime) - (Time.time - stateStartTime);	
				if (backwardsTime - stateStartTime < (fadeTime * 0.5f)) {
					// 1st half

					fadePercentComplete = (backwardsTime - stateStartTime) / (fadeTime * 0.5f);
					fadePercentComplete = fadePercentComplete * fadePercentComplete;
					cameraY = highCameraY + fadePercentComplete * ((previousCameraY - highCameraY) * 0.5f);
					cameraDistance = highCameraDistance + fadePercentComplete * ((previousCameraDistance - highCameraDistance) * 0.5f);

					cameraRotOffsetY = 0f + fadePercentComplete * ((previousCameraRotOffsetY - 0f) * 0.5f);
				}
				else if (backwardsTime - stateStartTime < fadeTime) {
					// 2nd half

					fadePercentComplete = ((backwardsTime - stateStartTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);				
					fadePercentComplete = fadePercentComplete + (fadePercentComplete - (fadePercentComplete * fadePercentComplete));
					cameraY = highCameraY + ((previousCameraY - highCameraY) * 0.5f) + fadePercentComplete * ((previousCameraY - highCameraY) * 0.5f);
					cameraDistance = highCameraDistance + ((previousCameraDistance - highCameraDistance) * 0.5f) + fadePercentComplete * ((previousCameraDistance - highCameraDistance) * 0.5f);

					cameraRotOffsetY = 0f + ((previousCameraRotOffsetY - 0f) * 0.5f) + fadePercentComplete * ((previousCameraRotOffsetY - 0f) * 0.5f);
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
				scoringSystem.ClearLastKillInfo(selectedPuma);
			}




			break;	

		}		
		


		//=======================
		// Update Positions
		//=======================
			
		float distance = 0f;

		pumaAnimator.SetBool("GuiMode", false);

		if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftControl)) {
			// dev only: camera distance and angle
			cameraDistance -= inputControls.GetInputVert() * Time.deltaTime * 20 * 5 * speedOverdrive;
			cameraRotOffsetY += inputControls.GetInputHorz() * Time.deltaTime * 40 * 5 * speedOverdrive;
			pumaHeading = mainHeading;
			inputControls.ResetControls();
		}
		
		else if (Input.GetKey(KeyCode.LeftShift)) {
			// dev only: camera height
			cameraY += inputControls.GetInputVert() * Time.deltaTime  * 20 * 5 * speedOverdrive;
			pumaHeading = mainHeading;
			inputControls.ResetControls();
		}
		
		else if (Input.GetKey(KeyCode.LeftControl)) {
			// dev only: camera pitch
			cameraRotX += inputControls.GetInputVert() * Time.deltaTime  * 50 * 5 * speedOverdrive;
			pumaHeading = mainHeading;
			inputControls.ResetControls();
		}
		
		else if (gameState == "gameStateGui" || gameState == "gameStateLeavingGameplay" || gameState == "gameStateLeavingGui") {
			if ((gameState != "gameStateLeavingGui") || (Time.time - stateStartTime < 1.8f))
				pumaAnimator.SetBool("GuiMode", true);
			distance = guiFlybySpeed * Time.deltaTime  * 12f * speedOverdrive;
			pumaHeading = mainHeading;
			pumaX += (Mathf.Sin(mainHeading*Mathf.PI/180) * distance);
			pumaZ += (Mathf.Cos(mainHeading*Mathf.PI/180) * distance);
		}
		
		else if (gameState == "gameStateCloseup" || gameState == "gameStateEnteringGameplay" ||
		         gameState == "gameStateCaught1" || gameState == "gameStateCaught2" || gameState == "gameStateCaught3" || gameState == "gameStateCaught4" || gameState == "gameStateCaught5" ||
				 gameState == "gameStateDied1" || gameState == "gameStateDied2" || gameState == "gameStateDied3" || gameState == "gameStateDied4" || gameState == "gameStateDied5")
		{
			distance = 0f;
			pumaHeading = mainHeading;
			pumaX += (Mathf.Sin(mainHeading*Mathf.PI/180) * distance);
			pumaZ += (Mathf.Cos(mainHeading*Mathf.PI/180) * distance);
		}
		
		else if (gameState == "gameStateChasing") {
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
		
		else if (gameState == "gameStateStalking") {		
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

		pumaAnimator.SetFloat("Distance", distance);

		cameraRotY = mainHeading + cameraRotOffsetY;
	
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

		float adjustedCameraX = cameraX;
		float adjustedCameraY = cameraY + GetTerrainHeight(cameraX, cameraZ);
		float adjustedCameraZ = cameraZ;	

		float idealVisualDistance = Vector3.Distance(new Vector3(0, 0, 0), new Vector3(cameraDistance, cameraY, 0));
		float currentVisualAngle = GetAngleFromOffset(0, pumaY, cameraDistance, adjustedCameraY);
		float adjustedCameraDistance = Mathf.Sin(currentVisualAngle*Mathf.PI/180) * idealVisualDistance;

		adjustedCameraY = pumaY + Mathf.Cos(currentVisualAngle*Mathf.PI/180) * idealVisualDistance;
		adjustedCameraX = pumaX - (Mathf.Sin(cameraRotY*Mathf.PI/180) * adjustedCameraDistance);
		adjustedCameraZ = pumaZ - (Mathf.Cos(cameraRotY*Mathf.PI/180) * adjustedCameraDistance);	

		float cameraRotXAdjustment = -1f * (GetAngleFromOffset(0, pumaY, cameraDistance, GetTerrainHeight(cameraX, cameraZ)) - 90f);
		cameraRotXAdjustment *= (cameraRotXAdjustment > 0) ? 0.65f : 0.8f;
		float adjustedCameraRotX = cameraRotX + cameraRotXAdjustment;

		displayVar1 = cameraRotX;
		displayVar2 = cameraRotXAdjustment;
		displayVar3 = adjustedCameraRotX;
		

		
		if (gameState == "gameStateGui") {
			cameraController.UpdateCameraPosition(pumaX, pumaY, pumaZ, mainHeading);
		}


		if (gameState == "gameStateLeavingGui") {
			cameraController.UpdateCameraPosition(pumaX, pumaY, pumaZ, mainHeading);
		}
		else {
			// update camera obj
			Camera.main.transform.position = new Vector3(adjustedCameraX, adjustedCameraY, adjustedCameraZ);
			Camera.main.transform.rotation = Quaternion.Euler(adjustedCameraRotX, cameraRotY, cameraRotZ);
		}
		
		
		
		
		
		
		// update deer objects
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





	void SelectCameraPosition(string targetPositionLabel, float targetRotOffsetY, float fadeTime, string mainCurve, string rotXCurve)
	{
		if (stateInitFlag == false) {
			cameraController.SelectRelativePosition(targetPositionLabel, targetRotOffsetY, fadeTime, mainCurve, rotXCurve);
			stateInitFlag = true;
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
	

	public float GetTerrainHeight(float x, float z)
	{
		float terrainX;
		float terrainZ;
	
		terrainX = terrain1.transform.position.x;
		terrainZ = terrain1.transform.position.z;		
		if (x >= terrainX && x < terrainX + 2000 && z >= terrainZ && z < terrainZ + 2000) {
			return  terrain1.SampleHeight(new Vector3(x, 0, z));
		}
			
		terrainX = terrain2.transform.position.x;
		terrainZ = terrain2.transform.position.z;		
		if (x >= terrainX && x < terrainX + 2000 && z >= terrainZ && z < terrainZ + 2000) {
			return  terrain2.SampleHeight(new Vector3(x, 0, z));
		}
			
		terrainX = terrain3.transform.position.x;
		terrainZ = terrain3.transform.position.z;		
		if (x >= terrainX && x < terrainX + 2000 && z >= terrainZ && z < terrainZ + 2000) {
			return  terrain3.SampleHeight(new Vector3(x, 0, z));
		}
			
		terrainX = terrain4.transform.position.x;
		terrainZ = terrain4.transform.position.z;		
		if (x >= terrainX && x < terrainX + 2000 && z >= terrainZ && z < terrainZ + 2000) {
			return  terrain4.SampleHeight(new Vector3(x, 0, z));
		}
			
		return 0f;
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

}














