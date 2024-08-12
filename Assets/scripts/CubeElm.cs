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

    private List<CubeElm> containingColList;
    public List<CubeElm> ContainingColList{
        set;
        get;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override string ToString(){
        return "CubElm Inst";
    }

}
