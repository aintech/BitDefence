using UnityEngine;
using System.Collections.Generic;

//FIX - swarm при выпуске ракет может повернуться и выбрать другого противка, даже если текущий жив

//TODO - forwardPointer в модельку Hammer
//TODO - настроить радиус турелей и проверить rangeSphere - чтобы отображала правильно радиус

public class Variables : MonoBehaviour {
	
//	private static bool healthBarVisible;
	
	public static int Money;
	
	public static int Level = 1;
	public static int Lives;
	
	public static bool GameOver;
	
	public static List<GameObject> turrets = new List<GameObject>();
	
	public static List<GameObject> enemies = new List<GameObject>();
	
	public static List<GameObject> markers = new List<GameObject>();
	
	//for elements that need to be remove - like actionmenu if created another menu
	private static List<GameObject> elements = new List<GameObject>();
	
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
	
	public static void PutInElements(GameObject obj)
	{
		elements.Add(obj);
	}
	
	public static void RemoveAllFromElements()
	{
		foreach(GameObject element in elements)
		{
			Destroy(element);
		}
		elements.RemoveRange(0, elements.Count);
	}
}
