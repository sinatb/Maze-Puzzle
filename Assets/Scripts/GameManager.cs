using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlayerState PlayerState;
    private void Awake()
    {
        if (Instance == null){
            Instance = this;
        }
        else {
            Destroy(Instance);
        }
    }
}
