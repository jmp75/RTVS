﻿using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Languages.Core.Text;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using Microsoft.VisualStudio.Text.Projection;
using Microsoft.VisualStudio.Utilities;

namespace Microsoft.VisualStudio.Editor.Mocks
{
    [ExcludeFromCodeCoverage]
    public sealed class TextViewMock : ITextView
    {
        public TextViewMock(ITextBuffer textBuffer)
        {
            TextBuffer = textBuffer;
            Selection = new TextSelectionMock(this, new TextRange(0, 0));
            TextDataModel = new TextDataModelMock(TextBuffer);
            TextViewModel = new TextViewModelMock(textBuffer);
        }

        public TextViewMock(ITextBuffer textBuffer, int caretPosition) :
            this(textBuffer)
        {
            Caret = new TextCaretMock(this, caretPosition);
            Selection = new TextSelectionMock(this, new TextRange(caretPosition, 0));
        }

        public IBufferGraph BufferGraph
        {
            get { return null; }
        }

        public ITextCaret Caret { get; private set; }

        public bool HasAggregateFocus
        {
            get { return true; }
        }

        public bool InLayout
        {
            get { return false; }
        }

        public bool IsClosed
        {
            get { return false; }
        }

        public bool IsMouseOverViewOrAdornments
        {
            get { return true; }
        }

        public double LineHeight
        {
            get { return 12; }
        }

        public double MaxTextRightCoordinate
        {
            get { return 100; }
        }

        public IEditorOptions Options
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public PropertyCollection Properties { get; private set; } = new PropertyCollection();

        public ITrackingSpan ProvisionalTextHighlight
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public ITextViewRoleSet Roles
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ITextSelection Selection { get; private set; }

        public ITextBuffer TextBuffer { get; private set; }

        public ITextDataModel TextDataModel { get; private set; }

        public ITextSnapshot TextSnapshot
        {
            get { return TextBuffer.CurrentSnapshot; }
        }

        public ITextViewLineCollection TextViewLines
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ITextViewModel TextViewModel { get; private set; }

        public double ViewportBottom
        {
            get { return 100; }
        }

        public double ViewportHeight
        {
            get { return 100; }
        }

        public double ViewportLeft { get; set; }

        public double ViewportRight { get; set; }

        public double ViewportTop
        {
            get { return 0; }
        }

        public double ViewportWidth
        {
            get { return 100; }
        }

        public IViewScroller ViewScroller
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ITextSnapshot VisualSnapshot
        {
            get { return this.TextSnapshot; }
        }

        public void Close()
        {
        }

        public void DisplayTextLineContainingBufferPosition(SnapshotPoint bufferPosition, double verticalDistance, ViewRelativePosition relativeTo)
        {
        }

        public void DisplayTextLineContainingBufferPosition(SnapshotPoint bufferPosition, double verticalDistance, ViewRelativePosition relativeTo, double? viewportWidthOverride, double? viewportHeightOverride)
        {
        }

        public SnapshotSpan GetTextElementSpan(SnapshotPoint point)
        {
            return new SnapshotSpan(this.TextSnapshot, point, 0);
        }

        public ITextViewLine GetTextViewLineContainingBufferPosition(SnapshotPoint bufferPosition)
        {
            throw new NotImplementedException();
        }

        public void QueueSpaceReservationStackRefresh()
        {
        }

#pragma warning disable 0067
        public event EventHandler Closed;
        public event EventHandler GotAggregateFocus;
        public event EventHandler<TextViewLayoutChangedEventArgs> LayoutChanged;
        public event EventHandler LostAggregateFocus;
        public event EventHandler<MouseHoverEventArgs> MouseHover;
        public event EventHandler ViewportHeightChanged;
        public event EventHandler ViewportLeftChanged;
        public event EventHandler ViewportWidthChanged;
    }
}