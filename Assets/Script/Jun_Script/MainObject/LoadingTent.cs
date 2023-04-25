using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingTent : MonoBehaviour
{
    Sunshine sun;
    private bool isTent = false;
    // bool isSunNow = false;
    PlayerBase player;

    public static LoadingTent Instance;

    private void Awake()
    {
        //player = FindObjectOfType<PlayerBase>();
        //if(Instance != null)
        //{
        //    Destroy(player);
        //    return;
        //}
        //Instance= this;
        //DontDestroyOnLoad(player);
    }
    private void Start()
    {
        sun = GameObject.Find("Directional Light").GetComponent<Sunshine>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (sun.isNight == true)
        {
            isTent= true;
            {
                if (other.CompareTag("Player"))
                {
                    if (isTent)
                    {
                        // 여기서 텐트에 G키 활성화 시키기.
                        if (Input.GetKeyDown(KeyCode.G))
                        {
                            DataController.Instance.SaveGameData();
                            SceneManager.LoadScene(1);
                        }
                    }
                }
            }
            isTent = false;
        }
    }
}
