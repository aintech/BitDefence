using UnityEngine;
using System.Collections;

//TODO - мины стандартно устанавливаются на некотором расстоянии от турели в сторону начала пути,
//а в TurretMenu добавляется контрольная кнопка, по которой можно установить зону в которой устанавливаются мины
//TODO - точку сбора мин можно ставить в радиусе shotRange

public class TurretMinerScript : TurretScript {
	
	public GameObject minePrefab;
	
	private Transform[] exits;
	
	private Transform closestExit;
	private float exitPointOffset = 1.2f;
	
	private Vector3 fieldCenter;
	private float fieldRange = 1;
	private float startingFieldOffset = 1;
	
	private int minesMax = 4;
	private float mineHalfHeight = 0.05f;
	
	public int MineCount { get; set; }
	public bool MineInTurret { get; set; }
	
	public override void Init ()
	{
		shotRange = MinerStartRange;
		reloadTime = 5;
		MineCount = 0;
		
		exits = new Transform[4]{trans.FindChild("ForwardExit"), 
								 trans.FindChild("BackExit"), 
								 trans.FindChild("LeftExit"), 
								 trans.FindChild("RightExit")};
		
		GameObject road = GameObject.Find("Road");
		Transform roadStartPoint = road.transform.FindChild("StartPoint").transform;
		
		Vector3 fieldDirection = new Vector3(roadStartPoint.position.x, trans.position.y, roadStartPoint.position.z) - trans.position;
		fieldCenter = trans.position + (fieldDirection.normalized * startingFieldOffset);
		
		GetExit();
		
//		Debug.DrawLine(trans.position, closestExit.position);
//		Debug.Break();
//		createMine();
	}
	
	private void Update()
	{
		countReload();
		if(loaded && MineCount < minesMax && !MineInTurret)
		{
			createMine();
		}
	}
	
	private void GetExit()
	{
		float distanceToField = 100;
		foreach(Transform exit in exits)
		{
			float dist = Vector3.Distance(fieldCenter, exit.position);
			if(dist < distanceToField)
			{
				distanceToField = dist;
				closestExit = exit;
			}
		}
	}
	
	private Vector3 GetRandomPositionForMine()
	{
		Vector2 randPos = Random.insideUnitCircle * fieldRange;
		return new Vector3(fieldCenter.x + randPos.x, fieldCenter.y, fieldCenter.z + randPos.y);
	}
	
	private void createMine()
	{
		Vector3 direction = Vector3.forward;
		if(closestExit == exits[0])
		{
			direction = Vector3.forward;
		}
		if(closestExit == exits[1])
		{
			direction = Vector3.back;
		}
		if(closestExit == exits[2])
		{
			direction = Vector3.left;
		}
		if(closestExit == exits[3])
		{
			direction = Vector3.right;
		}
		GameObject mine = Instantiate(minePrefab, closestExit.position + (Vector3.forward * 3), Quaternion.LookRotation(direction)) as GameObject;
		
//		Vector3 exitPoint = closestExit.position + (direction * exitPointOffset);
//		exitPoint += Vector3.up * mineHalfHeight;
//		Vector3 point = new Vector3(exitPoint.x, mineHeight, exitPoint.z);
		
//		Debug.DrawLine(closestExit.position, exitPoint);
		
		mine.GetComponent<TurretMinerMineScript>().Init(exitPointOffset, trans);
		MineCount++;
		MineInTurret = true;
		loaded = false;
		Debug.Break();
	}
}
