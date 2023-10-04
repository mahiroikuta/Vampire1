using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class EnemyPool
{
    GameState _gameState;
    GameEvent _gameEvent;
    private Dictionary<int, List<GameObject>> pool = new Dictionary<int, List<GameObject>>();

    public Vector3 _pPos;

    float hor;
    float ver;

    public EnemyPool(GameState gameState, GameEvent gameEvent)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;
        _gameEvent.onRemoveGameObject += OnRemoveEnemy;
        
        _pPos = _gameState.player.transform.position;
    }

    private void OnRemoveEnemy(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public GameObject OnSpawnEnemy(GameObject enemy)
    {
        Debug.Log(_pPos);
        GenerateRandomPos();
        int hash = enemy.GetHashCode();
        if (pool.ContainsKey(hash))
        {
            List<GameObject> targetPool = pool[hash];
            int count = targetPool.Count;
            for(int j=0 ; j<count ; j++)
            {
                if (targetPool[j].activeSelf == false)
                {
                    targetPool[j].SetActive(true);
                    return targetPool[j];
                }
            }
            GameObject targetEnemy = GameObject.Instantiate(targetPool[0], new Vector3(_pPos.x + hor, _pPos.y + ver, 0), Quaternion.identity);
            targetPool.Add(targetEnemy);
            targetEnemy.SetActive(true);
            return targetEnemy;
        }

        GameObject targetEnemy2 = GameObject.Instantiate(enemy, new Vector3(_pPos.x + hor, _pPos.y + ver, 0), Quaternion.identity);
        List<GameObject> poolList = new List<GameObject>();
        poolList.Add(targetEnemy2);
        pool.Add(hash, poolList);
        targetEnemy2.SetActive(true);
        return targetEnemy2;
    }

    private void GenerateRandomPos()
    {
        float rnd = Random.Range(1,4);
        switch (rnd)
        {
            case 1:
                hor = 22 + Random.Range(1,6);
                break;
            case 2:
                hor = -22 - Random.Range(1,6);
                break;
            case 3:
                hor = Random.Range(-22,22);
                break;
        }

        if (rnd == 3) rnd = Random.Range(1,3);
        else rnd = Random.Range(1,4);
        switch (rnd)
        {
            case 1:
                ver = 11 + Random.Range(1,6);
                break;
            case 2:
                ver = -11 - Random.Range(1,6);
                break;
            case 3:
                ver = Random.Range(-11,11);
                break;
        }
    }
}