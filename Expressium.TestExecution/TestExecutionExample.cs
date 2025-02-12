﻿using System;
using System.Collections.Generic;

namespace Expressium.TestExecution
{
    public class TestExecutionExample
    {
        public string Status { get; set; }
        public string Error { get; set; }
        public string Stacktrace { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public List<TestExecutionArgument> Arguments { get; set; }
        public List<TestExecutionStep> Steps { get; set; }
        public List<string> Attachments { get; set; }

        public TestExecutionExample()
        {
            Arguments = new List<TestExecutionArgument>();
            Steps = new List<TestExecutionStep>();
            Attachments = new List<string>();
        }
    }
}
