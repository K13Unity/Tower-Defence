using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamageDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshPro TextMeshPro;
    [SerializeField] private float damageDuration = 1;
    [SerializeField] private float heightCriticalDamage = 3f;
    [SerializeField] private float heightNormalDamage = 2.5f;
    private Vector3 spawnPosition;
    private Vector3 endPosition;
    private float length;

    public void ShowDamage(int damage, Vector3 position, bool isCritical)
    {
        var damageNumber = Instantiate(TextMeshPro, Vector3.zero, Quaternion.identity);
        damageNumber.text = damage.ToString();
        damageNumber.transform.rotation = Quaternion.Euler(90, 0, 0);

        if (!isCritical)
        {
            spawnPosition = new Vector3(position.x, position.y + heightNormalDamage, position.z);
            damageNumber.color = new Color32(255, 234, 71, 255);
            endPosition = spawnPosition + Vector3.forward * 1f;
            length = damageDuration * 0.25f;
        }
        else
        {
            spawnPosition = new Vector3(position.x, position.y + heightCriticalDamage, position.z);
            damageNumber.color = new Color32(255, 115, 71, 255);
            endPosition = spawnPosition + Vector3.forward * 1.5f;
            length = damageDuration * 0.5f;
        }

        damageNumber.transform.position = spawnPosition;
        damageNumber.transform.DOMove(endPosition, length)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => Destroy(damageNumber));
    }
}
