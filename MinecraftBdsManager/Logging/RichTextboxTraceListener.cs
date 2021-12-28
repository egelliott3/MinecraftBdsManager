using System.Diagnostics;

namespace MinecraftBdsManager.Logging
{
    internal class RichTextboxTraceListener : TraceListener
    {
        /// <summary>
        /// Built in formatting for messages based on their lead in.  List is processed in declared order so ensure to have them in priority order as the first one matched "wins"
        /// </summary>
        private static readonly List<RichTextboxMessageFormatting> _messageFormatting = new List<RichTextboxMessageFormatting>
        {
            new RichTextboxMessageFormatting{ TextToMatch = LoggingLeadIn.UserSentMessage, Color = Color.CornflowerBlue, FontStyle = FontStyle.Bold },
            new RichTextboxMessageFormatting{ TextToMatch = LoggingLeadIn.SystemChatMessage, Color = Color.Orange, FontStyle = FontStyle.Bold },
            new RichTextboxMessageFormatting{ TextToMatch = LoggingLeadIn.SystemErrorMessage, Color = Color.DarkRed, FontStyle = FontStyle.Bold },
            new RichTextboxMessageFormatting{ TextToMatch = LoggingLeadIn.SystemWarningMessage, Color = Color.DarkGoldenrod, FontStyle = FontStyle.Bold },
            new RichTextboxMessageFormatting{ TextToMatch = LoggingLeadIn.SystemInfoMessage, Color = Color.Green, FontStyle = FontStyle.Bold },           
        };

        private readonly RichTextBox _listenerTarget;

        public RichTextboxTraceListener(RichTextBox target, string? listenerName = default) : base(listenerName)
        {
            _listenerTarget = target;
        }

        private void FormatMessage(string message)
        {
            // For each of the formatting options available...
            foreach (var formatting in _messageFormatting)
            {
                // ... see if any match and apply the formatting if they do
                if (message.Contains(formatting.TextToMatch))
                {
                    _listenerTarget.SelectionStart = _listenerTarget.TextLength;
                    _listenerTarget.SelectionLength = 0;
                    _listenerTarget.SelectionColor = formatting.Color;
                    _listenerTarget.SelectionFont = new Font(_listenerTarget.Font, formatting.FontStyle);

                    // Formatting has been applied so quit out
                    break;
                }
            }
        }

        public override void Write(string? message)
        {
            // NOOP for now as this is only adding extra noise to the UI status screen that I would like to keep out.
            //
            //  This data is still going to the file logs, if enabled
            //

            //if (string.IsNullOrWhiteSpace(message))
            //{
            //    return;
            //}

            ////WriteSafe(message.Replace(nameof(MinecraftBdsManager), ""));
        }

        public override void WriteLine(string? message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            WriteSafe(message + Environment.NewLine);
        }

        /// <summary>
        /// Write in a thread safe manner to avoid conflicts with the UI thread(s).
        /// </summary>
        /// <param name="message">The message to write.</param>
        private void WriteSafe(string message)
        {
            if (_listenerTarget.InvokeRequired)
            {
                void safeWrite() { WriteSafe(message); }
                _listenerTarget.BeginInvoke(safeWrite);
            }
            else
            {
                var defaultSelectionColor = _listenerTarget.SelectionColor;

                FormatMessage(message);

                _listenerTarget.AppendText(message);
            }
        }
    }
}
