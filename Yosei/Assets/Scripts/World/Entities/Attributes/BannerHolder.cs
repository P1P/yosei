using UnityEngine;
using System.Collections;

public class BannerHolder : MonoBehaviour
{
	private GameObject m_banner;

	private UISlicedSprite m_core_background;
	private UISlicedSprite m_title_background;
	private UILabel m_core_text;
	private UILabel m_title_text;

	[SerializeField]
	private float _banner_height = 1f;
	public float m_banner_height
	{
		get { return _banner_height; }
		set { _banner_height = value; UpdateHeight(); }
	}

	[SerializeField]
	private float _banner_scale = 15f;
	public float m_banner_scale
	{
		get { return _banner_scale; }
		set { _banner_scale = value; UpdateScale(); }
	}

	private bool m_follow_camera = true;
    private Color m_base_text_core_color = Color.white;

	public void Awake()
	{
		m_banner = GameObject.Instantiate(Resources.Load("GUI/3D_Banner")) as GameObject;

		foreach (UISlicedSprite bg in m_banner.GetComponentsInChildren<UISlicedSprite>())
		{
			switch (bg.name)
			{
				case "Core_Background": m_core_background = bg; break;
				case "Title_Background": m_title_background = bg; break;
			}
		}

		foreach (UILabel text in m_banner.GetComponentsInChildren<UILabel>())
		{
			switch (text.name)
			{
				case "Core_Text": m_core_text = text; break;
				case "Title_Text": m_title_text = text; break;
			}
		}

		m_banner.transform.parent = transform;

		UpdateHeight();
		UpdateScale();
	}

	public void Update()
	{
		if (m_follow_camera)
		{
			m_banner.transform.rotation = Game.Inst.m_object_observer.transform.rotation;
		}
	}
	
	private void UpdateHeight()
	{
		if (m_banner != null)
		{
			m_banner.transform.position = transform.position + Vector3.up * m_banner_height;
		}
	}

	private void UpdateScale()
	{
		if (m_banner != null)
		{
            m_banner.transform.localScale = Vector3.one * m_banner_scale;
		}
	}

    public void ClearCoreText()
    {
        m_core_text.text = "";
    }

	public void AddCoreTextLine(string p_text, Color p_color)
	{
		m_core_text.text += "[" + ColorFactory.ToHexa(p_color) + "]" + p_text + "[-]" + "\n";
	}

    public void AddCoreTextLine(string p_test)
    {
        AddCoreTextLine(p_test, m_base_text_core_color);
    }

    public void AddCoreTextLine()
    {
        m_core_text.text += "\n";
    }

    public void AddCoreText(string p_text, Color p_color)
    {
        m_core_text.text += "[" + ColorFactory.ToHexa(p_color) + "]" + p_text + "[-]";
    }

    public void AddCoreText(string p_text)
    {
        AddCoreText(p_text, m_base_text_core_color);
    }

	public void SetTitleText(string p_text)
	{
		m_title_text.text = p_text;
	}

    public void SetCoreColor(Color p_color)
    {
        m_core_background.color = p_color;
    }

    public void SetTitleColor(Color p_color)
    {
        m_title_background.color = p_color;
    }
}
