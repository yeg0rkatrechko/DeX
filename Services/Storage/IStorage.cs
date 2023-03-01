namespace Services.Storage
{
    internal interface IStorage<T>
    {
        public void Add(T item);
        public void Delete(T item);
        public void Update(T item);
    }
}
