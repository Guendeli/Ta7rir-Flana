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
}

public class GameData {

	
}
