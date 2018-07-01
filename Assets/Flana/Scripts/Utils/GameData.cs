using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CategoryName
{
    Threatening,
    Emotional,
    Physical,
    Sexual
}

[System.Serializable]
public struct CategoryQuestions
{
    public string catName;
    public List<AudioClip> questionClips;
    public List<string> questionTitle;
    public List<Sprite> questionImages;
}

public static class GameData {

    public static CategoryName GAME_CATEGORY;
    public static string USER_ID = string.Empty;
}
