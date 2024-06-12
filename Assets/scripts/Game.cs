using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject testSphere;

    public Transform instPos;

    public List<GameObject> myGameObjectList;   // Start is called before the first frame update
    void Start()
    {
        /* int randomIndex = Random.Range(0, myGameObjectList.Count);
        Instantiate(myGameObjectList[randomIndex]); */
    }

    public void InstantiateShape(){
        int randomIndex = Random.Range(0, myGameObjectList.Count);
        Instantiate(myGameObjectList[randomIndex], instPos.position, instPos.rotation);
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
            Debug.Log("New Shap Inst key pressed!");
            InstantiateShape();
        }
    }
}
