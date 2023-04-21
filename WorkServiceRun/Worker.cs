using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WorkServiceRun
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private string _path = Directory.GetCurrentDirectory() + @"\Files\";
        private IFileData _fileData;
        private int _cout = 0;
        public Worker(ILogger<Worker> logger, IFileData fileData)
        {
            _logger = logger;
            _fileData = fileData;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
               // _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //await Task.Delay(1000, stoppingToken);
                await _fileData.Create($"{_path}{_cout++}.txt");
            }
        }
    }
}
