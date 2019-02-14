using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostManager : MonoBehaviour
{

    public float baseTurretCost;
    public float baseEnergyCost;
    public float costMultiplier;
    public int turretsOwned = 3;
    public int energyOwned;

   public float CalculateNewTurretPrice()
    {
        float price = baseTurretCost * Mathf.Pow(costMultiplier, turretsOwned);
        
        return Mathf.RoundToInt(price);
    }

    public float CalculateNewEnergyPrice()
    {
        float price = baseEnergyCost * Mathf.Pow(costMultiplier, energyOwned);

        return Mathf.RoundToInt(price);
    }


}
