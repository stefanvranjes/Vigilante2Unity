using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelEditor
{
    private static string previousPath = "";

    [MenuItem("CONTEXT/LevelManager/Import XBMP")]
    public static void ImportXBMP(MenuCommand menuCommand)
    {
        string file = EditorUtility.OpenFilePanel("Open xbmp file to load asset", "", "");
        string name = Path.GetFileNameWithoutExtension(file);
        string save = EditorUtility.SaveFilePanel("Save location for .bmp files", "", name, "bmp");

        IMP_XBMP.LoadAsset(file, save);
    }

    [MenuItem("CONTEXT/LevelManager/Import XBGM")]
    public static void ImportXBGM(MenuCommand menuCommand)
    {
        string file = EditorUtility.OpenFilePanel("Open xbgm file to load asset", "", "");
        string name = Path.GetFileNameWithoutExtension(file);
        string save = EditorUtility.SaveFilePanel("Save location for .bmp files", "", name, "bmp");

        IMP_XBGM.LoadAsset(file, save);
    }

    [MenuItem("CONTEXT/LevelManager/Import COLS")]
    public static void ImportCOLS(MenuCommand menuCommand)
    {
        string file = EditorUtility.OpenFilePanel("Open cols file to load asset", "", "");

        IMP_COLS.LoadAsset(file);
    }

    [MenuItem("CONTEXT/LevelManager/Import SUNA")]
    public static void ImportSUNA(MenuCommand menuCommand)
    {
        string file = EditorUtility.OpenFilePanel("Open suna file to load asset", "", "");

        IMP_SUNA.LoadAsset(file);
    }

    [MenuItem("CONTEXT/LevelManager/Import HEAD")]
    public static void ImportHEAD(MenuCommand menuCommand)
    {
        string file = EditorUtility.OpenFilePanel("Open head file to load asset", "", "");

        IMP_HEAD.LoadAsset(file);
    }

    [MenuItem("CONTEXT/LevelManager/Import TITL")]
    public static void ImportTITL(MenuCommand menuCommand)
    {
        string file = EditorUtility.OpenFilePanel("Open titl file to load asset", "", "");

        IMP_TITL.LoadAsset(file);
    }

    [MenuItem("CONTEXT/LevelManager/Import TEXT")]
    public static void ImportTEXT(MenuCommand menuCommand)
    {
        string file = EditorUtility.OpenFilePanel("Open text file to load asset", "", "");

        IMP_TEXT.LoadAsset(file);
    }

    [MenuItem("CONTEXT/LevelManager/Import BSP")]
    public static void ImportBSP(MenuCommand menuCommand)
    {
        string file = EditorUtility.OpenFilePanel("Open bsp file to load asset", "", "");

        IMP_BSP.LoadAsset(file);
    }
}
