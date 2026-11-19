using UnityEngine;
using System;



public class Entity : MonoBehaviour
{
     [Header("Base Settings")]
    public string unitName;
    public int maxHealth = 100;
    public int Power = 20;
    public int currentHealth;
    public bool isBoss;

  [Header("Armor")]
    public int BaseArmor;
    public int armor;
    public int maxArmor;
    private bool damageBlock;
    private GameObject Shield;

    public event Action<Entity> OnDeath;
    public event Action<Entity> onPlayerDamaged;
    public event Action<Entity> onPlayerHealing;
    public Action<Entity> isStun;
    public Action<Entity> isBurning;
    public Action<Entity> isBreakArmor;


    protected virtual void Start()
    {
        armor = BaseArmor;
        currentHealth = maxHealth;
    }

    public virtual void  TakeDamage(int damage)
    { 
    
        if(damageBlock)
        {
            currentHealth -= Mathf.CeilToInt((int)(damage * 0.25f));// Mathf.CeilToInt округляет в большую сторону

             currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            damageBlock = false;
            Destroy(Shield);
            return;
        }


        armor = Mathf.Clamp(armor, 0, maxArmor );
     int finalDamage = (int)(damage * (1f - (armor / 100f)));
        
        currentHealth -= finalDamage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        
        // Передаем ссылку на себя (this), а не через GetComponent
        onPlayerDamaged?.Invoke(this);

      

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        OnDeath?.Invoke(this);
        gameObject.SetActive(false);

    OnDeath = null;
    onPlayerDamaged = null;
    onPlayerHealing = null;
    isStun = null;
    isBurning = null;
    isBreakArmor = null;

        

    }

    public virtual void UseAbilities(Entity target)
    {
        target.TakeDamage(Power);
    }

    public void Healing(int healingPoint)
    {
        currentHealth += healingPoint;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        onPlayerHealing?.Invoke(this);
    }

    public void TriggerPlayerHealing()
    {
        onPlayerHealing?.Invoke(this);
    }

    public void PowerBuff(float buffCount)
    {
        Power = Mathf.RoundToInt(Power * buffCount);
    }



    public void DamageBlocked()
    {
        GameObject shieldPref = Resources.Load<GameObject>("Shield");

       Shield = Instantiate(shieldPref, transform.position, Quaternion.identity);
        damageBlock = true;

    }
    public void BreakArmor(int breakStuck)
    {
     if (!TryGetComponent<BreakArmor>(out BreakArmor Break))
        {
      BreakArmor breakArmor = gameObject.AddComponent<BreakArmor>();
      breakArmor.Initialize(breakStuck);
      }
      else Break.AddCharge(breakStuck);
    
    
    }
    public void Stunning(int Charges)
    {
        if (isBoss) return;
        if (!TryGetComponent<Stunning>(out Stunning stunn))
        {
      Stunning stun = gameObject.AddComponent<Stunning>();
      stun.Initialize(Charges);
      }
      else stunn.AddCharge(Charges);

    }


    public void AddArmor(int changeArmor)
    {
        armor += changeArmor;
    }
    public void Burning(int Charges, int fireDamage)
    {
        
        if (!TryGetComponent<Burning>(out Burning burning))
        {
      Burning Burning = gameObject.AddComponent<Burning>();
      Burning.Initialize(Charges, fireDamage);
      }
      else burning.AddCharge(Charges, fireDamage);
        
    }

   
            
        


   
    
}
