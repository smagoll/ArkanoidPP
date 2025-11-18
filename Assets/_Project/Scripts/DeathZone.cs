using System;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public event Action<Ball> OnBallEntered;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            var ball = other.GetComponent<Ball>();

            if (ball != null)
            {
                OnBallEntered?.Invoke(ball);
                Debug.Log("Ball entered");
            }
        }
    }
}
