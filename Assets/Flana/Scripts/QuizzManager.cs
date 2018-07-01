using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ArabicSupport;
using System.Linq;

public class QuizzManager : MonoBehaviour {
    [Header("Quizz Data")]
    public CategoryQuestions threatQuestions;
    public CategoryQuestions emotionalQuestions;
    public CategoryQuestions physicalQuestions;
    public CategoryQuestions sexualQuestions;

    [Header("Game Scene Parameters")]
    public CategoryName startingCategory;
    public LoopableAudioPlayer audioPlayer;

    [Header("USer Interface references")]
    public GameObject promptUI;
    public GameObject quizzUI;
    public GameObject scoreUI;
    public Image logoUI;

    public Text questionTitleText;
    public Text scoreText;

    private List<AudioClip> _loadedClips;
    private List<string> _questionTitles;
    private List<Sprite> _questionImages;
    private int _questionCounter;
    private float _yesCounter;
	// Use this for initialization
	void Start () {
        //init user:

        // init quizz
        InitQuizz();
	}
	
	// Update is called once per frame
	void Update () {
       
	}

    // private method are done here
   
    
    private void InitQuizz()
    {
        // initialization loop
        startingCategory = GameData.GAME_CATEGORY;
        _questionCounter = 1;
        _loadedClips = new List<AudioClip>();
        _questionTitles = new List<string>();
        _questionImages = new List<Sprite>();
        _yesCounter = 0;
        audioPlayer.ResetQuizz();
        FillAudio(startingCategory);
        // Set the Prompt UI
        promptUI.SetActive(true);
        quizzUI.SetActive(false);
        scoreUI.SetActive(false);
        // Debug: play 0 index audio: category title
        ArabicFixer.Fix(_questionTitles[0], false, false);
        PlayQuestion(0);

        // Analytics stuff: Log Screen
        GoogleAnalyticsV4 GA = GoogleAnalyticsV4.instance;
        if (GA != null)
        {
            GA.LogScreen("Category " + GameData.GAME_CATEGORY.ToString());
        }
    }

    private void FillAudio(CategoryName category)
    {
        switch (category)
        {
            case CategoryName.Threatening:
                foreach (AudioClip clip in threatQuestions.questionClips)
                {
                    _loadedClips.Add(clip);
                }
                foreach (string question in threatQuestions.questionTitle)
                {
                    _questionTitles.Add(question);
                }
                foreach (Sprite question in threatQuestions.questionImages)
                {
                    _questionImages.Add(question);
                }
                break;
            case CategoryName.Emotional:
                foreach (AudioClip clip in emotionalQuestions.questionClips)
                {
                    _loadedClips.Add(clip);
                }
                foreach (string question in emotionalQuestions.questionTitle)
                {
                    _questionTitles.Add(question);
                }
                foreach (Sprite question in emotionalQuestions.questionImages)
                {
                    _questionImages.Add(question);
                }
                break;
            case CategoryName.Physical:
                foreach (AudioClip clip in physicalQuestions.questionClips)
                {
                    _loadedClips.Add(clip);
                }
                foreach (string question in physicalQuestions.questionTitle)
                {
                    _questionTitles.Add(question);
                }
                foreach (Sprite question in physicalQuestions.questionImages)
                {
                    _questionImages.Add(question);
                }
                break;
            case CategoryName.Sexual:
                foreach (AudioClip clip in sexualQuestions.questionClips)
                {
                    _loadedClips.Add(clip);
                }
                foreach (string question in sexualQuestions.questionTitle)
                {
                    _questionTitles.Add(question);
                }
                foreach (Sprite question in sexualQuestions.questionImages)
                {
                    _questionImages.Add(question);
                }
                break;
            default:
                break;
        }
    }

    private void PlayQuestion(int value)
    {
        audioPlayer.ResetQuizz();
        if (value < _loadedClips.Count)
        {
            audioPlayer.SetQuestionAndPlay(_loadedClips[value]);
            logoUI.sprite = _questionImages[value];
            if (value < _questionTitles.Count)
            {
                questionTitleText.text = ArabicFixer.Fix(_questionTitles[value], false, false);
            }
        }
        else
        {
            EndGame();
        }
        
    }

    // public methods

    public void EndGame()
    {
        // we ended the round and shall show End prompt
        audioPlayer.StopAudio();

        questionTitleText.enabled = false;
        logoUI.enabled = false;
        quizzUI.SetActive(false);
        promptUI.SetActive(false);
        scoreUI.SetActive(true);
        
        Debug.Log("Total Yes: " + _yesCounter + ", Loaded Clips: " + _loadedClips.Count);
        float score = Mathf.RoundToInt((_yesCounter / _loadedClips.Count) * 100);
        Debug.Log("Score: " + score + "%");
        scoreText.text = score.ToString() + "%";

        // Anylitcs stuff
        GoogleAnalyticsV4 GA = GoogleAnalyticsV4.instance;
        if (GA != null)
        {
            GA.LogEvent("Complete " + GameData.GAME_CATEGORY, "User Id; " + GameData.USER_ID, score.ToString() + "%", 1);
        }
    }
    public void AddAnswer(bool answer)
    {
        if (answer)
        {
            _yesCounter++;
            Debug.Log("Yes Counted");
        }

        // should play next
        _questionCounter++;
   
        // TODO: Switch question text or whatever
        PlayQuestion(_questionCounter);
    }

    public void StartQuizz()
    {
        // change the UI Setup and set play Question 1
        promptUI.SetActive(false);
        quizzUI.SetActive(true);

        // setup the first question
        PlayQuestion(_questionCounter);
    }

    public void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
