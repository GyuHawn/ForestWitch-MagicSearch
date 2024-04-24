using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class UIManager : EditorWindow{

    private static int toolbarInt  = 0;
    private static Vector2 scrollPos = Vector2.zero;
    private static Texture2D baseTex, accentTex, secAccentTex;
    private static float buttonSizeX, buttonSizeY; 
    private static List<Texture> backgrounds = new List<Texture>(),
        banners = new List<Texture>(),
        buttons = new List<Texture>(),
        imageFrames = new List<Texture>(),
        progressbars = new List<Texture>(),
        other = new List<Texture>();
    private static EditorWindow window;

    [MenuItem("UI/UI Manager")]
    public static void ShowWindow(){
        window = EditorWindow.GetWindow<UIManager>(false, "UI Manager");
        LoadMiniatures(window);
    }

    static Texture FindTexture(string s){
        string phrase = s + " t:texture l:editor l:ui";
        if(AssetDatabase.FindAssets(s + " t:texture l:editor l:ui").Length == 0){
            Debug.Log("NOT FOUND: " + phrase);
            return null;
        }
        return AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets(s + " t:texture l:editor l:ui")[0]), typeof(Texture)) as Texture;
    }

    static GameObject FindPrefab(string s){
        string phrase = s + " t:prefab l:ui";
        if(AssetDatabase.FindAssets(s + " t:prefab l:ui").Length == 0){
            Debug.Log("NOT FOUND: " + phrase);
            return null;
        }
        return AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets(s + " t:prefab l:ui")[0]), typeof(GameObject)) as GameObject;
    }

    static void LoadMiniatures(EditorWindow window){
        baseTex = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        baseTex.SetPixel(0, 0, new Color(12f / 255f, 12f / 255f, 12f / 255f));
        baseTex.Apply();

        accentTex = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        accentTex.SetPixel(0, 0, new Color(18f / 255f, 18f / 255f, 18f / 255f));
        accentTex.Apply();

        secAccentTex = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        secAccentTex.SetPixel(0, 0, new Color(22f / 255f, 140f / 255f, 239f / 255f));
        secAccentTex.Apply();

        backgrounds.Clear();
        banners.Clear();
        buttons.Clear();
        imageFrames.Clear();
        progressbars.Clear();
        other.Clear();

        backgrounds.Add(FindTexture("background_large"));
        backgrounds.Add(FindTexture("background_medium"));
        backgrounds.Add(FindTexture("message_background_01"));
        backgrounds.Add(FindTexture("message_background_02"));
        backgrounds.Add(FindTexture("message_background_04"));
        backgrounds.Add(FindTexture("message_background_05"));
        backgrounds.Add(FindTexture("message_background_06"));
        backgrounds.Add(FindTexture("short_message_background_01"));
        backgrounds.Add(FindTexture("short_message_background_02"));
        backgrounds.Add(FindTexture("short_message_background_03"));
        backgrounds.Add(FindTexture("short_message_background_04"));
        backgrounds.Add(FindTexture("short_message_background_05"));
        backgrounds.Add(FindTexture("short_message_background_06"));
        backgrounds.Add(FindTexture("short_message_background_07"));

        banners.Add(FindTexture("banner_01"));
        banners.Add(FindTexture("banner_02"));
        banners.Add(FindTexture("banner_03"));
        banners.Add(FindTexture("banner_04"));

        buttons.Add(FindTexture("button_01"));
        buttons.Add(FindTexture("button_02"));
        buttons.Add(FindTexture("button_03"));
        buttons.Add(FindTexture("button_04"));
        buttons.Add(FindTexture("pad_button_a"));
        buttons.Add(FindTexture("pad_button_b"));
        buttons.Add(FindTexture("pad_button_x"));
        buttons.Add(FindTexture("pad_button_y"));
        buttons.Add(FindTexture("button_previous"));
        buttons.Add(FindTexture("button_next"));

        imageFrames.Add(FindTexture("image_background_01"));
        imageFrames.Add(FindTexture("image_background_02"));
        imageFrames.Add(FindTexture("image_background_03"));
        imageFrames.Add(FindTexture("image_background_04"));
        imageFrames.Add(FindTexture("image_background_05"));
        imageFrames.Add(FindTexture("image_background_06"));

        progressbars.Add(FindTexture("progressbar_01"));
        progressbars.Add(FindTexture("progressbar_02"));

        other.Add(FindTexture("carousel"));
        other.Add(FindTexture("stepsslider"));
        other.Add(FindTexture("selector"));
        other.Add(FindTexture("image_select"));
        other.Add(FindTexture("exclamation"));
        other.Add(FindTexture("exclamation_active"));
        other.Add(FindTexture("star_slot"));
        other.Add(FindTexture("star"));
        other.Add(FindTexture("toggle_01"));
        other.Add(FindTexture("toggle_02"));
        other.Add(FindTexture("toggle_03"));
        other.Add(FindTexture("toggle_04"));
        other.Add(FindTexture("message_01"));
        other.Add(FindTexture("keys_assign"));
        other.Add(FindTexture("scroll_view"));
        other.Add(FindTexture("message_02"));
        other.Add(FindTexture("message_03"));
        other.Add(FindTexture("message_04"));
        other.Add(FindTexture("message_05"));
    }

    void OnGUI(){
        if(window == null)
            window = EditorWindow.GetWindow<UIManager>();

        if(backgrounds.Count == 0 || baseTex == null)
            LoadMiniatures(window);

        GUI.DrawTexture(new Rect(0, 0, maxSize.x, maxSize.y), baseTex, ScaleMode.StretchToFill);

        buttonSizeX = window.position.width / 3 - 12;
        buttonSizeY = window.position.width / 3 - 12;

        GUIStyle style = new GUIStyle();
        style.padding = new RectOffset(10, 10, 10, 10);
        style.alignment = TextAnchor.MiddleCenter;
        style.margin = new RectOffset(5, 5, 5, 5);
        style.normal.background = accentTex; 
        style.normal.textColor = Color.white;
            style.onNormal.background = style.active.background = secAccentTex;
            style.onNormal.textColor = style.active.textColor = Color.white;
        
        toolbarInt = GUILayout.Toolbar(toolbarInt, new string[] {"Backgrounds", "Banners", "Buttons"}, style);
        toolbarInt = 3 + GUILayout.Toolbar(toolbarInt - 3, new string[] {"Image frames", "Progressbars", "Other"}, style);

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        switch(toolbarInt){
            case 0: //Backgrounds
                GUILayout.BeginVertical();
                GUILayout.BeginHorizontal();
                    AddButton(backgrounds[0], style, () => Add(FindPrefab("background_large")));
                    AddButton(backgrounds[1], style, () => Add(FindPrefab("background_medium")));
                    AddButton(backgrounds[2], style, () => Add(FindPrefab("message_background_01")));
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                    AddButton(backgrounds[3], style, () => Add(FindPrefab("message_background_02")));
                    AddButton(backgrounds[4], style, () => Add(FindPrefab("message_background_04")));
                    AddButton(backgrounds[5], style, () => Add(FindPrefab("message_background_05")));
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                    AddButton(backgrounds[6], style, () => Add(FindPrefab("message_background_06")));
                    AddButton(backgrounds[7], style, () => Add(FindPrefab("short_message_background_01")));
                    AddButton(backgrounds[8], style, () => Add(FindPrefab("short_message_background_02")));
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                    AddButton(backgrounds[9], style, () => Add(FindPrefab("short_message_background_03")));
                    AddButton(backgrounds[10], style, () => Add(FindPrefab("short_message_background_04")));
                    AddButton(backgrounds[11], style, () => Add(FindPrefab("short_message_background_05")));
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                    AddButton(backgrounds[12], style, () => Add(FindPrefab("short_message_background_06")));
                    AddButton(backgrounds[13], style, () => Add(FindPrefab("short_message_background_07")));
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
                break;
            
            case 1: //Banners
                GUILayout.BeginVertical();
                GUILayout.BeginHorizontal();
                    AddButton(banners[0], style, () => Add(FindPrefab("banner_01")));
                    AddButton(banners[1], style, () => Add(FindPrefab("banner_02")));
                    AddButton(banners[2], style, () => Add(FindPrefab("banner_03")));
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                    AddButton(banners[3], style, () => Add(FindPrefab("banner_04")));
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
                break;

            case 2: //Buttons
                GUILayout.BeginVertical();
                GUILayout.BeginHorizontal();
                    AddButton(buttons[0], style, () => Add(FindPrefab("button_01")));
                    AddButton(buttons[1], style, () => Add(FindPrefab("button_02")));
                    AddButton(buttons[2], style, () => Add(FindPrefab("button_03")));
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                    AddButton(buttons[3], style, () => Add(FindPrefab("button_04")));
                    AddButton(buttons[4], style, () => Add(FindPrefab("pad_button_a")));
                    AddButton(buttons[5], style, () => Add(FindPrefab("pad_button_b")));
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                    AddButton(buttons[6], style, () => Add(FindPrefab("pad_button_x")));
                    AddButton(buttons[7], style, () => Add(FindPrefab("pad_button_y")));
                    AddButton(buttons[8], style, () => Add(FindPrefab("button_previous")));
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                    AddButton(buttons[9], style, () => Add(FindPrefab("button_next")));
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
                break;
            
            case 3: //Image frames
                GUILayout.BeginVertical();
                GUILayout.BeginHorizontal();
                    AddButton(imageFrames[0], style, () => Add(FindPrefab("image_background_01")));
                    AddButton(imageFrames[1], style, () => Add(FindPrefab("image_background_02")));
                    AddButton(imageFrames[2], style, () => Add(FindPrefab("image_background_03")));
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                    AddButton(imageFrames[3], style, () => Add(FindPrefab("image_background_04")));
                    AddButton(imageFrames[4], style, () => Add(FindPrefab("image_background_05")));
                    AddButton(imageFrames[5], style, () => Add(FindPrefab("image_background_06")));
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
                break;
            
            case 4: //Progressbars
                GUILayout.BeginVertical();
                GUILayout.BeginHorizontal();
                    AddButton(progressbars[0], style, () => Add(FindPrefab("progressbar_01")));
                    AddButton(progressbars[1], style, () => Add(FindPrefab("progressbar_02")));
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
                break;

            case 5: //Other
                GUILayout.BeginVertical();
                GUILayout.BeginHorizontal();
                    AddButton(other[0], style, () => Add(FindPrefab("carousel")));
                    AddButton(other[1], style, () => Add(FindPrefab("stepsslider")));
                    AddButton(other[2], style, () => Add(FindPrefab("selector")));
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                    AddButton(other[3], style, () => Add(FindPrefab("image_select")));
                    AddButton(other[4], style, () => Add(FindPrefab("exclamation")));
                    AddButton(other[5], style, () => Add(FindPrefab("exclamation_active")));
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                    AddButton(other[6], style, () => Add(FindPrefab("star_slot")));
                    AddButton(other[7], style, () => Add(FindPrefab("star")));
                    AddButton(other[8], style, () => Add(FindPrefab("toggle_01")));
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                    AddButton(other[9], style, () => Add(FindPrefab("toggle_02")));
                    AddButton(other[10], style, () => Add(FindPrefab("toggle_03")));
                    AddButton(other[11], style, () => Add(FindPrefab("toggle_04")));
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                    AddButton(other[12], style, () => Add(FindPrefab("message_01")));
                    AddButton(other[13], style, () => Add(FindPrefab("keys_assign")));
                    AddButton(other[14], style, () => Add(FindPrefab("scroll_view")));
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                    AddButton(other[15], style, () => Add(FindPrefab("message_02")));
                    AddButton(other[16], style, () => Add(FindPrefab("message_03")));
                    AddButton(other[17], style, () => Add(FindPrefab("message_04")));
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                    AddButton(other[18], style, () => Add(FindPrefab("message_05")));
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
                break;
        }

        EditorGUILayout.EndScrollView();
    }

    void Add(GameObject gameObject){
        GameObject go = PrefabUtility.InstantiatePrefab(gameObject as GameObject) as GameObject;
        if(Selection.activeTransform != null)
            go.transform.SetParent(Selection.activeTransform); 
        go.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        go.name = go.name.Replace("(Clone)", "");
        Selection.activeTransform = go.transform;
    }

    void AddButton(Texture miniature, GUIStyle style, Action func){
        if(GUILayout.Button(new GUIContent(miniature), style, GUILayout.Height(buttonSizeX), GUILayout.Width(buttonSizeY))){
            func();
        }
    }
}
