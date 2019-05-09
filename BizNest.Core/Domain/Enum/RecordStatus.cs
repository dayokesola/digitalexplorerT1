using System;
using System.Collections.Generic;
using System.Text;

namespace BizNest.Core.Data.Enum
{
    /// <summary>
    /// Status of a record in the database
    /// </summary>
    public enum RecordStatus
    {
        /// <summary>
        /// Pending state
        /// </summary>
        Pending,
        /// <summary>
        /// Flagged as active
        /// </summary>
        Active,
        /// <summary>
        /// Flagged as inactive
        /// </summary>
        Inactive,
        /// <summary>
        /// Soft deleted
        /// </summary>
        Deleted,
        /// <summary>
        /// Archivere can delete and archive record
        /// </summary>
        Archive
    }
}
