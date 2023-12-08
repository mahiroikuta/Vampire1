using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpScreen : BaseScreen
{
    GameState _gameState;
    GameEvent _gameEvent;
    [SerializeField] private Button coolTimeButton;
    [SerializeField] private Button damageUpButton;
    [SerializeField] private Button bulletSpeedButton;
    PlayerComponent playerComponent;
    public override void Init(GameState gameState, GameEvent gameEvent)
    {
        coolTimeButton.onClick.AddListener(enhanceCoolTime);
        damageUpButton.onClick.AddListener(enhanceDamageUp);
        bulletSpeedButton.onClick.AddListener(enhanceBulletSpeed);

        playerComponent = gameState.player.GetComponent<PlayerComponent>();
        _gameState = gameState;
        _gameEvent = gameEvent;
        _gameEvent.showLevelUpScreen += this.OnShow;
        _gameEvent.hideLevelUpScreen += this.OnHide;
        _gameEvent.hideLevelUpScreen?.Invoke();
    }

    private void enhanceCoolTime()
    {
        playerComponent.coolTimeLevel++;
        _gameEvent.selectEnhance?.Invoke();
    }

    private void enhanceDamageUp()
    {
        playerComponent.damageUpLevel++;
        _gameEvent.selectEnhance?.Invoke();
    }

    private void enhanceBulletSpeed()
    {
        playerComponent.bulletSpeedLevel++;
        _gameEvent.selectEnhance?.Invoke();
    }
}
