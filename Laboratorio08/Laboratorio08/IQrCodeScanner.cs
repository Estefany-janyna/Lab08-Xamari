using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio08
{
    public interface IQrCodeScanner
    {
        Task<string> ScanQrCodeAsync();
    }
}
