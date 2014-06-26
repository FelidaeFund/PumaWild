using UnityEngine;
using System.Collections;

/// CameraController
/// Manages camera position relative to puma

public class CameraController : MonoBehaviour
{
	//===================================
	//===================================
	//		MODULE VARIABLES
	//===================================
	//===================================
	
	// current camera position (controllable params)
	private float currentCameraY;
	private float currentCameraRotX;
	private float currentCameraDistance;
	private float currentCameraRotOffsetY;
	
	// previous position
	private float previousCameraY;
	private float previousCameraRotX;
	private float previousCameraDistance;
	private float previousCameraRotOffsetY;
	
	// target position
	private float targetCameraY;
	private float targetCameraRotX;
	private float targetCameraDistance;
	private float targetCameraRotOffsetY;
	
	// transition processing
	private float transStartTime;
	private float transFadeTime;
	private string transMainCurve;
	private string transRotXCurve;
	
	// external module
	private LevelManager levelManager;

	//===================================
	//===================================
	//		INITIALIZATION
	//===================================
	//===================================

    void Start()
    {
		// connect to external modules
		levelManager = GetComponent<LevelManager>();

		currentCameraY = 0f;
		currentCameraRotX = 0f;
		currentCameraDistance = 0f;
		currentCameraRotOffsetY = 0f;
	}
	
	//===================================
	//===================================
	//	  PUBLIC FUNCTIONS
	//===================================
	//===================================
	
	//-----------------------
	// SelectRelativePosition
	//
	// sets target for trans
	// to relative position 
	//-----------------------

	public void SelectTargetPosition(string targetPositionLabel, float targetRotOffsetY, float fadeTime, string mainCurve, string rotXCurve)
	{
		// remember previous position
		previousCameraY = currentCameraY;
		previousCameraRotX = currentCameraRotX;
		previousCameraDistance = currentCameraDistance;
		previousCameraRotOffsetY = currentCameraRotOffsetY;
		
		// select target position
		switch (targetPositionLabel) {
		
		case "cameraPosHigh":
			targetCameraY = 5.7f;
			targetCameraRotX = 12.8f;
			targetCameraDistance = 8.6f;
			break;

		case "cameraPosMedium":
			targetCameraY = 4f;
			targetCameraRotX = 4f;
			targetCameraDistance = 7.5f;
			break;

		case "cameraPosLow":
			targetCameraY = 3f;
			targetCameraRotX = -2f;
			targetCameraDistance = 7f;
			break;

		case "cameraPosCloseup":
			targetCameraY = 2.75f;
			targetCameraRotX = 2.75f;
			targetCameraDistance = 6.5f;
			break;

		case "cameraPosEating":
			targetCameraY = 9f;
			targetCameraRotX = 30f;
			targetCameraDistance = 9f;
			break;

		case "cameraPosGui":
			targetCameraY = 90f;
			targetCameraRotX = 33f;
			targetCameraDistance = 48f;
			break;
		}

		if (targetRotOffsetY != 1000000f)
			targetCameraRotOffsetY = targetRotOffsetY;

		// constrain previousCameraRotOffsetY to within 180 degrees of targetCameraRotOffsetY
		// so that camera always swings around the shortest path
		if (previousCameraRotOffsetY > targetCameraRotOffsetY + 180f) 
			previousCameraRotOffsetY -= 360f;
		if (previousCameraRotOffsetY < targetCameraRotOffsetY - 180f)
			previousCameraRotOffsetY += 360f;

		transStartTime = Time.time;
		transFadeTime = fadeTime;
		transMainCurve = mainCurve;
		transRotXCurve = rotXCurve;
	}

	//-----------------------
	// UpdateCameraPosition
	//
	// sets the actual camera
	// position in 3D world
	// once per frame
	//-----------------------

	public void UpdateCameraPosition(float pumaX, float pumaY, float pumaZ, float mainHeading)
	{
		float fadePercentComplete;
		float cameraRotXPercentDone;
		float backwardsTime = (transStartTime + transFadeTime) - (Time.time - transStartTime);	
		
		if (Time.time >= transStartTime + transFadeTime) {
			// if trans has expired use target values
			currentCameraY = targetCameraY;
			currentCameraRotX = targetCameraRotX;
			currentCameraDistance = targetCameraDistance;
			currentCameraRotOffsetY = targetCameraRotOffsetY;
		}
		
		else {
			// otherwise calculate current position based on transition
	
			switch (transMainCurve) {
			
			case "mainCurveLinear":
				fadePercentComplete = (Time.time - transStartTime) / transFadeTime;
				currentCameraY = previousCameraY + fadePercentComplete * (targetCameraY - previousCameraY);
				currentCameraDistance = previousCameraDistance + fadePercentComplete * (targetCameraDistance - previousCameraDistance);
				currentCameraRotOffsetY = previousCameraRotOffsetY + fadePercentComplete * (targetCameraRotOffsetY - previousCameraRotOffsetY);
				break;
			
			case "mainCurveSForward":
				// combines two logarithmic curves to create an S-curve
				if (Time.time < transStartTime + (transFadeTime * 0.5f)) {
					// 1st half
					fadePercentComplete = (Time.time - transStartTime) / (transFadeTime * 0.5f);
					fadePercentComplete = fadePercentComplete * fadePercentComplete;  // apply bulge
					currentCameraY = previousCameraY + fadePercentComplete * ((targetCameraY - previousCameraY) * 0.5f);
					currentCameraDistance = previousCameraDistance + fadePercentComplete * ((targetCameraDistance - previousCameraDistance) * 0.5f);
					currentCameraRotOffsetY = previousCameraRotOffsetY + fadePercentComplete * ((targetCameraRotOffsetY - previousCameraRotOffsetY) * 0.5f);
				}
				else {
					// 2nd half
					fadePercentComplete = ((Time.time - transStartTime) - (transFadeTime * 0.5f)) / (transFadeTime * 0.5f);				
					fadePercentComplete = fadePercentComplete + (fadePercentComplete - (fadePercentComplete * fadePercentComplete));  // apply bulge in opposite direction
					currentCameraY = previousCameraY + ((targetCameraY - previousCameraY) * 0.5f) + fadePercentComplete * ((targetCameraY - previousCameraY) * 0.5f);
					currentCameraDistance = previousCameraDistance + ((targetCameraDistance - previousCameraDistance) * 0.5f) + fadePercentComplete * ((targetCameraDistance - previousCameraDistance) * 0.5f);
					currentCameraRotOffsetY = previousCameraRotOffsetY + ((targetCameraRotOffsetY - previousCameraRotOffsetY) * 0.5f) + fadePercentComplete * ((targetCameraRotOffsetY - previousCameraRotOffsetY) * 0.5f);
				}
				break;
			
			case "mainCurveSBackward":
				// same as mainCurveSCurveForward except it runs backwards in time (reversing 'target' and 'previous') to get a different feel
				if (backwardsTime < transStartTime + (transFadeTime * 0.5f)) {
					// 1st half
					fadePercentComplete = (backwardsTime - transStartTime) / (transFadeTime * 0.5f);
					fadePercentComplete = fadePercentComplete * fadePercentComplete;  // apply bulge
					currentCameraY = targetCameraY + fadePercentComplete * ((previousCameraY - targetCameraY) * 0.5f);
					currentCameraDistance = targetCameraDistance + fadePercentComplete * ((previousCameraDistance - targetCameraDistance) * 0.5f);
					currentCameraRotOffsetY = targetCameraRotOffsetY + fadePercentComplete * ((previousCameraRotOffsetY - targetCameraRotOffsetY) * 0.5f);
				}
				else {
					// 2nd half
					fadePercentComplete = ((backwardsTime - transStartTime) - (transFadeTime * 0.5f)) / (transFadeTime * 0.5f);				
					fadePercentComplete = fadePercentComplete + (fadePercentComplete - (fadePercentComplete * fadePercentComplete)); // apply bulge in opposite direction
					currentCameraY = targetCameraY + ((previousCameraY - targetCameraY) * 0.5f) + fadePercentComplete * ((previousCameraY - targetCameraY) * 0.5f);
					currentCameraDistance = targetCameraDistance + ((previousCameraDistance - targetCameraDistance) * 0.5f) + fadePercentComplete * ((previousCameraDistance - targetCameraDistance) * 0.5f);
					currentCameraRotOffsetY = targetCameraRotOffsetY + ((previousCameraRotOffsetY - targetCameraRotOffsetY) * 0.5f) + fadePercentComplete * ((previousCameraRotOffsetY - targetCameraRotOffsetY) * 0.5f);
				}		
				break;
			
			default:
				Debug.Log("ERROR - CameraController.UpdateActualPosition() got bad main curve: " + transMainCurve);
				break;
			}

			switch (transRotXCurve) {
			
			case "curveRotXLinear":
				cameraRotXPercentDone = (Time.time - transStartTime) / transFadeTime;
				currentCameraRotX = previousCameraRotX + cameraRotXPercentDone * (targetCameraRotX - previousCameraRotX);
				break;
			
			case "curveRotXLogarithmic":
				cameraRotXPercentDone = (Time.time - transStartTime) / transFadeTime;
				cameraRotXPercentDone = cameraRotXPercentDone * cameraRotXPercentDone; // apply bulge
				currentCameraRotX = previousCameraRotX + cameraRotXPercentDone * (targetCameraRotX - previousCameraRotX);
				break;
			
			case "curveRotXLinearSecondHalf":
				if (Time.time < transStartTime + (transFadeTime * 0.5f)) {
					// 1st half
					currentCameraRotX = previousCameraRotX; // no change
				}
				else {
					// 2nd half
					cameraRotXPercentDone = ((Time.time - transStartTime) - (transFadeTime * 0.5f)) / (transFadeTime * 0.5f);
					currentCameraRotX = previousCameraRotX + cameraRotXPercentDone * (targetCameraRotX - previousCameraRotX);
				}
				break;
			
			case "curveRotXLogarithmicSecondHalf":
				if (Time.time < transStartTime + (transFadeTime * 0.5f)) {
					// 1st half
					currentCameraRotX = previousCameraRotX; // no change
				}
				else {
					// 2nd half
					cameraRotXPercentDone = ((Time.time - transStartTime) - (transFadeTime * 0.5f)) / (transFadeTime * 0.5f);
					cameraRotXPercentDone = cameraRotXPercentDone * cameraRotXPercentDone; // apply bulge
					currentCameraRotX = previousCameraRotX + cameraRotXPercentDone * (targetCameraRotX - previousCameraRotX);
				}
				break;
			
			case "curveRotXLinearBackwardsSecondHalf":
				// same as curveRotXLinearSecondHalf except it runs backwards in time (reversing 'target' and 'previous') to get a different feel
				if (backwardsTime < transStartTime + (transFadeTime * 0.5f)) {
					// 1st half
					currentCameraRotX = targetCameraRotX; // no change
				}
				else {
					// 2nd half
					cameraRotXPercentDone = ((backwardsTime - transStartTime) - (transFadeTime * 0.5f)) / (transFadeTime * 0.5f);
					currentCameraRotX = targetCameraRotX + cameraRotXPercentDone * (previousCameraRotX - targetCameraRotX);
				}		
				break;

			case "curveRotXLogarithmicBackwardsSecondHalf":
				// same as curveRotXLogarithmicSecondHalf except it runs backwards in time (reversing 'target' and 'previous') to get a different feel
				if (backwardsTime < transStartTime + (transFadeTime * 0.5f)) {
					// 1st half
					currentCameraRotX = targetCameraRotX; // no change
				}
				else {
					// 2nd half
					cameraRotXPercentDone = ((backwardsTime - transStartTime) - (transFadeTime * 0.5f)) / (transFadeTime * 0.5f);
					cameraRotXPercentDone = cameraRotXPercentDone * cameraRotXPercentDone; // apply bulge
					currentCameraRotX = targetCameraRotX + cameraRotXPercentDone * (previousCameraRotX - targetCameraRotX);
				}		
				break;

			default:
				Debug.Log("ERROR - CameraController.UpdateActualPosition() got bad rotX curve: " + transRotXCurve);
				break;
			}
		}
			
		//-----------------------------------------------
		// set actual position
		//-----------------------------------------------

		float cameraRotX = currentCameraRotX;
		float cameraRotY = mainHeading + currentCameraRotOffsetY;
		float cameraRotZ = 0f;
		
		float cameraX = pumaX - (Mathf.Sin(cameraRotY*Mathf.PI/180) * currentCameraDistance);
		float cameraY = currentCameraY;
		float cameraZ = pumaZ - (Mathf.Cos(cameraRotY*Mathf.PI/180) * currentCameraDistance);	
	
		//-----------------------------------------------
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

		float idealVisualDistance = Vector3.Distance(new Vector3(0, 0, 0), new Vector3(currentCameraDistance, cameraY, 0));
		float currentVisualAngle = levelManager.GetAngleFromOffset(0, pumaY, currentCameraDistance, adjustedCameraY);
		float adjustedCameraDistance = Mathf.Sin(currentVisualAngle*Mathf.PI/180) * idealVisualDistance;

		adjustedCameraY = pumaY + Mathf.Cos(currentVisualAngle*Mathf.PI/180) * idealVisualDistance;
		adjustedCameraX = pumaX - (Mathf.Sin(cameraRotY*Mathf.PI/180) * adjustedCameraDistance);
		adjustedCameraZ = pumaZ - (Mathf.Cos(cameraRotY*Mathf.PI/180) * adjustedCameraDistance);	

		float cameraRotXAdjustment = -1f * (levelManager.GetAngleFromOffset(0, pumaY, currentCameraDistance, levelManager.GetTerrainHeight(cameraX, cameraZ)) - 90f);
		cameraRotXAdjustment *= (cameraRotXAdjustment > 0) ? 0.65f : 0.8f;
		float adjustedCameraRotX = cameraRotX + cameraRotXAdjustment;

		//-----------------------------------------------
		// write out values to camera object
		//-----------------------------------------------

		Camera.main.transform.position = new Vector3(adjustedCameraX, adjustedCameraY, adjustedCameraZ);
		Camera.main.transform.rotation = Quaternion.Euler(adjustedCameraRotX, cameraRotY, cameraRotZ);
	}
	
	//-----------------------
	// GetCurrentRotOffsetY
	//
	// returns current val
	//-----------------------

	public float GetCurrentRotOffsetY()
	{
		return currentCameraRotOffsetY;
	}

}


