using UnityEngine;

public class PlugTest : MonoBehaviour
{
    [SerializeField]
    float timer = 1f;

    [SerializeField]
    GameObject electricityVFX;

    [SerializeField]
    Material electricityCheck;


    private void Start()
    {
       electricityVFX.SetActive(false);

       //var electricityCheckMat = electricityCheck.GetComponent<Renderer>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Plug"))
        {
            electricityVFX.SetActive(true);

            Invoke(nameof(TimerOut), timer);
        }

        if (other.gameObject.CompareTag("ElectricityCheck"))
        {
            if (electricityVFX.activeInHierarchy)
            {
                electricityCheck.SetColor("_BaseColor", Color.green);
            }
            else
            {
                electricityCheck.SetColor("_BaseColor", Color.red);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ElectricityCheck"))
        {
            electricityCheck.SetColor("_BaseColor", Color.white);
        }
    }


    void TimerOut()
    {
        electricityVFX.SetActive(false);
    }
}