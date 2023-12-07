using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerComponent : MonoBehaviour
{
    public float speed;
    public float maxHp;
    public float hp;
    public float maxXp;
    public float xp;
    public float attack;
    public Slider hpBar;
    public GameObject emptyObj;
    public GameObject aim;
    public float coolTimer = 2f;
}
