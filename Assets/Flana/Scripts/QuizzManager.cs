using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizzManager : MonoBehaviour {
    [Header("Quizz Data")]
    public CategoryQuestions threatQuestions;
    public CategoryQuestions emotionalQuestions;
    public CategoryQuestions physicalQuestions;
    public CategoryQuestions sexualQuestions;

    [Header("Game Scene Parameters")]
    public CategoryName startingCategory;

    private List<AudioClip> _loadedClips;
    private int _questionCounter;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
