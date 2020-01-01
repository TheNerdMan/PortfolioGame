using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidgidNoiseFilter : INoiseFilter
{
    Noise noise = new Noise();
    NoiseSettings.RidgidNoiseSettings settings;

    public RidgidNoiseFilter(NoiseSettings.RidgidNoiseSettings settings)
    {
        this.settings = settings;
    }

    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = settings.baseRoughness;
        float amplitude = 1;
        float weight = 1;

        for (int i = 0; i < settings.numOfLayers; i++)
        {
            float v = Mathf.Abs(noise.Evaluate(point * frequency + settings.centerOfNoise)) -1;
            v *= v;
            v *= weight;
            weight = Mathf.Clamp01(v * settings.weightMultipier);
            noiseValue = v * amplitude;

            frequency *= settings.roughness;
            amplitude *= settings.persistence;
        }


        noiseValue = Mathf.Max(0, noiseValue - settings.minValue);
        return noiseValue * settings.strength;
    }
}
