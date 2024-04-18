using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject testSphere;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(testSphere);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
