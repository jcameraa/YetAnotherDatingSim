using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using TMPro; 
public class ReadNarrative : MonoBehaviour
{

    void Awake()
    {
        //RemoveChildren();
        StartStory();
    }

    // Creates a new Story object with the compiled story which we can then play!
    void StartStory()
    {
        story = new Story(inkJSONAsset.text);
        RefreshView();
    }

    // This is the main function called every time the story changes. It does a few things:
    // Destroys all the old content and choices.
    // Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
    void RefreshView()
    {
        // Display all the choices, if there are any!
        if (story.currentChoices.Count > 0)
        {
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                Button button = CreateChoiceView(choice.text.Trim());
                // Tell the button what to do when we press it
                button.onClick.AddListener(delegate
                {
                    OnClickChoiceButton(choice);
                });
            }
            canContinue = false;
        }
        else if (story.canContinue && canContinue)
        {
            StartCoroutine("WaitForClick");
            string text = story.Continue();
            text = text.Trim();
            CreateContentView(text);
            foreach (string tag in story.currentTags)
            {
                checkTags(tag);
            }
        }
        // If we've read all the content and there's no choices, the story is finished!
        else
        {
            Button choice = CreateChoiceView("End of story.\nRestart?");
            choice.onClick.AddListener(delegate
            {
                StartStory();
            });
        }
    }

    IEnumerator WaitForClick()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0) || Input.GetButtonUp("Submit"));
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0) || Input.GetButtonUp("Submit"));
        yield return null; 
        RefreshView();
        canContinue = true;
    }

    // When we click the choice button, tell the story to choose that choice!
    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        RefreshView();
    }

    // Creates a button showing the choice text
    void CreateContentView(string text)
    {
       // TMP_Text storyText = Instantiate(textPrefab) as TMP_Text;
        textPrefab.text = text;
        //Debug.Log("coloer" + storyText.color);
        //storyText.transform.SetParent(canvas.transform, false);
        //storyText.transform.SetParent(canvas.transform, false);
    }

    // Creates a button showing the choice text
    Button CreateChoiceView(string text)
    {
        // Creates the button from a prefab
        Button choice = Instantiate(buttonPrefab) as Button;
        choice.transform.SetParent(canvas.transform, false);

        // Gets the text from the button prefab
        Text choiceText = choice.GetComponentInChildren<Text>();
        choiceText.text = text;

        // Make the button expand to fit the text
        HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
        layoutGroup.childForceExpandHeight = false;

        return choice;
    }

    void checkTags(string tag)
    {
        switch (tag)
        {
            //CAESES FOR CHARACTERS 
            case "Narrator":
                //No change
                Debug.Log(tag);
                break;
            case "Lyla":

                Debug.Log(tag);
                listOfCharacter[0].gameObject.SetActive(true);
                //Show Lyla
                break;
            case "Gigi":
                listOfCharacter[1].gameObject.SetActive(true);
                Debug.Log(tag);
                //Show Gigi
                break;

            //CASES FOR BACKGROUNDS
            case "Background_Bedroom":
                background.sprite = listOfBackgrounds[0];
                break;
            case "Background_Street":
                background.sprite = listOfBackgrounds[1];
                break;
            case "Background_Cafe":
                background.sprite = listOfBackgrounds[2];
                break;

            //CASES FOR MUSIC



            //CASES FOR SFX

            case "Thud_Sound":
                SFXAudio.GetComponent<AudioSource>().clip = listOfSFX[5];
                SFXAudio.Play();
                break;

            case "Siren_Sound":
                SFXAudio.GetComponent<AudioSource>().clip = listOfSFX[4];
                SFXAudio.Play();
                break;

            case "BusCrash_Sound":
                SFXAudio.GetComponent<AudioSource>().clip = listOfSFX[2];
                SFXAudio.Play();
                break;

            case "BirdSounds_Sound":
                SFXAudio.GetComponent<AudioSource>().clip = listOfSFX[2];
                SFXAudio.Play();
                break;

            case "AlarmClock_Sound":
                SFXAudio.GetComponent<AudioSource>().clip = listOfSFX[0];
                SFXAudio.Play();
                break;



            //CASES FOR VOICE ACTING
            default:
                break;
        }

    }

    [SerializeField]
    private TextAsset inkJSONAsset;
    private Story story;

    [SerializeField]
    private Canvas canvas;

    private bool canContinue = true; 

    // UI Prefabs
    [SerializeField]
    private TMP_Text textPrefab;
    [SerializeField]
    private Button buttonPrefab;

    //CHARACTERS
    public Image[] listOfCharacter;

    //BACKGROUNDS
    public Image background;

    public Sprite[] listOfBackgrounds;

    //MUSIC
    public AudioSource mainAudio;

    public AudioClip[] listOfMainAudio;

    //SFX
    public AudioSource SFXAudio;

    public AudioClip[] listOfSFX; 
}
