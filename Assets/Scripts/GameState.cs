using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStatus
{
    Ready,
    IsPlaying,
    LevelUp,
    GameOver,
    GameClear,
}

[System.Serializable]
public class GameState
{
    [System.NonSerialized]
    public GameObject player;
    public GameObject playerPrefab;
    public GameObject camera;
    public GameObject enemyPrefab;
    public List<EnemyComponent> enemies;
    public GameObject bulletPrefab;
    public List<BulletComponent> bullets;
    public float enemyDuration;
    public Transform parentDamageText;
    public GameObject prefabDamageText;
    public List<DamageTextComponent> damageTexts;

    [System.NonSerialized]
    public GameStatus gameStatus = GameStatus.Ready;
}
