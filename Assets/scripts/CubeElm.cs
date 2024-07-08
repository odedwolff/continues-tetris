using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeElm : MonoBehaviour
{
    private  ShapeRoot parent;
    
    public ShapeRoot Parent
    {
        get { return parent; }
        set { 
            parent = value;
            Debug.Log("set parent " + parent);
         }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
