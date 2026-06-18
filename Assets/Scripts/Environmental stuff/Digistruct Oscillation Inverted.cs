using UnityEngine;

public class DigistructOscillationInverted : MonoBehaviour
{
    GameObject obj;
    Material mat;
    Collider col;
    float amount = 1f;
    bool goDown = false;
    bool pause = true;


    void Start()
    {
        obj = this.gameObject;

        mat = obj.GetComponent<MeshRenderer>().material;

        col = obj.GetComponent<Collider>();

        Invoke(nameof(Resume), 3f);
    }

    void FixedUpdate()
    {
        if (!pause && goDown)
        {
            amount -= 0.025f;
            mat.SetFloat("_Amount", amount);

            if ((mat.GetFloat("_Amount")) <= 0)
            {
                pause = true;

                Invoke(nameof(Resume), 2f);
            }
        }
        else if (!pause && !goDown)
        {
            amount += 0.025f;
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
