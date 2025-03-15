using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private int _currentPlayerMoney;
    [SerializeField] private TMP_Text _currentPlayerMoneyText;
    [SerializeField] private Canvas _canvas;

    public void Start()
    {
        _currentPlayerMoneyText.text= _currentPlayerMoney.ToString();
      
    }


    public void SetMoneyVisual(Component sender, object LoseMoney)
    {
        _currentPlayerMoney += (int) LoseMoney;
        _currentPlayerMoneyText.text = _currentPlayerMoney.ToString();

    }

}
