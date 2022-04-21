using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTest : MonoBehaviour
{
    public void GarlandTest(string eventName)
	{
		aScoreEventManager.instance.SetEvent(eventName, true);
	}
}
