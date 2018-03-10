using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthLevelHandler : MonoBehaviour
{
    [System.Serializable]
    private struct Level
    {
        public byte requireTree;
        public byte MinTreesInLevel;
        public byte MaxTreeInLevel;
        public float MinSpinSpeed;
        public float MaxSpinSpeed;
        public byte MinJewel;
        public byte MaxJewel;
        public bool isHaveSlow;
        public bool isHaveChangeRotate;
    }
    [Header("GameObject")]
    [SerializeField]
    private GameObject EarthObj;
    [SerializeField]
    private GameObject[] treeObj;

    [Header("ScriptsReference")]
    [SerializeField]
    private SpinHandler sprinHandlerScripts;
    [SerializeField]
    private UIPlantRequire uIPlantRequireScripts;

    [Header("LevelStatus")]
    [SerializeField]
    private Level[] levels;

    private int currentSpawnTreesNum;
    private int currenSpawnJewelsNum;
    private float currentSpinSet;
    private void Start()
    {
        setPropertyEarth();
        uIPlantRequireScripts.UpdateUI(levels[Main.playerStatus.LevelType1].requireTree);
    }
    private void setPropertyEarth()
    {
        int playerLevel1 = Main.playerStatus.LevelType1;
        int playerLevel2 = Main.playerStatus.LevelType2;

        int spawnObtacleNum = 0;
        int spawnJewelNum = 0;
        float spinSpeedSet = 0;
        if(playerLevel2 < 5)
        {
            byte maxTreeSpawn = (byte)((levels[playerLevel1].MinTreesInLevel + levels[playerLevel1].MaxTreeInLevel) / 2);
            byte maxJewelSpawn = (byte)((levels[playerLevel1].MinJewel + levels[playerLevel1].MaxJewel) / 2);
            float maxSpeedInLevel = (levels[playerLevel1].MinSpinSpeed + levels[playerLevel1].MaxSpinSpeed) / 2;
            spawnObtacleNum = Random.Range(0,maxTreeSpawn);
            spawnJewelNum = Random.Range(0, maxJewelSpawn);
            spinSpeedSet = Random.Range(0, maxSpeedInLevel);
        }
        else
        {
            byte minTreeSpawn = (byte)((levels[playerLevel1].MinTreesInLevel + levels[playerLevel1].MaxTreeInLevel) / 2);
            byte minJewelSpawn = (byte)((levels[playerLevel1].MinJewel + levels[playerLevel1].MaxJewel) / 2);
            float minSpeedInLevel = (levels[playerLevel1].MinSpinSpeed + levels[playerLevel1].MaxSpinSpeed) / 2;
            spawnObtacleNum = Random.Range(minTreeSpawn, levels[playerLevel1].MaxTreeInLevel);
            spawnJewelNum = Random.Range(minJewelSpawn, levels[playerLevel1].MaxJewel);
            spinSpeedSet = Random.Range(minSpeedInLevel, levels[playerLevel1].MaxSpinSpeed);
        }
        this.currentSpawnTreesNum = spawnObtacleNum;
        this.currenSpawnJewelsNum = spawnJewelNum;
        this.currentSpinSet = spinSpeedSet;

        InitEarth();
    }
    public void NextLevel()
    {

    }
    private void InitEarth()
    {
        GameObject[] treesSpawn = new GameObject[currentSpawnTreesNum];
        GameObject[] jewelsSpawn = new GameObject[currenSpawnJewelsNum];

        float currentSpawnRandomRotation = -180;

        for(int i = 0;i<currentSpawnTreesNum;i++)
        {
            float rotationSpawn =  Random.Range(currentSpawnRandomRotation, 180f);
            currentSpawnRandomRotation = rotationSpawn;
            EarthObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotationSpawn));
            GameObject temp = Instantiate(treeObj[Random.Range(0,treeObj.Length)], EarthObj.transform.position, Quaternion.identity);
            temp.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            temp.transform.parent = EarthObj.transform;
            temp.transform.position = Vector3.zero;
            temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y + (0.04f*30), temp.transform.position.z);
        }
    }
}
