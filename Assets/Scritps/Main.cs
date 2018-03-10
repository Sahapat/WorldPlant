using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GameStatus
{
    public bool isOpenSound;
    private int _LevelType1;
    public int LevelType1
    {
        get
        {
            return _LevelType1;
        }
        set
        {
            _LevelType1 = value;
            LevelType2 = 0;
        }
    }
    private int _LevelType2;
    public int LevelType2
    {
        get
        {
            return _LevelType2;
        }
        set
        {
            _LevelType2 = value;
            if(_LevelType2 < 0)
            {
                LevelType1--;
                _LevelType2 = 10;
            }
            else if(_LevelType2 > 10)
            {
                LevelType1++;
                _LevelType2 = 0;
            }
        }
    }
    public int Score;
}
public class Main : MonoBehaviour
{
    public static GameStatus playerStatus;
    public void ResetGame()
    {
        playerStatus.LevelType1 = 0;
        playerStatus.Score = 0;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
    public void SwitchSoundStatus()
    {
        playerStatus.isOpenSound = !playerStatus.isOpenSound;
    }
}
