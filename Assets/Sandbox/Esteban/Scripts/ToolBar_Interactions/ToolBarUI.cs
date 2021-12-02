using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolBarUI : MonoBehaviour
{
    public List<GameObject> UIToolbar;
    #region Singleton
    private static ToolBarUI _instance;

    public static ToolBarUI Instance { get { return _instance; } }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        #region Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddItem(Sprite Image)
    {
        for(int i = 0; i < UIToolbar.Count; i++)
        {
            if (UIToolbar[i].GetComponent<Image>().sprite == null)
            {
                UIToolbar[i].GetComponent<Image>().sprite = Image;
                return;
            }
        }
    }
}
