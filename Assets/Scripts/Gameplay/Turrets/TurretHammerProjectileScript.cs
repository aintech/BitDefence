using UnityEngine;
using System.Collections;

public class TurretHammerProjectileScript : MonoBehaviour {
	
	private Transform trans;
	private Transform launchPosition;
	private bool launched;
	private bool onStartPosition;
	private Transform turret;
	private Transform forwardPointer;
	
	private float moveCount = 0.1f;
	private float moveStep = 0.001f;
	
	private float prevYPos = 0;
	private bool falling;
	
	public void Init(Transform turret, Transform startPosition)
	{
		this.turret = turret;
		trans = transform;
		Physics.IgnoreCollision(transform.collider, turret.collider);
		launchPosition = startPosition;
		
		trans.position -= new Vector3(0, moveCount, 0);
	}
	
	private void Update()
	{
		if(!onStartPosition)
		{
			moveCount -= moveStep;
			trans.rotation = launchPosition.rotation;
			trans.position = launchPosition.position - trans.up * moveCount;
			if(moveCount <= 0)
			{
				onStartPosition = true;
				turret.GetComponent<TurretHammerScript>().ProjectileOnPosition = true;
			}
		}
		else if(!launched && onStartPosition)
		{
			trans.rotation = launchPosition.rotation;
			trans.position = launchPosition.position;
		}
		else if(!falling)
		{
			if(trans.position.y < prevYPos) falling = true;
			else prevYPos = trans.position.y;
		}
		if(falling)
		{
			trans.rotation = Quaternion.Lerp(trans.rotation, Quaternion.LookRotation(Vector3.down), 0.04f);
		}
	}
	
	public void Launch(float distance)
	{
		trans.rotation = launchPosition.rotation;
		trans.position = launchPosition.position;
		
		if(distance > 6) distance *= 0.85f;
		float estimatedDistance = distance + 1.9f;
		
		gameObject.rigidbody.isKinematic = false;
		gameObject.rigidbody.useGravity = true;
		gameObject.rigidbody.AddForce(trans.forward * estimatedDistance, ForceMode.Impulse);
		launched = true;
	}
	
	private void OnCollisionEnter(Collision collision)
	{
		if(!launched) return;
		foreach(ContactPoint point in collision)
		{
			if(point.otherCollider.gameObject.name.Contains("Enemy") || point.otherCollider.gameObject.name.Contains("Level"))
			{
//				Destroy(gameObject);
//				launched = false;
			}
		}
	}
}
