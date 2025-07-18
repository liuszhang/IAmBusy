
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
        public async Task<List<UserTask>> GetAllTasks(CancellationToken cancellationToken=default)
        {
            //var result = httpClient.GetFromJsonAsAsyncEnumerable<Department>("/api/department/GetAllDepartments", cancellationToken);
            //return await result.ToListAsync(cancellationToken); 
            var result = await httpClient.GetFromJsonAsync<IEnumerable<UserTask>?>("api/task/GetAllTasks", cancellationToken);
            return result?.ToList();
        }


        //创建新用户
        public async Task<UserTask?> CreateNewUserTask(UserTask newUserTask, CancellationToken cancellationToken = default)
        {
            var response = await httpClient.PostAsJsonAsync("api/task/CreateTask", newUserTask, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserTask>(cancellationToken: cancellationToken);
            }
            else
            {
                // 处理错误情况
                return null;
            }
        }

        //删除任务
        public async Task<bool> DeleteUserTask(int taskId,CancellationToken cancellationToken=default)
        {
            var response = await httpClient.DeleteAsync($"api/task/DeleteTask/{taskId}", cancellationToken);
            return response.IsSuccessStatusCode;
        }

    }
}
