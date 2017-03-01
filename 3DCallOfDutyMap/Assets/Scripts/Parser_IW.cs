using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Parser_IW : MonoBehaviour {

	public TextAsset textAsset;
	public const string nameID = "497";
	public const string altName = "638";
	public const string orientation = "65";
	//public string origin = "543";
	public const string origin = "origin";
	//public string localName = "822";
	public const string targetname = "target_name";
	//public string type = "157";
	public const string classname = "classname";
	string[] stringSeparators = new string[] {"\n},\n{\n"};
	string[] dataSeparators = new string[] {",\n"};

	public ScrollDynamic scrollDyn;

	void Awake()
	{
		var data = textAsset.text.Split(stringSeparators, StringSplitOptions.None);
		//print(data.Length);
		var dataLength = data.Length;
		data[0] = data[0].Substring(3);

		data[dataLength - 1] = data[dataLength - 1].Substring(0, data[dataLength - 1].Length - 1);

		var subData = new List<string[]>();

		foreach(var d in data)
		{
			//var temp = d.Split(dataSeparators, StringSplitOptions.None);

			subData.Add(d.Split(dataSeparators, StringSplitOptions.None));	
		}

		List<GameObject> parsedData = ParseData(subData);

		scrollDyn.CreateListOfItems(parsedData);
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
					var kv = ss.Split(':');
					string key = kv[0];
					string value = kv[1];

					if(key.Contains("\""))
					{
						key = key.Substring(1, key.Length - 2);
					}

					if(value[value.Length - 1] == ',')
					{
						value = value.Substring(1, value.Length - 3);
					}
					else
					{
						value = value.Substring(1, value.Length - 2);
					}

					switch(key)
					{
					case nameID:
						
						node.GetComponent<UpdateNodeText>().nameID.text += value;
						break;
					case altName:
						node.GetComponent<UpdateNodeText>().altName.text += value;
						break;
					case orientation:
						//node.GetComponent<UpdateNodeText>().nameID.text += value;
						break;
					case origin:
						node.GetComponent<UpdateNodeText>().origin.text += value;
						var xyzCoord = value.Split(' ');
						//print(ss);
						position = new Vector3(float.Parse(xyzCoord[0]), float.Parse(xyzCoord[2]), float.Parse(xyzCoord[1]));
						break;
					case targetname:
						node.GetComponent<UpdateNodeText>().localName.text += value;
						break;
					case classname:
						node.GetComponent<UpdateNodeText>().type.text += value;
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
