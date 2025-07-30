using UnityEngine;
using UnityEngine.SceneManagement;

public class ChaserAI : MonoBehaviour
{
    public Transform playerTransform; 
    public float speed = 3.0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void FixedUpdate()
    {
        if (playerTransform == null) return; 

        Vector3 direction = (playerTransform.position - transform.position).normalized;

        Vector3 moveVelocity = direction * speed;
        rb.linearVelocity = new Vector3(moveVelocity.x, rb.linearVelocity.y, moveVelocity.z); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
