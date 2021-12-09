using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameView : MonoBehaviour
{
    [SerializeField] private Image fillBarProgress;
    private float lastValue;
    void Update()
    {
        if (!GameManager.singeleton.GameStarted)
            return;
        float traveledDistance = GameManager.singeleton.EntireDistance - GameManager.singeleton.DistanceLeft;
        float value = traveledDistance / GameManager.singeleton.EntireDistance;

        if (GameManager.singeleton.gameObject && value < lastValue)
            return;

        fillBarProgress.fillAmount = Mathf.Lerp(fillBarProgress.fillAmount, value, 5 * Time.deltaTime);
        lastValue = value;
    }
}
