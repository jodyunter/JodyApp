namespace JodyApp.Domain
{
    public interface IReferenceableDomainObject
    {
        int? Id { get; set; }
        string Name { get; set; }
    }
}
