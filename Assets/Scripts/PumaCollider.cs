using UnityEngine;
using System.Collections;

/*
	This class checks whether the gameObject attached to it is colliding with something or not.
	Its current methods treats puma behavior for collisions with trees
*/
public class PumaCollider : MonoBehaviour {

	private GameObject scriptObject;
	private LevelManager levelManager;

	//Called at the beggining of the game
	void Start()
	{
		//Sets the script object to a local variable
		scriptObject = GameObject.Find("Script Object");
		//Sets the Level_Manager component to a local variable.
		levelManager = scriptObject.GetComponent<LevelManager>();
	}

	//Listen to collisions happening
	void OnCollisionStay (Collision col)
	{
		//TODO: Implement "Else if" alike funcionality
		//OR check public string variable "gamestate" and use switch case.
		//Checks if puma is stalking
		if(levelManager.IsStalkingState())
		{
			//Triggers stalkingCollision action
			levelManager.stalkingCollision(true);
		}
		//Checks if puma is chasing
		if(levelManager.IsChasingState())
		{
			//Triggers chasingCollision action
			levelManager.chasingCollision(true);
		}

	}

	//Listen to exit of collisions
	void OnCollisionExit (Collision col)
	{
		//Checks if puma is stalking
		if(levelManager.IsStalkingState())
		{
			//Triggers stalking out of collision action
			levelManager.stalkingCollision(false);
		}
		if(levelManager.IsChasingState())
		{
			//Triggers chasing out of collision action
			levelManager.chasingCollision(false);
		}
	}
}
