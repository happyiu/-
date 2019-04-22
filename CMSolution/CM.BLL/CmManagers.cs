using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;



namespace CM.BLL
{
    public class CmManagers
    {
        //数据库上下文,对象实例化
        Models.netcmsContext db = new Models.netcmsContext();
        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Exists(Models.CmManagers model)
        {
            return db.CmManagers.Contains(model) ;
        }

        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Models.CmManagers GetModel(int id)
        {
            return db.CmManagers.SingleOrDefault(o => o.Id == id);
        }

        /// <summary>
        /// 根据姓名关键字查询
        /// </summary>
        /// <param name="nameKey"></param>
        /// <returns></returns>
        public List<Models.CmManagers> GetList(string nameKey)
        {
            if (string.IsNullOrEmpty(nameKey))
                return db.CmManagers.ToList();
            else
                return db.CmManagers.Where(o => o.RealName.IndexOf(nameKey) > -1).ToList();
        }

        /// <summary>
        /// 取得所有记录模型
        /// </summary>
        /// <param name="nameKey"></param>
        /// <returns></returns>
        public List<Models.CmManagers> GetList()
        {
            return GetList("");
        }

        /// <summary>
        /// 添加保持
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(Models.CmManagers model)
        {
            int r = -1;
            if (!Exists(model))
            {
                db.CmManagers.Add(model);
                db.SaveChanges();
                r = db.CmManagers.Count();
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
            Models.CmManagers obj = GetModel(id);
            db.CmManagers.Remove(obj);
            r = db.SaveChanges() > 0;
            return r;
        }

        /// <summary>
        /// 根据模型到数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(Models.CmManagers model)
        {
            bool r = false;
            Models.CmManagers obj = GetModel(model.Id);
            obj.UserName = model.UserName;
            obj.RealName = model.RealName;
            obj.RoleId = model.RoleId;
            obj.Mobile = model.Mobile;
            obj.Remark = model.Remark;
            r = db.SaveChanges() > 0;
            return r;
        }
    }
}
