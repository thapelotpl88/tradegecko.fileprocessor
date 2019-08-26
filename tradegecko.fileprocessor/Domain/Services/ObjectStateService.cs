using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using tradegecko.fileprocessor.Domain.Entities;

namespace tradegecko.fileprocessor.Domain.Services
{
	public class ObjectStateService : IObjectStateService
	{
		private readonly TradegeckoDbContext _tradegeckoDbContext;

		public ObjectStateService(TradegeckoDbContext tradegeckoDbContext)
		{
			_tradegeckoDbContext = tradegeckoDbContext;
		}

		public async Task AddObjectTransactionsAsync(IEnumerable<ObjectTransaction> objectTransactions)
		{
			await _tradegeckoDbContext.ObjectTransaction.AddRangeAsync(objectTransactions);
			await _tradegeckoDbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<ObjectTransaction>> GetObjectTransaction(Expression<Func<ObjectTransaction, bool>> filter)
		{
			return  await _tradegeckoDbContext.ObjectTransaction.Where(filter).ToListAsync();
		}

	}
}
