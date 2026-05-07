using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    //The platforms
    [SerializeField]
    GameObject P1;
    bool P1Pos;
    [SerializeField]
    GameObject P2;
    bool P2Pos;
    [SerializeField]
    GameObject Platform;

    [Header("Tweaking")]
    [SerializeField]
    float Speed;

    
    void Start()
    {
        P2Pos = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (P2Pos)
        {
            Platform.transform.position = Vector3.MoveTowards(Platform.transform.position, P1.transform.position, Speed * Time.deltaTime);

            if (Platform.transform.position == P1.transform.position)
            {
                P2Pos = false;
                P1Pos = true;
            }
        }

        if(P1Pos)
        {
            Platform.transform.position = Vector3.MoveTowards(Platform.transform.position, P2.transform.position, Speed * Time.deltaTime);
            if (Platform.transform.position == P2.transform.position)
            {
                P1Pos = false;
                P2Pos = true;
            }
        }

    }
}
