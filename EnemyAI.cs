
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float detectionRange = 50f; // Distance at which the enemy detects and starts following the player
    public float moveSpeed = 5f;

    private Transform player;
    private Rigidbody rb;

    [Header("General")]
    [SerializeField] int Live;
    [SerializeField] GameObject DeadEffect;
    [SerializeField] GM gm;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assuming player has "Player" tag
        rb = GetComponent<Rigidbody>();
    }

    public void Hit ()
    {
        Live -= 10;

        if(Live <0)
        {
            gm.DeadEnemy();
            Instantiate(DeadEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Calculate distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if player is within detection range
        if (distanceToPlayer <= detectionRange)
        {
            // Move towards the player
            Vector3 moveDirection = (player.position - transform.position).normalized;
            rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);

            // Rotate to face the player (optional)
            transform.LookAt(player);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.CompareTag("Player"))
            collision.transform.gameObject.GetComponent<PlayerController>().MinusHp();
    }
}
