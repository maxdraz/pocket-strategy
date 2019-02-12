using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{

    public Transform target;

    private void Start()
    {
        if(target == null)
        {
            target = Camera.main.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
    }
}
