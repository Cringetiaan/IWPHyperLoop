using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dialoguemanager : MonoBehaviour
{
    [Header("Insert files below")]
    [SerializeField]
    TextAsset dialogueFile;
    GameObject ThisPrefab;

    List<string> CurrentDialogueWindow = new List<string>();


    //Voice lines
    [SerializeField]
    TMP_Text dialogueText;


    //Player input for dialogue progression
    [SerializeField]
    PlayerInput playerInput;
    [SerializeField]
    InputAction DialogueProgression;
    int dialogueCount = 0;


    //Audio source for voice lines - changed often
    //[SerializeField]
    //AudioClip[] SoundList;
    //[SerializeField]
    //AudioSource audioSource;

    public class DialogueWindow
    {
        public string characterName;
        public List<string> dialogue;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ThisPrefab = this.gameObject;
        DialogueProgression = playerInput.actions["Interact"];
        CurrentDialogueWindow = new List<string>(dialogueFile.text.Split('\n'));
        
    }

    // Update is called once per frame
    void Update()
    {
        dialogueText.text = CurrentDialogueWindow[dialogueCount];
        if (DialogueProgression.triggered)
        {
            //audioSource.PlayOneShot(SoundList[dialogueCount]);
            dialogueCount++;
            if (dialogueCount >= CurrentDialogueWindow.Count)
            {
                ThisPrefab.SetActive(false);
            }
        }
    }

}
