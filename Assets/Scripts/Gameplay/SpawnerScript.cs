using UnityEngine;
using System.Collections.Generic;

public class SpawnerScript : MonoBehaviour {
	
	public GameObject speederPrefab;
	public GameObject road;
	
	private float enemyDelay = 1f;
	private float enemyDelayCounter = 0;
	private int enemyInWaveCounter = 0;
	
	private List<List<int>> waves;
	
	public int CurrentWave { get; private set; }
	public float WaveDelayCounter { get; private set; }
	public float WaveDelay { get; private set; }
	public bool WaveStarted { get; private set; }
	
	private Vector3 speederYOffset = new Vector3(0, 0.6f, 0);
		
	private void Awake()
	{
		WaveDelay = 10;
		WaveDelayCounter = WaveDelay;
		enemyDelayCounter = enemyDelay;
		waves = LevelData.getEnemiesMap(Variables.Level);
		CurrentWave = 1;
		WaveStarted = false;
	}
	
	private void Update() {
		if(!WaveStarted)
		{
			return;
		}
		if(enemyInWaveCounter == waves[CurrentWave - 1].Count)
		{
			if(Variables.enemies.Count == 0)
			{
				CurrentWave++;
				WaveDelayCounter = 0;
				enemyInWaveCounter = 0;
				if(CurrentWave > waves.Count)
				{
					enemyInWaveCounter = 0;
					CurrentWave = 1;
					LevelWon();
				}
			}
		}
		else
		{
			if(WaveDelayCounter < WaveDelay)
			{
				WaveDelayCounter += Time.deltaTime;
			}
			else
			{
				if(enemyDelayCounter < enemyDelay) 
				{
					enemyDelayCounter += Time.deltaTime;
				} 
				else 
				{
					enemyDelayCounter = 0;
					
					int enemyNumber = waves[CurrentWave - 1][enemyInWaveCounter];
					GameObject enemy = null;
					Vector3 startPoint = road.transform.FindChild("StartPoint").transform.position;
					
					switch(enemyNumber)
					{
					case 0:
						enemy = Instantiate(speederPrefab, startPoint + speederYOffset, Quaternion.identity) as GameObject;
						enemy.GetComponent<EnemyScript>().YOffset = speederYOffset;
						break;
					}
					
					enemy.GetComponent<EnemyScript>().FirstPoint = road.transform.FindChild("RoadPoint_1").gameObject;
					enemy.GetComponent<EnemyScript>().Init();
					Variables.enemies.Add(enemy);
					
					enemyInWaveCounter++;
				}
			}
		}
	}
	
	public void StartWave()
	{
		WaveStarted = true;
	}
	
	private void LevelWon()
	{
		Debug.Log("YOU WIN!!!");
	}
}
