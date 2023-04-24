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


    string playerName;         //이름
    int playerHp;
    float playerPositionX;      //위치
    float playerPositionY;
    float playerPositionZ;

    int[] itemCount = null;             // 갯수
    int[] itemTypes = new int[ItemManager.Instance.itemInventory.ItemTypeArray.Length];        // 종류

    float workbenchPositionX;   //제작대 위치
    float workbenchPositionY;
    float workbenchPositionZ;

    ToolItemTag toolTag;        // 장착한 장비아이템 이름
    int toolName;
    int toolLevel;         // 장착한 장비아이템 레벨

    int currentDay;         // 날짜 저장 배열

    float currentTimeX;     // 로테이션 값으로 현재 시간 저장
    float currentTimeY;     // 로테이션 값으로 현재 시간 저장
    float currentTimeZ;     // 로테이션 값으로 현재 시간 저장

    int dataCount = 3;              //저장 데이터 칸

    const int NotUpdated = -1;      // 업데이트 안됨 표시

    int updatedIndex = NotUpdated;  //현재 업데이트 된 데이터 인덱스

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
        playerHp = newHp;   // 새 점수를 현재 랭킹에 끼워 넣기
        playerPositionX = player.transform.position.x;
        playerPositionY = player.transform.position.y;
        playerPositionZ = player.transform.position.z;

        player.GetToolItem(toolTag, toolLevel);
        toolName = (int)toolTag;
    }

    private void CurrentTime(UnityEngine.Quaternion sun)
    {
        //시간 저장
        currentTimeX = sun.x;
        currentTimeY = sun.y;
        currentTimeZ = sun.z;
    }

    public void SavePlayerData()
    {
        // 저장할 데이터를 생성합니다.
        SaveData playerData = new SaveData();

        // 데이터를 JSON 형식으로 변환합니다.
        string json = JsonUtility.ToJson(playerData);
        // JSON 파일을 생성하고 데이터를 저장합니다.
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

        // 생성한 인스턴스에 데이터 기록
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


        string json = JsonUtility.ToJson(saveData);     // saveData에 있는 내용을 json 양식으로 설정된 string으로 변경

        string path = $"{Application.dataPath}/Save/";  // 저장될 경로 구하기(에디터에서는 Assets 폴더)
        if (!Directory.Exists(path))                    // path에 저장된 폴더가 있는지 확인
        {
            Directory.CreateDirectory(path);            // 폴더가 없으면 그 폴더를 만든다.
        }

        string fullPath = $"{path}Save.json";           // 전체 경로 = 폴더 + 파일이름 + 파일확장자
        File.WriteAllText(fullPath, json);              // fullPath에 json내용 파일로 기록하기        
    }

    bool LoadRankingData()
    {
        bool result = false;

        string path = $"{Application.dataPath}/Save/";              // 경로
        string fullPath = $"{path}Save.json";                       // 전체 경로

        result = Directory.Exists(path) && File.Exists(fullPath);   // 폴더와 파일이 있는지 확인

        if (result)
        {
            // 폴더와 파일이 있으면 읽기
            string json = File.ReadAllText(fullPath);                   // 텍스트 파일 읽기
            SaveData loadData = JsonUtility.FromJson<SaveData>(json);   // json문자열을 파싱해서 SaveData에 넣기
        }
        else
        {
            // 파일에서 못읽었으면 디폴트 값 주기
            int size = rankLines.Length;
            for (int i = 0; i < size; i++)
            {
                int resultScore = 1;
                for (int j = size - i; j > 0; j--)
                {
                    resultScore *= 10;
                }
                highScores[i] = resultScore; // 10만, 만, 천, 백, 십

                char temp = 'A';
                temp = (char)((byte)temp + i);
                rankerNames[i] = $"{temp}{temp}{temp}";     // AAA,BBB,CCC,DDD,EEE
            }
        }
        RefreshSet();     // 로딩이 되었으니 RankLines 갱신
        return result;
    }

    /// <summary>
    /// 랭크 라인 화면 업데이트용 함수
    /// </summary>
    void RefreshSet()
    {
        //플레이어
        PlayerBase player = FindObjectOfType<PlayerBase>();
        player.transform.position = new UnityEngine.Vector3(playerPositionX, playerPositionY, playerPositionZ);
        LoadHp?.Invoke(playerHp);
        onChangeTool?.Invoke((ToolItemTag)toolName, toolLevel);

        //아이템

        ItemManager.Instance.itemInventory.ItemAmountArray = itemCount;
        for (int i = 0; i < itemTypes.Length; i++)
        {
            ItemManager.Instance.itemInventory.ItemTypeArray[i] = (ItemType)itemTypes[i];
        }

        //맵
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
            Debug.Log("불러오기 성공");
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
        }
        else
        {
            Debug.Log("새로운 파일 생성");

            _gameData= new GameData();
        }
    }

    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + GameDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log("저장완료");
    }
}