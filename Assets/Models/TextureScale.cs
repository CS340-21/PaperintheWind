using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TextureScale : MonoBehaviour
{

    public float scaleFactor = 5.0f;

    /// <summary>
    /// Scale only this object's material to the given factor
    /// </summary>
    private void ScaleMaterial()
    {
        Renderer renderer = GetComponent<Renderer>();

        Material tempMaterial = new Material(renderer.sharedMaterial);
        tempMaterial.mainTextureScale = new Vector2(transform.localScale.x / scaleFactor, transform.localScale.z / scaleFactor);
        renderer.sharedMaterial = tempMaterial;
    }

    private void Start()
    {
        ScaleMaterial();
    }

    private void Update()
    {

        if (transform.hasChanged && Application.isEditor && !Application.isPlaying)
        {
            ScaleMaterial();
        }

    }
}