using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerComponent : MonoBehaviour
{
    public float level;
    public float coolTimeLevel;
    public float damageUpLevel;
    public float bulletSpeedLevel;
    public float speed;
    public float maxHp;
    public float hp;
    public float maxXp;
    public float xp;
    public float attack;
    public float bulletSpeed;
    public Slider hpBar;
    public Slider xpBar;
    public GameObject emptyObj;
    public GameObject aim;
    public float coolTimer = 2f;
}
