﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretPlasmaCutterScript : TurretScript
{
	public GameObject rayPrefab;
	
	private Transform holder;
	private List<Transform> guns;
	private List<LineRenderer> rayRenderers;
	
	private float rayLength = 100;
	private float rayOffset = 0.03f;
	private float rayLifetime = 1;
	private float rayLifeCount;
	private bool raysOnStage;
	private float rayMaxWidth = 0.3f;
	private float rayShrinkSpeed = 0.6f;
	private float rayWidth;
	
	public override void Init ()
	{
		rotationSpeed = 5;
		shotRange = PlasmaCutterStartRange;
		reloadTime = 2;
		damage = 20;
		
		holder = body.FindChild("Holder");
		GetGunsAndAddRays();
	}
	
	private void GetGunsAndAddRays()
	{
		guns = new List<Transform>();
		guns.Add(holder.FindChild("Gun_1"));
		guns.Add(holder.FindChild("Gun_2"));
		guns.Add(holder.FindChild("Gun_3"));
		
		rayRenderers = new List<LineRenderer>();
		for(int i = 0; i < guns.Count; i++)
		{
			GameObject ray = Instantiate(rayPrefab) as GameObject;
			rayRenderers.Add(ray.GetComponent<LineRenderer>());
		}
		
		SetRaysToDefault();
	}
	
	private void SetRaysToDefault()
	{
		raysOnStage = false;
		rayLifeCount = 0;
		rayWidth = rayMaxWidth;
		foreach(LineRenderer ray in rayRenderers)
		{
			ray.SetPosition(0, trans.position);
			ray.SetPosition(1, trans.position);
			ray.SetWidth(rayMaxWidth, rayMaxWidth);
			Color color = new Color(.5f, .5f, .5f, .5f);
			ray.SetColors(color, color);
		}
	}
	
	private void Update()
	{
		countReload();
		if(enemy == null)
		{
			FindEnemy();
		}
		else
		{
			FollowEnemy();
		}
		if(raysOnStage)
		{
			AnimateRays();
		}
	}
	
	protected override void FollowEnemy ()
	{
		base.FollowEnemy ();
		if(lookAtEnemy && loaded)
		{
			ShootRays();
		}
	}
	
	private void ShootRays()
	{
		for(int i = 0; i < guns.Count; i++)
		{
			Transform gun = guns[i];
			LineRenderer ray = rayRenderers[i];
			
			ray.SetPosition(0, (gun.position + (gun.forward * rayOffset)));
			ray.SetPosition(1, (gun.position + (gun.forward * rayLength)));
			
			RaycastHit[] rayHits = Physics.RaycastAll(gun.position, gun.forward);
			foreach(RaycastHit hit in rayHits)
			{
				if(hit.transform.name.Contains("Enemy"))
				{
					hit.transform.GetComponent<EnemyScript>().damageEnemy(damage, armorPiercing);
				}
			}
		}
		loaded = false;
		raysOnStage = true;
	}
	
	private void AnimateRays()
	{
		rayLifeCount += Time.deltaTime;
		if(rayLifeCount <= rayLifetime)
		{
			if(rayWidth > 0)
			{
				rayWidth -= (rayShrinkSpeed * Time.deltaTime);
			}
			else
			{
				rayWidth = 0;
			}
			
			foreach(LineRenderer ray in rayRenderers)
			{
				ray.SetWidth(rayWidth, rayWidth);
			}
		}
		else
		{
			SetRaysToDefault();
		}
	}
}
