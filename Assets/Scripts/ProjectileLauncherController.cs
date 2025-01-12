using System;
using System.Collections;
using UnityEngine;

public class ProjectileLauncherController : MonoBehaviour
{
    [SerializeField] private Transform launchPoint;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private PowerUp powerUp;

    [Header("Trajectory Display")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int linePoints = 175;
    [SerializeField] private float timeIntervalPoints = 0.01f;

    [Header("Disabling")]
    [SerializeField] private float timeToWaitBeforeDisabling;

    public event Action onDisable;
    
    private float launchSpeed;
    private bool isThrowed;

    public bool IsThrowed { get { return isThrowed; } }

    private void Start()
    {
        isThrowed = false;
        if (powerUp != null) powerUp.enabled = false;
    }

    private void OnDisable()
    {
        Debug.Log("getting disabled");
        onDisable?.Invoke();
    }

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
            if (powerUp != null) powerUp.enabled = true;
        } else if (Input.GetMouseButton(0))
        {
            Vector2 direction = new Vector2(transform.position.x - mousePosition.x, transform.position.y - mousePosition.y);
            //Debug.Log(direction);
            launchSpeed = direction.magnitude * speedMultiplier;
            transform.up = direction;
            DrawTrajectory();
        }
    }

    private void FixedUpdate()
    {
        if (rigidBody.linearVelocity == Vector2.zero && rigidBody.bodyType != RigidbodyType2D.Kinematic)
        {
            gameObject.SetActive(false);
            //StartCoroutine(WaitAndDisable());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rigidBody.linearDamping = 1;
    }

    private IEnumerator WaitAndDisable()
    {
        Debug.Log("Disabling GameObject in " + timeToWaitBeforeDisabling + " seconds.");
        yield return new WaitForSeconds(timeToWaitBeforeDisabling);
        gameObject.SetActive(false);
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
