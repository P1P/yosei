using UnityEngine;
using System.Collections;

public class Commander : MonoBehaviour {
    private Highlighter _highlighter;

	public void Awake()
    {
        _highlighter = GetComponent<Highlighter>();
	}
	
	public void Update()
    {
        // Orders the Yosei to go to a clicked tile
        if ((_highlighter.Hl_tile != null) && (_highlighter.New_tile))
        {
            foreach (Yosei yosei in ReferenceHelper.Instance.Object_population.GetComponentsInChildren<Yosei>())
            {
                yosei.Pathfinder.GoTo(_highlighter.Hl_tile.transform.position);
            }
        }

        // Reset on keypress
        if (Input.GetKey(KeyCode.Backspace))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
	}
}
