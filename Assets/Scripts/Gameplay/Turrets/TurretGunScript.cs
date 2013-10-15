using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretGunScript : TurretScript
{
	
	public GameObject gunfirePrefab;
	
	private Transform leftGun;
	private Transform leftBarrelHolder;
	private Transform leftBarrel;
	private Transform rightGun;
	private Transform rightBarrelHolder;
	private Transform rightBarrel;
	private Transform pointer;
	
	private float barrelTurnSpeed = 10;
	private float currentBarrelRotation = 0;
	private float barrelRotationAmount = 120;
	
	private int gunsAmount = 1;
	
	private List<LineRenderer> gunfires;
	private float gunfireOffset = 0.78f;
	private float gunfireHeight = 0.6f;
	private float gunfireWidth = 1.5f;
	private float gunfireVisibleTime;
	private float gunfireVisibleMax = 0.02f;
	
	public override void Init ()
	{
		rotationSpeed = 10;
		shotRange = GunStartRange;
		gunfireVisibleTime = gunfireVisibleMax;
		
		pointer = body.FindChild("Pointer");
		leftGun = body.FindChild("LeftGun");
		leftBarrelHolder = leftGun.FindChild("LG_BarrelHolder");
		leftBarrel = leftBarrelHolder.FindChild("LG_Barrel");
		rightGun = body.FindChild("RightGun");
		if(rightGun != null)
		{
			gunsAmount = 2;
			rightBarrelHolder = rightGun.FindChild("RG_BarrelHolder");
			rightBarrel = rightBarrelHolder.FindChild("RG_Barrel");
		}
		
		if(Level == 5)
		{
			barrelRotationAmount = 60;
		}
		
		switch(Level)
		{
			case 1: damage = 2; break;
			case 2: damage = 2; break;
			case 3: damage = 4; break;
			case 4: damage = 4; break;
			case 5: damage = 4; break;
		}
		
		gunfires = new List<LineRenderer>();
		for(int i = 0; i < gunsAmount; i++)
		{
			LineRenderer fireRender = (Instantiate(gunfirePrefab) as GameObject).GetComponent<LineRenderer>();
			fireRender.SetPosition(0, trans.position);
			fireRender.SetPosition(1, trans.position);
			fireRender.SetWidth(gunfireHeight, gunfireHeight);
			gunfires.Add(fireRender);
		}
	}
	
	private void Update()
	{
		if(!loaded)
		{
			TurnBarrel();
		}
		
		if(enemy == null)
		{
			FindEnemy();
		}
		else
		{
			FollowEnemy();
		}
		
		if(gunfireVisibleTime < gunfireVisibleMax)
		{
			FireGuns();
		}
		else
		{
			HoldFire();
		}
	}
	
	private void TurnBarrel()
	{
		if(currentBarrelRotation < barrelRotationAmount)
		{
			leftBarrel.Rotate(Vector3.forward, barrelTurnSpeed);
			if(rightGun != null) rightBarrel.Rotate(Vector3.forward, barrelTurnSpeed);
			currentBarrelRotation += barrelTurnSpeed;
		}
		if(currentBarrelRotation >= barrelRotationAmount)
		{
			leftBarrel.Rotate(Vector3.forward, barrelRotationAmount - currentBarrelRotation);
			if(rightGun != null) rightBarrel.Rotate(Vector3.forward, barrelRotationAmount - currentBarrelRotation);
			currentBarrelRotation = 0;
			loaded = true;
		}
	}
	
	private void FireGuns()
	{	
		gunfireVisibleTime += Time.deltaTime;
		
		Vector3 leftStartPosition = leftGun.position + (leftGun.forward * gunfireOffset) - (leftGun.up * 0.014f);
		
		gunfires[0].SetPosition(0, leftStartPosition);
		gunfires[0].SetPosition(1, leftStartPosition + (leftGun.forward * gunfireWidth));
		
		if(rightGun != null)
		{
			Vector3 rightStartPosition = rightGun.position + (rightGun.forward * gunfireOffset) - (rightGun.up * 0.014f);
			gunfires[1].SetPosition(0, rightStartPosition);
			gunfires[1].SetPosition(1, rightStartPosition + (rightGun.forward * gunfireWidth));
		}
	}
	
	private void HoldFire()
	{
		foreach(LineRenderer renderer in gunfires)
		{
			renderer.SetPosition(0, trans.position);
			renderer.SetPosition(1, trans.position);
		}
	}
	
	override protected void FollowEnemy()
	{
		base.FollowEnemy();
		if(lookAtEnemy && loaded)
		{
			pointer.LookAt(enemy);
			Shoot();
		}
	}
	
	private void Shoot()
	{
		leftGun.rotation = pointer.rotation;
		if(rightGun != null) rightGun.rotation = pointer.rotation;
		
		DamageEnemy();
		loaded = false;
		gunfireVisibleTime = 0;
	}
	
	private void OnDestroy()
	{
		foreach(LineRenderer rend in gunfires)
		{
			if(rend != null && rend.gameObject != null)
			{
				Destroy(rend.gameObject);
			}
		}
	}
}
