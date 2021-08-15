using System;

namespace Flamingo.Fishes.Awaitables.FillFormHelper
{
    /// <summary>
    /// Carries a recently changed status is a for
    /// </summary>
    /// <remarks>When user answered or when waiting for an answer
    /// is timedout or any other changes in asking process</remarks>
    public enum FlamingoFormStatus
    {
        /// <summary>
        /// User answered a question and all is Ok.
        /// </summary>
        Answered = 0,

        /// <summary>
        /// User cancelled asking process.
        /// </summary>
        Cancelled = 1,

        /// <summary>
        /// Asking for a question is timed out.
        /// </summary>
        TimedOut = 2,

        /// <summary>
        /// Validation failed for a user input
        /// </summary>
        ValidatingFailed = 3,

        /// <summary>
        /// Asking is cancell
        /// But property is not required and can be skipped.
        /// </summary>
        RecoverableCancelled = 4,

        /// <summary>
        /// Asking is timedout
        /// But property is not required and can be skipped.
        /// </summary>
        RecoverableTimedOut = 5,

        /// <summary>
        /// Asking is fail in validation
        /// But property is not required and can be skipped.
        /// </summary>
        RecoverableFailure = 6,

    }
}
