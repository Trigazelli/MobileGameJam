using UnityEngine;

public class MousePositionTracker : MonoBehaviour
{
    [SerializeField] private MousePositionStocker stocker;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        stocker.mousePos = mousePos;
    }
}
