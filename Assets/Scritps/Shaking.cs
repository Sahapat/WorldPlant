using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaking : MonoBehaviour
{
    [Header("PlanetObject")]
    [SerializeField]
    private GameObject planetObj;
    private WaitForSeconds waitShake;
    
    private void Awake()
    {
        waitShake = new WaitForSeconds(0.01f);
    }
    public void Shake()
    {
        StartCoroutine(startShake());
    }
    private IEnumerator startShake()
    {
        for(int i = 0;i<10;i++)
        {
            Vector2 rotationPlanet = Random.insideUnitCircle;
            Vector2 rotationCamera = Random.insideUnitCircle;
            planetObj.transform.rotation = Quaternion.Euler(new Vector3(rotationPlanet.x*2,0, rotationPlanet.y*2));
            transform.rotation = Quaternion.Euler(new Vector3(rotationCamera.x, 0, rotationCamera.y));
            yield return waitShake;
        }
    }
}
