using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerComponent : MonoBehaviour
{
    public float speed;
    public int maxHp;
    public int hp;
    public Slider hpBar;
    public GameObject emptyObj;
    public GameObject aim;
    public float coolTimer = 2f;
}
