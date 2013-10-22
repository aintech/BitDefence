using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {
	
	public GUIStyle iconStyle;
	public GUIStyle counterStyle;
	public GUIStyle guiTextStyle;
	public GUIStyle messageStyle;
	
	private Vector2 btnDimension = new Vector2(30, 30);
	
	public Texture moneyIcon;
	public Texture livesIcon;
	public Texture waveIcon;
	public Texture enemiesLeftIcon;
	
	private Rect guiPosition;
	private Rect moneyIconPosition;
	private Rect livesIconPosition;
	private Rect waveIconPosition;
	private Rect enemiesLeftIconPosition;
	
	private Rect nextWaveCounterLabel;
	
	private float moneyIconOffset = 10;
	private float livesIconOffset = 100;
	private float waveIconOffset = 170;
	private float enemiesLeftIconOffset = 240;
	
	private Rect moneyLabel;
	private Rect livesLabel;
	private Rect waveLabel;
	private Rect enemiesLeftLabel;
	
	private Rect messageBox;
	private Rect messageLabel;
	
	private SpawnerScript spawnerScript;
	private int enemiesInWave;
	
	private void Awake() {
		Variables.Reload();
		
		spawnerScript = GameObject.Find("Spawner").GetComponent<SpawnerScript>();
		
		moneyIconPosition = new Rect(moneyIconOffset, 10, btnDimension.x, btnDimension.y);
		livesIconPosition = new Rect(livesIconOffset, 10, btnDimension.x, btnDimension.y);
		waveIconPosition = new Rect(waveIconOffset, 10, btnDimension.x, btnDimension.y);
		enemiesLeftIconPosition = new Rect(enemiesLeftIconOffset, 10, btnDimension.x, btnDimension.y);
		
		Vector2 labelDimension = guiTextStyle.CalcSize(new GUIContent("Word"));
		
		moneyLabel = new Rect(moneyIconPosition.x + moneyIconPosition.width + 5, 10, 100, labelDimension.y + 5);
		livesLabel = new Rect(livesIconPosition.x + livesIconPosition.width + 5, moneyLabel.y, 100, labelDimension.y + 5);
		waveLabel = new Rect(waveIconPosition.x + waveIconPosition.width + 5, livesLabel.y, 100, labelDimension.y + 5);
		nextWaveCounterLabel = new Rect(waveLabel.x,  waveLabel.height - waveLabel.height * .5f, 14, 14);
		enemiesLeftLabel = new Rect(enemiesLeftIconPosition.x + enemiesLeftIconPosition.width + 5, waveLabel.y, 100, labelDimension.y + 5);
		
		guiPosition = new Rect(5, 5, 300, btnDimension.y + 10);
		
		messageBox = new Rect(Screen.width - 5 - 200, 5, 200, labelDimension.y + 5);
		messageLabel = new Rect(messageBox.x, messageBox.y, messageBox.width, messageBox.height);
	}
	
	private void OnGUI()
	{
		GUI.Box(guiPosition, "");
		
		GUI.DrawTexture(moneyIconPosition, moneyIcon);
		GUI.DrawTexture(livesIconPosition, livesIcon);
		GUI.DrawTexture(waveIconPosition, waveIcon);
		GUI.DrawTexture(enemiesLeftIconPosition, enemiesLeftIcon);
		
		GUI.Label(moneyLabel, Variables.Money.ToString(), guiTextStyle);
		GUI.Label(livesLabel, Variables.Lives.ToString(), guiTextStyle);
		GUI.Label(waveLabel, spawnerScript.CurrentWave.ToString(), guiTextStyle);
		if(spawnerScript.WaveDelayCounter < spawnerScript.WaveDelay)
		{
			int waveCount = Mathf.RoundToInt(spawnerScript.WaveDelay - spawnerScript.WaveDelayCounter);
			GUI.Label(nextWaveCounterLabel, waveCount.ToString(), counterStyle);
		}
		GUI.Label(enemiesLeftLabel, Variables.EnemiesLeft == 0? "-": Variables.EnemiesLeft.ToString(), guiTextStyle);
		
		
		if(Variables.GameOver)
		{
			GUI.Box(messageBox, "");
			GUI.Label(messageBox, "GAME OVER");
		}
		else if(!spawnerScript.WaveStarted)
		{
			GUI.Box(messageBox, "");
			GUI.Label(messageBox, "Press 'SPACE' to start wave", messageStyle);
		}
	}
	
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Variables.Reload();
			Application.LoadLevel("LevelMenu");
		}
		
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(!spawnerScript.WaveStarted)
			{
				spawnerScript.StartWave();
			}
		}
	}
}
