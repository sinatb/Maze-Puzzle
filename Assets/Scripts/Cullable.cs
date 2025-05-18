using System;
using UnityEngine;

public class Cullable : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.cullables.Add(this);
    }
}
