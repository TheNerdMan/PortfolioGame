using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Range(2,256)]
    public int resolution = 10;
    public bool autoUpdate = true;
    public enum FaceRenderMask
    {
        All,
        Top,
        Bottom,
        Left,
        Right,
        Front,
        Back
    };
    public FaceRenderMask faceRenderMask;

    public ColourSettings colourSettings;
    public ShapeSettings shapeSettings;

    [HideInInspector]
    public bool colourSettingsFoldout;
    [HideInInspector]
    public bool shapeSettingsFoldout;

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    TextureFace[] terrainFaces;

    ShapeGenerator shapeGenerator = new ShapeGenerator();
    ColourGenerator colourGenerator = new ColourGenerator();
    
    void Initialize()
    {
        shapeGenerator.UpdateSettings(shapeSettings);
        colourGenerator.UpdateSettings(colourSettings);

        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }
        terrainFaces = new TextureFace[6];

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObject = new GameObject("mesh");
                meshObject.transform.parent = transform;

                meshObject.AddComponent<MeshRenderer>();
                meshFilters[i] = meshObject.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }
            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colourSettings.planetMaterial;
            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial.shader = colourSettings.planetShader;

            terrainFaces[i] = new TextureFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
            bool renderFace = faceRenderMask == FaceRenderMask.All || (int)faceRenderMask - 1 == i;
            meshFilters[i].gameObject.SetActive(renderFace);
        }
        
    }

    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColours();
    }

    public void OnShapeSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateMesh();
        }
    }

    public void OnColourSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateColours();
        }
    }

    void GenerateMesh()
    {
        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i].gameObject.activeSelf)
            {
                terrainFaces[i].ConstructMesh();
            }
        }

        colourGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
    }

    void GenerateColours()
    {
        colourGenerator.UpdateColours();
    }
}
