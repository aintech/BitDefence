using UnityEngine;
using System.Collections.Generic;

public class LevelData : MonoBehaviour {
	
	// --- enemy types ---
	//0 - Speeder
	public static List<List<int>> getEnemiesMap(int level)
	{
		List<List<int>> waves = new List<List<int>>();
		
		switch(level)
		{
		case 1:
			waves.Add(new List<int> { 0});
			waves.Add(new List<int> { 0, 0, 0, 0, 0});
			waves.Add(new List<int> { 0, 0, 0, 0, 0, 0, 0});
			waves.Add(new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0});
			waves.Add(new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0});
			waves.Add(new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0});
			break;
		}
		
		return waves;
	}
}
