using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class PowerUp : MonoBehaviour
{
    public Rigidbody2D body;
    private bool isSpeed = false;
    [SerializeField] ProjectileLauncherController projectileLauncherController;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Speed();
        }
    }

    void Speed()
    {
        if (body.bodyType == RigidbodyType2D.Dynamic && !isSpeed && projectileLauncherController.IsThrowed)
        {
            Debug.Log("Speed");
            isSpeed = true;
            body.linearVelocity = new Vector2(50, 0);
        }
    }
}
