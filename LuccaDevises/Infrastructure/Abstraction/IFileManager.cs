using Domain;

namespace Infrastructure.Abstraction
{
    public interface IFileManager
    {
        BaseData GetData(string filePath);
    }
}
