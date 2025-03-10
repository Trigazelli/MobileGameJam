using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{

    public static PoolController instance;

    [SerializeField] private int numberOfGo;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private Ending ending;

    [SerializeField] private List<GameObject> goPool = new();

    private float timer;
    private ProjectileLauncherController currentBall;
    private int currentBallIndex;
    private bool lastBall;
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
        BallsLeft = goPool.Count;
        currentBallIndex = 0;
        foreach (GameObject go in goPool)
        {
            go.SetActive(false);
            go.transform.position = spawnPos.position;
        }
        currentBall = goPool[currentBallIndex].GetComponent<ProjectileLauncherController>();
        goPool[currentBallIndex].SetActive(true);
        currentBall.onDisable += ActivateNextBall;
        currentBall.onDie += DieSuddenly;
    }

    private void DieSuddenly()
    {
        StartCoroutine(ending.Lose());
    }

    private void ActivateNextBall()
    {
        BallsLeft -= 1;
        currentBallIndex += 1;
        if (currentBallIndex >= goPool.Count || lastBall) return;
        if (currentBallIndex == goPool.Count - 1) lastBall = true;
        Debug.Log("activating next ball, index is " + currentBallIndex + ".");
        goPool[currentBallIndex].SetActive(true);
        currentBall.onDisable -= ActivateNextBall;
        currentBall.onDie -= DieSuddenly;
        currentBall = goPool[currentBallIndex].GetComponent<ProjectileLauncherController>();
        currentBall.onDisable += ActivateNextBall;
        currentBall.onDie += DieSuddenly;
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
