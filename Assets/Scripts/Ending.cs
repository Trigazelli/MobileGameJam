using System.Collections;
using TMPro;
using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField] private HealthEnemy[] _enemies;
    [SerializeField] private PoolController _poolController;
    [SerializeField] private TMP_Text _Text;
    [SerializeField] private GameObject _EndUI;


    private void Update()
    {
        bool EnemiesAlive = false;
        foreach (HealthEnemy enemy in _enemies)
        {
            if(enemy.IsAlive) EnemiesAlive = true;
        }

        if (EnemiesAlive && _poolController.BallsLeft == 0) StartCoroutine(Lose());
        if (!EnemiesAlive) StartCoroutine(Win());
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(2f);
        _EndUI.SetActive(true);
        _Text.text = "Victoire !";
    }

    public IEnumerator Lose()
    {
        yield return new WaitForSeconds(2f);
        _EndUI.SetActive(true);
        _Text.text = "Défaite ...";
    }
}
