using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunshine : MonoBehaviour
{
    public float forTimeInGame = 0.0f;  // 시간의 경과

    private bool isNight = false;

    private void Update()
    {
        transform.Rotate(Vector3.right, 0.1f * forTimeInGame * Time.deltaTime);

    }
}
