using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayInfo : MonoBehaviour
{
    TextMeshProUGUI recordText;
    TextMeshProUGUI recordText2;
    TextMeshProUGUI recordText3;
    private void Awake()
    {
        // 컴포넌트 찾기
        Transform child = transform.GetChild(0);
        recordText = child.GetComponent<TextMeshProUGUI>();
        Transform child1 = transform.GetChild(1);
        recordText2 = child1.GetComponent<TextMeshProUGUI>();
        Transform child3 = transform.GetChild(2);
        recordText2 = child3.GetComponent<TextMeshProUGUI>();
    }

    public void SetData(int record, int record2)
    {
        recordText2.text = record.ToString();
        recordText3.text = record2.ToString();
    }
}
