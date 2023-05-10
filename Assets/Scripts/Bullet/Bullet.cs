using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction;
    public bool hasHit = false;
    public float speedBase = 50f;
    [SerializeField] float speed = 1f;
    [SerializeField] private AnimationCurve speedCurve;
    public string ownerTag;
    private float spawnTime;

    private void Start()
    {
        spawnTime = Time.time;
    }

    private void Update()
    {
        speed = speedBase * speedCurve.Evaluate(Time.time - spawnTime);
        if (speed <= 0)
            StartCoroutine(DestroyBullet(.3f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!hasHit)
            GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ownerTag))
            return;

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.playerStats.ApplyDamage(new DamageData(2f, transform.position));
            StartCoroutine(DestroyBullet(.1f));
            return;
        }

        StartCoroutine(DestroyBullet(.1f));
    }

    IEnumerator DestroyBullet(float delay)
    {
        //Destroy effect
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
