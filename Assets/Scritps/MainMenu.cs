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
    private GameObject GroupOfBtn;
    [SerializeField]
    private GameObject txtHeader;
    [SerializeField]
    private GameObject ButtonSound;

    [Header("Resource")]
    [SerializeField]
    private Sprite[] soundShowing;
    [Header("Other")]
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

    private WaitForSeconds moveForSecond;
    private Image soundImage;
    private bool isStartGame = false;
    private void Awake()
    {
        moveForSecond = new WaitForSeconds(0.01f);
        soundImage = ButtonSound.GetComponent<Image>();
    }
    private void Start()
    {
        greenEarthMat.color = greenEarthColor;
        blueEarthMat.color = blueEarthColor;
    }
    private void Update()
    {
        EarthObj.transform.Rotate(Vector3.forward, 10 * Time.deltaTime, Space.World);

        if(Main.playerStatus.isOpenSound)
        {
            soundImage.sprite = soundShowing[0];
        }
        else
        {
            soundImage.sprite = soundShowing[1];
        }
    }
    public void StartGame()
    {
        if (!isStartGame)
        {
            StartCoroutine(TransitionAndStartGame(1));
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Tree()
    {

    }
    private IEnumerator TransitionAndStartGame(int index)
    {
        for(int i =0;i<30;i++)
        {
            GroupOfBtn.transform.position = new Vector3(GroupOfBtn.transform.position.x, GroupOfBtn.transform.position.y - 27f, GroupOfBtn.transform.position.z);
            txtHeader.transform.position = new Vector3(txtHeader.transform.position.x, txtHeader.transform.position.y + 10f, txtHeader.transform.position.z);
            EarthObj.transform.position = new Vector3(EarthObj.transform.position.x, EarthObj.transform.position.y + 0.012f, EarthObj.transform.position.z);
            yield return moveForSecond;
        }
        EarthObj.transform.position = new Vector3(0, 2.5f, 0);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(index);
        isStartGame = true;
    }
}
