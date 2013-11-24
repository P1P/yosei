using UnityEngine;
using System.Collections;

public class Yosei : Entity
{
    void Start()
    {
        m_lookable.SetAppearance(
            "Yosei",
            "Tiles/Material/Material",
            Game.Inst.m_textures.GetRandomGreyscaleTexture(1, 1, 0.9f, 0.1f),
            "Tiles/Mesh/Cube",
            Game.Inst.m_colors.GetRandomColor());
    }
}
