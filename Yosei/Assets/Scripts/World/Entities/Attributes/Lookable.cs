using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class Lookable : MonoBehaviour
{
    public string Description { get; private set; }
    public Color Base_color { get; private set; }

    private MeshFilter _mesh_filter;
    private bool _hl;

    public void Awake()
    {
        _mesh_filter = GetComponent<MeshFilter>();
    }

    public void SetAppearance(string p_description, string p_material, Texture2D p_texture, string p_mesh, Color p_color)
    {
        Description = p_description;
        
        renderer.material = Resources.Load(p_material) as Material;
        renderer.material.mainTexture = p_texture;
        renderer.material.color = p_color;
        Base_color = p_color;
        
        _mesh_filter.mesh = (Resources.Load(p_mesh) as GameObject).GetComponent<MeshHolder>().Mesh;
    }

    public void SetAppearance(string p_appearance, string p_material, string p_texture, string p_mesh, Color p_color)
    {
        SetAppearance(p_appearance, p_material, Resources.Load(p_texture) as Texture2D, p_mesh, p_color);
    }

    public void OnDestroy()
    {
        GameObject.Destroy(renderer.material.mainTexture);
        GameObject.Destroy(renderer.material);
    }

    public void SetHighlight(bool p_hl)
    {
        if (_hl != p_hl)
        {
            _hl = p_hl;
            renderer.material.color = _hl ? ColorFactory.Instance.HighlightColor(Base_color) : Base_color;
        }
    }
}
