using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WALLET
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private string _path = @"C:\GitPerson\Desarrollo_Anteriores\WALLET\Files\";//Directory.GetCurrentDirectory() + @"\Files\";
        public  IFileData _fileData;
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
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //await Task.Delay(1000, stoppingToken);
                //await _fileData.Create($"{_path}{_cout++}.txt");
                //await _fileData.Wallet();
                await _fileData.Transferencia();
                //await Task.Delay(100, stoppingToken);
                //await _fileData.Create($"{_path}{_cout++}.txt");
            }
        }
    }
}
