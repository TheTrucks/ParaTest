using Microsoft.EntityFrameworkCore;

namespace ParaTest.Models.Cityzen
{
    public class CityzenMssqlGetData : ICityzenDataGetter
    {
        private readonly CityzenDbContext _dbContext;
        private readonly ILogger<CityzenMssqlGetData> _logger;
        public CityzenMssqlGetData(CityzenDbContext dbContext, ILogger<CityzenMssqlGetData> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<Cityzen>> GetByBirthDate(DateTime BirthDate)
        {
            return await _dbContext.Cityzens
                .Where(x => x.BirthDate == BirthDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cityzen>> GetByDeathDate(DateTime? DeathDate)
        {
            return await _dbContext.Cityzens
                .Where(x => x.DeathDate == DeathDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cityzen>> GetByDTO(GetCityzenDTO Input)
        {
            IQueryable<Cityzen> DbQuery = _dbContext.Cityzens;
            if (Input.Inn != null)
                DbQuery = DbQuery.Where(x => x.INN == Input.Inn);
            if (Input.Snils != null)
                DbQuery = DbQuery.Where(x => x.SNILS == Input.Snils);
            if (Input.FullName != null)
                DbQuery = DbQuery.Where(x => x.FullName == Input.FullName);
            if (Input.BirthDate != null)
                DbQuery = DbQuery.Where(x => x.BirthDate == Input.BirthDate);
            if (Input.DeathDate != null)
            {
                if (Input.IsDead.HasValue && Input.IsDead.Value)
                    DbQuery = DbQuery.Where(x => x.DeathDate == Input.DeathDate);
            }
            else if (Input.IsDead.HasValue && !Input.IsDead.Value)
                DbQuery = DbQuery.Where(x => x.DeathDate == null);

            return await DbQuery.ToListAsync();
        }

        public async Task<IEnumerable<Cityzen>> GetByFullName(string FullName)
        {
            return await _dbContext.Cityzens
                .Where(x => x.FullName == FullName)
                .ToListAsync();
        }

        public async Task<Cityzen?> GetByINN(string INN)
        {
            return await _dbContext.Cityzens
                .Where(x => x.INN == INN)
                .FirstOrDefaultAsync();
        }

        public async Task<Cityzen?> GetBySNILS(string SNILS)
        {
            return await _dbContext.Cityzens
                .Where(x => x.SNILS == SNILS)
                .FirstOrDefaultAsync();
        }
    }

    public class CityzenMssqlSetData : ICityzenDataSetter
    {
        private readonly CityzenDbContext _dbContext;
        private readonly ILogger<CityzenMssqlSetData> _logger;
        public CityzenMssqlSetData(CityzenDbContext dbContext, ILogger<CityzenMssqlSetData> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<ISetterResponse> Insert(Cityzen NewCityzen)
        {
            _dbContext.Cityzens.Add(NewCityzen);
            string Message;
            bool Success = false;
            try
            {
                await _dbContext.SaveChangesAsync();
                Message = "Succesfully inserted new cityzen";
                Success = true;
            }
            catch (Exception InsExc)
            {
                _logger.LogError($"Unable to insert new cityzen:\r\n{(InsExc.InnerException != null ? InsExc.InnerException.ToString() : InsExc.ToString())}");
                Message = "An error occured while inserting a new cityzen";
            }

            return new CityzenMssqlResponse(Message, Success);
        }

        public async Task<ISetterResponse> Update(Cityzen UpdCityzen)
        {
            string Message;
            bool Success = false;
            try
            {
                var OldCityzen = await _dbContext.Cityzens.Where(x => x.Id == UpdCityzen.Id).FirstAsync();
                OldCityzen.Update(UpdCityzen);
                await _dbContext.SaveChangesAsync();
                Message = "Succesfully updated existing cityzen";
                Success = true;
            }
            catch (Exception UpdExc)
            {
                _logger.LogError($"Unable to update existing cityzen:\r\n{(UpdExc.InnerException != null ? UpdExc.InnerException.ToString() : UpdExc.ToString())}");
                Message = "An error occured while updating existing cityzen";
            }

            return new CityzenMssqlResponse(Message, Success);
        }

        public async Task<ISetterResponse> Delete(long Id)
        {
            string Message;
            bool Success = false;
            try
            {
                var OldCityzen = await _dbContext.Cityzens.Where(x => x.Id == Id).FirstAsync();
                _dbContext.Remove(OldCityzen);
                await _dbContext.SaveChangesAsync();
                Message = "Succesfully deleted existing cityzen";
                Success = true;
            }
            catch (Exception UpdExc)
            {
                _logger.LogError($"Unable to delete existing cityzen:\r\n{(UpdExc.InnerException != null ? UpdExc.InnerException.ToString() : UpdExc.ToString())}");
                Message = "An error occured while deleting existing cityzen";
            }

            return new CityzenMssqlResponse(Message, Success);
        }
    }

    public struct CityzenMssqlResponse : ISetterResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }

        public CityzenMssqlResponse(string Msg, bool Result)
        { 
            Message = Msg;
            Success = Result; 
        }
    }
}
