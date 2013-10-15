using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	
	private float speedMult = 0.05f;
	private float speed = 0;
	
	private float minDistance = 6;
	private float maxDistance = 15;
	
	private Transform trans;
	
	private void Awake()
	{
		trans = transform;
		SetSpeed();
	}
	
	private void Update() {
		Vector3 forward = new Vector3(trans.forward.x, 0, trans.forward.z).normalized;
		Vector3 right = new Vector3(trans.right.x, 0, trans.right.z).normalized;
		
		if(Input.GetKey(KeyCode.W)) {
			trans.position += forward * speed;
		}
		if(Input.GetKey(KeyCode.S)) {
			trans.position -= forward * speed;
		}
		if(Input.GetKey(KeyCode.A)) {
			trans.position -= right * speed;
		}
		if(Input.GetKey(KeyCode.D)) {
			trans.position += right * speed;
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
	
	private void SetSpeed()
	{
		speed = speedMult * trans.position.y;
	}
}
