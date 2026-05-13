using UnityEngine;

public class PlugTest : MonoBehaviour
{
    [SerializeField]
    float timer = 1f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Plug"))
        {
            Debug.Log("Enter");

            Invoke(nameof(TimerOut), timer);
        }
    }


    void TimerOut()
    {
        Debug.Log("Time Out");
    }
}