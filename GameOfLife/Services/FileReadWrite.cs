using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using GameOfLife.Models;

namespace GameOfLife.Services
{
    internal class FileReadWrite
    {
        public void Serialize(UniverseState obj)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream("MyFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, obj);
                stream.Close();
            }
            catch (Exception ex)
            {
                var output = UserInterfaceIO.GetInstance;
                output.ErrorMessage(ex.Message);
            }
        }

        public UniverseState Deserialize()
        {
            try
            {
                UniverseState obj = new UniverseState();
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream("MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
                obj = (UniverseState)formatter.Deserialize(stream);
                stream.Close();
                return obj;
            }
            catch (Exception ex)
            {
                var output = UserInterfaceIO.GetInstance;
                output.ErrorMessage(ex.Message);
            }
            return null;
        }
    }
}