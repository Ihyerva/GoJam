using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _enemySpawnLocation1,_enemySpawnLocation2;
    [SerializeField] private List<GameObject> _enemySpawnList = new List<GameObject>();
    private int _currentSpawnIndex;
    private int _currentSpawnerBuffer;
    [SerializeField] private List<float> _buffer = new List<float>();
    


    public void Awake()
    {
        _currentSpawnIndex = 0;
       _currentSpawnerBuffer = 0;
    }
    public void Start()
    {
        StartCoroutine(SpawnEnemies());
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
}
