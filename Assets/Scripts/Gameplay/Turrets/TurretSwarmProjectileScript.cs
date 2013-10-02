﻿using UnityEngine;
using System.Collections;

public class TurretSwarmProjectileScript : MonoBehaviour {
	
	private Transform turret;
	
	private float speed;
	private float rotationSpeed;
	private int damage;
	private int armorPiercing;
	private float range;
	
	private float startTargetingDelay;
	private float startSpeed;
	private bool targeting;
	
	Transform trans;
	
	public GameObject Target { get; private set; }
	
	private void Awake()
	{
		trans = transform;
		
		startSpeed = 6f;
		speed = 3f;
		rotationSpeed = 0.1f;
		startTargetingDelay = 0.3f;
	}
	
	public void Init(Transform turret, GameObject enemy, int damage, int armorPiercing)
	{
		Physics.IgnoreCollision(collider, turret.collider);
		this.turret = turret;
		Target = enemy;
		this.damage = damage;
		this.armorPiercing = armorPiercing;
	}
	
	private void Update()
	{
		if(startTargetingDelay > 0)
		{
			startTargetingDelay -= Time.deltaTime;
			trans.position += trans.forward * startSpeed * Time.deltaTime;
		}
		else if(Target != null)
		{
			float targetDistance = Vector3.Distance(trans.position, Target.transform.position);
			Vector3 target = (targetDistance < 0.6f)? Target.transform.position : Target.transform.position + new Vector3(0, 0.5f, 0);
			trans.rotation = Quaternion.Lerp(trans.rotation, Quaternion.LookRotation(target - trans.position), rotationSpeed);
			trans.position += trans.forward * speed * Time.deltaTime;
		}
		else
		{
			if(!FindTarget())
			{
				Explode();
			}
		}
	}
	
	private bool FindTarget()
	{
		float distance = 100f;
		foreach(GameObject tempEnemy in Variables.enemies)
		{
			if(tempEnemy != null)
			{
				float tempDist = Vector3.Distance(trans.position, tempEnemy.transform.position);
				if(tempDist < distance)
				{
					distance = tempDist;
					Target = tempEnemy;
					return true;
				}
			}
		}
		return false;
	}
	
	private void OnCollisionEnter(Collision collision)
	{
		GameObject other;
		foreach(ContactPoint point in collision.contacts)
		{
			other = point.otherCollider.gameObject;
			if(other.name.Contains("Enemy"))
			{
				other.GetComponent<EnemyScript>().damageEnemy(damage, armorPiercing);
				Explode();
				return;
			}
		}
	}
	
	private void Explode()
	{
		Destroy(gameObject);
	}
}