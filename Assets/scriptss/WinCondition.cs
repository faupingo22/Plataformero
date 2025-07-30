using UnityEngine;
using UnityEngine.SceneManagement; 

public class WinCondition : MonoBehaviour
{
    public GameObject winScreen; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Ganaste!");

            Time.timeScale = 0; 

            if (winScreen != null)
            {
                winScreen.SetActive(true);
            }
        }
    }
}
