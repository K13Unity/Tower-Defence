using UnityEngine;

public class DiePortal : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        Enemy enemyComponent = other.GetComponent<Enemy>();
        if (enemyComponent != null)
        {
            GameController.Instance.EnemyCounter(enemyComponent);
            enemyComponent.OnFinished();
        }
    }
}
