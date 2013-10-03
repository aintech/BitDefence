using UnityEngine;
using System.Collections;

public class BuyTurretScript : GUIScript {
	
	public GUIStyle buttonStyle;
	public GameObject actionMenuPrefab;
	
	private GameObject actionMenu;
	private Vector3 actionMenuOffset = new Vector3(0, 0.6f, 0);
	
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
	
	public GameObject gunPrefab;
	public GameObject hammerPrefab;
	public GameObject swarmPrefab;
	public GameObject freezePrefab;
	public GameObject antiMaterielPrefab;
	public GameObject teslaPrefab;
	public GameObject plasmaCutterPrefab;
	public GameObject minerPrefab;
	
	private GameObject installingTurret;
	
	private GameObject turretGhost;
	
	private Rect turretBtnBox;
	
	private bool showingTurretMenu;
	private Rect[] turretBtns;
	
	private Vector2 ghostBounds;
	
	override protected void Init()
	{
		GameObject level = GameObject.Find("Level");
		
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
		TurretScript.TurretType type = TurretScript.TurretType.Gun;
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
			if(turretGhost != null)
			{
				if(turretGhost.GetComponent<GhostTurretScript>().CanBePlaced)
				{
					ChooseTurret();
				}
				else
				{
					Destroy(turretGhost);
				}
			}
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
	
	private void ChooseTurret()
	{
		actionMenu = Instantiate(actionMenuPrefab, turretGhost.transform.position + actionMenuOffset, Quaternion.identity) as GameObject;
		actionMenu.GetComponent<ActionMenuScript>().Init(turretGhost.GetComponent<GhostTurretScript>().type, this);
	}
	
	public void InstallTurret(TurretScript.TurretType type)
	{
		switch(type)
		{
		case TurretScript.TurretType.AntiMaterial:
			installingTurret = Instantiate(antiMaterielPrefab, turretGhost.transform.position, Quaternion.identity) as GameObject;
			break;
			//CONTINIUM - Installing turrets
		}
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
