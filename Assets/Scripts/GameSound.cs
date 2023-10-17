using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSound : MonoBehaviour
{
    [Header("AudioClips")]
    public AudioClip mainMenu;
    public AudioClip result;
    public List<AudioClip> gamePlay_AudioList;

    public AudioClip swipeSFX1;
    public AudioClip swipeSFX2;
    public AudioClip swipeSFX3;
    public AudioClip swipeSFX4;
    public AudioClip swipeSFX5;
    public AudioClip swipeSFX6;

    public AudioClip playerHit;
    public AudioClip playerAttack1;
    public AudioClip playerAttack2;
    public AudioClip playerDead;
    public AudioClip monsterDead;
    [Header("AudioSource")]
    public AudioSource BGM_AS;
    public AudioSource playerSound_AS1;
    public AudioSource playerSound_AS2;
    public AudioSource monsterSound_AS;
    public AudioSource UI_AS1;
    public AudioSource UI_AS2;

    public float BGM_Volume=1.0f;
    public float SFX_Volume=1.0f;

    private static GameSound instance;

    private void FixedUpdate()
    {
        VolumeSet();
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    static public GameSound Instance()
    {
        return instance;
    }
    private void VolumeSet()
    {
        BGM_Volume = GameInfo.gamedata.BGM_Volume;
        SFX_Volume = GameInfo.gamedata.SFX_Volume;

        BGM_AS.volume = BGM_Volume;
        playerSound_AS1.volume = SFX_Volume;
        playerSound_AS2.volume = SFX_Volume;
        monsterSound_AS.volume = SFX_Volume;
        UI_AS1.volume = SFX_Volume;
        UI_AS2.volume = SFX_Volume;
    }

    public void PlayBGM(AudioClip audio)
    {
        BGM_AS.clip = audio;
        BGM_AS.loop = true;
        BGM_AS.Play();
    }
    public void PauseBGM()
    {
        BGM_AS.Pause();
    }
    public void ResumeBGM()
    {
        BGM_AS.Play();
    }
    public void PlayRandomInGameBGM()
    {
        if (gamePlay_AudioList.Count > 0)
        {
            int randNum = Random.Range(0, gamePlay_AudioList.Count);
           PlayBGM(gamePlay_AudioList[randNum]);
        }
        else
        {
            Debug.Log("BGM list is EMPTY");
        }
    }
    public void ShootPlayerSound1(AudioClip audio)
    {
        playerSound_AS1.PlayOneShot(audio, SFX_Volume);
    }
    public void ShootPlayerSound2(AudioClip audio)
    {
        playerSound_AS2.PlayOneShot(audio, SFX_Volume);
    }
    public void ShootMonsterSound(AudioClip audio)
    {
        monsterSound_AS.PlayOneShot(audio, SFX_Volume);
    }
    public void ShootUISound1(AudioClip audio)
    {
        UI_AS1.PlayOneShot(audio, SFX_Volume);
    }
    public void ShootUISound2(AudioClip audio)
    {
        UI_AS2.PlayOneShot(audio, SFX_Volume);
    }

}
