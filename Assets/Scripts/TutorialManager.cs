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
    public GameObject attackfinger;
    public GameObject canvas;
    public GameObject newgamefinger;
    public GameObject equiptmentfinger;
    public GameObject panel;
    public GameObject backfinger;
    public GameObject selectlevelfinger;
    private int page = 0;

    //sample variable strings: rangedtut, repeattut

    void Start()
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

        canvas.SetActive(true);
        panel.SetActive(false);
        joystickfinger.SetActive(false);
        moveblockfinger.SetActive(false);
        testingfinger.SetActive(false);
        taptocontinue.SetActive(false);
        messagepanel.SetActive(false);
        attackfinger.SetActive(false);
        newgamefinger.SetActive(false);
        equiptmentfinger.SetActive(false);
        backfinger.SetActive(false);
        selectlevelfinger.SetActive(false);

        if (currentscene == "Main Menu" && tutprogressdata == "buildtut")
        {
            PointToScene("equiptment");
        }
        else if (currentscene == "Equiptment" && tutprogressdata == "selectleveltut")
        {

            backfinger.SetActive(true);

        }
        else if (currentscene == "Main Menu" && tutprogressdata == "selectleveltut")
        {

            PointToScene("new game");
        }
        else if (currentscene == "Level Selection" && tutprogressdata == "selectleveltut")
        {
            selectlevelfinger.SetActive(true);
            SaveSystem.SaveTutorialProgress("leveltutorial");

        }
        else if (currentscene == "Level 1" && tutprogressdata == "leveltutorial")
        {
            DisplayNotification(0f, 5f, "Try to fight the enemies, and navigate yourself to the end of the level!");
            
            SaveSystem.SaveTutorialProgress("empty");

        }
        else if (currentscene == "Equiptment" && tutprogressdata == "buildtut")
        {
            page++;
            BuildingTutorial(page);
        }
        else if (currentscene == "Testing" && tutprogressdata == "playercontroltut")
        {
            page++;
            ControlsTutorial();
        }
        else if (currentscene == "Main Menu" && (tutprogressdata == "melee2intro" || tutprogressdata == "ranged1intro" || tutprogressdata == "ranged2intro" || tutprogressdata == "repeat10intro"))
        {
            PointToScene("equiptment");
        }
        else if (currentscene == "Equiptment" && (tutprogressdata == "melee2intro" || tutprogressdata == "ranged1intro" || tutprogressdata == "ranged2intro" || tutprogressdata == "repeat10intro"))
        {
            Debug.Log("Explain block");
            ExplainBlock(tutprogressdata);
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
        if(page == 1)
        {
            panel.SetActive(true);
            moveblockfinger.SetActive(true);
            taptocontinue.SetActive(true);
            messagepanel.SetActive(true);
            StartCoroutine(PopInPanel(1, "Put your finger on the block and drag it into the \"WHEN PRESS\" block."));
        }
        else if (page == 2)
        {
            panel.SetActive(true);
            testingfinger.SetActive(true);
            taptocontinue.SetActive(true);
            messagepanel.SetActive(true);
            StartCoroutine(PopInPanel(0.5f, "You may press the \"Testing\" button to test your script."));  
        }
        else if (page == 3)
        {
            messagepanel.SetActive(true);
            StartCoroutine(PopOutPanel(0));
            DisableCanvas(0.5f);
            SaveSystem.SaveTutorialProgress("playercontroltut");
        }
        //display lightbulb
    }

    public void ControlsTutorial()
    {
        if (page == 1)
        {
            panel.SetActive(true);
            joystickfinger.SetActive(true);
            taptocontinue.SetActive(true);
            messagepanel.SetActive(true);
            StartCoroutine(PopInPanel(1, "Put your finger at the bottom left of the screen and drag to control the character"));
        }
        else if (page == 2)
        {
            panel.SetActive(true);
            taptocontinue.SetActive(true);
            messagepanel.SetActive(true);
            attackfinger.SetActive(true);
            StartCoroutine(PopInPanel(0.5f, "Press the attack button to use your program."));
        }
        else if (page == 3)
        {
            messagepanel.SetActive(true);
            backfinger.SetActive(true);
            StartCoroutine(PopOutPanel(0));
            DisplayNotification(0.5f, 5, "You may press the back button when you are familiar with the controls.");
            SaveSystem.SaveTutorialProgress("selectleveltut");

        }
        
    }

    IEnumerator PopInPanel(float delay, string message)
    {
        yield return new WaitForSeconds(delay);
        TextMeshProUGUI messagepaneltext = messagepanel.GetComponentInChildren<TextMeshProUGUI>();
        messagepaneltext.text = message;
        messagepanelanimation.SetTrigger("Pop In");
    }

    IEnumerator PopOutPanel(float delay)
    {      
        yield return new WaitForSeconds(delay);
        messagepanelanimation.SetTrigger("Pop Out");
    }

    IEnumerator DisableCanvas(float delay)
    {
        yield return new WaitForSeconds(delay);
        canvas.SetActive(false);
    }


    public void DisplayNotification(float popindelay, float popoutdelay, string message)
    {
        canvas.SetActive(true);
        messagepanel.SetActive(true);
        StartCoroutine(PopInPanel(popindelay, message));
        StartCoroutine(PopOutPanel(popoutdelay));
        DisableCanvas(3);
    }

    public void PointToScene(string scene)
    {
        if(scene == "new game")
        {
            canvas.SetActive(true);
            newgamefinger.SetActive(true);
        }
        else if (scene == "equiptment")
        {
            canvas.SetActive(true);
            equiptmentfinger.SetActive(true);
        }
    }

    public void ExplainBlock(string tutprog)
    {
        if(tutprog == "melee2intro")
        {
            DisplayNotification(0, 5, "This is the second melee block that you can use.");
        }
        if (tutprog == "ranged1intro")
        {
            DisplayNotification(0, 5, "Calling this skill fires a bullet towards the enemies.");
        }
        if(tutprog == "ranged2intro")
        {
            DisplayNotification(0, 5, "This is the second ranged block that you can use.");
        }
        if(tutprog == "repeat10intro")
        {
            DisplayNotification(0, 5, "This block allows you to repeat the actions inside it for 10 times when activated.");
        }
        SaveSystem.SaveTutorialProgress("empty");
    }



}
