using System.Collections;
using System.Collections.Generic;
using PCG;
using Player;
using Pooling;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public ObjectPool     pool;
    public float          cullingDistance;
    private List<Cullable> _cullables;
    
    [SerializeField] private Generator  generator;
    public GameObject Player{get; private set; }
    private void Awake()
    {
        _cullables = new List<Cullable>();
        Player = GameObject.Find("Player");
        if (Instance == null){
            Instance = this;
        }
        else {
            Destroy(Instance);
        }
    }
    public static PlayerState GetPlayerState()
    {
        return Instance.Player.GetComponent<PlayerState>();
    }
    public static Vector3 GetPlayerCoordinates()
    {
        return Instance.Player.transform.position;
    }
    private IEnumerator Start()
    {
        yield return new WaitUntil(()=> pool.isReady);
        generator.Generate();
    }

    #region Culling
    public void AddCullable(Cullable c)
    {
        _cullables.Add(c);
    }
    private void Update()
    {
        foreach(var cullable in _cullables)
        {
            if (Vector3.Distance(cullable.transform.position, Player.transform.position) > cullingDistance)
                cullable.Cull();
            else
                cullable.UnCull();
        }
    }
    #endregion
}
