//using AutoMapper;
//using Customer.BusinessLogic.Database.Entities;
//using Customer.BusinessLogic.Models;
//using Customer.BusinessLogic.Models.Base;
//using Customer.DatabaseLogic.Repositories;
//using Microsoft.Extensions.Logging;

//namespace Customer.BusinessLogic.Services
//{
//    public abstract class BaseService<
//         TEntity,
//         TGenericRepository,
//         TCreateRequest,
//         TReadRequest,
//         TUpdateRequest,
//         TDeleteRequest,
//         TBaseResponse,
//         TReadResponse
//         > : IBaseService<
//             TReadRequest,
//             TReadResponse,
//             TCreateRequest,
//             TUpdateRequest,
//             TDeleteRequest,
//             TBaseResponse>
//         where TCreateRequest : BaseCreateRequest, new()
//         where TReadRequest : BaseReadRequest, new()
//         where TUpdateRequest : BaseUpdateRequest, new()
//         where TDeleteRequest : BaseDeleteRequest, new()
//         where TBaseResponse : BaseResponse, new()
//         where TReadResponse : BaseReadResponse, new()
//         where TGenericRepository : BaseRepository<TEntity>, new()
//         where TEntity : class, IBaseEntity
//    {
//        private readonly ILogger<BaseService<
//            TEntity,
//            TGenericRepository,
//            TCreateRequest,
//            TReadRequest,
//            TUpdateRequest,
//            TDeleteRequest,
//            TBaseResponse,
//            TReadResponse
//        >>? _logger;

//        private readonly IRepository<TEntity>? _repository;
//        internal readonly IMapper? _mapper;

//        //public BaseService()
//        //{
//        //    if (ServiceLocatorHelper.ServiceProvider == null)
//        //    {
//        //        _logger?.LogError("ServiceLocatorHelper.ServiceProvider is not initialized");
//        //        throw new Exception("ServiceLocatorHelper.ServiceProvider is not initialized");
//        //    }
//        //    _mapper = ServiceLocatorHelper.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
//        //    _repository = ServiceLocatorHelper.ServiceProvider.GetService(typeof(TGenericRepository)) as TGenericRepository;
//        //}

//        public BaseService(IMapper mapper, IRepository<TEntity> repository)
//        {
//            _mapper = mapper;
//            _repository = repository;
//        }

//        //public BaseService(TGenericRepository repository, IMapper mapper)
//        //{
//        //    if (ServiceLocatorHelper.ServiceProvider == null)
//        //    {
//        //        _logger?.LogError("ServiceLocatorHelper.ServiceProvider is not initialized");
//        //        throw new Exception("ServiceLocatorHelper.ServiceProvider is not initialized");
//        //    }
//        //    _mapper = ServiceLocatorHelper.ServiceProvider.GetService(typeof(IMapper)) as IMapper;

//        //    _mapper = mapper;
//        //    if (_mapper == null)
//        //    {
//        //        throw new Exception("Mapper is not initialized");
//        //    }

//        //    //_repository = ServiceLocatorHelper.ServiceProvider.GetService(typeof(TRepository)) as TRepository;
//        //    _repository = repository;
//        //    if (_repository == null)
//        //    {
//        //        throw new Exception($"Repository for '{typeof(TGenericRepository)}' is not found.");
//        //    }

//        //    //_logger = ServiceLocatorHelper.ServiceProvider.GetService(typeof(ILogger<BaseService<
//        //    //    TEntity,
//        //    //    TRepository,
//        //    //    TCreateUpdateRequest,
//        //    //    TBaseResponse,
//        //    //    TReadRequest,
//        //    //    TReadResponse,
//        //    //    TDeleteRequest,
//        //    //    TDbContext
//        //    //>>)) as ILogger<BaseService<
//        //    //    TEntity,
//        //    //    TRepository,
//        //    //    TCreateUpdateRequest,
//        //    //    TBaseResponse,
//        //    //    TReadRequest,
//        //    //    TReadResponse,
//        //    //    TDeleteRequest,
//        //    //    TDbContext
//        //    //>> ?? throw new Exception("Logger is not initialized");
//        //}

//        //public BaseService(
//        //    ILogger<BaseService<
//        //    TEntity,
//        //    TRepository,
//        //    TCreateUpdateRequest,
//        //    TBaseResponse,
//        //    TReadRequest,
//        //    TReadResponse,
//        //    TDeleteRequest,
//        //        TDbContext
//        //    >> logger,
//        //    IMapper mapper
//        //)
//        //{
//        //    _mapper = mapper;
//        //    _logger = logger;

//        //    //if (ServiceLocatorHelper.ServiceProvider == null)
//        //    //{
//        //    //    _logger?.LogError("ServiceLocatorHelper.ServiceProvider is not initialized");
//        //    //    throw new Exception("ServiceLocatorHelper.ServiceProvider is not initialized");
//        //    //}
//        //    //_repository = ServiceLocatorHelper.ServiceProvider.GetService(typeof(TRepository)) as TRepository;
//        //    //if (_repository == null)
//        //    //{
//        //    //    throw new Exception($"Repository for '{typeof(TRepository)}' is not initialized");
//        //    //}
//        //}

//        public IRepository<TEntity> GetRepository()
//        {
//            return _repository;
//            //var repository = ServiceLocatorHelper
//            //    .ServiceProvider?
//            //    .GetService(typeof(TGenericRepository)) as TGenericRepository;

//            //if (repository is null)
//            //{
//            //    throw new Exception($"Repository for '{typeof(TGenericRepository)}' is not initialized");
//            //}
//            //return repository;
//        }

//        public virtual async Task<Result<TReadResponse>> GetByIdAsync(
//            TReadRequest request)
//        {
//            Result<TReadResponse> result = new();
//            try
//            {
//                if (_mapper == null)
//                {
//                    _logger?.LogError("Mapper is not initialized");
//                    throw new Exception("Mapper is not initialized");
//                }

//                //var repository = BaseService<TEntity, TRepository, TCreateUpdateRequest, TBaseResponse, TReadRequest, TReadResponse, TDeleteRequest, TDbContext>.GetRepository();
//                var repository = GetRepository();
//                TEntity entity = await repository.GetByIdAsync(request.Id);
//                TReadResponse entityResponse = _mapper.Map<TReadResponse>(entity);
//                result.Data = entityResponse;
//            }
//            catch (Exception exc)
//            {
//                result.AddError(exc);
//            }
//            return result;
//        }

//        public IQueryable<TReadResponse> ToReadResponse(IQueryable<TEntity> entities)
//        {
//            if (_mapper == null)
//            {
//                throw new Exception("Mapper is not initialized");
//            }

//            List<TReadResponse> items = [];
//            foreach (var entity in entities)
//            {
//                TReadResponse entityResponse = _mapper.Map<TReadResponse>(entity);
//                items.Add(entityResponse);
//            }
//            return items.AsQueryable();
//        }

//        public TReadResponse ToReadResponse(TEntity entity)
//        {
//            if (_mapper == null)
//            {
//                throw new Exception("Mapper is not initialized");
//            }

//            TReadResponse entityResponse = _mapper.Map<TReadResponse>(entity);
//            return entityResponse;
//        }

//        public virtual async Task<Result<IQueryable<TReadResponse>>> GetAllAsync()
//        {
//            Result<IQueryable<TReadResponse>> result = new();
//            try
//            {
//                var repository = GetRepository();
//                IQueryable<TEntity> entities = await repository.GetAllAsync();
//                result.Data = ToReadResponse(entities);
//            }
//            catch (Exception exc)
//            {
//                result.AddError(exc);
//            }
//            return result;
//        }

//        //public virtual async Task<Result<IQueryable<TReadResponse>>> GetAllByFilterAsync(Expression<Func<TEntity, bool>> expression)
//        //{
//        //    Result<IQueryable<TReadResponse>> result = new();
//        //    try
//        //    {
//        //        //IQueryable<TEntity> entities = await BaseService<TEntity, TRepository, TCreateUpdateRequest, TBaseResponse, TReadRequest, TReadResponse, TDeleteRequest, TDbContext>.GetRepository().GetAllByFilterAsync(expression);
//        //        IQueryable<TEntity> entities = await GetRepository().GetAllByFilterAsync(expression);
//        //        result.Data = ToReadResponse(entities);
//        //    }
//        //    catch (Exception exc)
//        //    {
//        //        result.AddError(exc);
//        //    }
//        //    return result;
//        //}

//        public virtual async Task<Result<TBaseResponse>> DeleteByIdAsync(TDeleteRequest request)
//        {
//            Result<TBaseResponse> result = new();
//            try
//            {
//                TBaseResponse response = new()
//                {
//                    //Id = await BaseService<TEntity, TRepository, TCreateUpdateRequest, TBaseResponse, TReadRequest, TReadResponse, TDeleteRequest, TDbContext>.GetRepository().DeleteAsync(request.Id)
//                    Id = await GetRepository().DeleteAsync(request.Id)

//                };
//                if (response.Id == Guid.Empty)
//                {
//                    result.AddError("Delete Item not found");
//                }
//                result.Data = response;
//            }
//            catch (Exception exc)
//            {
//                result.AddError(exc);
//            }
//            return result;
//        }

//        public virtual async Task<Result<TBaseResponse>> UpdateAsync(TUpdateRequest request)
//        {
//            Result<TBaseResponse> result = new();
//            try
//            {
//                if (_mapper == null)
//                {
//                    throw new Exception("Mapper is not initialized");
//                }

//                TEntity entity = _mapper.Map<TEntity>(request);
//                //TEntity updateResult = await BaseService<TEntity, TRepository, TCreateUpdateRequest, TBaseResponse, TReadRequest, TReadResponse, TDeleteRequest, TDbContext>.GetRepository().UpdateAsync(entity);
//                TEntity updateResult = await GetRepository().UpdateAsync(entity);
//                TBaseResponse response = new()
//                {
//                    Id = updateResult.Id
//                };
//                result.Data = response;
//            }
//            catch (Exception exc)
//            {
//                result.AddError(exc);
//            }
//            return result;
//        }

//        public virtual async Task<Result<TBaseResponse>> CreateAsync(TCreateRequest request)
//        {
//            Result<TBaseResponse> result = new();
//            try
//            {
//                if (_mapper == null)
//                {
//                    throw new Exception("Mapper is not initialized");
//                }

//                TEntity newEntity = _mapper.Map<TEntity>(request);
//                TEntity newCreatedEntity = await GetRepository().AddAsync(newEntity);
//                TBaseResponse createResponse = _mapper.Map<TBaseResponse>(newCreatedEntity);
//                result.Data = createResponse;
//            }
//            catch (Exception exc)
//            {
//                result.AddError(exc);
//            }
//            return result;
//        }

//        public virtual async Task<Result<TBaseResponse>> CreateRangeAsync(IEnumerable<TCreateRequest> requests)
//        {
//            Result<TBaseResponse> result = new();
//            try
//            {
//                if (_mapper == null)
//                {
//                    throw new Exception("Mapper is not initialized");
//                }

//                IEnumerable<TEntity> newEntities = requests.Select(r => _mapper.Map<TEntity>(r));
//                var success = await GetRepository().AddRangeAsync(newEntities);

//                TBaseResponse response = new()
//                {
//                    Id = Guid.Empty
//                };
//                result.Data = response;
//            }
//            catch (Exception exc)
//            {
//                result.AddError(exc);
//            }
//            return result;
//        }
//    }
//}

