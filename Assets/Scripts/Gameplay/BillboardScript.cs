using UnityEngine;
using System.Collections;

public class BillboardScript : MonoBehaviour {
	
	public bool LookAway;
	private Transform trans;
	private Transform cameraTrans;
	
	private void Awake()
	{
		trans = transform;
		cameraTrans = Camera.main.transform;
	}
	
	void Update () {
		if(!LookAway)
		{
			trans.LookAt(cameraTrans.position);
		}
		else
		{
			Vector3 direction = trans.position - cameraTrans.position;
			trans.rotation = Quaternion.LookRotation(direction);
		}
	}
}
