using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretAntiMaterielScript : TurretScript {
	
	public GameObject gunfirePrefab;
	public GameObject bulletPrefab;
	
	private Transform gun;
	private Transform barrel;
	
	private List<LineRenderer> gunfires;
	private float gunfireForwardOffset = 0.37f;
	private float gunfireWidth = 0.5f;
	private float gunfireSideOffset = 0.06f;
	private float gunfireSideForwardOffset = 0.34f;
	private float gunfireSideWidth = 0.25f;
	private float gunfireVisibleTime;
	private float gunfireVisibleMax = 0.01f;
	
	private float maxRecoil = 0.15f;
	private float recoilSpeed = 2;
	private float currentRecoil;
	private bool recoilAnimation = false;
	private bool recoilBack = false;
	
	override public void Init()
	{
		reloadTime = 2;
		rotationSpeed = 5;
		shotRange = AntiMaterialStartRange;
		gunfireVisibleTime = gunfireVisibleMax;
		
		gun = body.FindChild("Gun");
		barrel = gun.FindChild("Barrel");
		
		switch(Level)
		{
		case 1: damage = 30; armorPiercing = 1; break;
		case 2: damage = 45; armorPiercing = 2; break;
		case 3: damage = 60; armorPiercing = 3; break;
		case 4: damage = 75; armorPiercing = 4; break;
		case 5: damage = 90; armorPiercing = 5; break;
		}
		
		gunfires = new List<LineRenderer>();
		for(int i = 0; i < 3; i++)
		{
			LineRenderer fireRender = (Instantiate(gunfirePrefab) as GameObject).GetComponent<LineRenderer>();
			fireRender.SetPosition(0, trans.position);
			fireRender.SetPosition(1, trans.position);
			
			if(i == 0) fireRender.SetWidth(0.3f, 0.3f);
			else if(i == 1) fireRender.SetWidth(0.2f, 0.2f);
			else if(i == 2) fireRender.SetWidth(0.2f, 0.2f);
			
			gunfires.Add(fireRender);
		}
	}
	
	private void Update()
	{
		countReload();
		
		if(enemy == null ) FindEnemy();
		else FollowEnemy();
		
		if(recoilAnimation) PlayRecoilAnimation();
		
		if(gunfireVisibleTime < gunfireVisibleMax) FireGun();
		else HoldGun();
	}
	
	override protected void FollowEnemy()
	{
		base.FollowEnemy();
		if(lookAtEnemy)
		{
			gun.LookAt(enemy);
			if(loaded) Shoot();
		}
	}
	
	private void PlayRecoilAnimation()
	{
		float recoilOffset = recoilSpeed * Time.deltaTime;
		if(currentRecoil < maxRecoil && !recoilBack) {
			currentRecoil += recoilOffset;
			barrel.position -= barrel.forward * recoilOffset;
		} else {
			recoilBack = true;
			currentRecoil -= recoilOffset * .2f;
			barrel.position += barrel.forward * (recoilOffset * .2f);
		}
		if(currentRecoil < 0) {
			barrel.position += barrel.forward * currentRecoil;
			currentRecoil = 0;
			recoilAnimation = false;
			recoilBack = false;
		}
	}
	
	private void Shoot()
	{
		DamageEnemy();
		loaded = false;
		recoilAnimation = true;
		gunfireVisibleTime = 0;
	}
	
	private void FireGun()
	{
		gunfireVisibleTime += Time.deltaTime;
		
		Vector3 mainStartPosition = gun.position + (gun.forward * gunfireForwardOffset);
		Vector3 leftStartPosition = gun.position + (gun.forward * gunfireSideForwardOffset) - (gun.right * gunfireSideOffset);
		Vector3 rightStartPosition = gun.position + (gun.forward * gunfireSideForwardOffset) + (gun.right * gunfireSideOffset);
		
		gunfires[0].SetPosition(0, mainStartPosition);
		gunfires[0].SetPosition(1, mainStartPosition + (gun.forward * gunfireWidth));
		
		gunfires[1].SetPosition(0, leftStartPosition);
		gunfires[1].SetPosition(1, leftStartPosition - (gun.right * gunfireSideWidth));
		
		gunfires[2].SetPosition(0, rightStartPosition);
		gunfires[2].SetPosition(1, rightStartPosition + (gun.right * gunfireSideWidth));
	}
	
	private void HoldGun()
	{
		foreach(LineRenderer renderer in gunfires)
		{
			renderer.SetPosition(0, trans.position);
			renderer.SetPosition(1, trans.position);
		}
	}
	
	private void OnDestroy()
	{
		foreach(LineRenderer rend in gunfires)
		{
			if(rend != null && rend.gameObject != null) Destroy(rend.gameObject);
		}
	}
}
