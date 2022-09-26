using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; //Any script with this statement will not be included in build version

public class EnemyDesignerWindow : EditorWindow
{
    [MenuItem("Window/Enemy Designer")] //Menu item and then drop down is where to open
    static void OpenWindow()
    {
        EnemyDesignerWindow window = (EnemyDesignerWindow)GetWindow(typeof(EnemyDesignerWindow));
        window.minSize = new Vector2(600, 300);
        window.Show();
    }


}
