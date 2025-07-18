using IAmBusy.Model.Models;


public interface IDepartmentManageService
    {
        Task<IEnumerable<Department>?> GetAllDepartmentsAsync(CancellationToken cancellationToken = default);
        Task<Department?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Department?> CreateDepartmentAsync(Department newDepartment, CancellationToken cancellationToken = default);
        Task<bool> DeleteDepartmentAsync(int DepartmentId, CancellationToken cancellationToken = default);
        Task<Department?> UpdateDepartmentAsync(Department updatedDepartment, CancellationToken cancellationToken = default);

    }

