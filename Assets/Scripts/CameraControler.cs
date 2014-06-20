using UnityEngine;
using System.Collections;


// Public class "CameraControler"
// 		Controls camera behavior
// Public Methods:
// 		void setCameraPosition(string cameraPosition, bool usesRotOffset=false, float rotOffset=0f)
//		void startCamera(float pumaX, float pumaY, float pumaZ, float angle)
//		void savePreviousCamera()
//		void setGuiFlybySpeed(float guiFlybySpeed)
//		void setTransition(string curveLabel, string cameraPosition="", float fadeTime=0f, float currentTime=0f, float startTime=0f)
//		void userInput(string inputType, float inputVert, float deltaTime, float speedOverdrive, float inputHorz=0f)
//		void finalCameraAdjustments(float mainHeading, float pumaX, float pumaY, float pumaZ)
public class CameraControler : MonoBehaviour {

	//=====================
	//  Module Variables
	//=====================

	//Camera Position
	private float cameraX = 0f;
	private float cameraY = 0f;
	private float cameraZ = 0f;
	//Camera Rotation
	private float cameraRotX = 0f;
	private float cameraRotY = 0f;
	private float cameraRotZ = 0f;
	//Camera distance to puma
	private float cameraDistance = 0f;
	//Angle camera out of the main/normal Rotation
	private float cameraRotOffsetY = 0f;
	private float previousCameraRotOffsetY = 0f;

	//Camera closeup settings (used when entering gameplay and after killing a deer)
	private float closeupCameraY = 2.75f;
	private float closeupCameraRotX = 2.75f;
	private float closeupCameraDistance = 6.5f;
	//Camera settings while walking
	private float highCameraY = 5.7f;
	private float highCameraRotX = 12.8f;
	private float highCameraDistance = 8.6f;
	//Camera mid-point from walking to chasing
	private float medCameraY = 4f;
	private float medCameraRotX = 4f;
	private float medCameraDistance = 7.5f;
	//Camera settings while chasing
	private float lowCameraY = 3f;
	private float lowCameraRotX = -2f;
	private float lowCameraDistance = 7f;
	//Camera settings while eating
	private float eatingCameraY = 9f;
	private float eatingCameraRotX = 30f;
	private float eatingCameraDistance = 9f;
	//Camera settings while at GUI mode
	private float guiCameraY = 90f;
	private float guiCameraRotX = 33f; //45f;
	private float guiCameraDistance = 48f;
	//Previous camera configuration
	private float previousCameraY = 0f;
	private float previousCameraRotX = 0f;
	private float previousCameraDistance = 0f;

	//Camera frame-Update settings
	private float fadePercentComplete;
	private float cameraRotPercentDone;
	public float guiFlybySpeed = 0f;

	//=====================
	//  External Modules
	//=====================	
	private LevelManager levelManager;


	//=====================
	//  Initialization
	//=====================	

	// Use this for initialization
	void Start ()
	{
		levelManager = GetComponent<LevelManager>();

	}

	//=====================
	//  Public Methods
	//=====================	
	
	// Public method "setCameraPosition"
	// Args: cameraPosition (string, specifies which camera-position option the camera should use).
	// 		 usesRotOffset (boolean, specifies whether the call will update the Rotation Offset or not. Set up as false by default).
	// 		 rotOffset (float, specifies the value for rotation offset)
	// Sets the camera in a specific position
	// Options are: guiCamera, highCamera, medCamera, lowCamera, eatingCamera, and closeupCamera.
	public void setCameraPosition(string cameraPosition, bool usesRotOffset=false, float rotOffset=0f)
	{
		switch (cameraPosition)
		{
			case "guiCamera":
				cameraY = guiCameraY;
				cameraRotX = guiCameraRotX;
				cameraDistance = guiCameraDistance;
				if (usesRotOffset==true) cameraRotOffsetY = rotOffset;
				break;

			case "closeupCamera":
				cameraY = closeupCameraY;
				cameraRotX = closeupCameraRotX;
				cameraDistance = closeupCameraDistance;
				if (usesRotOffset==true) cameraRotOffsetY = rotOffset;
				break;

			case "highCamera":
				cameraY = highCameraY;
				cameraRotX = highCameraRotX;
				cameraDistance = highCameraDistance;
				if (usesRotOffset == true) previousCameraRotOffsetY = cameraRotOffsetY = rotOffset;
				break;

			case "medCamera":
				cameraY = medCameraY;
				cameraRotX = medCameraRotX;
				cameraDistance = medCameraDistance;
				break;

			case "lowCamera":
				cameraY = lowCameraY;
				cameraRotX = lowCameraRotX;
				cameraDistance = lowCameraDistance;
				break;

			case "eatingCamera":
				cameraY = eatingCameraY;
				cameraRotX = eatingCameraRotX;
				cameraDistance = eatingCameraDistance;
				break;

		}
	}

	// Public method "startCamera"
	// Args: float (pumaX, pumaY, and pumaZ) => describe the puma's position.
	// 		 mainHeading => mainHeading value.
	// Sets the camera in its correct initial position
	public void startCamera(float pumaX, float pumaY, float pumaZ, float angle)
	{
		cameraX = pumaX - (Mathf.Sin(angle*Mathf.PI/180) * cameraDistance);
		cameraZ = pumaZ - (Mathf.Cos(angle*Mathf.PI/180) * cameraDistance);
	
		Camera.main.transform.position = new Vector3(cameraX, cameraY, cameraZ);
		Camera.main.transform.rotation = Quaternion.Euler(cameraRotX, angle, cameraRotZ);

	}

	// Public method "savePreviousCamera"
	// Saves the current camera configuration to the previousCamera variables.
	public void savePreviousCamera()
	{
		previousCameraY = cameraY;
		previousCameraRotX = cameraRotX;
		previousCameraDistance = cameraDistance;
		previousCameraRotOffsetY = cameraRotOffsetY;
	}

	///////TEMP!!!!(?)
	public void setGuiFlybySpeed(float guiFlybySpeed)
	{
		this.guiFlybySpeed = guiFlybySpeed;
	}

	// Public Method "setTransition"
	// Args: curveLabel (string, label that defines which function/curve should be used to update the camera)
	// 		 cameraPosition (string, defines which is the desired camera position at the end of the transition)
	// 		 fadeTime (float, defines the total fade time)
	// 		 currentTime (float, the time stamp at the moment LevelManager called this function [depending on the curve, it might be the current time, or backwards time]).
	// 		 startTime (float, the time the game state had started).
	public void setTransition(string curveLabel, string cameraPosition="", float fadeTime=0f, float currentTime=0f, float startTime=0f)
	{	
		//Variables used to generalize camera positions, so any curve/formula can be used to reach any final position
		float targetCameraY = 0f;
		float targetCameraRotX = 0f;
		float targetCameraDistance = 0f;
		//Variables used to generalize camera position in the case of the high -> med -> low camera transitions
		float currentCameraY = 0f;
		float currentCameraRotX = 0f;
		float currentCameraDistance = 0f;
		//BackwardsTime var
		float backwardsTime = 0f;


		//Sets the target camera configurations
		switch (cameraPosition)
		{
			case "closeupCamera":
				targetCameraY = closeupCameraY;
				targetCameraRotX = closeupCameraRotX;
				targetCameraDistance = closeupCameraDistance;
				break;

			case "highCamera":
				targetCameraY = highCameraY;
				targetCameraRotX = highCameraRotX;
				targetCameraDistance = highCameraDistance;
				break;

			case "guiCamera":
				targetCameraY = guiCameraY;
				targetCameraRotX = guiCameraRotX;
				targetCameraDistance = guiCameraDistance;
				break;

			case "medCamera":
				targetCameraY = medCameraY;
				targetCameraRotX = medCameraRotX;
				targetCameraDistance = medCameraDistance;

				currentCameraY = highCameraY;
				currentCameraRotX = highCameraRotX;
				currentCameraDistance = highCameraDistance;
				break;

			case "eatingCamera":
				targetCameraY = eatingCameraY;
				targetCameraRotX = eatingCameraRotX;
				targetCameraDistance = eatingCameraDistance;
				break;

		}

		//Computes frame-update for the camera in a given curve/function.
		switch (curveLabel)
		{
			case "curveLabel1":
				guiFlybySpeed = 1f - (currentTime - startTime) / fadeTime;
				fadePercentComplete = (currentTime - startTime) / (fadeTime * 0.5f);
				fadePercentComplete = fadePercentComplete * fadePercentComplete;
				cameraY = previousCameraY + fadePercentComplete * ((targetCameraY - previousCameraY) * 0.5f);
				cameraRotX = previousCameraRotX;
				cameraDistance = previousCameraDistance + fadePercentComplete * ((targetCameraDistance - previousCameraDistance) * 0.5f);
				break;

			case "curveLabel2":
				guiFlybySpeed = 1f - (currentTime - startTime) / fadeTime;
				fadePercentComplete = ((currentTime - startTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);				
				fadePercentComplete = fadePercentComplete + (fadePercentComplete - (fadePercentComplete * fadePercentComplete));
				cameraY = previousCameraY + ((targetCameraY - previousCameraY) * 0.5f) + fadePercentComplete * ((targetCameraY - previousCameraY) * 0.5f);
				cameraRotPercentDone = (float)((float)(currentTime - startTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);
				cameraRotX = previousCameraRotX + ((targetCameraRotX - previousCameraRotX) * cameraRotPercentDone * cameraRotPercentDone);
				cameraDistance = previousCameraDistance + ((targetCameraDistance - previousCameraDistance) * 0.5f) + fadePercentComplete * ((targetCameraDistance - previousCameraDistance) * 0.5f);
				break;

			//backwardsTime
			case "curveLabel3":
				fadePercentComplete = (currentTime - startTime) / (fadeTime * 0.5f);
				fadePercentComplete = fadePercentComplete * fadePercentComplete;
				cameraY = targetCameraY + fadePercentComplete * ((previousCameraY - targetCameraY) * 0.5f);
				cameraRotOffsetY = 0f + fadePercentComplete * ((-120f - 0f) * 0.5f);
				cameraRotX = targetCameraRotX;
				cameraDistance = targetCameraDistance + fadePercentComplete * ((previousCameraDistance - targetCameraDistance) * 0.5f);
				break;
			//backwardsTime
			case "curveLabel4":
				fadePercentComplete = ((currentTime - startTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);				
				fadePercentComplete = fadePercentComplete + (fadePercentComplete - (fadePercentComplete * fadePercentComplete));
				cameraY = targetCameraY + ((previousCameraY - targetCameraY) * 0.5f) + fadePercentComplete * ((previousCameraY - targetCameraY) * 0.5f);
				cameraRotOffsetY = 0f + ((-120f - 0f) * 0.5f) + fadePercentComplete * ((-120f - 0f) * 0.5f);
				cameraRotPercentDone = (float)((float)(currentTime - startTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);
				cameraRotX = targetCameraRotX + ((previousCameraRotX - targetCameraRotX) * cameraRotPercentDone);
				cameraDistance = targetCameraDistance + ((previousCameraDistance - targetCameraDistance) * 0.5f) + fadePercentComplete * ((previousCameraDistance - targetCameraDistance) * 0.5f);
				break;
			//backwardsTime
			case "curveLabel5":
				guiFlybySpeed = 1f - (currentTime - startTime) / fadeTime;
				fadePercentComplete = (currentTime - startTime) / (fadeTime * 0.5f);
				fadePercentComplete = fadePercentComplete * fadePercentComplete;
				cameraY = targetCameraY + fadePercentComplete * ((previousCameraY - targetCameraY) * 0.5f);
				if (previousCameraRotOffsetY > 60f) // constrain to within 180 degrees of -120 degrees (-120 is dest)
					previousCameraRotOffsetY -= 360f;
				if (previousCameraRotOffsetY < -300f)
					previousCameraRotOffsetY += 360f;
				cameraRotOffsetY = -120f + fadePercentComplete * ((previousCameraRotOffsetY - -120f) * 0.5f);
				cameraRotX = targetCameraRotX;
				cameraDistance = targetCameraDistance + fadePercentComplete * ((previousCameraDistance - targetCameraDistance) * 0.5f);
				break;
			//backwardsTime
			case "curveLabel6":
				guiFlybySpeed = 1f - (currentTime - startTime) / fadeTime;
				fadePercentComplete = ((currentTime - startTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);				
				fadePercentComplete = fadePercentComplete + (fadePercentComplete - (fadePercentComplete * fadePercentComplete));
				cameraY = targetCameraY + ((previousCameraY - targetCameraY) * 0.5f) + fadePercentComplete * ((previousCameraY - targetCameraY) * 0.5f);
				if (previousCameraRotOffsetY > 60f) // constrain to within 180 degrees of -120 degrees (-120 is dest)
					previousCameraRotOffsetY -= 360f;
				if (previousCameraRotOffsetY < -300f)
					previousCameraRotOffsetY += 360f;
				cameraRotOffsetY = -120f + ((previousCameraRotOffsetY - -120f) * 0.5f) + fadePercentComplete * ((previousCameraRotOffsetY - -120f) * 0.5f);
				cameraRotPercentDone = (float)((float)(currentTime - startTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);
				cameraRotX = targetCameraRotX + ((previousCameraRotX - targetCameraRotX) * cameraRotPercentDone * cameraRotPercentDone);
				cameraDistance = targetCameraDistance + ((previousCameraDistance - targetCameraDistance) * 0.5f) + fadePercentComplete * ((previousCameraDistance - targetCameraDistance) * 0.5f);
				break;
			//TO BE REDEFINED!!!!!!!!!!!!!!!!!!!!!
			//Using "high" and "med" are not good options
			case "curveLabel7":
				float zoomTime = fadeTime;
				float zoomPercentComplete;
				if (currentTime - startTime < zoomTime*0.75f) {
					zoomPercentComplete = (currentTime - startTime) / (zoomTime*0.75f);
					cameraY = currentCameraY - ((currentCameraY - targetCameraY) * zoomPercentComplete);
					cameraRotX = highCameraRotX - ((highCameraRotX - medCameraRotX) * zoomPercentComplete);
					cameraDistance = highCameraDistance - ((highCameraDistance - medCameraDistance) * zoomPercentComplete);
				}
				else {
					zoomPercentComplete = (currentTime - startTime - (zoomTime*0.75f)) / (zoomTime*0.25f);
					cameraY = targetCameraY - ((targetCameraY - lowCameraY) * zoomPercentComplete);
					cameraRotX = medCameraRotX - ((medCameraRotX - lowCameraRotX) * zoomPercentComplete);
					cameraDistance = medCameraDistance - ((medCameraDistance - lowCameraDistance) * zoomPercentComplete);
				}
				break;

			case "curveLabel8":
				cameraRotPercentDone = ((currentTime - startTime) / fadeTime);
				cameraRotPercentDone = cameraRotPercentDone * cameraRotPercentDone;
				cameraRotX = previousCameraRotX + (targetCameraRotX - previousCameraRotX) * cameraRotPercentDone;

				backwardsTime = (startTime + fadeTime) - (currentTime - startTime);	
				if (backwardsTime - startTime < (fadeTime * 0.5f)) {
					// 1st half
					fadePercentComplete = (backwardsTime - startTime) / (fadeTime * 0.5f);
					fadePercentComplete = fadePercentComplete * fadePercentComplete;
					cameraY = targetCameraY + fadePercentComplete * ((previousCameraY - targetCameraY) * 0.5f);
					cameraRotOffsetY = -160f + fadePercentComplete * ((0f - -160f) * 0.5f);
					cameraDistance = targetCameraDistance + fadePercentComplete * ((previousCameraDistance - targetCameraDistance) * 0.5f);
				}
				else if (backwardsTime - startTime < fadeTime) {
					// 2nd half
					fadePercentComplete = ((backwardsTime - startTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);				
					fadePercentComplete = fadePercentComplete + (fadePercentComplete - (fadePercentComplete * fadePercentComplete));
					cameraY = targetCameraY + ((previousCameraY - targetCameraY) * 0.5f) + fadePercentComplete * ((previousCameraY - targetCameraY) * 0.5f);
					cameraRotOffsetY = -160f + ((0f - -160f) * 0.5f) + fadePercentComplete * ((0f - -160f) * 0.5f);
					cameraDistance = targetCameraDistance + ((previousCameraDistance - targetCameraDistance) * 0.5f) + fadePercentComplete * ((previousCameraDistance - targetCameraDistance) * 0.5f);
				}
				break;
			//backwardsTime
			case "curveLabel9":
				//guiFlybySpeed = 1f - (backwardsTime - stateStartTime) / fadeTime;
				fadePercentComplete = (currentTime - startTime) / (fadeTime * 0.5f);
				fadePercentComplete = fadePercentComplete * fadePercentComplete;
				cameraY = targetCameraY + fadePercentComplete * ((previousCameraY - targetCameraY) * 0.5f);
				//cameraRotOffsetY = -120f + fadePercentComplete * ((0f - -120f) * 0.5f);
				//cameraRotX = eatingCameraRotX + fadePercentComplete * ((previousCameraRotX - eatingCameraRotX) * 0.5f);
				cameraRotX = previousCameraRotX + (targetCameraRotX - previousCameraRotX) * ((Time.time - startTime) / fadeTime);
				cameraDistance = targetCameraDistance + fadePercentComplete * ((previousCameraDistance - targetCameraDistance) * 0.5f);
				break;
			//backwardsTime
			case "curveLabel10":
				//guiFlybySpeed = 1f - (backwardsTime - stateStartTime) / fadeTime;
				fadePercentComplete = ((currentTime - startTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);				
				fadePercentComplete = fadePercentComplete + (fadePercentComplete - (fadePercentComplete * fadePercentComplete));
				cameraY = eatingCameraY + ((previousCameraY - targetCameraY) * 0.5f) + fadePercentComplete * ((previousCameraY - targetCameraY) * 0.5f);
				//cameraRotOffsetY = -120f + ((0f - -120f) * 0.5f) + fadePercentComplete * ((0f - -120f) * 0.5f);
				cameraRotPercentDone = (float)((float)(currentTime - startTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);
				cameraRotX = previousCameraRotX + (targetCameraRotX - previousCameraRotX) * ((Time.time - startTime) / fadeTime);
				cameraDistance = targetCameraDistance + ((previousCameraDistance - targetCameraDistance) * 0.5f) + fadePercentComplete * ((previousCameraDistance - targetCameraDistance) * 0.5f);
				break;
			//already used twice
			case "curveLabel11":
				cameraRotOffsetY -= Time.deltaTime + 0.03f;
				if (cameraRotOffsetY < -180f)
					cameraRotOffsetY += 360f;
				previousCameraRotOffsetY = cameraRotOffsetY;
				break;
			//already used twice
			case "curveLabel12":
				cameraRotPercentDone = ((currentTime - startTime) / fadeTime);
				cameraRotPercentDone = cameraRotPercentDone * cameraRotPercentDone;
				cameraRotX = previousCameraRotX + (targetCameraRotX - previousCameraRotX) * cameraRotPercentDone;

				backwardsTime = (startTime + fadeTime) - (currentTime - startTime);	
				if (backwardsTime - startTime < (fadeTime * 0.5f)) {
					// 1st half
					//guiFlybySpeed = 1f - (backwardsTime - stateStartTime) / fadeTime;
					fadePercentComplete = (backwardsTime - startTime) / (fadeTime * 0.5f);
					fadePercentComplete = fadePercentComplete * fadePercentComplete;
					cameraY = targetCameraY + fadePercentComplete * ((previousCameraY - targetCameraY) * 0.5f);
					cameraRotOffsetY = 0f + fadePercentComplete * ((previousCameraRotOffsetY - 0f) * 0.5f);
					//cameraRotX = highCameraRotX + fadePercentComplete * ((previousCameraRotX - highCameraRotX) * 0.5f);
					//cameraRotX = previousCameraRotX + (highCameraRotX - previousCameraRotX) * ((Time.time - stateStartTime) / fadeTime);
					cameraDistance = targetCameraDistance + fadePercentComplete * ((previousCameraDistance - targetCameraDistance) * 0.5f);
				}
				else if (backwardsTime - startTime < fadeTime) {
					// 2nd half
					//guiFlybySpeed = 1f - (backwardsTime - stateStartTime) / fadeTime;
					fadePercentComplete = ((backwardsTime - startTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);				
					fadePercentComplete = fadePercentComplete + (fadePercentComplete - (fadePercentComplete * fadePercentComplete));
					cameraY = targetCameraY + ((previousCameraY - targetCameraY) * 0.5f) + fadePercentComplete * ((previousCameraY - targetCameraY) * 0.5f);
					cameraRotOffsetY = 0f + ((previousCameraRotOffsetY - 0f) * 0.5f) + fadePercentComplete * ((previousCameraRotOffsetY - 0f) * 0.5f);
					cameraRotPercentDone = (float)((float)(backwardsTime - startTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);
					//cameraRotX = previousCameraRotX + (highCameraRotX - previousCameraRotX) * ((Time.time - stateStartTime) / fadeTime);
					cameraDistance = targetCameraDistance + ((previousCameraDistance - targetCameraDistance) * 0.5f) + fadePercentComplete * ((previousCameraDistance - targetCameraDistance) * 0.5f);
				}
				break;

			case "curveLabel13":
				cameraRotPercentDone = ((currentTime - startTime) / fadeTime);
				cameraRotPercentDone = cameraRotPercentDone * cameraRotPercentDone;
				cameraRotX = previousCameraRotX + (targetCameraRotX - previousCameraRotX) * cameraRotPercentDone;

				backwardsTime = (startTime + fadeTime) - (currentTime - startTime);	
				if (backwardsTime - startTime < (fadeTime * 0.5f)) {
					// 1st half
					fadePercentComplete = (backwardsTime - startTime) / (fadeTime * 0.5f);
					fadePercentComplete = fadePercentComplete * fadePercentComplete;
					cameraY = targetCameraY + fadePercentComplete * ((previousCameraY - targetCameraY) * 0.5f);
					cameraRotOffsetY = -160f + fadePercentComplete * ((0f - -160f) * 0.5f);
					cameraDistance = targetCameraDistance + fadePercentComplete * ((previousCameraDistance - targetCameraDistance) * 0.5f);
				}
				else if (backwardsTime - startTime < fadeTime) {
					// 2nd half
					fadePercentComplete = ((backwardsTime - startTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);				
					fadePercentComplete = fadePercentComplete + (fadePercentComplete - (fadePercentComplete * fadePercentComplete));
					cameraY = targetCameraY + ((previousCameraY - targetCameraY) * 0.5f) + fadePercentComplete * ((previousCameraY - targetCameraY) * 0.5f);
					cameraRotOffsetY = -160f + ((0f - -160f) * 0.5f) + fadePercentComplete * ((0f - -160f) * 0.5f);
					cameraDistance = targetCameraDistance + ((previousCameraDistance - targetCameraDistance) * 0.5f) + fadePercentComplete * ((previousCameraDistance - targetCameraDistance) * 0.5f);
				}
				break;
			//backwardsTime
			case "curveLabel14":
				fadePercentComplete = (currentTime - startTime) / (fadeTime * 0.5f);
				fadePercentComplete = fadePercentComplete * fadePercentComplete;
				cameraY = targetCameraY + fadePercentComplete * ((previousCameraY - targetCameraY) * 0.5f);
				cameraRotX = previousCameraRotX + (targetCameraRotX - previousCameraRotX) * ((Time.time - startTime) / fadeTime);
				cameraDistance = targetCameraDistance + fadePercentComplete * ((previousCameraDistance - targetCameraDistance) * 0.5f);
				break;
			//backwardsTime
			case "curveLabel15":
				//guiFlybySpeed = 1f - (backwardsTime - stateStartTime) / fadeTime;
				fadePercentComplete = ((currentTime - startTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);				
				fadePercentComplete = fadePercentComplete + (fadePercentComplete - (fadePercentComplete * fadePercentComplete));
				cameraY = targetCameraY + ((previousCameraY - targetCameraY) * 0.5f) + fadePercentComplete * ((previousCameraY - targetCameraY) * 0.5f);
				//cameraRotOffsetY = -120f + ((0f - -120f) * 0.5f) + fadePercentComplete * ((0f - -120f) * 0.5f);
				cameraRotPercentDone = (float)((float)(currentTime - startTime) - (fadeTime * 0.5f)) / (fadeTime * 0.5f);
				cameraRotX = previousCameraRotX + (targetCameraRotX - previousCameraRotX) * ((Time.time - startTime) / fadeTime);
				cameraDistance = targetCameraDistance + ((previousCameraDistance - targetCameraDistance) * 0.5f) + fadePercentComplete * ((previousCameraDistance - targetCameraDistance) * 0.5f);
				break;
		} ///End switch
				
	}/// End method

	// Public method "userInput"
	// Updates camera distance and angle, height, and pitch, according to user input
	public void userInput(string inputType, float inputVert, float deltaTime, float speedOverdrive, float inputHorz=0f)
	{
		switch (inputType)
		{
			case "distanceAndAngle":
				cameraDistance -= inputVert * deltaTime * 20 * 5 * speedOverdrive;
				cameraRotOffsetY += inputHorz * deltaTime * 40 * 5 * speedOverdrive;
				break;

			case "height":
				cameraY += inputVert * deltaTime  * 20 * 5 * speedOverdrive;
				break;

			case "pitch":
				cameraRotX += inputVert * deltaTime  * 50 * 5 * speedOverdrive;
				break;
		}
	}

	// Public method "finalCameraAdjustments"
	// calculate camera adjustments based on terrain
	public void finalCameraAdjustments(float mainHeading, float pumaX, float pumaY, float pumaZ)
	{
		cameraRotY = mainHeading + cameraRotOffsetY;
		
		cameraX = pumaX - (Mathf.Sin(cameraRotY*Mathf.PI/180) * cameraDistance);
		cameraZ = pumaZ - (Mathf.Cos(cameraRotY*Mathf.PI/180) * cameraDistance);	
	
		// calculate camera adjustments based on terrain
		//-----------------------------------------------
			// initially camera goes to 'cameraY' units above terrain
			// that screws up the distance to the puma in extreme slope terrain
			// the camera is then moved to the 'correct' distance along the vector from puma to camera
			// that screws up the viewing angle, putting the puma too high or low in field of view
			// lastly we calculate an angle offset for new position, and factor in some fudge to account for viewing angle problem

		float adjustedCameraX = cameraX;
		float adjustedCameraY = cameraY + levelManager.GetTerrainHeight(cameraX, cameraZ);
		float adjustedCameraZ = cameraZ;	

		float idealVisualDistance = Vector3.Distance(new Vector3(0, 0, 0), new Vector3(cameraDistance, cameraY, 0));
		float currentVisualAngle = levelManager.GetAngleFromOffset(0, pumaY, cameraDistance, adjustedCameraY);
		float adjustedCameraDistance = Mathf.Sin(currentVisualAngle*Mathf.PI/180) * idealVisualDistance;

		adjustedCameraY = pumaY + Mathf.Cos(currentVisualAngle*Mathf.PI/180) * idealVisualDistance;
		adjustedCameraX = pumaX - (Mathf.Sin(cameraRotY*Mathf.PI/180) * adjustedCameraDistance);
		adjustedCameraZ = pumaZ - (Mathf.Cos(cameraRotY*Mathf.PI/180) * adjustedCameraDistance);	

		float cameraRotXAdjustment = -1f * (levelManager.GetAngleFromOffset(0, pumaY, cameraDistance, levelManager.GetTerrainHeight(cameraX, cameraZ)) - 90f);
		cameraRotXAdjustment *= (cameraRotXAdjustment > 0) ? 0.65f : 0.8f;
		float adjustedCameraRotX = cameraRotX + cameraRotXAdjustment;

		levelManager.displayVar1 = cameraRotX;
		levelManager.displayVar2 = cameraRotXAdjustment;
		levelManager.displayVar3 = adjustedCameraRotX;
		
		// update camera obj
		Camera.main.transform.position = new Vector3(adjustedCameraX, adjustedCameraY, adjustedCameraZ);
		Camera.main.transform.rotation = Quaternion.Euler(adjustedCameraRotX, cameraRotY, cameraRotZ);
	}

}
