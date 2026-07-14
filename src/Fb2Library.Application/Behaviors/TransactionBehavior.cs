using MediatR;
using Fb2Library.Domain.Shared.Interfaces;

namespace Fb2Library.Application.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionBehavior(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                TResponse? response = await next().ConfigureAwait(false);
                await _unitOfWork.CommitTransactionAsync().ConfigureAwait(false);
                return response;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync().ConfigureAwait(false);
                throw;
            }
        }
    }
}
