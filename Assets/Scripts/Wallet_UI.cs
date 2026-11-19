using UnityEngine;
using TMPro;
public class Wallet_UI : MonoBehaviour
{
     [SerializeField]  private int moneyCount;
      
    public TextMeshProUGUI moneyText;
    private void Start() 
 {
    MoneyUpdate();
   
 }
 public void MoneyUpdate() 
 {
    moneyCount = PlayerPrefs.GetInt("Money", 0);

  moneyText.text = "Money:" + moneyCount;

 }
}
