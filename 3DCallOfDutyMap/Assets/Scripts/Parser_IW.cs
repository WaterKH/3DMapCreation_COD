using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Parser_IW : MonoBehaviour {

	public TextAsset textAsset;
	public const string nameID = "497";
	public const string altName = "638";
	public const string orientation = "65";
	public const string origin = "543";
	public const string origin_alt = "origin";
	public const string targetname = "822";
	public const string targetname_alt = "target_name";
	public const string classname = "157";
	public const string classname_alt = "classname";
	string[] stringSeparators = new string[] {"\n}\n{\n"};
	string[] dataSeparators = new string[] {"\n"};

	public ScrollDynamic scrollDyn;

	void Awake()
	{
		print("Generating Nodes.");
		var data = textAsset.text.Split(stringSeparators, StringSplitOptions.None);
		//print(data.Length);
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

		List<GameObject> parsedData = ParseData(subData);

		scrollDyn.CreateListOfItems(parsedData);

		print("Generation Completed.");
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
					string[] kv;
					string key, value;

					if(ss.Contains(":"))
					{
						kv = ss.Split(':');
						key = kv[0];
						value = kv[1];
					}
					else
					{
						key = ss.Split(' ')[0];
						value = ss.Substring(key.Length);
					}

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
					case origin_alt:
						node.GetComponent<UpdateNodeText>().origin.text += value;
						var xyzCoord_alt = value.Split(' ');
						//print(ss);
						position = new Vector3(float.Parse(xyzCoord_alt[0]), float.Parse(xyzCoord_alt[2]), float.Parse(xyzCoord_alt[1]));
						break;
					case targetname:
						node.GetComponent<UpdateNodeText>().localName.text += value;
						break;
					case targetname_alt:
						node.GetComponent<UpdateNodeText>().localName.text += value;
						break;
					case classname:
						node.GetComponent<UpdateNodeText>().type.text += value;
						break;
					case classname_alt:
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
