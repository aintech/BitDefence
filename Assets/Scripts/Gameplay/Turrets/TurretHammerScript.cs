using UnityEngine;
using System.Collections;

public class TurretHammerScript : TurretScript {
	
	public GameObject hammerProjectilePrefab;
	
	private GameObject projectile;
	
	private Transform launcher;
	private Transform launchPosition;
	
	private float launchAngle = 45;
	private float minDistance = 2;
	
	public bool ProjectileOnPosition { get; set; }
	
	override public void Init ()
	{
		rotationSpeed = 10f;
		shotRange = HammerStartRange;
		damage = 10;
		
		launcher = body.FindChild("Launcher");
		launchPosition = launcher.FindChild("StartPosition");
		
		ProjectileOnPosition = false;
		launcher.Rotate(-Vector3.right, launchAngle);
	}
	
	private void createProjectile()
	{
		projectile = Instantiate(hammerProjectilePrefab, trans.position, launcher.rotation) as GameObject;
		projectile.GetComponent<TurretHammerProjectileScript>().Init(trans, launchPosition);
	}
	
	private void Update()
	{
		if(loaded && projectile == null)
		{
			createProjectile();
		}
		if(enemy == null) SearchEnemy();
		else FollowEnemy();
	}
	
	private void SearchEnemy()
	{
		float distance = 100f;
		foreach(GameObject tempEnemy in Variables.enemies) {
			if(tempEnemy != null) {
				float tempDist = Vector3.Distance(trans.position, tempEnemy.transform.position);
				if(tempDist > shotRange || tempDist < minDistance) continue;
				if(tempDist < distance) {
					distance = tempDist;
					enemy = tempEnemy.transform;
				}
			}
		}
	}
	
	override protected void FollowEnemy()
	{
		float distance = Vector3.Distance(enemy.position, trans.position);
		if(distance > shotRange || distance < minDistance) {
			enemy = null;
			lookAtEnemy = false;
			return;
		}
		Vector3 enemyPosLessY = new Vector3(enemy.position.x, body.position.y, enemy.position.z);
		Vector3 enemyDirect = (enemyPosLessY - body.position);
		float angle = Vector3.Angle(body.forward, enemyDirect);
		if(angle < 10)
		{
			lookAtEnemy = true;
			body.LookAt(enemyPosLessY);
		}
		else
		{
			if(lookAtEnemy) lookAtEnemy = false;
			body.rotation = Quaternion.Lerp(body.rotation, Quaternion.LookRotation(enemyPosLessY - body.position), rotationSpeed * Time.deltaTime);
		}
		
		if(lookAtEnemy)
		{
			if(ProjectileOnPosition)
			{
				projectile.GetComponent<TurretHammerProjectileScript>().Launch(distance);
				projectile = null;
				ProjectileOnPosition = false;
			}
		}
	}
	
	private void OnDestroy()
	{
		if(projectile != null)
		{
			Destroy(projectile);
		}
	}
}
