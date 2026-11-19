using System.Collections.Generic;

using UnityEngine;

public class SoldierScript : PlayerEntity
{


    public override float FirstAbility(List<Entity> targets)
    {
       int doubleAttackChange = Random.Range(1, 100);
       if( doubleAttackChange <= 20) FirstAbility(targets);
         foreach (Entity target in targets)
        {
            if( target != null)
            {
             target.BreakArmor(1);
            target.TakeDamage(Power / 2);
            }
           
        }
        return firstAbilityDelay;
    }
     public override float SecondAbility(List<Entity> targets)
    {
        int doubleAttackChange = Random.Range(1, 100);
       if( doubleAttackChange <= 20) SecondAbility(targets);
         foreach (Entity target in targets)
        {
            if (target != null)
            {
             target.Burning(2, 40);
            target.TakeDamage(Power / 2);
            }
           
        }
        return firstAbilityDelay;
    }
     public override float ThirdAbility(List<Entity> targets)
    {
         int doubleAttackChange = Random.Range(1, 100);
       if( doubleAttackChange <= 20) ThirdAbility(targets);
         foreach (Entity target in targets)
        {
            if (target != null)
            {
             target.Stunning(2);
            target.TakeDamage(Power);
            }
           
        }
        return firstAbilityDelay;
    }







}
