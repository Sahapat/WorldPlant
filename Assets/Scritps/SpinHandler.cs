using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinHandler : MonoBehaviour
{
    [Header("SpawnObject")]
    [SerializeField]
    private GameObject[] bushsObj;
    [SerializeField]
    private GameObject[] treesObj;
    [SerializeField]
    private GameObject[] jewelsObj;

    [Header("ScriptsReference")]
    [SerializeField]
    private GameController gameControllerScripts;

    [Header("Particles")]
    [SerializeField]
    private GameObject SuccessParticle;
    [SerializeField]
    private GameObject FailParticle;

    public float speed = 20f;

    private Vector3 RotateRight = new Vector3(0, 0, 1);
    private Vector3 RotateLeft = new Vector3(0, 0, -1);

    private float speedSpinDuration = 3f;
    private float speedSpinCount;

    private float spinAndTwistDuration = 2f;
    private byte twistCount = 0;
    private float spinAndTwistCount;

    private float normalSpinDuration = 5f;
    private float normalSpinCount;

    private byte[] Spinhirarchy;
    public float currentSpeed;
    private int runIndex;

    private byte spinDirection; 
    private float decreastSpeed;

    private void Start()
    {
        Spinhirarchy = new byte[1];
        Spinhirarchy[0] = 0;
        SuccessParticle.SetActive(false);
        FailParticle.SetActive(false);
        normalSpinCount = normalSpinDuration;
        speedSpinCount = speedSpinDuration;
        spinAndTwistCount = spinAndTwistDuration;
    }
    void Update()
    {
        if (!gameControllerScripts.isGameEnd)
        {
            runEarthSpin();
        }
        else if (gameControllerScripts.isGamePass)
        {
            Finish();
            SuccessParticle.SetActive(true);
        }
        if (currentSpeed < 1)
        {
            normalSpinCount = normalSpinDuration;
            speedSpinCount = speedSpinDuration;
            spinAndTwistCount = spinAndTwistDuration;
            twistCount = 0;
            spinDirection = (byte)Random.Range(0, 2);
            runIndex = Random.Range(0, Spinhirarchy.Length);
            while (Spinhirarchy.Length >= 3)
            {
                int currentIndex = runIndex;
                runIndex = Random.Range(0, Spinhirarchy.Length);
                if (currentIndex != runIndex)
                {
                    break;
                }
            }
        }
    }
    public void setEarthSpin(byte[] spinHirarchy, float spinSpeedSet)
    {
        ResetEarthSpin();
        this.Spinhirarchy = spinHirarchy;
        this.speed = spinSpeedSet;
    }
    public void setEarthSpin(byte[] spinHirarchy, float spinSpeedSet, float SpinDuration)
    {
        ResetEarthSpin();
        this.Spinhirarchy = spinHirarchy;
        this.speed = spinSpeedSet;
        this.normalSpinDuration = SpinDuration;
    }
    public void setEarthSpin(byte[] spinHirarchy, float spinSpeedSet, float SpinDuration, float SpinAndStopDuration)
    {
        ResetEarthSpin();
        this.Spinhirarchy = spinHirarchy;
        this.speed = spinSpeedSet;
        this.normalSpinDuration = SpinDuration;
        this.speedSpinDuration = SpinAndStopDuration;
    }
    public void setEarthSpin(byte[] spinHirarchy, float spinSpeedSet, float SpinDuration, float SpinAndStopDuration, float SpinAndTwistDuration)
    {
        ResetEarthSpin();
        this.Spinhirarchy = spinHirarchy;
        this.speed = spinSpeedSet;
        this.normalSpinDuration = SpinDuration;
        this.speedSpinDuration = SpinAndStopDuration;
        this.spinAndTwistDuration = SpinAndTwistDuration;
    }
    private void NormalSpin(byte direction)
    {
        currentSpeed = speed;
        decreastSpeed = (speed * 2) * Time.deltaTime;
        switch (direction)
        {
            case 0:
                transform.Rotate(RotateRight, currentSpeed * Time.deltaTime, Space.World);
                break;
            case 1:
                transform.Rotate(RotateLeft, currentSpeed * Time.deltaTime, Space.World);
                break;
        }
    }
    private void SpinAndStop(byte direction)
    {
        currentSpeed = speed * 3;
        decreastSpeed = (speed * 6) * Time.deltaTime;
        switch (direction)
        {
            case 0:
                transform.Rotate(RotateRight, currentSpeed * Time.deltaTime, Space.World);
                break;
            case 1:
                transform.Rotate(RotateLeft, currentSpeed * Time.deltaTime, Space.World);
                break;
        }
    }
    private void SpinAndTwist(byte direction)
    {
        currentSpeed = speed * 5;
        decreastSpeed = (speed * 2) * Time.deltaTime;
        switch (direction)
        {
            case 0:
                transform.Rotate(RotateRight, currentSpeed * Time.deltaTime, Space.World);
                break;
            case 1:
                transform.Rotate(RotateLeft, currentSpeed * Time.deltaTime, Space.World);
                break;
        }
    }
    private void runEarthSpin()
    {
        switch (Spinhirarchy[runIndex])
        {
            case 0:
                if (normalSpinCount <= 0)
                {
                    updateIndex();
                }
                else
                {
                    NormalSpin(spinDirection);
                    normalSpinCount -= Time.deltaTime;
                }
                break;
            case 1:
                if (speedSpinCount <= 0)
                {
                    updateIndex();
                }
                else
                {
                    SpinAndStop(spinDirection);
                    speedSpinCount -= Time.deltaTime;
                }
                break;
            case 2:
                if (spinAndTwistCount <= 0)
                {
                    twistCount++;
                    if (twistCount >= 2)
                    {
                        updateIndex();
                    }
                    else
                    {
                        spinAndTwistCount = spinAndTwistDuration;
                    }
                }
                else
                {
                    SpinAndTwist(twistCount);
                    spinAndTwistCount -= Time.deltaTime;
                }
                break;
        }
    }
    private void updateIndex()
    {
        if (Spinhirarchy.Length > 1)
        {
            currentSpeed -= decreastSpeed;
            switch (spinDirection)
            {
                case 0:
                    transform.Rotate(RotateRight, currentSpeed * Time.deltaTime, Space.World);
                    break;
                case 1:
                    transform.Rotate(RotateLeft, currentSpeed * Time.deltaTime, Space.World);
                    break;
            }
        }
        else
        {
            normalSpinCount = normalSpinDuration;
            speedSpinCount = speedSpinDuration;
            spinAndTwistCount = spinAndTwistDuration;
            twistCount = 0;
            return;
        }
    }
    public void Finish()
    {
        transform.Rotate(RotateRight, 10 * Time.deltaTime, Space.World);
    }
    public void EndGame()
    {
        FailParticle.SetActive(true);
    }
    public void ResetEarthSpin()
    {
        SuccessParticle.SetActive(false);
        FailParticle.SetActive(false);
        normalSpinCount = normalSpinDuration;
        speedSpinCount = speedSpinDuration;
        spinAndTwistCount = spinAndTwistDuration;
    }
}
