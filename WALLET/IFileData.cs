using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WALLET.Controllers;

namespace WALLET
{
    public interface IFileData
    {
         
        Task Create(string path);
        Task Wallet();
        Task Transferencia();
    }
}
