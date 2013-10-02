using UnityEngine;
using System.Collections;

public class LevelMenuScript : MonoBehaviour {
	
	private Rect chooseTextBox;
	private Rect level1Box;
	private Rect upgradesBox;
	private Rect backBox;
	
	private void Awake()
	{
		chooseTextBox = new Rect(Screen.width / 2 - 60, 10, 120, 40);
		level1Box = new Rect(30, 30, 40, 40);
		upgradesBox = new Rect(Screen.width / 2 - 60, 100, 120, 40);
		backBox = new Rect(Screen.width / 2 - 60, 150, 120, 40);
	}
	
	private void OnGUI()
	{
		GUI.Label(chooseTextBox, "Choose Level");
		if(GUI.Button(level1Box, "1"))
		{
			Variables.Reload();
			Variables.Level = 1;
			Application.LoadLevel("Gameplay");
		}
		if(GUI.Button(upgradesBox, "UPGRADES"))
		{
			Application.LoadLevel("Upgrades");
		}
		if(GUI.Button(backBox, "BACK TO MENU"))
		{
			Application.LoadLevel("MainMenu");
		}
	}
}
