using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;
using SonicBloom.Koreo.Players;
using System.Runtime.CompilerServices;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private SimpleMusicPlayer _koreoMusicPlayer;
    
    [SerializeField]
    private Koreography _koreographer1;
    [SerializeField]
    private Koreography _koreographer2;
    private AudioSource _mainAudioSource;

    [Space]
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private List<Transform> _musicTracks;
    [SerializeField]
    private List<Transform> _sfxTracks;
    [SerializeField]
    private List<Transform> _eventTracks;

    private void Start()
    {
        _mainAudioSource = GetComponent<AudioSource>();
        _koreoMusicPlayer = GetComponent<SimpleMusicPlayer>();
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        _koreoMusicPlayer.LoadSong(_koreographer1, 0, false);
        _koreoMusicPlayer.Play();
        _mainAudioSource.loop = true;
        StartCoroutine(NextTrackEvent());
    }
    IEnumerator NextTrackEvent()
    {
        //Get the audioSouces length from track
        //and wait for the length of the track
        float trackLength = _mainAudioSource.clip.length;
        yield return new WaitForSeconds(trackLength * 0.5f);
        _koreoMusicPlayer.LoadSong(_koreographer2, 0, false);
        _koreoMusicPlayer.Play();
    }
    private void OnDisable()
    {
        
    }

    void Update()
    {
        
    }
}
