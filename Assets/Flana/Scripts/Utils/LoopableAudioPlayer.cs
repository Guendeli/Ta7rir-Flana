using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LoopableAudioPlayer : MonoBehaviour {

    /*
     *      Loopable Audio Player utility class
     *  Plays one or more audio clips, waits a delay and replay 
     */
    public List<AudioClip> clipsList;
    public float initDelay;
    public float interSoundDelay;
    public float replayDelay;

    private AudioSource _audioSource;
    private bool _hasStarted;
    private int _clipCounter;
    // Use this for initialization
	void Start () {
        Invoke("InitPlay", initDelay);	
	}
	
	// Update is called once per frame
	void Update () {
        CheckNextClip();
	}

    // helper methods are written here

    private void InitPlay()
    {
        _audioSource = GetComponent<AudioSource>();
        _clipCounter = 0;
        if (!_audioSource.playOnAwake)
        {
            _audioSource.clip = clipsList[_clipCounter];
            _audioSource.Play();
            _hasStarted = true;
            Debug.Log("Started playing Clip n° " + _clipCounter);
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
                Debug.Log("Started playing Clip n° " + _clipCounter);
            } else
            {
                // in case we ended our playlist
                _hasStarted = false;
                // invoke initPlay after replayDelay
                Invoke("InitPlay", replayDelay);
                Debug.Log("Will replay after " + replayDelay + " Seconds");
            }
        }
    }
}
