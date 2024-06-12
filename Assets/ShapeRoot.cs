using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class ShapeRoot : MonoBehaviour
{
    
    const  int LEFT = 1;
    const int RIGHT = 2;  

    const float SLIDE_QUANTOM = 20f; 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("UNPRENT ALL");
            UnparentAll();
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("key left");
            slide(LEFT);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("key right");
            slide(RIGHT);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Up key for rotate");
            rotate();
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
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 currentVelocity = rb.velocity;
        float diff = direction == LEFT ? -SLIDE_QUANTOM: SLIDE_QUANTOM;
        rb.MovePosition(rb.position + new Vector3(diff, 0, 0));
        rb.velocity = currentVelocity;
    }

    void rotate(){
        transform.Rotate(0f, 0f, 90f);
    }
}
