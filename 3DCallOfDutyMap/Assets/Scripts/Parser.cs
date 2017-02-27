using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Parser : MonoBehaviour {

	public TextAsset textAsset;
	public string nameID = "497";
	public string altName = "638";
	public string orientation = "65";
	public string origin = "543";
	public string localName = "822";
	public string type = "157";
	string[] stringSeparators = new string[] {"\n}\n{\n"};

	public ScrollDynamic scrollDyn;

	void Awake()
	{
		var data = textAsset.text.Split(stringSeparators, StringSplitOptions.None);
		var dataLength = data.Length;
		data[0] = data[0].Substring(3);

		data[dataLength - 1] = data[dataLength - 1].Substring(0, data[dataLength - 1].Length - 1);

		var subData = new List<string[]>();

		foreach(var d in data)
		{
			subData.Add(d.Split('\n'));	
		}

		//ScrollDynamic.itemCount = subData.Count;
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
				string key = ss.Split(' ')[0];
				string value = ss.Substring(key.Length);
				if(value.Length - 3 > 0)
					value = value.Substring(2, value.Length - 3);

				switch(key)
				{
				case "497":
					node.GetComponent<UpdateNodeText>().nameID.text += value;
					break;
				case "638":
					node.GetComponent<UpdateNodeText>().altName.text += value;
					break;
				case "65":
					//node.GetComponent<UpdateNodeText>().nameID.text += value;
					break;
				case "543":
					node.GetComponent<UpdateNodeText>().origin.text += value;
					var xyzCoord = value.Split(' ');
					position = new Vector3(float.Parse(xyzCoord[0]), float.Parse(xyzCoord[2]), float.Parse(xyzCoord[1]));
					break;
				case "822":
					node.GetComponent<UpdateNodeText>().localName.text += value;
					break;
				case "157":
					node.GetComponent<UpdateNodeText>().type.text += value;
					break;
				default:
					break;
				}
			}

			node.transform.position = position;

			listOfItems.Add(node);
		}

		return listOfItems;
	}
}
