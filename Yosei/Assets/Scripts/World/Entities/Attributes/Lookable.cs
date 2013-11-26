using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class Lookable : MonoBehaviour
{
    public string m_appearance { get; private set; }
    public Color m_base_color { get; private set; }
    private MeshFilter m_mesh_filter;

    private bool m_hl;

    public void Awake()
    {
        m_mesh_filter = GetComponent<MeshFilter>();
    }

    public void SetAppearance(string p_appearance, string p_material, Texture2D p_texture, string p_mesh, Color p_color)
    {
        m_appearance = p_appearance;

        renderer.material = Resources.Load(p_material) as Material;
        renderer.material.mainTexture = p_texture;
        renderer.material.color = p_color;
        m_base_color = p_color;

        // TODO: Performance caveat here
        GameObject go = (Instantiate((Resources.Load(p_mesh))) as GameObject);
        m_mesh_filter.mesh = go.GetComponent<MeshFilter>().mesh;
        GameObject.Destroy(go);
    }

    public void SetAppearance(string p_appearance, string p_material, string p_texture, string p_mesh, Color p_color)
    {
        SetAppearance(p_appearance, p_material, Resources.Load(p_texture) as Texture2D, p_mesh, p_color);
    }

    public void SetHighlight(bool p_hl)
    {
        if (m_hl != p_hl)
        {
            m_hl = p_hl;
            renderer.material.color = m_hl ? Game.Inst.m_colors.HighlightColor(m_base_color) : m_base_color;
        }
    }
}
