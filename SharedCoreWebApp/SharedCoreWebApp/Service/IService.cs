using System;
using SharedCoreWebApp.Models;

namespace SharedCoreWebApp.Service
{

    public interface IEntity
    {
        int Id { get; set; }
        int? NextId { get; set; }
        int? PrevId { get; set; }
        DateTime? CreateDate { get; set; }
        DateTime? ExpireDate { get; set; }
    }
    public interface IService<T> where  T: class, IEntity
    {
        MyDataTableResponse<T> GetAsPaging(int take, int? skip);
        MyEntityResponse<T> GetById(int id);
        MyEntityResponse<int> Create(T model);
        MyEntityResponse<int> Save(T model);
        MyEntityResponse<T> DeleteById(int id);
    }
}
