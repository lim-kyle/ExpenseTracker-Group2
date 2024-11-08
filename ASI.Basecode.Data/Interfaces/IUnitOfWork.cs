﻿using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;

namespace ASI.Basecode.Data.Interfaces
{
    /// <summary>
    /// Unit of Work Interface
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the database context
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        DbContext Database { get; }
        /// <summary>
        /// Saves the changes to database
        /// </summary>
        void SaveChanges();
        Task SaveChangesAsync(CancellationToken ct);

    }
}
