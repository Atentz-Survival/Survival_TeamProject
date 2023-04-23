using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputPlayerName : MonoBehaviour
{
    TMP_InputField inputField;

    NewButtonSceneChange change;

    private void Awake()
    {
        change = FindObjectOfType<NewButtonSceneChange>();
        inputField = GetComponentInChildren<TMP_InputField>();  // 컴포넌트 찾고
        inputField.onEndEdit.AddListener(OnNameInputEnd);       // 입력이 끝났을 때 실행될 함수 등록
    }

    private void Start()
    {
        inputField.transform.parent.transform.parent.gameObject.SetActive(false);
        change.whatIsYOurNAme += YOurNAme;
    }

    private void YOurNAme()
    {
        inputField.transform.parent.transform.parent.gameObject.SetActive(true);
    }

    private void OnNameInputEnd(string text)
    {
        SaveData saveData = new();
        saveData.playerName = text;       // 입력받은 텍스트를 해당 랭커의 이름으로 지정
        inputField.gameObject.SetActive(false); // 입력 완료되었으니 다시 안보이게 만들기
        SceneManager.LoadScene(2);
    }
}
