using UnityEngine;
using System.Collections;

public class GhostTurretScript : MonoBehaviour {
	
	private BuyTurretScript buyScript;
	private float radius;
	private Transform trans;
	
	public bool CanBePlaced { get; private set; }
	public TurretScript.TurretType type;
	
	private bool firstCheck = true;
	
	private void Awake()
	{
		trans = transform;
		radius = 0.7f;
	}
	
	public void Init(BuyTurretScript buyScript, TurretScript.TurretType type)
	{
		this.buyScript = buyScript;
		this.type = type;
		Variables.PutInElements(gameObject);
	}
	
	private void Update()
	{
		bool canPlace = true;
		Collider[] colls = Physics.OverlapSphere(trans.position, radius);
		foreach(Collider coll in colls)
		{
			if(coll.name != "Level")
			{
				canPlace = false;
			}
		}
		if(firstCheck || canPlace != CanBePlaced)
		{
			ChangeColor();
		}
	}
	
	private void ChangeColor()
	{
		if(CanBePlaced)
		{
			foreach(Transform child in trans)
			{
				child.renderer.material.color = Color.red;
			}
			CanBePlaced = false;
		}
		else
		{
			foreach(Transform child in trans)
			{
				child.renderer.material.color = Color.white;
			}
			CanBePlaced = true;
		}
		if(firstCheck)
		{
			firstCheck = false;
		}
	}
}
