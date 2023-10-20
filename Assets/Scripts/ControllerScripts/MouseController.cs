using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    // String
    public string mouseScriptCursorName;
    // Sprites
    [SerializeField] private Texture2D[] cursor;

    public void SetCursor(string name)
    {
        mouseScriptCursorName = name;
        for (int i = 0; i < cursor.Length; i ++)
        {
            if (mouseScriptCursorName == cursor[i].name)
            {
                Cursor.SetCursor(cursor[i], Vector3.zero, CursorMode.ForceSoftware);
            }
        }
    }
}
