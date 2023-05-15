﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CI.QuickSave;

public static class LocalizationManager
{
     static readonly Dictionary<string, Dictionary<string, string>> Dictionary = new Dictionary<string, Dictionary<string, string>>();
    public static event Action LocalizationChanged = () => { };

    private static string _language = "hz";
    public static string Language
    {
        get => _language;
        set { 
            _language = value;
            LocalizationChanged();
            var writer = QuickSaveWriter.Create("Language");
            writer.Write("current", value);
            writer.Commit();
        }
    }

    /// <summary>
    /// Чтение словаря переводов
    /// </summary>
    public static void Read()
    {
        string path = "Localization";
        var lines = Resources
            .Load<TextAsset>(path)
            .text
            .Split('\n')
            .Where(x => x != "").ToList();
        var languages = lines[0].Split(',').Skip(1).Select(i => i.Trim()).ToList();

        for (var i = 0; i < languages.Count; i++)
        {
            if (!Dictionary.ContainsKey(languages[i]))
            {
                Dictionary.Add(languages[i], new Dictionary<string, string>());
            }
        }

        for (var i = 1; i < lines.Count; i++)
        {
            var columns = lines[i].Split(',').Select(j => j.Trim()).ToList();
            var key = columns[0];

            if (key == "") continue;

            for (var j = 0; j < languages.Count; j++)
            {
                Dictionary[languages[j]].Add(key, columns[j+1].Replace("[_line_]", "\n"));
            }
        }
    }

    /// <summary>
    /// Получение текста по ключу
    /// </summary>
    /// <param name="localizationKey"></param>
    /// <returns>Строка на текущем языке</returns>
    public static string Localize(string localizationKey)
    {
        if(_language == "hz")
        {
            var reader = QSReader.Create("Language");
            string val;
            try
            {
                val = reader.Read<string>("current");
            }
            catch 
            {
                val = "English";
                //Debug.LogWarning("no saves");
            }
            _language = val;
        }

        if (Dictionary.Count == 0)
            Read();

        if (!Dictionary.ContainsKey(Language)) return "Language not found";

        if (!Dictionary[Language].ContainsKey(localizationKey)) return "Word not found";

        if (Dictionary[Language][localizationKey] == "") return "Empty translation";

        return Dictionary[Language][localizationKey];
    }
}
