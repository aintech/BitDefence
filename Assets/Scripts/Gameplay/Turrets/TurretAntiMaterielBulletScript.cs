using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretAntiMaterielBulletScript : MonoBehaviour {
	
	private Transform trans;
	private Vector3 startPosition;
	private float speed = 5;
	private float armorPiercing = 1;
	private int damage = 0;
	private List<int> enemyIds;
	
	private void Awake()
	{
		trans = transform;
		enemyIds = new List<int>();
	}
	
	public void Init(Transform turret, int damage)
	{
		this.damage = damage;
		startPosition = trans.position;
		Physics.IgnoreCollision(trans.collider, turret.collider);
	}
	
	private void Update()
	{
		trans.position += trans.forward * speed * Time.deltaTime;
		float distance = Vector3.Distance(startPosition, trans.position);
		if(distance > 20) Destroy(gameObject);
	}
	
	private void OnCollisionEnter(Collision collision)
	{
		foreach(ContactPoint point in collision)
		{
			if(point.otherCollider.gameObject.name == "Level")
			{
				Destroy(gameObject);
			}
			if(point.otherCollider.gameObject.name.Contains("Enemy"))
			{
				bool oldEnemy = false;
				GameObject enemy = point.otherCollider.gameObject;
				EnemyScript script = enemy.GetComponent<EnemyScript>();
				foreach(int id in enemyIds)
				{
					if(id == script.EnemyID) oldEnemy = true;
				}
				if(!oldEnemy)
				{
					enemyIds.Add(script.EnemyID);
					script.damageEnemy(damage, script.Armor);
					
					if(script.Health <= 0 || script.Armor <= armorPiercing)
					{
//						Debug.Log("Destroyed");
					}
					else
					{
						Destroy(gameObject);
					}
				}
			}
		}
	}
}
