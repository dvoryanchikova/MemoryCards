using UnityEngine;
using System.Collections;

public class UIButton : MonoBehaviour {

	[SerializeField] private GameObject targetObject;
	[SerializeField] private string targetMessage;
	public Color highlightColor = Color.cyan;

	public void OnMouseOver(){
		SpriteRenderer sprite = GetComponent<SpriteRenderer> ();
		if(sprite!= null){
			sprite.color = highlightColor;
		}
	}
	public void OnMouseExit(){
		SpriteRenderer sprite = GetComponent<SpriteRenderer> ();
		if(sprite != null){
			sprite.color = Color.white;
		}
	}

	public void OnMouseDown(){
		transform.localScale = new Vector3 (0.28f, 0.28f, 0.28f);
	}
	public void OnMouseUp(){
		transform.localScale = new Vector3 (0.255f, 0.255f, 0.255f);
		if(targetObject != null){
			targetObject.SendMessage (targetMessage);
		}
	}

}
