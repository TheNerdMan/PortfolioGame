using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings
{
    public enum FilterType
    {
        Simple,
        Ridgid
    }

    public FilterType filterType;
#if (UNITY_EDITOR)
    [ConditionalHide("filterType", 0)]
#endif
    public SimpleNoiseSettings simpleNoiseSettings;
#if (UNITY_EDITOR)
    [ConditionalHide("filterType", 1)]
#endif
    public RidgidNoiseSettings ridgidNoiseSettings;

    [System.Serializable]
    public class SimpleNoiseSettings
    {
        public float strength = 1;
        [Range(1, 8)]
        public int numOfLayers = 1;
        public float roughness = 2;
        public float baseRoughness = 1;
        public float persistence = .5f;
        public float minValue;
        public Vector3 centerOfNoise;
    }

    [System.Serializable]
    public class RidgidNoiseSettings : SimpleNoiseSettings
    {
        public float weightMultipier = .8f;
    }
}
