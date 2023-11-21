using UnityEngine;

public class MouseController : MonoBehaviour
{
    // String
    public string mouseScriptCursorName = "";
    // Bools
    private bool isActive;
    // Sprites
    [SerializeField] private Texture2D[] cursorTexture;
    [SerializeField] private Texture2D cursor_active;
    //[SerializeField] private Texture2D def_cursor;

    public void SetCursor(string name)
    {
        mouseScriptCursorName = name;
        if (isActive == true && mouseScriptCursorName == "") 
        {
            Cursor.SetCursor(cursor_active, Vector3.zero, CursorMode.ForceSoftware);
        }
        if (isActive == false && mouseScriptCursorName == "")
        {
            Cursor.SetCursor(default, Vector3.zero, CursorMode.ForceSoftware);
        }
        else
        {
            for (int i = 0; i < cursorTexture.Length; i++)
            {
                if (mouseScriptCursorName == cursorTexture[i].name)
                {
                    Cursor.SetCursor(cursorTexture[i], Vector3.zero, CursorMode.ForceSoftware);
                }
            }
        }
    }
    public void NulCursor(string name)
    {
        mouseScriptCursorName = name;
        Cursor.SetCursor(default, Vector3.zero, CursorMode.ForceSoftware);
    }
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(mouseScriptCursorName == "")
            {
                if (hit.transform.tag == "quest" || hit.transform.tag == "Puzzle" || hit.transform.tag == "Item")
                {
                    isActive = true;
                    SetCursor(mouseScriptCursorName);
                }
                if (hit.transform.tag != "quest" && hit.transform.tag != "Puzzle" && hit.transform.tag != "Item")
                {
                    isActive = false;
                    SetCursor(mouseScriptCursorName);
                }
            }
        }
    }
}
