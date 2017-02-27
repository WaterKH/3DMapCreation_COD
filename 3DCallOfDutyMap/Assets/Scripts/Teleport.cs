using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Teleport : MonoBehaviour {

	public Camera mainCamera;
	public CanvasGroup cameraGroup;

	public static bool canTeleport = false;

	string[] stringSeparators = new string[] {" ", ","};

	float x = 14;
	float y = 101.5f;
	float z = -271;

	public void TeleportTo(string value)
	{
		string[] coords = value.Split(stringSeparators, StringSplitOptions.None);

		var teleVec = new Vector3(float.Parse(coords[0]), float.Parse(coords[2]), float.Parse(coords[1]) + y);

		mainCamera.transform.position = teleVec;
		mainCamera.transform.LookAt(teleVec);
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.T) && !SearchBar.searching)
		{
			canTeleport = !canTeleport;
		}

		if(canTeleport)
		{
			cameraGroup.alpha = 1;
			cameraGroup.interactable = true;
			cameraGroup.blocksRaycasts = true;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		else if(!canTeleport && !ScrollDynamic.canScroll)
		{
			cameraGroup.alpha = 0;
			cameraGroup.interactable = false;
			cameraGroup.blocksRaycasts = false;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}
}
