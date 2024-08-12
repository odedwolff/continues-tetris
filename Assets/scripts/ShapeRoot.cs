using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ShapeRoot : MonoBehaviour
{
    
    const  int LEFT = 1;
    const int RIGHT = 2;  

    const int COL_WID = 10;

    bool isActive = false;

    float lastXPos = 0;

    const float MOVE_THRESH = 0.2f;


    private Game gameManager = null;

    public Game GameManager{
        get{return gameManager; }
        set{gameManager = value; }
    }


    public bool IsActive{
        get{return isActive; }
        set{isActive = value; }
    }

    const float SLIDE_QUANTOM = 10.0f; 
    // Start is called before the first frame update
    void Start()
    {
            
            
        // Get all child Transforms
        foreach (Transform child in transform)
        {
            //Debug.Log("Child name: " + child.name);
            
            // You can perform actions on each child here
            (child.GetComponent<CubeElm>()).Parent = transform.GetComponent<ShapeRoot>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        float curXPos = transform.position.x;
        if(Math.Abs(curXPos- lastXPos) > MOVE_THRESH){
            Debug.Log("delta move detected!!");
            updateGridMap();
            lastXPos = curXPos;
        }

        if (isActive && Input.GetKeyDown(KeyCode.D))
        {
            //Debug.Log("UNPRENT ALL");
            UnparentAll();
            Destroy(gameObject);
        }

        if (isActive && Input.GetKeyDown(KeyCode.LeftArrow))
        {
           // Debug.Log("key left");
            slide(LEFT);
        }

        if (isActive && Input.GetKeyDown(KeyCode.RightArrow))
        {
           // Debug.Log("key right");
            slide(RIGHT);

        }

        if (isActive && Input.GetKeyDown(KeyCode.UpArrow))
        {
            //Debug.Log("Up key for rotate");
            rotate();
        }

        if (isActive && Input.GetKeyDown(KeyCode.P))
        {
            //Debug.Log("Up key for rotate");
            //PrintChildrenWorldPositions();
        }

        if (isActive && Input.GetKeyDown(KeyCode.R))
        {
            removeChildrenFromGridMap();
        }

        
    }





    void UnparentAll()
    {

        // Loop through all child indices (0 to childCount-1)
        for (int i = 0; i < transform.childCount; i++)
        {
            
            // Get all child Transforms
            Transform[] childTransforms = transform.GetComponentsInChildren<Transform>(true);

            // Loop through each child Transform and unparent it
            foreach (Transform childTransform in childTransforms)
            {
                if (childTransform != transform) // Avoid unparenting the parent itself
                {
                    childTransform.SetParent(null, true); // Unparent with world position preservation

                    childTransform.GetComponent<Rigidbody>().isKinematic = false;

                    //copy parent's speed
                    childTransform.gameObject.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity;

                    BoxCollider boxCollider = childTransform.GetComponent<BoxCollider>();
                    if (boxCollider != null)
                    {
                        boxCollider.enabled = true; // Initially disable the collider
                    }
                }
            }
        }
    }

    void slide(int direction){
        //Debug.Log("slide()");
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 currentVelocity = rb.velocity;
        float diff = direction == LEFT ? -SLIDE_QUANTOM: SLIDE_QUANTOM;
        rb.MovePosition(rb.position + new Vector3(diff, 0, 0));
        rb.velocity = currentVelocity;
    }

    
    void PrintChildrenWorldPositions()
    {

        GameObject parent = gameObject;
        Debug.Log("\n\nRoot position:" + parent.transform.position);
        Debug.Log("root calculate coloumn:" + calcCol(parent.transform.position.x));

        // Iterate through each direct child of the parent object
        foreach (Transform child in parent.transform)
        {
            // Print the child's position in world coordinates
            Debug.Log("Child: " + child.name + ", Loc Position: " + child.localPosition + "; glob pos:" + child.position + "clac col:" + calcCol(child.position.x));
        }
    }


    void addChildrenToGridDel(){
        GameObject parent = gameObject;
        Debug.Log("Root position:" + parent.transform.position);
        // Iterate through each direct child of the parent object
        foreach (Transform child in parent.transform)
        {
            // Print the child's position in world coordinates
            //Debug.Log("Child: " + child.name + ", Loc Position: " + child.localPosition + "; glob pos:" + child.position);
        }
    }

    int calcCol(float xPos){
        float originX = GameManager.instPos.position.x;
        int res = (int)Math.Floor ((xPos - originX)/ ((float)COL_WID) ) + 1 ;
        //Debug.Log("calcCol(), xPos=" + xPos + ";  orgX=" + originX + ";  xDiff" + (xPos - originX) +";  out=" + res);
        return res; 
    }



    void checkForOverLapping()
    {

    }

    void rotate(){
        transform.Rotate(0f, 0f, 90f);
    }


    public void addChildrenGridMap(){
        GameObject parent = gameObject;
        Dictionary<int, List<CubeElm>>gridMap = gameManager.ColsToCubes;
        foreach (Transform child in parent.transform)
        {
            // Print the child's position in world coordinates
            //Debug.Log("Child: " + child.name + ", Position: " + child.position);
            int column = calcCol(child.position.x);
            CubeElm cube = child.GetComponent<CubeElm>();
            if (gridMap.ContainsKey(column))
            {
                gridMap[column].Add(cube);
            }
            else
            {
               gridMap[column] = new List<CubeElm> { cube };
            }
            cube.ContainingColList = gridMap[column];
        }
    }

    public void removeChildrenFromGridMap(){
         GameObject parent = gameObject;
        Dictionary<int, List<CubeElm>>gridMap = gameManager.ColsToCubes;
        foreach (Transform child in parent.transform)
        {
            CubeElm cube = child.GetComponent<CubeElm>();
            cube.ContainingColList.Remove(cube);
        }
    }


    //call after a new position or rotation is in place 
    public void updateGridMap(){
        removeChildrenFromGridMap();
        addChildrenGridMap();
    }
}
