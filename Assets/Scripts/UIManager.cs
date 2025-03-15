using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private int _currentPlayerMoney;
    [SerializeField] private TMP_Text _currentPlayerMoneyText;
    [SerializeField] private Canvas _canvas;


    public void Awake()
    {
       _currentPlayerMoney = _player.GetComponent<PlayerControl>().GetMoney();

        _currentPlayerMoneyText.text= _currentPlayerMoney.ToString();

    }

}
