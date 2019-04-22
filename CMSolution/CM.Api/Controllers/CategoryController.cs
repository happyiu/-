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
    public class CategoryController : Controller
    {
        private BLL.CmArticlecategories bll = new BLL.CmArticlecategories();
        // GET: api/Role
        [HttpGet]
        public ActionResult Gets()
        {
            ResponseJson<CmArticlecategories> rdata = new ResponseJson<CmArticlecategories>();
            try
            {
                List<CmArticlecategories> list = bll.GetList();
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

        // GET: api/Role/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            ResponseJson<CmArticlecategories> rdata = new ResponseJson<CmArticlecategories>();
            try
            {
                List<CmArticlecategories> list = new List<CmArticlecategories>();
                CmArticlecategories model = bll.GetModel(id);
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

        // POST: api/Role
        [HttpPost]
        public ActionResult Post([FromBody] CmArticlecategories model)
        {
            ResponseJson<CmArticlecategories> rdata = new ResponseJson<CmArticlecategories>();
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

        // PUT: api/Role/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CmArticlecategories model)
        {
            ResponseJson<CmArticlecategories> rdata = new ResponseJson<CmArticlecategories>();
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
            ResponseJson<CmArticlecategories> rdata = new ResponseJson<CmArticlecategories>();
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
