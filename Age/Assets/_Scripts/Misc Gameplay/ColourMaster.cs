using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourMaster {

    public void ChangeColours(Material[] materials, Color[] targetColours)
    {
        for(int index = 0; index < materials.Length; index++)
        {
            materials[index].color = targetColours[index];
        }
    }

    public void ChangeColours(Material[] materials, Color targetColour)
    {
        for(int index = 0; index < materials.Length; index++)
        {
            materials[index].color = targetColour;
        }
    }

    public void ChangeColours(Renderer[] renderers, Color[] targetColours)
    {
        for(int index = 0; index < renderers.Length; index++)
        {
            renderers[index].material.color = targetColours[index];
        }
    }

    public void ChangeColours(Renderer[] renderers, Color targetColour)
    {
        for(int index = 0; index < renderers.Length; index++)
        {
            renderers[index].material.color = targetColour;
        }
    }

    public Color[] GetColours(Renderer[] renderers)
    {
        List<Color> colours = new List<Color>();

        foreach(Renderer rend in renderers)
        {
            if(rend.material.color != null)
            {
                colours.Add(rend.material.color);
            }

            ParticleSystem partSys = rend.GetComponent<ParticleSystem>();
            if(partSys != null)
            {
                colours.Add(partSys.startColor);
            }
        }

        return colours.ToArray();
    }

    public Color[] GetColours(Material[] materials)
    {
        List<Color> colours = new List<Color>();

        foreach(Material mat in materials)
        {
            colours.Add(mat.color);
        }

        return colours.ToArray();
    }

    public Color ChangeAlpha(Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }
}
