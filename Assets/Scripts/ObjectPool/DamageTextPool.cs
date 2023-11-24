using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextPool
{
    GameState _gameState;
    GameEvent _gameEvent;
    private Dictionary<int, List<GameObject>> pool = new Dictionary<int, List<GameObject>>();

    public DamageTextPool(GameState gameState, GameEvent gameEvent)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;
        _gameEvent.onRemoveText += OnRemoveText;
    }

    private void OnRemoveText(DamageTextComponent damageTextComp)
    {
        damageTextComp.gameObject.SetActive(false);
        _gameState.damageTexts.Remove(damageTextComp);
    }

    public GameObject OnShowText(GameObject textPrefab, GameObject target)
    {
        int hash = textPrefab.GetHashCode();
        if (pool.ContainsKey(hash))
        {
            List<GameObject> targetPool = pool[hash];
            int count = targetPool.Count;
            for(int j=0 ; j<count ; j++)
            {
                if (targetPool[j].activeSelf == false)
                {
                    targetPool[j].SetActive(true);
                    targetPool[j].transform.position = target.transform.position;
                    return targetPool[j];
                }
            }
            GameObject damageText = GameObject.Instantiate(targetPool[0], target.transform.position, Quaternion.identity, _gameState.parentDamageText);
            targetPool.Add(damageText);
            damageText.SetActive(true);
            return damageText;
        }

        GameObject damageText2 = GameObject.Instantiate(textPrefab, target.transform.position, Quaternion.identity, _gameState.parentDamageText);
        List<GameObject> poolList = new List<GameObject>();
        poolList.Add(damageText2);
        pool.Add(hash, poolList);
        damageText2.SetActive(true);
        return damageText2;
    }
}