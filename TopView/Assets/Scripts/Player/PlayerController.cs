using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameState gameState;

    [SerializeField] private AudioClip getHit;
    [SerializeField] private AudioClip step;
    [SerializeField] private AudioClip pickUp;

    private AudioSource audioSource;
    private Rigidbody2D rb;
    private Vector2 movement;
    private AudioSettings audioSettings;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        audioSettings = FindObjectOfType<AudioSettings>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        movement.Normalize();
        rb.velocity = new Vector2(movement.x * speed * Time.deltaTime, movement.y * speed * Time.deltaTime);

        if (movement.sqrMagnitude > 0 && !audioSource.isPlaying && audioSettings.areSoundEffectsEnabled.Value)
        {
            audioSource.clip = step;
            audioSource.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            gameState.DecreaseHealth();

            if (audioSettings.areSoundEffectsEnabled.Value)
            {
                audioSource.PlayOneShot(getHit);
            }
        }

        if (collision.CompareTag("Coin"))
        {
            if (audioSettings.areSoundEffectsEnabled.Value)
            {
                audioSource.PlayOneShot(pickUp);
            }
        }
    }
}
