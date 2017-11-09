using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Teleport : MonoBehaviour {

    public Camera mainCamera;
    public CanvasGroup cameraGroup;

    public static bool canTeleport = false;

    string[] stringSeparators = new string[] { " ", "," };

    void Awake()
    {

    }

	public void TeleportTo(string value, float x = 0, float y = 0, float z = 0)
	{
		string[] coords = value.Split(stringSeparators, StringSplitOptions.None);

		var teleVec = new Vector3(float.Parse(coords[0]) + x, float.Parse(coords[2]) + y, float.Parse(coords[1]) + z);

		mainCamera.transform.position = teleVec;
		//mainCamera.transform.LookAt(teleVec);
        mainCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void TeleportTo(string value)
    {
        string[] coords = value.Split(stringSeparators, StringSplitOptions.None);

        var teleVec = new Vector3(float.Parse(coords[0]), float.Parse(coords[2]), float.Parse(coords[1]));

        mainCamera.transform.position = teleVec;
        //mainCamera.transform.LookAt(teleVec);
        mainCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
	{
		//if(Input.GetKeyDown(KeyCode.T))
		//{
		//	canTeleport = !canTeleport;
		//}

		/*if(canTeleport)
		{
			cameraGroup.alpha = 1;
			cameraGroup.interactable = true;
			cameraGroup.blocksRaycasts = true;

		}
		else if(!canTeleport)
		{
			cameraGroup.alpha = 0;
			cameraGroup.interactable = false;
			cameraGroup.blocksRaycasts = false;

		}*/
	}
}
