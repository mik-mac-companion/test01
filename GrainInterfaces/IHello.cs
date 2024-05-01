using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainInterfaces;

public interface IHello : IGrainWithIntegerKey
{
    ValueTask<string> SayHello(string greeting);
    ValueTask<int> Multiply(int a, int b);
    ValueTask<int> AModB(int a, int b);
    Task<string> Pulldata();
    Task<string> PullHttpsData();
    Task<string> PullXboxInventoryData();
}