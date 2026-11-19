using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public class PyroScript : PlayerEntity
{


    [Header("Character Settings")]
    public int Strike;
    public int StrikeForUlt;
    private String BaseButtonText;
    [SerializeField] private string ultimateName;
    [SerializeField] private int flameChargeCount;
 

   [SerializeField] private GameObject cumCanIcon;




   

    private BattleManagerScript bm;
    protected override void Start()
    {
        BaseButtonText =  ButtonsTexts[0];
    bm = GameObject.FindWithTag("BattleManager").GetComponent<BattleManagerScript>();
    BattleManagerScript.onUIUpdate += ButtonUpdate;
      base.Start();

    }
 public override float FirstAbility(List<Entity> targets)
    {
        if (Strike >= StrikeForUlt) // ульта
        {

            foreach(Entity target in bm.enemyteam.ToArray())
            {
             if (target.GetComponent<FireBoost>() != null) {target.TakeDamage(Power * 2); target.Burning(4, Power * 2);}

             else {target.TakeDamage(Power); target.Burning(3, Power);}
              
            }
            Strike -= StrikeForUlt;
        }

        else
        {
           

       foreach(Entity target in targets)
        {
            if (target.GetComponent<FireBoost>() != null){ target.TakeDamage(Power); target.Burning(3, Power);}

             else{ target.TakeDamage(Power / 2); target.Burning(2, Power / 2);}
               
            
            if (!target.gameObject.activeSelf) Strike += StrikeForUlt;
        }
          
        }
        Strike++;
          Strike = Mathf.Clamp(Strike, 0, 3);

         ButtonsTexts[0] = $"{BaseButtonText}: {Strike}/{StrikeForUlt}";
        return firstAbilityDelay;
    }


    public override float ThirdAbility(List<Entity> targets)
    {
       
       foreach(Entity enemy in targets)
        {
            if (enemy.TryGetComponent<FireBoost>(out FireBoost fireScript)) fireScript.fireBuffDirection = 3;
            else
            {
          FireBoost fireBuff = enemy.AddComponent<FireBoost>();
          fireBuff.fireBuffDirection = 3;
          fireBuff.cumCanIcon = cumCanIcon;
            }
        }
        return thirdAbilityDelay;

    }




   private void ButtonUpdate(PlayerEntity currentPlayer)
    {
        
        if (Strike >= StrikeForUlt) ButtonsTexts[0] = ultimateName;
    }
    public override void PowerDisplayUpdate()
    {
      if (Strike >= StrikeForUlt)powerDisplayCount[0] = Power;
      else powerDisplayCount[0] = (int)(Power * 0.60);
      powerDisplayCount[1] = Power;
      powerDisplayCount[2] = Power;
    }



}
