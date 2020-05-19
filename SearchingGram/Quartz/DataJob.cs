using Microsoft.Extensions.DependencyInjection;
using Quartz;
using SearchingGram.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchingGram.Quartz
{
    public class DataJob : IJob
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        public DataJob(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            using (var scope = serviceScopeFactory.CreateScope()) {
                var refresher = scope.ServiceProvider.GetService<IRefreshInfoService>();

                await refresher.RefreshAccountsInfo();
            }
        }
    }
}
