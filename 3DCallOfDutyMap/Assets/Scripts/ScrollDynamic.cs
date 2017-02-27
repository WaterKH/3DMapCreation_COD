using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
 
//https://forum.unity3d.com/threads/anybody-got-scrollrects-to-cooperate-with-dynamic-content.265600/
public class ScrollDynamic : MonoBehaviour {
 
    public GameObject item;
    bool firstPass = true;
    //public static int itemCount = 10, columnCount = 1;

    public static bool canScroll = false;
    public CanvasGroup scrollGroup;

    public static List<GameObject> scrollItems = new List<GameObject>();

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.I) && !SearchBar.searching)
		{
			canScroll = !canScroll;
		}

		if(canScroll)
		{
			scrollGroup.alpha = 1;
			scrollGroup.interactable = true;
			scrollGroup.blocksRaycasts = true;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		else if(!canScroll && !Teleport.canTeleport)
		{
			scrollGroup.alpha = 0;
			scrollGroup.interactable = false;
			scrollGroup.blocksRaycasts = false;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}

    public void CreateListOfItems (List<GameObject> data) 
    {
    	if(data.Count > 0)
    	{
	        RectTransform rowRectTransform = item.GetComponent<RectTransform>();
	        RectTransform containerRectTransform = gameObject.GetComponent<RectTransform>();
	   
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
	   
	        int j = 0;
	        for (int i = 0; i < data.Count; i++)
	        {
	            //this is used instead of a double for loop because itemCount may not fit perfectly into the rows/columns
	            if (i % 1 == 0)
	                j++;
	       
	            //create a new item, name it, and set the parent
	            GameObject newItem = Instantiate(item) as GameObject;

	            newItem.name = data[i].GetComponent<UpdateNodeText>().origin.text.Substring(8);
	            newItem.GetComponentInChildren<Text>().text = data[i].GetComponentInChildren<UpdateNodeText>().localName.text.Substring(6);
	           
				newItem.transform.SetParent(gameObject.transform, false);

				if(firstPass)
				{
					scrollItems.Add(newItem);
				}

	            //move and size the new item
	            RectTransform rectTransform = newItem.GetComponent<RectTransform>();
	       
	            float x = -containerRectTransform.rect.width / 2 + width * (i % 1);
	            float y = containerRectTransform.rect.height / 2 - height * j;
	            rectTransform.offsetMin = new Vector2(x, y);
	       
	            x = rectTransform.offsetMin.x + width;
	            y = rectTransform.offsetMin.y + height;
	            rectTransform.offsetMax = new Vector2(x, y);
	        }

	        firstPass = false;
        }
    }
}