using ANPRAuto.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VeichleOCR;
using VeichleOCR.Modal;
using ANPRData;
using System.Web.Script.Serialization;

namespace ANPRAuto.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var modal = new CamsConfig();
            return View(modal);
        }

        [HttpPost]
        public JsonResult UploadPic(string imageData, bool isRequired = false)
        {
            var innerfolder = DateTime.Now.ToShortDateString().Replace("/", "");
            string Pic_Path = Server.MapPath("~/UploadPics/" + innerfolder);
            // string path = @"C:\MP_Upload";
            if (!Directory.Exists(Pic_Path))
            {
                Directory.CreateDirectory(Pic_Path);
            }
            try
            {
                var fileName = Pic_Path + "/" + DateTime.Now.Ticks.ToString() + ".png";
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        byte[] data = Convert.FromBase64String(imageData);
                        bw.Write(data);
                        bw.Close();
                    }
                }
                if (isRequired)
                {
                    OCR ocr = new OCR();
                    var ocrDetails = ocr.GetDetails(fileName, string.Empty);
                    ocrDetails.ImagePath = fileName;
                    return Json(ocrDetails, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var ocrDetails = new vmOCR();
                    ocrDetails.ImagePath = fileName;
                    return Json(ocrDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            // return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveData(List<vmOCR> lstData)
        {
            var _context = new dbANPREntities();
            int i = 0;
            var lstAnprData = new List<tblANPRData>();
            if (lstData != null)
            {
                while (i < lstData.Count())
                {
                    try
                    {
                        string[] frameLst;
                        if (!string.IsNullOrEmpty(lstData[i].FrameStr))
                            frameLst = lstData[i].FrameStr.Split(',');

                        tblANPRData anprdata = new tblANPRData()
                        {
                            Date = DateTime.Now.Date,
                            Time = System.TimeSpan.Parse(lstData[i].Time),
                            Lane = lstData[i].Lane,
                            PlateNo = lstData[i].PlateNo,
                            CountryCode = lstData[i].CountryCode,
                            Status = lstData[i].Status,
                            PlateColour = lstData[i].PlateColor,
                            BackGroundColour = lstData[i].BackgroundColor,
                            Category = lstData[i].Category,
                            PlateSize = lstData[i].PlateSize,
                            Image1 = lstData[i].ImagePath,
                            //y1 = Convert.ToInt32(!string.IsNullOrEmpty(frameLst[0]) ? ),
                            //y2 = Convert.ToInt32(frameLst[1]),
                            //y3 = Convert.ToInt32(frameLst[2]),
                            //y4 = Convert.ToInt32(frameLst[3]),
                            //x1 = Convert.ToInt32(frameLst[0]),
                            //x2 = Convert.ToInt32(frameLst[1]),
                            //x3 = Convert.ToInt32(frameLst[2]),
                            //x4 = Convert.ToInt32(frameLst[3]),
                            Video = lstData[i].VideoPath,

                            // PlateImage =  ANPR_ShreeSoftware.ViewModel.Common.ExceuteTheConcept(ocrDetails.ImagePath, ocrDetails.Frame.x1, ocrDetails.Frame.y1,ocrDetails.Width, ocrDetails.Height),
                            CImage = ""
                        };
                        lstAnprData.Add(anprdata);


                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                    i++;
                }
                _context.tblANPRDatas.AddRange(lstAnprData);
                _context.SaveChanges();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetStateList(int CId)
        {
            var _context = new dbANPREntities();
            string retval = "[]";
            var lst = _context.tblstates.Where(x=>x.CountryId == CId).Select(y=> new { id=y.State,Name = y.State }).ToList();

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            retval = serializer.Serialize(lst);
            return Json(retval, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}