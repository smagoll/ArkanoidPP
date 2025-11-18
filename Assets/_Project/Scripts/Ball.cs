using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        Launch(new Vector2(0.5f, 0.5f));
    }

    public void Launch(Vector2 direction)
    {
        rb.linearVelocity = direction * speed;
    }
}
