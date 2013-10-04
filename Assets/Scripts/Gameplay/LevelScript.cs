using UnityEngine;
using System.Collections;

public class LevelScript : MonoBehaviour {
	
	public GameObject[] gunPrefabs;
	public GameObject[] freezePrefabs;
	public GameObject[] swarmPrefabs;
	public GameObject[] hammerPrefabs;
	public GameObject[] antiMaterielPrefabs;
	public GameObject[] teslaPrefabs;
	public GameObject[] plasmaCutterPrefabs;
	public GameObject[] minerPrefabs;
	
	private GameObject turret;
	private Vector3 turretInstallHeight = new Vector3(0, 0.02f, 0);
	
	public void InstallTurret(TurretScript.TurretType type,
							  Vector3 turretPosition,
							  int turretLevel, Quaternion rotation) {
		GameObject prefab = null;
		switch(type)
		{
			case TurretScript.TurretType.Gun:
				prefab = gunPrefabs[turretLevel - 1];
				break;
			case TurretScript.TurretType.Freeze:
				prefab = freezePrefabs[turretLevel - 1];
				break;
			case TurretScript.TurretType.Hammer:
				prefab = hammerPrefabs[turretLevel - 1];
				break;
			case TurretScript.TurretType.Swarm:
				prefab = swarmPrefabs[turretLevel - 1];
				break;
			case TurretScript.TurretType.AntiMaterial:
				prefab = antiMaterielPrefabs[turretLevel - 1];
				break;
			case TurretScript.TurretType.Tesla:
				prefab = teslaPrefabs[turretLevel - 1];
				break;
			case TurretScript.TurretType.PlasmaCutter:
				prefab = plasmaCutterPrefabs[turretLevel - 1];
				break;
			case TurretScript.TurretType.Miner:
				prefab = minerPrefabs[turretLevel - 1];
				break;
		}
		
		turret = Instantiate(prefab, (turretPosition + turretInstallHeight), Quaternion.identity) as GameObject;
		
		switch(type) {
			case TurretScript.TurretType.Gun:
				turret.GetComponent<TurretScript>().TurretCost = TurretScript.GunCost;
				turret.GetComponent<TurretScript>().UpgradeCost = TurretScript.GunUpdateCost;
				break;
			case TurretScript.TurretType.Hammer:
				turret.GetComponent<TurretScript>().TurretCost = TurretScript.HammerCost;
				turret.GetComponent<TurretScript>().UpgradeCost = TurretScript.HammerUpdateCost;
				break;
			case TurretScript.TurretType.Swarm:
				turret.GetComponent<TurretScript>().TurretCost = TurretScript.SwarmCost;
				turret.GetComponent<TurretScript>().UpgradeCost = TurretScript.SwarmUpdateCost;
				break;
			case TurretScript.TurretType.Freeze:
				turret.GetComponent<TurretScript>().TurretCost = TurretScript.FreezeCost;
				turret.GetComponent<TurretScript>().UpgradeCost = TurretScript.FreezeUpdateCost;
				break;
			case TurretScript.TurretType.AntiMaterial:
				turret.GetComponent<TurretScript>().TurretCost = TurretScript.AntiMaterialCost;
				turret.GetComponent<TurretScript>().UpgradeCost = TurretScript.AntiMaterialUpdateCost;
				break;
			case TurretScript.TurretType.Tesla:
				turret.GetComponent<TurretScript>().TurretCost = TurretScript.TeslaCost;
				turret.GetComponent<TurretScript>().UpgradeCost = TurretScript.TeslaUpdateCost;
				break;
			case TurretScript.TurretType.PlasmaCutter:
				turret.GetComponent<TurretScript>().TurretCost = TurretScript.PlasmaCutterCost;
				turret.GetComponent<TurretScript>().UpgradeCost = TurretScript.PlasmaCutterUpdateCost;
				break;
			case TurretScript.TurretType.Miner:
				turret.GetComponent<TurretScript>().TurretCost = TurretScript.MinerCost;
				turret.GetComponent<TurretScript>().UpgradeCost = TurretScript.MinerUpdateCost;
				break;
		}
		
		TurretScript turretScript = turret.GetComponent<TurretScript>();
		
		turretScript.Level = turretLevel;
		turretScript.Init();
		Variables.Money -= turretScript.TurretCost;
		turretScript.CurrentType = type;
		turretScript.BodyRotation = rotation;
		Variables.turrets.Add(turret);
	}
	
	public void UpgradeTurret(TurretScript.TurretType type, int currTurretLevel, Quaternion rotation)
	{
		Vector3 pos = turret.transform.position;
		UninstallTurret();
		int nextLevel = currTurretLevel + 1;
		InstallTurret(type, pos, nextLevel, rotation);
	}
	
	public void UninstallTurret() {
		if(turret != null) {
			Variables.Money += turret.GetComponent<TurretScript>().TurretCost;
			Variables.turrets.Remove(turret);
			Destroy(turret);
		}
	}
}
