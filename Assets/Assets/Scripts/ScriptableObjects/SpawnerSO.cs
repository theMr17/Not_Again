using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SpawnerSO : ScriptableObject {
    public List<SpawnDetails> roadSpawnerListSO;
    public List<SpawnDetails> waterSpawnerListSO;
}
