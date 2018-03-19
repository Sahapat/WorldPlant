using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [Header("GameObject")]
    [SerializeField]
    private GameObject EarthObj;
    [SerializeField]
    private GameObject ButtonSound;

    [Header("Resource")]
    [SerializeField]
    private Sprite[] soundsShowing;

    [Header("Other")]
    [SerializeField]
    private Animator treeAnim;
    [SerializeField]
    private Animator menuAnim;
    [SerializeField]
    private Animator fadeAnim;
    [SerializeField]
    private Color greenEarthColor;
    [SerializeField]
    private Color blueEarthColor;
    [SerializeField]
    private Material greenEarthMat;
    [SerializeField]
    private Material blueEarthMat;

    private AudioSource audioSource;
    private Image soundImage;

    private void Awake()
    {
        soundImage = ButtonSound.GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        greenEarthMat.color = greenEarthColor;
        blueEarthMat.color = blueEarthColor;
        Main.playerStatus.isOpenSound = true;
    }
    private void Update()
    { 
        EarthObj.transform.Rotate(new Vector3(0, 0, 1), 10f* Time.deltaTime, Space.World);
        if(Main.playerStatus.isOpenSound)
        {
            soundImage.sprite = soundsShowing[0];
            audioSource.mute = false;
        }
        else
        {
            soundImage.sprite = soundsShowing[1];
            audioSource.mute = true;
        }
    }
    public void StartGame()
    {
        fadeAnim.SetTrigger("FadeOut");
        StartCoroutine(TransitionAndStartGame(1));
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void openTree()
    {
        menuAnim.SetBool("isClose", true);
        treeAnim.SetBool("isOpen", true);
    }
    public void closeTree()
    {
        menuAnim.SetBool("isClose", false);
        treeAnim.SetBool("isOpen", false);
    }
    private IEnumerator TransitionAndStartGame(int index)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(index);
    }
}
