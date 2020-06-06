using Face.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 数据访问层
/// </summary>
namespace Face.DAL {
    //where后面用于限制泛型类型
    public class BaseService<T> : IDisposable where T : BaseEntity, new() {//实现IDisposable接口用于释放资源
        /*
         *      protected：受保护，只有继承这个类才能用
                 readonly:只读
             */
        protected readonly FaceContext _db;
        public BaseService(FaceContext db) {
            _db = db;
        }
        public void Dispose() {
            _db.Dispose();
        }
        /// <summary>
        /// 异步保存，第二个参数意思是：不是每一次添加都要保存，可以先add进去，再一起保存
        /// </summary>
        /// <param name="t">要保存的对象</param>
        /// <param name="isSave">是否保存</param>
        /// <returns></returns>
        public async Task CreatAsync(T t, bool isSave) {
            _db.Set<T>().Add(t);
            if (isSave) await _db.SaveChangesAsync();
        }
        public void Creat(T t) {
            _db.Set<T>().Add(t);
            _db.SaveChanges();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync() {
            await _db.SaveChangesAsync();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="t"></param>
        /// <param name="isSave"></param>
        /// <returns></returns>
        public async Task Edit(T t, bool isSave) {
            _db.Entry(t).State = System.Data.Entity.EntityState.Modified;
            _db.Entry(t).Property(m=>m.CreatTime).IsModified = false;
            _db.Entry(t).Property(m=>m.IsDelete).IsModified = false;
            if (isSave) {
                _db.Configuration.ValidateOnSaveEnabled = false;//自动校验
                await _db.SaveChangesAsync();
                _db.Configuration.ValidateOnSaveEnabled = true;
            }
        }

        /*
        public async Task Delete(Guid id, bool isSave) {
            T t = new T() {
                Id = id
            };
            _db.Entry(t).State = System.Data.Entity.EntityState.Unchanged;
            if (isSave) {
                _db.Configuration.ValidateOnSaveEnabled = false;//关闭自动校验
                await _db.SaveChangesAsync();
                _db.Configuration.ValidateOnSaveEnabled = true;
            }
        }
        */
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll() {

            return _db.Set<T>().AsNoTracking();
        }

        /// <summary>
        /// 可以自己添加where条件的方法
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate) {

            return _db.Set<T>().AsNoTracking().Where(predicate);
        }

        /// <summary>
        /// 增加排序
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <param name="PaiXu">升序OR降序</param>
        /// <returns></returns>
        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, bool PaiXu) {

            var data = _db.Set<T>().AsNoTracking().Where(predicate);
            if (PaiXu) {
                return data.OrderBy(T => T.CreatTime);
            }
            return data.OrderByDescending(T => T.CreatTime);
        }

        /// <summary>
        /// 添加分页效果
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="PaiXu"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, bool PaiXu, int PageSize, int PageIndex) {

            return GetAll(predicate, PaiXu).Skip(PageSize * PageIndex).Take(PageSize);
        }



        /// <summary>
        /// 根据id查询单个对象
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>对象</returns>
        //public async Task<T> GetOne(Guid id) {
        //    return await GetAll().FirstAsync(m => m.Id == id);
        //}
        public T GetOneById(int id) {
            return _db.Set<T>().Find(id);
        }

        public void Delete(T t) {
            _db.Set<T>().Attach(t);
            _db.Entry(t).Property(u => u.IsDelete).IsModified = true;
            _db.SaveChanges();
        }
    }
}
