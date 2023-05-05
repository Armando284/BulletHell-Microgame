using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Instance
    public static PlayerController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Vector2 movement;
    private bool canMove = true;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Recives a normalized Vector2
    /// </summary>
    /// <param name="moveVector"></param>
    public void Move(Vector2 moveVector)
    {
        if (!canMove)
            return;
        movement = moveVector;
    }
}
