using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class PowerUp : MonoBehaviour
{
    public Rigidbody2D body;
    private bool isSpeed = false;
    [SerializeField] private ProjectileLauncherController projectileLauncherController;
    [SerializeField] private float speed = 50f;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Speed();
        }
    }

    void Speed()
    {
        if (body.bodyType == RigidbodyType2D.Dynamic && !isSpeed && projectileLauncherController != null && projectileLauncherController.IsThrowed)
        {
            isSpeed = true;
            body.linearVelocity = new Vector2(speed, 0);
        }
        else if (projectileLauncherController == null)
        {
            Debug.LogError("ProjectileLauncherController n'est pas assigné.");
        }
    }
}
