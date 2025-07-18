using IAmBusy.Model.Models;
using Microsoft.EntityFrameworkCore;

public class DepartmentManageService : IDepartmentManageService
{
    private readonly MainDbContext _dbContext;

    public DepartmentManageService(MainDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbContext.Database.EnsureCreatedAsync();
    }

    public async Task<IEnumerable<Department>?> GetAllDepartmentsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _dbContext.Departments.ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }
    }
    public async Task<Department?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _dbContext.Departments.FindAsync(new object[] { id }, cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }

    }

    public async Task<Department?> CreateDepartmentAsync(Department newDepartment, CancellationToken cancellationToken = default)
    {
        try
        {
            //MyCon1sole.WriteLine($"创建新的图站，IP为：{newDepartmentAgentDepartmentConfig.DepartmentAgentHostIp}");
            _dbContext.Departments.Add(newDepartment);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return newDepartment;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return null;
    }
    public async Task<bool> DeleteDepartmentAsync(int DepartmentId, CancellationToken cancellationToken = default)
    {
        var Department = await _dbContext.Departments.FindAsync(DepartmentId);
        if (Department == null)
        {
            return false;
        }

        _dbContext.Departments.Remove(Department);
        await _dbContext.SaveChangesAsync();
        return true;
    }
    public async Task<Department?> UpdateDepartmentAsync(Department updatedDepartment, CancellationToken cancellationToken = default)
    {
        var existingDepartment = await _dbContext.Departments.FindAsync(new object[] { updatedDepartment.Id }, cancellationToken);
        if (existingDepartment == null)
        {
            await CreateDepartmentAsync(updatedDepartment);
            return updatedDepartment;
        }
        // 更新实体的属性
        //existingDepartment.DepartmentName = updatedDepartment.DepartmentName;
        //existingDepartment.DepartmentVersion = updatedDepartment.DepartmentVersion;
        //existingDepartment.DepartmentType = updatedDepartment.DepartmentType;
        //existingDepartment.DepartmentPath = updatedDepartment.DepartmentPath;
        //existingDepartment.DepartmentCompany= updatedDepartment.DepartmentCompany;
        //existingDepartment.CommandParameter = updatedDepartment.CommandParameter;
        //existingDepartment.DepartmentDescription = updatedDepartment.DepartmentDescription;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return existingDepartment;
    }

}

