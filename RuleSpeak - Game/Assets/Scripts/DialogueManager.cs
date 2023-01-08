using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;

public class DialogueManager: MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private TMP_Text dialogue;
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private TMP_Text nameText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    [SerializeField] private GameObject choicespanel;
    [SerializeField] private TMP_Text[] choicesText;

    public Animator potraitMover;



    public bool choicesEnabled = false;
    public bool countdownStarted = false;
    public bool dialogueIsPlaying = false;
    public bool coroutineIsPlaying;
    public bool gameStarted = false;
    public float countdown = 10f;

    private Vector3 dialogueVectorF = new Vector3(305, 370, 0);
    private Vector3 dialogueVectorR = new Vector3(570, 370, 0);
    private string save;

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
        if (countdownStarted == true && gameStarted == true)
        {
            Debug.Log("countdown");
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
        dialogueIsPlaying = true;
        choicespanel.SetActive(true);
        currentStory = new Story(inkJSON.text);
        ContinueStory();

    }

    public void ExitDialogueMode()
    {

        dialogue.text = "";
        dialogueIsPlaying = false;

    }






    public void ContinueStory()
    {
        if (coroutineIsPlaying)
        {
            StopAllCoroutines();
            dialogue.text = save;
            coroutineIsPlaying = false;
        }
        else 
        {
            if (currentStory.canContinue)
            {

                StartCoroutine(TypeSentence(currentStory.Continue()));


                buttonPopUp.Play("PopUp 0");
                DisplayChoices();
                TagHandler(currentStory.currentTags);

            }
            else if (!currentStory.canContinue && !choicesEnabled)
            {
                ExitDialogueMode();
            }
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

                    if (tagValue == "Regina")
                    {
                        dialogue.transform.SetPositionAndRotation(dialogueVectorR, Quaternion.identity);                     
                    }
                    else if (tagValue == "Flinta")
                    {
                        dialogue.transform.SetPositionAndRotation(dialogueVectorF, Quaternion.identity);
                    }


                    nameText.text = tagValue;
                    potraitMover.Play(tagValue);
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



    IEnumerator TypeSentence(string lsentence)
    {
        save = lsentence;
        coroutineIsPlaying = true;

        dialogue.text = "";

        foreach (char letter in lsentence.ToCharArray())
        {
            dialogue.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
        coroutineIsPlaying = false;

    }
}
