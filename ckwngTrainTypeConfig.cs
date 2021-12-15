using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ckwng.RailRoute
{
    [Serializable]
    public class ckwngTrainTypeConfig
    {
        public string Name;
        public int PassengerCars = -1;
        public int CargoCars = -1;
        public int Bidirectional = -1;

        public static ckwngTrainTypeConfig Decode(string name, string text)
        {
            var result = new ckwngTrainTypeConfig
            {
                Name = name,
            };
            foreach (var setting in text.Split(';'))
            {
                switch (setting[0])
                {
                    case 'P':
                        result.PassengerCars = int.Parse(setting.Substring(1));
                        break;
                    case 'C':
                        result.CargoCars = int.Parse(setting.Substring(1));
                        break;
                    case 'B':
                        result.Bidirectional = int.Parse(setting.Substring(1));
                        break;
                }
            }
            return result;
        }

        public static ckwngTrainTypeConfig Decode(string text)
        {
            return Decode(null, text);
        }
    }
}
