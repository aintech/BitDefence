using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretSwarmScript : TurretScript {
	
	public GameObject swarmProjectilePrefab;
	
	private Transform holder;
	private Transform launcher;
	private List<Transform> silos;
	
	private float launchDelay = 0.2f;
	private float launchDelayCounter = 0;
	private int siloIndex = 0;
	
	private bool launched = false;
	
	override public void Init ()
	{
		rotationSpeed = 5;
		shotRange = SwarmStartRange;
		reloadTime = 2;
		
		launcher = body.FindChild("Launcher");
		
		silos = new List<Transform>();
		
		foreach(Transform silo in launcher)
		{
			silos.Add(silo);
		}
		
		switch(Level)
		{
			case 1: damage = 10; break;
			case 2: damage = 15; break;
			case 3: damage = 20; break;
		}
	}
	
	private void Update()
	{
		countReload();
		
		if(!launched && enemy != null)
		{
			FollowEnemy();
		}
		else if(enemy == null)
		{
			FindEnemy();
		}
		
		if(loaded && !launched && enemy != null)
		{
			launched = true;
		}
		
		if(launched)
		{
			if(launchDelayCounter < launchDelay)
			{
				launchDelayCounter += Time.deltaTime;
			}
			else
			{
				launchDelayCounter = 0;
				LaunchProjectile();
			}
		}
	}
	
	private void LaunchProjectile()
	{
		if(siloIndex == silos.Count)
		{
			siloIndex = 0;
			loaded = false;
			launched = false;
		}
		else
		{
			GameObject projectile = Instantiate(swarmProjectilePrefab, silos[siloIndex].position, silos[siloIndex].rotation) as GameObject;
			projectile.GetComponent<TurretSwarmProjectileScript>().Init(trans, enemy.gameObject, damage, armorPiercing);
			siloIndex++;
		}
	}
}
