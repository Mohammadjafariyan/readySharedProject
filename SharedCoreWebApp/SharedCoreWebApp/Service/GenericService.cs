using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SharedCoreWebApp.ContextFactory;
using SharedCoreWebApp.Models;

namespace SharedCoreWebApp.Service
{
  

    public abstract class GenericService<T> : IService<T> where T : class, IEntity
    {
        protected readonly ContextFactoryService _contextFactoryService;
        protected AbstractDbContext _context;

        protected GenericService(ContextFactoryService contextFactoryService)
        {
            _contextFactoryService = contextFactoryService;
            _context = contextFactoryService.GetDbContext(null);
        }

        public MyDataTableResponse<T> GetAsPaging(int take, int? skip)
        {
            return QueryPaging(take, skip, null, null);
        }

        protected MyDataTableResponse<T> QueryPaging(int take, int? skip, Func<IQueryable<T>
                , IQueryable<T>> funcForInclude = null,
            Func<IQueryable<T>, IQueryable<T>> funcForFilter = null)
        {
            if (take <= 0)
            {
                take = 20;
            }

            if (skip <= 0)
            {
                throw new Exception("skip صفر یا کوچکتر از صفر پاس شده است");
            }

            var entities = _context.Set<T>().AsNoTracking().AsQueryable();

            if (funcForInclude != null)
            {
                entities = funcForInclude(entities);
            }

            if (funcForFilter != null)
            {
                entities = funcForFilter(entities);
            }

            IQueryable<T> res;
            if (skip.HasValue && skip > 0)
            {
                res = entities.OrderByDescending(e => e.Id).Skip(skip.Value).Take(take);
            }
            else
            {
                res = entities.OrderByDescending(e => e.Id).Take(take);
            }

            return new MyDataTableResponse<T>
            {
                LastSkip = skip,
                LastTake = take,
                List = res.ToList(),
                Total = res.Count(),
            };
        }


        public MyEntityResponse<T> GetById(int id)
        {
            var entities = _context.Set<T>().AsNoTracking().AsQueryable();

            var entity = entities.FirstOrDefault(e => e.Id == id);
            if (entity == null)
            {
                throw new Exception("رکورد یافت نشد");
            }

            return new MyEntityResponse<T>
            {
                Single = entity
            };
        }

        public MyEntityResponse<int> Create(T model)
        {
            var entities = _context.Set<T>();

            if (model.Id == 0)
            {
                model.CreateDate = DateTime.Now;
                model.ExpireDate = null;
                model.NextId = null;
                model.PrevId = null;
                entities.Add(model);
            }

            _context.SaveChanges();
            return new MyEntityResponse<int>
            {
                Single = model.Id
            };
        }

        protected MyEntityResponse<int> Update(T model,Func<T,T,T> updateFunc=null)
        {
            var entities = _context.Set<T>();

            if (model.Id == 0)
            {
                throw new Exception("رکورد با کد صفر داده شده است");
            }

            var record = entities.Find(model.Id);
            if (record == null)
            {
                throw new Exception("رکورد یافت نشد");
            }

            // یک تاریخچه از رکورد قبلی نگه می داریم
            record=MakeHistory(record);
            
            // مواظب هستیم این فیلد ها از بین نرود
// نگه میداریم تا یک موقع از بین نروند             
            var CreateDate = record.CreateDate;
            var ExpireDate = record.ExpireDate;
            var NextId = record.NextId;
            var PrevId = record.PrevId;

            if (updateFunc!=null)
            {
                record=updateFunc(record, model);
            }
            else
            {
                _context.Entry(record).CurrentValues.SetValues(model);
            }

            record.CreateDate = CreateDate;
            record.ExpireDate = ExpireDate;
            record.NextId = NextId;
            record.PrevId = PrevId;

            _context.SaveChanges();
            
          

            return new MyEntityResponse<int>
            {
                Single = record.Id
            };
        }

        protected T MakeHistory(T record)
        {
            var entities = _context.Set<T>();
            var copy = GlobalHelpers.ObjectCopier.Copy(record);

            copy.ExpireDate = DateTime.Now;
            copy.NextId = record.Id;

            copy.Id = 0; //new history

            entities.Add(copy);
            _context.SaveChanges();

            record.PrevId = copy.Id;
            record.CreateDate = DateTime.Now;

            _context.Entry(record).Property(r => r.PrevId).IsModified = true;
            _context.Entry(record).Property(r => r.CreateDate).IsModified = true;
            _context.SaveChanges();

            return record;
        }

        public MyEntityResponse<int> Save(T model)
        {
            if (model.Id == 0)
            {
               return Create(model);
            }

            return Update(model);
        }

        public MyEntityResponse<T> DeleteById(int id)
        {
            var myEntityResponse = GetById(id);
            
            myEntityResponse.Single.ExpireDate=DateTime.Now;

            _context
                .Entry(myEntityResponse.Single).State = EntityState.Modified;
            _context.SaveChanges();

            return myEntityResponse;
        }
    }
}