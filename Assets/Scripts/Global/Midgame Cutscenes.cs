using UnityEngine;

public class MidgameCutscenes : MonoBehaviour
{

    [SerializeField]
    GameObject PlayerCamera;
    [SerializeField]
    GameObject CutsceneCamera;
    [SerializeField]
    GameObject Dialogue;

    // Update is called once per frame
    void Update()
    {
        if (Dialogue.GetComponent<Dialoguemanager>().exitCutscene)
        {
            PlayerCamera.SetActive(true);
            CutsceneCamera.SetActive(false);
            Dialogue.GetComponent<Dialoguemanager>().exitCutscene = false;
        }
    }
}
