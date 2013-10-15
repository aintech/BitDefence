using UnityEngine;
using System.Collections;

//TODO - спидер по идее летающий враг - ему надо придать силу - сопротивление гравитации
//TODO - если высота спидера не соответствует нужной применяем силу вниз соответственно высота-нужная высота-гравитация

public class EnemySpeederScript : EnemyScript {
	
	private void Awake()
	{
		healthBarOffset = new Vector3(0, 0.9f, 0);
		moneyDrop = 20;
	}
}
