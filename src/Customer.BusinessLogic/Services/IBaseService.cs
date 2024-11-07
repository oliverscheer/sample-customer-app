//using Customer.BusinessLogic.Models;
//using Customer.BusinessLogic.Models.Base;

//namespace Customer.BusinessLogic.Services
//{
//    internal interface IBaseService<
//        TReadRequest, 
//        TReadResponse,
//        TCreateRequest,
//        TUpdateRequest,
//        TDeleteRequest,
//        TBaseResponse>
//        where TReadRequest : BaseReadRequest, new()
//        where TReadResponse : BaseReadResponse, new()
//        where TCreateRequest : BaseCreateRequest, new()
//        where TUpdateRequest : BaseUpdateRequest, new()
//        where TBaseResponse : BaseResponse, new()
//        where TDeleteRequest : BaseDeleteRequest, new()
//    {
//        Task<Result<IQueryable<TReadResponse>>> GetAllAsync();
//        Task<Result<TReadResponse>> GetByIdAsync(TReadRequest request);
//        Task<Result<TBaseResponse>> CreateAsync(TCreateRequest request);
//        Task<Result<TBaseResponse>> UpdateAsync(TUpdateRequest request);
//        Task<Result<TBaseResponse>> DeleteByIdAsync(TDeleteRequest request);
//    }
//}
