using System;
using UnityEngine;

public class FireBoost : MonoBehaviour
{
    
    [NonSerialized] public GameObject cumCanIcon;
    public int fireBuffDirection;

    void Start()
    {
         BattleManagerScript.onEnemyturnEnd += EveryTurn; 

        Transform iconManager = transform.Find("Effect Status/Icon Manager");
       cumCanIcon = Instantiate(cumCanIcon, iconManager);
    }

    private void EveryTurn()
    {
       fireBuffDirection --;
       if (fireBuffDirection <= 0)
        {
          
          Destroy(cumCanIcon);

         Destroy(this);
        }
    }

    private void OnDestroy()
    {
              BattleManagerScript.onEnemyturnEnd -= EveryTurn; 
    }
}
