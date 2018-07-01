using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ArabicSupport;
using System.Linq;
public class MenuManager : MonoBehaviour {

    // public methods are done here
    [Header("UI References")]
    public Text titleText;

    [Header("Localisation stuff")]
    public string[] categoryNamesArabic;

    [Header("Database Crap here")]
    public GoogleAnalyticsV4 GoogleAnalytics;
    private string _gameTitle;

    // unity Methods
    void Start()
    {
        _gameTitle = titleText.text;
        InitUser();    
    }


    // privates
    private void InitUser()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        System.Random random = new System.Random();
        if (GameData.USER_ID == string.Empty)
        {
            GameData.USER_ID = new string(Enumerable.Repeat(chars, 4).Select(s => s[random.Next(s.Length)]).ToArray());
            GoogleAnalytics.LogEvent("Loggin", "New User", "User Id: " + GameData.USER_ID, 1);
        }
    }
    // Helper Methods
    public void GoToGame(string cat)
    {
        try
        {
            CategoryName enumerable = (CategoryName)System.Enum.Parse(typeof(CategoryName), cat);
            GameData.GAME_CATEGORY = enumerable;
            SceneManager.LoadScene(1);
            //Foo(enumerable); //Now you have your enum, do whatever you want.
        }
        catch (System.Exception)
        {
            Debug.LogErrorFormat("Parse: Can't convert {0} to enum, please check the spell.", cat);
        }
       
    }

    public void SetCategoryTitle(string cat)
    {
        try
        {
            CategoryName enumerable = (CategoryName)System.Enum.Parse(typeof(CategoryName), cat);
            titleText.text = ArabicFixer.Fix(categoryNamesArabic[(int)enumerable]);
            //Foo(enumerable); //Now you have your enum, do whatever you want.
        }
        catch (System.Exception)
        {
            Debug.LogErrorFormat("Parse: Can't convert {0} to int, please check the spell.", cat);
        }
    }

    public void ResetCategoryTitle()
    {
        titleText.text = _gameTitle;
    }
}
