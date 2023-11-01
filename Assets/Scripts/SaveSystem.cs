using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveSkill()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saved.skill";
        FileStream stream = new FileStream(path, FileMode.Create);

        SkillData skilldata = new SkillData();

        formatter.Serialize(stream, skilldata);
        stream.Close();
    }

    public static SkillData LoadSkill()
    {
        string path = Application.persistentDataPath + "/saved.skill";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SkillData data = formatter.Deserialize(stream) as SkillData;
            stream.Close();
            return data;
        }
        else
        {
            SaveSkill();
            LoadSkill();
            return null;
        }
    }

    public static void SaveTutorialProgress(string tutprogressname)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/tutprogress.tut";
        FileStream stream = new FileStream(path, FileMode.Create);

        TutorialProgressData tutprogressdata = new TutorialProgressData(tutprogressname);

        formatter.Serialize(stream, tutprogressdata);
        stream.Close();
    }

    public static TutorialProgressData LoadTutorialProgress()
    {
        string path = Application.persistentDataPath + "/tutprogress.tut";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            TutorialProgressData tutprogressdata = formatter.Deserialize(stream) as TutorialProgressData;
            stream.Close();
            return tutprogressdata;
        }
        else
        {
            
            SaveTutorialProgress("buildtut");

            return LoadTutorialProgress();
        }
    }

    public static void ResetData()
    {
        foreach (var directory in Directory.GetDirectories(Application.persistentDataPath))
        {
            DirectoryInfo data_dir = new DirectoryInfo(directory);
            data_dir.Delete(true);
        }

        foreach (var file in Directory.GetFiles(Application.persistentDataPath))
        {
            FileInfo file_info = new FileInfo(file);
            file_info.Delete();
        }
    }




}
