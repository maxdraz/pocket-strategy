using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPad : MonoBehaviour
{
    public GameObject turretPrefab;
    public float cost;
    public float costIncrement;
    GameObject gm;
    MoneyManager mm;
    public float cooldown;
    public GameObject notEnoughTextGO;

    private void Awake()
    {
        gm = GameObject.FindWithTag("GM");
        mm = gm.GetComponent<MoneyManager>();
    }

    private void OnMouseDown()
    {
        if(mm.money < cost)
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
        notEnoughTextGO.GetComponent<TextMesh>().text = "Cost: " + cost.ToString();
        yield return new WaitForSeconds(t);
        notEnoughTextGO.SetActive(false);
    }

    void SpawnTurret()
    {
        //take away money
        mm.RemoveMoney(cost);
        //instantiate turret GO
        GameObject turret = (GameObject)Instantiate(turretPrefab);
        turret.transform.position = transform.position;
        turret.transform.rotation = transform.rotation;

        //get all buildpads in scene and increment cost
        BuildPad[] buildpads= FindObjectsOfType<BuildPad>();
        foreach(BuildPad bp in buildpads)
        {
            bp.cost += costIncrement;
        }

        //destroy the buildpad
        Destroy(gameObject);
    }
}
