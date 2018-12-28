using gx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeichleOCR.Modal
{
    public class vmOCR
    {
        public DateTime Date { get; set; }
        public string Image1 { get; set; }
        public string ImagePath { get; set; }
        public string Time { get; set; }
        public string Lane { get; set; }
        public string VideoPath { get; set; }
        public string StateCode { get; set; }
        public string PlateNo { get; set; }
        public string CountryCode { get; set; }
        public string BackgroundColor { get; set; }
        public string PlateColor { get; set; }
        public gxPG4 Frame { get; set; }

        public string FrameStr { get; set; }

        public byte[] PlateImage { get; set; }
        public string message { get; set; }
        public string type { get; set; }

        public string VeichleType { get; set; }

        public double Width { get; set; }
        public double Height { get; set; }
        public string Status { get; set; }
        public string PlateSize { get; set; }

        public string Category { get; set; }

        public List<string> snapList { get; set; }

    }
}
