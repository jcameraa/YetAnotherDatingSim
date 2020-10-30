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
        // Destroy all children in layout group
        foreach(Transform child in canvas.transform.Find("ChoiceView").transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    // Creates a button showing the choice text
    void CreateContentView(string text)
    {
       // TMP_Text storyText = Instantiate(textPrefab) as TMP_Text;
        textPrefab.text = text;
        //Debug.Log("color" + storyText.color);
        //storyText.transform.SetParent(canvas.transform, false);
        //storyText.transform.SetParent(canvas.transform, false);
    }

    // Creates a button showing the choice text
    Button CreateChoiceView(string text)
    {
        // Creates the button from a prefab
        Button choice = Instantiate(buttonPrefab) as Button;
        choice.transform.SetParent(canvas.transform.Find("ChoiceView").transform);

        // Gets the text from the button prefab
        TMP_Text choiceText = choice.GetComponentInChildren<TMP_Text>();
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

            //CASES FOR BACKGROUNDS --------------------------------------------------------
            case "Background_Bedroom":
                background.sprite = listOfBackgrounds[0];
                break;
            case "Background_Street":
                background.sprite = listOfBackgrounds[1];
                break;
            case "Background_Cafe":
                background.sprite = listOfBackgrounds[2];
                break;

            //CASES FOR MUSIC ----------------------------------------------------------

            case "DayOneWakeUp":
                mainAudio.GetComponent<AudioSource>().clip = listOfMainAudio[0];
                mainAudio.Play();
                break;

            case "DayTwoWakeUp":
                mainAudio.GetComponent<AudioSource>().clip = listOfMainAudio[1];
                mainAudio.Play();
                break;

            case "DayThreeWakeUp":
                mainAudio.GetComponent<AudioSource>().clip = listOfMainAudio[2];
                mainAudio.Play();
                break;

            case "DayFourWakeUp":
                mainAudio.GetComponent<AudioSource>().clip = listOfMainAudio[3];
                mainAudio.Play();
                break;

            case "DayFiveWakeUp":
                mainAudio.GetComponent<AudioSource>().clip = listOfMainAudio[4];
                mainAudio.Play();
                break;

            case "WeddingHell":
                mainAudio.GetComponent<AudioSource>().clip = listOfMainAudio[9];
                mainAudio.Play();
                break;

            case "MeetPartnerOne":
                mainAudio.GetComponent<AudioSource>().clip = listOfMainAudio[6];
                mainAudio.Play();
                break;

            case "MeetPartnerTwo":
                mainAudio.GetComponent<AudioSource>().clip = listOfMainAudio[5];
                mainAudio.Play();
                break;

            case "MeetParents":
                mainAudio.GetComponent<AudioSource>().clip = listOfMainAudio[7];
                mainAudio.Play();
                break;

            case "Awkward":
                mainAudio.GetComponent<AudioSource>().clip = listOfMainAudio[8];
                mainAudio.Play();
                break;

            case "EndCredits":
                mainAudio.GetComponent<AudioSource>().clip = listOfMainAudio[10];
                mainAudio.Play();
                break;





            //CASES FOR SFX -----------------------------------------------------------------------------------

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



            //CASES FOR VOICE ACTING---------------------------------------------------------------------------- 

            // GIGI VA
            case "Angry_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[0];
                VAAudio.Play();
                break;

            // MISSING: AngryHey 

            case "Annoyed_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[2];
                VAAudio.Play();
                break;

            case "Confused_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[3];
                VAAudio.Play();
                break;

            case "DisappointedOh_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[4];
                VAAudio.Play();
                break;

            case "DisappointedSigh_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[5];
                VAAudio.Play();
                break;

            case "FellInLove_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[6];
                VAAudio.Play();
                break;

            case "HereUseThis_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[7];
                VAAudio.Play();
                break;

            // MISSING: Hey

            // MISSING: IDo

            case "ILoveYou_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[10];
                VAAudio.Play();
                break;

            case "ImGigi_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[11];
                VAAudio.Play();
                break;

            case "ImSorry_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[12];
                VAAudio.Play();
                break;

            case "ImTryingToSaveUs_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[13];
                VAAudio.Play();
                break;

            case "InterestedOh_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[14];
                VAAudio.Play();
                break;

            case "Laugh_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[15];
                VAAudio.Play();
                break;

            case "LookOutLyla_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[16];
                VAAudio.Play();
                break;

            case "LylaName_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[17];
                VAAudio.Play();
                break;

            case "MarryMe_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[18];
                VAAudio.Play();
                break;

            case "Muffins_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[19];
                VAAudio.Play();
                break;

            case "No_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[20];
                VAAudio.Play();
                break;

            case "Stuttering1_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[21];
                VAAudio.Play();
                break;

            case "Stuttering2_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[22];
                VAAudio.Play();
                break;

            case "Um_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[23];
                VAAudio.Play();
                break;

            case "Wait_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[24];
                VAAudio.Play();
                break;

            // LYLA VA
            case "CockyLaugh_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[38];
                VAAudio.Play();
                break;

            case "DoIKnowYou_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[40];
                VAAudio.Play();
                break;

            case "ExcuseMe_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[41];
                VAAudio.Play();
                break;

            // MISSING: ExcuseMeButSassier

            // NO MATCH
            case "FuckYou_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[0];
                VAAudio.Play();
                break;

            case "Gasp1_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[43];
                VAAudio.Play();
                break;

            case "Gasp2_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[44];
                VAAudio.Play();
                break;

            case "Goddamnit_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[45];
                VAAudio.Play();
                break;

            case "Groan_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[46];
                VAAudio.Play();
                break;

            case "HotDamn_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[47];
                VAAudio.Play();
                break;

            case "ImLyla_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[48];
                VAAudio.Play();
                break;

            case "ItsADate_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[49];
                VAAudio.Play();
                break;

            case "ItsFine_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[50];
                VAAudio.Play();
                break;

            case "LetMeGo_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[51];
                VAAudio.Play();
                break;

            case "LetMeGoButMoreFirm_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[52];
                VAAudio.Play();
                break;

            case "LetsGo_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[53];
                VAAudio.Play();
                break;

            case "SoUhh_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[54];
                VAAudio.Play();
                break;

            case "Stuttering_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[55];
                VAAudio.Play();
                break;

            // MISSING: TooDamnHot

            // MISSING: Wait

            case "WTF_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[58];
                VAAudio.Play();
                break;

            // KRISTLE VA
            case "AnnoyedK_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[31];
                VAAudio.Play();
                break;

            case "Honey_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[32];
                VAAudio.Play();
                break;

            case "Mistaken_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[33];
                VAAudio.Play();
                break;

            case "Nonsense_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[34];
                VAAudio.Play();
                break;

            case "NoRing_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[35];
                VAAudio.Play();
                break;

            case "Recognized_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[36];
                VAAudio.Play();
                break;

            // JEROLD VA
            case "AnnoyedJ_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[25];
                VAAudio.Play();
                break;

            case "Doctor_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[26];
                VAAudio.Play();
                break;

            case "Firefighter_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[27];
                VAAudio.Play();
                break;

            case "Recommend_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[28];
                VAAudio.Play();
                break;

            case "Right_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[29];
                VAAudio.Play();
                break;

            case "Time_VA":
                VAAudio.GetComponent<AudioSource>().clip = listOfVA[30];
                VAAudio.Play();
                break;

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

    //VA
    public AudioSource VAAudio;

    public AudioClip[] listOfVA;
}
