using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class TutorialManager : MonoBehaviour, IPointerClickHandler
{
    public Animator messagepanelanimation;
    public GameObject joystickfinger;
    public GameObject moveblockfinger;
    public GameObject testingfinger;
    public GameObject taptocontinue;
    public GameObject messagepanel;
    public GameObject canvas;
    private int page = 0;

    //sample variable strings: rangedtut, repeattut
    
    
    void Awake()
    {
        string tutprogressdata = SaveSystem.LoadTutorialProgress().tutprogress;
        
        InitializeScene();
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        InitializeScene();
        StartCoroutine(PopOutPanel(0));
        Debug.Log(page);

    }

    public void InitializeScene()
    {
        string currentscene = SceneManager.GetActiveScene().name;
        string tutprogressdata = SaveSystem.LoadTutorialProgress().tutprogress;
        
        //enable canvas before starting tutorial!!!!


        //scene checker

        

        Debug.Log(tutprogressdata);

        if (currentscene == "Equiptment" && tutprogressdata == "buildtut")
        {
            
            page++;
            BuildingTutorial(page);
            
        }
        else if (currentscene == "Testing" && tutprogressdata == "playercontroltut")
        {
            page++;
            ControlsTutorial();
            
        }
        else
        {
            canvas.SetActive(false);
        }
    }
    public void UpdateNewProgress(string tutprogress)
    {
        SaveSystem.SaveTutorialProgress(tutprogress);

    }

    public void UpdateNull()
    {
        SaveSystem.SaveTutorialProgress(null);

    }

    public void BuildingTutorial(int page)
    {
        //display finger overlay
        if(page == 1)
        {
            joystickfinger.SetActive(false);
            moveblockfinger.SetActive(true);
            testingfinger.SetActive(false);
            taptocontinue.SetActive(true);
            messagepanel.SetActive(true);
            StartCoroutine(PopInPanel(1, "Put your finger on the block and drag it into the \"WHEN PRESS\" block."));
        }
        else if (page == 2)
        {
            joystickfinger.SetActive(false);
            moveblockfinger.SetActive(false);
            testingfinger.SetActive(true);
            taptocontinue.SetActive(true);
            messagepanel.SetActive(true);
            StartCoroutine(PopInPanel(1, "You may press the \"Testing\" button to test your script."));
            SaveSystem.SaveTutorialProgress("playercontroltut");
        }
        
        //display lightbulb
    }

    public void ControlsTutorial()
    {
        if (page == 1)
        {
            joystickfinger.SetActive(true);
            moveblockfinger.SetActive(false);
            testingfinger.SetActive(false);
            taptocontinue.SetActive(true);
            messagepanel.SetActive(true);
            StartCoroutine(PopInPanel(1, "Put your finger at the bottom left of the screen and drag to control the character"));
        }
        else if (page == 2)
        {
            joystickfinger.SetActive(false);
            moveblockfinger.SetActive(false);
            testingfinger.SetActive(false);
            taptocontinue.SetActive(true);
            messagepanel.SetActive(true);
            StartCoroutine(PopInPanel(1, "Press the attack button to use your program."));
            SaveSystem.SaveTutorialProgress("empty");
        }
    }

    IEnumerator PopInPanel(float delay, string message)
    {

        TextMeshProUGUI messagepaneltext = messagepanel.GetComponentInChildren<TextMeshProUGUI>();
        messagepaneltext.text = message;
        yield return new WaitForSeconds(delay);

        messagepanelanimation.SetTrigger("Pop In");

    }

    IEnumerator PopOutPanel(float delay)
    {

        
        yield return new WaitForSeconds(delay);

        messagepanelanimation.SetTrigger("Pop Out");

    }



}
