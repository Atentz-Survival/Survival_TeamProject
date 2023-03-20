using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasementObject : MonoBehaviour
{
    // 돌 , 꽃 , 나무가 공통으로 가지는 Base
    // 체력 , effect

    public int maxHP = 3;

    public int HP = 3;

    public GameObject Effect;   // 치집 , 채굴시의 이펙트
    public GameObject Meffect;

    // 리스폰
    // 만약에 원래좌표에 오브젝트가 없는 경우 밤이지나고 리스폰하는 코드

}
