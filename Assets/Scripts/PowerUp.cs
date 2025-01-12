using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class PowerUp : MonoBehaviour
{
    public Rigidbody2D body;
    private bool hasBeenPoweredUp = false;
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
        if (hasBeenPoweredUp) return;
        Debug.Log("Powered up");
        hasBeenPoweredUp = true;
        body.linearVelocity += new Vector2(speed, 0);
    }
}
