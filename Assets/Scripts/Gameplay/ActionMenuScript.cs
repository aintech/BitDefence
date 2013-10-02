using UnityEngine;
using System.Collections;

public class ActionMenuScript : MonoBehaviour {
	
	public GameObject actionMenuTextPrefab;
	
	private GameObject actionMenuText;
	private TextMesh menuText;
	
	private MarkerMenuScript parentScript;
	private int turretCost;
	
	public TurretScript.TurretType TurretType { get; private set; }
	
	private void Awake()
	{
		foreach(Transform child in transform)
		{
			if(child.name == "Agree" || child.name == "Cancel")
			{
				child.gameObject.AddComponent<ActionMenuButtonScript>();
			}
		}
		
		actionMenuText = Instantiate(actionMenuTextPrefab, transform.position, Quaternion.identity) as GameObject;
		menuText = actionMenuText.transform.FindChild("TextField").GetComponent<TextMesh>();
	}
	
	public void Init(TurretScript.TurretType turretType, MarkerMenuScript parentScript)
	{
		TurretType = turretType;
		this.parentScript = parentScript;
		switch(turretType)
		{
			case TurretScript.TurretType.AntiMaterial:
				menuText.text = "Anti\nMaterial";
				turretCost = TurretScript.AntiMaterialCost;
				break;
			case TurretScript.TurretType.Freeze:
				menuText.text = "Freeze";
				turretCost = TurretScript.FreezeCost;
				break;
			case TurretScript.TurretType.Gun:
				menuText.text = "Gun";
				turretCost = TurretScript.GunCost;
				break;
			case TurretScript.TurretType.Hammer:
				menuText.text = "Hammer";
				turretCost = TurretScript.HammerCost;
				break;
			case TurretScript.TurretType.Swarm:
				menuText.text = "Swarm";
				turretCost = TurretScript.SwarmCost;
				break;
			case TurretScript.TurretType.Tesla:
				menuText.text = "Tesla";
				turretCost = TurretScript.TeslaCost;
				break;
			case TurretScript.TurretType.PlasmaCutter:
				menuText.text = "Plasma\nCutter";
				turretCost = TurretScript.PlasmaCutterCost;
				break;
			case TurretScript.TurretType.Miner:
				menuText.text = "Miner";
				turretCost = TurretScript.MinerCost;
				break;
		}
	}
	
	public void setAnswer(bool agree)
	{
		if(agree)
		{
			if(turretCost <= Variables.Money) parentScript.TurretForInstall(TurretType);
		}
		parentScript.HideActionMenu();
	}
	
	private void OnDestroy()
	{
		if(actionMenuText != null)
		{
			Destroy(actionMenuText);
		}
	}
}
