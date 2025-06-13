using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGeneration : MonoBehaviour
{

    private int lastEnvironmentIndex = 0;
    private List<GameObject> onScreenEnvironmentList = new List<GameObject>();
    private float oneEnvironmentLength = 100f;
    private int noOfEnvironmentsOnScreen = 5;
    private float nextPlacingEnvironmentZ = 0f;
    private bool isFirstTime = true;
    private float spawnNextEnvironmentWhenPass = 40f;

    public Transform playerTransform;
    public GameObject[] environmentList;

    void Start()
    {
        for(int i = 0; i < noOfEnvironmentsOnScreen; i++)
        {
            SpawnEnvironment();
        }
    }

    void Update()
    {
        
        if(playerTransform.position.z > spawnNextEnvironmentWhenPass)
        {
            SpawnEnvironment();
            spawnNextEnvironmentWhenPass += oneEnvironmentLength;
        }

        if(
            onScreenEnvironmentList.Count > noOfEnvironmentsOnScreen + 1
            )
        {
            DeleteEnvironment();
        }

    }

    void SpawnEnvironment()
    {
        int index;
        if (isFirstTime)
        {
            index = 0;
            isFirstTime = false;
        }
        else
        {
            do
            {
                index = Random.Range(0, environmentList.Length);
            } while (index == lastEnvironmentIndex);

            lastEnvironmentIndex = index;
        }

        GameObject environment = Instantiate(environmentList[index],Vector3.forward * nextPlacingEnvironmentZ,Quaternion.identity);
        onScreenEnvironmentList.Add(environment);

        nextPlacingEnvironmentZ += oneEnvironmentLength;

    }

    void DeleteEnvironment()
    {
        Destroy(onScreenEnvironmentList[0]);
        onScreenEnvironmentList.RemoveAt(0);
    }
}
