﻿using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System.Collections;
using TMPro;

public class DialogueManager: MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private TMP_Text dialogue;
    [SerializeField] private TMP_Text countdownText;


    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    [SerializeField] private GameObject choicespanel;
    [SerializeField] private TMP_Text[] choicesText;


    public bool choicesEnabled = false;
    public bool countdownStarted = false;
    public bool gameStarted = false;
    public float countdown = 10f;

    private Animator buttonPopUp;
    private Story currentStory;

    private static DialogueManager instance;

    private const string SPEAKER_TAG = "speaker";
    private const string STATE_TAG = "state";




    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;

    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        choicesText = new TMP_Text[choices.Length];

        buttonPopUp = choicespanel.GetComponent<Animator>();


        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TMP_Text>();
            index++;
        }

    }

    public void Update()
    {
        if (dialogue.text == "")
        {
            Debug.Log(dialogue.text);
            ContinueStory();
        }

        if (countdownStarted == true && gameStarted == true)
        {
            countdown -= (1 * Time.deltaTime);
            countdownText.text = countdown.ToString("0");

            if (countdown <= 0)
            {
                MakeChoice(4);
            }
        }
        
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        choicespanel.SetActive(true);
        currentStory = new Story(inkJSON.text);
        ContinueStory();

    }

    public void ExitDialogueMode()
    {

        dialogue.text = "";

    }

    public void ContinueStory()
    {
        //checks and reacts to them if there are choices or tags + sets animation to default state + continues the current story
        if (currentStory.canContinue)
        {

            dialogue.text = currentStory.Continue();

            buttonPopUp.Play("PopUp 0");
            DisplayChoices();
            TagHandler(currentStory.currentTags);

        }
        else if (!currentStory.canContinue && !choicesEnabled)
        {
            ExitDialogueMode();
        }
    }


    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);

            ContinueStory();
        
        
        choicesEnabled = false;
        buttonPopUp.SetBool("choicesEnabled", false);
        buttonPopUp.Play("PopUp 0");

        countdownStarted = false;

    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > 0)
        {
            choicesEnabled = true;
            buttonPopUp.SetBool("choicesEnabled", true);
            countdown = 10f;
            countdownStarted = true;
        }

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choiches than possible");
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

       // StartCoroutine(ButtonAnimation());
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        
    }

   /* private IEnumerator ButtonAnimation()
    {
        buttonPopUp.Play("PopUp 1");

        yield return new WaitForSeconds(1f);
        buttonPopUp.Play("PopUp 2");
        yield return new WaitForSeconds(1f);
        buttonPopUp.Play("PopUp 3");
        yield return new WaitForSeconds(0.2f);
        buttonPopUp.Play("PopUp 4");
        StopCoroutine(ButtonAnimation());
    } */


    public void TagHandler(List<string> currentTags)
    {

        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:

                    break;
                case STATE_TAG:
                    if (tagValue == "loop")
                    {
                        gameStarted = true;
                    }
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            }
        }
    }
}