using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickDetector : MonoBehaviour
{
    public LayerMask mask;
    GameObject gm;
    EnergyManager em;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindWithTag("GM");
        em = gm.GetComponent<EnergyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //left click
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000f, mask))
            {

                
                Debug.Log("clicked turret");
                TurretFinal turret = hit.collider.GetComponentInParent<TurretFinal>();
                turret.numClicks += 1;
                if (turret.energy < turret.maxEnergy && turret.numClicks <= turret.maxEnergy && em.energy >= 1)
                {

                    StartCoroutine(em.GiveEnergy(turret, turret.energyCD));
                    
                }
                else
                {
                    return;
                }

            }
        }
            //right click
            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 1000f, mask))
                {

                    Debug.Log("right clicked turret");
                    TurretFinal turret = hit.collider.GetComponentInParent<TurretFinal>();
                
                    StartCoroutine(em.TakeEnergy(turret, turret.energyCD));
                }

            }
        }
    }


  

    


