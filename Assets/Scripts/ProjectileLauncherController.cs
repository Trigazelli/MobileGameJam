using UnityEngine;

public class ProjectileLauncherController : MonoBehaviour
{
    [SerializeField] private Transform launchPoint;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private Rigidbody2D rigidBody;

    [Header("Trajectory Display")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int linePoints = 175;
    [SerializeField] private float timeIntervalPoints = 0.01f;
    
    private float launchSpeed;
    private bool isThrowed;

    public bool IsThrowed { get { return isThrowed; } }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        if (mousePosition.x > transform.position.x || isThrowed)
        {
            lineRenderer.enabled = false;
            return;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Ball throwed");
            lineRenderer.enabled = false;
            rigidBody.bodyType = RigidbodyType2D.Dynamic;
            rigidBody.linearVelocity = launchSpeed * launchPoint.up;
            isThrowed = true;
        } else if (Input.GetMouseButton(0))
        {
            Vector2 direction = new Vector2(transform.position.x - mousePosition.x, transform.position.y - mousePosition.y);
            //Debug.Log(direction);
            launchSpeed = direction.magnitude * speedMultiplier;
            transform.up = direction;
            DrawTrajectory();
        }
    }

    private void DrawTrajectory()
    {
        lineRenderer.enabled = true;
        Vector3 origin = launchPoint.position;
        Vector3 startVelocity = launchSpeed * launchPoint.up;
        lineRenderer.positionCount = linePoints;
        float time = 0;
        for (int i = 0; i < linePoints; i++)
        {
            // s = u*t + 1/2*g*t*t
            float x = (startVelocity.x * time) + (Physics.gravity.x / 2 * time * time);
            float y = (startVelocity.y * time) + (Physics.gravity.y / 2 * time * time);
            Vector3 point = new Vector3(x, y, 0);
            lineRenderer.SetPosition(i, origin + point);
            time += timeIntervalPoints;
        }
    }
}
