using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _enemySpawnLocation1,_enemySpawnLocation2;
    [SerializeField] private List<GameObject> _enemySpawnList = new List<GameObject>();
    [SerializeField] private int _baseHealth = 100;
    [SerializeField] private int _currentBaseHealth;
    


    public void Awake()
    {
        _currentBaseHealth = _baseHealth;
    }
    public void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

  

    private IEnumerator SpawnEnemies()
    {
;
        while (true)
        {
            int x = Random.Range(0, 6);
            int y = Random.Range(1, 4);

            if (_enemySpawnList[x] != null)
            {
                if(_enemySpawnList[x].GetComponent<Enemy>().getDimension() == 1)
                {
                    Instantiate(_enemySpawnList[x], _enemySpawnLocation1.position, Quaternion.identity);
                    _enemySpawnList[x].GetComponent<Enemy>().shiftSpawnLocation = _enemySpawnLocation2;
                }
                else
                {
                    Instantiate(_enemySpawnList[x], _enemySpawnLocation2.position, Quaternion.identity);
                    _enemySpawnList[x].GetComponent<Enemy>().shiftSpawnLocation = _enemySpawnLocation1;
                }

                
            }

            yield return new WaitForSeconds(y);



        }

    }



    public void ChangeBaseHealth(int BaseHealth)
    {

        _currentBaseHealth -= BaseHealth;

    }



}
