using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;
using System.Text;
using System;
using System.Reflection;
using GooglePlayGames.BasicApi;

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
    public int adventLevel = 1;

    // 클리어 정보
    public bool onStory = false;

    // 게임 레벨
    public int maxExp = 50;
    public int currentExp = 0;
    public int gameLevel = 50;

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

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void BasicData()
    {
        dataSettings = new DataSettings();
    }

    #region 저장 
    public void UpdateAbility<T>(string fieldName, T fieldValue)
    {
        SaveFieldData(fieldName, fieldValue);
    }

    public void SaveFieldData<T>(string fieldName, T fieldValue)
    {
        try
        {
            FieldInfo fieldInfo = typeof(DataSettings).GetField(fieldName);
            if (fieldInfo != null && fieldInfo.FieldType == typeof(T))
            {
                fieldInfo.SetValue(dataSettings, fieldValue);
                var json = JsonUtility.ToJson(dataSettings);
                SaveJsonToCloud(json);
            }
            else
            {
                Debug.LogError("Field not found or type mismatch");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error saving data: {ex.Message}");
        }
    }

    private void SaveJsonToCloud(string json)
    {
        OpenSaveGame(json);
    }

    private void OpenSaveGame(string json)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        savedGameClient.OpenWithAutomaticConflictResolution(fileName,
                                                            DataSource.ReadCacheOrNetwork,
                                                            ConflictResolutionStrategy.UseLastKnownGood,
                                                            (status, game) => OnSavedGameOpened(status, game, json));
    }

    private void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game, string json)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Save successful");

            byte[] bytes = Encoding.UTF8.GetBytes(json);
            var update = new SavedGameMetadataUpdate.Builder().Build();

            savedGameClient.CommitUpdate(game, update, bytes, OnSavedGameWritten);
        }
        else
        {
            Debug.LogError("Save failed");
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
            Debug.LogError("Failed to save data");
        }
    }
    #endregion

    #region 불러오기

    public void LoadData()
    {
        OpenLoadGame();
    }

    private void OpenLoadGame()
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        savedGameClient.OpenWithAutomaticConflictResolution(fileName,
                                                             DataSource.ReadCacheOrNetwork,
                                                             ConflictResolutionStrategy.UseLastKnownGood,
                                                             LoadGameData);
    }

    private void LoadGameData(SavedGameRequestStatus status, ISavedGameMetadata data)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Load completed successfully");

            savedGameClient.ReadBinaryData(data, OnSavedGameDataRead);
        }
        else
        {
            Debug.LogError("Failed to load data");
        }
    }

    private void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] loadedData)
    {
        if (status == SavedGameRequestStatus.Success && loadedData.Length > 0)
        {
            string data = Encoding.UTF8.GetString(loadedData);
            if (data != "")
            {
                dataSettings = JsonUtility.FromJson<DataSettings>(data);
            }
        }
        else
        {
            Debug.LogError("Failed to read saved data");
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
            savedGameClient.Delete(data);
            BasicData();
            Debug.Log("Data deleted and reset to default");
        }
        else
        {
            Debug.LogError("Failed to delete data");
        }
    }
    #endregion
}