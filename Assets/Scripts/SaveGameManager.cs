using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    private const string SAVE_FILE_NAME = "/Save.dat";
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager uiManager;

    private SerializedSaveGame serializedSaveGame;

    [ContextMenu("Save!")]
    public void SaveGame()
    {
        serializedSaveGame = new SerializedSaveGame(); 

        serializedSaveGame.playerPosition = gameManager.playerController.transform.position;
        serializedSaveGame.playerRotation = gameManager.playerController.transform.eulerAngles;
        serializedSaveGame.playerHP = gameManager.playerController.CurrentHP;
        serializedSaveGame.currentWaypointIndex = gameManager.indexCounter.currentWaypointIndex;

        SaveToJson();
        //SaveToBinary();
    }

    [ContextMenu("Load!")]
    public void LoadGame()
    {
        LoadFromJson();
        //LoadFromBinary();

        gameManager.playerController.transform.position = serializedSaveGame.playerPosition;
        gameManager.playerController.transform.eulerAngles = serializedSaveGame.playerRotation;
        gameManager.playerController.currentHP = serializedSaveGame.playerHP;
        uiManager.RefreshHPText();
        gameManager.indexCounter.currentWaypointIndex = serializedSaveGame.currentWaypointIndex;
        gameManager.indexCounter.SetDestination();
    }

    private void SaveToJson()
    {
        string jsonString = JsonUtility.ToJson(serializedSaveGame, prettyPrint: true);
        File.WriteAllText(Application.persistentDataPath + SAVE_FILE_NAME, jsonString);
    }

    private void LoadFromJson()
    {
        string jsonString = File.ReadAllText(Application.persistentDataPath + SAVE_FILE_NAME);
        serializedSaveGame = JsonUtility.FromJson<SerializedSaveGame>(jsonString);
    }

    private void SaveToBinary()
    {

    }

    private void LoadFromBinary()
    {

    }

}
