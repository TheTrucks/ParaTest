namespace ParaTest.Models.Cityzen
{
    public interface ICityzenDataGetter
    {
        Task<Cityzen?> GetBySNILS(string SNILS);
        Task<Cityzen?> GetByINN(string INN);
        Task<IEnumerable<Cityzen>> GetByFullName(string FullName);
        Task<IEnumerable<Cityzen>> GetByBirthDate(DateTime BirthDate);
        Task<IEnumerable<Cityzen>> GetByDeathDate(DateTime? DeathDate);
        Task<IEnumerable<Cityzen>> GetByDTO(GetCityzenDTO Input);
    }

    public interface ICityzenDataSetter
    {
        Task<ISetterResponse> Insert(Cityzen NewCityzen);
        Task<ISetterResponse> Update(Cityzen UpdCityzen);
        Task<ISetterResponse> Delete(long Id);
    }

    public interface ISetterResponse
    {
        string Message { get; set; }
        bool Success { get; set; }
    }
}
