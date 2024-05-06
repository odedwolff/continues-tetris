using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeRoot : MonoBehaviour
{
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
}
