using UnityEngine;
using System.Collections;

public class ActionMenuButtonScript : MonoBehaviour {
	
	private void OnMouseDown()
	{
		if(transform.name == "Agree")
		{
			transform.parent.GetComponent<ActionMenuScript>().setAnswer(true);
		}
		else
		{
			transform.parent.GetComponent<ActionMenuScript>().setAnswer(false);
		}
	}
}
