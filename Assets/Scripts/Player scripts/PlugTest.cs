using UnityEngine;

public class PlugTest : MonoBehaviour
{
    public float timer;

    public GameObject electricityVFX;

    [SerializeField]
    Material electricityCheck;


    private void Start()
    {
       electricityVFX.SetActive(false);
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


    public void TimerOut()
    {
        electricityVFX.SetActive(false);
    }
}