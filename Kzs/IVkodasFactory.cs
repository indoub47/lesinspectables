using System.Collections.Generic;
namespace Kzs
{
    public interface IVkodasFactory
    {
        Vk Make(object[] rec);
    }
}