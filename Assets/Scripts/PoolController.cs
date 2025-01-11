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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        foreach (GameObject go in goPool)
        {
            go.transform.position = spawnPos.position;
            go.SetActive(false);
        }
        goPool[0].SetActive(true);
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
