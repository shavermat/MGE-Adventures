using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
public class Engineer : PlayerEntity
{
    [Header("Character Settings")]
    private Transform turret;
     private int turretPower;
 [SerializeField] private int maxTurretPower;

     private int turretBasePower;

    [SerializeField] private GameObject turretIcon; // префаб
         private TextMeshProUGUI turretIconText;
         private GameObject newIcon;// экземпляр
         public Vector2 turretScaleAdd = new Vector2(0.1f, 0.1f);



   [SerializeField] private List<Entity> target = new List<Entity>();
    [SerializeField]private int currentTurnCount;
    [SerializeField] private int turretMaxTurnCount = 3;

    private BattleManagerScript bm;
    private  Canvas mainCanvas;
    void Awake()
    {
      turret =  transform.Find("Turret");
      
      turretBasePower = Power / 2;
      turretPower = turretBasePower;

      powerDisplayCount[0] = turretPower;
      powerDisplayCount[1] = turretPower * 2;
      powerDisplayCount[2] = Power;

      currentTurnCount = turretMaxTurnCount;
    }
    protected override void Start()
{
   mainCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();

  bm = GameObject.FindWithTag("BattleManager").GetComponent<BattleManagerScript>();



    base.Start();
    BattleManagerScript.onPlayerturnEnd += PlayerTurnEnd;

     newIcon = Instantiate(turretIcon, mainCanvas.transform);

        RectTransform rect = newIcon.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(-885.57f, 211);
        rect.localScale = Vector3.one;
        turretIconText = newIcon.GetComponentInChildren<TextMeshProUGUI>();

}



    public override float FirstAbility(List<Entity> targets)
    {
        if (turret.gameObject.activeSelf) // перезарядка
        { 
          currentTurnCount = turretMaxTurnCount; 
          turretIconText.text = turretMaxTurnCount.ToString();
           return 0;
        }
        target.Clear();
        turret.gameObject.SetActive(true);
       newIcon.SetActive(true);
       turretIconText.text = turretMaxTurnCount.ToString();
  

      target = targets;


        return firstAbilityDelay;
    }






   public override float SecondAbility(List<Entity> targets)
    {
         turretPower *= 2;

       if (turretPower < maxTurretPower)
      {
        turret.localScale += (Vector3)turretScaleAdd;
      }

       turretPower = Mathf.Clamp(turretPower, 0, maxTurretPower);  
         PowerDisplayUpdate();
        
        return secondAbilityDelay;
    }













    public void PlayerTurnEnd()
    {

      
        if (turret.gameObject.activeSelf)
        {
        if (currentTurnCount > 0)
        {
               target.RemoveAll(enemy => enemy == null);
          if (target.Count <= 0 || target == null || !target[0].gameObject.activeSelf)  target.Add( bm.enemyteam[0]);
          
           
            
            foreach (Entity enemy in target)
            {
              
            enemy.TakeDamage(turretPower);
              
            }
            currentTurnCount --;
            turretIconText.text = currentTurnCount.ToString();
        }
         else 
         {

          turret.gameObject.SetActive(false);
           newIcon.SetActive(false);
           currentTurnCount = turretMaxTurnCount;
           turretPower = turretBasePower;
           turretScaleAdd = new Vector2 (1, 1);
           newIcon.SetActive(false);


         }
        }
       
    }
    public override void PowerDisplayUpdate()
    {
        
      powerDisplayCount[0] = turretPower;
      powerDisplayCount[1] = turretPower * 2;
      powerDisplayCount[2] = Power;
      
    }

     void OnDestroy()
    {
        BattleManagerScript.onPlayerturnEnd -= PlayerTurnEnd;
    }
}
