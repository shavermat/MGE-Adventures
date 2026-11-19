using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;



public class LevelManagerScript : MonoBehaviour
{
    
[Header("Levels Data")]
 private List<LevelConfigSO> allLevels = new List<LevelConfigSO>();
[SerializeField] private List<GameObject> spawnPoints;
[SerializeField] private Button nextButton;
[SerializeField] private Button BackButton;
[SerializeField] private TextMeshProUGUI endText;
private Wallet wallet;

private List<Entity> SpawnedEnemies = new List<Entity>();
private LocationConfig location;

private int currentLevelIndex = 0;
private BattleManagerScript bm;


private void Start()
    {
       
       
        location = GetComponent<LocationSelector>().SelectedLocation;
       
       allLevels.AddRange(location.levels);
        wallet = GetComponent<Wallet>();
        bm = GetComponent<BattleManagerScript>();
        LoadLevel(currentLevelIndex);
        bm.end += EndCheked;
       

    }
public void LoadLevel(int index)
    {
        if (SpawnedEnemies != null) 
        {
            SpawnedEnemies.Clear();
        }


        
        if (index < 0 || index >= allLevels.Count) return;
        currentLevelIndex = index;

        LevelConfigSO currentLevel = allLevels[currentLevelIndex];

       int i = 0;
      foreach(GameObject enemypref in currentLevel.enemyprefabs)
      {
         GameObject obj;
         if (enemypref.GetComponent<Entity>().isBoss)
         {
             obj = Instantiate(enemypref, spawnPoints[1].transform.position, Quaternion.identity);
         }
         else{   obj = Instantiate(enemypref, spawnPoints[i].transform.position, Quaternion.identity);}
         
        Entity objEntity = obj.GetComponent<Entity>();

        SpawnedEnemies.Add(objEntity);
        
        objEntity.TriggerPlayerHealing();
         i++; 
      }
      bm.InitializeEnemyTeam(SpawnedEnemies);

    }

     private void EndCheked()
    {
     
     
        wallet.ChangeMoney(allLevels[currentLevelIndex].levelReward);
      
     if (currentLevelIndex + 1 >= allLevels.Count)
        {
           
             RectTransform rectTransform = BackButton.GetComponent<RectTransform>();
         if (nextButton != null) nextButton.gameObject.SetActive(false);
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.anchoredPosition = Vector2.zero;
          
            if (bm.state == BattleState.WON)
            {  endText.text = " Location cleared";

            PlayerPrefs.SetInt(location.ID, 1);

        
            PlayerPrefs.Save();
          

            location.isCompleted = true;
            }

        }
      

    }

    public void NextLevel()
    {
        
        if (currentLevelIndex + 1 < allLevels.Count)
        {
            currentLevelIndex ++;
            LoadLevel(currentLevelIndex);
        }
       
    }
}
