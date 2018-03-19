using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthLevelCreator : MonoBehaviour
{
    [System.Serializable]
    private struct Level
    {
        public byte requireTree;
        public byte MinTreesInLevel;
        public byte MaxTreeInLevel;
        public float MinSpinSpeed;
        public float MaxSpinSpeed;
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
    private SpinHandler spinHandlerScripts;
    [SerializeField]
    private UIPlantRequire uIPlantRequireScripts;

    [Header("LevelStatus")]
    [SerializeField]
    private Level[] levels;

    private int currentSpawnTreesNum;
    private float currentSpinSet;
    private byte[] spinHirarchy;

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
        float spinSpeedSet = 0;
        if(playerLevel2 < 5)
        {
            byte maxTreeSpawn = (byte)((levels[playerLevel1].MinTreesInLevel + levels[playerLevel1].MaxTreeInLevel) / 2);
            spawnObtacleNum = Random.Range(0,maxTreeSpawn);
            spinSpeedSet = Random.Range(levels[playerLevel1].MinSpinSpeed, levels[playerLevel1].MaxSpinSpeed);
            int indexHirarchy = Random.Range(1, 4);
            spinHirarchy = new byte[indexHirarchy];

            for(int i =0;i<indexHirarchy;i++)
            {
                float probability;
                bool isGetHirarchy = false;

                if(levels[playerLevel1].isHaveSlow)
                {
                    probability = Random.value;
                    if(probability <= 0.4f)
                    {
                        isGetHirarchy = true;
                        spinHirarchy[i] = 1;
                    }
                    else
                    {
                        isGetHirarchy = false;
                    }
                }
                
                if(levels[playerLevel1].isHaveChangeRotate && !isGetHirarchy)
                {
                    probability = Random.value;
                    if(probability <= 0.4f)
                    {
                        isGetHirarchy = true;
                        spinHirarchy[i] = 2;
                    }
                    else
                    {
                        isGetHirarchy = false;
                    }
                }
                else if(!isGetHirarchy)
                {
                    spinHirarchy[i] = 0;
                }
            }
        }
        else
        {
            byte minTreeSpawn = (byte)((levels[playerLevel1].MinTreesInLevel + levels[playerLevel1].MaxTreeInLevel) / 2);
            float minSpeedInLevel = (levels[playerLevel1].MinSpinSpeed + levels[playerLevel1].MaxSpinSpeed) / 2;
            spawnObtacleNum = Random.Range(minTreeSpawn, levels[playerLevel1].MaxTreeInLevel);
            spinSpeedSet = Random.Range(levels[playerLevel1].MinSpinSpeed + minSpeedInLevel, levels[playerLevel1].MaxSpinSpeed);
            byte indexHirarchy = 3;
            spinHirarchy = new byte[indexHirarchy];

            for (int i = 0; i < indexHirarchy; i++)
            {
                float probability;
                bool isGetHirarchy = false;

                if (levels[playerLevel1].isHaveSlow)
                {
                    probability = Random.value;
                    if (probability <= 0.6f)
                    {
                        isGetHirarchy = true;
                        spinHirarchy[i] = 1;
                    }
                    else
                    {
                        isGetHirarchy = false;
                    }
                }

                if (levels[playerLevel1].isHaveChangeRotate && !isGetHirarchy)
                {
                    probability = Random.value;
                    if (probability <= 0.6f)
                    {
                        isGetHirarchy = true;
                        spinHirarchy[i] = 2;
                    }
                    else
                    {
                        isGetHirarchy = false;
                    }
                }
                else if (!isGetHirarchy)
                {
                    spinHirarchy[i] = 0;
                }
            }
        }
        this.currentSpawnTreesNum = spawnObtacleNum;
        this.currentSpinSet = spinSpeedSet;
        spinHandlerScripts.setEarthSpin(spinHirarchy, currentSpinSet);
        InitEarth();
    }
    private void ClearEarth()
    {
        foreach(Transform child in EarthObj.transform)
        {
            Destroy(child.gameObject);
        }
    }
    public void upLevel()
    {
        ClearEarth();
        Main.playerStatus.LevelType2++;
        setPropertyEarth();
        uIPlantRequireScripts.UpdateUI(levels[Main.playerStatus.LevelType1].requireTree);
        uIPlantRequireScripts.ResetEarth();
    }
    private void InitEarth()
    {
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
