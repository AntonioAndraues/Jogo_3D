using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
// referencia https://github.com/SebLague/Procedural-Landmass-Generation
// definindo que é um gerador do tipo MapGenerator
[CustomEditor (typeof (MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator mapGen = (MapGenerator)target;

        if (DrawDefaultInspector())
        {
            if (mapGen.autoUpdate)
            {
                mapGen.GenerateMap();
            }
        }

        if (GUILayout.Button ("Gerar novo mapa"))
        {
            mapGen.GenerateMap();
        }
    }
}
