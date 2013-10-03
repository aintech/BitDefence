using UnityEngine;
using System.Collections;

public abstract class GUIScript : MonoBehaviour {
	
	protected Vector2 btnDimension = new Vector2(30, 30);
	protected float btnOffset = 3;
	
	private void Awake() {
		Variables.Reload();
		Init();
	}
	
	protected abstract void Init();
	
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Variables.Reload();
			Application.LoadLevel("LevelMenu");
		}
	}
}
