using FableMod.CLRCore;

namespace ChocolateBox
{
    /// <summary>
    /// Provides progress tracking for long-running operations.
    /// Extends ProgressInterface from FableMod.CLRCore to provide standardized progress reporting.
    ///
    /// Usage:
    /// <code>
    /// Progress progress = new Progress();
    /// progress.Begin(100); // Total steps
    /// progress.Info = "Processing files...";
    /// progress.StepInfo = "Step 1 of 100";
    /// progress.Update(); // Increment progress
    /// progress.End();
    /// </code>
    /// </summary>
    public class Progress : ProgressInterface
    {
        private string myInfo;
        private string myStepInfo;
        private float myValue;

        /// <summary>
        /// Sets the current progress value (0.0 to 1.0 representing 0% to 100%).
        /// This method is called internally by the ProgressInterface base class.
        /// </summary>
        /// <param name="value">Progress value between 0.0 and 1.0</param>
        protected override void SetValue(float value)
        {
            myValue = value;
        }

        /// <summary>
        /// Gets or sets the main progress information message.
        /// This typically describes the overall operation being performed.
        /// Example: "Exporting textures..." or "Loading BIG file..."
        /// </summary>
        public string Info
        {
            get => myInfo;
            set => myInfo = value;
        }

        /// <summary>
        /// Gets or sets detailed step information for the current operation.
        /// This typically provides more specific details about the current step.
        /// Example: "(Bank 1 of 2: GBANK_MAIN_PC - 6290 entries)"
        /// </summary>
        public string StepInfo
        {
            get => myStepInfo;
            set => myStepInfo = value;
        }

        /// <summary>
        /// Gets or sets the current progress value (0.0 to 1.0 representing 0% to 100%).
        /// Setting this value will update the progress through the base class SetValue method.
        /// </summary>
        public float Value
        {
            get => myValue;
            set => SetValue(value);
        }
    }
}
