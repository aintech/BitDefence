using UnityEngine;
using System.Collections.Generic;

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
	
	public static int EnemiesLeft { get; private set; }
	
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
	
	public static float TurretHeight(TurretScript.TurretType type)
	{
		switch(type)
		{
			case TurretScript.TurretType.AntiMaterial: 	return 2; break;
			case TurretScript.TurretType.Freeze: 		return 2; break;
			case TurretScript.TurretType.Gun: 			return 2; break;
			case TurretScript.TurretType.Hammer: 		return 2; break;
			case TurretScript.TurretType.Miner: 		return 2; break;
			case TurretScript.TurretType.PlasmaCutter: 	return 2; break;
			case TurretScript.TurretType.Swarm: 		return 2; break;
			case TurretScript.TurretType.Tesla: 		return 2; break;
			default:									return 2;
		}
	}
	
	public static void removeEnemy(GameObject enemy)
	{
		enemies.Remove(enemy);
		EnemiesLeft--;
	}
	
	public static void SetEnemiesInWave(int value)
	{
		EnemiesLeft = value;
	}
}
