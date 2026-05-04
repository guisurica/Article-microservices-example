using SharedService.Returns;

namespace ArticleService.Application.Contracts.Usecases;

public interface  IUseCase<TReturn, TParam>
{
    Task<Result<TReturn>> HandleAsync(TParam param);
}
