﻿using Newtonsoft.Json;
using UAssetAPI.JSON;

namespace UAssetAPI.UnrealTypes
{
    /// <summary>
    /// Floating point quaternion that can represent a rotation about an axis in 3-D space.
    /// The X, Y, Z, W components also double as the Axis/Angle format.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class FQuat
    {
        private float? _x1;
        private double _x2;
        private float? _y1;
        private double _y2;
        private float? _z1;
        private double _z2;
        private float? _w1;
        private double _w2;

        /// <summary>The quaternion's X-component.</summary>
        [JsonProperty]
        [JsonConverter(typeof(FSignedZeroJsonConverter))]
        public double X
        {
            get
            {
                return _x1 == null ? _x2 : (double)_x1;
            }
            set
            {
                _x1 = null;
                _x2 = value;
            }
        }

        [JsonIgnore]
        public float XFloat => _x1 == null ? (float)_x2 : (float)_x1;

        /// <summary>The quaternion's Y-component.</summary>
        [JsonProperty]
        [JsonConverter(typeof(FSignedZeroJsonConverter))]
        public double Y
        {
            get
            {
                return _y1 == null ? _y2 : (double)_y1;
            }
            set
            {
                _y1 = null;
                _y2 = value;
            }
        }

        [JsonIgnore]
        public float YFloat => _y1 == null ? (float)_y2 : (float)_y1;

        /// <summary>The quaternion's Z-component.</summary>
        [JsonProperty]
        [JsonConverter(typeof(FSignedZeroJsonConverter))]
        public double Z
        {
            get
            {
                return _z1 == null ? _z2 : (double)_z1;
            }
            set
            {
                _z1 = null;
                _z2 = value;
            }
        }

        [JsonIgnore]
        public float ZFloat => _z1 == null ? (float)_z2 : (float)_z1;

        /// <summary>The quaternion's W-component.</summary>
        [JsonProperty]
        [JsonConverter(typeof(FSignedZeroJsonConverter))]
        public double W
        {
            get
            {
                return _w1 == null ? _w2 : (double)_w1;
            }
            set
            {
                _w1 = null;
                _w2 = value;
            }
        }

        [JsonIgnore]
        public float WFloat => _w1 == null ? (float)_w2 : (float)_w1;

        public FQuat(double x, double y, double z, double w)
        {
            _x2 = x;
            _y2 = y;
            _z2 = z;
            _w2 = w;
        }

        public FQuat(float x, float y, float z, float w)
        {
            _x1 = x;
            _y1 = y;
            _z1 = z;
            _w1 = w;
        }

        public FQuat()
        {

        }
    }
}
