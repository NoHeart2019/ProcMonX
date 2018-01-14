﻿using Microsoft.Diagnostics.Tracing;
using ProcMonX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcMonX.ViewModels {
    public sealed class TraceEventDataViewModel {
        static int _globalIndex;
        public int Index { get; }
        public DateTime TimeStamp => Data.TimeStamp;

        public TraceEvent Data { get; }
        public EventType Type { get; }
        public string TypeAsString { get; }
        public EventCategory Category { get; }
        public string Opcode => Data.OpcodeName;

        public int ProcessId => Data.ProcessID;
        public string ProcessName { get; }
        public int? ThreadId => Data.ThreadID < 0 ? (int?)null : Data.ThreadID;
        public int Processor => Data.ProcessorNumber;

        public string Details { get; }

        internal TraceEventDataViewModel(TraceEvent evt, EventType type, string details = null) {
            Data = evt;
            Type = type;
            var info = EventInfo.AllEventsByType[type];
            ProcessName = string.Intern(evt.ProcessName);
            TypeAsString = info.AsString ?? type.ToString();
            Index = Interlocked.Increment(ref _globalIndex);
            Category = info.Category;
            Details = details ?? string.Empty;
        }
    }
}