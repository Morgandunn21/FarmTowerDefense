using UnityEngine;
using System.Collections;

public class CollectableBounce : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.name == "PlayerCharacter")
		{
			Destroy(gameObject);
		}
	}
}
