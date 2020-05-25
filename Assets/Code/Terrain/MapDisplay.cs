using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// referencia https://github.com/SebLague/Procedural-Landmass-Generation
public class MapDisplay : MonoBehaviour
{
    // Gera uma textura para aplicar em um plano

    // referencia ao plano
    public Renderer textureRender;

    public void DrawTexture(Texture2D texture) {
  
        // Parte que nos permite visualizar as textura sem ser em runtime
        textureRender.sharedMaterial.mainTexture = texture;
        textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height);

    }

}
