using System;
using System.Threading.Tasks;
using SmartHome.DomainCore.InfrastructureInterfaces;
using SmartHome.DomainCore.ServiceInterfaces.Permission;

namespace SmartHome.Services.Permission
{
    public class DeletePermissionService : IDeletePermissionService
    {
        private readonly IPermissionRepository repository;

        public DeletePermissionService(IPermissionRepository repository)
        {
            this.repository = repository;
        }

        public Task DeleteAsync(long id)
        {
            return repository.DeleteAsync(id);
        }
    }
}