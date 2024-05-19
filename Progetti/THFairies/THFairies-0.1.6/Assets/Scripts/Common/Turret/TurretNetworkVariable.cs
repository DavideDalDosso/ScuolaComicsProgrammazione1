using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
public class TurretDataNetworkVariable : NetworkVariableBase
{
    public int[] upgrades = new int[0];
    public override void ReadDelta(FastBufferReader reader, bool keepDirtyDelta)
    {

    }

    public override void ReadField(FastBufferReader reader)
    {
        var itemCount= (int)0;
        reader.ReadValueSafe(out itemCount);
        upgrades = new int[itemCount];
        for (int i = 0; i < itemCount; i++){
            reader.ReadValueSafe(out upgrades[i]);
        }
    }

    public override void WriteDelta(FastBufferWriter writer)
    {
        
    }

    public override void WriteField(FastBufferWriter writer)
    {
        writer.WriteValueSafe(upgrades.Length);
        foreach (var upgradeEntry in upgrades){
            writer.WriteValueSafe(upgradeEntry);
        }
    }
}