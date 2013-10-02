using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {
	
	public Texture turretIcon;
	public Texture gunIcon;
	public Texture hammerIcon;
	public Texture swarmIcon;
	public Texture freezeIcon;
	public Texture antiMaterialIcon;
	public Texture teslaIcon;
	public Texture plasmaCutterIcon;
	public Texture minerIcon;
	
	public GameObject ghostGun;
	public GameObject ghostHammer;
	public GameObject ghostSwarm;
	public GameObject ghostFreeze;
	public GameObject ghostAntiMaterial;
	public GameObject ghostTesla;
	public GameObject ghostPlasmaCutter;
	public GameObject ghostMiner;
	
	private GameObject turretGhost;
	
	private Vector2 btnDimension = new Vector2(30, 30);
	private float btnOffset = 3;
	
	public GUIStyle style;
	public GUIStyle MessageStyle;
	public GUIStyle buttonStyle;
	
	private Rect moneyBox;
	private Rect moneyLabel;
	private Rect livesBox;
	private Rect livesLabel;
	private Rect waveBox;
	private Rect waveLabel;
	
	private Rect turretBtnBox;
	
	private Rect gameOverLabel;
	
	private SpawnerScript spawnerScript;
	
	private bool showingTurretMenu;
	private Rect[] turretBtns;
	
	private void Awake() {
		Variables.Reload();
		
		spawnerScript = GameObject.Find("Spawner").GetComponent<SpawnerScript>();
		string label = "Money: " + Variables.Money + "$";
		Vector2 labelDimension = style.CalcSize(new GUIContent(label));
		
		gameOverLabel = new Rect(200, 5, 220, 20);
		
		moneyLabel = new Rect(10, 10, 100, labelDimension.y + 5);
		moneyBox = new Rect(moneyLabel.x - 5, moneyLabel.y - 5, moneyLabel.width + 10, moneyLabel.height + 10);
		
		livesLabel = new Rect(moneyLabel.x, moneyLabel.y + moneyLabel.height + 15, moneyLabel.width, moneyLabel.height);
		livesBox = new Rect(livesLabel.x - 5, livesLabel.y - 5, livesLabel.width + 10, livesLabel.height + 10);
		
		waveLabel = new Rect(moneyLabel.x, livesLabel.y + livesLabel.height + 15, moneyLabel.width, moneyLabel.height);
		waveBox = new Rect(waveLabel.x - 5, waveLabel.y - 5, waveLabel.width + 10, waveLabel.height + 10);
		
		turretBtnBox = new Rect(5, Screen.height - btnDimension.y - 5, btnDimension.x, btnDimension.y);
		
		turretBtns = new Rect[8];
		for(int i = 0; i < turretBtns.Length; i++)
		{
			turretBtns[i] = new Rect(5 + (i * (btnDimension.x + btnOffset)), turretBtnBox.y - btnDimension.y - 5, btnDimension.x, btnDimension.y);
		}
	}
	
	private void OnGUI() {
		GUI.Box(moneyBox, "");
		GUI.Label(moneyLabel, "Money: " + Variables.Money + "$", style);
		
		GUI.Box(livesBox, "");
		GUI.Label(livesLabel, "Lives: " + Variables.Lives, style);
		
		GUI.Box(waveBox, "");
		if(spawnerScript.WaveDelayCounter < spawnerScript.WaveDelay)
		{
			int waveCount = Mathf.RoundToInt(spawnerScript.WaveDelay - spawnerScript.WaveDelayCounter);
			GUI.Label(waveLabel, "Wave: " + spawnerScript.CurrentWave.ToString() + " - in: " + waveCount.ToString(), style);
		}
		else
		{
			GUI.Label(waveLabel, "Wave: " + spawnerScript.CurrentWave.ToString(), style);
		}
		
		if(Variables.GameOver)
		{
			GUI.Label(gameOverLabel, "Game Over");
		}
		else if(!spawnerScript.WaveStarted)
		{
			GUI.Label(gameOverLabel, "Press 'SPACE' to start wave", MessageStyle);
		}
		
		if(GUI.Button(turretBtnBox, turretIcon, buttonStyle))
		{
			showingTurretMenu = !showingTurretMenu;
		}
		
		if(showingTurretMenu)
		{
			Texture tex = null;
			
			for(int i = 0; i < turretBtns.Length; i++)
			{
				switch(i)
				{
					case 0: tex = gunIcon; break;
					case 1: tex = hammerIcon; break;
					case 2: tex = swarmIcon; break;
					case 3: tex = freezeIcon; break;
					case 4: tex = antiMaterialIcon; break;
					case 5: tex = teslaIcon; break;
					case 6: tex = plasmaCutterIcon; break;
					case 7: tex = minerIcon; break;
				}
				if(GUI.RepeatButton(turretBtns[i], tex, buttonStyle))
				{
					showingTurretMenu = false;
					CreateGhostTurret(tex);
				}
			}
		}
	}
	
	private void CreateGhostTurret(Texture tex)
	{
		if(tex == gunIcon)
		{
			turretGhost = Instantiate(ghostGun) as GameObject;
		}
		if(tex == hammerIcon)
		{
			turretGhost = Instantiate(ghostHammer) as GameObject;
		}
		if(tex == swarmIcon)
		{
			turretGhost = Instantiate(ghostSwarm) as GameObject;
		}
		if(tex == freezeIcon)
		{
			turretGhost = Instantiate(ghostFreeze) as GameObject;
		}
		if(tex == antiMaterialIcon)
		{
			turretGhost = Instantiate(ghostAntiMaterial) as GameObject;
		}
		if(tex == teslaIcon)
		{
			turretGhost = Instantiate(ghostTesla) as GameObject;
		}
		if(tex == plasmaCutterIcon)
		{
			turretGhost = Instantiate(ghostPlasmaCutter) as GameObject;
		}
		if(tex == minerIcon)
		{
			turretGhost = Instantiate(ghostMiner) as GameObject;
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
				spawnerScript.WaveStarted = true;
			}
		}
		if(Input.GetMouseButtonUp(0))
		{
			if(turretGhost != null)
			{
				Destroy(turretGhost);
			}
		}
		
		if(turretGhost != null)
		{
			//CONTINIUM - рэйкаст по Y на Level
			turretGhost.transform.position = Input.mousePosition;
		}
	}
}
