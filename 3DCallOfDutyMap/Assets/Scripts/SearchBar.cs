using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SearchBar : MonoBehaviour {

	public ScrollDynamic scrollDyn;
	bool change = false;
	public static bool searching = false;

	public GameObject item;
	public GameObject parent;
	public GameObject notUsedParent;

	public void SearchContents(string value)
	{
		searching = true;
	}

	public void End(string value)
	{
		searching = false;

		var items = new List<GameObject>();
		var unusedItems = new List<GameObject>();

		foreach(var i in ScrollDynamic.scrollItems)
		{
			if(i.GetComponentInChildren<Text>().text.Contains(value))
			{
				items.Add(i);
			}
			else
			{
				unusedItems.Add(i);
			}
		}
        print("End search");
		UpdateView(items, unusedItems);
        print("Displaying search");
	}

	public void UpdateView(List<GameObject> data, List<GameObject> notUsedData)
	{
		if(data.Count > 0)
    	{
            print("Updating View");
	        RectTransform rowRectTransform = item.GetComponent<RectTransform>();
	        RectTransform containerRectTransform = parent.GetComponent<RectTransform>();
	   
	        //calculate the width and height of each child item.
	        float width = containerRectTransform.rect.width;
	        float ratio = width / rowRectTransform.rect.width;
	        float height = rowRectTransform.rect.height * ratio;
	        int rowCount = data.Count;
	        if (data.Count % rowCount > 0)
	            rowCount++;
	   
	        //adjust the height of the container so that it will just barely fit all its children
	        float scrollHeight = height * rowCount;
	        containerRectTransform.offsetMin = new Vector2(containerRectTransform.offsetMin.x, -scrollHeight / 2);
	        containerRectTransform.offsetMax = new Vector2(containerRectTransform.offsetMax.x, scrollHeight / 2);
	   
	        for (int i = 0; i < data.Count; i++)
	        {
				data[i].transform.SetParent(parent.transform, false);
	        }

	        for(int i = 0; i < notUsedData.Count; ++i)
	        {
	        	notUsedData[i].transform.SetParent(notUsedParent.transform, false);
	        }
        }
	}
}
