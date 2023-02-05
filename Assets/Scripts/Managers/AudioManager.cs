using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;
using SonicBloom.Koreo.Players;
using System.Runtime.CompilerServices;

public class AudioManager : MonoBehaviour
{
    //Use Call the instance and obtain a koreoStage track and return that to this isntance and play it.
    //to call LoadKoreoTrack() must obtain Track using the instance and passing that to LoadKoreoTrack();
    //null check outside this skript for missing tracks.
    public static AudioManager Instance;

    private SimpleMusicPlayer _koreoMusicPlayer;
    private AudioSource _mainAudioSource;
    
    [SerializeField]
    private Koreography _koreoStage1;
    public Koreography KoreoStage1 { get { return _koreoStage1; } private set { } }
    [SerializeField]
    private Koreography _koreoStage2;
    public Koreography KoreoStage2 { get { return _koreoStage2; } private set { } }
    
    [SerializeField]
    private Koreography _koreoStage3;
    public Koreography KoreoStage3 { get { return _koreoStage3; } private set { } }
    [SerializeField]
    private Koreography _koreoStage4;
    public Koreography KoreoStage4 { get { return _koreoStage4; } private set { } }
    [SerializeField]
    private Koreography _koreoStage5;
    public Koreography KoreoStage5 { get { return _koreoStage5; } private set { } }

    [Space]
    [SerializeField]
    private List<Transform> _sfxTracks;
    

    [Space]
    [SerializeField]
    private AudioSource _audioSource;

    [Space]
    private float _musicTrackLegth = 0;

    private void Start()
    {
        _mainAudioSource = GetComponent<AudioSource>();
        _koreoMusicPlayer = GetComponent<SimpleMusicPlayer>();
        
        ManagerInstance();
    }
    private void ManagerInstance()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void LoadKoreoTrack(Koreography koreoTrack, int bpmTrack, bool autoPlay)
    {
        _koreoMusicPlayer.LoadSong(koreoTrack, bpmTrack, autoPlay);
    }
    public void PlayKoreoTrack(bool isLooping)
    {
        _mainAudioSource.loop = isLooping;
        _koreoMusicPlayer.Play();
        _musicTrackLegth = GetSongLength();
    }
    public float GetSongLength()
    {
        if (_audioSource == null) return 0;
        float clip = _audioSource.clip.length;
        return clip;
    }
}
