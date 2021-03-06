﻿using UnityEngine;
using System.Collections;

public class RangeSphereScript : MonoBehaviour {
	
	private Material mat;
	private float speed = 0.5f;
	private float scaleAmount = 0.65f;
	
	private void Awake()
	{
		Transform sphere = transform.FindChild("RangeSphere");
		mat = sphere.renderer.material;
	}
	
	public void SetRange(float turretRange)
	{
		float scale = turretRange * scaleAmount;
		Debug.Log(transform.localScale);
		transform.localScale = new Vector3(scale, scale, scale);
		Debug.Log(transform.localScale);
	}
	
	private void Update()
	{
		float offset = Time.time * speed;
		mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
	}
}
