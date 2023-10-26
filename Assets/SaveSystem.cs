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
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }
}
