using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeUniter : MonoBehaviour
{

    public int testId;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("id= " + this.testId);
    }


    void testRemove(){
        Debug.Log("testRemove(), my testid= " + this.testId);
        //just a randoom valued indicating this is the brick to be removed, simulating the completion of a row 
        if(this.testId == 1){
            Debug.Log("SELF DESTRUCT!!");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)){
           // testRemove();
        }

    }
}
