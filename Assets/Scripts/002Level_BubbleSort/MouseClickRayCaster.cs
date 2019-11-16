using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickRayCaster : MonoBehaviour {

    public LayerMask clickMask;
    public Transform cubeTransform;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 clickPosition = -Vector3.one;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //dont need a z distance
            RaycastHit hit; //dont have to assign this as the raycast will assign this procedurally

            if (Physics.Raycast(ray, out hit, 100f, clickMask)) //need to add max distance, and layerMask
            {
                clickPosition = hit.point;
                cubeTransform.position = clickPosition;
            }
        }
    }
}
