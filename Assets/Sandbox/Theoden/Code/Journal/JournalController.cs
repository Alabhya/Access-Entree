using UnityEngine;
using UnityEngine.UI;

public class JournalController : MonoBehaviour
{
	private int pageIndex = 0;
	[SerializeField] private GameObject[] jPage, guiBtns;
	[SerializeField] private Color selectedBtnColor, normalColor;

    private void OnEnable() {
		ChangePage(0);
		guiBtns[0].GetComponent<Image>().color = selectedBtnColor;
	}

    //Start is called before the first frame update    
    void Start()
	{
		//Set starting page
		ChangePage(pageIndex);
	}
	
	public void JournalToggle()
	{
		//Turn on
		if(!gameObject.activeSelf)
		{
			//Animation
			//Show Journal
			gameObject.SetActive(true);
		}
		
		//Turn off
		else
		{
			//Animation
			//Hide Journal
			gameObject.SetActive(false);
		}
	}
	
	public void ChangePage(int newPage)
	{
		if(newPage != 0)
			guiBtns[0].GetComponent<Image>().color = normalColor;

		//Hide current page page
		jPage[pageIndex].SetActive(false);
		pageIndex = newPage;
		
		//Animation
		
		//Show new page
		jPage[newPage].SetActive(true);
	}
}
