using TMPro;
using UnityEngine;

public class WalletUI : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI balanceText;

  private int moneyCount;

  private void Start()
    {
      moneyCount = PlayerPrefs.GetInt("Money",0);

      balanceText.text = "Money:" + moneyCount;
     

    }

}
