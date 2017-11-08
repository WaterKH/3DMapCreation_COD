using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Parser_BO3 : MonoBehaviour {

	public TextAsset textAsset;
	public const string modelName = "model";
	public const string origin = "origin";
	public const string targetname = "targetname";
	public const string classname = "classname";
	string[] stringSeparators = new string[] {"\n}\n{\n", "\r\n}\r\n{\r\n" };
	string[] dataSeparators = new string[] {"\n"};

	public ScrollDynamic scrollDyn;

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

					key = ss.Split(' ')[0];
					value = ss.Substring(key.Length);

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
						else if(value[value.Length - 2] == '\"' && value[1] == '\"')
						{
							value = value.Substring(2, value.Length - 4);
						}
					}
					//print(value);

					switch(key)
					{
					case modelName:
						node.GetComponent<UpdateNodeText>().nameID.text += value;
						break;
					case origin:
						node.GetComponent<UpdateNodeText>().origin.text += value;
						var xyzCoord = value.Split(' ');
						//print(value);
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
