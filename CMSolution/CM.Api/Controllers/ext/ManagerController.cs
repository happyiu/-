using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CM.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CM.Models.Ext;

namespace CM.Api.Controllers.ext
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ManagerController : Controller
    {
        // GET: api/Manager
        [HttpGet]
        public ActionResult GetManagerData()
        {
            ResponseJson<ManagerResponse> rdata = new ResponseJson<ManagerResponse>();
            try
            {
                BLL.Ext.Manager bll = new BLL.Ext.Manager();
                List<ManagerResponse> list =new List<ManagerResponse>();
                list.Add(bll.GetList());
                rdata.Count = list.Count;
                rdata.Data = list;
            }
            catch (Exception ex)
            {
                rdata.Code = 500;
                rdata.Msg = ex.Message;
                rdata.Count = 0;
            }
            return Json(rdata);
        }

    }
}
