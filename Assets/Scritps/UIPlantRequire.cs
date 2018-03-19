using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class UIPlantRequire : MonoBehaviour
{
    [Header("ImageObject")]
    [SerializeField]
    private Image[] requires;

    [Header("Resource")]
    [SerializeField]
    private Sprite[] PlantState;
    [SerializeField]
    private Material[] earthMaterials;

    [Header("Scripts")]
    [SerializeField]
    private EarthLevelCreator earthLevelHandlerScript;
    [SerializeField]
    private GameController gameControllerScripts;

    [Header("Other")]
    [SerializeField]
    private Color endGreenColor;
    [SerializeField]
    private Color endBlueColor;

    private int require = 0;
    private int playerPlant = 0;

    private void Start()
    {
        ResetEarth();
    }
    public void UpdateUI(int require)
    {
        this.require = require;
        for(int i =0;i<requires.Length;i++)
        {
            if(i < require)
            {
                requires[i].color = new Color(255, 255, 255, 255);
                requires[i].sprite = PlantState[0];
            }
            else
            {
                requires[i].color = new Color(0, 0, 0, 0);
            }
        }
    }
    public void ResetEarth()
    {
        earthMaterials[0].color = Color.white;
        earthMaterials[1].color = Color.white;
        playerPlant = 0;
    }
    public void PlantAndUpdate()
    {
        playerPlant += 1;
        for (int i = 0; i < require; i++)
        {
            if(i < playerPlant)
            {
                requires[i].sprite = PlantState[1];
            }
            else
            {
                requires[i].sprite = PlantState[0];
            }
        }
        if (playerPlant >= require)
        {
            earthMaterials[0].color = endGreenColor;
            earthMaterials[1].color = endBlueColor;
            gameControllerScripts.FinishPlant();
            for(int i = 0;i<requires.Length;i++)
            {
                requires[i].color = new Color(0, 0, 0, 0); 
            }
        }
    }
}
