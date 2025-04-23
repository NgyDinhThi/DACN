using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(WayPoint))]
public class WaypointEditor : Editor
{
    private WayPoint muctieudd => target as WayPoint;



    private void OnSceneGUI()
    {
        if (muctieudd.Diadiem.Length <= 0f)
            return;

        Handles.color = Color.white;
        for (int i = 0; i < muctieudd.Diadiem.Length; i++)
        {
            EditorGUI.BeginChangeCheck();

            Vector3 currentPoint = muctieudd.entitypPosition + muctieudd.Diadiem[i];

            Vector3 newPosition = Handles.FreeMoveHandle(currentPoint, 0.5f, Vector3.one * 0.5f, Handles.SphereHandleCap);

            GUIStyle text = new GUIStyle();
            text.fontStyle = FontStyle.Bold;
            text.fontSize = 16;
            text.normal.textColor = Color.black;
            Vector3 textPos = new Vector3(0.2f, -0.2f);
            Handles.Label(muctieudd.entitypPosition + muctieudd.Diadiem [i] + textPos, $"{i +1}", text);


            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Free move");
                muctieudd.Diadiem[i] = newPosition - muctieudd.entitypPosition;
            }
        }
    }
}