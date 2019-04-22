using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using CM.Models;
using CM.Utility;

namespace CM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : Controller
    {
        private BLL.CmManagers bll = new BLL.CmManagers();
        // GET: api/Manager
        [HttpGet]
        public ActionResult Gets()
        {
            ResponseJson<CmManagers> rdata = new ResponseJson<CmManagers>();
            try
            {
                List<CmManagers> list = bll.GetList();
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

        // GET: api/Manager/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            ResponseJson<CmManagers> rdata = new ResponseJson<CmManagers>();
            try
            {
                List<CmManagers> list = new List<CmManagers>();
                CmManagers model = bll.GetModel(id);
                if (model != null)
                    list.Add(model);           
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

        // POST: api/Manager
        [HttpPost]
        public ActionResult Post([FromBody] CmManagers model)
        {
            ResponseJson<CmManagers> rdata = new ResponseJson<CmManagers>();
            try
            {
                if (bll.Exists(model))
                {
                    rdata.Code = 500;
                    rdata.Msg = "数据已存在，不能重复添加";
                }
                else
                {
                    if (bll.Add(model) <= 0)
                    {
                        rdata.Code = 500;
                        rdata.Msg = "保存失败";
                    }
                }
            }
            catch (Exception ex)
            {
                rdata.Code = 500;
                rdata.Msg = ex.Message;
                rdata.Count = 0;
            }
            return Json(rdata);
        }

        // PUT: api/Manager/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CmManagers model)
        {
            ResponseJson<CmManagers> rdata = new ResponseJson<CmManagers>();
            try
            {
                if (!bll.Exists(model))
                {
                    rdata.Code = 500;
                    rdata.Msg = "数据不存在";
                }
                else
                {
                    if (!bll.Update(model))
                    {
                        rdata.Code = 500;
                        rdata.Msg = "保存失败";
                    }
                }
            }
            catch (Exception ex)
            {
                rdata.Code = 500;
                rdata.Msg = ex.Message;
            }
            return Json(rdata);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            ResponseJson<CmManagers> rdata = new ResponseJson<CmManagers>();
            try
            {
                if (!bll.Remove(id))
                {
                    rdata.Code = 500;
                    rdata.Msg = "删除失败";
                }
            }
            catch (Exception ex)
            {
                rdata.Code = 500;
                rdata.Msg = ex.Message;
            }
            return Json(rdata);
        }
    }
}
