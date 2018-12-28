using ANPRData;
using DataTables.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VeichleOCR.Modal;

namespace ANPRAuto.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetVeichleInfo([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            var _dbContext = new dbANPREntities();

            IQueryable<tblANPRData> query = _dbContext.tblANPRDatas;
            var totalCount = query.Count();

            #region Filtering 

            // Apply filters for searching 
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.BackGroundColour.Contains(value) ||
                                         p.Category.Contains(value) ||
                                         p.CountryCode.Contains(value) ||
                                         p.Lane.Contains(value) ||
                                         p.PlateColour.Contains(value) ||
                                         p.PlateNo.Contains(value) ||
                                         p.PlateSize.Contains(value) ||
                                         p.Status.Contains(value) ||
                                         p.Time.Value.Equals(value) ||
                                         p.Date.Equals(value)
                                   );
            }

            var filteredCount = query.Count();

            #endregion Filtering 


            // Paging 
            query = query.OrderBy(c => c.Id).Skip(requestModel.Start).Take(requestModel.Length);


            var data = query.Select(ToModel).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        public vmOCR ToModel(tblANPRData user)
        {
            return new vmOCR()
            {
                Date = user.Date    ,
                Time = user.Time.ToString(),
                Lane = user.Lane,
                PlateNo = user.PlateNo,
                CountryCode = user.CountryCode,
                Status = user.Status,
                PlateColor = user.PlateColour,
                BackgroundColor = user.BackGroundColour,
                Category = user.Category,
                type = user.PlateSize,
                ImagePath = user.CImage,
                Image1 = user.Image1

            };
        }
    }
}