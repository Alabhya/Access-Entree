using UnityEngine;
using UnityEngine.Events;

//Used to add functions as responses to dialogue
[System.Serializable]
public class ResponseEvent
{
	[HideInInspector] public string name;
	[SerializeField] private UnityEvent onPickedResponse;
	
	public UnityEvent OnPickedResponse => onPickedResponse;
}
