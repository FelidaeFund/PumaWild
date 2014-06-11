using UnityEngine;
using System.Collections;

/// ScoringSystem
/// Keeps track of game progress and current score

public class ScoringSystem : MonoBehaviour
{
	//===================================
	//===================================
	//		MODULE VARIABLES
	//===================================
	//===================================

	// meat consumption
	private float meatLimitForLevel;
	private float meatTotalEaten;

	// health points
	private float maxHealth;
	private float[] healthPoints;
	
	// energy usage
	private float expensePerMeterChasing;
	private float expensePerMeterStalking;

	// amounts for last kill
	private float lastKillMeatEaten;
	private float lastKillCaloriesEaten;
	private string lastKillDeerType;
	private float[] lastKillExpenses;

	// kill scoreboard
	private int[] bucksKilled;
	private int[] doesKilled;
	private int[] fawnsKilled;

	// energy scoreboard
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
		// meat consumption
		meatLimitForLevel = 1000f;
		meatTotalEaten = 0f;

		// health points
		maxHealth = 175000f;
		healthPoints = new float[] { maxHealth * 0.5f, maxHealth * 0.5f, maxHealth * 0.5f,
									 maxHealth * 0.5f, maxHealth * 0.5f, maxHealth * 0.5f};
		// energy usage
		expensePerMeterChasing = 75f;
		expensePerMeterStalking = expensePerMeterChasing * 0.25f;

		// amounts for last kill
		lastKillMeatEaten = 0f;
		lastKillCaloriesEaten = 0f;
		lastKillDeerType = "None";
		lastKillExpenses = new float[] {0f, 0f, 0f, 0f, 0f, 0f};

		// kill scoreboard
		bucksKilled = new int[] {0, 0, 0, 0, 0, 0};
		doesKilled = new int[] {0, 0, 0, 0, 0, 0};
		fawnsKilled = new int[] {0, 0, 0, 0, 0, 0};
		//bucksKilled = new int[] {2, 5, 0, 0, 1, 0};
		//doesKilled = new int[] {3, 0, 2, 2, 0, 1};
		//fawnsKilled = new int[] {1, 0, 3, 0, 5, 2};

		// energy scoreboard - totals for energy spent and calories eaten; NOTE: index 6 is total for all
		buckExpenses = new float[] {0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f};
		buckCalories = new float[] {0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f};
		doeExpenses = new float[] {0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f};
		doeCalories = new float[] {0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f};
		fawnExpenses = new float[] {0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f};
		fawnCalories = new float[] {0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f};
		//buckExpenses = new float[] {5f, 4f, 6f, 4f, 6f, 3f, 5f};
		//buckCalories = new float[] {4f, 5f, 3f, 6f, 4f, 5f, 3f};
		//doeExpenses = new float[] {4f, 6f, 3f, 4f, 6f, 4f, 5f};
		//doeCalories = new float[] {4f, 5f, 4f, 6f, 4f, 5f, 3f};
		//fawnExpenses = new float[] {5f, 4f, 6f, 3f, 5f, 4f, 5f};
		//fawnCalories = new float[] {4f, 5f, 3f, 5f, 4f, 6f, 3f};
	}
	
	//===========================================
	//===========================================
	//	SCORE UPDATES
	//
	// 	This set of functions is called by the 
	// 	level manager when game events happen
	//	that result in changes in game score 
	//===========================================
	//===========================================

	//------------------------------------------
	// PumaHasWalked(), PumaHasRun()
	// 
	// Called for every movement of the puma
	// when under player control; these are the 
	// main source of puma energy 'expenses'
	//------------------------------------------

	public void PumaHasWalked(int selectedPuma, float distance)
	{
		if (selectedPuma == -1)
			return;
		if (distance < 0)
			distance *= -1f;
			
		float newExpense = distance * expensePerMeterStalking;
		lastKillExpenses[selectedPuma] += newExpense;
		healthPoints[selectedPuma] -= newExpense;
		if (healthPoints[selectedPuma] < 0)
			healthPoints[selectedPuma] = 0;
	}
	
	public void PumaHasRun(int selectedPuma, float distance)
	{
		if (selectedPuma == -1)
			return;

		if (distance < 0)
			distance *= -1f;
			
		float newExpense = distance * expensePerMeterChasing;
		lastKillExpenses[selectedPuma] += newExpense;
		healthPoints[selectedPuma] -= newExpense;
		if (healthPoints[selectedPuma] < 0)
			healthPoints[selectedPuma] = 0;
	}
	
	//--------------------------------------------
	// DeerCaught()
	// 
	// Called whenever a puma catches its prey;
	// this is the source of calories for 'health'
	//--------------------------------------------

	public void DeerCaught(int selectedPuma, string deerType)
	{
		float meatEaten;
		float caloriesEaten;
	
		switch (deerType) {
		case "Buck":
			meatEaten = Random.Range(35f, 43f);
			caloriesEaten = meatEaten * Random.Range(900f, 1100f);
			bucksKilled[selectedPuma] += 1;
			buckExpenses[selectedPuma] += lastKillExpenses[selectedPuma];
			buckCalories[selectedPuma] += caloriesEaten;
			buckExpenses[6] += lastKillExpenses[selectedPuma];  // population total
			buckCalories[6] += caloriesEaten;  					// population total
			break;
		
		case "Doe":
			meatEaten = Random.Range(25f, 32f);
			caloriesEaten = meatEaten * Random.Range(900f, 1100f);
			doesKilled[selectedPuma] += 1;
			doeExpenses[selectedPuma] += lastKillExpenses[selectedPuma];
			doeCalories[selectedPuma] += caloriesEaten;
			doeExpenses[6] += lastKillExpenses[selectedPuma];   // population total
			doeCalories[6] += caloriesEaten;  					// population total
			break;
		
		case "Fawn":
			meatEaten = Random.Range(17f, 23f);
			caloriesEaten = meatEaten * Random.Range(900f, 1100f);
			fawnsKilled[selectedPuma] += 1;
			fawnExpenses[selectedPuma] += lastKillExpenses[selectedPuma];
			fawnCalories[selectedPuma] += caloriesEaten;
			fawnExpenses[6] += lastKillExpenses[selectedPuma];  // population total
			fawnCalories[6] += caloriesEaten;  					// population total
			break;
			
		default:
			System.Console.WriteLine("ScoringSystem.DeerCaught got bad deerType: " + deerType);
			return;
		}

		lastKillMeatEaten = meatEaten;
		lastKillCaloriesEaten = caloriesEaten;
		lastKillDeerType = deerType;
		
		meatTotalEaten += meatEaten;
		healthPoints[selectedPuma] += caloriesEaten;
		if (healthPoints[selectedPuma] > maxHealth)
			healthPoints[selectedPuma] = maxHealth;
	}
	
	//------------------------------------------
	// ClearLastKillInfo()
	// 
	// Called when leaving the Feeding Display,
	// which is the point where the accumulated
	// expenditures and other data are cleared
	//------------------------------------------

	public void ClearLastKillInfo(int selectedPuma)
	{
		lastKillMeatEaten = 0f;
		lastKillCaloriesEaten = 0f;
		lastKillDeerType = "None";
		lastKillExpenses[selectedPuma] = 0f;
	}

	//===========================================
	//===========================================
	//	GET THE SCORE
	//
	// 	This set of functions is called by the 
	// 	GUI to display the score for the game
	//===========================================
	//===========================================

	public float GetMeatLevel()
	{
		return meatTotalEaten / meatLimitForLevel;
	}
		
	public float GetPumaHealth(int pumaNum)
	{
		if (pumaNum == -1)
			return -1f;

		return healthPoints[pumaNum] / maxHealth;
	}

	public float GetLastKillMeatEaten()
	{
		return lastKillMeatEaten;
	}
		
	public float GetLastKillCaloriesEaten()
	{
		return lastKillCaloriesEaten;
	}
	
	public string GetLastKillDeerType()
	{
		return lastKillDeerType;
	}
	
	public float GetLastKillExpense(int selectedPuma)
	{
		return lastKillExpenses[selectedPuma];
	}
	
	public int GetBucksKilled(int selectedPuma)
	{
		return bucksKilled[selectedPuma];
	}

	public int GetDoesKilled(int selectedPuma)
	{
		return doesKilled[selectedPuma];
	}

	public int GetFawnsKilled(int selectedPuma)
	{
		return fawnsKilled[selectedPuma];
	}

	public float GetBuckExpenses(int pumaNum)
	{
		return buckExpenses[pumaNum];
	}
	
	public float GetDoeExpenses(int pumaNum)
	{
		return doeExpenses[pumaNum];
	}
	
	public float GetFawnExpenses(int pumaNum)
	{
		return fawnExpenses[pumaNum];
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
	

}

















