using System.Collections.Generic;
using UnityEngine;

namespace NecatiAkpinar.Data
{
    [CreateAssetMenu(fileName = "LevelContainer", menuName = "Data/Containers/LevelContainer", order = 1)]
    public class ContainerLevelData : ScriptableObject
    {
        [SerializeField] private List<LevelData> _levels;

        public List<LevelData> Levels => _levels;
    }
}