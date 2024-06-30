using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject testSphere;

    public Transform instPos;

    ShapeRoot activeShape = null; 

    public List<GameObject> prefabShapes;  

    public List<ShapeRoot> shapeInstances = new List<ShapeRoot>();
    void Start()
    {
        /* int randomIndex = Random.Range(0, myGameObjectList.Count);
        Instantiate(myGameObjectList[randomIndex]); */
    }

    public void InstantiateShape(){
        int randomIndex = Random.Range(0, prefabShapes.Count);
        if(activeShape != null){
            activeShape.IsActive = false; 
        }
        activeShape = Instantiate(prefabShapes[randomIndex], instPos.position, instPos.rotation).GetComponent<ShapeRoot>();
        activeShape.IsActive = true;
        shapeInstances.Add(activeShape);
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
    }
}
