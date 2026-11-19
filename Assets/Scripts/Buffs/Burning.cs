using UnityEngine;

public class Burning : MonoBehaviour
{

public int fireCharge;
public int fireDamage;
private Entity entity;


    public void Initialize(int charges, int damage)
    {
        entity = gameObject.GetComponent<Entity>();
      
        BattleManagerScript.onEnemyturnEnd += EveryTurn;
        fireCharge = charges;
        fireDamage = damage;
          entity.isBurning?.Invoke(entity);
    }
     public void AddCharge(int charges, int damage)
    {

        entity.isBurning?.Invoke(entity);
        fireCharge += charges;
        if (damage > fireDamage) fireDamage = damage;
    }


 private void EveryTurn()
    {
        if (fireCharge > 0)
        {
         entity.TakeDamage(fireDamage);
        fireCharge --;
         entity.isBurning?.Invoke(entity);

        if (fireCharge == 0)
        { 
        entity.isBurning?.Invoke(entity);
         Destroy(this);
        }
        }

    }
    private void OnDestroy()
    {
    BattleManagerScript.onEnemyturnEnd -= EveryTurn;
    }

}


