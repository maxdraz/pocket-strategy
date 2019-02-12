using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{

    public int energy = 3;
    public Text energyText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       energyText.text = "Energy: " + energy.ToString();
    }
     public IEnumerator GiveEnergy(TurretFinal t, float cd)
    {
       if(energy < 1)
        {
            yield break;
        }
        else
        {
            energy -= 1;
            yield return new WaitForSeconds(cd);
            t.energy += 1;
        }


     }

    public IEnumerator TakeEnergy(TurretFinal t, float cd)
    {
        if (t.energy < 1)
        {
            yield break;
        }
        else
        {
            t.energy -= 1;
            yield return new WaitForSeconds(cd);
            energy += 1;
        }


    }


}
