using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool
{
    GameState _gameState;
    GameEvent _gameEvent;
    private Dictionary<int, List<GameObject>> pool = new Dictionary<int, List<GameObject>>();

    PlayerComponent _player;

    public BulletPool(GameState gameState, GameEvent gameEvent)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;

        _gameEvent.onRemoveBullet += OnRemoveBullet;
        _gameEvent.startGame += Init;
    }

    private void Init()
    {
        _player = _gameState.player.GetComponent<PlayerComponent>();
    }

    private void OnRemoveBullet(BulletComponent bulletComp)
    {
        bulletComp.gameObject.SetActive(false);
        _gameState.bullets.Remove(bulletComp);
    }

    public GameObject OnShootBullet(GameObject bulletPrefab)
    {
        int hash = bulletPrefab.GetHashCode();
        if (pool.ContainsKey(hash))
        {
            List<GameObject> targetPool = pool[hash];
            int count = targetPool.Count;
            for(int j=0 ; j<count ; j++)
            {
                if (targetPool[j].activeSelf == false)
                {
                    targetPool[j].SetActive(true);
                    targetPool[j].transform.position = _player.aim.transform.position;
                    targetPool[j].transform.rotation = _player.emptyObj.transform.rotation;
                    return targetPool[j];
                }
            }
            GameObject targetBullet = GameObject.Instantiate(targetPool[0], _player.aim.transform.position, _player.emptyObj.transform.rotation);
            targetPool.Add(targetBullet);
            targetBullet.SetActive(true);
            return targetBullet;
        }

        GameObject targetBullet2 = GameObject.Instantiate(bulletPrefab, _player.aim.transform.position, _player.emptyObj.transform.rotation);
        List<GameObject> poolList = new List<GameObject>();
        poolList.Add(targetBullet2);
        pool.Add(hash, poolList);
        targetBullet2.SetActive(true);
        return targetBullet2;
    }
}