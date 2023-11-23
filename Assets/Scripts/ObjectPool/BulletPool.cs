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

        _player = _gameState.player.GetComponent<PlayerComponent>();
    }

    private void OnRemoveBullet(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public GameObject OnShootBullet(GameObject bullet)
    {
        int hash = bullet.GetHashCode();
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
            GameObject bullet = GameObject.Instantiate(targetPool[0], _player.transform.position, _player.emptyObj.rotation);
            targetPool.Add(bullet);
            bullet.SetActive(true);
            return bullet;
        }

        GameObject bullet2 = GameObject.Instantiate(enemy, _player.transform.position, _player.emptyObj.rotation);
        List<GameObject> poolList = new List<GameObject>();
        poolList.Add(bullet2);
        pool.Add(hash, poolList);
        bullet2.SetActive(true);
        return bullet2;
    }
}