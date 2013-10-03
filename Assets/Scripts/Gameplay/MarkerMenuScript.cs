using UnityEngine;
using System.Collections;

public class MarkerMenuScript : MonoBehaviour {
	
	public GameObject costTextPrefub;
	public GameObject actionMenuprefab;
	public GameObject rangeSpherePrefab;
	
	public GameObject gunGhostPrefab;
	public GameObject freezeGhostPrefab;
	public GameObject swarmGhostPrefab;
	public GameObject hammerGhostPrefab;
	public GameObject antiMaterialGhostPrefab;
	public GameObject teslaGhostPrefab;
	public GameObject plasmaCutterGhostPrefab;
	public GameObject minerGhostPrefab;
	
	private GameObject costText;
	private GameObject actionMenu;
	private GameObject rangeSphere;
	
	private Transform trans;
	
	private GameObject ghostTurret;
	private Vector3 ghostInstallHeight = new Vector3(0, 0.02f, 0);
	
	public GameObject Marker{ get; set; }
	
	private void Awake()
	{
		trans = transform;
		foreach(Transform childTrans in trans) 
		{
			childTrans.gameObject.AddComponent<MarkerMenuButtonScript>();
		}
		
		Vector3 costTextPos = new Vector3(trans.position.x, trans.position.y + 0.1f, trans.position.z);
		costText = Instantiate(costTextPrefub, costTextPos, Quaternion.identity) as GameObject;
		
		foreach(Transform costTextTrans in costText.transform)
		{
			TextMesh textMesh = costTextTrans.GetComponent<TextMesh>();
			if(costTextTrans.name == "GunCostText")
			{
				textMesh.text = TurretScript.GunCost.ToString();
			}
			else if(costTextTrans.name == "HammerCostText")
			{
				textMesh.text = TurretScript.HammerCost.ToString();
			}
			else if(costTextTrans.name == "SwarmCostText")
			{
				textMesh.text = TurretScript.SwarmCost.ToString();
			}
			else if(costTextTrans.name == "FreezeCostText")
			{
				textMesh.text = TurretScript.FreezeCost.ToString();
			}
			else if(costTextTrans.name == "AntiMaterialCostText")
			{
				textMesh.text = TurretScript.AntiMaterialCost.ToString();
			}
			else if(costTextTrans.name == "TeslaCostText")
			{
				textMesh.text = TurretScript.TeslaCost.ToString();
			}
			else if(costTextTrans.name == "PlasmaCutterCostText")
			{
				textMesh.text = TurretScript.PlasmaCutterCost.ToString();
			}
			else if(costTextTrans.name == "MinerCostText")
			{
				textMesh.text = TurretScript.MinerCost.ToString();
			}
			else
			{
				Debug.Log("MarkerMenuScript: costTextTrans.name wrong");
			}
		}
	}
	
	public void RemoveMarkerMenu() 
	{
		Marker.GetComponent<MarkerScript>().RemoveMarkerMenu();
	}
	
	public void TurretForInstall(TurretScript.TurretType type) 
	{
		Marker.GetComponent<MarkerScript>().InstallTurret(type, 1, Quaternion.identity);
	}
	
	public void ShowActionMenu(TurretScript.TurretType turretType)
	{
		GameObject prefab = null;
		
		switch(turretType)
		{
			case TurretScript.TurretType.AntiMaterial: prefab = antiMaterialGhostPrefab; break;
			case TurretScript.TurretType.Freeze: prefab = freezeGhostPrefab; break;
			case TurretScript.TurretType.Gun: prefab = gunGhostPrefab; break;
			case TurretScript.TurretType.Hammer: prefab = hammerGhostPrefab; break;
			case TurretScript.TurretType.Swarm: prefab = swarmGhostPrefab; break;
			case TurretScript.TurretType.Tesla: prefab = teslaGhostPrefab; break;
			case TurretScript.TurretType.PlasmaCutter: prefab = plasmaCutterGhostPrefab; break;
			case TurretScript.TurretType.Miner: prefab = minerGhostPrefab; break;
		}
		
		ghostTurret = Instantiate(prefab, Marker.transform.position + ghostInstallHeight, Quaternion.identity) as GameObject;
		
//		actionMenu = Instantiate(actionMenuprefab, trans.position + new Vector3(0, 0.2f, 0), Quaternion.identity) as GameObject;
//		actionMenu.GetComponent<ActionMenuScript>().Init(turretType, this);
		
		rangeSphere = Instantiate(rangeSpherePrefab, Marker.transform.position, Quaternion.identity) as GameObject;
		rangeSphere.GetComponent<RangeSphereScript>().SetRange(getTurretRange(turretType));
		
		foreach(Transform costTextTrans in costText.transform)
		{
			costTextTrans.gameObject.SetActive(false);
		}
		foreach(Transform child in trans)
		{
			//child.GetComponent<MarkerMenuButtonScript>().SetAvailable(false);
			child.gameObject.SetActive(false);
		}
	}
	
	public void HideActionMenu()
	{
		foreach(Transform costTextTrans in costText.transform)
		{
			costTextTrans.gameObject.SetActive(true);
		}
		foreach(Transform child in trans)
		{
			//child.GetComponent<MarkerMenuButtonScript>().SetAvailable(true);
			child.gameObject.SetActive(true);
		}
		if(ghostTurret != null)
		{
			Destroy(ghostTurret);
		}
		if(actionMenu != null)
		{
			Destroy(actionMenu);
		}
		if(rangeSphere != null)
		{
			Destroy(rangeSphere);
		}
	}
	
	private float getTurretRange(TurretScript.TurretType type)
	{
		switch(type)
		{
			case TurretScript.TurretType.Gun:
				return TurretScript.GunStartRange;
			case TurretScript.TurretType.Freeze:
				return TurretScript.FreezeStartRange;
			case TurretScript.TurretType.AntiMaterial:
				return TurretScript.AntiMaterialStartRange;
			case TurretScript.TurretType.Hammer:
				return TurretScript.HammerStartRange;
			case TurretScript.TurretType.Swarm:
				return TurretScript.SwarmStartRange;
			case TurretScript.TurretType.Tesla:
				return TurretScript.TeslaStartRange;
			case TurretScript.TurretType.PlasmaCutter:
				return TurretScript.PlasmaCutterStartRange;
			case TurretScript.TurretType.Miner:
				return TurretScript.MinerStartRange;
			default:
				return 0;
		}
	}
	
	private void OnDestroy()
	{
		if(costText != null)
		{
			Destroy(costText);
		}
	}
}
