using UnityEngine;
using UnityEngine.UI;

public class ResourceBar : MonoBehaviour
{
    [SerializeField]
    PlugTest plugTest;

    [SerializeField]
    GameObject bar;

    [SerializeField]
    Image resource;

    float elapsedTime = 0;

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