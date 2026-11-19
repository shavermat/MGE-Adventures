using UnityEngine;

public class Stunning : MonoBehaviour
{

public int stunCharge;
private Entity entity;


    public void Initialize(int charges)
    {
        entity = gameObject.GetComponent<Entity>();
      
        BattleManagerScript.onEnemyturnEnd += EveryTurn;
        stunCharge = charges;
          entity.isStun?.Invoke(entity);
    }
     public void AddCharge(int charges)
    {
        entity.isStun?.Invoke(entity);
        stunCharge += charges;
    }


 private void EveryTurn()
    {
        if (stunCharge > 0)
        {
        stunCharge --;
         entity.isStun?.Invoke(entity);

        if (stunCharge == 0)
        { 
        entity.isStun?.Invoke(entity);
         Destroy(this);
        }
        }

    }
    private void OnDestroy()
    {
    BattleManagerScript.onEnemyturnEnd -= EveryTurn;
    }

}
