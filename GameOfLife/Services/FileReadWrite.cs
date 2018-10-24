using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using GameOfLife.Models;

namespace GameOfLife.Services
{
    public class FileReadWrite
    {
        #region LazySingleton

        private static readonly Lazy<FileReadWrite> instance =
            new Lazy<FileReadWrite>(() => new FileReadWrite());

        public static FileReadWrite GetReadWriteService
        {
            get
            {
                return instance.Value;
            }
        }

        private FileReadWrite() { }

        #endregion LazySingleton

        public void Serialize(UniverseState[] obj) //split 
        {
            try
            {
                IFormatter formatter = new BinaryFormatter(); //Streams
                Stream stream = new FileStream("MyFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, obj); //serial
                stream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public UniverseState[] Deserialize()
        {
            try
            {
                UniverseState[] obj;
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream("MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
                obj = (UniverseState[])formatter.Deserialize(stream);
                stream.Close();
                return obj;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}