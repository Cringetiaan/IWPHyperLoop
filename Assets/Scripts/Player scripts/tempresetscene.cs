using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tempresetscene : MonoBehaviour
{
    [SerializeField]
    GameObject CutsceneCamera;
    [SerializeField]
    GameObject Dialogue;
    [SerializeField]
    GameObject Train;
    [SerializeField]
    GameObject Target;

    bool contact;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            contact = true;

            CutsceneCamera.SetActive(true);

            Dialogue.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        if (contact)
        {
            StartCoroutine(TrainDelay());
        }
        
        if (Dialogue.GetComponent<Dialoguemanager>().exitCutscene)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    IEnumerator TrainDelay()
    {
        yield return new WaitForSeconds(1.5f);
        Train.transform.position = Vector3.Lerp(Train.transform.position, Target.transform.position, 0.075f);
    }
}
