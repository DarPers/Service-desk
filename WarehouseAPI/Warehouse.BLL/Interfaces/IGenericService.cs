using Warehouse.BLL.Models;

namespace Warehouse.BLL.Interfaces;
public interface IGenericService<TModel> where TModel : DeviceModel
{
    public Task<List<TModel>> GetAllModels(CancellationToken cancellationToken);

    public Task<TModel> GetModelById(Guid id, CancellationToken cancellationToken);

    public Task<TModel> CreateModel(TModel model, CancellationToken cancellationToken);

    public Task<TModel> UpdateModel(Guid id, TModel model, CancellationToken cancellationToken);

    public Task DeleteModel(Guid id, CancellationToken cancellationToken);

    public Task<List<TModel>> GetModelsByName(string name, CancellationToken cancellationToken);
}
