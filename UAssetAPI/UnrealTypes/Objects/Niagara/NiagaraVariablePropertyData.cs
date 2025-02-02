﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UAssetAPI.PropertyTypes;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.PropertyTypes.Structs;
using UAssetAPI.Unversioned;

namespace UAssetAPI.UnrealTypes
{
    /*
        The code within this file is modified from LongerWarrior's UEAssetToolkitGenerator project, which is licensed under the Apache License 2.0.
        Please see the NOTICE.md file distributed with UAssetAPI and UAssetGUI for more information.
    */

    public class NiagaraVariablePropertyData : StructPropertyData 
    {
        [JsonProperty]
        public FName VariableName;
        public int VariableOffset;

        public NiagaraVariablePropertyData(FName name) : base(name)
        {
            Value = new List<PropertyData>();
        }

        public NiagaraVariablePropertyData(FName name, FName forcedType) : base(name)
        {
            StructType = forcedType;
            Value = new List<PropertyData>();
        }

        public NiagaraVariablePropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("NiagaraVariable");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            VariableName = reader.ReadFName();
            List<PropertyData> resultingList = new List<PropertyData>();
            PropertyData data = null;
            var unversionedHeader = new FUnversionedHeader(reader);
            while ((data = MainSerializer.Read(reader, Ancestry, Name, unversionedHeader, true)) != null)
            {
                resultingList.Add(data);
            }

            Value = resultingList;

            VariableOffset = reader.ReadInt32();
        }


        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            int here = (int)writer.BaseStream.Position;
            writer.XFERNAME(VariableName);
            
            if (Value != null) {
                var dat = Value;
                MainSerializer.GenerateUnversionedHeader(ref dat, Name, writer.Asset)?.Write(writer);
                foreach (var t in dat) {
                    MainSerializer.Write(t, writer, true);
                }
            }
            if (!writer.Asset.HasUnversionedProperties) writer.Write(FName.FromString(writer.Asset, "None"));
            writer.Write(VariableOffset);
            return (int)writer.BaseStream.Position - here;
        }

    }

    public class NiagaraVariableWithOffsetPropertyData : NiagaraVariablePropertyData {

        public NiagaraVariableWithOffsetPropertyData(FName name) : base(name)
        {
            Value = new List<PropertyData>();
        }

        public NiagaraVariableWithOffsetPropertyData(FName name, FName forcedType) : base(name)
        {
            StructType = forcedType;
            Value = new List<PropertyData>();
        }

        public NiagaraVariableWithOffsetPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("NiagaraVariableWithOffset");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            base.Read(reader, includeHeader, leng1);
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            return base.Write(writer, includeHeader);
        }
    }
}