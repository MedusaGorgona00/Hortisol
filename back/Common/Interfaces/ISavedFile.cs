namespace Common.Interfaces;

public interface ISavedFile : IIdHas<int>
{
    public string Name { get; set; }

    public string Path { get; set; }

    public byte[] Md5 { get; set; }
}
