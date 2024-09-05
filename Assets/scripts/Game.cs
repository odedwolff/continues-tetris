using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;


public class Game : MonoBehaviour
{
    public GameObject testSphere;

    public Transform instPos;

    ShapeRoot activeShape = null; 

    public List<GameObject> prefabShapes;  

    public List<ShapeRoot> shapeInstances = new List<ShapeRoot>();


    public const int LEFT_MOST_COL = -8;

    public const int RIGHT_MOST_COL = 8;

    const float MAX_STRIPE_DIFF_Y = 10f;



    

    //private Dictionary<int, List<double>> gridMap = new Dictionary<int, List<double>>();


    private Dictionary<int, List<CubeElm>> colsToCubes = new Dictionary<int, List<CubeElm>>();
    public Dictionary<int, List<CubeElm>> ColsToCubes{
        get{return colsToCubes;}
        set{colsToCubes = value;}
    }


    void Start()
    {
        /* int randomIndex = Random.Range(0, myGameObjectList.Count);
        Instantiate(myGameObjectList[randomIndex]); */
    }

    public void InstantiateShape(){
        int randomIndex = UnityEngine.Random.Range(0, prefabShapes.Count);
        if(activeShape != null){
            activeShape.IsActive = false; 
        }
        activeShape = Instantiate(prefabShapes[randomIndex], instPos.position, instPos.rotation).GetComponent<ShapeRoot>();
        activeShape.IsActive = true;
        shapeInstances.Add(activeShape);

        activeShape.GameManager = this;
        activeShape.addChildrenGridMap();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            // Perform an action when space is pressed down (e.g., jump)
            Debug.Log("Destroy key pressed!");
        }

         if (Input.GetKeyDown(KeyCode.N))
        {
            // Perform an action when space is pressed down (e.g., jump)
           // Debug.Log("New Shap Inst key pressed!");
            InstantiateShape();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            printColToCube();
            //Debug.Log("Up key for rotate");
            //PrintChildrenWorldPositions();
        }
    }


    void printColToCube(){
        
        foreach (var kvp in colsToCubes)
        {
            //Debug.Log("Key" +  kvp.Key);
            string buf = "Key" +  kvp.Key + ": keys :";
            foreach (var item in kvp.Value)
            {
                //Debug.Log("{" + item + "}...");
                buf = buf + "{" + item + "}...";
            }
            Debug.Log("val:   " + buf);
        }
    }



    //returns the complete row, or null if there isn't one 
    List<CubeElm> isThereFullRow(){
        for(int i = LEFT_MOST_COL ; i <= RIGHT_MOST_COL ; i++){
            if(!colsToCubes.ContainsKey(i) || colsToCubes[i].Count== 0){
                return null;
            }
        }

        //it is sufficient to itereate over cubes in any colum, because a complete row must contin all coluomns...
        
         List<CubeElm> fullLine;
        //iterate first column
        foreach (CubeElm cube1 in colsToCubes[LEFT_MOST_COL])
        {
            fullLine = new List<CubeElm> ();
            bool foundInFristNCols = true;
            fullLine.Add(cube1);
            //iterate all other columns 
            for (int i = LEFT_MOST_COL + 1 ; foundInFristNCols && i <= RIGHT_MOST_COL; i++)
            {
                bool foundInCol = false;
                foreach (CubeElm cubeN in colsToCubes[i]){
                    if (!foundInCol && Math.Abs(cube1.transform.position.y - cubeN.transform.position.y) < MAX_STRIPE_DIFF_Y){
                        foundInCol = true;
                        fullLine.Add(cubeN);
                    }
                }
                if(!foundInCol){
                    foundInFristNCols = false;
                }
            }
            if(foundInFristNCols){
                return fullLine;
            }
        }
        return null;
    }



}
