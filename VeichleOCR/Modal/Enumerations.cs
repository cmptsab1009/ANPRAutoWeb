using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeichleOCR.Modal
{
    public class Enumerations
    {
        public enum PlateType
        {
            UnAssinged,
            Assinged
        }

        public enum OCRStatus
        {
            All,
            Success,
            UnRecognized,
            NoPlate
        }

        public enum Category
        {
            All,
            Private,
            Taxi,
            Transport,
            Police,
            Protocol
        }

        public enum BackgroundColor
        {
            All,
            White = 16777215,
            Black = 0,
            Blue = 16711680,
            Cyan = 16776960,
            Green = 65280,
            Orange = 33023,
            Yellow = 65535,
            Red = 255
        };

        public enum PlateSize
        {
            All,
            Long,
            Short
        }

        
    }
}
