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

    GUISkin skin;


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
        skin = Resources.Load<GUISkin>("guiStyles/EnemyDesignerSkin"); //must be in resources folder 
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

        GUILayout.Label("Enemy Designer",skin.GetStyle("Header1"));


        GUILayout.EndArea();
    }

    void DrawMageSettings()
    {
        GUILayout.BeginArea(mageSection); 

        GUILayout.Label("Mage", skin.GetStyle("Header2"));

        EditorGUILayout.BeginHorizontal(); //Creates a horizontal row to put things in --> remember to end it aswell
        GUILayout.Label("Damage", skin.GetStyle("Field"));
        mageData.dmgType = (MageDmgType)EditorGUILayout.EnumPopup(mageData.dmgType); //Right hand side draws the enumeration field
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(); 
        GUILayout.Label("Weapon", skin.GetStyle("Field"));
        mageData.wpnType = (MageWpnType)EditorGUILayout.EnumPopup(mageData.wpnType); 
        EditorGUILayout.EndHorizontal();

        //Guilayout.button will return t/f if btn clicked
        if(GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.MAGE);
        }

        GUILayout.EndArea();
    }
    void DrawWarriorSettings()
    {
        GUILayout.BeginArea(warriorSection);

        GUILayout.Label("Warrior", skin.GetStyle("Header2"));

        EditorGUILayout.BeginHorizontal(); //Creates a horizontal row to put things in --> remember to end it aswell
        GUILayout.Label("Class", skin.GetStyle("Field"));
        warriorData.classType = (WarriorClassType)EditorGUILayout.EnumPopup(warriorData.classType); //Right hand side draws the enumeration field
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon", skin.GetStyle("Field"));
        warriorData.wpnType = (WarriorWpnType)EditorGUILayout.EnumPopup(warriorData.wpnType);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.WARRIOR);
        }

        GUILayout.EndArea();
    }
    void DrawRougeSettings()
    {
        GUILayout.BeginArea(rougeSection);

        GUILayout.Label("Rouge", skin.GetStyle("Header2"));

        EditorGUILayout.BeginHorizontal(); //Creates a horizontal row to put things in --> remember to end it aswell
        GUILayout.Label("Strategy", skin.GetStyle("Field"));
        rougeData.strType = (RougeStrategyType)EditorGUILayout.EnumPopup(rougeData.strType); //Right hand side draws the enumeration field
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon", skin.GetStyle("Field"));
        rougeData.wpnType = (RougeWpnType)EditorGUILayout.EnumPopup(rougeData.wpnType); //Right hand side draws the enumeration field
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.ROUGE);
        }

        GUILayout.EndArea();
    }

    void DrawHealerSettings()
    {

        GUILayout.BeginArea(healerSection);

        GUILayout.Label("Healer", skin.GetStyle("Header2"));

        EditorGUILayout.BeginHorizontal(); //Creates a horizontal row to put things in --> remember to end it aswell
        GUILayout.Label("Strategy", skin.GetStyle("Field"));
        healerData.strType = (HealerStrategyType)EditorGUILayout.EnumPopup(healerData.strType); //Right hand side draws the enumeration field
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon", skin.GetStyle("Field"));
        healerData.wpnType = (HealerWpnType)EditorGUILayout.EnumPopup(healerData.wpnType); //Right hand side draws the enumeration field
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.HEALER);
        }

        GUILayout.EndArea();
        
    }
}

public class GeneralSettings : EditorWindow
{
    public enum SettingsType
    {
        MAGE,
        WARRIOR,
        HEALER,
        ROUGE
    }

    static SettingsType dataSetting;
    static GeneralSettings window;

    public static void OpenWindow(SettingsType setting)
    {
        dataSetting = setting;
        window = (GeneralSettings)GetWindow(typeof(GeneralSettings));
        window.minSize = new Vector2(250, 200);
        window.Show();

    }

    private void OnGUI()
    {
        switch (dataSetting)
        {
            case SettingsType.MAGE:
                DrawSettings((CharacterData)EnemyDesignerWindow.MageInfo); //Passing only the characterData portion of the class since class specific was handled in other window
                break;
            case SettingsType.WARRIOR:
                DrawSettings((CharacterData)EnemyDesignerWindow.WarriorInfo);
                break;
            case SettingsType.ROUGE:
                DrawSettings((CharacterData)EnemyDesignerWindow.RougeInfo);
                break;
            case SettingsType.HEALER:
                DrawSettings((CharacterData)EnemyDesignerWindow.HealerInfo);
                break;
        }
    }

    void DrawSettings(CharacterData charData)
    {

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Prefab");
        charData.prefab = (GameObject)EditorGUILayout.ObjectField(charData.prefab, typeof(GameObject), false); //false here is to ensure that only a prefab can be dragged in.. doesn't allow scene objects
        //for the above     ^^^^^^^ need to also cast it here based on the typeof object
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Max Health");
        charData.maxHealth = EditorGUILayout.FloatField(charData.maxHealth);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Max Energy");
        charData.maxEnergy = EditorGUILayout.FloatField(charData.maxEnergy);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Power");
        charData.power = EditorGUILayout.Slider(charData.power, 0, 100); // tales 3 parameters for sliders, parameter, min and max
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("% Crit Chance");
        charData.critChance = EditorGUILayout.Slider(charData.critChance, 0, charData.power); //here max value depends on power
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name");
        charData.name = EditorGUILayout.TextField(charData.name);
        EditorGUILayout.EndHorizontal();

        if(charData.prefab == null)
        {
            EditorGUILayout.HelpBox("This enemy needs a [Prefab] before it can be created.", MessageType.Warning); //Msg types -- info, warning, error
        }
        else if(charData.name == null || charData.name.Length < 1)
        {
            EditorGUILayout.HelpBox("This enemy needs a [Name] before it can be created.", MessageType.Warning); 

        }
        else if (GUILayout.Button("Finish & Save", GUILayout.Height(30))) //Button will only show if the other if statements aren't hit
        {
            SaveCharacterData();
            window.Close();
        }
    }

    void SaveCharacterData()
    {
        string prefabPath; //path of base prefab
        string newPrefabPath = "Assets/prefabs/characters/";
        string dataPath = "Assets/prefabs/resources/characterData/data/";

        switch(dataSetting)
        {

            case SettingsType.MAGE:
                //create .asset file
                dataPath += "mage/" + EnemyDesignerWindow.MageInfo.name + ".asset"; //lets unity know its .asset
                AssetDatabase.CreateAsset(EnemyDesignerWindow.MageInfo, dataPath); //creates the actual asset file
                newPrefabPath += "mage/" + EnemyDesignerWindow.MageInfo.name + ".prefab"; //sets path for the actual prefab creation
                prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.MageInfo.prefab); //going to give us string value of the previous dragged in prefab
                AssetDatabase.CopyAsset(prefabPath, newPrefabPath);

                //Now need to save and refresh to actually edit the new prefab
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject magePrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
                if (!magePrefab.GetComponent<Mage>())
                    magePrefab.AddComponent(typeof(Mage));
                magePrefab.GetComponent<Mage>().mageData = EnemyDesignerWindow.MageInfo; //"Mage" script has the link to the mageData 


                break;

            case SettingsType.WARRIOR:

                dataPath += "warrior/" + EnemyDesignerWindow.WarriorInfo.name + ".asset";
                AssetDatabase.CreateAsset(EnemyDesignerWindow.WarriorInfo, dataPath);
                newPrefabPath += "warrior/" + EnemyDesignerWindow.WarriorInfo.name + ".prefab";
                prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.MageInfo.prefab);
                AssetDatabase.CopyAsset(prefabPath, newPrefabPath);

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject warriorPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
                if (!warriorPrefab.GetComponent<Warrior>())
                    warriorPrefab.AddComponent(typeof(Warrior));
                warriorPrefab.GetComponent<Warrior>().warriorData = EnemyDesignerWindow.WarriorInfo;


                break;

            case SettingsType.HEALER:

                dataPath += "healer/" + EnemyDesignerWindow.HealerInfo.name + ".asset";
                AssetDatabase.CreateAsset(EnemyDesignerWindow.HealerInfo, dataPath);
                newPrefabPath += "healer/" + EnemyDesignerWindow.HealerInfo.name + ".prefab";
                prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.MageInfo.prefab);
                AssetDatabase.CopyAsset(prefabPath, newPrefabPath);

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject healerPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
                if (!healerPrefab.GetComponent<Healer>())
                    healerPrefab.AddComponent(typeof(Healer));
                healerPrefab.GetComponent<Healer>().healerData = EnemyDesignerWindow.HealerInfo;


                break;

            case SettingsType.ROUGE:


                dataPath += "rouge/" + EnemyDesignerWindow.RougeInfo.name + ".asset";
                AssetDatabase.CreateAsset(EnemyDesignerWindow.RougeInfo, dataPath);
                newPrefabPath += "rouge/" + EnemyDesignerWindow.RougeInfo.name + ".prefab";
                prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.MageInfo.prefab);
                AssetDatabase.CopyAsset(prefabPath, newPrefabPath);

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject rougePrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
                if (!rougePrefab.GetComponent<Rouge>())
                    rougePrefab.AddComponent(typeof(Rouge));
                rougePrefab.GetComponent<Rouge>().rougeData = EnemyDesignerWindow.RougeInfo;

                break;


        }




    }


}