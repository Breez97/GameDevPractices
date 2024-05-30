using System;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    [SerializeField] private Transform[] tetros;
    [SerializeField] private Timer timer;

    private void Awake()
    {
        Instance = this;
    }

    public void Save()
    {
        for (int i = 0; i < tetros.Length; i++)
        {
            ES3.Save($"tetro_{i}", tetros[i].transform);
        }

        ES3.Save("timer", timer);
    }
    
    public void Load()
    {
        for (int i = 0; i < tetros.Length; i++)
        {
            ES3.LoadInto($"tetro_{i}", tetros[i].transform);
        }

        ES3.LoadInto("timer", timer);
    }

    public bool HasSaves()
    {
        return ES3.KeyExists("timer");
    }

    public void DeleteAllKeys()
    {
        for (int i = 0; i < 20; i++)
        {
            ES3.DeleteKey($"tetro_{i}");
        }

        ES3.DeleteKey("timer");
    }
}
