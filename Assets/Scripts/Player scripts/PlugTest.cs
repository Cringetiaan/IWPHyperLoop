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
        if (electricityVFX.activeInHierarchy)
        {
            CancelInvoke(nameof(TimerOut));
            TimerOut();
            barx.bar.SetActive(false);
            barx.elapsedTime = 0;
            barx.resource.fillAmount = 1;

        }
        else if (other.gameObject.CompareTag("Plug"))
        {
            electricityVFX.SetActive(true);

            Invoke(nameof(TimerOut), timer);
        }

        if (other.gameObject.CompareTag("Sink"))
        {
            if (electricityVFX.activeInHierarchy)
            {
                sinkVFX.SetActive(true);
                barrier.SetActive(false);

                TimerOut();
            }
        }
    }

    public void TimerOut()
    {
        electricityVFX.SetActive(false);
    }
}