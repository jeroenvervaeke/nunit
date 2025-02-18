// Copyright (c) Charlie Poole, Rob Prouse and Contributors. MIT License - see LICENSE.txt

namespace NUnit.Framework.Constraints
{
    /// <summary>
    /// ConstraintStatus represents the status of a ConstraintResult
    /// returned by a Constraint being applied to an actual value.
    /// </summary>
    public enum ConstraintStatus
    {
        /// <summary>
        /// The status has not yet been set
        /// </summary>
        Unknown,

        /// <summary>
        /// The constraint succeeded
        /// </summary>
        Success,

        /// <summary>
        /// The constraint failed
        /// </summary>
        Failure,

        /// <summary>
        /// An error occurred in applying the constraint (reserved for future use)
        /// </summary>
        Error
    }

    /// <summary>
    /// Contains the result of matching a <see cref="Constraint"/> against an actual value.
    /// </summary>
    public class ConstraintResult
    {
        readonly IConstraint _constraint;

        #region Constructors

        /// <summary>
        /// Constructs a <see cref="ConstraintResult"/> for a particular <see cref="Constraint"/>.
        /// </summary>
        /// <param name="constraint">The Constraint to which this result applies.</param>
        /// <param name="actualValue">The actual value to which the Constraint was applied.</param>
        public ConstraintResult(IConstraint constraint, object actualValue)
        {
            _constraint = constraint;
            ActualValue = actualValue;
        }

        /// <summary>
        /// Constructs a <see cref="ConstraintResult"/> for a particular <see cref="Constraint"/>.
        /// </summary>
        /// <param name="constraint">The Constraint to which this result applies.</param>
        /// <param name="actualValue">The actual value to which the Constraint was applied.</param>
        /// <param name="status">The status of the new ConstraintResult.</param>
        public ConstraintResult(IConstraint constraint, object actualValue, ConstraintStatus status)
            : this(constraint, actualValue)
        {
            Status = status;
        }

        /// <summary>
        /// Constructs a <see cref="ConstraintResult"/> for a particular <see cref="Constraint"/>.
        /// </summary>
        /// <param name="constraint">The Constraint to which this result applies.</param>
        /// <param name="actualValue">The actual value to which the Constraint was applied.</param>
        /// <param name="isSuccess">If true, applies a status of Success to the result, otherwise Failure.</param>
        public ConstraintResult(IConstraint constraint, object actualValue, bool isSuccess)
            : this(constraint, actualValue)
        {
            Status = isSuccess ? ConstraintStatus.Success : ConstraintStatus.Failure;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The actual value that was passed to the <see cref="Constraint.ApplyTo{TActual}(TActual)"/> method.
        /// </summary>
        public object ActualValue { get; }

        /// <summary>
        /// Gets and sets the ResultStatus for this result.
        /// </summary>
        public ConstraintStatus Status { get; set; }

        /// <summary>
        /// True if actual value meets the Constraint criteria otherwise false.
        /// </summary>
        public virtual bool IsSuccess => Status == ConstraintStatus.Success;

        /// <summary>
        /// Display friendly name of the constraint.
        /// </summary>
        public string Name => _constraint.DisplayName;

        /// <summary>
        /// Description of the constraint may be affected by the state the constraint had
        /// when <see cref="Constraint.ApplyTo{TActual}(TActual)"/> was performed against the actual value.
        /// </summary>
        public string Description => _constraint.Description;

        #endregion

        #region Write Methods

        /// <summary>
        /// Write the failure message to the MessageWriter provided
        /// as an argument. The default implementation simply passes
        /// the result and the actual value to the writer, which
        /// then displays the constraint description and the value.
        ///
        /// Constraints that need to provide additional details,
        /// such as where the error occurred, can override this.
        /// </summary>
        /// <param name="writer">The MessageWriter on which to display the message</param>
        public virtual void WriteMessageTo(MessageWriter writer)
        {
            writer.DisplayDifferences(this);
        }

        /// <summary>
        /// Write some additional failure message.
        /// </summary>
        /// <param name="writer">The MessageWriter on which to display the message</param>
        public virtual void WriteAdditionalLinesTo(MessageWriter writer)
        {
            //By default it does not write anything to writer but can be overriden in classes where needed.
        }

        /// <summary>
        /// Write the actual value for a failing constraint test to a
        /// MessageWriter. The default implementation simply writes
        /// the raw value of actual, leaving it to the writer to
        /// perform any formatting.
        /// </summary>
        /// <param name="writer">The writer on which the actual value is displayed</param>
        public virtual void WriteActualValueTo(MessageWriter writer)
        {
            writer.WriteActualValue(ActualValue);
        }

        #endregion
    }
}
