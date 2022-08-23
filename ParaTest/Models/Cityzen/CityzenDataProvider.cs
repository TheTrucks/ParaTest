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

    public abstract class CityzenDataProvider : ICityzenDataGetter, ICityzenDataSetter
    {
        public abstract Task<IEnumerable<Cityzen>> GetByBirthDate(DateTime BirthDate);
        public abstract Task<IEnumerable<Cityzen>> GetByDeathDate(DateTime? DeathDate);
        public abstract Task<IEnumerable<Cityzen>> GetByDTO(GetCityzenDTO Input);
        public abstract Task<IEnumerable<Cityzen>> GetByFullName(string FullName);
        public abstract Task<Cityzen?> GetByINN(string INN);
        public abstract Task<Cityzen?> GetBySNILS(string SNILS);


        public abstract Task<ISetterResponse> Insert(Cityzen NewCityzen);
        public abstract Task<ISetterResponse> Update(Cityzen UpdCityzen);
        public abstract Task<ISetterResponse> Delete(long Id);
    }
}
