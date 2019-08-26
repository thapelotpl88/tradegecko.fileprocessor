using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using tradegecko.fileprocessor.Domain.Entities;

namespace tradegecko.fileprocessor.Domain.Services
{
	public interface IObjectStateService
	{
		Task<IEnumerable<ObjectTransaction>> GetObjectTransaction(Expression<Func<ObjectTransaction, bool>> filter);
		Task AddObjectTransactionsAsync(IEnumerable<ObjectTransaction> objectTransactions);
	}
}
