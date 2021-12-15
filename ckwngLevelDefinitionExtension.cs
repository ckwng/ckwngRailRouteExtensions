using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ckwng.RailRoute
{
    [Serializable]
    public class ckwngLevelDefinitionExtension
    {
        public ckwngTrainTypeConfig CommuterConfig;

        public ckwngTrainTypeConfig ICConfig;

        public ckwngTrainTypeConfig UrbanConfig;

        public ckwngTrainTypeConfig FreightConfig;
    }
}
