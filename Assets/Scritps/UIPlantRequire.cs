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

    [Header("Other")]
    [SerializeField]
    private Color endGreenColor;
    [SerializeField]
    private Color endBlueColor;

    private int require = 0;
    private int playerPlant = 0;
    private Vector3 colorGreenPerLevel;
    private Vector3 colorBluePerLevel;
    private Vector3 endColorGreen;
    private Vector3 endColorBlue;

    private void Start()
    {
        earthMaterials[0].color = Color.white;
        earthMaterials[1].color = Color.white;
        endColorGreen = new Vector3(endGreenColor.r, endGreenColor.g, endGreenColor.b);
        endColorBlue = new Vector3(endBlueColor.r, endBlueColor.g, endBlueColor.b);
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
        }
        else
        {
        }
    }
}
