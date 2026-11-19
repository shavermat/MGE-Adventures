using UnityEngine;
using System.Collections.Generic;
public class HeavyScript : PlayerEntity
{
      [Header("Character Settings")]
    public int ArmorBoost = 10;
   public override float FirstAbility(List<Entity> targets)
    {
         foreach(Entity target in targets)
        {

        target.armor += ArmorBoost;

        }
        return firstAbilityDelay;
    }

    public override float SecondAbility(List<Entity> targets)
    {
         foreach(Entity target in targets)
        {

        target.DamageBlocked();

        }
        return secondAbilityDelay;
    }


     public override float ThirdAbility(List<Entity> targets)
    {
        foreach(Entity target in targets)
        {

        target.BreakArmor(3);
        target.TakeDamage((int)(Power / 1.5f));

        }
        return thirdAbilityDelay;
    }


        public override void PowerDisplayUpdate()
    {
        
      powerDisplayCount[0] = 10;
      powerDisplayCount[1] = Power;
      powerDisplayCount[2] = (int)(Power / 1.5f);
    }

}
