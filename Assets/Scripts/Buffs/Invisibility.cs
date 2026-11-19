
using UnityEngine;


public class Invisibility : MonoBehaviour
{
    private SpriteRenderer sp;
    private Entity entity;
 private int invisibleDuration;
 public void InvisibleInitialize(int Duration)
    {
        entity = GetComponent<Entity>();
        sp = GetComponent<SpriteRenderer>();
        BattleManagerScript.onEnemyturnEnd += EveryTurn;
        invisibleDuration = Duration;
         sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 0.3f);
    }

  private void EveryTurn()
    {
        if (invisibleDuration > 0)
        {
            entity.Healing(10);
           
            invisibleDuration --;
            if(invisibleDuration == 0)
            {
                sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1f);
                Destroy(this);
            }
        }
    }

}
