using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField]
    private Transform EarthObj;
    [SerializeField]
    private Image SoundImage;
    [SerializeField]
    private Text txtLevel;
    [SerializeField]
    private Text txtScore;
    [SerializeField]
    private Text txtTotalScore;
    [SerializeField]
    private GameObject endGame;
    [SerializeField]
    private GameObject credit;
    [SerializeField]
    private Text wholeScore;

    [Header("ScriptsReference")]
    [SerializeField]
    private Shaking shakeScripts;
    [SerializeField]
    private UIPlantRequire uIPlantRequireScripts;
    [SerializeField]
    private SpinHandler spinHandlerScripts;
    [SerializeField]
    private FireSeed fireSeedScripts;
    [SerializeField]
    private EarthLevelCreator earthLevelCreatorScripts;

    [Header("Resource")]
    [SerializeField]
    private AudioClip plantSound;
    [SerializeField]
    private AudioClip successSound;
    [SerializeField]
    private AudioClip failSound;
    [SerializeField]
    private Sprite[] soundSpriteRef;
    [SerializeField]
    private Animator gameOverAnim;
    [SerializeField]
    private Animator finishAnim;
    [SerializeField]
    private AudioSource backgroundAudiosource;

    [Header("TextColor")]
    [SerializeField]
    private Color dangerColor;
    [SerializeField]
    private Color normalColor;

    private WaitForSeconds treeGrowningForSecond;
    private AudioSource audioSource;
    public bool isGameEnd;
    public bool isGamePass;

    private int score;
    
    private void Awake()
    {
        treeGrowningForSecond = new WaitForSeconds(0.01f);
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        endGame.SetActive(false);
        credit.SetActive(false);
    }
    private void Update()
    {
        if (Main.playerStatus.isOpenSound)
        {
            SoundImage.sprite = soundSpriteRef[0];
            audioSource.mute = false;
            backgroundAudiosource.mute = false;
        }
        else
        {
            SoundImage.sprite = soundSpriteRef[1];
            audioSource.mute = true;
            backgroundAudiosource.mute = true;
        }
        txtLevel.text = "Level " + (Main.playerStatus.LevelType1 + 1) + "-" + (Main.playerStatus.LevelType2 + 1);
        txtLevel.color = (Main.playerStatus.LevelType2 > 6) ? dangerColor : normalColor;
        txtScore.text = score.ToString();
    }
    public void FinishPlant()
    {
        spinHandlerScripts.Finish();
        if (Main.playerStatus.LevelType1 >= 9 && Main.playerStatus.LevelType2 >= 9)
        {
            endGame.SetActive(true);
            wholeScore.text = score.ToString();
        }
        else
        {
            finishAnim.SetBool("isFinish", true);
        }
        fireSeedScripts.setVisible(false);
        isGameEnd = true;
        isGamePass = true;
        audioSource.PlayOneShot(successSound);
    }
    public void NextLevel()
    {
        if (!(Main.playerStatus.LevelType1 >= 9 && Main.playerStatus.LevelType2 >= 9))
        {
            earthLevelCreatorScripts.upLevel();
            finishAnim.SetBool("isFinish", false);
            fireSeedScripts.setVisible(true);
            isGameEnd = false;
            isGamePass = false;
        }
    }
    public void Plant(GameObject spawnObj)
    {
        audioSource.PlayOneShot(plantSound);
        score++;
        GameObject temp = Instantiate(spawnObj, EarthObj.position, Quaternion.identity);
        temp.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        temp.transform.parent = EarthObj.transform;
        temp.transform.position = Vector3.zero;
        uIPlantRequireScripts.PlantAndUpdate();
        if (spinHandlerScripts.currentSpeed > 100)
        {
            temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y + 1.2f, temp.transform.position.z);
        }
        else
        {
            StartCoroutine(Growning(temp));
        }
    }
    public void openCredit()
    {
        credit.SetActive(true);
    }
    public void closeCredit()
    {
        credit.SetActive(false);
    }
    public void cantPlant()
    {
        audioSource.PlayOneShot(failSound);
        backgroundAudiosource.volume = 0;
        shakeScripts.Shake();
        gameOverAnim.SetBool("isOver", true);
        spinHandlerScripts.EndGame();
        isGameEnd = true;
        txtTotalScore.text = score.ToString();
    }
    private IEnumerator Growning(GameObject temp)
    {
        for (int i = 0; i < 10; i++)
        {
            try
            {
                temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y + 0.12f, temp.transform.position.z);
            }
            catch(MissingReferenceException)
            {
                break;
            }
            yield return treeGrowningForSecond;
        }
    }
}
