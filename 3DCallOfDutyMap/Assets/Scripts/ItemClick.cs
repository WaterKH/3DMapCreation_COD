using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClick : MonoBehaviour {

	Teleport tele;
	
	void Awake()
	{
		tele = GameObject.FindGameObjectWithTag("Teleport").GetComponent<Teleport>();
	}

	public void OnClickItem(GameObject item)
	{
		tele.TeleportTo(item.name);
	}
}
