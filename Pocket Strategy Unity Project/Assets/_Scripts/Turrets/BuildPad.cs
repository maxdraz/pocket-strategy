using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPad : MonoBehaviour
{
    public GameObject turretPrefab;    
    public float price;
    CostManager cm;
    GameObject gm;
    MoneyManager mm;
    public float cooldown;
    public GameObject notEnoughTextGO;

    private void Awake()
    {
        gm = GameObject.FindWithTag("GM");
        mm = gm.GetComponent<MoneyManager>();
        cm = gm.GetComponent<CostManager>();
    }

    private void Start()
    {
        price = cm.baseTurretCost;
    }

    private void OnMouseDown()
    {
        if(mm.money < price)
        {
            StartCoroutine(NotEnoughMoney(cooldown));
        }
        else
        {
            SpawnTurret();
        }
    }

    IEnumerator NotEnoughMoney(float t)
    {
        notEnoughTextGO.SetActive(true);
        notEnoughTextGO.GetComponent<TextMesh>().text = "Cost: " + price.ToString();
        yield return new WaitForSeconds(t);
        notEnoughTextGO.SetActive(false);
    }

    void SpawnTurret()
    {
        //take away money
        mm.RemoveMoney(price);
        cm.turretsOwned += 1;
        //instantiate turret GO
        GameObject turret = (GameObject)Instantiate(turretPrefab);
        turret.transform.position = transform.position;
        turret.transform.rotation = transform.rotation;

        //get all buildpads in scene and increment cost
        BuildPad[] buildpads= FindObjectsOfType<BuildPad>();
        foreach(BuildPad bp in buildpads)
        {
            bp.price = cm.CalculateNewTurretPrice();
        }

        //destroy the buildpad
        Destroy(gameObject);
    }

   
}
