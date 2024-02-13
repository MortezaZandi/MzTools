using System.IO.MemoryMappedFiles;
using System.IO;

namespace MMF_IPC
{
    public class MMF
    {
        private MemoryMappedFile memoryMappedFile;
        private string memoryMappedFileName = "Logs";
        private long capacity = 1024 * 1024;

        public MMF(string name, long capacity)
        {
            this.memoryMappedFileName = name;
            this.capacity = capacity;
        }

        public void Open()
        {
            this.memoryMappedFile = MemoryMappedFile.OpenExisting(this.memoryMappedFileName, MemoryMappedFileRights.ReadWrite, HandleInheritability.Inheritable);
        }

        public void Create()
        {
            this.memoryMappedFile = MemoryMappedFile.CreateOrOpen(this.memoryMappedFileName, this.capacity, MemoryMappedFileAccess.ReadWrite);
        }

        public string Read()
        {
            using (MemoryMappedViewStream stream = memoryMappedFile.CreateViewStream())
            {
                BinaryReader reader = new BinaryReader(stream);
                return reader.ReadString();
            }
        }

        public void Write(string data)
        {
            using (MemoryMappedViewStream stream = this.memoryMappedFile.CreateViewStream())
            {
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(data);
            }
        }
    }
}
