using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{

    public TextAsset inkJSON;

    public void DialogueTrigger()
    {
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
    }

    public void Skip()
    {
        DialogueManager.GetInstance().ContinueStory();
    }
}
