using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {
	
	private Rect startBox;
	private Rect settingsBox;
	private Rect exitBox;
	
	private void Awake()
	{
		startBox = new Rect(Screen.width / 2 - 60, 50, 120, 40);
		settingsBox = new Rect(Screen.width / 2 - 60, 100, 120, 40);
		exitBox = new Rect(Screen.width / 2 - 60, 150, 120, 40);
	}
	
	private void OnGUI()
	{
		if(GUI.Button(startBox, "START GAME"))
		{
			Application.LoadLevel("LevelMenu");
		}
		if(GUI.Button(settingsBox, "SETTINGS"))
		{
			Application.LoadLevel("Settings");
		}
		if(GUI.Button(exitBox, "EXIT"))
		{
			
		}
	}
}
