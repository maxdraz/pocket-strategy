using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{

    public int energy = 3;
    public GameObject textDisplay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textDisplay.GetComponent<TextMesh>().text = energy.ToString();
    }
    //public void GiveEnergy(GameObject t)
    //{
     //   energy -= 1;
     //   t.GetComponent<Turret>().energy += 1;


    //              }
    

    
}
