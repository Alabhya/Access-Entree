using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public void DestroyGameObject()
    {
        DestroyImmediate(gameObject, true);
    }
	
	public void TestFunction()
	{
		Debug.Log("Test success I guess...");
	}
	
	public void TestFunction2()
	{
		Debug.Log("This is another test.");
	}
	
	public void TestFunction3()
	{
		Debug.Log("This however, is a test");
	}
}
