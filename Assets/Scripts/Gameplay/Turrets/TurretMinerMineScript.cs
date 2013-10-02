using UnityEngine;
using System.Collections;

public class TurretMinerMineScript : MonoBehaviour {
	
	private bool moveToExit = true;
	private float speed = .5f;
	
	private Transform turret;
	private float exitDistance;
	
	private Transform trans;
	
	private void Awake()
	{
		trans = transform;
	}
	
	public void Init(float exitDistance, Transform turret)
	{
//		Physics.IgnoreCollision(collider, turret.collider);
		this.exitDistance = exitDistance;
		this.turret = turret;
		Physics.IgnoreCollision(turret.collider, trans.collider);
	}
	
	private void Update()
	{
		if(moveToExit)
		{
//			float moveDist = speed * Time.deltaTime;
//			trans.position += trans.forward * moveDist;
//			exitDistance -= moveDist;
//			if(exitDistance < 0.05f)
//			{
//				moveToExit = false;
//				turret.GetComponent<TurretMinerScript>().MineInTurret = false;
//				Debug.Break();
//			}
		}
		else
		{
			
		}
	}
	
	private void FixedUpdate()
	{
		rigidbody.AddForce(Vector3.forward * 10);
	}
	
	private void OnDestroy()
	{
		if(turret != null)
		{
			turret.GetComponent<TurretMinerScript>().MineCount--;
		}
	}
}
