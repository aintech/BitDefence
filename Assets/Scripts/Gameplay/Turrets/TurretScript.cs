using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Gun - "Пушка"
 * 3 - "Слабое место" - шанс, что данная пушка данной цели начнет наносить повышенный урон
 * 5 - "Критическое попадание" - шанс, что выстрел нанесет 5-ти кратный урон (или 10-кратный, если под воздействием "Слабое место")
 * 
 * Freeze - "Подавитель"
 * 3 - "Тотальное подавление" - шанс на некоторое время полностью обездвижить врага
 * 5 - "Зона подавления" - при "Тотальном подавлении" обездвиживает врага и всех, кто рядом
 * 
 * AntiMateriel - "Бронебой"
 * 3 - "Разлом" - шанс на некоторое время снизить показатель брони врага
 * 5 - "Критический разлом" - при "разломе" броня снижается навсегда
 * 
 * Hammer - "Подрывник"
 * 3 - "Напалм" - шанс, что враг будет подожжен и будет некоторое время получать урон
 * 5 - "Шипучий напалм" - все вокруг подоженного врага тоже получают урон, пока враг горит
 * 
 * Swarm - "Рой"
 * 3 - "Открывашка" - ракеты игнорируют какую-то часть брони врага
 * 5 - "Отчет о миссии" - шанс, что ракета нашла уязвимость врага, и теперь любые ракеты наносят ему повышенный урон
 * 
 * Tesla - "Тесла"
 * 3 - "Потеря ориентации" - шанс, что враг потеряет цель куда ему надо двигаться и начнет двигаться к рандомной точке
 * 5 - "Сбой цели" - враг не просто теряет ориентацию, а начнает считать, что его цель - начало пути, а не конец
 * 
 * PlasmaCutter - "Резак"
 * 3 - "Всплески луча" - в некоторых местах луча появляются локальные тепловые всплески - враги попавшие под всплески получают дополнительный урон
 * 						 //убойная сила луча не падает проходя через врагов (без этого улучшения - каждый враг на пути луча ослабляет его на 20%)
 * 						 //если цель уничтожена или показатель брони ниже определенного уровня - патрон летит дальше и не теряет своей убойной силы
 * 5 - "Перегретый луч" - локальные тепловые всплески луча при попадании во врага взрываются и наносят урон врагу и всем, кто вокруг
 * 						 //если враг не уничтожен выстрелом, то патрон взрывается нанося дополнительный урон и задевая ближайших врагов
 * 
 * Miner - "Минёр"
 * 3 - "Локаторы для мин" - мины бегут навстречу врагу
 * 5 - "реактивный движок" - мины умеют атаковать воздушные цели
 */

public abstract class TurretScript : MonoBehaviour {
	
	public GameObject turretMenuPrefab;
	public GameObject rangeSpherePrefab;
	
	public enum TurretType
	{
		Gun,
		Hammer,
		Swarm,
		Freeze,
		AntiMaterial,
		Tesla,
		PlasmaCutter,
		Miner
	}
	
	public static int GunCost = 100;
	public static int HammerCost = 100;
	public static int SwarmCost = 100;
	public static int FreezeCost = 100;
	public static int AntiMaterialCost = 100;
	public static int TeslaCost = 100;
	public static int PlasmaCutterCost = 100;
	public static int MinerCost = 100;
	
	public static int GunUpdateCost = 50;
	public static int HammerUpdateCost = 50;
	public static int SwarmUpdateCost = 50;
	public static int FreezeUpdateCost = 50;
	public static int AntiMaterialUpdateCost = 50;
	public static int TeslaUpdateCost = 50;
	public static int PlasmaCutterUpdateCost = 50;
	public static int MinerUpdateCost = 50;
	
	public static float GunStartRange = 4;
	public static float HammerStartRange = 8;
	public static float SwarmStartRange = 4;
	public static float FreezeStartRange = 4;
	public static float AntiMaterialStartRange = 4;
	public static float TeslaStartRange = 4;
	public static float PlasmaCutterStartRange = 4;
	public static float MinerStartRange = 4;
	
	private GameObject turretMenu;
	private GameObject rangeSphere;
	private bool turretMenuAppear = false;
	
	protected int damage;
	protected int armorPiercing;
	
	protected float rotationSpeed;
	protected float shotRange;
	protected float reloadTime;
	
	protected Transform enemy;
	protected Transform body;
	protected Transform trans;
	
	protected bool loaded;
	private float reloadCounter;
	
	private int baseTurretCost;
	
	protected bool lookAtEnemy;
	
	private Vector3 menuHeight = new Vector3(0, 1.2f, 0);
	
	public GameObject Marker { get; set; }
	public TurretType CurrentType { get; set; }
	public int UpgradeCost { get; set; }
	public int Level { get; set; }
	
	public int TurretCost {
		set { baseTurretCost = value; }
		get { return baseTurretCost + ((Level - 1) * UpgradeCost); }
	}
	
	public Quaternion BodyRotation
	{
		set { body.rotation = value; }
		get { return body.rotation; }
	}
	
	private void Awake()
	{
		reloadCounter = reloadTime;
		loaded = true;
		
		trans = transform;
		body = trans.FindChild("Body");
	}
	
	abstract public void Init();
	
	virtual protected void FollowEnemy()
	{
		if(Vector3.Distance(enemy.position, trans.position) > shotRange)
		{
			enemy = null;
			lookAtEnemy = false;
			return;
		}
		Vector3 enemyPosLessY = new Vector3(enemy.position.x, body.position.y, enemy.position.z);
		Vector3 enemyDirect = (enemyPosLessY - body.position);
		float angle = Vector3.Angle(body.forward, enemyDirect);
		if(angle < 10)
		{
			lookAtEnemy = true;
			body.LookAt(enemyPosLessY);
		}
		else
		{
			if(lookAtEnemy)
			{
				lookAtEnemy = false;
			}
			body.rotation = Quaternion.Lerp(body.rotation, Quaternion.LookRotation(enemyPosLessY - body.position), rotationSpeed * Time.deltaTime);
		}
	}
	
	protected void countReload()
	{
		if(!loaded)
		{
			if(reloadCounter < reloadTime)
			{
				reloadCounter += Time.deltaTime;
			}
			else
			{
				loaded = true;
				reloadCounter = 0;
			}
		}
	}
	
	protected bool FindEnemy()
	{
		float distance = 100f;
		foreach(GameObject tempEnemy in Variables.enemies)
		{
			if(tempEnemy != null)
			{
				float tempDist = Vector3.Distance(trans.position, tempEnemy.transform.position);
				if(tempDist > shotRange)
				{
					continue;
				}
				if(tempDist < distance)
				{
					distance = tempDist;
					enemy = tempEnemy.transform;
				}
			}
		}
		
		if(enemy != null)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	private void OnMouseDown()
	{
		if(!turretMenuAppear)
		{
			turretMenu = Instantiate(turretMenuPrefab, (trans.position + menuHeight), Quaternion.identity) as GameObject;
			turretMenuAppear = true;
			turretMenu.GetComponent<TurretMenuScript>().Turret = trans.gameObject;
			turretMenu.GetComponent<TurretMenuScript>().InitCostText(Level);
			
			rangeSphere = Instantiate(rangeSpherePrefab, trans.position, Quaternion.identity) as GameObject;
			rangeSphere.GetComponent<RangeSphereScript>().SetRange(shotRange);
		}
	}
	
	public void UninstallTurret()
	{
		Marker.GetComponent<MarkerScript>().UninstallTurret();
	}
	
	public void RemoveTurretMenu()
	{
		if(turretMenu != null)
		{
			Destroy(turretMenu);
			turretMenuAppear = false;
		}
		if(rangeSphere != null)
		{
			Destroy(rangeSphere);
		}
	}
	
	protected void DamageEnemy()
	{
		enemy.GetComponent<EnemyScript>().damageEnemy(damage, armorPiercing);
	}
}
