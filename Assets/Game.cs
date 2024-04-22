using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject testSphere;

    public List<GameObject> myGameObjectList;   // Start is called before the first frame update
    void Start()
    {
        int randomIndex = Random.Range(0, myGameObjectList.Count);
        Instantiate(myGameObjectList[randomIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
