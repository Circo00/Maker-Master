using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    public GameObject canvas;
    public int unlocklevel;
    public string unlockskill;
    public string tutprogress;
    public TutorialManager tutorialmanager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Activated Level Complete");
            if(SaveSystem.LoadLevelData().level < unlocklevel)
            {
                SaveSystem.SaveLevelData(unlocklevel);
                Debug.Log("unlocked");
            }
            //if not yet unlocked
            if (!SaveSystem.LoadUnlockedBlock().GetUnlockedBlockData(unlockskill))
            {
                
                SaveSystem.UnlockBlock(unlockskill);
                SaveSystem.SaveTutorialProgress(tutprogress);
                tutorialmanager.DisplayNotification(0, 3, "New Skill Unlocked!");
            }
            
            canvas.SetActive(true);
            
        }

    }
}
