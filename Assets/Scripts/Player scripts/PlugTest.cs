using System.Xml;
using UnityEngine;

public class PlugTest : MonoBehaviour
{
    public float timer;

    public GameObject electricityVFX;

    [SerializeField]
    GameObject sinkVFX;

    [SerializeField]
    GameObject barrier;

    [SerializeField]
    ResourceBar barx;


    private void Start()
    {
       electricityVFX.SetActive(false);
        sinkVFX.SetActive(false);
        barrier.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Plug"))
        {
            if (electricityVFX.activeInHierarchy)
            {
                CancelInvoke(nameof(TimerOut));
                TimerOut();
                barx.bar.SetActive(false);
                barx.elapsedTime = 0;
                barx.resource.fillAmount = 1;

            }
            else
            {
                electricityVFX.SetActive(true);

                Invoke(nameof(TimerOut), timer);
            }
        }
        else if (other.gameObject.CompareTag("Sink"))
        {
            if (electricityVFX.activeInHierarchy)
            {
                sinkVFX.SetActive(true);
                barrier.SetActive(false);

                CancelInvoke(nameof(TimerOut));
                TimerOut();
                barx.bar.SetActive(false);
                barx.elapsedTime = 0;
                barx.resource.fillAmount = 1;
            }
        }
    }

    public void TimerOut()
    {
        electricityVFX.SetActive(false);
    }
}