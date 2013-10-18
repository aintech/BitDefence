using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	
	private float speedMult = 0.05f;
	private float speed = 0;
	
	private float minDistance = 6;
	private float maxDistance = 15;
	
	private Transform trans;
	
	private CameraRestrict restrict = new CameraRestrict(10, -18, 24, -14);
	
	private void Awake()
	{
		trans = transform;
		SetSpeed();
	}
	
	private void Update() {
		cameraMovement();
	}
	
	private void SetSpeed()
	{
		speed = speedMult * trans.position.y;
	}
	
	private void cameraMovement()
	{
		Vector3 forward = new Vector3(trans.forward.x, 0, trans.forward.z).normalized;
		Vector3 right = new Vector3(trans.right.x, 0, trans.right.z).normalized;
		
		if(Input.GetKey(KeyCode.W)) {
			if(trans.position.x > restrict.PositiveX &&
			   trans.position.z < restrict.NegativeZ)
			{
				//right-upper corner restriction
			}
			else if(trans.position.x > restrict.PositiveX && 
					trans.position.z > restrict.NegativeZ)
			{
				trans.position += Vector3.back * speed;
			}
			else if(trans.position.z < restrict.NegativeZ)
			{
				trans.position += Vector3.right * speed;
			}
			else
			{
				trans.position += forward * speed;
			}
		}
		if(Input.GetKey(KeyCode.S)) {
			if(trans.position.x < restrict.NegativeX &&
			   trans.position.z > restrict.PositiveZ)
			{
				//left-down corner restriction
			}
			else if(trans.position.x < restrict.NegativeX && 
					trans.position.z < restrict.PositiveZ)
			{
				trans.position += Vector3.forward * speed;
			}
			else if(trans.position.z > restrict.PositiveZ)
			{
				trans.position += Vector3.left * speed;
			}
			else
			{
				trans.position -= forward * speed;
			}
		}
		if(Input.GetKey(KeyCode.A)) {
			if(trans.position.x > restrict.PositiveX &&
			   trans.position.z > restrict.PositiveZ)
			{
				//left-upper corner restriction
			}
			else if(trans.position.x < restrict.PositiveX &&
					trans.position.z > restrict.PositiveZ)
			{
				trans.position += Vector3.right * speed;
			}
			else if(trans.position.x > restrict.PositiveX)
			{
				trans.position += Vector3.forward * speed;
			}
			else
			{
				trans.position -= right * speed;
			}
		}
		if(Input.GetKey(KeyCode.D)) {
			if(trans.position.x < restrict.NegativeX &&
			   trans.position.z < restrict.NegativeZ)
			{
				//right-lower corner restriction
			}
			else if(trans.position.x > restrict.NegativeX &&
					trans.position.z < restrict.NegativeZ)
			{
				trans.position += Vector3.left * speed;
			}
			else if(trans.position.x < restrict.NegativeX)
			{
				trans.position += Vector3.back * speed;
			}
			else
			{
				trans.position += right * speed;
			}
		}
		
		if(Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			if(trans.position.y > minDistance)
			{
				trans.position += trans.forward;
				SetSpeed();
			}
		}
		if(Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			if(trans.position.y < maxDistance)
			{
				trans.position -= trans.forward;
				SetSpeed();
			}
		}
	}
	
	private class CameraRestrict
	{
		public float PositiveX { get; private set; }
		public float NegativeX { get; private set; }
		public float PositiveZ { get; private set; }
		public float NegativeZ { get; private set; }
		
		public CameraRestrict(float positiveX, float negativeX, float positiveZ, float negativeZ)
		{
			PositiveX = positiveX;
			NegativeX = negativeX;
			PositiveZ = positiveZ;
			NegativeZ = negativeZ;
		}
	}
}
