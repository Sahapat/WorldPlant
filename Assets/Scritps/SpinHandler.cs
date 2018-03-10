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

    public float speed = 10f;

    private Vector3 RotateRight = new Vector3(0, 0, 1);
    private Vector3 RotateLeft = new Vector3(0, 0, -1);

    void Update()
    {
        transform.Rotate(RotateRight, speed * Time.deltaTime, Space.World);
    }
}
