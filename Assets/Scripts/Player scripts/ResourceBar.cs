using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ResourceBar : MonoBehaviour
{
    [SerializeField]
    PlugTest plugTest;

    public GameObject bar;

    public Image resource;

    public float elapsedTime = 0;

    void Start()
    {
        bar.SetActive(false);
        //elapsedTime = 0;
    }
    void Update()
    {
        if (plugTest.electricityVFX.activeInHierarchy)
        {
            bar.SetActive(true);

            elapsedTime += Time.deltaTime;

            resource.fillAmount = 1 - (elapsedTime / plugTest.timer);

            if (elapsedTime >= plugTest.timer)
            {
                elapsedTime = 0;

                plugTest.TimerOut();
                bar.SetActive(false);
            }
        }
    }
}