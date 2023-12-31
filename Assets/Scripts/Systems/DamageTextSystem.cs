using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextSystem
{
    private GameState _gameState;
    private GameEvent _gameEvent;
    private DamageTextPool _damageTextPool;
    private DamageTextComponent _damageTextComp;

    private List<DamageTextComponent> textsToRemove = new List<DamageTextComponent>();

    public DamageTextSystem(GameState gameState, GameEvent gameEvent, DamageTextPool damageTextPool)
    {
        _gameState = gameState;
        _gameEvent = gameEvent;
        _damageTextPool = damageTextPool;

        _gameEvent.damageText += GeneText;
    }

    public void OnUpdate()
    {
        if (_gameState.gameStatus != GameStatus.IsPlaying) return;
        TextsAction();
    }

    private void TextsAction()
    {
        foreach (DamageTextComponent damageTextComp in _gameState.damageTexts)
        {
            _damageTextComp = damageTextComp;
            _damageTextComp.timer += Time.deltaTime;
            if (_damageTextComp.timer > _damageTextComp.removeTime)
            {
                textsToRemove.Add(damageTextComp);
                _damageTextComp.timer = 0;
            }
        }

        foreach (DamageTextComponent damageTextComp in textsToRemove)
        {
            _gameEvent.onRemoveText?.Invoke(damageTextComp);
        }
        textsToRemove.Clear();
    }

    private void GeneText(GameObject attacker)
    {
        GameObject damageText = _damageTextPool.OnShowText(_gameState.prefabDamageText, attacker);
        DamageTextComponent damageTextComp = damageText.GetComponent<DamageTextComponent>();
        if (attacker.CompareTag("Player"))
        {
            PlayerComponent playerComp = attacker.GetComponent<PlayerComponent>();
            damageTextComp.damage = playerComp.attack;
        }
        else if (attacker.CompareTag("Enemy"))
        {
            EnemyComponent enemyComp = attacker.GetComponent<EnemyComponent>();
            damageTextComp.damage = enemyComp.attack;
        }
        else return;

        damageTextComp.damageText.SetText(damageTextComp.damage.ToString());
        _gameState.damageTexts.Add(damageTextComp);
    }
}
