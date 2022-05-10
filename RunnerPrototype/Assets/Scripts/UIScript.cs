using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//here we control our UI
public class UIScript : MonoBehaviour
{
    [SerializeField] private Text _counter;
    [SerializeField] private Text _gameOver;
    [SerializeField] private Text _timer;

    [SerializeField] private Image healthBarImage;

    private float endTime;

    public float EndTime
    {
        set { endTime = value; }
    }

    private float time = 0f;

    public void TimerUpdate(float time)   //this shows a timer, which counts time until the game stops
    {
        float temp = endTime - time;
        temp = Mathf.Round(temp * 100f) / 100f;
        if (temp < 0) temp = 0;
        _timer.text = temp.ToString();
    }

    public void UpdateCount(int count)  //this updates our game score
    {
        _counter.text = "Count: " + count;
    }

    public void UpdateHealth(float health, float maxHealth) //this is for health
    {
        healthBarImage.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1f);
    }

    public void EndGameText()   //and this shows message, when the game ends.
    {
        _gameOver.gameObject.SetActive(true);
    }
}
