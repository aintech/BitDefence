using UnityEngine;
using System.Collections;

public class SettingsScript : MonoBehaviour {
	
	private Rect backBox;
	
	private void Awake()
	{
		backBox = new Rect(Screen.width / 2 - 60, 150, 120, 40);
	}
	
	private void OnGUI()
	{
		if(GUI.Button(backBox, "BACK TO MENU"))
		{
			Application.LoadLevel("MainMenu");
		}
	}
}
