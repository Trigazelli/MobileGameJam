using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{

    public static PoolController instance;

    [SerializeField] private int numberOfGo;
    [SerializeField] private Transform spawnPos;


    [SerializeField] private List<GameObject> goPool = new();

    private float timer;
    private ProjectileLauncherController currentBall;
    private int currentBallIndex;
    public float BallsLeft {get; private set;}

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        BallsLeft = 0;
        currentBallIndex = 0;
        foreach (GameObject go in goPool)
        {
            go.SetActive(false);
            go.transform.position = spawnPos.position;
        }
        currentBall = goPool[currentBallIndex].GetComponent<ProjectileLauncherController>();
        goPool[currentBallIndex].SetActive(true);
        currentBall.onDisable += ActivateNextBall;
    }

    private void ActivateNextBall()
    {
        BallsLeft -= 1;
        currentBallIndex += 1;
        if (currentBallIndex >= goPool.Count) return;
        Debug.Log("activating next ball, index is " + currentBallIndex + ".");
        currentBall.onDisable -= ActivateNextBall;
        currentBall = goPool[currentBallIndex].GetComponent<ProjectileLauncherController>();
        currentBall.onDisable += ActivateNextBall;
        goPool[currentBallIndex].SetActive(true);
    }

    private GameObject GetPooledObject()
    {
        foreach (GameObject prefab in goPool)
        {
            if (!prefab.activeInHierarchy) return prefab;
        }
        return null;
    }

    public void InitPrefab()
    {
        GameObject enabledprefab = GetPooledObject();
        if (enabledprefab != null)
        {
            enabledprefab.transform.position = transform.position;
            enabledprefab.SetActive(true);
            return;
        }
    }
}
