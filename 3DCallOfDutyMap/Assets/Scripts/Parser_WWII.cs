using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Parser_WWII : MonoBehaviour {

	public TextAsset textAsset;
	/*public const string typename = "58";
	public const string origin = "278";
	public const string targetname = "421";
	public const string classname = "357";
    */
    public const string origin = "278";
    public const string methodname = "421";
    public const string coderef = "58";
    public const string altcoderef = "262";
    public const string entityname = "357";
    public const string eventname = "33178";
    public const string altentityname = "33376";
    public const string spawner = "30";

    string[] stringSeparators = new string[] {"\n}\n{\n", "\r\n}\r\n{\r\n"};
	string[] dataSeparators = new string[] {"\n", "\r\n"};
    public static List<GameObject> parsedData;

    //public ScrollDynamic scrollDyn;


    void Awake()
	{
		print("Generating Nodes.");
		var data = textAsset.text.Split(stringSeparators, StringSplitOptions.None);
		print("Number of Nodes: " + data.Length);
		var dataLength = data.Length;
		data[0] = data[0].Substring(3);

		data[dataLength - 1] = data[dataLength - 1].Substring(0, data[dataLength - 1].Length - 1);

		var subData = new List<string[]>();

		foreach(var d in data)
		{
			//var temp = d.Split(dataSeparators, StringSplitOptions.None);
			//print(temp[0]);

			subData.Add(d.Split(dataSeparators, StringSplitOptions.None));	
		}

        parsedData = ParseData(subData);

        print("Generation Completed.");

        //scrollDyn.CreateListOfItems(parsedData);

        print("Condensing UI for easier viewing.");
        UI_Sorter.Condense_UI(parsedData);
        print("Condensing Completed.");

        Traverse_UI.SetData(parsedData);
	}

	public List<GameObject> ParseData(List<string[]> subData)
	{
		var listOfItems = new List<GameObject>();

		for(int i = 0; i < subData.Count; ++i)
		{
			GameObject node = Instantiate(Resources.Load("Node")) as GameObject;
			Vector3 position = Vector3.zero;

			foreach(var ss in subData[i])
			{
				if(ss.Length > 1)
				{
					//string[] kv;
					string key, value;

					key = ss.Trim().Split(' ')[0];
					value = ss.Trim().Substring(key.Length);
                    //print(key);
                    //print(value);
					if(key.Contains("\""))
					{
						key = key.Substring(1, key.Length - 2);
					}

					if(value.Length - 1 > 0)
					{
						if(value[value.Length - 1] == ',')
						{
							value = value.Substring(1, value.Length - 3);
						}
						else if(value[value.Length - 1] == '\"' && value[1] == '\"')
						{
							value = value.Substring(2, value.Length - 3);
						}
					}
					//print(value);

					switch(key)
					{
                    case origin:
                        node.GetComponent<UpdateNodeText_WWII>().origin.text += value;
                        var xyzCoord = value.Split(' ');
                        //print(value);
                        position = new Vector3(float.Parse(xyzCoord[0]), float.Parse(xyzCoord[2]), float.Parse(xyzCoord[1]));
                        break;
                    case methodname:
						node.GetComponent<UpdateNodeText_WWII>().methodname.text += value;
                        break;
                    case coderef:
                        node.GetComponent<UpdateNodeText_WWII>().coderef.text += value;
                        break;
                    case altcoderef:
                        node.GetComponent<UpdateNodeText_WWII>().altcoderef.text += value;
                        break;
                    case entityname:
                        node.GetComponent<UpdateNodeText_WWII>().entityname.text += value;
                        break;
                    case eventname:
                        node.GetComponent<UpdateNodeText_WWII>().eventname.text += value;
                        break;
                    case altentityname:
                        node.GetComponent<UpdateNodeText_WWII>().altentityname.text += value;
                        break;
                    case spawner:
                        node.GetComponent<UpdateNodeText_WWII>().spawner.text += value;
                        break;

                        default:
						break;
					}
				}
			}

			node.transform.position = position;

			listOfItems.Add(node);
		}

		return listOfItems;
	}
}
