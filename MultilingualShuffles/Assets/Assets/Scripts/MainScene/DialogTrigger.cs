using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogCharacter
{
    public string[] name;
    public Sprite icon;
}

[System.Serializable]
public class DialogLine
{
    public DialogCharacter character;
    public string[] line;
}

[System.Serializable]
public class Dialog
{
    public List<DialogLine> dialogLines = new List<DialogLine>();
}

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;

    [SerializeField] private DialogManager dialogManager;
    [SerializeField] private PopUpText popUpTextShown;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && !dialogManager.IsDialogStarted && popUpTextShown.IsCharacter)
        {
            TriggerDialog();
        }
    }

    public void TriggerDialog()
    {
        DialogManager.Instance.StartDialog(dialog);
    }
}
