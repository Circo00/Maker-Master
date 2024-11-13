using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int level_to_unlock;
    public List<GameObject> levellist = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //SaveSystem.SaveLevelData(level_to_unlock);
        //Debug.Log(SaveSystem.LoadLevelData());
        EnableLevels();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableLevels()
    {
        int leveldata = SaveSystem.LoadLevelData().level;

        foreach(GameObject level in levellist)
        {
            level.GetComponent<Button>().enabled = false;
            level.GetComponent<Image>().color = new Color(0.21f, 0.23f, 0.35f);
            level.transform.GetChild(0).GetComponent<Image>().color = new Color(0.21f, 0.23f, 0.35f);
        }

        for (int i = 0; i < leveldata; i++)
        {
            levellist[i].GetComponent<Button>().enabled = true;
            levellist[i].GetComponent<Image>().color = new Color(1f, 1f, 1f);
            levellist[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f);
        }

    }

}
