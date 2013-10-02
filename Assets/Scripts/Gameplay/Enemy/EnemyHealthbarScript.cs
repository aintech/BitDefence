using UnityEngine;
using System.Collections;

public class EnemyHealthbarScript : MonoBehaviour {
	
	private Transform trans;
	
	private void Awake()
	{
		trans = transform;
	}
	
	void Update ()
	{
		Vector3 dir = trans.position - Camera.main.transform.position;
		trans.rotation = Quaternion.LookRotation(dir);
	}
}
