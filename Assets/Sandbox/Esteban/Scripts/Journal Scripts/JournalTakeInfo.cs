using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalTakeInfo : MonoBehaviour
{
    public Text MyBioName;
    public Text MyBio;
    public Image MyBioImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Bio(string BioName, string BioText, Image BioImage)
    {
        MyBioName.text = BioName;
        MyBio.text = BioText;
        MyBioImage = BioImage;
    }

}
