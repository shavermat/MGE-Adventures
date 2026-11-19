using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoutScript : PlayerEntity
{
    public int doudgeChance;
    public TextMeshProUGUI doudgeText;
    private BattleManagerScript bm;

    protected override void Start()
    {
      bm = GameObject.FindWithTag("BattleManager").GetComponent<BattleManagerScript>();
        base.Start();
    }
    public override void TakeDamage(int damage)
    {
        int doudge = Random.Range(0, 100);
        if (doudge <= doudgeChance)
        {
            doudgeText.gameObject.SetActive(true);
           StartCoroutine(DoudgeTextOffRoutine());
        }

        else base.TakeDamage(damage);

        
    }
    IEnumerator DoudgeTextOffRoutine()
    {
        yield return new WaitForSeconds(0.7f);

        doudgeText.gameObject.SetActive(false);

    }

    public override float FirstAbility(List<Entity> targets)
    {
        foreach(Entity target in targets)
        {
            target.TakeDamage(Power);
        }
      Invisibility buff =  gameObject.AddComponent<Invisibility>();
      buff.InvisibleInitialize(2);
      
        
        return firstAbilityDelay;
    }

    public override float SecondAbility(List<Entity> targets)
    {
       Entity target = targets[0];
        int targetIndex = bm.enemyteam.IndexOf(targets[0]);

    switch (targetIndex)
    {
        case 0: 
        target.TakeDamage(Power / 2);
         target.Stunning(1);
          break;

        case 1:
         target.TakeDamage(Power);
          target.Stunning(2);
           break;

        case 2:
         target.TakeDamage(Power * 2);
          target.Stunning(3);
           break;

       
    }
        return secondAbilityDelay;
    }

    public override float ThirdAbility(List<Entity> targets)
    {
        foreach(Entity target in targets)
        {
            target.Healing(40);
            target.AddArmor(30);
        }
        return thirdAbilityDelay;
    }
}

