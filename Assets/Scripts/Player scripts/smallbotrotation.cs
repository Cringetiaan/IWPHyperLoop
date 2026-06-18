using UnityEngine;

public class smallbotrotation : MonoBehaviour
{
    [SerializeField]
    SkinnedMeshRenderer bigbot;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = bigbot.transform.rotation;
    }
}
