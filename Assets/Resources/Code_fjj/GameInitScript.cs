using System.IO;
using System.Text;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;



public class GameInitScript : MonoBehaviour
{
    private void Awake()
    {
        GameDataInit();
    }

    private void GameDataInit()
    {
        if (File.Exists(GetPersistentFilePath("Data/Role.csv")))
        {
            DataEnable();
        }
        else
        {
            Directory.CreateDirectory(GetPersistentFilePath("Data"));
            Directory.CreateDirectory(GetPersistentFilePath("UI"));
            StartCoroutine(DataMigration());
            Invoke("DataEnable", 1);
        }
    }

    private IEnumerator DataMigration()
    {
        string filePath = GetStreamFilePath("MigrationList.txt");
        UnityWebRequest file = UnityWebRequest.Get(filePath);
        yield return file.SendWebRequest();
        if (file.isDone)
        {
            byte[] array = Encoding.UTF8.GetBytes(file.downloadHandler.text);
            MemoryStream sBuf = new MemoryStream(array);
            StreamReader reader = new StreamReader(sBuf);
            string Buf;
            Buf = reader.ReadLine();
            while (Buf != null)
            {
                StartCoroutine(DataMigrationHelp(Buf));
                Buf = reader.ReadLine();
            }
        }
    }
    private IEnumerator DataMigrationHelp(string path)
    {
        string filePath = GetStreamFilePath(path);
        UnityWebRequest file = UnityWebRequest.Get(filePath);
        yield return file.SendWebRequest();
        string savePath = GetPersistentFilePath(path);
        FileInfo t = new FileInfo(savePath);
        FileStream fs = t.Create();
        fs.Close();
        fs.Dispose();
        if (file.isDone)
        {
            File.WriteAllBytes(savePath, file.downloadHandler.data);
        }
    }

    private void DataEnable()
    {
        GetComponent<Canvas>().enabled = false;
        transform.parent.Find("GameScript").gameObject.SetActive(true);
        transform.parent.Find("MainUI").gameObject.SetActive(true);
    }

    private string GetStreamFilePath(string FileName)
    {
        string filePath;
        if (Application.platform == RuntimePlatform.Android)
        {
            filePath = Application.streamingAssetsPath + '/' + FileName;
        }
        else
        {
            filePath = "file://" + Application.streamingAssetsPath + '/' + FileName;
        }
        return filePath;
    }
    private string GetPersistentFilePath(string FileName)
    {
        string filePath;
        if (Application.platform == RuntimePlatform.Android)
        {
            filePath = Application.persistentDataPath + '/' + FileName;
        }
        else
        {
            filePath = "file://" + Application.persistentDataPath + '/' + FileName;
        }
        return filePath;
    }
}
