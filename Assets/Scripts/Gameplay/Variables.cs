using UnityEngine;
using System.Collections.Generic;

//FIX - swarm при выпуске ракет может повернуться и выбрать другого противка, даже если текущий жив

public class Variables : MonoBehaviour {
	
//	private static bool healthBarVisible;
	
	public static int Money;
	
	public static int Level = 1;
	public static int Lives;
	
	public static bool GameOver;
	
	public static List<GameObject> turrets = new List<GameObject>();
	
	public static List<GameObject> enemies = new List<GameObject>();
	
	public static List<GameObject> markers = new List<GameObject>();
	
//	public static bool HealthBarVisible
//	{
//		set 
//		{
//			
//		}
//		get { return healthBarVisible; }
//	}
	
	public static void Reload()
	{
		GameOver = false;
		Money = 2000;
		Lives = 10;
	}
}
