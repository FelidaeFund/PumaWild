using UnityEngine;
using System.Collections;

/// Keeps track of game progress and current score
/// 

public class ScoringSystem : MonoBehaviour
{
	//===================================
	//===================================
	//		MODULE VARIABLES
	//===================================
	//===================================

	// global amounts
	private float meatLimitForLevel;
	private float meatTotalEaten;

	// health points
	private float maxHealth;
	private float[] healthPoints;
	
	// energy usage
	private float expensePerMeterChasing = 75f;
	private float expensePerMeterStalking = 75f * 0.25f;

	// amounts for last kill
	private float lastKillMeatEaten;
	private float lastKilCaloriesEaten;
	private int lastKillDeerType;
	private float[] expensesSinceLastKill;

	// kill scoreboard
	private int[] bucksKilled;
	private int[] doesKilled;
	private int[] fawnsKilled;

	// energy scoreboard - for each prey type, totals for energy spent and calories eaten
	private float[] buckExpenses;
	private float[] buckCalories;
	private float[] doeExpenses;
	private float[] doeCalories;
	private float[] fawnExpenses;
	private float[] fawnCalories;

	//===================================
	//===================================
	//		INITIALIZATION
	//===================================
	//===================================

    void Start()
    {
		InitScoringSystem();
	}

    private void InitScoringSystem()
    {
		// global amounts
		meatLimitForLevel = 1000f;
		meatTotalEaten = 0f;

		// health points
		maxHealth = 175000f;
		healthPoints = new float[] {
			maxHealth * 0.5f,
			maxHealth * 0.5f,
			maxHealth * 0.5f,
			maxHealth * 0.5f,
			maxHealth * 0.5f,
			maxHealth * 0.5f
		};
		
		// energy usage
		expensePerMeterChasing = 75f;
		expensePerMeterStalking = expensePerMeterChasing * 0.25f;

		// amounts for last kill
		lastKillMeatEaten = 0f;
		lastKilCaloriesEaten = 0f;
		lastKillDeerType = -1;
		expensesSinceLastKill = new float[] {0f, 0f, 0f, 0f, 0f, 0f};

		// kill scoreboard
		bucksKilled = new int[] {0, 0, 0, 0, 0, 0};
		doesKilled = new int[] {0, 0, 0, 0, 0, 0};
		fawnsKilled = new int[] {0, 0, 0, 0, 0, 0};
		//bucksKilled = new int[] {2, 5, 0, 0, 1, 0};
		//doesKilled = new int[] {3, 0, 2, 2, 0, 1};
		//fawnsKilled = new int[] {1, 0, 3, 0, 5, 2};

		// energy scoreboard - for each prey type, totals for energy spent and calories eaten
		buckExpenses = new float[] {0f, 0f, 0f, 0f, 0f, 0f, 0f};
		buckCalories = new float[] {0f, 0f, 0f, 0f, 0f, 0f, 0f};
		doeExpenses = new float[] {0f, 0f, 0f, 0f, 0f, 0f, 0f};
		doeCalories = new float[] {0f, 0f, 0f, 0f, 0f, 0f, 0f};
		fawnExpenses = new float[] {0f, 0f, 0f, 0f, 0f, 0f, 0f};
		fawnCalories = new float[] {0f, 0f, 0f, 0f, 0f, 0f, 0f};
		//buckExpenses = new float[] {5f, 4f, 6f, 4f, 6f, 3f};
		//buckCalories = new float[] {4f, 5f, 3f, 6f, 4f, 5f};
		//doeExpenses = new float[] {4f, 6f, 3f, 4f, 6f, 4f};
		//doeCalories = new float[] {4f, 5f, 4f, 6f, 4f, 5f};
		//fawnExpenses = new float[] {5f, 4f, 6f, 3f, 5f, 4f};
		//fawnCalories = new float[] {4f, 5f, 3f, 5f, 4f, 6f};
	}
	
	
	// AbsorbExpenditures()
	// 
	// Called when leaving the Feeding Display,
	// which is the point where the accumulated
	// expenditures are absorbed into the history

	public void AbsorbExpenses(int selectedPuma)
	{

	
	}

}

















