using UnityEngine;
using System.Collections;

public class TurretMenuScript : MonoBehaviour {

	public GameObject costTextPrefub;
	
	private GameObject costText;
	
	public GameObject Turret { get; set; }
	
	private Transform trans;
	
	private void Awake()
	{
		trans = transform;
		foreach(Transform childTrans in transform)
		{
			childTrans.gameObject.AddComponent<TurretMenuButtonScript>();
		}
		
		Vector3 costTextPos = new Vector3(trans.position.x, trans.position.y + 0.1f, trans.position.z);
		costText = Instantiate(costTextPrefub, costTextPos, Quaternion.identity) as GameObject;
	}
	
	public void InitCostText(int level)
	{
		foreach(Transform costTextTrans in costText.transform)
		{
			TextMesh textMesh = costTextTrans.GetComponent<TextMesh>();
			if(costTextTrans.name.Equals("UninstallCostText"))
			{
				textMesh.text = Turret.GetComponent<TurretScript>().TurretCost.ToString();
			}
			else if(costTextTrans.name.Equals("UpgradeCostText"))
			{
				if(level == 5)
				{
					costTextTrans.gameObject.SetActive(false);
					foreach(Transform childTrans in transform)
					{
						if(childTrans.name.Equals("Upgrade"))
						{
							childTrans.gameObject.SetActive(false);
						}
					}
				}
				else
				{
					textMesh.text = Turret.GetComponent<TurretScript>().UpgradeCost.ToString();
				}
			}
		}
		
		foreach(Transform childTrans in transform)
		{
			childTrans.GetComponent<TurretMenuButtonScript>().Init();
		}
	}
	
	public void RemoveTurretMenu()
	{
		Turret.GetComponent<TurretScript>().RemoveTurretMenu();
	}
	
	private void OnDestroy() 
	{
		if(costText != null)
		{
			Destroy(costText);
		}
	}
}
