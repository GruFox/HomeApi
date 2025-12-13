using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.Contracts.Models.Devices;

public class DeleteDeviceRequest
{
    public string NewRoom { get; set; }
    public string NewName { get; set; }
    public string NewSerial { get; set; }
}
