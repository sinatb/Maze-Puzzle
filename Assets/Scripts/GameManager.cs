using System;
using System.Collections;
using System.Collections.Generic;
using PCG;
using Pooling;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [HideInInspector]
    public List<Cullable> cullables;
    public ObjectPool     pool;
    public float          cullingDistance;

    
    [SerializeField] private Generator  generator;
    [SerializeField] private GameObject player;
    private void Awake()
    {
        if (Instance == null){
            Instance = this;
        }
        else {
            Destroy(Instance);
        }
    }
    public static PlayerState GetPlayerState()
    {
        return Instance.player.GetComponent<PlayerState>();
    }

    public static Vector3 GetPlayerCoordinates()
    {
        return Instance.player.transform.position;
    }

    private IEnumerator Start()
    {
        yield return new WaitUntil(()=> pool.isReady);
        generator.Generate();
    }

    private void Update()
    {
        foreach(var cullable in cullables)
        {
            cullable.gameObject.SetActive(!(Vector3.Distance(cullable.transform.position, player.transform.position) >
                                            cullingDistance));
        }
    }
}
