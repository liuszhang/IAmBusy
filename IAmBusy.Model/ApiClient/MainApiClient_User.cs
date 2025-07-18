
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
        public async Task<List<User>> GetAllUsers(CancellationToken cancellationToken=default)
        {
            //var result = httpClient.GetFromJsonAsAsyncEnumerable<Department>("/api/department/GetAllDepartments", cancellationToken);
            //return await result.ToListAsync(cancellationToken); 
            var result = await httpClient.GetFromJsonAsync<IEnumerable<User>?>("api/user/GetAllUsers", cancellationToken);
            return result?.ToList();
        }


        //创建新用户
        public async Task<User?> CreateNewUser(User newUser, CancellationToken cancellationToken = default)
        {
            var response = await httpClient.PostAsJsonAsync("api/user/CreateUser", newUser, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<User>(cancellationToken: cancellationToken);
            }
            else
            {
                // 处理错误情况
                return null;
            }
        }


        //通过ID删除人员
        public async Task<bool> DeleteUser(int userId, CancellationToken cancellationToken = default)
        {
            var response = await httpClient.DeleteAsync($"api/user/DeleteUser/{userId}", cancellationToken);
            return response.IsSuccessStatusCode;
        }

    }
}
