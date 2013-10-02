using UnityEngine;
using System.Collections;

public class TurretFreezeScript : TurretScript {
	
	override public void Init ()
	{
		shotRange = FreezeStartRange;
		Transform shoulder = body.FindChild("Shoulder");
		
		foreach(Transform child in shoulder)
		{
			if(child.name.Contains("Head"))
			{
				child.GetComponent<TurretFreezeHeadScript>().InitRange(shotRange);
			}
		}
	}
}
