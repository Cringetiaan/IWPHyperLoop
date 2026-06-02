using UnityEditor;
using UnityEngine;

public class DigistructOscillation : MonoBehaviour
{
    GameObject obj;
    Material mat;
    Collider col;
    float amount = 1f;
    bool goDown = true;
    bool pause = false;


    void Start()
    {
        obj = this.gameObject;

        mat = obj.GetComponent<MeshRenderer>().material;

        col = obj.GetComponent<Collider>();
    }

    void Update()
    {
        if (!pause && goDown)
        {
            amount -= 0.02f;
            mat.SetFloat("_Amount", amount);

            if ((mat.GetFloat("_Amount")) <= 0)
            {
                pause = true;

                Invoke(nameof(Resume), 2f);
            }
        }
        else if (!pause && !goDown)
        {
            amount += 0.02f;
            mat.SetFloat("_Amount", amount);

            if ((mat.GetFloat("_Amount")) >= 1)
            {
                pause = true;

                Invoke(nameof(Resume), 2f);
            }
        }

        if (mat.GetFloat("_Amount") >= 0.5f)
        {
            col.enabled = false;
        }
        else
        {
            col.enabled = true;
        }
    }

    void Resume()
    {
        pause = false;
        goDown = !goDown;
    }
}