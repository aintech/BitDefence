using UnityEngine;
using System.Collections;

public class BuyTurretScript : MonoBehaviour {
	
	public GUIStyle buttonStyle;
	public GameObject actionMenuPrefab;
	
	private Vector2 btnDimension = new Vector2(30, 30);
	private float btnOffset = 3;
	
	private GameObject actionMenu;
	private TurretScript.TurretType type;
	
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
	
	private GameObject installingTurret;
	
	private GameObject turretGhost;
	
	private Rect turretBtnBox;
	
	private bool showingTurretMenu;
	private Rect[] turretBtns;
	
	private Vector2 ghostBounds;
	
	private GameObject level;
	
	private void Awake()
	{
		level = GameObject.Find("Level");
		
		Bounds levelBounds = level.collider.bounds;
		
		ghostBounds = new Vector2(levelBounds.extents.x - 0.5f, levelBounds.extents.z - 0.5f);
		
		turretBtnBox = new Rect(5, Screen.height - btnDimension.y - 5, btnDimension.x, btnDimension.y);
		
		turretBtns = new Rect[8];
		for(int i = 0; i < turretBtns.Length; i++)
		{
			turretBtns[i] = new Rect(5 + (i * (btnDimension.x + btnOffset)), turretBtnBox.y - btnDimension.y - 5, btnDimension.x, btnDimension.y);
		}
	}
	
	private void OnGUI()
	{
		if(GUI.Button(turretBtnBox, turretIcon, buttonStyle))
		{
			showingTurretMenu = !showingTurretMenu;
			Variables.RemoveAllFromElements();
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
			type = TurretScript.TurretType.Gun;
		}
		if(tex == hammerIcon)
		{
			turretGhost = Instantiate(ghostHammer) as GameObject;
			type = TurretScript.TurretType.Hammer;
		}
		if(tex == swarmIcon)
		{
			turretGhost = Instantiate(ghostSwarm) as GameObject;
			type = TurretScript.TurretType.Swarm;
		}
		if(tex == freezeIcon)
		{
			turretGhost = Instantiate(ghostFreeze) as GameObject;
			type = TurretScript.TurretType.Freeze;
		}
		if(tex == antiMaterialIcon)
		{
			turretGhost = Instantiate(ghostAntiMaterial) as GameObject;
			type = TurretScript.TurretType.AntiMaterial;
		}
		if(tex == teslaIcon)
		{
			turretGhost = Instantiate(ghostTesla) as GameObject;
			type = TurretScript.TurretType.Tesla;
		}
		if(tex == plasmaCutterIcon)
		{
			turretGhost = Instantiate(ghostPlasmaCutter) as GameObject;
			type = TurretScript.TurretType.PlasmaCutter;
		}
		if(tex == minerIcon)
		{
			turretGhost = Instantiate(ghostMiner) as GameObject;
			type = TurretScript.TurretType.Miner;
		}
		
		turretGhost.GetComponent<GhostTurretScript>().Init(this, type);
	}
	
	private void Update()
	{
		if(Input.GetMouseButtonUp(0))
		{
			performUnclick();
		}
		
		if(turretGhost != null)
		{
			RaycastHit hit;
			Ray scrRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			LayerMask mask = 1 << LayerMask.NameToLayer("Default");
			
			if(Physics.Raycast(scrRay, out hit, Mathf.Infinity, mask))
			{
				if(hit.point.x > -ghostBounds.x && hit.point.x < ghostBounds.x &&
					hit.point.z > -ghostBounds.y && hit.point.z < ghostBounds.y && 
					actionMenu == null)
				{
					turretGhost.transform.position = hit.point;
				}
			}
		}
	}
	
	private void performUnclick()
	{
		if(turretGhost != null)
		{
			if(turretGhost.GetComponent<GhostTurretScript>().CanBePlaced)
			{
				if(actionMenu == null)
				{
					Vector3 instHeight = new Vector3(0, Variables.TurretHeight(type) + 0.5f, 0);
					actionMenu = Instantiate(actionMenuPrefab, turretGhost.transform.position + instHeight, Quaternion.identity) as GameObject;
					actionMenu.GetComponent<ActionMenuScript>().Init(turretGhost.GetComponent<GhostTurretScript>().type, this);
				}
				else
				{
					Variables.RemoveAllFromElements();
				}
			}
			else
			{
				Destroy(turretGhost);
			}
		}
	}
	
	public void InstallTurret(TurretScript.TurretType type)
	{
		level.GetComponent<LevelScript>().InstallTurret(type, turretGhost.transform.position, 1, Quaternion.identity);
		Destroy(turretGhost);
	}
	
	public void RemoveActionMenu()
	{
		if(actionMenu != null)
		{
			Destroy(actionMenu);
		}
		if(turretGhost != null)
		{
			Destroy(turretGhost);
		}
	}
}
