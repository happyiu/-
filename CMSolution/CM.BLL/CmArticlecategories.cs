using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace CM.BLL
{
    public class CmArticlecategories
    {
        //数据库上下文,对象实例化
        Models.netcmsContext db = new Models.netcmsContext();

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Exists(Models.CmArticlecategories model)
        {
            return db.CmArticlecategories.Contains(model) ;
        }

        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Models.CmArticlecategories GetModel(long id)
        {
            return db.CmArticlecategories.SingleOrDefault(o => o.Id == id);
        }

        /// <summary>
        /// 根据姓名关键字查询
        /// </summary>
        /// <param name="nameKey"></param>
        /// <returns></returns>
        public List<Models.CmArticlecategories> GetList(string nameKey)
        {
            if (string.IsNullOrEmpty(nameKey))
                return db.CmArticlecategories.ToList();
            else
                return db.CmArticlecategories.Where(o => o.Name.IndexOf(nameKey) > -1).ToList();
        }

        /// <summary>
        /// 取得所有记录模型
        /// </summary>
        /// <param name="nameKey"></param>
        /// <returns></returns>
        public List<Models.CmArticlecategories> GetList()
        {
            return GetList("");
        }

        /// <summary>
        /// 添加保持
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(Models.CmArticlecategories model)
        {
            int r = -1;
            if (!Exists(model))
            {
                db.CmArticlecategories.Add(model);
                db.SaveChanges();
                r = db.CmArticlecategories.Count();
            }
            return r;
        }

        /// <summary>
        /// 删除指定id的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Remove(int id)
        {
            bool r = false;
            Models.CmArticlecategories obj = GetModel(id);
            db.CmArticlecategories.Remove(obj);
            r = db.SaveChanges() > 0;
            return r;
        }

        /// <summary>
        /// 根据模型到数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(Models.CmArticlecategories model)
        {
            bool r = false;
            Models.CmArticlecategories obj = GetModel(model.Id);
            //obj.Name = model.Name;
            //obj.Rank = model.Rank;
            r = db.SaveChanges() > 0;
            return r;
        }
    }
}
