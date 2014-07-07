using UnityEngine;
using System.Collections;

public class SwitchLevels : MonoBehaviour {

	public GameObject terrain1A;
	public GameObject terrain1B;
	public GameObject terrain1C;
	public GameObject terrain1D;

	public GameObject terrain2A;
	public GameObject terrain2B;
	public GameObject terrain2C;
	public GameObject terrain2D;

	public GameObject terrain3A;
	public GameObject terrain3B;
	public GameObject terrain3C;
	public GameObject terrain3D;

	public GameObject terrain4A;
	public GameObject terrain4B;
	public GameObject terrain4C;
	public GameObject terrain4D;

	public GameObject terrain5A;
	public GameObject terrain5B;
	public GameObject terrain5C;
	public GameObject terrain5D;
	
	public GameObject terrain2A_paint;
	public GameObject terrain2B_paint;
	public GameObject terrain2C_paint;
	public GameObject terrain2D_paint;

	public GameObject terrain3A_paint;
	public GameObject terrain3B_paint;
	public GameObject terrain3C_paint;
	public GameObject terrain3D_paint;

	public GameObject terrain4A_paint;
	public GameObject terrain4B_paint;
	public GameObject terrain4C_paint;
	public GameObject terrain4D_paint;

	public GameObject terrain5A_paint;
	public GameObject terrain5B_paint;
	public GameObject terrain5C_paint;
	public GameObject terrain5D_paint;


	public void switchLevel(int levelNum, bool paintEnabled)
	{
		terrain1A.SetActive((levelNum == 1) ? true : false);
		terrain1B.SetActive((levelNum == 1) ? true : false);
		terrain1C.SetActive((levelNum == 1) ? true : false);
		terrain1D.SetActive((levelNum == 1) ? true : false);

		terrain2A.SetActive((levelNum == 2) ? true : false);
		terrain2B.SetActive((levelNum == 2) ? true : false);
		terrain2C.SetActive((levelNum == 2) ? true : false);
		terrain2D.SetActive((levelNum == 2) ? true : false);

		terrain3A.SetActive((levelNum == 3) ? true : false);
		terrain3B.SetActive((levelNum == 3) ? true : false);
		terrain3C.SetActive((levelNum == 3) ? true : false);
		terrain3D.SetActive((levelNum == 3) ? true : false);

		terrain4A.SetActive((levelNum == 4) ? true : false);
		terrain4B.SetActive((levelNum == 4) ? true : false);
		terrain4C.SetActive((levelNum == 4) ? true : false);
		terrain4D.SetActive((levelNum == 4) ? true : false);

		terrain5A.SetActive((levelNum == 5) ? true : false);
		terrain5B.SetActive((levelNum == 5) ? true : false);
		terrain5C.SetActive((levelNum == 5) ? true : false);
		terrain5D.SetActive((levelNum == 5) ? true : false);
		
		terrain2A_paint.SetActive((levelNum == 2 && paintEnabled == true) ? true : false);
		terrain2B_paint.SetActive((levelNum == 2 && paintEnabled == true) ? true : false);
		terrain2C_paint.SetActive((levelNum == 2 && paintEnabled == true) ? true : false);
		terrain2D_paint.SetActive((levelNum == 2 && paintEnabled == true) ? true : false);

		terrain3A_paint.SetActive((levelNum == 3 && paintEnabled == true) ? true : false);
		terrain3B_paint.SetActive((levelNum == 3 && paintEnabled == true) ? true : false);
		terrain3C_paint.SetActive((levelNum == 3 && paintEnabled == true) ? true : false);
		terrain3D_paint.SetActive((levelNum == 3 && paintEnabled == true) ? true : false);

		terrain4A_paint.SetActive((levelNum == 4 && paintEnabled == true) ? true : false);
		terrain4B_paint.SetActive((levelNum == 4 && paintEnabled == true) ? true : false);
		terrain4C_paint.SetActive((levelNum == 4 && paintEnabled == true) ? true : false);
		terrain4D_paint.SetActive((levelNum == 4 && paintEnabled == true) ? true : false);

		terrain5A_paint.SetActive((levelNum == 5 && paintEnabled == true) ? true : false);
		terrain5B_paint.SetActive((levelNum == 5 && paintEnabled == true) ? true : false);
		terrain5C_paint.SetActive((levelNum == 5 && paintEnabled == true) ? true : false);
		terrain5D_paint.SetActive((levelNum == 5 && paintEnabled == true) ? true : false);

	}
}
