using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorScript : MonoBehaviour
{

    [SerializeField] Texture2D cursor;

    void Start()
    {
        Cursor.SetCursor(cursor, Vector3.zero, CursorMode.ForceSoftware);
    }
    //void Update()
    //{
    //    transform.position = Camera.main.ScreenToViewportPoint(Input.mousePosition) + new Vector3(0f, 0f, 1f);
    //}
}
