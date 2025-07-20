using System.Collections.Generic;
using UnityEngine;

public class AudioService : MonoBehaviour
{
    static public AudioService Instance;

    [SerializeField] private string _soundtracksPropertyName;
    [SerializeField] private string _soundEffectsPropertyName;
    [SerializeField] private AudioSource _soundtracksSource;
    [SerializeField] private AudioSource _soundEffectsSource;

    [SerializeField] private List<AudioClip> _clips;
    
    private int _currentClipIndex = 0;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
        else
            Destroy(gameObject);
    }

    private void Init()
    {
        if (_clips.Count > 0)
        {
            _soundtracksSource.clip = _clips[0];
            _soundEffectsSource.volume = PlayerPrefs.GetFloat(_soundEffectsPropertyName, 1f);
            _soundtracksSource.volume = PlayerPrefs.GetFloat(_soundtracksPropertyName, 1f);
            _soundtracksSource.Play();
        }
    }


    private void Update()
    {
        if (IsTrackEnd())
            ChangeTrack();
    }

    public void ChangeVolumeSoundtracks(float value)
    {
        _soundtracksSource.volume = value;
    }

    public void ChangeVolumeSoundEffects(float value)
    {
        _soundEffectsSource.volume = value;
    }

    private void ChangeTrack()
    {
        _currentClipIndex = _currentClipIndex + 1 >= _clips.Count ? 0 : _currentClipIndex + 1;
        _soundtracksSource.clip = _clips[_currentClipIndex];
        _soundtracksSource.Play();
    }

    private bool IsTrackEnd()
    {
        if (_clips.Count > 0)
        {
            if (((_soundtracksSource.clip.length - _soundtracksSource.time) < 5f))
                return true;
        }
        return false;
    }

    public void PlayEffect(AudioClip clip)
    {
        _soundEffectsSource.PlayOneShot(clip);
    }

    public void SoundPause()
    {
        _soundtracksSource.Pause();
    }

    public void SoundUnPause()
    {
        _soundtracksSource.UnPause();
    }

}
