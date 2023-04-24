/*using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class Save : MonoBehaviour
{
    SaveDataPanel playerInputName;
    SetUpItem setUpItem;
    Sunshine sunshine;


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

    float currentTimeX;     // �����̼� ������ ���� �ð� ����
    float currentTimeY;     // �����̼� ������ ���� �ð� ����
    float currentTimeZ;     // �����̼� ������ ���� �ð� ����

    int dataCount = 3;              //���� ������ ĭ

    const int NotUpdated = -1;      // ������Ʈ �ȵ� ǥ��

    int updatedIndex = NotUpdated;  //���� ������Ʈ �� ������ �ε���

    public Action<int> LoadHp;
    public Action<ToolItemTag, int> onChangeTool;
    public Action<UnityEngine.Quaternion> onLoardRotate;
    private void Awake()
    {
        playerInputName = GetComponent<SaveDataPanel>();
        playerInputName.nameData += PlayerGetName;
        setUpItem = FindObjectOfType<SetUpItem>();
        sunshine.DayTimeDeli += CurrentTime;
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

    private void CurrentTime(UnityEngine.Quaternion sun)
    {
        //�ð� ����
        currentTimeX = sun.x;
        currentTimeY = sun.y;
        currentTimeZ = sun.z;
    }

    public void SavePlayerData()
    {
        // ������ �����͸� �����մϴ�.
        SaveData playerData = new SaveData();

        // �����͸� JSON �������� ��ȯ�մϴ�.
        string json = JsonUtility.ToJson(playerData);
        // JSON ������ �����ϰ� �����͸� �����մϴ�.
        string filePath = Application.dataPath + "/Resources/player_data.json";
        File.WriteAllText(filePath, json);
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
        else
        {
            // ���Ͽ��� ���о����� ����Ʈ �� �ֱ�
            int size = rankLines.Length;
            for (int i = 0; i < size; i++)
            {
                int resultScore = 1;
                for (int j = size - i; j > 0; j--)
                {
                    resultScore *= 10;
                }
                highScores[i] = resultScore; // 10��, ��, õ, ��, ��

                char temp = 'A';
                temp = (char)((byte)temp + i);
                rankerNames[i] = $"{temp}{temp}{temp}";     // AAA,BBB,CCC,DDD,EEE
            }
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
        player.transform.position = new UnityEngine.Vector3(playerPositionX, playerPositionY, playerPositionZ);
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
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataController : MonoBehaviour
{
    GameData playerData = new GameData();

    static GameObject _container;
    static GameObject Container
    {
        get
        {
            return _container;
        }
    }

    static DataController _instance;
    public static DataController Instance
    {
        get
        {
            if(!_instance)
            {
                _container = new GameObject();
                _container.name = "DataController";
                _instance = _container.AddComponent(typeof(DataController)) as DataController;
                DontDestroyOnLoad(_container);
            }
            return _instance;
        }
    }

    public string GameDataFileName = "Save.json";

    public GameData _gameData;
    public GameData gameData
    {
        get
        {
            if(_gameData == null)
            {
                LoadGameData();
                SaveGameData();
            }
            return _gameData;
        }
    }

    private void Start()
    {
    }

    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + GameDataFileName;
        if(File.Exists(filePath))
        {
            Debug.Log("�ҷ����� ����");
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
        }
        else
        {
            Debug.Log("���ο� ���� ����");

            _gameData= new GameData();
        }
    }

    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + GameDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log("����Ϸ�");
    }
}