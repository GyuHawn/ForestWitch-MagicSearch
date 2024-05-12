using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.BasicApi;
using System.Text;
using System;
using System.Reflection;
using TMPro;

public class DataSettings
{
    // 레벨 능력
    public int ability1Num = 0;
    public int ability2Num = 0;
    public int ability3Num = 0;
    public int ability4Num = 0;
    public int ability5Num = 0;
    public int ability6Num = 0;

    // 모험 레벨
    public int maxLevel = 1;
    public int currentLevel = 1;

    // 클리어 정보
    public bool onStory = false;
    public int gameExp = 0;

    // 게임 레벨
    public int maxExp = 50;
    public int currentExp = 0;
    public int gameLevel = 1;

    // 게임 세팅
    public int playerNum = 1;
    public int cannonNum1 = 1;
    public int cannonNum2 = 2;

    // 오디오
    public float bgmVolume = 1.0f;
    public float fncVolume = 1.0f;
    public float monsterVolume = 1.0f;
}

public class GameDatas : MonoBehaviour
{
    public DataSettings dataSettings = new DataSettings();

    private string fileName = "file.dat";

    public TMP_Text text;
    public void Update()
    {
        string infoText = "데이터 확인\n" +
                          "Abilities: 0, 0, 0, 0, 0, 0\n" +
                          $"Adventure Level: {dataSettings.maxLevel} (Current: {dataSettings.currentLevel})\n" +
                          $"Story Cleared: {dataSettings.onStory}\n" +
                          $"Game Exp: {dataSettings.gameExp}\n" +
                          $"Game Level: {dataSettings.gameLevel}\n" +
                          $"Player Count: {dataSettings.playerNum}\n" +
                          $"Cannons: {dataSettings.cannonNum1}, {dataSettings.cannonNum2}\n" +
                          $"Audio Settings - BGM: {dataSettings.bgmVolume}, Effects: {dataSettings.fncVolume}, Monster: {dataSettings.monsterVolume}";

        text.text = infoText;
    }


    void BasicData()
    {
        // 레벨 능력 기초
        dataSettings.ability1Num = 0;
        dataSettings.ability2Num = 0;
        dataSettings.ability3Num = 0;
        dataSettings.ability4Num = 0;
        dataSettings.ability5Num = 0;
        dataSettings.ability6Num = 0;

        // 모험 레벨 기초
        dataSettings.maxLevel = 1;
        dataSettings.currentLevel = 1;

        // 클리어 정보 기초
        dataSettings.onStory = false;
        dataSettings.gameExp = 0;

        // 게임 레벨 기초
        dataSettings.maxExp = 0;
        dataSettings.currentExp = 0;
        dataSettings.gameLevel = 1;

        // 게임 세팅 기초
        dataSettings.playerNum = 1;
        dataSettings.cannonNum1 = 1;
        dataSettings.cannonNum2 = 2;

        // 오디오 설정 기초
        dataSettings.bgmVolume = 1.0f;
        dataSettings.fncVolume = 1.0f;
        dataSettings.monsterVolume = 1.0f;
    }

    #region 저장 
    public void SaveFieldData<T>(string fieldName, T fieldValue)
    {
        // 필드 정보를 가져옴
        FieldInfo fieldInfo = typeof(DataSettings).GetField(fieldName);

        // 필드 타입이 일치하는 경우에만 값을 설정
        if (fieldInfo != null && fieldInfo.FieldType == typeof(T))
        {
            fieldInfo.SetValue(dataSettings, fieldValue);

            // 데이터를 JSON으로 변환
            var json = JsonUtility.ToJson(dataSettings);

            // JSON 데이터를 클라우드에 저장
            OpenSaveGame(json);
        }
        else
        {
            Debug.LogError("Field not found or type mismatch");
        }
    }

    /*    private void SaveJsonToCloud(string json)
        {
            // Google Play Games 서비스를 사용하여 클라우드에 저장
            OpenSaveGame(json);
        }
    */
    private void OpenSaveGame(string json)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        savedGameClient.OpenWithAutomaticConflictResolution(fileName,
                                                            DataSource.ReadCacheOrNetwork,
                                                            ConflictResolutionStrategy.UseLastKnownGood,
                                                            (status, game) => OnsavedGameOpened(status, game, json));
    }

    private void OnsavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game, string json)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Save successful");

            // 데이터를 바이트 배열로 변환
            byte[] bytes = Encoding.UTF8.GetBytes(json);

            var update = new SavedGameMetadataUpdate.Builder().Build();

            // 변경된 데이터를 클라우드에 저장
            savedGameClient.CommitUpdate(game, update, bytes, OnSavedGameWritten);
        }
        else
        {
            Debug.Log("Save failed");
        }
    }

    private void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Save completed successfully");
        }
        else
        {
            Debug.Log("Failed to save data");
        }
    }
    #endregion

    #region 불러오기

    // 필드 데이터를 불러오는 범용 메서드
    public void LoadFieldData<T>(string fieldName, Action<T> onSuccess, Action onFailure)
    {
        OpenLoadGame((dataSettings) =>
        {
            FieldInfo fieldInfo = typeof(DataSettings).GetField(fieldName);
            if (fieldInfo != null && fieldInfo.FieldType == typeof(T))
            {
                T value = (T)fieldInfo.GetValue(dataSettings);
                onSuccess?.Invoke(value);  // 콜백을 호출하여 성공 처리
            }
            else
            {
                Debug.LogError("Field not found or type mismatch");
                onFailure?.Invoke();  // 실패 콜백 호출
            }
        });
    }

    private void OpenLoadGame(Action<DataSettings> onLoadedCallback)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(fileName,
                                                            DataSource.ReadCacheOrNetwork,
                                                            ConflictResolutionStrategy.UseLastKnownGood,
                                                            (status, data) => LoadGameData(status, data, onLoadedCallback));
    }

    private void LoadGameData(SavedGameRequestStatus status, ISavedGameMetadata data, Action<DataSettings> onLoadedCallback)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Load successful");
            savedGameClient.ReadBinaryData(data, (readStatus, loadedData) => OnSavedGameDataRead(readStatus, loadedData, onLoadedCallback));
        }
        else
        {
            Debug.Log("Load failed");
        }
    }

    private void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] loadedData, Action<DataSettings> onLoadedCallback)
    {
        if (status == SavedGameRequestStatus.Success && loadedData.Length > 0)
        {
            string jsonData = System.Text.Encoding.UTF8.GetString(loadedData);
            Debug.Log("Loaded data: " + jsonData);

            DataSettings loadedSettings = JsonUtility.FromJson<DataSettings>(jsonData);
            onLoadedCallback(loadedSettings);
        }
        else
        {
            Debug.Log("No data found, initializing default data.");
            BasicData();
            onLoadedCallback(dataSettings);
        }
    }
    #endregion

    #region 삭제
    public void DeleteData()
    {
        DeleteGameData();
    }

    private void DeleteGameData()
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        savedGameClient.OpenWithAutomaticConflictResolution(fileName,
                                                            DataSource.ReadCacheOrNetwork,
                                                            ConflictResolutionStrategy.UseLastKnownGood,
                                                            DeleteSaveGame);
    }

    private void DeleteSaveGame(SavedGameRequestStatus status, ISavedGameMetadata data)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        if (status == SavedGameRequestStatus.Success)
        {
            // 성공적으로 게임 데이터 파일을 열었으면, 삭제를 진행
            savedGameClient.Delete(data);
            // 삭제 로직이 성공적으로 수행되었다고 가정하고 기본 데이터로 초기화
            BasicData();
            Debug.Log("삭제 요청 성공, 데이터를 초기화합니다.");
        }
        else
        {
            Debug.Log("삭제 실패: 게임 데이터 파일 열기에 실패했습니다.");
        }
    }

    #endregion
}
