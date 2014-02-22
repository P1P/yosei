using UnityEngine;
using System.Collections;

public class BannerHolder : MonoBehaviour
{
    [SerializeField]
    private float _banner_height = 1f;
    public float Banner_height
    {
        get { return _banner_height; }
        set { _banner_height = value; UpdateHeight(); }
    }

    [SerializeField]
    private float _banner_scale = 12.5f;
    public float Banner_scale
    {
        get { return _banner_scale; }
        set { _banner_scale = value; UpdateScale(); }
    }


    [SerializeField]
    private bool _enabled = true;
    public bool BannerEnabled
    {
        get { return _enabled; }
        set { _enabled = value; UpdateScale(); }
    }

    private bool _follow_camera = true;
    private Color _base_text_core_color = Color.white;

	private GameObject _banner;
	private UISlicedSprite _core_background;
	private UISlicedSprite _title_background;
	private UILabel _core_text;
	private UILabel _title_text;

	public void Awake()
	{
		_banner = GameObject.Instantiate(Resources.Load("GUI/3D_Banner")) as GameObject;

		foreach (UISlicedSprite bg in _banner.GetComponentsInChildren<UISlicedSprite>())
		{
			switch (bg.name)
			{
				case "Core_Background": _core_background = bg; break;
				case "Title_Background": _title_background = bg; break;
			}
		}

		foreach (UILabel text in _banner.GetComponentsInChildren<UILabel>())
		{
			switch (text.name)
			{
				case "Core_Text": _core_text = text; break;
				case "Title_Text": _title_text = text; break;
			}
		}

		_banner.transform.parent = transform;

		UpdateHeight();
		UpdateScale();
	}

	public void Update()
	{
		if (_follow_camera)
		{
			_banner.transform.rotation = ReferenceHelper.Instance.Object_observer.transform.rotation;
		}
	}
	
	private void UpdateHeight()
	{
		if (_banner != null)
		{
			_banner.transform.position = transform.position + Vector3.up * Banner_height;
		}
	}

	private void UpdateScale()
	{
		if (_banner != null)
		{
            if (BannerEnabled)
            {
                _banner.transform.localScale = Vector3.one * Banner_scale;
            }
            else
            {
                _banner.transform.localScale = Vector3.zero;
            }
		}
	}

    public void ClearCoreText()
    {
        _core_text.text = "";
    }

	public void AddCoreTextLine(string p_text, Color p_color)
	{
		_core_text.text += "[" + ColorFactory.ToHexa(p_color) + "]" + p_text + "[-]" + "\n";
	}

    public void AddCoreTextLine(string p_test)
    {
        AddCoreTextLine(p_test, _base_text_core_color);
    }

    public void AddCoreTextLine()
    {
        _core_text.text += "\n";
    }

    public void AddCoreText(string p_text, Color p_color)
    {
        _core_text.text += "[" + ColorFactory.ToHexa(p_color) + "]" + p_text + "[-]";
    }

    public void AddCoreText(string p_text)
    {
        AddCoreText(p_text, _base_text_core_color);
    }

	public void SetTitleText(string p_text)
	{
		_title_text.text = p_text;
	}

    public void SetCoreColor(Color p_color)
    {
        _core_background.color = p_color;
    }

    public void SetTitleColor(Color p_color)
    {
        _title_background.color = p_color;
    }
}
