using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public float money;
    public Text moneyText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "Money: " + money.ToString();
    }

    public void AddMoney(float m)
    {
        money += m;
    }

    public void RemoveMoney(float m)
    {
        money -= m;
    }

    
}
