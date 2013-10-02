using UnityEngine;
using System.Collections;

public class RangeSphereScript : MonoBehaviour {
	
	private Material mat;
	private float speed = 0.5f;
	
	private void Awake()
	{
		Transform sphere = transform.FindChild("RangeSphere");
		mat = sphere.renderer.material;
	}
	
	public void SetRange(float turretRange)
	{
		float scale = turretRange * 2;
		transform.localScale = new Vector3(scale, scale, scale);
	}
	
	private void Update()
	{
		float offset = Time.time * speed;
		mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
	}
}
