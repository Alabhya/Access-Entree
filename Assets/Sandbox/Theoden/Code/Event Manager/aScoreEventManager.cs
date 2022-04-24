using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class aScoreEventManager : MonoBehaviour
{
	[Serializable]
	public class KeyValuePair
	{
		public string key;
		public bool val;
	}
	
	public static aScoreEventManager instance;
	[SerializeField] private List<KeyValuePair> events = new List<KeyValuePair>();
	private Dictionary<string, bool> eventsDict = new Dictionary<string, bool>();
	
	private void Awake()
	{
		instance = this; 
	}
	
	private void Start()
	{
		foreach (var kvp in events)
		{
			eventsDict[kvp.key] = kvp.val;
		}
	}
	
	public void SetEvent(string eventName, bool newVal)
	{
		if(eventsDict.ContainsKey(eventName))
			eventsDict[eventName] = newVal;
		else
			Debug.LogError("Cannot change "+ eventName + ". Event does not exist.");
	}
	
	public bool GetEvent(string eventName)
	{
		if(!eventsDict.ContainsKey(eventName))
		{
			Debug.LogError("Cannot get "+ eventName + " value. Event does not exist.");
			return(false);
		}
		return eventsDict[eventName];
	}
	
	//Event Listeners
}
