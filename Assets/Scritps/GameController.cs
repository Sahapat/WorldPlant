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

    [Header("ScriptsReference")]
    [SerializeField]
    private Shaking shakeScripts;
    [SerializeField]
    private UIPlantRequire uIPlantRequireScripts;

    [Header("Resource")]
    [SerializeField]
    private Sprite[] soundSpriteRef;
    
    private WaitForSeconds treeGrowningForSecond;
    private void Awake()
    {
        treeGrowningForSecond = new WaitForSeconds(0.01f);
    }
    private void Update()
    {
        if(Main.playerStatus.isOpenSound)
        {
            SoundImage.sprite = soundSpriteRef[0];
        }
        else
        {
            SoundImage.sprite = soundSpriteRef[1];
        }
    }
    public void Plant(GameObject spawnObj)
    {
        GameObject temp = Instantiate(spawnObj, EarthObj.position, Quaternion.identity);
        temp.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        temp.transform.parent = EarthObj.transform;
        temp.transform.position = Vector3.zero;
        uIPlantRequireScripts.PlantAndUpdate();
        StartCoroutine(Growning(temp));
    }
    public void cantPlant()
    {
        shakeScripts.Shake();
    }
    public void backToHome()
    {
        SceneManager.LoadScene(0);
    }
    private IEnumerator Growning(GameObject temp)
    {
        for(int i = 0;i<30;i++)
        {
            temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y + 0.04f, temp.transform.position.z);
            yield return treeGrowningForSecond;
        }
    }
}
