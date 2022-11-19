// https://frarees.github.io/default-gist-license
//Modified for optional property of extra constructor

using System;
using UnityEngine;

namespace AttributeExt
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class MinMaxSliderAttribute : PropertyAttribute
    {
        public float Min { get; set; }
        public float Max { get; set; }
        public float AmountWidth { get; set; }
        public bool DataFields { get; set; } = true;
        public bool FlexibleFields { get; set; } = true;
        public bool Bound { get; set; } = true;
        public bool Round { get; set; } = true;

        public MinMaxSliderAttribute() : this(0, 1)
        {
        }

        public MinMaxSliderAttribute(float min, float max)
        {
            Min = min;
            Max = max;
            AmountWidth = 18f;
        }
        public MinMaxSliderAttribute(float min, float max, float numberWidthInInspector)
        {
            Min = min;
            Max = max;
            AmountWidth = numberWidthInInspector;
        }
    }
}