using UnityEngine;
using System.Collections;

public class MarkerMenuButtonScript : MonoBehaviour {
	
	private Shader diffuseShader = Shader.Find("Diffuse");
	private Shader grayscaleShader = Shader.Find("Custom/GrayScale");
	
	private Material mainMaterial;
	
	private Transform trans;
	
	private bool available;
	
	public TurretScript.TurretType TurretType { get; private set; }
	public int TurretCost { get; private set; }
	
	private void Awake()
	{
		trans = transform;
		available = true;
		if(trans.name.Equals("Buy_Gun"))
		{
			TurretType = TurretScript.TurretType.Gun;
			TurretCost = TurretScript.GunCost;
		}
		else if(trans.name == "Buy_Hammer")
		{
			TurretType = TurretScript.TurretType.Hammer;
			TurretCost = TurretScript.HammerCost;
		}
		else if(trans.name == "Buy_Swarm")
		{
			TurretType = TurretScript.TurretType.Swarm;
			TurretCost = TurretScript.SwarmCost;
		}
		else if(trans.name == "Buy_Freeze")
		{
			TurretType = TurretScript.TurretType.Freeze;
			TurretCost = TurretScript.FreezeCost;
		}
		else if(trans.name == "Buy_AntiMaterial")
		{
			TurretType = TurretScript.TurretType.AntiMaterial;
			TurretCost = TurretScript.AntiMaterialCost;
		}
		else if(trans.name == "Buy_Tesla")
		{
			TurretType = TurretScript.TurretType.Tesla;
			TurretCost = TurretScript.TeslaCost;
		}
		else if(trans.name == "Buy_PlasmaCutter")
		{
			TurretType = TurretScript.TurretType.PlasmaCutter;
			TurretCost = TurretScript.PlasmaCutterCost;
		}
		else if(trans.name == "Buy_Miner")
		{
			TurretType = TurretScript.TurretType.Miner;
			TurretCost = TurretScript.MinerCost;
		}
		else
		{
			TurretCost = 0;
		}
		
		mainMaterial = trans.renderer.material;
	}
	
	private void Update() {
		if(TurretCost <= Variables.Money && !available)
		{
			SetAvailable(true);
		}
		else if(TurretCost > Variables.Money && available)
		{
			SetAvailable(false);
		}
	}
	
	private void SetAvailable(bool available)
	{
		this.available = available;
		if(this.available)
		{
			mainMaterial.shader = diffuseShader;
		}
		else
		{
			mainMaterial.shader = grayscaleShader;
		}
	}
	
	private void OnMouseDown()
	{
		if(!available) return;
		if(trans.name.Equals("Cancel"))
		{
			trans.parent.GetComponent<MarkerMenuScript>().RemoveMarkerMenu();
		}
		else if(TurretCost <= Variables.Money)
		{
			trans.parent.GetComponent<MarkerMenuScript>().ShowActionMenu(TurretType);
		}
	}
}
