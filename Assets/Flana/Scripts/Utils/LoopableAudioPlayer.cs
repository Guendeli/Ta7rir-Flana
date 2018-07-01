using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LoopableAudioPlayer : MonoBehaviour {

    /*
     *      Loopable Audio Player utility class
     *  Plays one or more audio clips, waits a delay and replay 
     */
    public bool onStart;
    public List<AudioClip> clipsList;
    public float initDelay;
    public float interSoundDelay;
    public float replayDelay;

    private AudioSource _audioSource;
    private bool _hasStarted;
    private int _clipCounter;
    // Use this for initialization
	void Awake () {
        _audioSource = GetComponent<AudioSource>();
	}

    void Start()
    {
        if (onStart)
        {
            Invoke("InitPlay", initDelay);
        }
    }
	
	// Update is called once per frame
	void Update () {
        //CheckNextClip();
	}

    // helper methods are written here

    private void InitPlay()
    {
        _clipCounter = 0;
        if (!_audioSource.playOnAwake)
        {
            _audioSource.clip = clipsList[_clipCounter];
            _audioSource.Play();
            _hasStarted = true;
           
        }
    }

    private void CheckNextClip()
    {
        
        if(_hasStarted && !_audioSource.isPlaying)
        {
            // in case we still have clips on the list
            // increase counter and play
            _clipCounter++;
            if(_clipCounter < clipsList.Count)
            {
                _audioSource.clip = clipsList[_clipCounter];
                _audioSource.Play();
                
            } else
            {
                // in case we ended our playlist
                _hasStarted = false;
                // invoke initPlay after replayDelay
                Invoke("InitPlay", replayDelay);
                
            }
        }
    }

    // public methods here: used from QuizzManager

    public void ResetQuizz()
    {
        _hasStarted = false;
        if (_audioSource.clip != null)
        {
            if (_audioSource.isPlaying)
            {
                _audioSource.Stop();
            }
        }
        if(clipsList.Count > 0)
        {
            clipsList.Clear();
            clipsList = new List<AudioClip>();
        }
    }

    public void SetQuestionAndPlay(AudioClip clip)
    {
        clipsList.Add(clip);
        InitPlay();
    }

    public void StopAudio()
    {
        _hasStarted = false;
        if (_audioSource.clip != null)
        {
            _audioSource.Stop();
            CancelInvoke("InitPlay");
        }
    }
}
