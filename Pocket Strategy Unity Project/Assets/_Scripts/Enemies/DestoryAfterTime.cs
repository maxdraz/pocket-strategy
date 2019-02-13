using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryAfterTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DestroyAfterT(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(gameObject);
    }
}
