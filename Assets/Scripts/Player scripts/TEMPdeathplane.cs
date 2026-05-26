using UnityEngine;

public class TEMPdeathplane : MonoBehaviour
{
    [SerializeField]
    Collider Platform;
    [SerializeField]
    GameObject PlayerSpawnpoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            other.transform.position = PlayerSpawnpoint.transform.position;
        }
    }
}
