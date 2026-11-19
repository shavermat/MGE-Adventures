
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
  [Header("Lobby")]

  public GameObject CollectionPanel;
   public GameObject InfoPanel;
   public GameObject settingsMenu;
   public GameObject creatorsPanel;


  [Header("Levels Menu")]
  [SerializeField] private GameObject ErrorWindows;


  [Header("GamePlay")]

  [SerializeField] private HealthBarScript healthBarManager;
   [SerializeField] private GameObject endScreen;
      [SerializeField] private GameObject battleMenu;
  private LevelManagerScript levelManager;
 private BattleManagerScript battleManager;



   private void Start()
  {
    battleManager = GetComponent<BattleManagerScript>();
    levelManager = GetComponent<LevelManagerScript>();
   

  }
  public void LevelsMenu()
  { 
    
    SceneManager.LoadScene(1);


  }

  public void Collection()
  {
    
    if (CollectionPanel.activeSelf == false)
    {
      settingsMenu.SetActive(false);
      
      CollectionPanel.SetActive(true);

    }
    else
    {
      CollectionPanel.SetActive(false);
       InfoPanel.SetActive(false);
      
    }


  }

  public void Play()
  {
    if (CharactersManagerScript.characters.Count >= 3)
    SceneManager.LoadScene(2);
    else ErrorWindows.SetActive(true);
  }
   public void BackToLobby()
  {
    
    SceneManager.LoadScene(0);
    CharactersManagerScript.characters.Clear();

  }
  public void NextLevel()
  {
    
levelManager.NextLevel();
 battleManager.attackButtonPanel.SetActive(true);
       healthBarManager.gameObject.SetActive(true);
       endScreen.SetActive(false);
   foreach(int deathplayer in healthBarManager.GetComponent<HealthBarScript>().deathPlayersIndex)
    {
      healthBarManager.GetComponent<HealthBarScript>().deathIcon[deathplayer].SetActive(true);
    }
  }

  public void Gacha()
  {
    SceneManager.LoadScene(3);
  }
  public void SettingsMenu()
  {
    if (settingsMenu.activeSelf == false) settingsMenu.SetActive(true);
    else if (settingsMenu.activeSelf == true) settingsMenu.SetActive(false);
  }
  public void CreatorsOn()
  {
    creatorsPanel.SetActive(true);
    if (settingsMenu.activeSelf == true) settingsMenu.SetActive(false);
    
  }
  public void CreatorsOff()
  {
    creatorsPanel.SetActive(false);
  }

  public void BattleMenu()
  {
    if (!battleMenu.activeSelf) battleMenu.SetActive(true);
    
    else if (battleMenu.activeSelf) battleMenu.SetActive(false);
  }
  public void Quit()
  {
    Application.Quit();
  }



}
