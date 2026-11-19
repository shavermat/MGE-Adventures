using UnityEngine;

public class BreakArmor : MonoBehaviour
{
    public int BreakCharge;
private Entity entity;

 public void Initialize(int charges)
    {
        entity = gameObject.GetComponent<Entity>();
      
        BattleManagerScript.onEnemyturnEnd += EveryTurn;
        BreakCharge = charges;
          entity.isBreakArmor?.Invoke(entity);
          entity.armor = 0;
    }
     public void AddCharge(int charges)
    {
        entity.isBreakArmor?.Invoke(entity);
        BreakCharge += charges;
    }


 private void EveryTurn()
    {
        if (BreakCharge > 0)
        {
        BreakCharge --;
         entity.isBreakArmor?.Invoke(entity);

        if (BreakCharge == 0)
        { 
            entity.armor = entity.BaseArmor;
        entity.isBreakArmor?.Invoke(entity);
         Destroy(this);
        }
        }

    }
    private void OnDestroy()
    {
    BattleManagerScript.onEnemyturnEnd -= EveryTurn;
    }
}
