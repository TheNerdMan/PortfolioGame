﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ShapeSettings : ScriptableObject
{
    public float planetRadius = 10;

    public NoiseLayer[] noiseLayers; 

    [System.Serializable]
    public class NoiseLayer
    {
        public bool enabled = true;
        public bool useFirstLayerAsMask;
        public bool useFirstLayerAsNegitiveMask;
        public NoiseSettings noiseSettings;
    }

}

