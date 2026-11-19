using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpyScript : PlayerEntity
{
    public int InvisibleDuration = 2;
    private String normalButtonName;
    public String ultButtonName;
    protected override void Start()
    {
        normalButtonName = ButtonsTexts[2];
        BattleManagerScript.onUIUpdate += ButtonUpdate;
        base.Start();
    }
  public  override float FirstAbility(List<Entity> target)
    {
     Invisibility buff = gameObject.AddComponent<Invisibility>();

        buff.InvisibleInitialize(InvisibleDuration);

        return firstAbilityDelay;
    }
     public  override float SecondAbility(List<Entity> target)
    {
         foreach(Entity enemy in target)
        {
            enemy.Stunning(1);
            enemy.BreakArmor(2);
            enemy.TakeDamage(Power / 2);

           Invisibility buff = enemy.AddComponent<Invisibility>();
           buff.InvisibleInitialize(1);


        }

        return secondAbilityDelay;
    }

    public override float ThirdAbility(List<Entity> targets)
    {
        foreach(Entity target in targets)
        {
            if(TryGetComponent<Invisibility>(out Invisibility component)) target.TakeDamage((int)(Power * 2));
            else target.TakeDamage(Power);
        }
        return thirdAbilityDelay;
    }



    public override void PowerDisplayUpdate()
    {
     
      powerDisplayCount[0] = 10;
      powerDisplayCount[1] = Power / 2;

       if(TryGetComponent<Invisibility>(out Invisibility component)) powerDisplayCount[2] = Power * 2;

       else  powerDisplayCount[2] = Power;
    }
       private void ButtonUpdate(PlayerEntity currentPlayer)
    {
        if (GetComponent<Invisibility>())
        { 
            ButtonsColor[2] = new Color(255, 70, 0);
            ButtonsTexts[2] = ultButtonName;
        }
        else
        {
         ButtonsColor[2] = new Color(255, 175, 0);
         ButtonsTexts[2] = normalButtonName;
        }
        
    }
}
