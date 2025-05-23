using System.Collections;
using System.Collections.Generic;
using PCG;
using Pooling;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public ObjectPool     pool;
    public float          cullingDistance;
    private List<Cullable> _cullables;
    
    [SerializeField] private Generator  generator;
    [SerializeField] private GameObject player;
    private void Awake()
    {
        _cullables = new List<Cullable>();
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
    public void AddCullable(Cullable c)
    {
        _cullables.Add(c);
    }
    private void Update()
    {
        foreach(var cullable in _cullables)
        {
            if (Vector3.Distance(cullable.transform.position, player.transform.position) > cullingDistance)
                cullable.Cull();
            else
                cullable.UnCull();
        }
    }
}
