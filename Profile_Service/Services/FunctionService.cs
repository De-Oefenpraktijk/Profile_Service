using AutoMapper;
using MongoDB.Driver;
using Profile_Service.DTO;
using Profile_Service.Entities;

namespace Profile_Service.Services
{
    public class FunctionService
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;

        public FunctionService(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FunctionDTO>> GetAll()
        {
            var result = await _context.Function.Find(_ => true).ToListAsync();
            var list = new List<FunctionDTO>();
            foreach (var i in result)
            {
                var function = _mapper.Map<FunctionDTO>(i);
                list.Add(function);
            }

            return list;
        }

        public async Task<Function> AddFunction(AddFunctionDTO functionDTO)
        {
            var function = _mapper.Map<Function>(functionDTO);
            await _context.Function.InsertOneAsync(function);
            return function;
        }

        public async Task<string> DeleteFunction(string id)
        {
            var result = await _context.Function.DeleteOneAsync(x => x.Id == id);
            if (result.DeletedCount >= 1)
            {
                return id;
            }
            return null;
        }
    }
}
