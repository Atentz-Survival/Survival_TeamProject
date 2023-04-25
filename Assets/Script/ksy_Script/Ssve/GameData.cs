using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[Serializable]
public class GameData
{

    public string playerName;         //이름
    public int playerHp;
    public Vector3 playerPosition;      //위치

    /*public int[] itemCount;             // 갯수
    public int[] itemTypes;          // 종류
*/
   /* public Vector3 workbenchPosition;   //제작대 위치*/

    public int currentToolItem;
    public int toolLevel;         // 장착한 장비아이템 레벨

    public int currentDay;         // 날짜 저장 배열
    public int currentTime;

    public float currentSunRotate;
}
