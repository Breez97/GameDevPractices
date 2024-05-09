using I2.Loc;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropDownMenu : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;

    private void Start()
    {
        LocalizationManager.CurrentLanguage = "English";
        SetLanguages();
    }

    public void GetDropdownValue()
    {
        string currentOption = dropdown.options[dropdown.value].text;
        if(currentOption == "English" || currentOption == "Английский" || currentOption == "Englisch")
        {
            LocalizationManager.CurrentLanguage = "English";
        }
        else if (currentOption == "Russian" || currentOption == "Русский" || currentOption == "Russisch")
        {
            LocalizationManager.CurrentLanguage = "Russian";
        }
        else if (currentOption == "German" || currentOption == "Немецкий" || currentOption == "Deutsch")
        {
            LocalizationManager.CurrentLanguage = "German";
        }
        SetLanguages();
    }

    private void SetLanguages()
    {
        dropdown.ClearOptions();
        if (LocalizationManager.CurrentLanguage == "English")
        {
            List<string> languageOptions = new List<string>
            {
                "English",
                "Russian",
                "German"
            };
            dropdown.AddOptions(languageOptions);
        }
        else if (LocalizationManager.CurrentLanguage == "Russian")
        {
            List<string> languageOptions = new List<string>
            {
                "Русский",
                "Английский",
                "Немецкий"
            };
            dropdown.AddOptions(languageOptions);
        }
        else if (LocalizationManager.CurrentLanguage == "German")
        {
            List<string> languageOptions = new List<string>
            {
                "Deutsch",
                "Englisch",
                "Russisch"
            };
            dropdown.AddOptions(languageOptions);
        }
    }
}
