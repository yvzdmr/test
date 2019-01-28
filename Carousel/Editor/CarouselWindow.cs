using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CarouselWindow : EditorWindow
{
    public string carouselName;

    const string parentFolder = "Assets";
    const string folderName = "Carousels";

    [MenuItem("Window/Carousel")]
    public static void ShowWindow() {
        GetWindow<CarouselWindow>("Carousel Creator");
    }

    private void OnGUI() {


        GUILayout.BeginVertical();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Carousel Name", EditorStyles.boldLabel);
        carouselName = GUILayout.TextField(carouselName).Replace(" ", "");
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Create"))
            Create();
        GUILayout.EndVertical();
    }

    void Create() {
        if (IsCarouselFolderExist(carouselName)) {
            Debug.LogError(carouselName + " Carousel Already exist!!!");
            return;
        }

        if (!string.IsNullOrEmpty(carouselName)) {

            string carouselDataName = carouselName + "CarouselData";
            string carouselItemName = carouselName + "CarouselItem";
            string _carouselName = carouselName + "Carousel";

            GenerateCarouselDataScript(carouselDataName);
            GenerateCarouselItemScript(carouselItemName, carouselDataName);
            GenerateCarouselScript(_carouselName, carouselItemName, carouselDataName);

            AssetDatabase.Refresh();
            
        }
        else {
            Debug.LogError("Carousel name must be set");
        }

    }

    void GenerateCarouselDataScript(string carouselDataName) {
        var script = GetTemplate("CarouselDataTemplate").Replace("#CarouselDataName#", carouselDataName);
        CreateScript(carouselDataName, script);
    }
    void GenerateCarouselItemScript(string carouselItemName, string carouselDataName) {
        var script = GetTemplate("CarouselItemTemplate").Replace("#CarouselItemName#", carouselItemName).Replace("#CarouselDataName#", carouselDataName);
        CreateScript(carouselItemName, script);
    }
    void GenerateCarouselScript(string carouselName, string carouselItemName, string carouselDataName) {
        var script = GetTemplate("CarouselTemplate").Replace("#CarouselName#", carouselName).Replace("#CarouselItemName#", carouselItemName).Replace("#CarouselDataName#", carouselDataName);
        CreateScript(carouselName, script);
    }


    string GetTemplate(string templateFileName) {
        string path = GetTemplateFolderPath() + "/" + templateFileName + ".txt";
        return File.ReadAllText(path);
    }

    string GetTemplateFolderPath() {
        MonoScript ms = MonoScript.FromScriptableObject(this);
        string path = AssetDatabase.GetAssetPath(ms);
        var pathItems = path.Split('/');
        string parentFolderPath = "";
        for(int i = 1; i < pathItems.Length - 1; i++) { 
            parentFolderPath += pathItems[i] + "/";
        }
        return Application.dataPath + "/" + parentFolderPath + "Templates";
    }

    void CreateScript(string fileName, string scriptContent) {
        EnsureCarouselsFolder();
        File.WriteAllText(Application.dataPath + "/" + folderName + "/" + carouselName + "/" + fileName + ".cs", scriptContent);
    }

    void EnsureCarouselsFolder() {
        if (!AssetDatabase.IsValidFolder(parentFolder + "/" + folderName)) {
            AssetDatabase.CreateFolder(parentFolder, folderName);
        }
        if(!AssetDatabase.IsValidFolder(parentFolder + "/" + folderName + "/" + carouselName)) {
            AssetDatabase.CreateFolder(parentFolder + "/" + folderName, carouselName);
        }
    }

    bool IsCarouselFolderExist(string carouselName) {
        return AssetDatabase.IsValidFolder(parentFolder + "/" + folderName + "/" + carouselName);
    }
}

