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
			case 4: damage = 25; break;
			case 5: damage = 30; break;
		}
	}
	
	private void Update()
	{
		countReload();
		
		if(!loaded && enemy != null) FollowEnemy();
		else FindEnemy();
		
		if(loaded && enemy != null)
		{
			
			Vector3 enemyDirect = (enemy.position - body.position).normalized;
			float angle = Vector3.Angle(body.forward, enemyDirect);
			if(angle < 30f) {
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
			else
			{
				FollowEnemy();
			}
		}
	}
	
	private void LaunchProjectile()
	{
		if(siloIndex == silos.Count)
		{
			siloIndex = 0;
			loaded = false;
		}
		else
		{
			GameObject projectile = Instantiate(swarmProjectilePrefab, silos[siloIndex].position, silos[siloIndex].rotation) as GameObject;
			projectile.GetComponent<TurretSwarmProjectileScript>().Init(trans, enemy.gameObject, damage, armorPiercing);
			siloIndex++;
		}
	}
}
