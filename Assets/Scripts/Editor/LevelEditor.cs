using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelEditor
{
    private static string previousSave = Application.dataPath;
    private static string previousOpen = "";

    [MenuItem("CONTEXT/LevelManager/Import XBMP")]
    public static void ImportXBMP(MenuCommand menuCommand)
    {
        string file = EditorUtility.OpenFilePanel("Open xbmp file to load asset", previousOpen, "");
        string save = EditorUtility.SaveFolderPanel("Save location for .bmp files", previousSave, "");
        previousOpen = Path.GetDirectoryName(file);
        previousSave = Path.GetDirectoryName(save);

        IMP_XBMP.LoadAsset(file, save);
    }

    [MenuItem("CONTEXT/LevelManager/Import XBGM")]
    public static void ImportXBGM(MenuCommand menuCommand)
    {
        string file = EditorUtility.OpenFilePanel("Open xbgm file to load asset", previousOpen, "");
        string save = EditorUtility.SaveFolderPanel("Save location for .bmp files", previousSave, "");
        previousOpen = Path.GetDirectoryName(file);
        previousSave = Path.GetDirectoryName(save);

        IMP_XBGM.LoadAsset(file, save);
    }

    [MenuItem("CONTEXT/LevelManager/Import COLS")]
    public static void ImportCOLS(MenuCommand menuCommand)
    {
        string file = EditorUtility.OpenFilePanel("Open cols file to load asset", previousOpen, "");
        previousOpen = Path.GetDirectoryName(file);

        IMP_COLS.LoadAsset(file);
    }

    [MenuItem("CONTEXT/LevelManager/Import SUNA")]
    public static void ImportSUNA(MenuCommand menuCommand)
    {
        string file = EditorUtility.OpenFilePanel("Open suna file to load asset", previousOpen, "");
        previousOpen = Path.GetDirectoryName(file);

        IMP_SUNA.LoadAsset(file);
    }

    [MenuItem("CONTEXT/LevelManager/Import HEAD")]
    public static void ImportHEAD(MenuCommand menuCommand)
    {
        string file = EditorUtility.OpenFilePanel("Open head file to load asset", previousOpen, "");
        previousOpen = Path.GetDirectoryName(file);

        IMP_HEAD.LoadAsset(file);
    }

    [MenuItem("CONTEXT/LevelManager/Import TITL")]
    public static void ImportTITL(MenuCommand menuCommand)
    {
        string file = EditorUtility.OpenFilePanel("Open titl file to load asset", previousOpen, "");
        previousOpen = Path.GetDirectoryName(file);

        IMP_TITL.LoadAsset(file);
    }

    [MenuItem("CONTEXT/LevelManager/Import TEXT")]
    public static void ImportTEXT(MenuCommand menuCommand)
    {
        string file = EditorUtility.OpenFilePanel("Open text file to load asset", previousOpen, "");
        previousOpen = Path.GetDirectoryName(file);

        IMP_TEXT.LoadAsset(file);
    }

    [MenuItem("CONTEXT/LevelManager/Import AIMP")]
    public static void ImportAIMP(MenuCommand menuCommand)
    {
        string file = EditorUtility.OpenFilePanel("Open aimp file to load asset", previousOpen, "");
        previousOpen = Path.GetDirectoryName(file);

        IMP_AIMP.LoadAsset(file);
    }

    [MenuItem("CONTEXT/LevelManager/Import BSP")]
    public static void ImportBSP(MenuCommand menuCommand)
    {
        string file = EditorUtility.OpenFilePanel("Open bsp file to load asset", previousOpen, "");
        previousOpen = Path.GetDirectoryName(file);

        IMP_BSP.LoadAsset(file);
    }

    [MenuItem("CONTEXT/LevelManager/Import OBJ")]
    public static void ImportOBJ(MenuCommand menuCommand)
    {
        string file = EditorUtility.OpenFilePanel("Open obj file to load asset", previousOpen, "");
        previousOpen = Path.GetDirectoryName(file);
        string name = Path.GetFileNameWithoutExtension(file);
        int index = (byte)name[name.Length - 1] - 0x30;

        while (true)
        {
            if (!IMP_OBJ.LoadAsset(file)) break;

            index++;
            file = previousOpen + "\\FORM" + index.ToString().PadLeft(4, '0');
        }
    }

    [MenuItem("CONTEXT/LevelManager/Build Junction")]
    public static void BuildJunction(MenuCommand menuCommand)
    {
        LevelManager levelManager = menuCommand.context as LevelManager;
        levelManager.FUN_7E6C();
        EditorUtility.SetDirty(levelManager);
    }

    [MenuItem("CONTEXT/XOBF_DB/Import BIN")]
    public static void ImportBIN(MenuCommand menuCommand)
    {
        string file = EditorUtility.OpenFilePanel("Open xobf file to load asset", previousOpen, "");
        previousOpen = Path.GetDirectoryName(file);

        XOBF_DB xobf = menuCommand.context as XOBF_DB;
        xobf.LoadDB(file, "BIN ");
    }

    [MenuItem("CONTEXT/XOBF_DB/Import ANM")]
    public static void ImportANM(MenuCommand menuCommand)
    {
        string file = EditorUtility.OpenFilePanel("Open xobf file to load asset", previousOpen, "");
        previousOpen = Path.GetDirectoryName(file);

        XOBF_DB xobf = menuCommand.context as XOBF_DB;
        xobf.LoadDB(file, "ANM ");
    }

    [MenuItem("CONTEXT/XOBF_DB/Import SND")]
    public static void ImportSND(MenuCommand menuCommand)
    {
        string file = EditorUtility.OpenFilePanel("Open xobf file to load asset", previousOpen, "");
        previousOpen = Path.GetDirectoryName(file);

        XOBF_DB xobf = menuCommand.context as XOBF_DB;
        xobf.LoadDB(file, "SND ");
    }

    [MenuItem("CONTEXT/JUNC_DB/Import JUNC")]
    public static void ImportJUNC(MenuCommand menuCommand)
    {
        string file = EditorUtility.OpenFilePanel("Open junc file to load asset", previousOpen, "");
        previousOpen = Path.GetDirectoryName(file);
        string name = Path.GetFileNameWithoutExtension(file);
        int index = (byte)name[name.Length - 1] - 0x30;
        JUNC_DB junc = menuCommand.context as JUNC_DB;

        while (true)
        {
            if (!junc.LoadDB(file)) break;

            index++;
            file = previousOpen + "\\JUNC" + index.ToString().PadLeft(4, '0');
            GameObject obj = new GameObject("JUNC" + index.ToString().PadLeft(2, '0'));
            junc = obj.AddComponent<JUNC_DB>();
        }
    }

    [MenuItem("CONTEXT/RSEG_DB/Import RSEG")]
    public static void ImportRSEG(MenuCommand menuCommand)
    {
        string file = EditorUtility.OpenFilePanel("Open rseg file to load asset", previousOpen, "");
        previousOpen = Path.GetDirectoryName(file);
        string name = Path.GetFileNameWithoutExtension(file);
        int index = (byte)name[name.Length - 1] - 0x30;
        RSEG_DB rseg = menuCommand.context as RSEG_DB;

        while (true)
        {
            if (!rseg.LoadDB(file)) break;

            index++;
            file = previousOpen + "\\RSEG" + index.ToString().PadLeft(4, '0');
            GameObject obj = new GameObject("RSEG" + index.ToString().PadLeft(2, '0'));
            rseg = obj.AddComponent<RSEG_DB>();
        }
    }

    [MenuItem("CONTEXT/XRTP_DB/Import XRTP")]
    public static void ImportXRPT(MenuCommand menuCommand)
    {
        string file = EditorUtility.OpenFilePanel("Open xrtp file to load asset", previousOpen, "");
        previousOpen = Path.GetDirectoryName(file);

        XRTP_DB xrtp = menuCommand.context as XRTP_DB;
        xrtp.LoadDB(file);
    }

    [MenuItem("CONTEXT/RECT_DB/Import RECT")]
    public static void ImportRECT(MenuCommand menuCommand)
    {
        string file = EditorUtility.OpenFilePanel("Open rect file to load asset", previousOpen, "");
        previousOpen = Path.GetDirectoryName(file);
        string name = Path.GetFileNameWithoutExtension(file);
        int index = (byte)name[name.Length - 1] - 0x30;
        RECT_DB rect = menuCommand.context as RECT_DB;

        while (true)
        {
            if (!rect.LoadDB(file)) break;

            index++;
            file = previousOpen + "\\RECT" + index.ToString().PadLeft(4, '0');
            GameObject obj = new GameObject("RECT" + index.ToString().PadLeft(2, '0'));
            rect = obj.AddComponent<RECT_DB>();
        }
    }
}
