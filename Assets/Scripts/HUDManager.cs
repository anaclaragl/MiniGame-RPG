using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public Text moneyTxt;
    public void UpdateMoney(){
        moneyTxt.text = "$ " + GameManager.instance.money.ToString();
    }

    public void BuyItem(int price){
        if(GameManager.instance.money > 0){
            GameManager.instance.money -= price;
            UpdateMoney();
        }
    }
}
