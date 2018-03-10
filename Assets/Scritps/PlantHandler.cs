using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantHandler : MonoBehaviour
{
    private GameController controller;
    private Rigidbody plantRig;
    private FireSeed fireScripts;
    private GameObject plantObj;
    private bool isPlant;
    private void Awake()
    {
        plantRig = GetComponent<Rigidbody>();
    }
    public void settingReference(GameController controller)
    {
        this.controller = controller;
    }
    public void FireSeed(FireSeed fire,float shootAmount,Transform shootPos,GameObject plantObj)
    {
        isPlant = false;
        this.plantObj = plantObj;
        transform.position = shootPos.position;
        fireScripts = fire;
        plantRig.velocity = new Vector3(0, shootAmount, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isPlant)
        {
            if (other.CompareTag("Planet"))
            {
                isPlant = true;
                plantRig.velocity = Vector3.zero;
                controller.Plant(plantObj);
                fireScripts.toPoolingPos(this.gameObject);
            }
            if (other.CompareTag("Plant"))
            {
                isPlant = true;
                plantRig.velocity = Vector3.zero;
                controller.cantPlant();
                fireScripts.toPoolingPos(this.gameObject);
            }
        }
    }
}
