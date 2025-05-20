using UnityEngine;

public class Cullable : MonoBehaviour
{
    public bool IsCulled { get; private set; }
    public void Cull()
    {
        IsCulled = true;
        gameObject.SetActive(false);
    }    
    public void UnCull()
    {
        IsCulled = false;
        gameObject.SetActive(true);
    }
    private void Start()
    {
        GameManager.Instance.AddCullable(this);
    }
}
