using UnityEngine;

public class LevelStartController : MonoBehaviour
{
    private void Start()
    {
        if (SaveManager.Instance.HasSaves())
            SaveManager.Instance.Load();
    }
}
