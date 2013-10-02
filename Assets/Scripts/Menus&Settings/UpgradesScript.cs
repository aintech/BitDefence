using UnityEngine;
using System.Collections;

public class UpgradesScript : MonoBehaviour {
	
	private Rect backBox;
	
	private void Awake()
	{
		backBox = new Rect(Screen.width / 2 - 60, 150, 200, 40);
		backBox.x = 100;
	}
	
	private void OnGUI()
	{
		if(GUI.Button(backBox, "BACK TO LEVEL MENU"))
		{
			Application.LoadLevel("LevelMenu");
		}
	}
}
