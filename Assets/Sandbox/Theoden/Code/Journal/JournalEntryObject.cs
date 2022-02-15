using UnityEngine;
using UnityEngine.UI;

//Create a Character Profile template type
[CreateAssetMenu(menuName = "JournalEntry/JournalEntryObject")]
public class JournalEntryObject : ScriptableObject
{
	//Changeable elements
	[SerializeField] private Sprite mainPortrait;
	[SerializeField] private string objectName;
	[TextArea] [SerializeField] private string[] objectInfo;
	[SerializeField] private Icons[] objectIcons;
	
	//Getters
	public Sprite MainPortrait => mainPortrait;
	public string ObjectName => objectName;
	public string[] ObjectInfo => objectInfo;
	public Icons[] ObjectIcons => objectIcons;
}

[System.Serializable]
public class Icons : ISerializationCallbackReceiver
{
	public string iconListTitle;
	public Sprite[] iconList;
	
	//Needed for OnValidate to work
	void ISerializationCallbackReceiver.OnBeforeSerialize () {}
    void ISerializationCallbackReceiver.OnAfterDeserialize () {}
}