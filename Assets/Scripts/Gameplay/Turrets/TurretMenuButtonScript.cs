using UnityEngine;
using System.Collections;

public class TurretMenuButtonScript : MonoBehaviour {
	
	private Shader diffuseShader = Shader.Find("Diffuse");
	private Shader grayscaleShader = Shader.Find("Custom/GrayScale");
	
	private Transform trans;
	private Material mainMaterial;
	TurretScript turScript;
	
	private bool available = true;
	
	private int upgradeCost = 0;
	
	private void Awake()
	{
		trans = transform;
		mainMaterial = transform.renderer.material;
	}
	
	public void Init()
	{
		turScript = trans.parent.GetComponent<TurretMenuScript>().Turret.GetComponent<TurretScript>();
		if(trans.name.Equals("Upgrade"))
		{
			upgradeCost = turScript.UpgradeCost;
		}
	}
	
	private void Update()
	{
		if(trans.name != "Upgrade") return;
		if(upgradeCost <= Variables.Money && !available)
		{
			setAvailable(true);
		}
		else if(upgradeCost > Variables.Money && available)
		{
			setAvailable(false);
		}
	}
	
	private void setAvailable(bool available)
	{
		this.available = available;
		if(available)
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
		if(trans.name.Equals("Cancel"))
		{
			transform.parent.GetComponent<TurretMenuScript>().RemoveTurretMenu();
		}
		else if(transform.name.Equals("Uninstall"))
		{
//			transform.parent.GetComponent<TurretMenuScript>().Turret.GetComponent<TurretScript>().RemoveTurretMenu();
//			transform.parent.GetComponent<TurretMenuScript>().Turret.GetComponent<TurretScript>().Marker.GetComponent<MarkerScript>().UninstallTurret();
		}
		else if(transform.name.Equals("Upgrade"))
		{
			if(available)
			{
//				transform.parent.GetComponent<TurretMenuScript>().Turret.GetComponent<TurretScript>().RemoveTurretMenu();
//				transform.parent.GetComponent<TurretMenuScript>().Turret.GetComponent<TurretScript>().Marker.GetComponent<MarkerScript>().UpgradeTurret(transform.parent.GetComponent<TurretMenuScript>().Turret.GetComponent<TurretScript>().CurrentType, 
//																																						transform.parent.GetComponent<TurretMenuScript>().Turret.GetComponent<TurretScript>().Level, 
//																																						transform.parent.GetComponent<TurretMenuScript>().Turret.GetComponent<TurretScript>().BodyRotation);
			}
		}
	}
}
