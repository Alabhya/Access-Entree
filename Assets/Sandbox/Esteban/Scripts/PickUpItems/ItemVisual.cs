using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemVisual : MonoBehaviour
{
    public bool Red = true;
    public bool Blue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent blue cube at the transforms position
        if (Red)
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
        } else
        {
            Gizmos.color = new Color(0, 0, 1, 1);
        }

        Gizmos.DrawCube(transform.position, new Vector3(2, 2, 2));
    }
}
