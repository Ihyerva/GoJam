using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _enemySpawnLocation1,_enemySpawnLocation2;
    [SerializeField] private List<GameObject> _enemySpawnList = new List<GameObject>();
    private int _currentSpawnIndex;
    private int _currentSpawnerBuffer;
    [SerializeField] private List<float> _buffer = new List<float>();
    [SerializeField] private int _baseHealth = 100;
    [SerializeField] private int _currentBaseHealth;
    [SerializeField] private Image _healthBar;
    


    public void Awake()
    {
        _currentBaseHealth = _baseHealth;
        _currentSpawnIndex = 0;
       _currentSpawnerBuffer = 0;
    }
    public void Start()
    {
        StartCoroutine(SpawnEnemies());
    }


    private void Update()
    {
        EndGame();
        _healthBar.fillAmount = (float)_currentBaseHealth / _baseHealth;
    }



    private IEnumerator SpawnEnemies()
    {
        while(_currentSpawnIndex < _enemySpawnList.Count)
        {
            if (_enemySpawnList[_currentSpawnIndex] != null)
            {
                if(_enemySpawnList[_currentSpawnIndex].GetComponent<Enemy>().getDimension() == 1)
                {
                    Instantiate(_enemySpawnList[_currentSpawnIndex], _enemySpawnLocation1.position, Quaternion.identity);
                    _enemySpawnList[_currentSpawnIndex].GetComponent<Enemy>().shiftSpawnLocation = _enemySpawnLocation2;
                }
                else
                {
                    Instantiate(_enemySpawnList[_currentSpawnIndex], _enemySpawnLocation2.position, Quaternion.identity);
                    _enemySpawnList[_currentSpawnIndex].GetComponent<Enemy>().shiftSpawnLocation = _enemySpawnLocation1;
                }

                _currentSpawnIndex++;
                
            }

            yield return new WaitForSeconds(_buffer[_currentSpawnerBuffer]);
            _currentSpawnerBuffer++;



        }

    }


    public void EndGame()
    {
        if(_currentBaseHealth <= 0)
        {

            SceneManager.LoadScene("GameOverScreen");

        }


    }
    public void ChangeBaseHealth(int BaseHealth)
    {

        _currentBaseHealth -= BaseHealth;

    }


    public void QuitGame()
    {

        Application.Quit();
    }

    public void NewGame()
    {
        SceneManager.LoadScene("berkcantest");


    }


    public void MainMenu()
    {

        SceneManager.LoadScene("MainMenuScreen");
    }


}
