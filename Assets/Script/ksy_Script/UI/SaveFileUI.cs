using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveFileUI : MonoBehaviour
{
    Button save1;

    SaveDataPanel playerInputName;
    SetUpItem setUpItem;
    Sunshine sunshine;
    Timer timer;

    string playerName;         //�̸�
    int playerHp;
    float playerPositionX;      //��ġ
    float playerPositionY;
    float playerPositionZ;

    int[] itemCount = null;             // ����
    int[] itemTypes = new int[ItemManager.Instance.itemInventory.ItemTypeArray.Length];        // ����

    float workbenchPositionX;   //���۴� ��ġ
    float workbenchPositionY;
    float workbenchPositionZ;

    ToolItemTag toolTag;        // ������ �������� �̸�
    int toolName;
    int toolLevel;         // ������ �������� ����

    int currentDay;         // ��¥ ���� �迭
    int currentTime;

    float currentTimeX;     // �����̼� ������ ���� �ð� ����
    float currentTimeY;     // �����̼� ������ ���� �ð� ����
    float currentTimeZ;     // �����̼� ������ ���� �ð� ����

    public Action<int> LoadHp;
    public Action<ToolItemTag, int> onChangeTool;
    public Action<Quaternion> onLoardRotate;
    public Action<int> onLoardDay;
    public Action<int> onLoardTime;

    private void Awake()
    {
        playerInputName = GetComponent<SaveDataPanel>();
        setUpItem = FindObjectOfType<SetUpItem>();
        timer = GetComponent<Timer>();
        save1 = transform.GetChild(0).GetComponent<Button>();

    }
    private void OnEnable()
    {
        save1.onClick.AddListener(SaveFile);
        playerInputName.nameData += PlayerGetName;
        sunshine.DayTimeDeli += CurrentTime;
        timer.dayChange += DayChange;
        timer.hourChange += HourChange;
    }

    private void HourChange(int obj)
    {
        currentTime = obj;
    }

    private void DayChange(int obj)
    {
        currentDay = obj;
    }

    private void PlayerGetName(string obj)
    {
        playerName = obj;
    }

    private void PlayerData(PlayerBase player)
    {
        int newHp = player.HP;
        playerHp = newHp;   // �� ������ ���� ��ŷ�� ���� �ֱ�
        playerPositionX = player.transform.position.x;
        playerPositionY = player.transform.position.y;
        playerPositionZ = player.transform.position.z;

        player.GetToolItem(toolTag, toolLevel);
        toolName = (int)toolTag;
    }

    private void CurrentTime(Quaternion sun)
    {
        //�ð� ����
        currentTimeX = sun.x;
        currentTimeY = sun.y;
        currentTimeZ = sun.z;
    }

    void SaveItem()
    {
        itemCount = ItemManager.Instance.itemInventory.ItemAmountArray;
        for (int i = 0; i < ItemManager.Instance.itemInventory.ItemTypeArray.Length; i++)
        {
            itemTypes[i] = (int)ItemManager.Instance.itemInventory.ItemTypeArray[i];
        }
    }

    void SaveRankingData()
    {
        SaveData saveData = new();

        // ������ �ν��Ͻ��� ������ ���
        saveData.playerName = playerName;
        saveData.playerHp = playerHp;
        saveData.playerPositionX = playerPositionX;
        saveData.playerPositionY = playerPositionY;
        saveData.playerPositionZ = playerPositionZ;

        saveData.currentToolItemTag = toolName;
        saveData.toolLevel = toolLevel;

        saveData.currentTimeX = currentTimeX;
        saveData.currentTimeY = currentTimeY;
        saveData.currentTimeZ = currentTimeZ;

        saveData.currentTime = currentTime;
        saveData.currentDay = currentDay;

        saveData.itemCount = ItemManager.Instance.itemInventory.ItemAmountArray;
        for (int i = 0; i < ItemManager.Instance.itemInventory.ItemTypeArray.Length; i++)
        {
            saveData.itemTypes[i] = (int)ItemManager.Instance.itemInventory.ItemTypeArray[i];
        }

        saveData.workbenchPositionX = setUpItem.transform.position.x;
        saveData.workbenchPositionY = setUpItem.transform.position.y;
        saveData.workbenchPositionZ = setUpItem.transform.position.z;


        string json = JsonUtility.ToJson(saveData);     // saveData�� �ִ� ������ json ������� ������ string���� ����

        string path = $"{Application.dataPath}/Save/";  // ����� ��� ���ϱ�(�����Ϳ����� Assets ����)
        if (!Directory.Exists(path))                    // path�� ����� ������ �ִ��� Ȯ��
        {
            Directory.CreateDirectory(path);            // ������ ������ �� ������ �����.
        }

        string fullPath = $"{path}Save.json";           // ��ü ��� = ���� + �����̸� + ����Ȯ����
        File.WriteAllText(fullPath, json);              // fullPath�� json���� ���Ϸ� ����ϱ�        
    }

    bool LoadRankingData()
    {
        bool result = false;

        string path = $"{Application.dataPath}/Save/";              // ���
        string fullPath = $"{path}Save.json";                       // ��ü ���

        result = Directory.Exists(path) && File.Exists(fullPath);   // ������ ������ �ִ��� Ȯ��

        if (result)
        {
            // ������ ������ ������ �б�
            string json = File.ReadAllText(fullPath);                   // �ؽ�Ʈ ���� �б�
            SaveData loadData = JsonUtility.FromJson<SaveData>(json);   // json���ڿ��� �Ľ��ؼ� SaveData�� �ֱ�
        }
        RefreshSet();     // �ε��� �Ǿ����� RankLines ����
        return result;
    }

    /// <summary>
    /// ��ũ ���� ȭ�� ������Ʈ�� �Լ�
    /// </summary>
    void RefreshSet()
    {
        //�÷��̾�
        PlayerBase player = FindObjectOfType<PlayerBase>();
        player.transform.position = new Vector3(playerPositionX, playerPositionY, playerPositionZ);
        LoadHp?.Invoke(playerHp);
        onChangeTool?.Invoke((ToolItemTag)toolName, toolLevel);

        //������

        ItemManager.Instance.itemInventory.ItemAmountArray = itemCount;
        for (int i = 0; i < itemTypes.Length; i++)
        {
            ItemManager.Instance.itemInventory.ItemTypeArray[i] = (ItemType)itemTypes[i];
        }

        //��
        UnityEngine.Quaternion rotation = UnityEngine.Quaternion.Euler(currentTimeX, currentTimeY, currentTimeZ);
        onLoardRotate?.Invoke(rotation);
        //ui
        onLoardDay?.Invoke(currentDay);
        onLoardTime?.Invoke(currentTime);
    }

    private void SaveFile()
    {

    }
}