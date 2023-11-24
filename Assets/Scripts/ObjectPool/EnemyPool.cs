using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool
{
    GameState _gameState;
    GameEvent _gameEvent;
    private Dictionary<int, List<GameObject>> pool = new Dictionary<int, List<GameObject>>();

    PlayerComponent _player;
    EnemyComponent _enemyComp;

    public EnemyPool(GameState gameState, GameEvent gameEvent)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;
        _gameEvent.onRemoveEnemy += OnRemoveEnemy;

        _player = _gameState.player.GetComponent<PlayerComponent>();
    }

    private void OnRemoveEnemy(EnemyComponent enemyComp)
    {
        enemyComp.gameObject.SetActive(false);
        _gameState.enemies.Remove(enemyComp);
    }

    public GameObject OnSpawnEnemy(GameObject enemyPrefab)
    {
        Vector3 pPos = _player.transform.position;
        Vector3 addVec = GenerateRandomPos();
        int hash = enemyPrefab.GetHashCode();
        if (pool.ContainsKey(hash))
        {
            List<GameObject> targetPool = pool[hash];
            int count = targetPool.Count;
            for(int j=0 ; j<count ; j++)
            {
                if (targetPool[j].activeSelf == false)
                {
                    targetPool[j].SetActive(true);
                    targetPool[j].transform.position = pPos+addVec;
                    _enemyComp = targetPool[j].GetComponent<EnemyComponent>();
                    _enemyComp.hp = _enemyComp.maxHp;
                    _enemyComp.hpBar.value = _enemyComp.maxHp;
                    return targetPool[j];
                }
            }
            GameObject targetEnemy = GameObject.Instantiate(targetPool[0], pPos+addVec, Quaternion.identity);
            _enemyComp = targetEnemy.GetComponent<EnemyComponent>();
            _enemyComp.hp = _enemyComp.maxHp;
            _enemyComp.hpBar.maxValue = _enemyComp.maxHp;
            _enemyComp.hpBar.value = _enemyComp.maxHp;
            targetPool.Add(targetEnemy);
            targetEnemy.SetActive(true);
            return targetEnemy;
        }

        GameObject targetEnemy2 = GameObject.Instantiate(enemyPrefab, pPos+addVec, Quaternion.identity);
        _enemyComp = targetEnemy2.GetComponent<EnemyComponent>();
        _enemyComp.hp = _enemyComp.maxHp;
        _enemyComp.hpBar.maxValue = _enemyComp.maxHp;
        _enemyComp.hpBar.value = _enemyComp.maxHp;
        List<GameObject> poolList = new List<GameObject>();
        poolList.Add(targetEnemy2);
        pool.Add(hash, poolList);
        targetEnemy2.SetActive(true);
        return targetEnemy2;
    }

    private Vector3 GenerateRandomPos()
    {
        float hor = 0;
        float ver = 0;
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
        Vector3 addVec = new Vector3(hor, ver, 0);
        return addVec; 
    }
}