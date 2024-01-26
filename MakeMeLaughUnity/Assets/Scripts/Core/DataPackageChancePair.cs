using System;
using Utils;

namespace Core
{
    [Serializable]
    public class DataPackageChancePair : Pair<DataPackageController, float>
    {
        public DataPackageChancePair(DataPackageController one, float two) : base(one, two)
        { }
    }
}