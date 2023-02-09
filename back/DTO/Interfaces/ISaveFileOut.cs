using Common.Interfaces;

namespace DTO.Interfaces;

public interface ISaveFileOut: IIdHas<int>
{
    public string Name { get; init; }

    public string Link { get; init; }
}
