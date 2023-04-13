using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingTent : MonoBehaviour
{
    Sunshine sun;

    private bool isTent = false;
    TextMeshProUGUI text;
    // bool isSunNow = false;

    private void Start()
    {
        sun = GameObject.Find("Directional Light").GetComponent<Sunshine>();
    }

    private void Update()
    {
        if(sun.isNight == true)
        {
            // Debug.Log("밤"); 
            isTent = true;
        }
        else
        {
            // Debug.Log("밤이 아닙니다.");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (isTent)
            {
                // 여기서 텐트에 G키 활성화 시키기.
                if (Input.GetKeyDown(KeyCode.G))
                {
                    SceneManager.LoadSceneAsync(0);
                }
            }
        }

    }
}
