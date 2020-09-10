using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Texture2D cursor;
    public Vector3 cursorOffset = Vector3.zero;

    private void Awake()
    {
        Cursor.SetCursor(cursor, cursorOffset, CursorMode.ForceSoftware);
    }
}
