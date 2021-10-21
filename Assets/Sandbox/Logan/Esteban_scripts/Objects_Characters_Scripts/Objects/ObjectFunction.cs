using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFunction : ObjectFunctionRules
{
    public enum Objects { None, Candle, Cat, Light}
    public Objects CurrentObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoAction()
    {
        switch (CurrentObject)
        {
            case Objects.Candle:
                Candle();
                break;
            case Objects.Cat:
                Cat();
                break;
            case Objects.Light:
                Light();
                break;
            case Objects.None:
                break;
        }
    }
}
