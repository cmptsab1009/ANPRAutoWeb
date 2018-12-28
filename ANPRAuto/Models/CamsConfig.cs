using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ANPRAuto.Models
{
    public class CamsConfig
    {
        public CamsConfig()
        {
            var posCamUrl = ConfigurationManager.AppSettings["posCamUrl"];
            var cam1 = ConfigurationManager.AppSettings["cam1"].Split('-');
            if(cam1.Count() > 0)
            { 
             cam1Name = cam1[0].Trim();
                cam1IPAdd = cam1[1].Trim();
             cam1IP = "http://"+ cam1IPAdd + posCamUrl;
            }

            var cam2 = ConfigurationManager.AppSettings["cam2"].Split('-');
            if (cam2.Count() > 0)
            {
                cam2Name = cam2[0].Trim();
                cam2IPAdd = cam2[1].Trim();
                cam2IP = "http://" + cam2IPAdd + posCamUrl;
            }

            var cam3 = ConfigurationManager.AppSettings["cam3"].Split('-');
            if (cam3.Count() > 0)
            {
                cam3Name = cam3[0].Trim();
                cam3IPAdd = cam3[1].Trim();
                cam3IP = "http://" + cam3IPAdd + posCamUrl;
            }

            var cam4 = ConfigurationManager.AppSettings["cam4"].Split('-');
            if (cam4.Count() > 0)
            {
                cam4Name = cam4[0].Trim();
                cam4IPAdd = cam4[1].Trim();
                cam4IP = "http://" + cam4IPAdd + posCamUrl;
            }
        }
        public string cam1Name { get; set; }
        public string cam1IP { get; set; }
        public string cam1IPAdd { get; set; }

        public string cam2Name { get; set; }
        public string cam2IP { get; set; }
        public string cam2IPAdd { get; set; }


        public string cam3Name { get; set; }
        public string cam3IP { get; set; }
        public string cam3IPAdd { get; set; }

        public string cam4Name { get; set; }
        public string cam4IP { get; set; }
        public string cam4IPAdd { get; set; }
    }

   
}