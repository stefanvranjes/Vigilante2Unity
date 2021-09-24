using System.IO;
using UnityEngine;
using UnityEditor;

public class MenuItems
{
    [MenuItem("Tools/Import TIM")]
    private static void ImportTIM()
    {
        string file = EditorUtility.OpenFilePanel("Open .tim file to load asset", "", "tim");
        string name = Path.GetFileNameWithoutExtension(file);
        string save = EditorUtility.SaveFilePanel("Save location for .bmp files", "", name, "bmp");

        IMP_TIM.LoadAsset(file, save);
    }

    [MenuItem("Tools/Export EXP")]
    private static void ExportEXP()
    {
        string file = EditorUtility.OpenFilePanel("Open .exp file to extract asset", "", "exp");
        string save = EditorUtility.SaveFolderPanel("Save location", "", "");

        EXP_EXP.ExtractEXP(file, save);
    }

    [MenuItem("Tools/Export XOB")]
    private static void ExportXOB()
    {
        string file = EditorUtility.OpenFilePanel("Open .xob file to extract asset", "", "xob");
        string save = EditorUtility.SaveFolderPanel("Save location", "", "");

        EXP_XOB.ExtractXOB(file, save);
    }

    [MenuItem("Tools/Export SND")]
    private static void ExportSND()
    {
        string file = EditorUtility.OpenFilePanel("Open .snd file to extract asset", "", "snd");
        string save = EditorUtility.SaveFolderPanel("Save location", "", "");

        EXP_SND.ExtractSND(file, save);
    }
}
