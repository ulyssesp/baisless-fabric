﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] Ground Ground;
    [SerializeField] public List<SpawnTrigger> SpawnTriggers;
    public GameObject Pool;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Ground.GameSpeed.RuntimeValue == 0) {
            // No point in positioning things if we don't have locations for them
            return;
        }

        List<SpawnTrigger> removes = new List<SpawnTrigger>();
        foreach(SpawnTrigger st in SpawnTriggers) {
            Optional<SpawnInfo> spawn = st.Spawn();
            if(!spawn.isNothing) {
                SpawnInfo si = spawn.value;
                GameObject instantiated = 
                    Instantiate(
                        si.Prefab, 
                        new Vector3(si.xOffset, si.yOffset, 4f), 
                        Quaternion.identity
                    );
                if(st.oneshot) {
                    removes.Add(st);
                }
            }
        }

        foreach(SpawnTrigger rem in removes) {
            SpawnTriggers.Remove(rem);
        }
    }
}
