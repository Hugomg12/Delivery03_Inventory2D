using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Lenguage : MonoBehaviour
{
    public TMP_Dropdown languageDropdown;
    private Dictionary<string, Dictionary<string, string>> translations;

    void Start()
    {
        // Vincular el Dropdown si no está asignado manualmente
        if (languageDropdown == null)
        {
            languageDropdown = GameObject.Find("LanguageDropdown").GetComponent<TMP_Dropdown>();
        }

        // Configurar traducciones
        SetupTranslations();

        // Cargar idioma guardado o usar inglés por defecto
        int savedLanguage = PlayerPrefs.GetInt("SelectedLanguage", 0);
        languageDropdown.value = savedLanguage;
        ChangeLanguage(savedLanguage);

        // Agregar listener al Dropdown
        languageDropdown.onValueChanged.AddListener(delegate { ChangeLanguage(languageDropdown.value); });
    }

    void SetupTranslations()
    {
        translations = new Dictionary<string, Dictionary<string, string>>();

        translations["English"] = new Dictionary<string, string>
        {
            {"title", "Shopping Game"},
            {"buy", "Buy"},
            {"sell", "Sell"},
            {"use", "Use"},
            {"exit", "Exit"}
        };

        translations["Español"] = new Dictionary<string, string>
        {
            {"title", "Juego de Compras"},
            {"buy", "Comprar"},
            {"sell", "Vender"},
            {"use", "Usar"},
            {"exit", "Salir"}
        };

        translations["Català"] = new Dictionary<string, string>
        {
            {"title", "Joc de Compres"},
            {"buy", "Comprar"},
            {"sell", "Vendre"},
            {"use", "Utilitzar"},
            {"exit", "Sortir"}
        };
    }

    public void ChangeLanguage(int languageIndex)
    {
        string selectedLanguage = "English";

        switch (languageIndex)
        {
            case 0:
                selectedLanguage = "English";
                break;
            case 1:
                selectedLanguage = "Español";
                break;
            case 2:
                selectedLanguage = "Català";
                break;
        }

        PlayerPrefs.SetInt("SelectedLanguage", languageIndex);
        PlayerPrefs.Save();

        UpdateUIText(selectedLanguage);
    }

    void UpdateUIText(string language)
    {
        TMP_Text buyText = GameObject.Find("ButtonBuy")?.GetComponentInChildren<TMP_Text>();
        TMP_Text sellText = GameObject.Find("ButtonSell")?.GetComponentInChildren<TMP_Text>();
        TMP_Text useText = GameObject.Find("ButtonUse")?.GetComponentInChildren<TMP_Text>();

        if (buyText != null)
            buyText.text = translations[language]["buy"];
        else
            Debug.LogError("❌ No se encontró 'ButtonBuy' o 'TextBuy' en la jerarquía.");

        if (sellText != null)
            sellText.text = translations[language]["sell"];
        else
            Debug.LogError("❌ No se encontró 'ButtonSell' o 'TextSell' en la jerarquía.");

        if (useText != null)
            useText.text = translations[language]["use"];
        else
            Debug.LogError("❌ No se encontró 'ButtonUse' o 'TextUse' en la jerarquía.");
    }   

}