using Repositories.Models;

namespace Repositories
{
	public sealed class UnitOfWork
	{
		private readonly PRO192_Auto_GraderContext context;


		public UnitOfWork(PRO192_Auto_GraderContext context)
		{
			this.context = context;
		}
	}
}
