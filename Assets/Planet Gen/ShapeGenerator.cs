using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator
{
    ShapeSettings settings;
    INoiseFilter[] noiseFilters;
    public MinMax elevationMinMax;
     
    public void UpdateSettings(ShapeSettings shapeSettings)
    {
        this.settings = shapeSettings;
        noiseFilters = new INoiseFilter[settings.noiseLayers.Length];
        for (int i = 0; i < noiseFilters.Length; i++)
        {
            noiseFilters[i] = NoiseFilterFactory.CreateNoiseFilter(settings.noiseLayers[i].noiseSettings);
        }
        elevationMinMax = new MinMax();
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnSphere)
    {
        float firstLayerValue = 0;
        float elevation = 0;
        if(noiseFilters.Length > 0)
        {
            firstLayerValue = noiseFilters[0].Evaluate(pointOnSphere);
            if (settings.noiseLayers[0].enabled)
            {
                elevation = firstLayerValue;
            }
        }

        for (int i = 1; i < noiseFilters.Length; i++)
        {
            if (settings.noiseLayers[i].enabled)
            {
                if (settings.noiseLayers[i].useFirstLayerAsMask)
                {
                    elevation += noiseFilters[i].Evaluate(pointOnSphere) * firstLayerValue;
                }
                else if(settings.noiseLayers[i].useFirstLayerAsNegitiveMask)
                {
                    float calculatedElevation = noiseFilters[i].Evaluate(pointOnSphere);
                    elevation -= elevation < settings.planetRadius ? calculatedElevation : 0;
                }
                else
                {
                    elevation += noiseFilters[i].Evaluate(pointOnSphere);
                }
            }
        }

        elevation = settings.planetRadius * (1 + elevation);
        elevationMinMax.AddValue(elevation);
        return pointOnSphere * elevation;
    }
}
