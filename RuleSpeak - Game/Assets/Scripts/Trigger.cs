using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Trigger : MonoBehaviour
{

    public TextAsset inkJSON;

    private CursorControls controls;

    public void Awake()
    {
        controls = new CursorControls();

    }

    private void Start()
    {
        controls.Mouse.Click.started += _ => DialogueTrigger();
 
    }

    private void OnEnable()
    {
        controls.Enable(); 
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public void DialogueTrigger()
    {

        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            Skip();
        }
        else
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        }
        
    }

    public void Skip()
    {
        DialogueManager.GetInstance().ContinueStory();
    }
}
