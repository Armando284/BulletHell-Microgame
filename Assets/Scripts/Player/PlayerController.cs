using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Singleton
    public static PlayerController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Vector2 movement;
    private Rigidbody2D rb;
    public Animator animator;
    public PlayerStats playerStats;
    public Attack attack;

    [SerializeField] private float restTimeTotal = 1f;
    [SerializeField] private float restTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        playerStats = GetComponent<PlayerStats>();
        attack = GetComponent<Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerStats.HasMaxEnergy())
        {
            restTime -= Mathf.Clamp(Time.deltaTime, 0, restTimeTotal);
            if (attack.IsAttacking())
            {
                restTime = restTimeTotal;
            }
            if (restTime <= 0)
            {
                playerStats.RecoverEnergy(.1f);
                restTime = .3f;
            }
        }

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
        if (!playerStats.canMove)
            return;
        movement = moveVector;
    }
}
