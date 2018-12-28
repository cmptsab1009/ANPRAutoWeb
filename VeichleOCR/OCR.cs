using cm;
using gx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VeichleOCR.Modal;
using static VeichleOCR.Modal.Enumerations;

namespace VeichleOCR
{
    public class OCR
    {
        

        public static string GetVehicleTypeFromColour(int code)
        {
            switch (code)
            {
                case (int)BackgroundColor.White:
                    return BackgroundColor.White.ToString();

                case (int)BackgroundColor.Black:
                    return BackgroundColor.Black.ToString();

                case (int)BackgroundColor.Blue:
                    return BackgroundColor.Blue.ToString();

                case (int)BackgroundColor.Cyan:
                    return BackgroundColor.Cyan.ToString();

                case (int)BackgroundColor.Green:
                    return BackgroundColor.Green.ToString();

                case (int)BackgroundColor.Orange:
                    return BackgroundColor.Orange.ToString();

                case (int)BackgroundColor.Yellow:
                    return BackgroundColor.Yellow.ToString();

                case (int)BackgroundColor.Red:
                    return BackgroundColor.Red.ToString();

                default:
                    return "Color Not Found";
            }
        }

        public static string GetCategory(string countryCode, string PlateColor, string BackGrdColor)
        {
            if (countryCode == "Dubai")
            {
                if (PlateColor == "White" && BackGrdColor == "White")
                    return Category.Private.ToString();
                else if (PlateColor == "Yellow" && BackGrdColor == "Yellow")
                    return Category.Taxi.ToString();
                else if (PlateColor == "Green" && BackGrdColor == "Green")
                    return Category.Transport.ToString();
                else if (PlateColor == "Green" && BackGrdColor == "White")
                    return Category.Police.ToString();
                else if (PlateColor == "Black" && BackGrdColor == "Black")
                    return Category.Protocol.ToString();
            }
            if (countryCode == "Abu_Dhabi")
            {
                if (PlateColor == "White" && BackGrdColor == "White")
                    return Category.Private.ToString();
                else if (PlateColor == "Yellow" && BackGrdColor == "White")
                    return Category.Taxi.ToString();
                else if (PlateColor == "Green" && BackGrdColor == "White")
                    return Category.Transport.ToString();
                else if (PlateColor == "Red" && BackGrdColor == "White")
                    return Category.Private.ToString();
                else if (PlateColor == "Red" && BackGrdColor == "Red")
                    return Category.Police.ToString();
            }
            return "N/A";
        }

        public static string GetColorByID(int code)
        {
            switch (code)
            {
                case (int)BackgroundColor.White:
                    return BackgroundColor.White.ToString();

                case (int)BackgroundColor.Black:
                    return BackgroundColor.Black.ToString();

                case (int)BackgroundColor.Blue:
                    return BackgroundColor.Blue.ToString();

                case (int)BackgroundColor.Cyan:
                    return BackgroundColor.Cyan.ToString();

                case (int)BackgroundColor.Green:
                    return BackgroundColor.Green.ToString();

                case (int)BackgroundColor.Orange:
                    return BackgroundColor.Orange.ToString();

                case (int)BackgroundColor.Yellow:
                    return BackgroundColor.Yellow.ToString();

                case (int)BackgroundColor.Red:
                    return BackgroundColor.Red.ToString();

                default:
                    return "Color Not Found";
            }
        }

        private string GetPlateSize(double xwidth, double yHeight)//gxPG4 cordinates, 
        {
            //var xwidth = CommonFunction.DistanceTo(cordinates.x1, cordinates.y1, cordinates.x2, cordinates.y2);
            //var yHeight = CommonFunction.DistanceTo(cordinates.x1, cordinates.y1, cordinates.x4, cordinates.y4);
            if (xwidth > yHeight)
                return PlateSize.Long.ToString();
            else
                return PlateSize.Short.ToString();
        }

        cmAnpr anpr;
        gxImage image;
        // string imagePath;
        public OCR()
        {
            // Creates the ANPR object
            anpr = new cmAnpr("default");

            //Sets an engine, that can read empty ADR plates
            //anpr.SetProperty("anprname", "cmanpr-7.3.9.74 : latin_general");
            //anpr.SetProperty("anprname", "cmanpr-7.3.9.141 : arab");
            // anpr.SetProperty("anprname", "cmanpr-7.3.9.199:arab");cmanpr-7.3.10.17:arab
            anpr.SetProperty("anprname", "cmanpr-7.3.10.17:arab");
            //     anpr.SetProperty("anprname", "cmanpr-7.2.7.108 : general");
            Console.WriteLine("Engine: '{0}'\n", anpr.GetProperty("anprname"));

            // Checks the licenses for the default engine 
            if (!anpr.CheckLicenses4Engine("", 0))
            {
                Console.WriteLine("Cannot find licenses for the current engine!!!");
                return;
            }
            //Enables the engine to return license, ADR and empty ADR plate
            anpr.SetProperty("general", 13);

            // Creates the image object
            image = new gxImage("default");

            //imagePath = path;
        }

        public vmOCR GetDetails(string path, string vpath)
        {
            vmOCR ocr = new vmOCR();
            //ocr.snapList = fnameList;
            //string path = fnameList[0];
            image = new gxImage("default");
            try
            {
                image.Load(path);

                if (anpr.FindFirst(image))
                {
                    ocr.ImagePath = path;
                    ocr.VideoPath = vpath;
                    // Get short country code
                    String cc = anpr.GetCountryCode(anpr.GetType(), (int)CC_TYPE.CCT_STATE_LONG);    //CC_TYPE.CCT_STATE_LONG);
                    var plateText = anpr.GetText();
                    if (!string.IsNullOrEmpty(plateText))
                    {
                        ocr.Status = Enumerations.OCRStatus.Success.ToString();
                        ocr.message = "OCR done successfully";
                    }
                    else
                    {
                        ocr.Status = Enumerations.OCRStatus.UnRecognized.ToString();
                        ocr.message = "No. PLate not recognized";
                    }
                    var noPlate = plateText.Substring(1, plateText.Length - 1);
                    ocr.StateCode = plateText.Substring(0, 1);
                    //Match match = Regex.Match(ocr.StateCode, @"\ba\w*\b", RegexOptions.IgnoreCase);
                    ocr.PlateNo = ocr.StateCode + (Regex.IsMatch(ocr.StateCode, @"^\d+") == true ? @"" : " ") + noPlate;
                    ocr.CountryCode = cc.Length > 0 ? cc : "No plate type" + " " + anpr.GetType();
                    ocr.BackgroundColor = GetColorByID(anpr.GetBkColor());
                    ocr.PlateColor = GetColorByID(anpr.GetColor());
                    ocr.Frame = anpr.GetFrame();
                    //ocr.Width = CommonFunction.GetDistance(ocr.Frame.x1-20, ocr.Frame.y1-20, ocr.Frame.x2-20, ocr.Frame.y2-20);
                    //ocr.Height = CommonFunction.GetDistance(ocr.Frame.x1-20, ocr.Frame.y1-20, ocr.Frame.x4-20, ocr.Frame.y4-20);
                    ocr.Width = CommonFunction.GetDistance(ocr.Frame.x1, ocr.Frame.y1, ocr.Frame.x2, ocr.Frame.y2);
                    ocr.Height = CommonFunction.GetDistance(ocr.Frame.x1, ocr.Frame.y1, ocr.Frame.x4, ocr.Frame.y4);
                    ocr.PlateSize = GetPlateSize(ocr.Width, ocr.Height);
                    ocr.VeichleType = GetVehicleTypeFromColour(anpr.GetColor());
                    ocr.Category = GetCategory(ocr.CountryCode, ocr.PlateColor, ocr.BackgroundColor);


                    //  ocr.PlateStatus = Enumerations.PlateStatus.Assinged.ToString();

                }
                else
                {
                    ocr.message = "No plate found";
                    ocr.Status = Enumerations.OCRStatus.NoPlate.ToString();
                    //Console.WriteLine("No plate found");
                }
                try
                {
                    //Finds the empty ADR plate
                    if (anpr.FindEmptyAdr())
                    {
                        ocr.message = "Empty ADR plate found.";
                        ocr.Status = Enumerations.OCRStatus.NoPlate.ToString();
                        //ocr.type = anpr.GetType();
                        //Console.WriteLine("Empty ADR plate found.");
                        //Console.WriteLine("Type: '{0}'", anpr.GetType());
                        gxPG4 fr = anpr.GetFrame();
                        //ocr.Frame = fr.x1.ToString() + " " + fr.y1.ToString() + "-" + 
                        Console.WriteLine("Frame: ({0},{1}) - ({2},{3}) - ({4},{5}) - ({6},{7})",
                            fr.x1, fr.y1, fr.x2, fr.y2, fr.x3, fr.y3, fr.x4, fr.y4);
                    }
                    else
                    {
                        ocr = new vmOCR();
                        ocr.message = "No plate found";
                        ocr.Status = Enumerations.OCRStatus.UnRecognized.ToString();
                        Console.WriteLine("Empty ADR plate not found.");
                    }
                }
                catch (Exception e)
                {
                    ocr = new vmOCR();
                    ocr.message = "No plate found";
                    ocr.Status = Enumerations.OCRStatus.NoPlate.ToString();
                }
            }
            catch (Exception ex)
            {
                ocr = new vmOCR();
                ocr.message = "No plate found";
                ocr.Status = Enumerations.OCRStatus.UnRecognized.ToString();
            }
            return ocr;
        }
    }
}
