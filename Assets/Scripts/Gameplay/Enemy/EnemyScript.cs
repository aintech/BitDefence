using UnityEngine;
using System.Collections.Generic;

public class EnemyScript : MonoBehaviour {
	
	public enum EnemyType
	{
		Speeder
	}
	
	public GameObject EnemyHealthBar;
	private Transform healthBar;
	protected Vector3 healthBarOffset;
	
	private LineRenderer barRenderer;
	private Color fullBarColor = new Color(0, 0.3f, 0, 1);
	private Color hitBarColor = Color.green;
	private Color damagedBarColor = Color.yellow;
	private Color almostDestroyedBarColor = Color.red;
		
	private float barForwardOffset = 0.05f;
	private float barLeftOffset = 0.4f;
	private float barRightOffset = 0.4f;
	private float barHeight = 0.21f;
	
	private Transform trans;
	
	protected float speed;
	private float rotationSpeed = 10f;
	private float freezeModifier = 0.5f;
	private float maxHealth = 100;
	
	private Transform currLinePoint;
	
	protected int moneyDrop;
	
	public float Health { get; private set; }
	public Vector3 YOffset { get; set; }
	public GameObject FirstPoint { get; set; }
	public bool Freeze { get; set; }
	public float FreezeRayId { get; set; }
	public int Armor { get; set; }
	public int EnemyID { get; private set; }
	
	public void Init()
	{
		trans = transform;
		Health = maxHealth;
		currLinePoint = FirstPoint.transform;
		trans.LookAt(currLinePoint.position);
		
		healthBar = (Instantiate(EnemyHealthBar, trans.position + healthBarOffset, Quaternion.identity) as GameObject).transform;
		barRenderer = healthBar.GetComponent<LineRenderer>();
		barRenderer.SetWidth(barHeight, barHeight);
		barRenderer.SetColors(fullBarColor, fullBarColor);
		
		Freeze = false;
		EnemyID = Random.Range(1, 90000);
	}
	
	private void Update()
	{
		Vector3 pointVector = new Vector3(currLinePoint.position.x, YOffset.y, currLinePoint.position.z);
		Vector3 enemyPoint = new Vector3(trans.position.x, YOffset.y, trans.position.z);
		trans.rotation = Quaternion.Slerp(trans.rotation, Quaternion.LookRotation(pointVector - enemyPoint), (Freeze? rotationSpeed * freezeModifier : rotationSpeed) * Time.deltaTime);
		trans.position += trans.forward * (Freeze? speed * freezeModifier : speed) * Time.deltaTime;
		
		healthBar.position = trans.position + healthBarOffset;
		
		barRenderer.SetPosition(0, healthBar.position + (healthBar.forward * barForwardOffset) + (healthBar.right * barLeftOffset));
		barRenderer.SetPosition(1, healthBar.position + (healthBar.forward * barForwardOffset) - (healthBar.right * barRightOffset));
		
		float distance = Vector3.Distance(pointVector, enemyPoint);
		if(distance < 0.2f) {
			bool endPoint;
			Transform linePoint;
			nextPoint(out linePoint, out endPoint);
			if(endPoint)
			{
				DestroyEnemy();
				PassThrow();
			}
			else
			{
				currLinePoint = linePoint;
			}
		}
	}
	
	private void nextPoint(out Transform linePoint, out bool endPoint)
	{
		endPoint = false;
		linePoint = null;
		GameObject[] points = currLinePoint.GetComponent<RoadPointScript>().NextPoints;
		if(points.Length == 0)
		{
			endPoint = true;
			return;
		}
		else if(points.Length == 1) linePoint = points[0].transform;
		else
		{
			int pointIndex = Random.Range(0, points.Length);
			linePoint = points[pointIndex].transform;
		}
	}
	
	public void damageEnemy(float amount, int ignoreArmorAmount)
	{
		int ignoreArmor = Armor - ignoreArmorAmount;
		if(ignoreArmor < 0) ignoreArmor = 0;
		float damageAmount = amount - (ignoreArmor * 10);
		if(Health - damageAmount <= 0) {
			DestroyEnemy();
			Variables.Money += moneyDrop;
		} 
		else Health -= damageAmount;
		
		float ratio = Health / maxHealth;
		
		barRightOffset = (0.26f * ratio) - 0.13f;
		
		if(ratio > 0.95f) barRenderer.SetColors(fullBarColor, fullBarColor);
		else if(ratio > 0.7f) barRenderer.SetColors(hitBarColor, hitBarColor);
		else if(ratio > 0.2f) barRenderer.SetColors(damagedBarColor, damagedBarColor);
		else barRenderer.SetColors(almostDestroyedBarColor,almostDestroyedBarColor);
	}
	
	private void DestroyEnemy()
	{
		Variables.enemies.Remove(transform.gameObject);
		Destroy(healthBar.gameObject);
		Destroy(gameObject);
	}
	
	private void PassThrow()
	{
		Variables.Lives--;
		if(Variables.Lives == 0) Variables.GameOver = true;
	}
}
