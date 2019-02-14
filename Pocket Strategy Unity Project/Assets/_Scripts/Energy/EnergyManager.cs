using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{

    public int energy = 2;
    public Text energyText;
    public int maxEnergy = 2;

    public float upgradeCost;
    public float costIncrement;
    public Text upgradecostText;
    MoneyManager mm;
    public GameObject energyOrbPrefab;
    public GameObject energyOrbUsedPrefab;

    // Start is called before the first frame update
    void Start()
    {
        mm = GameObject.FindWithTag("GM").GetComponent<MoneyManager>();
        energy = maxEnergy;
        upgradecostText.text = "+1 Energy: Cost " + upgradeCost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        energyText.text = "Energy: " + energy.ToString() + "/" + maxEnergy.ToString(); 

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
            //instantiate energy orb
           GameObject energyOrbGO = (GameObject)Instantiate(energyOrbPrefab, transform.position, transform.rotation);
            EnergyOrb eorb = energyOrbGO.GetComponent<EnergyOrb>();
            eorb.target = t.gameObject.transform;
            eorb.CalculateSpeed(cd);
            
            //energyOrbGO.transform.position = transform.position;
           // EnergyOrb eo = energyOrbGO.GetComponent<EnergyOrb>();
           // StartCoroutine(eo.TravelTo(t.transform, cd));

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

            GameObject energyOrbUsedGO = (GameObject)Instantiate(energyOrbUsedPrefab, t.transform.position, t.transform.rotation);
            EnergyOrb eorb = energyOrbUsedGO.GetComponent<EnergyOrb>();
            eorb.target = transform;
            eorb.CalculateSpeed(cd);
            yield return new WaitForSeconds(cd);
            energy += 1;
        }


    }

    public void UpgradeEnergy()
    {
        if(upgradeCost > mm.money)
        {
            return;
        }
        else
        {
            mm.RemoveMoney(upgradeCost);
            maxEnergy += 1;
            energy += 1;
            upgradeCost += costIncrement;
            upgradecostText.text = "+1 Energy: Cost " + upgradeCost.ToString();
        }
        
    }


}
