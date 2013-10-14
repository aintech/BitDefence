using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretFreezeHeadScript : MonoBehaviour {
	
	public GameObject freezeRayPrefab;
	
	private GameObject ray;
	private LineRenderer rayRenderer;
	
	private float range;
	private float rotationSpeed;
	
	private Transform trans;
	private Transform target;
	private EnemyScript targetScript;
	
	private float freezeRayId;
	
	private void Awake()
	{
		rotationSpeed = 3;
		trans = transform;
		freezeRayId = Random.Range(1, 90000);
		
		ray = Instantiate(freezeRayPrefab) as GameObject;
		rayRenderer = ray.GetComponent<LineRenderer>();
		rayRenderer.SetPosition(0, trans.position);
		rayRenderer.SetPosition(1, trans.position);
		rayRenderer.SetWidth(.05f, .05f);
	}
	
	public void InitRange(float range)
	{
		this.range = range;
	}
	
	private void Update()
	{
		if(target == null) 
		{
			FindTarget();
			rayRenderer.SetPosition(1, trans.position);
		}
		else FreezeTarget();
		
		if(target != null && Vector3.Distance(target.position, transform.position) > range)
		{
			target.GetComponent<EnemyScript>().Freeze = false;
			target.GetComponent<EnemyScript>().FreezeRayId = 0;
			target = null;
		}
	}
	
	private void FindTarget()
	{
		float distance = 100f;
		foreach(GameObject tempEnemy in Variables.enemies) {
			if(tempEnemy != null) {
				float tempDist = Vector3.Distance(trans.position, tempEnemy.transform.position);
				
				if(tempDist > range) continue;
				if(tempDist < distance && !tempEnemy.GetComponent<EnemyScript>().Freeze) 
				{
					distance = tempDist;
					target = tempEnemy.transform;
					targetScript = target.GetComponent<EnemyScript>();
					targetScript.FreezeRayId = freezeRayId;
				}
			}
		}
	}
	
	private void FreezeTarget()
	{
		trans.rotation = Quaternion.Lerp(trans.rotation, Quaternion.LookRotation(target.position - trans.position), rotationSpeed);
		Vector3 enemyDirection = (target.position - trans.position).normalized;
		float angle = Vector3.Angle(trans.forward, enemyDirection);
		if(angle < 10f && targetScript.FreezeRayId == freezeRayId) 
		{
			if(!targetScript.Freeze) targetScript.Freeze = true;
			rayRenderer.SetPosition(1, target.position);
		} 
		else if(targetScript.Freeze && targetScript.FreezeRayId != freezeRayId)
		{
			target = null;
			rayRenderer.SetPosition(1, trans.position);
		}
	}
	
	private void OnDestroy()
	{
		if(target != null && targetScript.FreezeRayId == freezeRayId)
		{
			targetScript.Freeze = false;
			targetScript.FreezeRayId = 0;
			target = null;
		}
		Destroy(ray);
	}
}
