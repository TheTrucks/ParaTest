namespace ParaTest.Models.Cityzen
{
    public interface ICityzenSerializer
    {
        Task<IEnumerable<Cityzen>> Deserialize(Stream Input);
        Task<Stream> Serialize(IEnumerable<Cityzen> Input);
    }
    public class BasicCityzenSerializer : ICityzenSerializer
    {
        public Task<IEnumerable<Cityzen>> Deserialize(Stream Input)
        {
            throw new NotImplementedException();
        }

        public async Task<Stream> Serialize(IEnumerable<Cityzen> Input)
        {
            MemoryStream Result = new();
            using (StreamWriter SW = new(Result, System.Text.Encoding.UTF8))
            {
                foreach (var EachCityzen in Input)
                    await SW.WriteLineAsync(EachCityzen.ToString());
            }
            return Result;
        }
    }
}
