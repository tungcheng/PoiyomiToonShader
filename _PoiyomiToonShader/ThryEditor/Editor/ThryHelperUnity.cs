﻿// Material/Shader Inspector for Unity 2017/2018
// Copyright (C) 2019 Thryrallo

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Thry
{
    public class UnityHelper
    {
        public static string FindPathOfAssetWithExtension(string filename)
        {
            string[] guids = AssetDatabase.FindAssets(filename.RemoveFileExtension());
            foreach (string s in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(s);
                if (path.EndsWith(filename))
                    return path;
            }
            return filename;
        }

        public static List<string> FindAssetOfFilesWithExtension(string filename)
        {
            List<string> ret = new List<string>();
            string[] guids = AssetDatabase.FindAssets(filename.RemoveFileExtension());
            foreach (string s in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(s);
                if (path.EndsWith(filename))
                    ret.Add(path);
            }
            return ret;
        }

        public static void SetDefineSymbol(string symbol, bool active)
        {
            SetDefineSymbol(symbol, active, false);
        }

        public static void SetDefineSymbol(string symbol, bool active, bool refresh_if_changed)
        {
            try
            {
                string symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(
                        BuildTargetGroup.Standalone);
                if (!symbols.Contains(symbol) && active)
                {
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(
                                  BuildTargetGroup.Standalone, symbols + ";" + symbol);
                    AssetDatabase.Refresh();
                }
                else if (symbols.Contains(symbol) && !active)
                {
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(
                                  BuildTargetGroup.Standalone, Regex.Replace(symbols, @";?" + @symbol, ""));
                    AssetDatabase.Refresh();
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        public static void RepaintInspector(System.Type t)
        {
            Editor[] ed = (Editor[])Resources.FindObjectsOfTypeAll<Editor>();
            for (int i = 0; i < ed.Length; i++)
            {
                if (ed[i].GetType() == t)
                {
                    ed[i].Repaint();
                    return;
                }
            }
        }

        public static void RepaintEditorWindow(Type t)
        {
            EditorWindow window = FindEditorWindow(t);
            if (window != null) window.Repaint();
        }

        public static EditorWindow FindEditorWindow(System.Type t)
        {
            EditorWindow[] ed = (EditorWindow[])Resources.FindObjectsOfTypeAll<EditorWindow>();
            for (int i = 0; i < ed.Length; i++)
            {
                if (ed[i].GetType() == t)
                {
                    return ed[i];
                }
            }
            return null;
        }
    }

}