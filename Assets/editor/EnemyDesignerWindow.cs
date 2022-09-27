using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;
using UnityEditor; //Any script with this statement will not be included in build version

public class EnemyDesignerWindow : EditorWindow
{

    Texture2D headerSectionTexture;
    Texture2D rougeSectionTexture;
    Texture2D warriorSectionTexture;
    Texture2D mageSectionTexture;
    Texture2D healerSectionTexture;

    Color headerSectionColor = new Color(13f / 255f, 32f / 255f, 44f / 255f, 1f);

    //Rect is 2D rectangle
    Rect headerSection;
    Rect rougeSection;
    Rect warriorSection;
    Rect mageSection;
    Rect healerSection;

    static MageData mageData;
    static WarriorData warriorData;
    static RougeData rougeData;
    static HealerData healerData;

    //Methods to return data pointers above
    public static MageData MageInfo { get { return mageData; } }
    public static WarriorData WarriorInfo { get { return warriorData; } }
    public static HealerData HealerInfo { get { return healerData; } }
    public static RougeData RougeInfo { get { return rougeData; } }



    [MenuItem("Window/Enemy Designer")] //Menu item and then drop down is where to open
    static void OpenWindow()
    {
        EnemyDesignerWindow window = (EnemyDesignerWindow)GetWindow(typeof(EnemyDesignerWindow));
        window.minSize = new Vector2(600, 300);
        window.Show();
    }

    //Similar to Start or Awake function
    private void OnEnable()
    {
        InitTextures();
        InitData();
    }

    public static void InitData()
    {
        //Casting the scriptable objects created
        mageData = (MageData)ScriptableObject.CreateInstance(typeof(MageData));
        warriorData = (WarriorData)ScriptableObject.CreateInstance(typeof(WarriorData));
        healerData = (HealerData)ScriptableObject.CreateInstance(typeof(HealerData));
        rougeData = (RougeData)ScriptableObject.CreateInstance(typeof(RougeData));
    }


    //Initialize 2D textures
    void InitTextures()
    {
        headerSectionTexture = new Texture2D(1, 1); //1 pixel high, 1 pixel wide -- then set color of that pixel
        headerSectionTexture.SetPixel(0, 0, headerSectionColor);
        headerSectionTexture.Apply();

        mageSectionTexture = Resources.Load<Texture2D>("icons/t1");
        warriorSectionTexture = Resources.Load<Texture2D>("icons/t2");
        healerSectionTexture = Resources.Load<Texture2D>("icons/t3");
        rougeSectionTexture = Resources.Load<Texture2D>("icons/t4");
    }

    //Similar to update function but called one or more times per interaction
    void OnGUI()
    {
        DrawLayouts();
        DrawHeader();
        DrawMageSettings();
        DrawWarriorSettings();
        DrawHealerSettings();
        DrawRougeSettings();
    }

    //Define rect values and paints textures based on those
    void DrawLayouts()
    {
        //Puts it at top left 
        headerSection.x = 0;
        headerSection.y = 0;
        headerSection.width = Screen.width; //Ensure stays same place even if window resizes
        headerSection.height = 50;

        mageSection.x = 0;
        mageSection.y = 50;
        mageSection.width = Screen.width / 4f; //Dividing by 4 b/c there are 4 characters
        mageSection.height = Screen.width - 50;

        warriorSection.x = Screen.width / 4f;
        warriorSection.y = 50;
        warriorSection.width = Screen.width / 4f; 
        warriorSection.height = Screen.width - 50;

        healerSection.x = (Screen.width / 4f) * 2;
        healerSection.y = 50;
        healerSection.width = Screen.width / 4f; 
        healerSection.height = Screen.width - 50;

        rougeSection.x = (Screen.width / 4f) * 3;
        rougeSection.y = 50;
        rougeSection.width = Screen.width / 4f; 
        rougeSection.height = Screen.width - 50;


        //Draws it over the GUI with the rectangle and texture
        GUI.DrawTexture(headerSection, headerSectionTexture);
        GUI.DrawTexture(mageSection, mageSectionTexture);
        GUI.DrawTexture(warriorSection, warriorSectionTexture);
        GUI.DrawTexture(healerSection, healerSectionTexture);
        GUI.DrawTexture(rougeSection, rougeSectionTexture);
    }

    //Draw contents of header
    void DrawHeader()
    {
        //Every begin call should end with a end call -- think by curly braces
        GUILayout.BeginArea(headerSection); //Takes the exact section so it knows the location

        GUILayout.Label("Enemy Designer");


        GUILayout.EndArea();
    }

    void DrawMageSettings()
    {
        GUILayout.BeginArea(mageSection); 

        GUILayout.Label("Mage");

        EditorGUILayout.BeginHorizontal(); //Creates a horizontal row to put things in --> remember to end it aswell
        GUILayout.Label("Damage");
        mageData.dmgType = (MageDmgType)EditorGUILayout.EnumPopup(mageData.dmgType); //Right hand side draws the enumeration field
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(); 
        GUILayout.Label("Weapon");
        mageData.wpnType = (MageWpnType)EditorGUILayout.EnumPopup(mageData.wpnType); 
        EditorGUILayout.EndHorizontal();


        GUILayout.EndArea();
    }
    void DrawWarriorSettings()
    {
        GUILayout.BeginArea(warriorSection);

        GUILayout.Label("Warrior");

        EditorGUILayout.BeginHorizontal(); //Creates a horizontal row to put things in --> remember to end it aswell
        GUILayout.Label("Class");
        warriorData.classType = (WarriorClassType)EditorGUILayout.EnumPopup(warriorData.classType); //Right hand side draws the enumeration field
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon");
        warriorData.wpnType = (WarriorWpnType)EditorGUILayout.EnumPopup(warriorData.wpnType);
        EditorGUILayout.EndHorizontal();

        GUILayout.EndArea();
    }
    void DrawRougeSettings()
    {
        GUILayout.BeginArea(rougeSection);

        GUILayout.Label("Rouge");

        EditorGUILayout.BeginHorizontal(); //Creates a horizontal row to put things in --> remember to end it aswell
        GUILayout.Label("Strategy");
        rougeData.strType = (RougeStrategyType)EditorGUILayout.EnumPopup(rougeData.strType); //Right hand side draws the enumeration field
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon");
        rougeData.wpnType = (RougeWpnType)EditorGUILayout.EnumPopup(rougeData.wpnType); //Right hand side draws the enumeration field
        EditorGUILayout.EndHorizontal();


        GUILayout.EndArea();
    }

    void DrawHealerSettings()
    {

        GUILayout.BeginArea(healerSection);

        GUILayout.Label("Healer");

        EditorGUILayout.BeginHorizontal(); //Creates a horizontal row to put things in --> remember to end it aswell
        GUILayout.Label("Strategy");
        healerData.strType = (HealerStrategyType)EditorGUILayout.EnumPopup(healerData.strType); //Right hand side draws the enumeration field
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon");
        healerData.wpnType = (HealerWpnType)EditorGUILayout.EnumPopup(healerData.wpnType); //Right hand side draws the enumeration field
        EditorGUILayout.EndHorizontal();

        GUILayout.EndArea();
        
    }
}
