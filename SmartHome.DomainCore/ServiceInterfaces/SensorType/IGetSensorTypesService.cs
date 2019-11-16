using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHome.DomainCore.Data;
using SmartHome.DomainCore.Data.Models;

namespace SmartHome.DomainCore.ServiceInterfaces.SensorType
{
    public interface IGetSensorTypesService
    {
        Task<IList<SensorTypeModel>> GetAllSensorTypesAsync();
    }
}