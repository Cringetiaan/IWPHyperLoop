using UnityEngine;

public class ActivateDialogue : MonoBehaviour
{
    [SerializeField]
    GameObject DialoguePrefab;

    [SerializeField]
    Collider Trigger;
     void Awake()
    {
       
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   void OnTriggerEnter(Collider other)
  {
     if (other.CompareTag("Player"))
     {
       Debug.Log("Player entered dialogue trigger");
            DialoguePrefab.SetActive(true);
       Trigger.enabled = false;

     }
 }

}
