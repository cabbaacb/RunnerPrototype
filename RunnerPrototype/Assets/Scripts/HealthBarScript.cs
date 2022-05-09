using UnityEngine;
using UnityEngine.UI;
public class HealthBarScript : MonoBehaviour
{
    [SerializeField] private Image healthBarImage;
    [SerializeField] private PlayerBehaviour player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
    }

    public void UpdateHealthBar()
    {
        healthBarImage.fillAmount = Mathf.Clamp(player.health / player.maxHealth, 0, 1f);
    }
}