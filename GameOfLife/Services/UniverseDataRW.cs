using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using GameOfLife.Models;

namespace GameOfLife.Services
{
    class UniverseDataRW
    {
        //Try catch
        public void Serialize()
        {
            UniverseState obj = new UniverseState();
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("MyFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, obj);
            stream.Close();
        }
        
        public UniverseState Deserialize()
        {
            UniverseState obj = new UniverseState();
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            obj = (UniverseState)formatter.Deserialize(stream);
            stream.Close();
            return obj;
        }
    }
}
