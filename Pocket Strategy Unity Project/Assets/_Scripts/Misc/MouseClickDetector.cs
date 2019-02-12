using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickDetector : MonoBehaviour
{
    public LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray,out hit, 10000f, mask)){
                Debug.Log(hit.collider.gameObject.name);
            }
        } 
    }
}
