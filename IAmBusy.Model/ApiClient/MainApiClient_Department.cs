
using IAmBusy.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IAmBusy.Model.ApiClient
{
    public partial class MainApiClient
    {
        public async Task<List<Department>> GetAllDepartments(CancellationToken cancellationToken=default)
        {
            //var result = httpClient.GetFromJsonAsAsyncEnumerable<Department>("/api/department/GetAllDepartments", cancellationToken);
            //return await result.ToListAsync(cancellationToken); 
            var result = await httpClient.GetFromJsonAsync<IEnumerable<Department>?>("api/department/GetAllDepartments", cancellationToken);
            return result?.ToList();
        }


        public async Task<Department?> CreateNewDepartment(Department newDepartment, CancellationToken cancellationToken=default)
        {
            var response = await httpClient.PostAsJsonAsync("api/department/CreateDepartment", newDepartment, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Department>(cancellationToken: cancellationToken);
            }
            else
            {
                // 处理错误情况
                return null;
            }
        }

        //通过ID删除部门
        public async Task<bool> DeleteDepartment(int departmentId, CancellationToken cancellationToken = default)
        {
            var response = await httpClient.DeleteAsync($"api/department/DeleteDepartment/{departmentId}", cancellationToken);
            return response.IsSuccessStatusCode;
        }




        }
}
