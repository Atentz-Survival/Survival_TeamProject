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
        inputField = GetComponentInChildren<TMP_InputField>();  // ������Ʈ ã��
        inputField.onEndEdit.AddListener(OnNameInputEnd);       // �Է��� ������ �� ����� �Լ� ���
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
        saveData.playerName = text;       // �Է¹��� �ؽ�Ʈ�� �ش� ��Ŀ�� �̸����� ����
        inputField.gameObject.SetActive(false); // �Է� �Ϸ�Ǿ����� �ٽ� �Ⱥ��̰� �����
        SceneManager.LoadScene(2);
    }
}
