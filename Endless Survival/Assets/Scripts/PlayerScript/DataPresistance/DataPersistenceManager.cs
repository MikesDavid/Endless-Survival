using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool initializeDataIfNull = false;
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncription;

    private GameData gameData;

    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler DataHandler;

    public static DataPersistenceManager instance { get; private set; }


    private void Awake()
    {
        
        if (instance != null)
        {
            Destroy(this.gameObject); 
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        
    }


    private void OnEnable()
    {
        fileName = DBManager.username + ".json";
        this.DataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncription);

        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        Debug.Log("OnSceneLoaded Called");
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void  NewGame()
    {
        this.gameData = new GameData();
        SaveGame();
    }
    public void LoadGame()
    {
        
        this.gameData = DataHandler.Load();
        if (this.gameData == null && initializeDataIfNull)
        {
            NewGame();
        }
        if (this.gameData == null)
        {
            //Debug.Log("No Data was found. Initializing data to defaults.");
            NewGame();
        }
        if (this.gameData != null)
        {
            foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
            {
                dataPersistenceObj.LoadData(gameData);
            }
        }
        //Debug.Log("Loaded level = " + gameData.level);
    }
    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(gameData);
        }
        Debug.Log("Saved level = " + gameData.level);

        DataHandler.Save(gameData);
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
