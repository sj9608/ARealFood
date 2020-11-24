using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodRotator : MonoBehaviour
{
    public GameObject objectToRotate;
    private PlacementIndicator placementIndicator; 

    void Start()
    {
        objectToRotate = GetComponent<GameObject>();
    }

    
    void Update()
    {
        // if(Input.touches[0] == Input.touches)
    }
}
