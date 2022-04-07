using UnityEngine;
using UnityEngine.UI;

/* This script creates a generic journal element scriptable object that is designed to be used
   by a myriad of element types.
   
   This object is highly customizable and will need specific filler code in order to fill in the
   various element types properly.
*/
[CreateAssetMenu(menuName = "JournalEntry/JournalEntryObject")]
public class JournalEntryObject : ScriptableObject
{
	//Changeable elements
	[SerializeField] private Sprite mainPortrait;
	[SerializeField] private string objectName;
	[TextArea] [SerializeField] private string[] objectInfo;
	[SerializeField] private Icons[] objectIcons;
	//[SerializeField] private bool showProfile;
	[SerializeField] private string contEvt;
	
	//Getters
	public Sprite MainPortrait => mainPortrait;
	public string ObjectName => objectName;
	public string[] ObjectInfo => objectInfo;
	public Icons[] ObjectIcons => objectIcons;
	//public bool isEnabled => showProfile;
	public string ContEvt => contEvt;
}

[System.Serializable]
public class Icon : ISerializationCallbackReceiver
	{
		public Sprite iconImg;
		//public bool isEnabled;
		public string contEvt;
		
		//Needed for OnValidate to work
		void ISerializationCallbackReceiver.OnBeforeSerialize () {}
		void ISerializationCallbackReceiver.OnAfterDeserialize () {}
	}

[System.Serializable]
public class Icons : ISerializationCallbackReceiver
{	
	public string iconListTitle;
	public Icon[] iconList;
	
	//Needed for OnValidate to work
	void ISerializationCallbackReceiver.OnBeforeSerialize () {}
    void ISerializationCallbackReceiver.OnAfterDeserialize () {}
}