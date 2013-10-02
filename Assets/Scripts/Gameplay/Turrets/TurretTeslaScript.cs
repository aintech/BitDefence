using UnityEngine;
using System.Collections;

public class TurretTeslaScript : TurretScript {
	
	override public void Init()
	{
		damage = 5;
		shotRange = TeslaStartRange;
		Transform caster = body.FindChild("Caster");
		
		foreach(Transform child in caster)
		{
			if(child.name.Contains("Head"))
			{
				child.GetComponent<TurretTeslaHeadScript>().Init(shotRange, damage);
			}
		}
	}
}
