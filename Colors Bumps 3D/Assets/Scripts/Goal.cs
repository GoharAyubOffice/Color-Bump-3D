using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        BallController ball = other.GetComponent<BallController>();
        if (!ball || GameManager.singeleton.GameEnded)
            return;

        GameManager.singeleton.EndGame(true);
    }
}