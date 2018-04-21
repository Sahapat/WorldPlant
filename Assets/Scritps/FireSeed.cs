using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSeed : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField]
    private GameObject SeedPrefab;
    [SerializeField]
    private GameObject fireObj;
    
    [Header("Shoot property")]
    [SerializeField]
    private float shootAmount = 20f;
    [SerializeField]
    private Transform shootPos;
    [SerializeField]
    private Transform poolingPos;

    [Header("ScriptsReference")]
    [SerializeField]
    private GameController controller;
    [Header("Resource")]
    [SerializeField]
    private GameObject[] Trees;

    private GameObject[] SeedPooling;
    private PlantHandler[] SeedScripts;
    private MeshRenderer Mrenderer;
    private void Awake()
    {
        SeedPooling = new GameObject[4];
        SeedScripts = new PlantHandler[4];
        Mrenderer = fireObj.GetComponent<MeshRenderer>();
    }
    private void Start()
    {
        for(int i =0;i<4;i++)
        {
            SeedPooling[i] = Instantiate(SeedPrefab, poolingPos.position, Quaternion.identity);
            SeedScripts[i] = SeedPooling[i].GetComponent<PlantHandler>();
            SeedScripts[i].settingReference(controller);
        }
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && !controller.isGameEnd)
        {
            for(int i =0;i<SeedPooling.Length;i++)
            {
                if(SeedPooling[i].transform.position == poolingPos.position)
                {
                    SeedScripts[i].FireSeed(this, shootAmount,shootPos,Trees[Random.Range(0,Trees.Length)]);
                    break;
                }
            }
        }
        else if(Input.GetMouseButtonDown(0) && controller.isGamePass)
        {
            controller.StopAllCoroutines();
            controller.NextLevel();
        }
    }
    public void setVisible(bool status)
    {
        Mrenderer.enabled = status;
    }
    public void toPoolingPos(GameObject obj)
    {
        obj.transform.position = poolingPos.transform.position;
    }
}
