using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonUtils
{

    public static string LoadJsonFile(string filePath)
    {
        return File.ReadAllText(filePath);
    }
    public static List<T> FromJsonToList<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return new List<T>(wrapper.items);
    }

    public static T FromJsonToObject<T>(string json)
    {
        return JsonUtility.FromJson<T>(json);
    }

    public static string FromListToJson<T>(List<T> list, bool isPrettyPrintActivated)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.items = list.ToArray();
        return JsonUtility.ToJson(wrapper, isPrettyPrintActivated);
    }

    public static string FromObjectToJson<T>(T objectToConvert, bool isPrettyPrintActivated)
    {
        return JsonUtility.ToJson(objectToConvert, isPrettyPrintActivated);
    }
}
