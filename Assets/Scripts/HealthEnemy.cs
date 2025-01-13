using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HealthEnemy : MonoBehaviour
{
    [SerializeField] private UnityEvent OnDie;
    public bool IsAlive {  get; private set; }
    private int touchWalls = 0;

    private void Awake()
    {
        IsAlive = true;
    }

    private IEnumerator Die()
    {
        OnDie?.Invoke();
        IsAlive = false;
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    private void AddWallTouch()
    {
        touchWalls++;
        if (touchWalls > 1) StartCoroutine(Die());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { StartCoroutine(Die()); }
        if (collision.CompareTag("Ground")) { StartCoroutine(Die()); }
        if (collision.CompareTag("Wall")) { AddWallTouch(); }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall")) { touchWalls--; }
    }
}
