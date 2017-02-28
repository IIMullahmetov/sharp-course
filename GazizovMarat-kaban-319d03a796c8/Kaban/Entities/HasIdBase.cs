namespace Kaban.Entities
{
    public interface IHasId
    {
        object Id { get; }
    }

    public interface IHasId<out T> : IHasId
    {
        new T Id { get; }
    }

    public abstract class HasIdBase<T> : IHasId<T>
    {
        public T Id { get; set; }

        object IHasId.Id => Id;

  
    }
}