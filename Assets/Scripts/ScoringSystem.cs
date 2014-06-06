using UnityEngine;
using System.Collections;

/// Keeps track of game progress and current score
/// 

public class ScoringSystem : MonoBehaviour
{
	// global amounts
	private float meatMaxLevel;
	private float meatTotalEaten;

	// amounts for last kill
	private float	meatJustEaten;
	private float	caloriesJustEaten;
	private int		deerTypeJustCaught;
	private float[] expendituresSinceLastTally = new float[] {0f, 0f, 0f, 0f, 0f, 0f};

	// health points
	private float 	pumaMaxHealth = 175000f;
	private float[] pumaHealthPoints = new float[] {175000f*0.5f, 175000f*0.5f, 1f  *  175000f*0.5f, 1f    *     175000f*0.5f, 175000f*0.5f, 175000f*0.5f};
	
	// kill scoreboard
	private int[] bucksKilled = new int[] {2, 5, 0, 0, 1, 0};
	private int[] doesKilled = new int[] {3, 0, 2, 2, 0, 1};
	private int[] fawnsKilled = new int[] {1, 0, 3, 0, 5, 2};
	//private int[] bucksKilled = new int[] {0, 0, 0, 0, 0, 0};
	//private int[] doesKilled = new int[] {0, 0, 0, 0, 0, 0};
	//private int[] fawnsKilled = new int[] {0, 0, 0, 0, 0, 0};

	// energy scoreboard - for each prey type, totals for energy spent and calories eaten by pumas 0-5; TOTAL for all pumas uses index 6
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

	// energy usage
	private float expenditurePerMeterChasing = 75f;
	private float expenditurePerMeterStalking = 75f * 0.25f;
	
    void Start()
    {
		InitScoringSystem();
	}

    private void InitScoringSystem()
    {



	
	
	
	}
	
	
	// TallyExpenditures()
	// 
	// called when leaving the Feeding Display,
	// which is the point where the accumulated
	// expenditures are absorbed into the history

	public void TallyExpenditures(int selectedPuma)
	{

	
	}

}

















