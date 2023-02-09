namespace Common.Interfaces;

public interface IIdHasRecord<T>
{
    T Id { get; init; }
}

public interface INameHasRecord
{
    string Name { get; init; }
}

public interface IIdNameHasRecord : IIdHasRecord<int>, INameHasRecord { }
