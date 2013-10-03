using UnityEngine;
using System.Collections;

public class GameStatisticScript : GUIScript {
	
	public GUIStyle labelStyle;
	public GUIStyle messageStyle;
	
	private Rect moneyBox;
	private Rect moneyLabel;
	private Rect livesBox;
	private Rect livesLabel;
	private Rect waveBox;
	private Rect waveLabel;
	
	private Rect gameOverLabel;
	
	private SpawnerScript spawnerScript;
	
	override protected void Init()
	{
		spawnerScript = GameObject.Find("Spawner").GetComponent<SpawnerScript>();
		
		string label = "Money: " + Variables.Money + "$";
		Vector2 labelDimension = labelStyle.CalcSize(new GUIContent(label));
		
		gameOverLabel = new Rect(200, 5, 220, 20);
		
		moneyLabel = new Rect(10, 10, 100, labelDimension.y + 5);
		moneyBox = new Rect(moneyLabel.x - 5, moneyLabel.y - 5, moneyLabel.width + 10, moneyLabel.height + 10);
		
		livesLabel = new Rect(moneyLabel.x, moneyLabel.y + moneyLabel.height + 15, moneyLabel.width, moneyLabel.height);
		livesBox = new Rect(livesLabel.x - 5, livesLabel.y - 5, livesLabel.width + 10, livesLabel.height + 10);
		
		waveLabel = new Rect(moneyLabel.x, livesLabel.y + livesLabel.height + 15, moneyLabel.width, moneyLabel.height);
		waveBox = new Rect(waveLabel.x - 5, waveLabel.y - 5, waveLabel.width + 10, waveLabel.height + 10);
	}
	
	private void OnGUI()
	{
		GUI.Box(moneyBox, "");
		GUI.Label(moneyLabel, "Money: " + Variables.Money + "$", labelStyle);
		
		GUI.Box(livesBox, "");
		GUI.Label(livesLabel, "Lives: " + Variables.Lives, labelStyle);
		
		GUI.Box(waveBox, "");
		
		if(spawnerScript.WaveDelayCounter < spawnerScript.WaveDelay)
		{
			int waveCount = Mathf.RoundToInt(spawnerScript.WaveDelay - spawnerScript.WaveDelayCounter);
			GUI.Label(waveLabel, "Wave: " + spawnerScript.CurrentWave.ToString() + " - in: " + waveCount.ToString(), labelStyle);
		}
		else
		{
			GUI.Label(waveLabel, "Wave: " + spawnerScript.CurrentWave.ToString(), labelStyle);
		}
		
		if(Variables.GameOver)
		{
			GUI.Label(gameOverLabel, "Game Over");
		}
		else if(!spawnerScript.WaveStarted)
		{
			GUI.Label(gameOverLabel, "Press 'SPACE' to start wave", messageStyle);
		}
	}
	
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(!spawnerScript.WaveStarted)
			{
				spawnerScript.WaveStarted = true;
			}
		}
	}
}
