using UnityEngine;
using System.Collections;

public class TurretTeslaHeadScript : MonoBehaviour {
	
	public GameObject teslaRayPrefab;
	
	private GameObject ray;
	private LineRenderer rayRenderer;
	private Material rayMaterial;
	private float textureOffset = 0.125f;
	private float textureChangeCounter = 0;
	
	private float range;
	
	private Transform trans;
	private Transform target;
	private EnemyScript targetScript;
	
	private int damage;
	
	private void Awake()
	{
		trans = transform;
		ray = Instantiate(teslaRayPrefab) as GameObject;
		rayRenderer = ray.GetComponent<LineRenderer>();
		rayRenderer.SetPosition(0, trans.position);
		rayRenderer.SetPosition(1, trans.position);
		rayRenderer.SetWidth(.3f, .3f);
		
		rayMaterial = rayRenderer.renderer.material;
		rayMaterial.SetTextureScale("_MainTex", new Vector2(1, textureOffset));
	}
	
	public void Init(float range, int damage)
	{
		this.range = range;
		this.damage = damage;
	}
	
	private void Update()
	{
		if(target == null)
		{
			FindTarget();
			rayRenderer.SetPosition(1, trans.position);
		}
		else
		{
			LightningTarget();
		}
		
		if(target != null && Vector3.Distance(target.position, trans.position) > range)
		{
			target = null;
			rayRenderer.SetPosition(1, trans.position);
		}
	}
	
	private void FindTarget()
	{
		float distance = 100f;
		foreach(GameObject tempEnemy in Variables.enemies) {
			if(tempEnemy != null) {
				float tempDist = Vector3.Distance(trans.position, tempEnemy.transform.position);
				
				if(tempDist > range) 
				{
					continue;
				}
				if(tempDist < distance) 
				{
					distance = tempDist;
					target = tempEnemy.transform;
					targetScript = target.GetComponent<EnemyScript>();
				}
			}
		}
	}
	
	private void LightningTarget()
	{
		rayRenderer.SetPosition(1, target.position);
		if(textureChangeCounter < 0.02f) 
		{
			textureChangeCounter += Time.deltaTime;
		}
		else
		{
			rayMaterial.SetTextureOffset("_MainTex", new Vector2(0, rayMaterial.GetTextureOffset("_MainTex").y + textureOffset));
			textureChangeCounter = 0;
		}
		targetScript.damageEnemy(damage * Time.deltaTime, targetScript.Armor);
		if(targetScript.Health < 0)
		{
			target = null;
			rayRenderer.SetPosition(1, trans.position);
			textureChangeCounter = 0;
		}
	}
	
	private void OnDestroy()
	{
		target = null;
		Destroy(ray);
	}
}
