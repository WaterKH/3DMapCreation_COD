using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Traverse_UI : MonoBehaviour {

    static List<GameObject> data;
    public Camera main_camera;
    public Teleport teleport;
    public Text node;

    float x = 28.46f;
    float y = 131.14f;
    float z = -261.12f;
    int index = 0;
    public static bool traverse = true;
    bool isActive = false;

    void Start()
    {
        teleport.TeleportTo("0 0 0", x, y, z);
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.U))
        {
            traverse = !traverse;
        }

        if(traverse && isActive)
        {
            foreach (var d in data)
            {
                d.SetActive(false);
            }

            isActive = false;

            CalculateTeleport();
        }
        else if (traverse && !isActive)
        {
            isActive = false;

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                data[index].SetActive(false);
                --index;
                if (index < 0)
                {
                    index = data.Count - 1;
                }
                CalculateTeleport(index);
                //print("Move to decrement index: " + index);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                data[index].SetActive(false);
                ++index;
                if (index > data.Count - 1)
                {
                    index = 0;
                }
                CalculateTeleport(index);
                //print("Move to increment index: " + index);
            }
        }
        else if(!traverse && !isActive)
        {
            foreach(var d in data)
            {
                d.SetActive(true);
            }
            isActive = true;
        }
	}

    public void RandomNodeTeleport()
    {
        data[index].SetActive(false);
        int node_index = Random.Range(0, data.Count - 1);
        //print(node_index);
        CalculateTeleport(node_index);
    }

    public void CalculateTeleport(int anIndex = -1)
    {
        if (anIndex != -1)
        {
            index = anIndex;
        }

        var dest = data[index].GetComponentsInChildren<Text>();

        string origin = "";
        foreach (var item in dest)
        {
            if (item.name == "Origin")
            {
                origin = item.text;
                break;
            }
        }
        if (origin != "")
        {
            //print(origin.Substring("Location (Origin)[278]: ".Length));
            teleport.TeleportTo(origin.Substring("Location (Origin)[278]: ".Length), x, y, z);
        }

        data[index].SetActive(true);
        node.text = "Curr Node: " + index;
    }

    public static void SetData(List<GameObject> parsedData)
    {
        data = parsedData;

        foreach(var d in data)
        {
            d.SetActive(false);
        }

        data[0].SetActive(true);
        var dest = data[0].GetComponentsInChildren<Text>();
        
        foreach (var item in dest)
        {
            if (item.name == "Origin")
            {
                item.text += "0 0 0";
                break;
            }
        }
    }
}
