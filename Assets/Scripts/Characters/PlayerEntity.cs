
using UnityEngine;

using System.Collections.Generic;
using System;


public class PlayerEntity : Entity
{

   [NonSerialized] public List<int> powerDisplayCount = new List<int>{20, 20, 20};

   [Header("Abilities Delay")]
      public float firstAbilityDelay = 0.12f;
        public float secondAbilityDelay = 0.12f;
          public float thirdAbilityDelay = 0.12f;
   
     // Настройки кнопок теперь задаются в инспекторе Unity для каждого префаба отдельно
     [Header("Button options")]
    public Color[] ButtonsColor = { Color.red, Color.red, Color.red };
    public string[] ButtonsTexts = { "First Attack", "Second Attack", "Third Attack" };
    [SerializeField] private abilityType[] abilitiesTypes = new abilityType[3];
    public abilityType[] AbilitiesTypes => abilitiesTypes;


 public enum abilityType {Enemy, Player, massEnemy, massPlayer,}

 public String Passive = "None";

 public int playerIndex;

    void Awake()
    {
       unitName = GetComponent<CharacterData>().characterName;
    }




    public virtual float FirstAbility(List<Entity> targets)
    {
        
        foreach(Entity target in targets)
        {
         target.TakeDamage(Power);

        }
        return firstAbilityDelay;
 
    }    
     public virtual float SecondAbility(List<Entity> targets)
    {
        
        foreach(Entity target in targets)
        {
         target.TakeDamage(Power);

        }
        return secondAbilityDelay;
 
    }    
     public virtual float ThirdAbility(List<Entity> targets)
    {
        
        foreach(Entity target in targets)
        {
         target.TakeDamage(Power);

        }
        return thirdAbilityDelay;
 
    }    
    public virtual void PowerDisplayUpdate()
    {
        
      powerDisplayCount[0] = Power;
      powerDisplayCount[1] = Power;
      powerDisplayCount[2] = Power;
    }



}
