using UnityEngine;
using System.Collections;

public class MapDisplay : MonoBehaviour {

    public Renderer textureRender;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public GameObject meshObject;

    public void DrawTexture(Texture2D texture) {
        textureRender.sharedMaterial.mainTexture = texture;
        textureRender.transform.localScale = new Vector3 (texture.width, 1, texture.height);
    }

    public void DrawMesh(MeshData meshData, Texture2D texture) {
        var mesh = meshData.CreateMesh ();
        meshFilter.sharedMesh = mesh;
        meshRenderer.sharedMaterial.mainTexture = texture;
        DestroyImmediate(meshObject.GetComponent<MeshCollider>());
        var collider = meshObject.AddComponent<MeshCollider>();
        collider.sharedMesh = mesh;
        
    }

}