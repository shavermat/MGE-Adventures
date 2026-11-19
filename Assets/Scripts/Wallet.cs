
using System.Data.SqlTypes;
using UnityEngine;

public class Wallet : MonoBehaviour
{
   [SerializeField] private int _money;



private void Start()
    {
          _money = PlayerPrefs.GetInt("Money", 0);

          if (!PlayerPrefs.HasKey("FirstGameOpened")) FirstOpened();
    }

   public void ChangeMoney(int amount)
    {
            if (amount < 0 && _money + amount < 0)
    {
 
        return;
    }

        _money += amount;
  

        PlayerPrefs.SetInt("Money", _money);
        PlayerPrefs.Save();

    }

    private void FirstOpened()
    {
        ChangeMoney(250);
        PlayerPrefs.SetInt("FirstGameOpened", 1);
    }




}
